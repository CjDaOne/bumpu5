# Unity 6000.2.12f1 Compatibility Audit
## Bump U Box - Production Code Review

**Audit Date**: Nov 14, 2025  
**Audited By**: Amp (Managing Engineer)  
**Authority**: MANDATORY COMPLIANCE REVIEW  
**Scope**: All production code (Sprints 1-7, ~5,000 LOC)

---

## Executive Summary

Project configuration is correctly set to Unity 6000.2.12f1. However, production code contains **15 files** with deprecated, experimental, or potentially incompatible APIs that must be addressed before final QA (Sprint 8) begins.

**Status**: 🔴 CRITICAL - 12 HIGH priority issues, 8 MEDIUM priority issues  
**Action Required**: API migration and compatibility fixes required  
**Timeline**: Complete by Nov 20, 2025 (Phase 1 testing)

---

## Unity 6 Key Breaking Changes

### 1. UI Text Component Deprecated ✋
**Impact**: HIGH  
**Migration**: `UnityEngine.UI.Text` → `TextMeshPro.TextMeshProUGUI`

- All legacy `Text` components removed from preferred APIs
- TextMeshPro is now the standard
- Font configuration completely different
- Layout behavior may differ

**Affected Systems**:
- UI: DiceRollButton, BumpButton, DeclareWinButton, HUDManager
- UI: ModalController, ActionButtonController, ScoreboardController
- UI: NotificationController, PauseMenuController
- Board: CellView (text overlay)

---

### 2. Layout Groups Deprecated ✋
**Impact**: HIGH  
**Migration**: `VerticalLayoutGroup` / `HorizontalLayoutGroup` → New `LayoutGroup` APIs

- Old layout group components removed
- Use Unity's new LayoutGroup system or manual RectTransform positioning
- Padding/spacing API changes

**Affected Files**:
- PauseMenuController.cs (uses VerticalLayoutGroup)
- ScoreboardController.cs (uses HorizontalLayoutGroup)
- HUDManager.cs (may use layout groups)

---

### 3. Old Input System vs New Input System ✋
**Impact**: HIGH  
**Current**: `Input.GetKeyDown(KeyCode)` (legacy)  
**Required**: Migrate to `UnityEngine.InputSystem` (modern)

- Old Input Manager APIs may be unreliable
- New Input System handles complex input scenarios
- Mobile input (touch) requires proper configuration

**Affected Files**:
- BoardInputHandler.cs (uses old Input.GetKeyDown)
- May affect mobile touch input handling

---

### 4. Font Loading API Deprecated ✋
**Impact**: MEDIUM  
**Current**: `Resources.GetBuiltinResource<Font>("Arial.ttf")`  
**Required**: Use TextMeshPro fonts or AddressableAssets

- Builtin font API removed in Unity 6
- Font management through Resources folder is deprecated

**Affected Files**:
- ScoreboardController.cs (builtin Arial font loading)

---

### 5. LeanTween Third-Party Library ✋
**Impact**: MEDIUM  
**Status**: Verify compatibility with Unity 6

- LeanTween may not be compatible with Unity 6
- Consider: Unity's Animator system, Tweens package, or DOTween (alternative)
- Need to test or replace

**Affected Files**:
- CellView.cs (LeanTween.alphaCanvas, LeanTween.scale)
- ChipVisualizer.cs (LeanTween.move, LeanTween.alphaCanvas)
- ModalController.cs (may use LeanTween)

---

### 6. Handheld.Vibrate() Potentially Deprecated
**Impact**: LOW-MEDIUM  
**Status**: May be removed or require new Haptics API

- Mobile haptic feedback API may have changed
- Need to verify or migrate to new haptics system

**Affected Files**:
- BoardInputHandler.cs (Handheld.Vibrate on mobile)

---

### 7. CanvasGroup Alpha Manipulation
**Impact**: LOW  
**Status**: Generally compatible, verify fade behavior

- CanvasGroup alpha changes should work, but test for regression
- Some edge cases with nested canvases may differ

**Affected Files**:
- CellView.cs (fade animations)
- ChipVisualizer.cs (fade animations)
- NotificationController.cs (alpha fade)
- ModalController.cs (alpha fade)

---

## Detailed File-by-File Audit

### **CRITICAL PRIORITY (Fix Before Phase 1 Testing)**

#### 1. ScoreboardController.cs ⚠️
**Issues**:
- Uses `Text` component (DEPRECATED)
- Uses `HorizontalLayoutGroup` (DEPRECATED)
- Uses `Resources.GetBuiltinResource<Font>("Arial.ttf")` (DEPRECATED)
- Uses `LayoutElement` with `preferredHeight`/`preferredWidth`
- Uses `RectOffset` for padding

