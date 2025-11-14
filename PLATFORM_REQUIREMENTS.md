# Platform Requirements
**Created**: Nov 14, 2025  
**Owner**: Build Engineer  
**Status**: ACTIVE

---

## Performance Targets

### Frame Rate Targets
- **WebGL (Desktop)**: 60 FPS (target), 30 FPS minimum
- **Android**: 60 FPS (modern devices), 30 FPS minimum (older)
- **iOS**: 60 FPS (standard), 120 FPS (Pro models, if optimized)

### Memory Constraints
- **WebGL**: 512MB (modern browser allocation)
- **Android**: 256MB-512MB (varies by device)
- **iOS**: 512MB-2GB (varies by device)

### Battery Impact
- **Target**: < 50mA average drain (idle game)
- **Active Play**: 100-200mA (acceptable for game)
- **GPU Intensive**: 300-400mA (high but acceptable)

---

## WebGL Requirements

### Browser Support
| Browser | Min Version | Note |
|---------|-------------|------|
| Chrome | 90+ | WebGL 2.0 |
| Firefox | 88+ | WebGL 2.0 |
| Safari | 14+ | WebGL 2.0 (partial) |
| Edge | 90+ | WebGL 2.0 |

### Memory Requirements
- **Available VRAM**: 256MB minimum
- **System RAM**: 2GB minimum
- **Allocation**: Allow 512MB for game

### GPU Requirements
- **Minimum**: Intel HD Graphics 520 (2015+)
- **Recommended**: Dedicated GPU (NVIDIA/AMD)
- **Fallback**: WebGL 1.0 support (slower)

### Input Requirements
- **Mouse**: Standard mouse input
- **Keyboard**: Escape to exit, Enter to submit (optional)
- **Touch**: If on tablet browser (HTML5 touch events)

### Display Requirements
- **Minimum Resolution**: 480×720 (smallest phone)
- **Typical Resolutions**: 720×1280 to 3840×2160
- **Aspect Ratios**: 16:9, 9:16, 4:3, custom

### Network Requirements
- **Download**: < 50MB (WebGL build)
- **Load Time**: < 5 seconds (on average connection)
- **Offline**: Game works offline (no internet needed)

---

## Android Requirements

### OS Versions
- **Minimum**: Android 5.0 (API 21)
- **Target**: Android 12+ (API 32+)
- **Latest**: Android 13+ (API 33+)

### Device Classes
| Category | RAM | Screen | Examples |
|----------|-----|--------|----------|
| Budget | 2GB | 4.5" | Moto G, A-series |
| Mid-Range | 4GB | 5.5"-6" | Pixel 5, S10 |
| Flagship | 6GB+ | 5.5"-6.7" | Pixel 6, S21 |
| Tablet | 4GB+ | 7"-12" | iPad, Galaxy Tab |

### Performance by Category
- **Budget**: 30 FPS (acceptable)
- **Mid-Range**: 60 FPS (target)
- **Flagship**: 60+ FPS (easy)
- **Tablet**: 60 FPS (expected)

### GPU/CPU Requirements
- **Minimum**: Snapdragon 410 (2014, basic)
- **Recommended**: Snapdragon 765 (2019, mid-range)
- **Optimal**: Snapdragon 8 series (flagship)

### Memory Requirements
- **RAM**: 2GB minimum (game uses ~150-300MB)
- **Storage**: 50MB free (APK install)
- **Installed Size**: ~200MB (with all assets)

### Display Requirements
- **DPI**: ldpi to xxxhdpi (1x to 4x)
- **Resolutions**: 480×800 to 1440×3040+
- **Aspect Ratios**: 9:16 to 20:9 (foldables)
- **Notches**: Account for notch (top safe area)
- **Refresh Rate**: 60Hz (standard), 90Hz+, 120Hz (newer)

### Input Requirements
- **Touch**: Multi-touch capable (standard)
- **Buttons**: Back, Home (system buttons)
- **Sensors**: Optional (accelerometer, gyro for tilt)
- **Keyboard**: Not required (virtual keyboard as fallback)

### Network Requirements
- **Optional**: Game works offline
- **Graphics Update**: May check for assets online (optional)

---

## iOS Requirements

### OS Versions
- **Minimum**: iOS 11.0
- **Target**: iOS 14+
- **Recommended**: iOS 15+

### Device Classes
| Category | RAM | Screen | Examples |
|----------|-----|--------|----------|
| Older | 2GB | 4.7" | iPhone 6s, 7, 8 |
| Current | 4GB | 5.4"-6.7" | iPhone 12, 13 |
| Pro | 6GB+ | 5.8"-6.7" | iPhone 13 Pro |
| iPad | 4GB+ | 7.9"-12.9" | iPad Air, Pro |

### Performance by Category
- **Older**: 30-60 FPS (depends on game state)
- **Current**: 60 FPS (target)
- **Pro**: 60+ FPS (easy, consider 120 FPS)
- **iPad**: 60 FPS (expected)

### GPU/CPU Requirements
- **Minimum**: A9 chip (iPhone 6s, 2015)
- **Recommended**: A12 chip (iPhone XS, 2018+)
- **Optimal**: A15 Bionic (iPhone 13, 2021+)

### Memory Requirements
- **RAM**: 2GB minimum (game uses ~150-300MB)
- **Storage**: 50MB free (app download)
- **Installed Size**: ~200MB (with all assets)

