# Build Pipeline

**Created**: Nov 14, 2025  
**Owner**: Build Engineer  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Overview

The Build Pipeline defines the complete process for creating optimized, release-ready builds for all three target platforms: WebGL (browser), Android (Play Store), and iOS (App Store). This document standardizes build settings, optimization steps, and quality gates.

---

## WebGL Build

### Target Specification
- **Platform**: WebGL (IL2CPP backend)
- **Resolution**: 1080 × 1920 (portrait, 9:16 aspect ratio)
- **Target Browsers**: Chrome, Firefox, Safari (latest 2 versions)
- **FPS Target**: 60 FPS (desktop), 30 FPS minimum (mobile browsers)
- **File Size Target**: < 50 MB (compressed)

### Build Settings (Unity)

```
File → Build Settings

Platform: WebGL
Scene List:
  - MainMenu
  - Gameplay
  - Settings
  
Architecture: IL2CPP
Scripting Backend: IL2CPP (not Mono)
IL2CPP Code Generation: Faster builds

Target Architecture: WebGL 2.0
Compression: Gzip (reduces .js.gz by 70%)

Graphics:
  - Rendering Path: Forward
  - Color Space: sRGB
  - Anti Aliasing: 2x
  - Texture Compression: Default

Resolution:
  - Default Canvas Width: 1080
  - Default Canvas Height: 1920
  - Match Browser Window: true (responsive)

Other:
  - Strip Engine Code: true (smaller build)
  - Managed Stripping Level: High
  - Player Loop: Optimization for WebGL
```

### WebGL-Specific Optimizations

**Compression**:
```
Build → Output directory
Compression: Gzip (server-side)
Result: ~15-20 MB gzip
```

**Memory Management**:
```csharp
// In game code: Optimize garbage collection
Instantiate() in Update → Create pool instead
Destroy() during gameplay → Defer until phase end
Texture memory: Use atlases instead of individual textures
```

**Loading Screen**:
- Build → Player → WebGL Template
- Create custom loading screen (percentage, spinner)
- Target: < 10s initial load, < 3s scene load

**JavaScript Size**:
- Use LOD system for complex scenes
- Strip unused code (managed stripping)
- Minimize 3D objects (board 12 cells only, no extras)

### Build Command Line

```bash
# Windows
"C:\Program Files\Unity\Hub\Editor\[Version]\Editor\Unity.exe" \
  -projectPath "C:\Projects\BumpU" \
  -buildTarget WebGL \
  -customBuildTarget WebGL \
  -executeMethod BuildScript.BuildWebGL \
  -quit -batchmode

# Mac/Linux
/Applications/Unity/Hub/Editor/[Version]/Unity \
  -projectPath "/Projects/BumpU" \
  -buildTarget WebGL \
  -executeMethod BuildScript.BuildWebGL \
  -quit -batchmode
```

### Output Structure

```
WebGL Build/
├─ index.html
├─ Build/
│  ├─ [ProjectName].framework.js.gz
│  ├─ [ProjectName].data.gz
│  ├─ [ProjectName].wasm.gz
│  └─ [ProjectName].symbols.json
├─ TemplateData/
│  ├─ style.css
│  ├─ favicon.ico
│  └─ UnityProgress.js
└─ StreamingAssets/
   └─ [any streamed content]
```

---

## Android Build

### Target Specification
- **Platform**: Android
- **Min API Level**: 24 (Android 7.0)
- **Target API Level**: 34 (Android 14)
- **Architecture**: ARM64 (armv8) primary, ARMv7 fallback
- **FPS Target**: 60 FPS (modern), 30 FPS minimum (older devices)
- **File Size Target**: < 150 MB (APK/AAB)

### Build Settings (Unity)

```
File → Build Settings

Platform: Android
Scene List:
  - MainMenu
  - Gameplay
  - Settings

Player Settings:
  Graphics:
    - API: OpenGL ES 3.0
    - Auto Graphics API: Off (specify ES 3.0 only)
    - Rendering Path: Forward
    - Color Space: sRGB
    - V Sync: Off (use FPS limiter in code)
    - Anti Aliasing: 2x
    - Texture Compression: ETC2
  
  Resolution:
    - Default Orientation: Portrait
    - Orientation: Portrait (lock)
    - Allow Fullscreen: Yes
    - Default Width: 1080
    - Default Height: 1920
    - Refresh Rate: 60 Hz (or device max)
  
  Identification:
    - Package Name: com.bumpu.game
    - Version: 1.0.0
    - Bundle Version Code: 1
    - Minimum API Level: 24
    - Target API Level: 34
  
  Other:
    - Scripting Backend: IL2CPP
    - IL2CPP Code Generation: Faster builds
    - Managed Stripping Level: High
    - Strip Engine Code: true
    - Build Splitting: Split per CPU architecture

Publishing Settings:
  - Keystore: Create/use signing keystore
  - Key Alias: [your-alias]
  - Key Password: [secure password]
  - Keystore Password: [secure password]
  - Create: Check "Create new keystore if not found"
```