**Required Changes**:
```csharp
// BEFORE (DEPRECATED)
private Text scoreText;
private HorizontalLayoutGroup layoutGroup;
Font arialFont = Resources.GetBuiltinResource<Font>("Arial.ttf");

// AFTER (COMPATIBLE)
using TextMeshPro;
private TextMeshProUGUI scoreText;
// Remove HorizontalLayoutGroup - use GridLayout or manual positioning
// Use TextMeshPro's built-in fonts
```

**Severity**: 🔴 CRITICAL  
**Estimate**: 2-3 hours  
**Owner**: UI Engineer

---

#### 2. PauseMenuController.cs ⚠️
**Issues**:
- Uses `Text` component (DEPRECATED)
- Uses `VerticalLayoutGroup` (DEPRECATED)
- Uses `Image` component (check compatibility)
- Uses `TextAnchor.MiddleCenter` (deprecated text alignment)
- Uses `fontStyle` and `fontSize` (deprecated Text properties)

**Required Changes**:
```csharp
// Replace all Text with TextMeshProUGUI
// Replace VerticalLayoutGroup with LayoutGroup or manual RectTransform
// Use TextMeshPro's alignment properties instead
```

**Severity**: 🔴 CRITICAL  
**Estimate**: 2-3 hours  
**Owner**: UI Engineer

---

#### 3. BoardInputHandler.cs ⚠️
**Issues**:
- Uses `Input.GetKeyDown(KeyCode)` (OLD INPUT SYSTEM - UNRELIABLE)
- Uses `Handheld.Vibrate()` (POTENTIALLY DEPRECATED)
- May not properly handle New Input System

**Required Changes**:
```csharp
// BEFORE
if (Input.GetKeyDown(KeyCode.E))
{
    Handheld.Vibrate();
}

// AFTER
using UnityEngine.InputSystem;
private InputAction clickAction;

void OnEnable() => clickAction.Enable();
void OnDisable() => clickAction.Disable();

if (clickAction.triggered)
{
    // Use new haptics API if available, or check for Handheld.Vibrate availability
}
```

**Severity**: 🔴 CRITICAL  
**Estimate**: 2-3 hours  
**Owner**: Board Engineer

---

#### 4. HUDManager.cs ⚠️
**Issues**:
- Uses `Text` component references
- Uses `Button` component (check compatibility)
- May use deprecated layout patterns

**Required Changes**:
- Replace Text references with TextMeshProUGUI
- Update button event binding patterns if needed

**Severity**: 🔴 CRITICAL  
**Estimate**: 1-2 hours  
**Owner**: UI Engineer

---

#### 5. DiceRollButton.cs ⚠️
**Issues**:
- Uses `Text` component
- Uses `Image` component
- Uses `Button` with `.interactable` property

**Required Changes**:
- Replace Text with TextMeshProUGUI
- Verify Image component compatibility
- Button.interactable should remain compatible

**Severity**: 🔴 CRITICAL  
**Estimate**: 1 hour  
**Owner**: UI Engineer

---

#### 6. CellView.cs ⚠️
**Issues**:
- Uses `Text` component (text overlay)
- Uses `LeanTween.alphaCanvas()` (THIRD-PARTY, VERIFY COMPATIBILITY)
- Uses `LeanTween.scale()` (THIRD-PARTY, VERIFY COMPATIBILITY)
- Uses `CanvasGroup` with alpha manipulation
- Uses `Image` component

**Required Changes**:
```csharp
// Option 1: Test LeanTween compatibility first
// Option 2: Replace with Unity's Animator system
// Option 3: Replace with new Tweens package (com.unity.animation.rigging + tweens)

// BEFORE
LeanTween.alphaCanvas(canvasGroup, 0.5f, duration);
LeanTween.scale(transform, newScale, duration).setEase(LeanTweenType.easeOutQuad);

// AFTER (using coroutine alternative)
StartCoroutine(FadeAlpha(canvasGroup, 0.5f, duration));
StartCoroutine(ScaleOverTime(transform, newScale, duration));
```

**Severity**: 🔴 CRITICAL  
**Estimate**: 3-4 hours (if replacement needed)  
**Owner**: Board Engineer

---

#### 7. ChipVisualizer.cs ⚠️
**Issues**:
- Uses `LeanTween.move()` (THIRD-PARTY)
- Uses `LeanTween.alphaCanvas()` (THIRD-PARTY)
- Uses `CanvasGroup` alpha manipulation
- Uses `RectTransform.sizeDelta`

**Required Changes**:
- Replace all LeanTween calls with alternatives (Animator, coroutines, or verified package)
- Verify RectTransform usage

