# Bay Doors

A utility mod for Nuclear Option. Open and close the weapon bay doors of the aircraft
you are flying, on a key of your choosing.

[![Release](https://img.shields.io/github/v/release/mosdef31/NO-Bay-Doors)](https://github.com/mosdef31/NO-Bay-Doors/releases/latest)
[![Downloads](https://img.shields.io/github/downloads/mosdef31/NO-Bay-Doors/total)](https://github.com/mosdef31/NO-Bay-Doors/releases)
[![Game](https://img.shields.io/badge/Nuclear%20Option-0.34.2-blue)](https://store.steampowered.com/app/2168680/Nuclear_Option/)
[![Licence](https://img.shields.io/badge/licence-MIT-green)](LICENSE)
[![Issues](https://img.shields.io/github/issues/mosdef31/NO-Bay-Doors)](https://github.com/mosdef31/NO-Bay-Doors/issues)

[Download](https://github.com/mosdef31/NO-Bay-Doors/releases/latest) |
[Changelog](CHANGELOG.md) |
[Credits](ATTRIBUTION.md) |
[Report a bug](https://github.com/mosdef31/NO-Bay-Doors/issues)

## What it is

### Controls

- **A key that opens the bays.** Alt+B by default, and you can set it to anything.
- **Toggle or hold.** Press once to open and again to close, or have them open only
  while the key is down.

### Behaviour

- **Every weapon bay on the aircraft**, not just the one you last fired from.
- **The landing gear is left alone.** Gear doors are not weapon bay doors and the mod
  cannot reach them.
- **Firing still works normally.** The game opens a bay to release from it, and this
  does not fight that.

Transport ramps count as bay doors to the game, so a transport with a ramp on a
hardpoint opens it on the same key.

## Installing

Needs BepInEx. Put `BayDoorsMod.dll` in `BepInEx/plugins`.

## Settings

They are in `BepInEx/config/com.baydoors.cfg`, and you can also edit them in
Configuration Manager if you have it.

| Setting | Default | Does |
|---|---|---|
| `ToggleKey` | `Alt+B` | The key that opens and closes the doors |
| `HoldToKeepOpen` | `false` | Off: press once to open, press again to close. On: the doors stay open only while the key is held |

## If nothing happens

Not every aircraft has a weapon bay. The mod writes one line per aircraft to
`BepInEx/LogOutput.log` saying how many bay doors it found, so that is the first
thing to read.

## AI use

I use an AI agent to help with coding, refactoring, asset modification, and
authoring long bodies of text and lore.

It raises the quality ceiling beyond what my own skills currently guarantee, while I
learn and develop them. Every decision, every number, and everything that ships is
mine.

## About this source

`src/` is the mod's C# with the comments stripped. It is there to read, not to build:
there is no project file and no game assemblies, so it will not compile as it stands.

See [ATTRIBUTION.md](ATTRIBUTION.md) for anything in here that is not my own work.
