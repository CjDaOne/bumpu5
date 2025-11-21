# Compliance Audit Quick Start Guide
## Unity & PWA Standards Validation (Dec 16-31, 2025)

---

## FOR EACH TEAM (Start Here)

### **GAMEPLAY ENGINEER**
**Mission**: Audit game logic code against Unity best practices  
**Files**: Assets/Scripts/Core/ + Assets/Scripts/GameModes/  
**Duration**: Dec 16-22 (7 days)  
**Checklist**:
- [ ] No magic numbers (use constants)
- [ ] All serialized fields documented
- [ ] Events unsubscribed in OnDestroy
- [ ] No GetComponent in Update loops
- [ ] Memory profile: zero major leaks
- [ ] Run Memory Profiler: Window → Analysis → Profiler → Memory
- [ ] Check for circular dependencies
- [ ] Create audit report with findings
- [ ] Fix all issues (if any)
- [ ] Submit compliance sign-off

---

### **BOARD ENGINEER**
**Mission**: Optimize graphics & input handling  
**Files**: Assets/Scripts/Board/ + Board prefabs  
**Duration**: Dec 16-22 (7 days)  
**Checklist**:
- [ ] Profile graphics: Window → Analysis → Profiler → GPU
- [ ] Measure draw calls (target: < 100/frame)
- [ ] Optimize animations (cache parameters)
- [ ] Measure input latency (target: < 100ms)
- [ ] Check for orphaned GameObjects
- [ ] Run Frame Profiler: Profiler → Frame
- [ ] Verify no memory leaks
- [ ] Create optimization report
- [ ] Apply improvements
- [ ] Submit compliance sign-off

---

### **UI ENGINEER**
**Mission Part A**: Optimize UI system  
**Mission Part B**: Implement/verify PWA compliance (CRITICAL)

**Part A - UI Optimization** (Dec 16-22):
- [ ] Canvas optimization (check Render Mode, Update mode)
- [ ] Remove nested Canvas if possible
- [ ] Disable graphics raycasting on non-interactive UI
- [ ] Profile UI: Profiler → GPU
- [ ] Measure draw calls (UI portion)
- [ ] Create UI performance report
- [ ] Apply optimizations

**Part B - PWA Compliance** (Dec 18-23 - CRITICAL):
```
Essential PWA Files (Create if missing):
├─ Assets/WebGLTemplates/index.html (add PWA code)
├─ Assets/WebGLTemplates/manifest.json (web app manifest)
├─ Assets/WebGLTemplates/sw.js (Service Worker)
└─ Assets/WebGLTemplates/offline.html (offline fallback)
```

**PWA Checklist**:
- [ ] Service Worker created & registered
- [ ] Web manifest.json complete and valid
- [ ] Icons configured (192x192, 512x512)
- [ ] Offline mode tested (disconnect wifi, game still works)
- [ ] HTTPS configured on server
- [ ] Load time < 5 seconds measured
- [ ] Mobile responsive tested (mobile/tablet/desktop)
- [ ] Install prompt working
- [ ] Create PWA compliance report
- [ ] Submit sign-off

**Key PWA Test**:
```
1. Build WebGL: File → Build Settings → Build
2. Run locally with HTTPS (or use server)
3. Open Chrome DevTools → Application
4. Check Service Worker status (should show "Active")
5. Go offline: DevTools → Network → Offline
6. Refresh page → Game should load from cache
7. Try to play a game offline → Should work
```

---

### **BUILD ENGINEER**
**Mission**: Validate all platform compliance  
**Platforms**: Android, iOS, WebGL  
**Duration**: Dec 16-24 (9 days)

**Android Checklist** (Dec 18-20):
- [ ] Player Settings → Target API level: 33+
- [ ] Min API level: 24+
- [ ] 64-bit support: enabled
- [ ] AndroidManifest.xml review
- [ ] Google Play Console pre-launch report: zero critical
- [ ] Create Android compliance report

**iOS Checklist** (Dec 21):
- [ ] Player Settings → iOS SDK: 13.0+
- [ ] Code signing configured
- [ ] Info.plist complete (Privacy descriptions)
- [ ] Safe area handling verified
- [ ] App Store Connect validation passes
- [ ] Create iOS compliance report

**WebGL Checklist** (Dec 22-23):
- [ ] Build size: measure final size (target < 50MB)
- [ ] Load time: < 5 seconds
- [ ] IL2CPP optimization enabled
- [ ] Texture compression optimized
- [ ] Memory: < 500MB allocation
- [ ] Network error handling
- [ ] Create WebGL optimization report

