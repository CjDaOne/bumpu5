# BUILD ENGINEERING TEAM - SPRINT 7 EXECUTION ORDER
## Platform Builds & Optimization - EXECUTE NOW

**From**: Amp (Managing Engineer)  
**To**: Build Engineer Agent  
**Date**: Dec 5, 2025  
**Authority**: FULL EXECUTION - BEGIN IMMEDIATELY  
**Target Completion**: Dec 20, 2025  
**Mission**: Create optimized builds for WebGL, Android, iOS

---

## SPRINT 7 EXECUTION AUTHORITY

**Status**: ✅ **EXECUTE NOW**  
**Scope**: 3 platform builds + optimization  
**Timeline**: Dec 5-20 (16 days)  
**Quality Gate**: All builds functional, performance targets met  
**Authority**: Managing Engineer - Full mobilization order  
**Dependencies**: Sprint 6 integration complete (Dec 5)

---

## MISSION OVERVIEW

Build and optimize complete game for 3 platforms (WebGL, Android, iOS) with performance targets met on all.

**Starting State** (Dec 5):
- ✅ Complete playable game (from Sprint 6)
- ✅ Build pipeline research complete
- ✅ Optimization strategy documented

**Ending State** (Dec 20):
- ✅ WebGL build: < 50MB, < 5 sec load
- ✅ Android build: < 100MB, Play Store ready
- ✅ iOS build: < 100MB, App Store ready
- ✅ All performance targets met (60/30 FPS)

---

## DELIVERABLES (6 TOTAL)

### 1. WebGL Build Pipeline
**Target**: Dec 5-8  

**Deliverable**:
- Optimized WebGL build configuration
- Build automation script
- Asset bundle strategy
- Compression configuration (gzip/brotli)
- Performance profiling results

**Configuration**:
```
IL2CPP Settings:
- Compilation: IL2CPP (mandatory for WebGL)
- Optimization: Highest
- Code stripping: Aggressive
- Managed stripping: Aggressive

Compression:
- Build compression: gzip
- Network compression: enabled
- Texture compression: WebP where possible

Performance Targets:
- Load time: < 5 seconds
- FPS: 60 on modern, 30 minimum
- Memory: < 200MB
- Build size: < 50MB
```

**Build Process**:
```bash
# Automated build script
unity -batchmode \
  -quit \
  -executeMethod BuildScript.BuildWebGL \
  -projectPath . \
  -logFile build.log
```

**Testing**:
- [ ] Load time measurement
- [ ] FPS verification on modern browser
- [ ] FPS verification on older browser
- [ ] Memory profiling
- [ ] Cross-browser testing (Chrome, Firefox, Safari)
- [ ] Mobile browser testing

**Deliverables**:
- Build configuration document
- Build automation script
- Performance report
- Browser compatibility list

---

### 2. Android Build Pipeline
**Target**: Dec 8-12  

**Deliverable**:
- Signed release APK
- Build automation script
- Keystore configuration
- Optimization settings
- Performance profiling results

**Configuration**:
```
Android Settings:
- Target API: 35 (current)
- Minimum API: 24 (Android 7.0)
- Architecture: ARM64-v8a (primary) + ARMv7 (fallback)
- Build type: Release (signed)

Optimization:
- ProGuard/R8: Enabled (aggressive)
- Code minification: Enabled
- Resource shrinking: Enabled
- IL2CPP optimization: Highest

Performance Targets:
- FPS: 60 on modern, 30 minimum
- Load time: < 3 seconds
- Memory: < 200MB
- APK size: < 100MB
```

**Keystore Setup**:
```bash
# Create release keystore
keytool -genkey -v \
  -keystore bumpu.keystore \
  -keyalg RSA \
  -keysize 2048 \
  -validity 10000 \
  -alias bumpu
```

**Build Process**:
```bash
# Automated build script
unity -batchmode \
  -quit \
  -executeMethod BuildScript.BuildAndroid \
  -projectPath . \
  -logFile build.log
```

**Testing**:
- [ ] Load time measurement (Pixel 8, Galaxy S23)
- [ ] FPS verification (modern & mid-range devices)
- [ ] Memory profiling
- [ ] APK size verification
- [ ] Input latency test
- [ ] Battery impact test
- [ ] Heat generation test

**Play Store Requirements**:
- [ ] App signing certificate
- [ ] Content rating questionnaire
- [ ] Privacy policy link
- [ ] Screenshots (6-8)
- [ ] Feature graphic (1024x500px)
- [ ] App icon (512x512px)
- [ ] Description & long description
- [ ] Release notes

---

### 3. iOS Build Pipeline
**Target**: Dec 12-15  

**Deliverable**:
- Signed release IPA
- Xcode project configuration
- Provisioning profile setup
- Code signing configuration
- Performance profiling results

