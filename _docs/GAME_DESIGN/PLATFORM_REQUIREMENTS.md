# Platform Requirements

**Created**: Nov 14, 2025  
**Owner**: Build Engineer  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Overview

Platform Requirements define the technical constraints, device specifications, and platform-specific considerations for supporting WebGL (browser), Android (Play Store), and iOS (App Store).

---

## WebGL (Browser)

### Minimum Browser Requirements

| Browser | Min Version | OS |
|---------|-------------|-----|
| Chrome | 90+ | Windows, Mac, Linux |
| Firefox | 88+ | Windows, Mac, Linux |
| Safari | 14+ | macOS, iOS (iPad only) |
| Edge | 90+ | Windows |

### Hardware Requirements

| Component | Minimum | Recommended |
|-----------|---------|-------------|
| CPU | Dual-core 2 GHz | Quad-core 2.5 GHz |
| RAM | 2 GB | 4+ GB |
| GPU | Integrated (Intel HD) | Dedicated (NVIDIA/AMD) |
| Storage | 50 MB (download) | 100 MB (cache) |
| Network | 5 Mbps | 10+ Mbps |

### Performance Constraints

**Memory Limits**:
- Single-context memory: ~200-400 MB (varies by browser)
- Garbage collection must be active (60-120ms pause acceptable)
- No WebAssembly size limit (practical: < 50 MB)

**Graphics**:
- WebGL 2.0 (fallback to 1.0 not supported)
- Texture compression: ASTC not available (use uncompressed)
- Max texture size: 4096 × 4096 (most devices)
- Rendering: Forward pipeline only

**Network**:
- Initial load: Assets served over HTTPS
- Caching: Browser cache used (ETag, Last-Modified headers)
- No streaming assets (all included in build)

### Browser-Specific Issues

**Chrome**:
- Aggressive garbage collection (may pause 50-100ms)
- Good WebGL 2.0 support
- Developer tools integrated

**Firefox**:
- More stable performance
- Good WebGL support
- Canvas rendering optimized

**Safari**:
- Limited WebGL support (but 14+ sufficient)
- May throttle background tabs
- Power saving on battery

### Deployment

**Host Options**:
1. **Itch.io** (recommended for MVP)
   - Free hosting
   - Built-in analytics
   - Easy updates
   - Embeddable

2. **Custom Server**
   - Full control
   - Domain customization
   - HTTPS required (for microphone/camera access, future)
   - CDN recommended for global delivery

3. **AWS/Google Cloud**
   - S3 + CloudFront (AWS)
   - Cloud Storage + CDN (Google)
   - Higher cost, better global performance

**Server Setup (Nginx example)**:
```nginx
server {
    listen 443 ssl http2;
    server_name bumpu.example.com;
    
    ssl_certificate /path/to/cert.pem;
    ssl_certificate_key /path/to/key.pem;
    
    gzip on;
    gzip_types application/wasm text/javascript application/javascript;
    
    location / {
        root /var/www/bumpu-webgl;
        try_files $uri $uri/ /index.html;
    }
    
    # Cache-busting for versioned assets
    location ~* \.(js|wasm|data)\.gz$ {
        add_header Cache-Control "max-age=31536000, immutable";
    }
    
    location ~* \.html$ {
        add_header Cache-Control "max-age=3600";
    }
}
```

---

## Android

### Device Requirements

**Minimum Spec**:
- **OS**: Android 7.0 (API 24)
- **CPU**: ARMv8 64-bit (ARM64)
- **RAM**: 2 GB minimum
- **Storage**: 150 MB (APK/AAB size)

**Target Devices**:
- Phone: 6.0" - 6.7" diagonal
- Aspect ratio: 18:9 to 20:9 (modern phones)
- Resolution: 1080 × 2160 minimum (1440 × 3120 common)

**Recommended Spec** (for 60 FPS):
- **OS**: Android 10+ (API 29+)
- **CPU**: Snapdragon 855+ or Exynos 9820+
- **RAM**: 4+ GB
- **GPU**: Mali-G77, Adreno 665 or better

### API Level Strategy

**Min API Level**: 24 (Android 7.0)
- Target most phones (90%+ of active devices)
- Essential features available

**Target API Level**: 34 (Android 14)
- Required by Play Store (as of 2024)
- Access to latest OS features
- Better security

**Supported Versions**:
```
Android 7.0 (API 24)   → 5% active devices
Android 8-9 (API 26-28) → 15% active devices
Android 10-13 (API 29-33) → 70% active devices
Android 14+ (API 34+)  → 10%+ active devices
```

