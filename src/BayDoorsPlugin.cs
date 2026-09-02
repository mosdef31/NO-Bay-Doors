using System.Collections.Generic;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using UnityEngine;

namespace BayDoors
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInProcess("NuclearOption.exe")]
    public sealed class BayDoorsPlugin : BaseUnityPlugin
    {

        public const string PluginGuid    = "com.baydoors";
        public const string PluginName    = "Bay Doors";
        public const string PluginVersion = "1.0.2";

        public static ManualLogSource Log { get; private set; } = null!;

        private const float RefreshSeconds = 0.25f;

        private ConfigEntry<KeyboardShortcut> _key = null!;
        private ConfigEntry<bool> _hold = null!;

        private bool _open;

        private Aircraft? _cachedFor;
        private BayDoor[] _doors = new BayDoor[0];

        private void Awake()
        {
            Log = Logger;

            _key = Config.Bind(
                "General", "ToggleKey", new KeyboardShortcut(KeyCode.B, KeyCode.LeftAlt),
                "The key that opens and closes the bay doors.");

            _hold = Config.Bind(
                "General", "HoldToKeepOpen", false,
                "Off: press once to open, press again to close. "
                + "On: the doors stay open only while the key is held.");

            Logger.LogInfo($"{PluginName} {PluginVersion} loaded.");
        }

        private void Update()
        {
            KeyboardShortcut key = _key.Value;

            if (_hold.Value)
            {
                _open = IsHeld(key);
            }
            else if (key.IsDown())
            {
                _open = !_open;
            }

            if (!_open) return;

            foreach (BayDoor door in DoorsOfFlownAircraft())
                if (door != null)
                    door.OpenDoor(RefreshSeconds);
        }

        private static bool IsHeld(KeyboardShortcut key)
        {
            if (key.MainKey == KeyCode.None) return false;
            if (!Input.GetKey(key.MainKey)) return false;

            foreach (KeyCode modifier in key.Modifiers)
                if (!Input.GetKey(modifier)) return false;

            return true;
        }

        private IReadOnlyList<BayDoor> DoorsOfFlownAircraft()
        {
            if (!GameManager.GetLocalAircraft(out Aircraft aircraft) || aircraft == null)
            {
                _cachedFor = null;
                _doors = new BayDoor[0];
                return _doors;
            }

            if (!ReferenceEquals(aircraft, _cachedFor))
            {
                _cachedFor = aircraft;
                _doors = WeaponBayDoorsOf(aircraft);
                SilenceSharedDoorAudio(_doors);

                Log.LogInfo(_doors.Length > 0
                    ? $"{aircraft.name}: {_doors.Length} weapon bay door(s) on the key."
                    : $"{aircraft.name} has no weapon bay doors, so the key does nothing here.");
            }

            return _doors;
        }

        private static readonly FieldInfo? DoorAudioField =
            typeof(BayDoor).GetField("doorAudioSource",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        private static void SilenceSharedDoorAudio(BayDoor[] doors)
        {
            if (doors.Length < 2) return;

            if (DoorAudioField == null)
            {
                Log.LogWarning(
                    "BayDoor no longer has a 'doorAudioSource' field, so shared-source door "
                    + "audio cannot be de-conflicted. If the servo sound stutters with several "
                    + "bays open, this is why.");
                return;
            }

            var owners = new List<AudioSource>();
            int silenced = 0;

            foreach (BayDoor door in doors)
            {
                if (door == null) continue;
                if (DoorAudioField.GetValue(door) is not AudioSource source || source == null)
                    continue;

                bool seen = false;
                foreach (AudioSource had in owners)
                    if (ReferenceEquals(had, source)) { seen = true; break; }

                if (!seen)
                {
                    owners.Add(source);
                    continue;
                }

                DoorAudioField.SetValue(door, null);
                silenced++;
            }

            if (silenced > 0)
                Log.LogInfo(
                    $"{silenced} bay door(s) shared an AudioSource with another door and had it "
                    + "cleared, so they animate silently instead of restarting each other's clip "
                    + "every frame. This is the stuttering servo sound.");
            else
                Log.LogInfo(
                    "No two bay doors on this aircraft share an AudioSource, so nothing was "
                    + "silenced.");
        }

        private static BayDoor[] WeaponBayDoorsOf(Aircraft aircraft)
        {
            WeaponManager? weapons = aircraft.weaponManager;
            if (weapons == null || weapons.hardpointSets == null) return new BayDoor[0];

            var found = new List<BayDoor>();

            foreach (HardpointSet set in weapons.hardpointSets)
            {
                if (set == null || set.hardpoints == null) continue;

                foreach (Hardpoint hardpoint in set.hardpoints)
                {
                    if (hardpoint == null || hardpoint.bayDoors == null) continue;

                    foreach (BayDoor door in hardpoint.bayDoors)
                    {
                        if (door == null) continue;
                        if (found.Contains(door)) continue;
                        found.Add(door);
                    }
                }
            }

            return found.ToArray();
        }
    }
}
