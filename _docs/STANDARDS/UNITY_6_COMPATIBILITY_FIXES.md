# Unity 6.0 Compatibility Fixes - Tracking Log
## Progress Log for Migration Work

**Directive Issued**: Nov 14, 2025  
**Deadline**: Nov 17-18, 2025 (by team)  
**Tracking**: Real-time progress updates  
**Authority**: Managing Engineer (Amp)

---

## Team Status Overview

| Team | Files | Status | Lead | ETA |
|------|-------|--------|------|-----|
| **UI Engineer** | 9 files | 🟡 PENDING | [Name] | Nov 17 EOD |
| **Board Engineer** | 3 files | 🟡 PENDING | [Name] | Nov 17 EOD |
| **Build Engineer** | 3 files | 🟡 PENDING | [Name] | Nov 18 EOD |
| **Total** | **15 files** | **🟡 PENDING** | **Teams** | **Nov 18 EOD** |

---

## UI ENGINEER ASSIGNMENTS (9 files)

### 1. ScoreboardController.cs
**Status**: 🟡 NOT STARTED  
**Assigned**: [Date]  
**Started**: [Date]  
**Completed**: [Date]  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Line X: Migrate `Text` → `TextMeshProUGUI`
- [ ] Line Y: Remove `HorizontalLayoutGroup`, replace with...
- [ ] Line Z: Replace `Resources.GetBuiltinResource<Font>`

**Code Snippets** (before/after):
```csharp
// BEFORE
private Text scoreText;
private HorizontalLayoutGroup layoutGroup;
Font arialFont = Resources.GetBuiltinResource<Font>("Arial.ttf");

// AFTER
private TextMeshProUGUI scoreText;
// Layout group removed - using [describe alternative]
// TextMeshPro font - [describe how]
```

**Tested**:
- [ ] Compiles without errors
- [ ] Existing unit tests pass
- [ ] Visual: Scoreboard displays correctly
- [ ] Layout: Elements aligned properly

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: ScoreboardController - Text + Layout migration`

**Notes**: [Any issues encountered, solutions, or additional context]

---

### 2. PauseMenuController.cs
**Status**: 🟡 NOT STARTED  
**Assigned**: [Date]  
**Started**: [Date]  
**Completed**: [Date]  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate all `Text` → `TextMeshProUGUI`
- [ ] Replace `VerticalLayoutGroup`
- [ ] Update text alignment (`TextAnchor` → TextMeshPro alignment)

**Code Snippets**:
```csharp
// BEFORE
using UnityEngine.UI;
private Text titleText;
private VerticalLayoutGroup layoutGroup;
titleText.alignment = TextAnchor.MiddleCenter;
titleText.fontStyle = FontStyle.Bold;
titleText.fontSize = 24;

// AFTER
using TMPro;
private TextMeshProUGUI titleText;
// Layout group removed - [describe how]
titleText.alignment = TextAlignmentOptions.Center;
titleText.fontSize = 24;
// fontStyle not available - use TextMeshPro style tags or separate font asset
```

**Tested**:
- [ ] Compiles without errors
- [ ] Existing unit tests pass
- [ ] Visual: Menu displays correctly
- [ ] Layout: Elements positioned properly

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: PauseMenuController - Text + Layout migration`

**Notes**: [Any issues encountered]

---

### 3. HUDManager.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate `Text` references → `TextMeshProUGUI`

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: HUDManager - Text migration`

---

### 4. DiceRollButton.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate `Text` → `TextMeshProUGUI`

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: DiceRollButton - Text migration`

---

### 5. BumpButton.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate `Text` → `TextMeshProUGUI`

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: BumpButton - Text migration`

---

### 6. DeclareWinButton.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate `Text` → `TextMeshProUGUI`

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: DeclareWinButton - Text migration`

---

### 7. ActionButtonController.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate `Text` → `TextMeshProUGUI`

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: ActionButtonController - Text migration`

---

### 8. ModalController.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate `Text` → `TextMeshProUGUI`
- [ ] Verify/fix LeanTween compatibility

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: ModalController - Text migration + LeanTween verification`

---

### 9. NotificationController.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate `Text` → `TextMeshProUGUI`

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: NotificationController - Text migration`

---

## BOARD ENGINEER ASSIGNMENTS (3 files)

### 1. BoardInputHandler.cs
**Status**: 🟡 NOT STARTED  
**Assigned**: [Date]  
**Started**: [Date]  
**Completed**: [Date]  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate `Input.GetKeyDown()` → New `InputSystem`
- [ ] Update/verify `Handheld.Vibrate()` (potentially deprecated)

**Code Snippets**:
```csharp
// BEFORE
if (Input.GetKeyDown(KeyCode.E))
{
    Handheld.Vibrate();
}

// AFTER
using UnityEngine.InputSystem;
private InputAction clickAction;

void OnEnable()
{
    clickAction = new InputAction("click", InputActionType.Button, "<Keyboard>/e");
    clickAction.Enable();
}

void OnDisable()
{
    clickAction?.Disable();
}

