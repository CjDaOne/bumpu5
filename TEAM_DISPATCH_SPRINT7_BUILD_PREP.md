# BUILD ENGINEERING TEAM - SPRINT 7 RESEARCH PHASE
## Platform Builds & Optimization Preparation - BEGIN NOW

**From**: Amp (Managing Engineer)  
**To**: Build Engineer Agent  
**Date**: Nov 14, 2025  
**Authority**: BEGIN RESEARCH PHASE NOW  
**Target Completion**: Research complete by Dec 18 (when Sprint 6 finishes)  
**Formal Execution**: Sprint 7 starts Dec 26

---

## MISSION OVERVIEW

Research and plan multi-platform build strategy to deploy to WebGL, Android, and iOS. This enables:
- Web version (WebGL)
- Android app (Play Store)
- iOS app (App Store)
- All optimized for performance

**Current Status**: Research phase (longest lead time of all teams)

---

## BUILD RESEARCH TASKS

### Task 1: WebGL Build Pipeline Research
**Purpose**: Create optimized browser-playable version

**Research Objectives**:
```
1. Build Configuration
   - IL2CPP compilation settings
   - Compression options (gzip, brotli)
   - Optimization flags
   - Asset bundling strategy

2. Performance Targets
   - Load time < 5 seconds
   - Runtime FPS: 60 on modern, 30 minimum on old
   - Memory usage < 200MB
   - Network size < 50MB total

3. Platform Features
   - Input handling (mouse, touch)
   - Screen scaling strategies
   - Canvas settings
   - Graphics quality options

4. Testing Environment
   - Local testing setup
   - CI/CD possibilities
   - Multiple browser testing

5. Deployment
   - Hosting options
   - Build automation
   - Version updates strategy
```

**Research Questions**:
- What IL2CPP settings give best performance?
- How much compression do we need?
- What texture quality for web?
- How to handle shader variants?
- Best practices for WebGL build size?
- Compatibility with older browsers?

**Deliverable**: WebGL build research document:
```
1. Recommended Build Settings
   - IL2CPP settings
   - Compression approach
   - Optimization checklist
   - Texture compression (WebP, etc.)

2. Performance Optimization Strategy
   - Asset size targets
   - Memory management
   - Shader optimization
   - Loading sequence

3. Testing Plan
   - Browsers to test (Chrome, Firefox, Safari)
   - Devices to test (desktop, mobile browsers)
   - Performance profiling approach
   - Regression testing

4. Deployment Strategy
   - Hosting recommendation
   - CI/CD pipeline outline
   - Version management
   - Update strategy

5. Known Limitations
   - WebGL capabilities vs native
   - Performance expectations
   - Unsupported features (if any)
```

**Target**: Complete by Nov 28

---

### Task 2: Android Build Pipeline Research
**Purpose**: Create Play Store-ready APK

**Research Objectives**:
```
1. Build Configuration
   - Target API level (current minimum)
   - Architecture support (ARMv7, ARM64, x86)
   - Build type (Debug, Release, Signed)
   - ProGuard/R8 minification strategy

2. Signing & Keystore
   - How to create release keystore
   - Key alias and password management
   - Keystore security best practices
   - Version code management

3. Performance Targets
   - FPS: 60 on modern devices, 30 minimum
   - Load time < 3 seconds
   - Memory < 200MB
   - APK size < 100MB

4. Input Handling
   - Touch input best practices
   - Back button handling
   - System navigation integration
   - Controller support (if needed)

5. Screen Handling
   - Safe area implementation (notches)
   - Orientation handling (portrait/landscape)
   - Resolution scaling
   - DPI scaling

6. Google Play Requirements
   - App signing requirements
   - Privacy policy
   - Content rating classification
   - Store listing requirements

7. Testing
   - Device testing plan
   - Version testing matrix
   - Performance testing
   - Regression testing
```

**Research Questions**:
- What's minimum API level we should support?
- How to optimize for different device architectures?
- What's signing/keystore process?
- How to manage version numbers?
- Play Store submission requirements?
- Testing on real devices vs emulator?
- What about app permissions needed?

