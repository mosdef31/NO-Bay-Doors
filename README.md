# Bay Doors

**Open and close your aircraft's weapon bay doors with a key.**

[![Latest release](https://img.shields.io/github/v/release/mosdef31/NO-Bay-Doors?style=for-the-badge&label=download&color=2ea043)](https://github.com/mosdef31/NO-Bay-Doors/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/mosdef31/NO-Bay-Doors/total?style=for-the-badge&color=blue)](https://github.com/mosdef31/NO-Bay-Doors/releases)
[![Game version](https://img.shields.io/badge/Nuclear%20Option-0.34%2B-orange?style=for-the-badge)](https://store.steampowered.com/app/2168680/Nuclear_Option/)
[![Issues](https://img.shields.io/github/issues/mosdef31/NO-Bay-Doors?style=for-the-badge&color=orange)](https://github.com/mosdef31/NO-Bay-Doors/issues)

📥 **[Download](https://github.com/mosdef31/NO-Bay-Doors/releases/latest)** &nbsp;·&nbsp;
📝 **[What's new](./CHANGELOG.md)** &nbsp;·&nbsp;
🐛 **[Report a bug](https://github.com/mosdef31/NO-Bay-Doors/issues)**

---

## What it does

Press the key and the bay doors on the aircraft you are flying swing open. Press
it again and they close. That is all it does.

The doors still work normally on their own. Firing a weapon out of a bay opens
them the way it always did, and holding them open does not change how anything
loads, aims or launches.

## Controls

| Key | Does |
|---|---|
| **Alt+B** | Opens the doors, or closes them if they are open |

You can change the key in the settings below.

## Settings

They are in `BepInEx/config/com.baydoors.cfg`, and you can also edit them in
Configuration Manager if you have it.

| Setting | Default | Does |
|---|---|---|
| `ToggleKey` | `B + LeftAlt` | The key that opens and closes the doors |
| `HoldToKeepOpen` | `false` | Turn this on and the doors stay open only while you hold the key |

## Notes

- Every weapon bay on the aircraft opens, not just the one you last fired from.
- Landing gear doors are left alone. They are a different mechanism and the key
  never touches them.
- Aircraft with no weapon bay ignore the key.
- On a transport, the cargo ramp counts as a door and opens too.
- Other people do not see your doors move unless the game itself opened them.

## Installing

You need BepInEx 5. Put `BayDoorsMod.dll` in `BepInEx/plugins/`.