**Configuration**:
```
Xcode Settings:
- Minimum iOS: 14.0
- Target: ARM64 (Apple Silicon compatible)
- Build type: Release
- Code signing: Automatic (Apple ID)

Optimization:
- Code optimization: Fastest -O3
- Metal graphics: Enabled
- Bitcode: Enabled
- Strip debugging: Enabled

Performance Targets:
- FPS: 60 on modern, 30 minimum
- Load time: < 3 seconds
- Memory: < 200MB
- IPA size: < 100MB
```

**Apple Setup**:
- [ ] Apple Developer account created
- [ ] App ID created
- [ ] Provisioning profiles generated
- [ ] Development certificate installed
- [ ] Distribution certificate installed
- [ ] Keychain configured

**Build Process**:
```bash
# Automated build script
unity -batchmode \
  -quit \
  -executeMethod BuildScript.BuildIOS \
  -projectPath . \
  -logFile build.log
# Then use Xcode to build IPA
```

**Testing**:
- [ ] Load time measurement (iPhone 15, iPhone 14)
- [ ] FPS verification (modern & older models)
- [ ] Memory profiling
- [ ] IPA size verification
- [ ] Safe area handling (notch, Dynamic Island)
- [ ] Input latency test
- [ ] Battery impact test

**App Store Requirements**:
- [ ] App privacy policy
- [ ] Screenshots (6-8, multiple resolutions)
- [ ] App icon (1024x1024px)
- [ ] Preview video (optional)
- [ ] Description & promotional text
- [ ] Keywords (5)
- [ ] Support URL
- [ ] Marketing URL (optional)
- [ ] Release notes
- [ ] Content rating (ESRB/PEGI)

---

### 4. Performance Optimization Report
**Target**: Dec 15-16  
**Lines**: ~200 documentation

**Contents**:
- FPS profiling on all platforms
- Memory usage analysis
- CPU usage analysis
- Startup time measurement
- Load time measurement
- Input latency measurement
- Optimization strategies applied
- Performance comparison vs. targets

**Performance Targets Met**:
- [ ] WebGL: 60 FPS modern, 30 FPS old browser
- [ ] Android: 60 FPS modern (Pixel 8), 30 FPS budget (Moto G54)
- [ ] iOS: 60 FPS modern (iPhone 15), 30 FPS older (iPhone SE)
- [ ] Memory: < 200MB on all platforms
- [ ] Load time: < 5 sec WebGL, < 3 sec mobile
- [ ] Input latency: < 100ms on all platforms

**Optimization Techniques**:
- Texture compression (WebP for WebGL)
- Sprite atlasing
- Draw call reduction
- Memory pooling
- Garbage collection tuning
- LOD systems (if needed)

---

### 5. Build Automation & CI/CD
**Target**: Dec 16-17  

**Deliverable**:
- Automated build scripts (WebGL, Android, iOS)
- Version management system
- Build artifact storage
- CI/CD pipeline configuration
- Automated performance testing

**Build Automation**:
```bash
#!/bin/bash
# master build script
BUILD_TYPE=$1  # webgl, android, or ios
GAME_VERSION=$2  # e.g., 1.0.0

case $BUILD_TYPE in
  webgl)
    unity -batchmode -quit \
      -executeMethod BuildScript.BuildWebGL \
      -customArg $GAME_VERSION
    ;;
  android)
    unity -batchmode -quit \
      -executeMethod BuildScript.BuildAndroid \
      -customArg $GAME_VERSION
    ;;
  ios)
    unity -batchmode -quit \
      -executeMethod BuildScript.BuildIOS \
      -customArg $GAME_VERSION
    ;;
esac
```

**Version Management**:
- Automated version bumping
- Build number tracking
- Release notes generation
- Artifact naming convention

**Performance Testing**:
- Automated FPS measurement
- Memory profiling automation
- Load time benchmarking
- Regression detection

---

### 6. Store Submission Preparation
**Target**: Dec 17-20  

**Deliverable**:
- App Store submission package
- Play Store submission package
- Store metadata (titles, descriptions, etc.)
- Screenshots & promotional materials
- Release notes & change log

**Google Play Store Submission**:
- [ ] APK signed and tested
- [ ] Content rating completed
- [ ] Privacy policy provided
- [ ] Screenshots uploaded (6-8)
- [ ] Feature graphic uploaded
- [ ] App icon uploaded
- [ ] Description & long description
- [ ] Release notes prepared
- [ ] Testing on 5+ devices verified
- [ ] Ready to submit

**Apple App Store Submission**:
- [ ] IPA built and tested
- [ ] Provisioning profiles correct
- [ ] Certificates valid
- [ ] Privacy policy provided
- [ ] Screenshots uploaded (6-8)
- [ ] App icon uploaded
- [ ] Preview video (optional)
- [ ] Description & promotional text
- [ ] Keywords set
- [ ] Support & marketing URLs
- [ ] Content rating completed
- [ ] TestFlight beta approved
- [ ] Ready to submit

**WebGL Deployment**:
- [ ] Server configured
- [ ] CORS settings correct
- [ ] Build deployed
- [ ] Performance verified
- [ ] Load test completed
- [ ] Live monitoring enabled