**Deliverable**: Android build research document:
```
1. Recommended Build Settings
   - Minimum API level
   - Target API level
   - Architecture support
   - Build optimization flags
   - Signing configuration

2. Keystore & Signing Strategy
   - How to create keystore
   - Key management best practices
   - Security considerations
   - Backup strategy

3. Performance Optimization
   - APK size targets
   - Memory optimization
   - Graphics quality options
   - Loading strategy

4. Google Play Submission
   - Required assets
   - Listing information
   - Content rating
   - Privacy policy
   - Screenshots/store images

5. Testing Strategy
   - Device matrix (Samsung, Google Pixel, OnePlus, etc.)
   - OS versions to test (API 28, 30, 33, 35+)
   - Performance profiling tools
   - Regression testing

6. Input & Screen Handling
   - Touch input implementation
   - Safe area handling
   - Orientation support
   - Resolution scaling

7. Known Issues & Workarounds
   - Common Android problems
   - Unity-specific gotchas
   - Device-specific quirks
```

**Target**: Complete by Nov 30

---

### Task 3: iOS Build Pipeline Research
**Purpose**: Create App Store-ready IPA

**Research Objectives**:
```
1. Build Configuration
   - Xcode project settings
   - Signing & provisioning profiles
   - Build architecture (ARM64, etc.)
   - Development vs Release builds

2. Signing & Provisioning
   - Apple Developer account requirements
   - Certificate creation process
   - Provisioning profile management
   - Device registration

3. Performance Targets
   - FPS: 60 on modern devices, 30 minimum
   - Load time < 3 seconds
   - Memory < 200MB
   - IPA size < 100MB (over cellular limit 150MB)

4. App Store Requirements
   - Privacy policy requirements
   - Content rating classification
   - Screenshots for store listing
   - App icon specifications
   - Version numbering

5. Screen & Input Handling
   - Safe area implementation (notch, Dynamic Island)
   - Portrait/landscape orientation
   - Touch input best practices
   - Gestures (if needed)

6. Testing
   - Device testing (iPhone models, iOS versions)
   - Xcode simulator vs real device
   - Performance testing
   - Regression testing

7. App Store Review Guidelines
   - Common rejection reasons
   - What Apple looks for
   - Review timeframe expectations
```

**Research Questions**:
- What's the iOS App Store submission process?
- What provisioning profiles do we need?
- How to handle code signing?
- What are certificate/keychain requirements?
- App Store review typical timeline?
- What's the minimum iOS version?
- Safe area & notch handling strategy?
- What about TestFlight for beta testing?

**Deliverable**: iOS build research document:
```
1. Apple Developer Account Setup
   - Registration process
   - Certificate creation
   - App ID creation
   - Team management

2. Xcode Project Configuration
   - Build settings
   - Signing & capabilities
   - Framework dependencies
   - Code signing configuration

3. Provisioning Profile Management
   - Development profiles
   - Distribution profiles
   - Ad-hoc testing profiles
   - Lifecycle management

4. Performance Optimization
   - IPA size targets
   - Memory optimization
   - Graphics quality
   - Loading strategy

5. App Store Submission
   - Required metadata
   - Screenshots specifications
   - Icon specifications
   - Privacy policy requirements
   - Rating classification

6. Screen & Input Handling
   - Safe area implementation
   - Notch/Dynamic Island handling
   - Portrait/landscape support
   - Touch input strategy

7. Testing Strategy
   - Device matrix (iPhone X, 13, 14, 15, etc.)
   - iOS version matrix (15, 16, 17, 18)
   - TestFlight beta testing
   - Performance profiling

8. Review Guidelines
   - Common rejection reasons
   - Best practices
   - Review timeline
   - Appeal process
```

**Target**: Complete by Dec 2

---

### Task 4: Platform-Specific Input Handling
**Purpose**: Design input strategy for all platforms

**Research Objectives**:
```
1. Touch Input (Mobile)
   - Single tap handling
   - Multi-touch (if needed)
   - Gesture recognition (swipe, pinch, long-press)
   - Touch responsiveness tuning
   - Visual feedback for touches

2. Mouse/Keyboard (Desktop/Web)
   - Click detection
   - Drag & drop (if needed)
   - Keyboard input (if any)
   - Hover effects
   - Cursor visibility

3. Input Consistency
   - Same game behavior across platforms
   - Touch targets ≥44px
   - No platform-specific edge cases
   - Unified input mapper

4. Edge Cases
   - Back button (Android)
   - Home button (iOS)
   - Alt-tab (Web)
   - Screen rotation during play
   - App backgrounding/resuming

5. Performance
   - Input latency minimization
   - Touch polling rate
   - Input event batching
   - No frame drops on input
```

