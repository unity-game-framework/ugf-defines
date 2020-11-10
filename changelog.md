# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0](https://github.com/unity-game-framework/ugf-defines/releases/tag/2.0.0) - 2020-11-10  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-defines/milestone/3?closed=1)  
    

### Changed

- Update to Unity 2020.2 ([#9](https://github.com/unity-game-framework/ugf-defines/pull/9))  
    - Also updated dependencies, `com.ugf.customsettings` to `3.1.0` and `com.ugf.editortools` to `1.6.0`.

### Removed

- Remove DefinesEditorSettings.Save method ([#10](https://github.com/unity-game-framework/ugf-defines/pull/10))  
    - Remove `DefinesEditorSettings.Save` method, use `DefinesEditorSettings.Settings.SaveSettings` instead.
    - Change `DefinesEditorSettingsData` class to be public.
    - Change `DefinesEditorSettings.Settings` property name to `DefinesEditorSettings.PlatformSettings`.
    - Change `CustomSettingsEditorPackage<DefinesEditorSettingsData>` instance to be public and accessible from `DefinesEditorSettings`.

## [1.0.1](https://github.com/unity-game-framework/ugf-defines/releases/tag/1.0.1) - 2020-10-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-defines/milestone/2?closed=1)  
    

### Changed

- Change build processors callback order value to bypass Unity internal bug ([#5](https://github.com/unity-game-framework/ugf-defines/pull/5))

## [1.0.0](https://github.com/unity-game-framework/ugf-defines/releases/tag/1.0.0) - 2020-10-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-defines/milestone/1?closed=1)  
    

### Added

- Add build and editor custom compile define settings ([#2](https://github.com/unity-game-framework/ugf-defines/pull/2))  
    - Add project settings to store custom defines which can be enabled or disable to be included in player build.


