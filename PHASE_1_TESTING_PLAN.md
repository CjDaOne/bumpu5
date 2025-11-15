# Phase 1 Testing Plan - Unity 6.0 Compatibility & Integration

**Date**: Nov 16, 2025  
**Status**: ACTIVE EXECUTION  
**Target Completion**: Nov 20, 2025  
**Authority**: QA Lead (Amp)

---

## Overview

Phase 1 Testing validates the Unity 6.0 migration completed in Sprint 8. All 15 files were migrated from deprecated APIs to Unity 6.0-compatible equivalents. Phase 1 ensures:

1. **Compilation**: All code compiles without errors or warnings
2. **Runtime**: All migrated components initialize and function correctly
3. **Integration**: Migrated components work together seamlessly
4. **Regression**: No gameplay mechanics altered by the migration
5. **Performance**: Code maintains or improves performance metrics

---

## Test Scope

### Files Under Test (15 total)

**UI Controllers (5)**
- ScoreboardController.cs
- PauseMenuController.cs
- HUDManager.cs
- DiceRollButton.cs
- ActionButtonController.cs

**Specialized Button Controllers (2)**
- BumpButton.cs
- DeclareWinButton.cs

**Modal & Notification (2)**
- ModalController.cs
- NotificationController.cs

**Input System (1)**
- BoardInputHandler.cs

**Board UI (2)**
- CellView.cs
- ChipVisualizer.cs

**Performance & Build (3)**
- PerformanceProfiler.cs
- iOSBuildConfig.cs
- AndroidBuildConfig.cs

---

## Test Categories

### 1. Compilation Tests (5 tests)
Verify each file compiles without errors or warnings.

| Test ID | File | Expected Result | Status |
|---------|------|-----------------|--------|
| COMP-001 | ScoreboardController.cs | No errors, no warnings | ⏳ |
| COMP-002 | PauseMenuController.cs | No errors, no warnings | ⏳ |
| COMP-003 | HUDManager.cs | No errors, no warnings | ⏳ |
| COMP-004 | DiceRollButton.cs | No errors, no warnings | ⏳ |
| COMP-005 | ActionButtonController.cs | No errors, no warnings | ⏳ |
| COMP-006 | BumpButton.cs | No errors, no warnings | ⏳ |
| COMP-007 | DeclareWinButton.cs | No errors, no warnings | ⏳ |
| COMP-008 | ModalController.cs | No errors, no warnings | ⏳ |
| COMP-009 | NotificationController.cs | No errors, no warnings | ⏳ |
| COMP-010 | BoardInputHandler.cs | No errors, no warnings | ⏳ |
| COMP-011 | CellView.cs | No errors, no warnings | ⏳ |
| COMP-012 | ChipVisualizer.cs | No errors, no warnings | ⏳ |
| COMP-013 | PerformanceProfiler.cs | No errors, no warnings | ⏳ |
| COMP-014 | iOSBuildConfig.cs | No errors, no warnings | ⏳ |
| COMP-015 | AndroidBuildConfig.cs | No errors, no warnings | ⏳ |

### 2. TextMeshProUGUI Migration Tests (8 tests)
Verify Text → TextMeshProUGUI migrations work correctly.

| Test ID | Component | Aspect | Expected Result | Status |
|---------|-----------|--------|-----------------|--------|
| TMP-001 | ScoreboardController | playerNameText display | "Player 1", "Player 2" render | ⏳ |
| TMP-002 | ScoreboardController | scoreText display | "Score: 0" renders | ⏳ |
| TMP-003 | PauseMenuController | Title text | "Game Paused" renders | ⏳ |
| TMP-004 | PauseMenuController | Button text | Resume/Settings/Rules/Quit render | ⏳ |
| TMP-005 | DiceRollButton | Button text | "Roll Dice" / "Rolled: X+Y" displays | ⏳ |
| TMP-006 | ModalController | Modal text | Title and message render correctly | ⏳ |
| TMP-007 | NotificationController | Notification message | Messages display with proper color | ⏳ |
| TMP-008 | CellView | Chip count | "1" renders in cell when occupied | ⏳ |