### Display Requirements
- **Safe Area**: Notch (top), Home Indicator (bottom)
- **Resolutions**: 375×812 (iPhone) to 1024×1366 (iPad)
- **Aspect Ratios**: 9:19.5 to 4:3
- **Refresh Rate**: 60Hz (standard), 120Hz (ProMotion on Pro)
- **Dynamic Island**: Optional (treat like notch)

### Input Requirements
- **Touch**: Multi-touch capable
- **Buttons**: Home button or gesture
- **Gestures**: Respect system swipe gestures
- **Face ID/Touch ID**: Optional (no authentication needed for game)

### Network Requirements
- **Optional**: Game works offline
- **iCloud**: Not required (no save sync)

### App Store Specific
- **Age Rating**: ESRB, PEGI (non-violent game, likely 3+)
- **Privacy Policy**: Required
- **App Clips**: Optional (preview before install)

---

## Safe Area Handling

### iOS Safe Area Insets
- **Top**: Notch (44pt iPhone, 0pt landscape)
- **Bottom**: Home Indicator (34pt, 0pt iPhone 8 and earlier)
- **Left/Right**: 0pt (no side notches currently)

### Android Safe Area
- **Status Bar**: 24dp (system info)
- **Navigation Bar**: 48-72dp (bottom, varies)
- **Cutout Areas**: Notch handling (varies by device)

### Implementation
```csharp
// Unity's safe area API
Rect safeArea = Screen.safeArea;
// Returns insets: left, top, right, bottom

// Recommended: Inset UI by safeArea
RectTransform uiRect = GetComponent<RectTransform>();
uiRect.offsetMin = new Vector2(safeArea.x, safeArea.y);
uiRect.offsetMax = new Vector2(-safeArea.width, -safeArea.height);
```

### Game Board Safe Area
- Never place critical board cells under notch
- Board should fit in safe area entirely
- HUD buttons inset from edges
- Status messages clear of top bar

---

## Input Latency Targets

### Touch Input Latency
- **Detection**: < 16ms (one frame at 60 FPS)
- **Response**: < 50ms (feel responsive)
- **Acceptable**: < 100ms (noticeable but playable)
- **Unacceptable**: > 200ms (feels laggy)

### Network Latency (if applicable)
- Not applicable (offline game)

---

## Temperature & Thermal Management

### Target Temperatures
- **Idle**: Device temperature + 5°C
- **Active Play**: Device temperature + 15-20°C
- **Maximum**: 45°C (device safety limit)

### Thermal Throttling
- Devices throttle performance above 40°C
- Game should perform below throttling point
- If throttling occurs: Reduce graphics, lower target FPS

### Mitigation
- Optimize rendering (fewer particles, simpler shaders)
- Cap FPS at 60 (don't push unnecessary)
- Monitor GPU usage (use Unity Profiler)

---

## Graphics & Rendering

### Graphics API Support
| Platform | Primary | Fallback | Notes |
|----------|---------|----------|-------|
| WebGL | WebGL 2 | WebGL 1 | Automatic fallback |
| Android | Vulkan | OpenGL ES 3 | Automatic selection |
| iOS | Metal | OpenGL ES 2 | Metal only (iOS 8+) |

### Rendering Resolution
- **Base**: 1920×1080 (design resolution)
- **Scaling**: Maintain aspect ratio, scale to device
- **Minimum**: 480×720 (smallest phones)
- **Maximum**: 3840×2160 (large displays)

### Texture Quality
- **Compression**: ASTC (Android), PVRTC (iOS)
- **MipMaps**: Yes (reduce aliasing)
- **Anisotropic Filtering**: Optional (affects memory)

### Shader Complexity
- **Simple Scenes**: 1-2 light sources max
- **Particle Effects**: < 100 particles per effect
- **Post-Processing**: Minimal (blur, color grading)

---

## Audio Requirements

### Audio Formats
- **WebGL**: MP3, OGG Vorbis, WAV
- **Android**: OGG, MP3, WAV
- **iOS**: M4A (AAC), MP3, WAV

### Audio Quality
- **Target**: 128-192 kbps (compressed)
- **Size**: All audio < 20MB combined
- **Channels**: Mono or Stereo (3D audio optional)

### Audio Performance
- **Simultaneous**: 4-8 sounds max
- **Master Volume**: Always controllable
- **Mute Option**: Respected system mute switch

---

## Storage & Installation

### Installation Size
- **WebGL**: 50MB (browser cache)
- **Android APK**: 35-45MB
- **Android AAB**: 45-60MB (Play Store, varies by device)
- **iOS**: 50-80MB (App Store download)

### Installed Size
- **WebGL**: 0MB (streamed)
- **Android**: 150-200MB total
- **iOS**: 150-200MB total

### Storage Requirements
- **Minimum**: 50MB free space
- **Recommended**: 200MB free space
- **No Cloud Save**: Game doesn't require cloud storage

---

## Network Requirements

### Connectivity
- **Offline**: Game fully playable offline
- **No Server**: No server required
- **No Ads**: No ad network integration (optional)

---

## Accessibility Requirements

### Color Contrast
- **Text**: 4.5:1 ratio minimum
- **UI**: 3:1 ratio minimum
- **Colorblind**: Support deuteranopia (green-red blind)

### Text Size
- **Minimum**: 12px (12pt on screen)
- **Recommended**: 16px+
- **Scaling**: Respects system font size setting

### Touch Targets
- **Minimum**: 44×44px (iOS), 48×48px (Android)
- **Spacing**: 8px minimum between targets

---

## Related Documents
- BUILD_PIPELINE.md
- APP_STORE_REQUIREMENTS.md
- SPRINT_7_BUILD_PREP.md

---

**Status**: Complete - Production Ready