### Architecture Support

**Primary**: ARM64 (armv8-a)
- Modern standard
- All current devices support
- Better performance
- 64-bit advantage (precision, memory)

**Secondary (Optional)**: ARMv7
- Older devices (pre-2015)
- Backwards compatibility
- Increases build size (~20%)
- Not required for MVP

**x86/x86_64**:
- Emulators only
- Some tablets
- Not prioritized

### Graphics/Performance

**GPU Support**:
- OpenGL ES 3.0 minimum
- Vulkan (if available, future optimization)
- Support for both (automatic selection)

**Performance Targets**:
- Modern phones (2020+): 60 FPS
- Mid-range (2018-2019): 45-60 FPS, capped at 60
- Budget (2016-2017): 30 FPS (locked)

**Memory Management**:
- Target heap: < 200 MB
- Peak usage: < 400 MB
- Monitor via Android Profiler
- Avoid frequent GC pauses

### Storage & Permissions

**Minimum Permissions**:
```xml
<uses-permission android:name="android.permission.INTERNET" />
```

**Optional Permissions** (future):
```xml
<!-- For future multiplayer/cloud saves -->
<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
<uses-permission android:name="android.permission.CAMERA" />
<uses-permission android:name="android.permission.RECORD_AUDIO" />
```

**Storage**:
- App size on disk: 100-150 MB
- Requires available space for decompression
- No save data in MVP (stateless)

### Device Fragmentation

**Fragmentation Challenges**:
- Screen sizes: 4.5" to 7.5" common
- Resolutions: 720p to 1440p to 2K
- Safe areas: Notches (top), gesture buttons (bottom)
- Manufacturers: Samsung, Google, OnePlus, Xiaomi (different UX)

**Mitigation**:
- Canvas scaler (responsive)
- Safe area calculation
- Test on representative devices
- Avoid hardware-specific features

### Distribution (Play Store)

**Release Types**:
1. **Closed Testing**: 20+ testers, internal only
2. **Open Testing**: Public beta, limited countries
3. **Production**: Full release, all countries

**Requirement for Submission**:
- Target API 34+
- 64-bit architecture
- Privacy policy
- Content rating
- Minimum app size (no bloat)

---

## iOS

### Device Requirements

**Minimum Spec**:
- **OS**: iOS 12.0
- **Device**: iPhone 6s or later
- **RAM**: 2 GB minimum
- **Storage**: 150 MB

