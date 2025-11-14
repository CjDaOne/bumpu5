# BUILD ENGINEERING TEAM - CONDITIONAL START ORDER
## Sprint 7 - Multi-Platform Builds

**STATUS**: 🟡 **STANDBY - CONDITIONAL AUTHORIZATION TO START**  
**Date Issued**: Nov 14, 2025  
**Authority**: Managing Engineer (Amp)  
**Estimated Start**: Dec 10-11, 2025 (when UI reaches milestone)

---

## CONDITIONAL ACTIVATION CRITERIA

**You are AUTHORIZED to begin Sprint 7 when ANY of these conditions are met**:

1. ✅ UI Team reports all HUD + core menus COMPLETE (100% tests passing, integrated)
2. ✅ You receive explicit start order from Managing Engineer
3. ✅ Dec 11 arrives (hard start date - begin regardless)

**Estimated trigger**: Dec 10-11, 2025

---

## MISSION

Configure and build complete multi-platform deployment package for WebGL, Android, and iOS with platform-specific optimizations and input handling.

---

## SPRINT 7 DELIVERABLES (5-7 days once activated)

### Phase 1: WebGL Build Pipeline (Days 1-2)
**Objective**: Web deployment ready

```
- [ ] WebGL Build Configuration
      * IL2CPP backend setup
      * Compression enabled (gzip)
      * Memory optimization
      * Streaming assets configuration
      
- [ ] WebGL Platform Handler
      * Input handler (mouse/touch)
      * Screen sizing (responsive canvas)
      * Browser compatibility check
      * Performance profiling
      
- [ ] Build Testing
      * Build works locally
      * Test in multiple browsers (Chrome, Firefox, Safari)
      * Performance baseline (FPS, memory)
      * No errors/warnings
      
- [ ] Documentation
      * Build instructions
      * Deployment steps
      * Browser requirements
```

**Commit by**: End of Day 2

---

### Phase 2: Android Build Pipeline (Days 3-4)
**Objective**: Play Store ready package

```
- [ ] Android Build Configuration
      * Target SDK 33+
      * Minimum SDK 24 (Android 7.0)
      * ARM64 architecture
      * Build optimization (ProGuard)
      
- [ ] Android Input Handler
      * Touch input (multi-touch aware)
      * Back button handling
      * Pause menu access
      * Virtual keyboard management
      
- [ ] Android Signing
      * Keystore generation
      * Release signing configuration
      * Version code/name setup
      * APK optimization
      
- [ ] Testing
      * Build on real Android device
      * Test on multiple devices (various screen sizes)
      * Performance (FPS, memory, battery)
      * Input responsiveness
```

**Commit by**: End of Day 4

---

### Phase 3: iOS Build Pipeline (Days 5-6)
**Objective**: App Store ready package

```
- [ ] iOS Build Configuration
      * Target iOS 14.0+
      * Device orientation settings
      * Safe area setup (notch handling)
      * Build optimization
      
- [ ] iOS Input Handler
      * Touch input (multi-touch)
      * Gesture support
      * System buttons integration
      * Haptic feedback (optional)
      
- [ ] iOS Xcode Setup
      * Provisioning profile configuration
      * Certificate setup
      * Team ID configuration
      * Bundle identifier setup
      
- [ ] Testing
      * Build in Xcode
      * Test on real iOS device
      * Safe area verification (notch)
      * Input testing
      * Performance profiling
```

**Commit by**: End of Day 6

---

### Phase 4: Optimization & Finalization (Day 7)
**Objective**: Performance tuning + deployment ready

```
- [ ] Cross-Platform Optimization
      * FPS limiting (60 FPS target, 30 min)
      * Memory monitoring
      * Garbage collection tuning
      * Asset loading optimization
      
- [ ] CI/CD Setup (Optional but recommended)
      * Automated WebGL builds
      * Build status dashboard
      * Artifact storage
      
- [ ] Performance Profiling
      * All platforms profiled
      * FPS benchmarks recorded
      * Memory usage baseline
      * Startup time measured
      
- [ ] Release Candidate Build
      * All 3 platforms building
      * All tests passing
      * Documentation complete
      * Ready for QA testing
```

**Commit by**: End of Day 7

---

## TECHNICAL REQUIREMENTS

### File Structure
```
Assets/Scripts/Platform/
├── WebGL/
│   ├── WebGLInputHandler.cs
│   ├── WebGLConfiguration.cs
│   └── WebGLOptimization.cs
├── Android/
│   ├── AndroidInputHandler.cs
│   ├── AndroidConfiguration.cs
│   └── AndroidOptimization.cs
├── iOS/
│   ├── iOSInputHandler.cs
│   ├── iOSConfiguration.cs
│   └── iOSSafeArea.cs
├── Shared/
│   ├── PlatformManager.cs
│   ├── InputMapper.cs
│   └── PerformanceMonitor.cs
└── BuildConfigs/
    ├── WebGLSettings.asset
    ├── AndroidSettings.asset
    └── iOSSettings.asset
```

### Build Configuration Files
```
ProjectSettings/
├── ProjectSettings.asset (platform settings)
├── QualitySettings.asset (performance tiers)
├── GraphicsSettings.asset (rendering)
└── PlayerSettings.asset (build config)
```