### 3. Layout Migration Tests (4 tests)
Verify HorizontalLayoutGroup/VerticalLayoutGroup → GridLayoutGroup migrations.

| Test ID | Component | Change | Expected Result | Status |
|---------|-----------|--------|-----------------|--------|
| LAY-001 | ScoreboardController | GridLayoutGroup (4 col) | Cells layout horizontally | ⏳ |
| LAY-002 | PauseMenuController | GridLayoutGroup (1 col) | Buttons stack vertically | ⏳ |
| LAY-003 | ScoreboardController | Spacing 8px | 8px gap between cells | ⏳ |
| LAY-004 | PauseMenuController | Spacing 12px | 12px gap between buttons | ⏳ |

### 4. Input System Tests (3 tests)
Verify Input.GetKeyDown() → InputAction.triggered migration.

| Test ID | Action | Trigger | Expected Result | Status |
|---------|--------|---------|-----------------|--------|
| INP-001 | RollDice (R key) | Press R during RollingDice | Dice rolls | ⏳ |
| INP-002 | Bump (B key) | Press B during Bumping | Bump action triggers | ⏳ |
| INP-003 | DeclareWin (W key) | Press W with win condition met | Game ends | ⏳ |

### 5. Animation System Tests (4 tests)
Verify LeanTween compatibility with Unity 6.0.

| Test ID | Component | Animation | Expected Result | Status |
|---------|-----------|-----------|-----------------|--------|
| ANI-001 | ModalController | alphaCanvas fade-in | Modal fades in 0.3s | ⏳ |
| ANI-002 | NotificationController | Fade in/out | Notification appears/disappears | ⏳ |
| ANI-003 | CellView | Placement animation | Cell fades in 0.3s | ⏳ |
| ANI-004 | ChipVisualizer | Bump animation | Chip bounces and fades | ⏳ |

### 6. Performance Profiler Tests (2 tests)
Verify Profiler API compatibility.

| Test ID | API | Expected Result | Status |
|---------|-----|-----------------|--------|
| PERF-001 | Profiler.GetTotalAllocatedMemoryLong() | Returns memory in bytes | ⏳ |
| PERF-002 | Profiler.GetTotalReservedMemoryLong() | Returns memory in bytes | ⏳ |

### 7. Build Config Tests (2 tests)
Verify platform-specific build configurations.

| Test ID | Config | Validation | Expected Result | Status |
|---------|--------|-----------|-----------------|--------|
| BUILD-001 | iOSBuildConfig | enableBitcode removed | No compilation error | ⏳ |
| BUILD-002 | AndroidBuildConfig | Vulkan graphics API | Metal and Vulkan both valid | ⏳ |

### 8. Initialization Tests (6 tests)
Verify each component initializes without errors.

| Test ID | Component | Init Method | Expected Result | Status |
|---------|-----------|------------|-----------------|--------|
| INIT-001 | ScoreboardController | Initialize(gsm, parent) | isInitialized = true | ⏳ |
| INIT-002 | PauseMenuController | Initialize(gsm, btn) | isInitialized = true | ⏳ |
| INIT-003 | HUDManager | Initialize(gsm) | isInitialized = true | ⏳ |
| INIT-004 | BoardInputHandler | Initialize(bgm, gsm) | isInitialized = true | ⏳ |
| INIT-005 | PerformanceProfiler | Initialize() | isInitialized = true | ⏳ |
| INIT-006 | ModalController | Initialize(prefab) | isInitialized = true | ⏳ |

### 9. Regression Tests (8 tests)
Verify gameplay mechanics unchanged.