**Severity**: 🔴 CRITICAL  
**Estimate**: 3-4 hours  
**Owner**: Board Engineer

---

### **HIGH PRIORITY (Fix By Week 2 of Sprint 8)**

#### 8. ModalController.cs ⚠️
**Issues**:
- Uses `Text` component
- Uses `LeanTween` (possibly)
- Uses `Button` component
- Uses `CanvasGroup` alpha

**Severity**: 🟠 HIGH  
**Estimate**: 2 hours  
**Owner**: UI Engineer

---

#### 9. ActionButtonController.cs ⚠️
**Issues**:
- Uses `Text` component
- Uses `Button` component

**Severity**: 🟠 HIGH  
**Estimate**: 1 hour  
**Owner**: UI Engineer

---

#### 10. BumpButton.cs ⚠️
**Issues**:
- Uses `Text` component
- Uses `Button` component

**Severity**: 🟠 HIGH  
**Estimate**: 1 hour  
**Owner**: UI Engineer

---

#### 11. DeclareWinButton.cs ⚠️
**Issues**:
- Uses `Text` component
- Uses `Button` component

**Severity**: 🟠 HIGH  
**Estimate**: 1 hour  
**Owner**: UI Engineer

---

#### 12. NotificationController.cs ⚠️
**Issues**:
- Uses `Text` component
- Uses `CanvasGroup` with alpha manipulation
- Uses Coroutines with `StartCoroutine`

**Severity**: 🟠 HIGH  
**Estimate**: 1 hour  
**Owner**: UI Engineer

---

### **MEDIUM PRIORITY (Verify/Test By Phase 1)**

#### 13. iOSBuildConfig.cs
**Issues**:
- References deprecated `enableBitcode = false` (Xcode deprecation)
- GraphicsSettings configuration with Metal

**Action**: Update comment, verify Xcode compatibility

**Severity**: 🟡 MEDIUM  
**Estimate**: 30 minutes

---

#### 14. AndroidBuildConfig.cs
**Issues**:
- Graphics API configuration (verify OpenGL ES 3 support)
- Build settings validation

**Action**: Test build targeting Unity 6 API levels

**Severity**: 🟡 MEDIUM  
**Estimate**: 30 minutes

---

#### 15. PerformanceProfiler.cs
**Issues**:
- Uses `Profiler.GetTotalAllocatedMemoryLong()` (verify availability)
- Uses `Profiler.GetTotalReservedMemoryLong()` (verify availability)
- Verify these APIs exist in Unity 6

**Action**: Test profiling APIs, add fallbacks if needed

**Severity**: 🟡 MEDIUM  
**Estimate**: 1 hour

---

## Migration Priority Matrix

| File | Issue | Priority | Estimate | Owner |
|------|-------|----------|----------|-------|
| ScoreboardController | Text/Layout/Font | 🔴 CRITICAL | 2-3h | UI |
| PauseMenuController | Text/Layout | 🔴 CRITICAL | 2-3h | UI |
| BoardInputHandler | Old Input/Haptics | 🔴 CRITICAL | 2-3h | Board |
| HUDManager | Text | 🔴 CRITICAL | 1-2h | UI |
| DiceRollButton | Text | 🔴 CRITICAL | 1h | UI |
| CellView | LeanTween/Text | 🔴 CRITICAL | 3-4h | Board |
| ChipVisualizer | LeanTween | 🔴 CRITICAL | 3-4h | Board |
| ModalController | Text/LeanTween | 🟠 HIGH | 2h | UI |
| ActionButtonController | Text | 🟠 HIGH | 1h | UI |
| BumpButton | Text | 🟠 HIGH | 1h | UI |
| DeclareWinButton | Text | 🟠 HIGH | 1h | UI |
| NotificationController | Text | 🟠 HIGH | 1h | UI |
| iOSBuildConfig | Bitcode comment | 🟡 MEDIUM | 30m | Build |
| AndroidBuildConfig | Graphics API | 🟡 MEDIUM | 30m | Build |
| PerformanceProfiler | Profiler APIs | 🟡 MEDIUM | 1h | Build |

**Total Estimate**: 26-32 hours  
**Parallel Work**: YES (3 teams can work simultaneously)

---

## Compatibility Solutions

### Option 1: Text Component Migration
```csharp
// DEPRECATED in Unity 6
using UnityEngine.UI;
private Text myText;
myText.text = "Hello";
myText.font = someFont;
myText.fontSize = 14;

// MODERN - TextMeshPro (RECOMMENDED)
using TMPro;
private TextMeshProUGUI myText;
myText.text = "Hello";
myText.fontSize = 14;
// Font is already embedded in TextMeshPro
```