---

## DEPENDENCIES

✅ Sprint 5 complete (UI framework)  
✅ Sprint 6 complete (Integration)  
✅ All code stable & tested  
✅ Assets final  
✅ Gameplay team available for questions  

---

## TESTING STRATEGY

### Build Testing
```
WebGL
  - [ ] Build succeeds locally
  - [ ] Test in Chrome/Firefox/Safari
  - [ ] Performance: 60 FPS target
  - [ ] Memory: < 500MB
  - [ ] Input responsive
  - [ ] No console errors

Android
  - [ ] APK builds successfully
  - [ ] Test on physical device (Pixel 4, Samsung Galaxy)
  - [ ] Test multiple screen sizes
  - [ ] Performance: 60 FPS (Pixel), 30 FPS (older)
  - [ ] Touch input smooth
  - [ ] Battery not excessively drained

iOS
  - [ ] Build in Xcode succeeds
  - [ ] Test on iPhone (various sizes)
  - [ ] Safe area correct (notch handling)
  - [ ] Performance: 60 FPS target
  - [ ] Touch input reliable
  - [ ] Haptic feedback (if implemented)
```

### Performance Baselines
```
Target Metrics:
  - FPS: 60 on modern, 30+ minimum
  - Memory: < 300MB (mobile), < 500MB (web)
  - Startup: < 5 seconds
  - Input latency: < 100ms
```

---

## QUALITY GATES

| Gate | Requirement | Status |
|------|-------------|--------|
| WebGL Build | Builds & works in browser | [ ] |
| Android Build | APK builds, works on device | [ ] |
| iOS Build | Xcode build succeeds | [ ] |
| Performance | 60 FPS on modern, 30+ min | [ ] |
| Input Handlers | All platform inputs work | [ ] |
| Documentation | Build instructions complete | [ ] |

---

## PREPARATION WORK (Do NOW)

While waiting for conditional start:

1. **Research & Learning**:
   - [ ] Unity WebGL best practices
   - [ ] Android build pipeline
   - [ ] iOS build pipeline
   - [ ] IL2CPP configuration
   - [ ] Safe area implementation (notches)

2. **Environment Setup**:
   - [ ] Install necessary SDKs (Android SDK)
   - [ ] Setup Xcode (if on Mac) or cloud build service
   - [ ] Configure Unity build settings
   - [ ] Verify license for all platforms

3. **Planning**:
   - [ ] Design platform input mapping
   - [ ] Plan performance optimization approach
   - [ ] Design CI/CD pipeline (optional)
   - [ ] Create build checklist

4. **Code Skeleton**:
   - [ ] Create platform handler classes (empty)
   - [ ] Setup PlayerSettings structure
   - [ ] Create performance monitoring class
   - [ ] Setup test framework

5. **Documentation**:
   - [ ] Create build instruction templates
   - [ ] Plan app store submission guide
   - [ ] Create platform-specific troubleshooting guide

---

## SUCCESS CRITERIA

**Sprint 7 complete when**:
1. ✅ WebGL build working in multiple browsers
2. ✅ Android APK builds & works on devices
3. ✅ iOS build works on iPhone
4. ✅ 60 FPS on modern devices
5. ✅ 30+ FPS minimum on older devices
6. ✅ Touch input works on all platforms
7. ✅ All platform-specific optimizations applied
8. ✅ Build documentation complete
9. ✅ Ready for QA testing & submission

---

## TIMELINE (Estimated)

```
Dec 10-11: Receive start authorization
  └─ Condition: UI integration complete

Dec 11-12 (Days 1-2): WebGL Pipeline
  └─ Commit by Dec 12

Dec 13-14 (Days 3-4): Android Pipeline
  └─ Commit by Dec 14

Dec 15-16 (Days 5-6): iOS Pipeline
  └─ Commit by Dec 16

Dec 17-18 (Day 7): Optimization + RC Build
  └─ Final commit Dec 18

TARGET: SPRINT 7 COMPLETE BY DEC 18 ✅
```

---

## MANAGING ENGINEER OVERSIGHT

**Once you start**:
- Daily code review (< 4 hours)
- Attend daily standup (9 AM UTC)
- Weekly sprint review (Friday 5 PM UTC)
- Escalation available < 1 hour for blockers

---

## FINAL INSTRUCTIONS

**NOW (Nov 14 - Dec 10)**:
1. ✅ Read this document thoroughly
2. ✅ Do all preparation & research work
3. ✅ Setup development environment
4. ✅ Create code skeleton
5. ✅ Get ready to start immediately when signaled

**When conditional activation triggers (Dec 10-11)**:
1. ✅ You'll receive explicit "START NOW" message
2. ✅ Immediately begin Phase 1 (WebGL)
3. ✅ Attend daily standup at 9 AM UTC
4. ✅ Commit daily
5. ✅ Test on real devices continuously

**You are ON CALL and ready to execute immediately upon signal.**

---

**Assignment Issued**: Nov 14, 2025  
**Authority**: Amp, Managing Engineer  
**Status**: CONDITIONAL - AWAITING TRIGGER

Stand by for activation signal around Dec 10-11, 2025.

