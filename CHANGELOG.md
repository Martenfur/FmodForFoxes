# [Changelog](http://keepachangelog.com/en/1.0.0/):

## [Unreleased]

### Added:

- Added an option to load from stream or from byte buffer for `LoadSound`, `LoadStreamedSound` and `LoadBank`.

### Changed:

- Made constructors for `Attributes3D`, `Bank`, `Bus`, `EventDescription`, `EventInstance`, `VCA`, `Channel`, `Sound` public instead of internal.
- Updated to FMOD 2.02.25
- Migrated to NET8.

## [v3.1.0] - *04.12.2023*

### Added:

- Added	`Position3D`, `Velocity3D`, `MinDistance`, `MaxDistance` to `EventInstance`.

### Fixed:

- Fixed 3D sound attibutes not being set properly when using Fmod Studio.

## [v3.0.0] - *22.09.2022*

### Breaking:

- Renamed to FmodForFoxes.
- Migrated to .NET6.
- Renamed `TriggerCue` to `KeyOff`.
- Renamed `FMODManager` to `FmodManager`.
- Replaced the `NativeLibraryLoader` class with `INativeLibraryLoader`.

### Added

- Added fmod_errors header.

### Changed

- Removed shared projects.
- Updated to FMOD 2.02.08
- Changed the license to MIT.

## [v2.0.0.0] - *29.06.2020*

### Added:

- Added FMOD Studio support.

### Changed:

- Reworked the overall API.

### Removed:

- Removed `fmod.jar` from the Android library.