| Test ID | Mechanic | Expected Behavior | Status |
|---------|----------|-------------------|--------|
| REG-001 | Player highlighting | Current player row highlights | ⏳ |
| REG-002 | Pause/Resume | Game pauses when menu shows | ⏳ |
| REG-003 | Win detection | Win condition still works | ⏳ |
| REG-004 | Bump logic | Bump rules unchanged | ⏳ |
| REG-005 | Cell selection | Valid cells still selectable | ⏳ |
| REG-006 | Chip placement | Chips appear in correct position | ⏳ |
| REG-007 | Dice roll | Dice roll displays correctly | ⏳ |
| REG-008 | Notifications | Game messages display | ⏳ |

---

## Test Execution Protocol

### Pre-Test Setup
1. Open Unity 6000.2.12f1
2. Load project and wait for full compile
3. Open Test Framework (Window → Testing → Test Runner)
4. Verify IDE installed (optional, for unit test execution)

### Test Execution Order
1. Compilation tests (verify no errors)
2. Initialization tests (verify components start)
3. TextMeshProUGUI tests (verify UI rendering)
4. Layout tests (verify positioning)
5. Animation tests (verify LeanTween)
6. Input tests (verify keyboard shortcuts)
7. Performance tests (verify Profiler APIs)
8. Build config tests (verify platform configs)
9. Regression tests (verify gameplay unchanged)

### Pass/Fail Criteria
- **PASS**: Feature works as expected, no errors in console
- **FAIL**: Feature broken, compilation error, or runtime exception
- **WARN**: Feature works but with deprecation warning (investigate)
- **SKIP**: Test inapplicable to current environment

### Required Resources
- Unity 6000.2.12f1
- Visual Studio 2022 or equivalent
- Test devices: PC/Mac, Android (if available), iOS (if available)
- 2+ hours for complete test cycle

---

## Test Results Template

```
Test ID: [ID]
Component: [Component Name]
Test: [Description]
Status: [PASS / FAIL / WARN / SKIP]
Notes: [Any observations]
Executed: [Timestamp]
Evidence: [Screenshot or log reference]
```

---

## Blocker Classification

| Severity | Definition | Example | Action |
|----------|-----------|---------|--------|
| CRITICAL | Game unplayable | Compilation error, crash on init | BLOCK RELEASE |
| HIGH | Major feature broken | HUD doesn't display, input doesn't work | Fix < 24h |
| MEDIUM | Partial feature broken | One button doesn't highlight | Fix < 48h |
| LOW | Minor/cosmetic | Text color slightly off | Fix on schedule |

---

## Success Criteria

Phase 1 is COMPLETE when:
- ✅ All 15 files compile without errors
- ✅ Zero deprecation warnings
- ✅ All 45+ tests pass
- ✅ No regressions from Sprints 1-7
- ✅ Performance metrics stable
- ✅ All critical and high severity issues resolved

**Phase 1 Gate**: Before proceeding to Phase 2 (Nov 30), all tests must PASS.

---

## Timeline

| Date | Activity | Owner | Status |
|------|----------|-------|--------|
| Nov 16 (today) | Phase 1 test execution | QA Lead | 🔄 STARTING |
| Nov 17 | Test execution continues | QA Lead | ⏳ |
| Nov 18 | Test execution continues | QA Lead | ⏳ |
| Nov 19 | Results compilation & analysis | QA Lead | ⏳ |
| Nov 20 | Phase 1 sign-off | QA Lead | ⏳ |
| Nov 21+ | Phase 2 testing (full playtest) | QA Team | ⏳ |

---

## Test Execution Log

### Test Session 1 - [Date]

[Test results will be recorded here as testing progresses]

---

## Sign-Off

**Phase 1 QA Lead**: [Name]  
**Date**: [Date]  
**Result**: [APPROVED / PENDING / REJECTED]  
**Notes**: [Any final observations]

---

**Document Version**: 1.0  
**Created**: Nov 16, 2025  
**Last Updated**: Nov 16, 2025