**Deliverable**: Input handling research document:
```
1. Input Strategy Architecture
   - Unified input mapper
   - Platform abstraction layer
   - Event dispatching system

2. Touch Input Implementation
   - Single touch handling
   - Multi-touch possibilities
   - Gesture recognition approach
   - Touch target sizing (≥44px)

3. Mouse/Keyboard Implementation
   - Click handling
   - Keyboard mapping (if any)
   - Hover state management
   - Cursor handling

4. Platform-Specific Handling
   - Android back button
   - iOS safe area
   - Web browser considerations
   - Orientation changes

5. Performance Optimization
   - Input latency targets
   - Polling strategy
   - Event batching
   - Profiling approach

6. Cross-Platform Testing
   - Device matrix per platform
   - Input lag measurement
   - Edge case validation
   - User feedback collection
```

**Target**: Complete by Dec 5

---

### Task 5: Performance Profiling Strategy
**Purpose**: Define how to measure and optimize performance

**Research Objectives**:
```
1. Performance Metrics
   - FPS (frames per second)
   - Memory usage
   - CPU usage
   - Startup time
   - Load time per screen
   - Input latency

2. Profiling Tools
   - Unity Profiler
   - Platform-specific tools (Android Profiler, Xcode Instruments)
   - Browser profiling (Chrome DevTools for WebGL)
   - Third-party tools

3. Optimization Targets
   - 60 FPS on modern devices
   - 30 FPS minimum on old devices
   - Startup < 3 seconds
   - Memory < 200MB
   - No frame drops on input

4. Continuous Profiling
   - Automated performance testing
   - Regression detection
   - Performance budgets
   - CI/CD integration

5. Device-Specific Testing
   - High-end device expectations
   - Mid-range device targets
   - Low-end device minimums
   - Older vs newer OS versions
```

**Deliverable**: Performance profiling research document:
```
1. Performance Metrics Definition
   - FPS targets per device
   - Memory budgets
   - Startup time targets
   - Load time targets

2. Profiling Tools Setup
   - Unity Profiler configuration
   - Android Profiler setup
   - Xcode Instruments setup
   - Chrome DevTools setup (WebGL)

3. Optimization Checklist
   - Texture optimization
   - Shader optimization
   - Asset bundling
   - Memory management
   - Draw call reduction

4. Device Testing Matrix
   - Devices to profile on
   - OS versions to test
   - Expected performance per device
   - Acceptable variance

5. Regression Testing
   - Automated performance tests
   - CI/CD integration
   - Thresholds and alerts
   - Performance budgets

6. Reporting & Tracking
   - Performance dashboard
   - Trend analysis
   - Bottleneck identification
```

**Target**: Complete by Dec 7

---

### Task 6: Build Optimization Checklist
**Purpose**: Comprehensive optimization guide for final builds

**Research Objectives**:
```
1. Asset Optimization
   - Texture compression
   - Audio compression
   - Sprite atlasing
   - Model optimization
   - Animation compression

2. Code Optimization
   - IL2CPP settings
   - Code stripping
   - Managed code optimization
   - Scripting backend selection

3. Graphics Optimization
   - Quality settings per device
   - LOD (Level of Detail) strategy
   - Draw call reduction
   - Shader variants
   - Particle optimization

4. Memory Optimization
   - Memory pooling strategy
   - Object reuse patterns
   - Garbage collection tuning
   - Memory profiling approach

5. Build Size Reduction
   - Unused asset removal
   - Code minification
   - Compression strategies
   - Platform-specific builds

6. Deployment Optimization
   - Update strategy (if needed)
   - Delta updates
   - Asset streaming
   - Lazy loading
```