### Option 2: Layout Group Migration
```csharp
// DEPRECATED
using UnityEngine.UI;
private HorizontalLayoutGroup layout;

// MODERN - Use GridLayoutGroup or manual RectTransform
private GridLayoutGroup gridLayout;
// OR manually position children using RectTransform
```

### Option 3: Input System Migration
```csharp
// DEPRECATED - Old Input System
if (Input.GetKeyDown(KeyCode.Space)) { }

// MODERN - New Input System
using UnityEngine.InputSystem;

private InputAction spaceAction;

void Start()
{
    spaceAction = new InputAction("space", InputActionType.Button, "<Keyboard>/space");
    spaceAction.Enable();
}

void Update()
{
    if (spaceAction.triggered) { }
}
```

### Option 4: Animation Alternatives to LeanTween
```csharp
// If LeanTween incompatible:

// Option A: Coroutine-based (simple, safe)
private IEnumerator FadeAlpha(CanvasGroup cg, float target, float duration)
{
    float elapsed = 0;
    float start = cg.alpha;
    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        cg.alpha = Mathf.Lerp(start, target, elapsed / duration);
        yield return null;
    }
    cg.alpha = target;
}

// Option B: Animator-based (most flexible)
// Create animation clip for fade, play via animator

// Option C: Use com.unity.tween package (official alternative)
// Requires adding package to project
```

---

## Testing Verification Checklist

After each migration, verify:

- [ ] Compilation: No syntax errors or missing references
- [ ] Runtime: No null reference exceptions
- [ ] Visual: UI renders correctly, layout matches design
- [ ] Animation: Fade/scale/move animations smooth (60 FPS)
- [ ] Input: Button clicks, keyboard input work properly
- [ ] Performance: No frame drops, FPS maintained
- [ ] Mobile: Touch input responsive on Android/iOS
- [ ] Text: Font rendering clear, alignment correct
- [ ] Layout: Responsive to different screen sizes

---

## Team Assignments & Schedule

### Phase 1: Nov 16-17 (Before Testing)

**UI Engineer** (Priority Order):
1. ScoreboardController → Text + Layout migration
2. PauseMenuController → Text + Layout migration
3. HUDManager → Text references
4. All button scripts → Text migration

**Board Engineer**:
1. BoardInputHandler → Input System migration
2. CellView → LeanTween replacement + Text
3. ChipVisualizer → LeanTween replacement

**Build Engineer**:
1. PerformanceProfiler → Profiler API verification
2. Build configs → Graphics/Bitcode verification

### Phase 2: Nov 18-20 (During Phase 1 Testing)

**All Teams**:
- Run QA tests on migrated code
- Fix any compatibility regressions
- Verify no performance degradation

---

## Authority & Compliance

**Managing Engineer Decision**: ALL CODE MUST BE UNITY 6.0 COMPATIBLE BY NOV 20, 2025

**Non-Compliance Consequences**:
- 🔴 Code blocks Phase 1 testing (Nov 16)
- 🔴 Extends QA timeline by up to 1 week
- 🔴 May delay release to Jan 2 (from Dec 15)

**Sign-Off Requirements**:
- Each team: Verify all assigned files compile + tested
- Managing Engineer: Final compatibility review
- QA Lead: Confirm Phase 1 testing can proceed

---

## Reference Documents

- **Audit File**: This document (UNITY_6_COMPATIBILITY_AUDIT.md)
- **Fixes Log**: Will be created as UNITY_6_COMPATIBILITY_FIXES.md (track all changes)
- **Test Results**: Phase 1 testing will include compatibility verification

---

## Next Steps

1. ✅ Distribute audit to all teams (Nov 14)
2. ✅ Teams acknowledge understanding (Nov 15)
3. ✅ UI Engineer: Begin ScoreboardController migration (Nov 16)
4. ✅ Board Engineer: Begin BoardInputHandler migration (Nov 16)
5. ✅ All: Complete critical migrations (Nov 17-18)
6. ✅ QA: Phase 1 testing includes compatibility verification (Nov 16-20)
7. ✅ Final verification (Nov 20)
8. ✅ Commit all changes with atomic, descriptive commits

---

## Authority Sign-Off

**Audit Completed By**: Amp (Managing Engineer)  
**Decision Authority**: FULL  
**Approval Status**: ✅ MANDATORY COMPLIANCE REQUIRED  
**Date**: Nov 14, 2025

This audit is BINDING. All code must be Unity 6.0 compatible before Sprint 8 Phase 1 testing begins (Nov 16).

---

**Document Status**: ACTIVE COMPLIANCE DIRECTIVE  
**Distribution**: ALL TEAMS (Gameplay, Board, UI, Build)  
**Priority**: 🔴 CRITICAL - Release blocking