---

## DAILY SPRINT SCHEDULE

### Days 1-3 (Dec 5-7)
- [ ] Sprint 7 kickoff
- [ ] Review build pipeline research
- [ ] Set up build environment
- [ ] WebGL build configuration started
- **Standup**: Build environment ready, WebGL started

### Days 4-5 (Dec 8-9)
- [ ] WebGL build complete & tested
- [ ] WebGL performance profiling done
- [ ] Android build configuration started
- [ ] Android keystore created
- **Standup**: WebGL ready, Android started

### Days 6-7 (Dec 10-11)
- [ ] Android build complete & tested
- [ ] Android performance profiling done
- [ ] iOS build configuration started
- [ ] Apple certificates & profiles created
- **Standup**: Android ready, iOS started

### Days 8-9 (Dec 12-13)
- [ ] iOS build complete & tested
- [ ] iOS performance profiling done
- [ ] Performance report started
- [ ] Cross-platform testing started
- **Standup**: All builds ready, profiling in progress

### Days 10-11 (Dec 14-15)
- [ ] Performance optimization report complete
- [ ] Build automation scripts complete
- [ ] CI/CD pipeline configured
- [ ] Store submission prep started
- **Standup**: All builds optimized, store prep started

### Days 12-14 (Dec 16-18)
- [ ] Google Play submission package complete
- [ ] Apple App Store submission package complete
- [ ] Final testing on real devices
- [ ] Store metadata finalized
- **Standup**: Submission packages ready

### Days 15-16 (Dec 19-20)
- [ ] Final code review with Managing Engineer
- [ ] All systems verified
- [ ] Ready for submission
- [ ] Formal approval
- **Standup**: Ready to submit to stores

---

## QUALITY STANDARDS

**Build Standards**:
- ✅ All builds functional (tested on real devices)
- ✅ Signed correctly (APK & IPA)
- ✅ Performance targets met on all platforms
- ✅ No critical bugs in builds
- ✅ Store requirements met

**Performance Standards**:
- ✅ 60 FPS on modern devices (all platforms)
- ✅ 30 FPS minimum on older devices
- ✅ < 100ms input latency
- ✅ Memory < 200MB
- ✅ Load time < 5 sec (WebGL), < 3 sec (mobile)

**Documentation Standards**:
- ✅ Build configuration documented
- ✅ Performance results documented
- ✅ Optimization strategies documented
- ✅ Deployment guide provided
- ✅ Troubleshooting guide provided

---

## DEPENDENCIES & TESTING

**What You Need From Others**:
- ✅ Complete game from Sprint 6 (Dec 5)
- ✅ Final assets from all teams
- ✅ Game configuration finalized
- ✅ Content rating decisions from stakeholders

**Testing Devices** (minimum):
- **WebGL**: Chrome latest, Firefox latest, Safari latest
- **Android**: Pixel 8, Galaxy S23, Moto G54 (budget), Galaxy A13 (old)
- **iOS**: iPhone 15 Pro, iPhone 15, iPhone 14, iPhone SE 3rd Gen

**Testing Process**:
1. Build created on each platform
2. Installed on minimum 3 devices each
3. Full gameplay test (all modes)
4. Performance measurement
5. Edge case testing
6. Store requirements verification

---

## SUCCESS CRITERIA

✅ **WebGL build**: Functional, optimized, < 50MB  
✅ **Android build**: Signed, optimized, < 100MB, Play Store ready  
✅ **iOS build**: Signed, optimized, < 100MB, App Store ready  
✅ **Performance**: 60/30 FPS targets met on all platforms  
✅ **Memory**: < 200MB on all platforms  
✅ **Load time**: < 5 sec WebGL, < 3 sec mobile  
✅ **Input latency**: < 100ms on all platforms  
✅ **Cross-platform testing**: All devices tested  
✅ **Store requirements**: All metadata & assets ready  
✅ **Code review approved** by Managing Engineer  
✅ **Ready for App Store/Play Store submission**

---

## RESOURCES & SUPPORT

**Managing Engineer** (Amp):
- Build configuration questions: immediate response
- Code review: < 2 hours turnaround
- Blocker resolution: < 1 hour
- Contact: Direct message for urgent, #build for updates

**Reference Materials**:
- CODING_STANDARDS.md
- TEAM_DISPATCH_SPRINT7_BUILD_PREP.md (research docs)
- BUILD_PIPELINE.md (if exists)
- Platform documentation (Unity manual)

---

## GO/NO-GO DECISION

**Current**: 🟢 **GO** - Game complete, dependencies met, build tools ready

**Status**: Begin immediately. Build thoroughly. Test extensively. Optimize aggressively.

**Authority**: Full build engineering team mobilization - EXECUTE NOW

---

**From**: Amp (Managing Engineer)  
**Date**: Dec 5, 2025, 6:00 PM UTC  
**Authority**: FULL EXECUTION ORDER

---

*End of Dispatch*