**Target Devices**:
- **Small**: iPhone 12 mini, 13 mini (5.4")
- **Standard**: iPhone 12, 13, 14, 15 (6.1")
- **Pro/Plus**: iPhone 12 Pro Max, 13 Pro Max, 14 Plus, 15 Plus (6.7")
- **SE**: iPhone SE (4.7" - budget)
- **Tablets**: iPad (9.7"+) - optional for v1

**Recommended Spec** (for 60 FPS):
- **OS**: iOS 15+ (A14 Bionic+)
- **Device**: iPhone 12 or later
- **RAM**: 4+ GB
- **GPU**: Apple GPU Gen 4+

### iOS Version Strategy

**Min iOS Version**: 12.0
- Support most active devices (~70%)
- Essential features available
- Requires careful testing

**Target iOS Version**: 17.0+
- Current standard (2024)
- Latest features available
- Recommended for new projects

**Supported Versions**:
```
iOS 12-13  → 10% of active devices
iOS 14-15  → 40% of active devices
iOS 16     → 30% of active devices
iOS 17+    → 20%+ of active devices
```

### Architecture & Performance

**Architecture**: ARM64 only
- Universal (works on all modern iPhones)
- 32-bit support removed (iOS 11+)
- Better memory access (performance)

**Graphics**:
- Metal API (primary)
- OpenGL ES 3.0 fallback (if needed)
- Metal provides 2-3x better performance

**Performance Targets**:
- Modern iPhones (12+): 60 FPS
- Older iPhones (11, XS): 45-60 FPS
- Oldest iPhones (SE, 8): 30 FPS

**Memory**:
- Target heap: < 200 MB
- Peak usage: < 400 MB
- iOS aggressive memory management (app termination if over limit)

### Safe Area Handling

**Top Safe Area** (notch/Dynamic Island):
- iPhone 12+: ~47 pixels (notch)
- iPhone 12 Pro/Pro Max: ~47 pixels
- Must reserve space in HUD

**Bottom Safe Area** (home indicator):
- All modern iPhones: ~34 pixels
- Must reserve space for buttons, UI

**Calculation**:
```csharp
Rect safeArea = Screen.safeArea;
float topInset = Screen.height - safeArea.yMax;
float bottomInset = safeArea.yMin;
float leftInset = safeArea.xMin;
float rightInset = Screen.width - safeArea.xMax;

// Apply to RectTransform
hudPanel.offsetMin = new Vector2(leftInset, bottomInset);
hudPanel.offsetMax = new Vector2(-rightInset, -topInset);
```

### Permissions & Privacy

**Minimum Permissions**: None (game doesn't access device features)

**Privacy Policy** (required for App Store):
- No user data collection
- No analytics (or disclose)
- No third-party SDKs (or disclose)
- COPPA compliance (if targeting under 13)

**Example Privacy Policy**:
```
Bump U does not collect, store, or transmit any personal data.
No user accounts are required.
No analytics or tracking is performed.
All gameplay occurs offline.
```

### Device Fragmentation

**Fragmentation Challenges**:
- Screen sizes: 4.7" to 6.7" (plus notch variations)
- Safe areas: Top (notch/Dynamic Island), bottom (home)
- Performance: A14 Bionic → A18 Pro (significant gap)

**Mitigation**:
- Canvas scaler (responsive)
- Safe area layout guide
- Profiling on oldest target device
- Avoid deprecated APIs

### Distribution (App Store)

**Review Process**:
- Initial review: 24-48 hours
- Rejection possible (grounds: crashes, inappropriate content, misleading)
- Resubmission: < 24 hours again

**Requirement for Submission**:
- iOS 12.0+
- Arm64 architecture
- App Privacy Policy
- ESRB Content Rating
- Screenshots (5 required)
- No beta/unfinished content

**TestFlight (Closed Testing)**:
- Optional beta program
- 100 internal testers (free)
- 10,000 external testers (paid account)
- Distribute early for feedback

---

## Cross-Platform Consistency

### Resolution Strategy

**Base Resolution**: 1080 × 1920 (9:16 portrait)

**Scaling by Platform**:
```
WebGL: Fixed canvas (responsive via CSS scaling)
Android: Dynamic (varies by device)
iOS: Dynamic (varies by device)

Canvas scaler in Unity:
- Reference resolution: 1080 × 1920
- Scale with screen size: enabled
- Match: Width or Height (height priority)
```

### Input Handling

**Mouse/Touch**:
```
WebGL: Mouse (desktop) + Touch (iPad)
Android: Touch (required)
iOS: Touch (required)
```

**Supported Interactions**:
- Tap cell (all)
- No multi-touch
- No drag (future)
- No keyboard input (menus only)

### Graphics Settings

| Setting | WebGL | Android | iOS |
|---------|-------|---------|-----|
| Antialiasing | 2x | 2x | 2x |
| Texture Compression | Uncompressed | ETC2 | ASTC |
| Rendering Path | Forward | Forward | Forward |
| V Sync | Off | Off | Off |
| Target FPS | 60 | 60 | 60 |

### Performance Profiling

**Tools**:
- **WebGL**: Chrome DevTools (Performance tab)
- **Android**: Android Profiler (Android Studio)
- **iOS**: Xcode Instruments (Metal, CPU profiler)

**Metrics to Monitor**:
- FPS (stable 60 or capped 30)
- Memory (< 400 MB)
- CPU usage (< 80%)
- GPU utilization (< 90%)
- Garbage collection pauses (< 50ms)

---

## Testing Checklist

### WebGL
- [ ] Loads in < 5 seconds
- [ ] 60 FPS on desktop
- [ ] 30 FPS on mobile browser
- [ ] Mobile responsive (portrait only)
- [ ] Touch input works
- [ ] All game modes functional
- [ ] No console errors

### Android
- [ ] Installs and launches
- [ ] 60 FPS on modern device
- [ ] 30 FPS on budget device
- [ ] Safe area respected
- [ ] Touch input responsive
- [ ] Memory under 400 MB
- [ ] No crashes

### iOS
- [ ] Installs (TestFlight or device)
- [ ] 60 FPS on iPhone 12+
- [ ] 30 FPS on iPhone SE
- [ ] Safe area respected (notch, home)
- [ ] Touch input responsive
- [ ] Memory under 400 MB
- [ ] No crashes

---

## Related Documents

- BUILD_PIPELINE.md
- APP_STORE_REQUIREMENTS.md
- SPRINT_7_BUILD_PREP.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Implementation