**Deliverable**: Optimization checklist:
```
PRE-BUILD CHECKLIST
- [ ] All unused assets removed
- [ ] Textures compressed & formatted
- [ ] Audio optimized
- [ ] Scripts profiled & optimized
- [ ] Memory leaks identified & fixed

BUILD CONFIGURATION CHECKLIST
- [ ] IL2CPP settings optimized
- [ ] Code stripping enabled
- [ ] Unused features disabled
- [ ] Platform-specific builds created
- [ ] Version numbers set

ASSET OPTIMIZATION CHECKLIST
- [ ] Texture compression verified
- [ ] Sprite atlases created
- [ ] LOD systems implemented
- [ ] Particle count optimized
- [ ] Animation optimized

TESTING CHECKLIST
- [ ] Performance profiled
- [ ] FPS targets met
- [ ] Memory usage acceptable
- [ ] Load times acceptable
- [ ] Regression tests passed
- [ ] All platforms tested

POST-BUILD CHECKLIST
- [ ] Build sizes acceptable
- [ ] Store requirements met
- [ ] Signing/provisioning correct
- [ ] Store metadata complete
- [ ] Ready for submission
```

**Target**: Complete by Dec 10

---

## RESEARCH DOCUMENT TEMPLATE

For each research task, create a document with:

```markdown
## [Platform/Topic] Research

### Overview
[High-level explanation of what this covers]

### Key Findings
- [Finding 1]
- [Finding 2]
- [Finding 3]

### Recommended Configuration
[Specific settings/approach we should use]

### Performance Targets
- [Metric]: [Target value]
- [Metric]: [Target value]

### Testing Strategy
- [How to test]
- [Devices to test]
- [Metrics to measure]

### Potential Issues & Workarounds
- [Issue]: [Workaround]
- [Issue]: [Workaround]

### Tools & Resources
- [Tool]: [Purpose]
- [Resource]: [Why useful]

### Timeline & Next Steps
- [Phase 1]: [Timeline]
- [Phase 2]: [Timeline]

### Open Questions
- [Question to resolve]
- [Question to resolve]
```

---

## REFERENCE MATERIALS

**Project requirements**: Review game mode specs to understand what needs to be performant
- SPRINT_3_DETAILED_BRIEFING.md
- Understand any platform-specific features needed

**Code standards**: CODING_STANDARDS.md for consistency

---

## DAILY PROGRESS REPORTING

Report at 9 AM UTC standup:
- ✅ Completed since yesterday
- 🔄 In progress today
- 🚫 Blockers
- 📈 % complete
- 📋 Research documents ready

**Example Day 1**:
> ✅ Completed: None (kickoff day)  
> 🔄 In Progress: WebGL pipeline research  
> 🚫 Blockers: None  
> 📈 Progress: 10%  
> 📋 Research docs: 0/6 ready

---

## SUCCESS CRITERIA

✅ **Complete research** for all 3 platforms  
✅ **Research documents** for all 6 tasks  
✅ **Platform-specific implementation plan** for each  
✅ **Performance optimization strategy** defined  
✅ **Input handling approach** confirmed  
✅ **Ready to implement** Dec 26 (no research gaps)

---

## MANAGING ENGINEER SUPPORT

Available for:
- Platform-specific questions
- Performance target clarification
- Build pipeline architecture review
- Research validation & feedback

Contact: Direct message for urgent, #build for updates

---

## TIMELINE

| Date | Phase | Target |
|------|-------|--------|
| Nov 14-28 | Research start | WebGL & Android research |
| Nov 28-Dec 5 | Research progress | iOS & input handling |
| Dec 5-10 | Research completion | Performance & optimization |
| Dec 10-17 | Research review | Final planning |
| Dec 18 | Ready for execution | Sprint 6 complete, Sprint 7 ready |
| Dec 26 | Sprint 7 kickoff | Begin implementation |

---

## NEXT: FORMAL SPRINT 7 EXECUTION

When Sprints 3-6 complete, I'll issue TEAM_DISPATCH_SPRINT7_BUILD_EXECUTE.md with:
- Detailed build tasks per platform
- Specific configuration files
- Testing requirements
- Store submission checklist

Your research work now ensures Sprint 7 execution is smooth and all platforms build correctly.

---

**Status**: BEGIN RESEARCH PHASE NOW  
**Authority**: FULL BUILD OWNERSHIP  
**Deadline**: Research complete Dec 18  
**Impact**: Enables multi-platform release, App Store/Play Store submissions

**You're authorized to proceed. Begin immediately. Research thoroughly. Deliver on time.**

---

**From**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025

---

*End of Dispatch*
