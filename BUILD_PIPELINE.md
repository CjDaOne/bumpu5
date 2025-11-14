# Build Pipeline
**Created**: Nov 14, 2025  
**Owner**: Build Engineer  
**Status**: ACTIVE

---

## WebGL Build Setup

### Build Settings Configuration
```
File → Build Settings
├─ Platform: WebGL
├─ Target Arch: WebGL 2.0 (hardware accelerated)
├─ Development Build: No (production)
├─ IL2CPP: Yes (faster, optimized bytecode)
├─ Compression Format: Gzip (80% smaller)
└─ Development Build: Disabled
```

### WebGL Optimization
- **Compression**: Gzip (reduces build size 60-80%)
- **IL2CPP**: Enabled (IL2CPP→C++ compilation, faster)
- **Linking Level**: Aggressive (remove unused code)
- **Strip Engine Code**: Yes (removes unused Unity features)
- **Memory**: 256MB minimum allocation

### Build Output
- **Location**: `./WebGLBuilds/`
- **Files**:
  - `index.html` (entry point)
  - `Build/[name].json` (metadata)
  - `Build/[name].js` (game code, IL2CPP)
  - `Build/[name].wasm` (WebAssembly bytecode)
- **Size Target**: < 50MB (with compression)

### Build Command (CLI)
```bash
unity -projectPath . \
  -buildTarget WebGL \
  -executeMethod BuildTools.BuildWebGL \
  -quit -batchmode
```

---

## Android Build Process

### Build Settings Configuration
```
File → Build Settings
├─ Platform: Android
├─ Minimum API Level: 21 (Android 5.0)
├─ Target API Level: 33+ (latest)
├─ Graphics API: OpenGL ES 3.0 / Vulkan
├─ Development Build: No (production)
├─ IL2CPP: Yes
└─ Compression: LZ4 (balance size/speed)
```

### APK Build Process
1. **Keystore Setup** (first build only)
   - Create keystore: `keytool -genkey -v -keystore release.keystore ...`
   - Location: `Assets/Keystore/release.keystore`
   - Password: [secure password]

2. **Player Settings**
   - Bundle Identifier: `com.yourcompany.bumpu`
   - Version: 1.0.0
   - Build Number: 1
   - Minimum API: 21
   - Target API: 33+

3. **Build APK**
   ```
   Unity Editor → Build Settings → Build
   Output: Bump_U_1.0.0.apk
   ```

4. **Sign APK** (automatic with keystore)
   - Signing certificate: Present in keystore
   - v2 Signing: Enabled

### AAB (App Bundle) Build
- **For Play Store**: Use AAB format (more flexible)
- **Format**: Android App Bundle (.aab)
- **Build Settings**: Same as APK, select "Build App Bundle"
- **Output**: `Bump_U_1.0.0.aab`

### Build Configuration
- **Compression**: LZ4 (faster load, slightly larger)
- **Engine Code**: Stripped (remove unused)
- **Graphics**: Vulkan primary, OpenGL fallback
- **Memory**: 512MB (standard for modern devices)

### Build Command (CLI)
```bash
unity -projectPath . \
  -buildTarget Android \
  -executeMethod BuildTools.BuildAndroid \
  -quit -batchmode
```

---

## iOS Build Process

### Build Settings Configuration
```
File → Build Settings
├─ Platform: iOS
├─ Minimum iOS Version: 11.0
├─ Targeted Device Family: iPhone + iPad
├─ Graphics API: Metal
├─ Development Build: No (production)
├─ IL2CPP: Yes
└─ Debugging: Disabled
```

### Xcode Export Process
1. **Initial Build**
   ```
   Unity → Build Settings → Build
   Output: Xcode project (iOS_Build/)
   ```

2. **Open in Xcode**
   ```bash
   open iOS_Build/Unity-iPhone.xcodeproj
   ```

3. **Configure in Xcode**
   - Team ID: [Apple Developer Account]
   - Bundle Identifier: `com.yourcompany.bumpu`
   - Version: 1.0.0
   - Build: 1
   - Provisioning Profile: Automatic

4. **Archive**
   ```
   Product → Archive
   Output: .xcarchive
   ```

5. **Upload to App Store Connect**
   - Use Xcode Organizer or Transporter
   - Upload .xcarchive
   - Notarization: Automatic (Apple handles)

### Build Configuration
- **Metal**: Primary graphics API (faster on iOS)
- **Bitcode**: Enabled (for device compatibility)
- **Optimization**: Fastest, Smallest (tradeoff)
- **Signing**: Automatic (Xcode manages)

### Build Command (CLI)
```bash
unity -projectPath . \
  -buildTarget iOS \
  -executeMethod BuildTools.BuildiOS \
  -quit -batchmode
```

---

## CI/CD Pipeline (Optional)

### GitHub Actions Workflow
```yaml
name: Build on Push

on: [push, pull_request]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Build WebGL
        run: |
          unity -projectPath . \
            -buildTarget WebGL \
            -executeMethod BuildTools.BuildWebGL \
            -quit -batchmode
      - name: Upload Artifact
        uses: actions/upload-artifact@v2
        with:
          name: WebGL-Build
          path: WebGLBuilds/
```

