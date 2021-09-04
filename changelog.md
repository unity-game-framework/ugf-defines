# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.1.5](https://github.com/unity-game-framework/ugf-defines/releases/tag/2.1.5) - 2021-09-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-defines/milestone/9?closed=1)  
    

### Changed

- Change display of defines collection in defines project settings ([#28](https://github.com/unity-game-framework/ugf-defines/pull/28))  
    - Update dependencies: `com.ugf.editortools` to `1.13.0` version.
    - Change display of defines collection in defines project settings.

## [2.1.4](https://github.com/unity-game-framework/ugf-defines/releases/tag/2.1.4) - 2021-05-25  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-defines/milestone/8?closed=1)  
    

### Changed

- Update dependencies ([#26](https://github.com/unity-game-framework/ugf-defines/pull/26))  
    - Update dependencies: `com.ugf.customsettings` to `3.4.1` and `com.ugf.editortools` to `1.11.0`.

## [2.1.3](https://github.com/unity-game-framework/ugf-defines/releases/tag/2.1.3) - 2021-05-25  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-defines/milestone/7?closed=1)  
    

### Changed

- Change project settings root name ([#24](https://github.com/unity-game-framework/ugf-defines/pull/24))  
    - Update project to Unity `2020.3`.
    - Change project settings root name to `Unity Game Framework`.

## [2.1.2](https://github.com/unity-game-framework/ugf-defines/releases/tag/2.1.2) - 2021-01-24  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-defines/milestone/6?closed=1)  
    

### Fixed

- Fix defines settings not display ([#21](https://github.com/unity-game-framework/ugf-defines/pull/21))  
    - Update dependencies with required fix: `com.ugf.editortools` to `1.10.0` version.

## [2.1.1](https://github.com/unity-game-framework/ugf-defines/releases/tag/2.1.1) - 2021-01-23  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-defines/milestone/5?closed=1)  
    

### Fixed

- Change build processors order value when Unity fix become available ([#16](https://github.com/unity-game-framework/ugf-defines/pull/16))  
    - Update `DefinesBuildPostprocess` and `DefinesBuildPreprocess` order values.
    - Change required _Unity_ version to `2020.2.2f1`.

## [2.1.0](https://github.com/unity-game-framework/ugf-defines/releases/tag/2.1.0) - 2020-12-03  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-defines/milestone/4?closed=1)  
    

### Changed

- Update dependencies ([#14](https://github.com/unity-game-framework/ugf-defines/pull/14))

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