void Update()
{
    if (clickAction.triggered)
    {
        // Haptics - verify Handheld.Vibrate still works or migrate to new API
        Handheld.Vibrate();
    }
}
```

**Tested**:
- [ ] Compiles without errors
- [ ] Keyboard input responsive
- [ ] Mobile touch input responsive
- [ ] Haptics functional (or gracefully disabled)

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: BoardInputHandler - InputSystem migration + haptics verification`

**Notes**: [Research on Handheld.Vibrate deprecation status]

---

### 2. CellView.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Migrate `Text` → `TextMeshProUGUI`
- [ ] Replace/verify LeanTween animations
  - [ ] `LeanTween.alphaCanvas()`
  - [ ] `LeanTween.scale()`

**LeanTween Resolution**:
- [ ] Decision: Test LeanTween compatibility / Replace with coroutine / Use alternative package
- [ ] If replacing: Implement coroutine-based animation
- [ ] If verifying: Document compatibility version

**Code Snippet** (if replacing LeanTween):
```csharp
// BEFORE
LeanTween.alphaCanvas(canvasGroup, 0.5f, 0.3f);
LeanTween.scale(transform, newScale, 0.5f).setEase(LeanTweenType.easeOutQuad);

// AFTER
StartCoroutine(FadeAlpha(canvasGroup, 0.5f, 0.3f));
StartCoroutine(ScaleOverTime(transform, newScale, 0.5f));

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

private IEnumerator ScaleOverTime(Transform trans, Vector3 target, float duration)
{
    float elapsed = 0;
    Vector3 start = trans.localScale;
    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float t = elapsed / duration;
        // easeOutQuad: 1 - (1-t)^2
        float eased = 1 - Mathf.Pow(1 - t, 2);
        trans.localScale = Vector3.Lerp(start, target, eased);
        yield return null;
    }
    trans.localScale = target;
}
```

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: CellView - Text migration + animation replacement`

---

### 3. ChipVisualizer.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Replace/verify LeanTween animations
  - [ ] `LeanTween.move()`
  - [ ] `LeanTween.alphaCanvas()`

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: ChipVisualizer - animation replacement`

---

## BUILD ENGINEER ASSIGNMENTS (3 files)

### 1. PerformanceProfiler.cs
**Status**: 🟡 NOT STARTED  
**Assigned**: [Date]  
**Started**: [Date]  
**Completed**: [Date]  
**Owner**: [Engineer Name]

**Verification Required**:
- [ ] `Profiler.GetTotalAllocatedMemoryLong()` available in Unity 6
- [ ] `Profiler.GetTotalReservedMemoryLong()` available in Unity 6
- [ ] Add fallback/error handling if deprecated

**Code Snippet** (if fallback needed):
```csharp
// Add safe accessor for deprecated Profiler APIs
private long GetTotalAllocatedMemory()
{
    try
    {
        return Profiler.GetTotalAllocatedMemoryLong();
    }
    catch (System.NotSupportedException)
    {
        // Fallback for Unity 6 if API removed
        return 0; // Or implement alternative measurement
    }
}
```

**Tested**:
- [ ] Compiles without errors
- [ ] Profiler reports generated correctly
- [ ] No exceptions at runtime

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: PerformanceProfiler - Profiler API verification`

---

### 2. iOSBuildConfig.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Update bitcode deprecation comment
- [ ] Verify Metal API configuration

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: iOSBuildConfig - Bitcode deprecation update`

---

### 3. AndroidBuildConfig.cs
**Status**: 🟡 NOT STARTED  
**Owner**: [Engineer Name]

**Changes Made**:
- [ ] Verify OpenGL ES 3 configuration still valid in Unity 6
- [ ] Test build validation

**Git Commit**: `[Sprint 8] Unity 6.0 compatibility: AndroidBuildConfig - Graphics API verification`

---

## SUMMARY

### Overall Progress
- **Total Files**: 15
- **Completed**: 0
- **In Progress**: 0
- **Not Started**: 15
- **Blocked**: 0

### Timeline
- **Started**: Nov 16, 2025
- **Target**: Nov 17-18 EOD
- **Actual**: [Will update]

### Key Blockers
- None yet (tracking)

### Issues Encountered
- [Will update as teams work]

### Lessons Learned
- [Will update post-completion]

---

## DAILY UPDATES

**Nov 16 Morning Standup**:
- Teams acknowledge receipt
- Begin work on assigned files
- Report any blockers immediately

**Nov 16 Afternoon Update**:
- [To be filled by teams]

**Nov 17 Morning Update**:
- [To be filled by teams]

**Nov 17 EOD Update**:
- UI and Board engineers: All files must be complete
- Build engineer: Complete by Nov 18 EOD

---

## SIGN-OFF

**When all 15 files completed**:

[ ] UI Engineer: All 9 files completed, tested, committed  
[ ] Board Engineer: All 3 files completed, tested, committed  
[ ] Build Engineer: All 3 files completed, tested, committed  
[ ] Managing Engineer: Final compatibility review passed  
[ ] **GATE RELEASED**: Phase 1 testing proceeds Nov 16+

---

**Tracking Document Status**: ACTIVE  
**Last Updated**: Nov 14, 2025  
**Authority**: Managing Engineer (Amp)