### Jenkins Pipeline (Alternative)
- Trigger on git push
- Build all platforms in parallel
- Run automated tests
- Deploy to staging
- Generate release notes

---

## Build Outputs & Locations

### WebGL
```
WebGLBuilds/
├─ index.html
├─ TemplateData/
└─ Build/
   ├─ WebGL.loader.js
   ├─ WebGL.framework.js.gz
   ├─ WebGL.data.unityweb
   └─ WebGL.wasm
```

### Android
```
Builds/Android/
├─ Bump_U_Release.apk (file)
├─ Bump_U_Release.aab (for Play Store)
└─ symbols.zip (debug symbols)
```

### iOS
```
iOS_Build/
├─ Unity-iPhone.xcodeproj (Xcode project)
└─ Built Products/
   └─ Bump_U.ipa (installed app file)
```

---

## Compression Settings

### WebGL Compression
- **Format**: Gzip (.gz)
- **Compression Level**: 9 (maximum)
- **Target Size**: < 50MB
- **Decompression Time**: < 2 seconds (browser)

### Android Compression
- **Format**: LZ4 (fast decompression)
- **Uncompressed Assets**: ~80MB
- **Compressed APK**: ~30-40MB
- **Uncompressed at Install**: ~150MB total

### iOS Compression
- **Format**: Bitcode (Apple compresses further)
- **App Size**: ~40-60MB (on App Store)
- **Downloaded Size**: ~30-50MB (varies by compression)
- **Installed Size**: ~200MB (full game + assets)

---

## IL2CPP Configuration

### IL2CPP Settings
- **Enable**: Yes (for all platforms)
- **Linker**: Aggressive (remove unused code)
- **CPU: arm64 (Android & iOS 64-bit)
- **Script Stripping**: Enabled

### Script Stripping Process
- Analyzes code dependencies
- Removes unused classes/methods
- Can cause runtime errors if not careful
- Add `link.xml` for protected code

### link.xml Example
```xml
<?xml version="1.0" encoding="utf-8"?>
<linker>
  <assembly fullname="Assembly-CSharp">
    <namespace fullname="Gameplay">
      <type fullname="GameMode" preserve="all"/>
    </namespace>
  </assembly>
</linker>
```

---

## Build Optimization Checklist

### Pre-Build
- [ ] All scenes included in Build Settings
- [ ] No compile errors
- [ ] All assets imported
- [ ] Player settings configured per platform
- [ ] Keystore/signing setup for Android/iOS

### Build Phase
- [ ] IL2CPP enabled
- [ ] Compression enabled
- [ ] Engine code stripping enabled
- [ ] Development build disabled
- [ ] Linking level aggressive

### Post-Build
- [ ] Test on actual devices (not just Editor)
- [ ] Verify all gameplay works
- [ ] Check performance (FPS, memory)
- [ ] Verify graphics rendering
- [ ] Test touch input (mobile)

### Size Optimization
- [ ] Compress textures (reduce to 25% if possible)
- [ ] Remove unused assets
- [ ] Use asset bundles for large content (optional)
- [ ] Verify final build size < targets

---

## Build Size Targets

### WebGL
- **Target**: < 50MB (with compression)
- **Decompress Time**: < 2 seconds
- **Testing**: Chrome, Firefox, Safari

### Android
- **APK Target**: < 40MB
- **AAB Target**: < 45MB (Play Store)
- **Testing**: Multiple device sizes/OS versions

### iOS
- **App Size**: < 60MB
- **Download Size**: < 50MB (on App Store)
- **Testing**: iPhone + iPad, various iOS versions

---

## Platform-Specific Build Settings

### Graphics API Per Platform
| Platform | Primary | Fallback | Auto-Graphics |
|----------|---------|----------|--------------|
| WebGL | WebGL 2 | WebGL 1 | Automatic |
| Android | Vulkan | OpenGL ES 3 | Automatic |
| iOS | Metal | OpenGL ES 2 | Metal only |

### CPU Architecture
| Platform | Arch | Notes |
|----------|------|-------|
| WebGL | wasm | WebAssembly |
| Android | arm64 | 64-bit standard |
| iOS | arm64 | 64-bit standard |

---

## Testing After Build

### Automated Tests
- Unit tests (gameplay logic)
- Integration tests (board + UI)
- Performance tests (FPS, memory)

### Manual Testing
1. Launch game
2. Select game mode
3. Play full round (test win condition)
4. Test all 5 modes
5. Verify FPS on target devices

### Device Testing Matrix
```
WebGL:
- Chrome (latest)
- Firefox (latest)
- Safari (latest)

Android:
- Pixel 5 / Galaxy S21 (modern)
- Pixel 4 / Galaxy S10 (mid-range)
- Older device (performance baseline)

iOS:
- iPhone 12 / 13
- iPhone 11 / XS
- iPad (if supported)
```

---

## Related Documents
- PLATFORM_REQUIREMENTS.md
- APP_STORE_REQUIREMENTS.md
- SPRINT_7_BUILD_PREP.md

---

**Status**: Complete - Production Ready