### Keystore Creation (One-Time)

```bash
# Generate keystore
keytool -genkey -v \
  -keystore bumpu.keystore \
  -keyalg RSA \
  -keysize 2048 \
  -validity 10000 \
  -alias bumpu_key

# Then reference in Player Settings:
# Publishing Settings → Keystore Path: bumpu.keystore
# Key Alias: bumpu_key
```

### Android-Specific Optimizations

**Graphics**:
```csharp
// Frame rate limiting
QualitySettings.vSyncCount = 0; // Disable V Sync
Application.targetFrameRate = 60; // Cap at 60 FPS

// Detect device capability and adjust
if (SystemInfo.processorFrequency < 2000)
{
    // Older device: reduce effects
    QualitySettings.SetQualityLevel(0); // Lowest
}
```

**Memory**:
```csharp
// Monitor memory (typical limit: 2GB)
long memoryUsage = SystemInfo.systemMemorySize;
if (memoryUsage < 2000)
{
    // Low-end device: reduce asset quality
    Resources.UnloadUnusedAssets();
}
```

**Safe Area**:
```csharp
// Handle system gesture areas
Rect safeArea = Screen.safeArea;
// Adjust HUD panels to account for notches, gesture buttons
```

### Build Types

**APK (Direct Installation)**:
```
Build → Build APK
Output: BumpU.apk (~100-150 MB)
Use for: Testing, sideloading
Limitation: Single architecture (slower build)
```

**AAB (App Bundle for Play Store)**:
```
Build → Build and Run (or Build App Bundle)
Output: app-release.aab
Use for: Play Store submission
Benefit: Dynamic feature modules, optimized delivery
```

### Build Command Line

```bash
# Android APK
Unity.exe -projectPath "C:\Projects\BumpU" \
  -buildTarget Android \
  -executeMethod BuildScript.BuildAndroidAPK \
  -quit -batchmode

# Android AAB
Unity.exe -projectPath "C:\Projects\BumpU" \
  -buildTarget Android \
  -executeMethod BuildScript.BuildAndroidAAB \
  -quit -batchmode
```

---

## iOS Build

### Target Specification
- **Platform**: iOS (iPhone, iPad)
- **Min iOS Version**: 12.0
- **Target iOS Version**: 17.0+
- **Architecture**: ARM64 (universal, no 32-bit)
- **FPS Target**: 60 FPS (modern), 30 FPS minimum (older)
- **File Size Target**: < 150 MB (App Store)

### Build Settings (Unity)

```
File → Build Settings

Platform: iOS
Scene List:
  - MainMenu
  - Gameplay
  - Settings

Player Settings:
  Graphics:
    - Metal Editor Support: Yes
    - Metal API Validation: No (release builds)
    - API: Metal
    - Rendering Path: Forward
    - Color Space: sRGB
    - Anti Aliasing: 2x
    - Texture Compression: ASTC (iOS best)
  
  Resolution:
    - Default Orientation: Portrait
    - Orientation: Portrait (lock)
    - Status Bar Hidden: No
    - Supported Device Orientations: Portrait only
    - Default Width: 1080
    - Default Height: 1920
    - Refresh Rate: 60 Hz (or device max)
  
  Identification:
    - Product Name: Bump U
    - Bundle Identifier: com.bumpu.game
    - Version: 1.0.0
    - Build: 1
    - Supported Device Families: iPhone only
  
  Other:
    - Scripting Backend: IL2CPP
    - IL2CPP Code Generation: Faster builds
    - Arm64: Yes (only)
    - Managed Stripping Level: High
    - Strip Engine Code: true

Architecture:
  - iOS Architecture: ARM64 only
  - Enable on-demand resources: No (for v1)

Signing & Capabilities:
  - Automatically Sign: Yes (or manual)
  - Team ID: [Your Apple Developer Team ID]
  - Development Team: [Your Team Name]
  - Provisioning Profile: Automatic
```

### Provisioning & Signing

**One-Time Setup**:
1. Register app bundle identifier: `com.bumpu.game`
2. Create App ID in Apple Developer account
3. Create provisioning profiles (Development, Distribution)
4. Download certificates & profiles