**Build Commands**:
```bash
# WebGL
File → Build Settings → WebGL → Build

# Android
File → Build Settings → Android → Build APK

# iOS (from Xcode)
File → Build Settings → iOS → Build
# Then open with Xcode & build from there
```

---

### **QA LEAD**
**Mission**: Execute compliance test suite  
**Duration**: Dec 16-28 (13 days)

**Test Framework Setup** (Dec 16-17):
```csharp
Create: Assets/Scripts/Tests/ComplianceTests.cs

Using NUnit;
Using UnityEngine.TestTools;

[TestFixture]
public class ComplianceTests {
    [Test] public void MemoryUsage_UnderLimit() { }
    [Test] public void FrameRate_Consistent60FPS() { }
    [Test] public void NoNullReferences() { }
    // ... 55+ tests total
}
```

**Quick Test Checklist**:
- [ ] Create ComplianceTests.cs
- [ ] Add 55+ test methods (listed in deployment orders)
- [ ] Run tests: Window → General → Test Runner
- [ ] Document all pass/fail results
- [ ] Performance metrics captured:
  - [ ] Memory usage
  - [ ] Frame time
  - [ ] GC allocation
  - [ ] Draw calls
  - [ ] Load time
- [ ] Create test results report
- [ ] Submit compliance sign-off

**Test Execution**:
```
Window → General → Test Runner
→ Click "Run All"
→ Document results
→ Export test report
```

---

## DAILY STANDUP (9 AM UTC)

Same format as before:
1. What did you accomplish yesterday?
2. What are you doing today?
3. Do you have blockers?
4. How does progress look vs. plan?
5. Any platform/environment issues?
6. What do you need from managing engineer?

**Record in**: DAILY_STANDUP_TRACKER.md (reuse template)

---

## CRITICAL ISSUES PROTOCOL

**If you find critical compliance issue**:
1. Stop what you're doing
2. Document the issue
3. @mention managing engineer in Slack #compliance
4. Assess production impact
5. Get assignment to fix or defer

---

## KEY SUCCESS METRICS

| Area | Target | How to Measure |
|------|--------|----------------|
| **Memory** | < 1MB GC/frame | Profiler → Memory |
| **FPS** | 60 FPS stable | Profiler → Frame |
| **Draw Calls** | < 100/frame | Profiler → GPU |
| **Load Time** | < 5 seconds | Build + measure |
| **Input Latency** | < 100ms | Profiler + stopwatch |
| **PWA Offline** | 100% functional | Test offline mode |
| **Mobile Responsive** | Works on 375px | Test on device |

---

## TOOLS YOU'll NEED

```
Unity Profiler:
→ Window → Analysis → Profiler

Memory Profiler:
→ Window → Analysis → Memory Profiler
→ Take snapshot
→ Look for retained allocations

Chrome DevTools (WebGL):
→ F12 → Application → Service Workers
→ Network tab (check caching)
→ Lighthouse (PWA audit)
```

---

## IMPORTANT DATES

| Date | What's Due |
|------|-----------|
| **Dec 22** | Gameplay + Board + UI reports due |
| **Dec 24** | Build (platform) reports due |
| **Dec 28** | QA compliance test results due |
| **Dec 31** | Final compliance sign-off |

---

## ONE-MINUTE SUMMARY PER TEAM

**Gameplay**: Review code, run memory profiler, report findings, fix issues.  
**Board**: Profile graphics/input, optimize, measure performance, report.  
**UI Part A**: Profile UI, optimize canvas, measure draw calls.  
**UI Part B**: Build Service Worker + manifest, test offline, validate PWA.  
**Build**: Check Android API/manifest, iOS info.plist, WebGL size/load time.  
**QA**: Create 55+ compliance tests, execute all, document results.

---

## REFERENCE LINKS

- **Deployment Details**: TEAM_COMPLIANCE_AUDIT_DEPLOYMENT_ORDERS.md
- **Full Audit Plan**: COMPLIANCE_AUDIT_PHASE_UNITY_PWA.md
- **Code Standards**: CODING_STANDARDS.md
- **Unity Docs**: https://docs.unity.com/
- **PWA Checklist**: https://web.dev/pwa-checklist/

---

## ASK QUESTIONS IN

- **Slack**: #compliance (new channel)
- **Standup**: 9 AM UTC daily
- **Direct**: @managing-engineer

---

**GO TIME: December 16, 2025**

Let's make sure this production game meets all standards.