**Build Process**:
1. Unity → Build
2. Xcode opens automatically
3. Xcode: General → Signing & Capabilities
4. Select Team → Xcode auto-signs
5. Product → Archive
6. Organizer → Distribute App

### iOS-Specific Optimizations

**Safe Area**:
```csharp
// Handle notch, home indicator
Rect safeArea = Screen.safeArea;
RectTransform hudPanel = GetComponent<RectTransform>();
hudPanel.offsetMin = new Vector2(safeArea.xMin, safeArea.yMin);
hudPanel.offsetMax = new Vector2(-safeArea.xMax, -safeArea.yMax);
```

**Frame Rate**:
```csharp
// iOS ProMotion (120 Hz) detection
int maxRefreshRate = Screen.currentResolution.refreshRateRatio.numerator / 
                     Screen.currentResolution.refreshRateRatio.denominator;
Application.targetFrameRate = Mathf.Min(maxRefreshRate, 60);
```

**Memory**:
```csharp
// Typical limit: 2-4 GB depending on device
long memoryUsage = SystemInfo.systemMemorySize;
```

### Build Command Line

```bash
# iOS Xcode project generation
Unity.exe -projectPath "C:\Projects\BumpU" \
  -buildTarget iOS \
  -executeMethod BuildScript.BuildiOS \
  -quit -batchmode

# Then use Xcode CLI to build & archive
xcodebuild -project BumpU.xcodeproj \
  -scheme Unity-iPhone \
  -configuration Release \
  -archivePath build/BumpU.xcarchive \
  archive

# Upload to App Store
xcodebuild -exportArchive \
  -archivePath build/BumpU.xcarchive \
  -exportOptionsPlist ExportOptions.plist \
  -exportPath build/
```

---

## Device Testing Matrix

### Minimum Device List

| Platform | Device | OS | RAM | Notes |
|----------|--------|----|----|-------|
| Android | Pixel 5a | 12 | 6GB | Reference mid-range |
| Android | Galaxy S21 | 12 | 8GB | High-end |
| Android | Galaxy A10 | 10 | 2GB | Low-end (optional) |
| Android | Pixel 4a | 11 | 6GB | Performance baseline |
| iOS | iPhone 12 | 15 | 4GB | Modern reference |
| iOS | iPhone 13 | 16 | 4GB | Current standard |
| iOS | iPhone SE | 15 | 3GB | Budget option |
| WebGL | Chrome | Latest | N/A | Primary browser |
| WebGL | Firefox | Latest | N/A | Secondary browser |
| WebGL | Safari | Latest | N/A | iOS web support |

### Test Checklist Per Device

- [ ] Game launches without crash
- [ ] Menu loads in < 5 seconds
- [ ] Gameplay smooth (60 FPS or stable 30 FPS)
- [ ] All buttons responsive
- [ ] Safe area respected (notch, gesture areas)
- [ ] Touch input accurate
- [ ] Audio plays correctly
- [ ] No memory warnings
- [ ] Battery usage reasonable

---

## Performance Targets

### Frame Rate
- **Target**: 60 FPS
- **Acceptable**: 30 FPS minimum (locked)
- **Monitor**: Use Profiler in-game

### Memory Usage
- **WebGL**: < 200 MB (browser memory)
- **Android**: < 500 MB (app memory)
- **iOS**: < 500 MB (app memory)

### Load Times
- **App Launch**: < 5 seconds
- **Scene Load**: < 2 seconds
- **UI Response**: < 100ms

### Build Size
- **WebGL**: < 50 MB (gzipped)
- **Android**: < 150 MB (AAB)
- **iOS**: < 150 MB (App Store)

---

## Quality Assurance Gate

Before submission, verify:

- [ ] All 5 game modes work end-to-end
- [ ] No crashes on supported devices
- [ ] No memory leaks
- [ ] FPS target met
- [ ] Load times acceptable
- [ ] All UI responsive
- [ ] Save/load works (if applicable)
- [ ] Settings persist
- [ ] Audio/music works
- [ ] Performance profiler OK

---

## Continuous Integration (Optional, Future)

**Setup** (post-launch):
```
GitHub → Actions → Create workflow
Trigger: On push to main
Steps:
  1. Checkout code
  2. Run Unity tests
  3. Build WebGL
  4. Build Android AAB
  5. Upload artifacts
Result: Automated builds on every commit
```

---

## Related Documents

- PLATFORM_REQUIREMENTS.md
- APP_STORE_REQUIREMENTS.md
- SPRINT_7_BUILD_PREP.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Implementation
