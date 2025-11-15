# Phase 2 Testing Briefing - Full Game Playtest

**Date**: Nov 21, 2025  
**Target Completion**: Nov 29, 2025  
**Authority**: QA Lead (Amp)  
**Status**: READY TO EXECUTE

---

## Overview

Phase 2 is a comprehensive full-game playtest. Phase 1 validated code compatibility; Phase 2 validates gameplay across all 5 game modes, all platforms, and all major features. This is the first end-to-end validation since Unity 6.0 migration.

### Phase 1 → Phase 2 Transition

**Phase 1 Results**: ✅ 100% PASS (78/78 tests)
- All 15 migrated files compile cleanly
- All APIs updated correctly
- All components initialize properly
- No regressions detected

**Phase 2 Scope**: Full game validation
- Play all 5 game modes start-to-finish
- Test all UI screens and transitions
- Verify all input methods (keyboard, mouse, touch)
- Test pause/resume functionality
- Validate win/loss detection
- Check error handling edge cases
- Verify notifications and modals display correctly
- Performance profiling on actual hardware

---

## Test Scenarios

### Game Mode Testing (5 modes × 3 scenarios = 15 tests)

#### Game 1: Bump5 (Classic)
- **Test 2-1**: 2-player game to completion
- **Test 2-2**: 4-player game with bumping
- **Test 2-3**: Edge case - last chip on board wins

#### Game 2: Krazy6
- **Test 2-4**: 2-player game with 6-roll rule
- **Test 2-5**: 4-player game with chip stealing
- **Test 2-6**: Edge case - simultaneous 6 rolls

#### Game 3: PassTheChip
- **Test 2-7**: Chip passing mechanic works
- **Test 2-8**: 4-player pass and bump interactions
- **Test 2-9**: Edge case - chip passes correctly on rolls

#### Game 4: BumpUAnd5
- **Test 2-10**: Alternative bumping rules enforced
- **Test 2-11**: 4-player with unique bump mechanics
- **Test 2-12**: Edge case - bump validation correct

#### Game 5: Solitary
- **Test 2-13**: Single player to win condition
- **Test 2-14**: No opponent interaction
- **Test 2-15**: Solo win detection works

### UI & Screen Transitions (8 tests)

| Test ID | Screen | Scenario | Expected Result |
|---------|--------|----------|-----------------|
| 2-16 | Main Menu | Load game | Menu displays, selectable |
| 2-17 | Game Mode Select | Choose mode | Mode selected, game starts |
| 2-18 | Game Board | During play | HUD displays correctly, buttons functional |
| 2-19 | Scoreboard | Update scores | Scores reflect dice rolls and placements |
| 2-20 | Phase Indicator | Phase changes | Indicator updates (Rolling, Placing, Bumping, etc.) |
| 2-21 | Pause Menu | Pause game | Menu appears, game paused, resume works |
| 2-22 | Win Modal | Game won | Modal shows winner, options to play again |
| 2-23 | Notification | Game events | Messages appear and dismiss automatically |

### Input Method Testing (6 tests)

| Test ID | Input Method | Test | Expected |
|---------|--------------|------|----------|
| 2-24 | Keyboard (R) | Roll dice during RollingDice phase | Dice rolls |
| 2-25 | Keyboard (B) | Bump during Bumping phase | Bump executes |
| 2-26 | Keyboard (W) | Declare win with condition met | Game ends |
| 2-27 | Mouse Click | Click cell during Placing phase | Chip places |
| 2-28 | Mouse Click | Click pause button | Game pauses |
| 2-29 | Keyboard (ESC) | Press ESC during game | Pause menu appears |

### Edge Cases & Error Handling (10 tests)

| Test ID | Scenario | Expected Behavior |
|---------|----------|-------------------|
| 2-30 | Click invalid cell | No chip placed, notification "Invalid move" |
| 2-31 | Try to bump own chip | Validation prevents bump, message shown |
| 2-32 | Double-tap same cell | Gesture detected (no unintended action) |
| 2-33 | Pause then resume quickly | Game state preserved |
| 2-34 | Null GameStateManager | Initialize gracefully without crash |
| 2-35 | Missing TextMeshPro font | Fallback to default, no crash |
| 2-36 | Roll dice in invalid phase | Button disabled, no action |
| 2-37 | Declare win without condition | Button disabled or error message |
| 2-38 | Rapid button clicks | Queue actions or ignore duplicates (no crash) |
| 2-39 | Very long game (100 turns) | No memory leak, stable performance |

### Platform-Specific Testing (6 tests)

| Test ID | Platform | Test | Expected |
|---------|----------|------|----------|
| 2-40 | WebGL | Full game mode | Runs smoothly, no compilation errors |
| 2-41 | Android | Touch input on cell | Chip places correctly |
| 2-42 | Android | Haptic feedback | Vibration on valid/invalid moves |
| 2-43 | iOS | Touch input on cell | Chip places correctly |
| 2-44 | iOS | Frame rate capped | Battery-optimized 30 FPS |
| 2-45 | Desktop | Keyboard shortcuts | All shortcuts work |

### Performance Testing (5 tests)

| Test ID | Metric | Test | Target | Expected |
|---------|--------|------|--------|----------|
| 2-46 | Frame Rate | Play 20-turn game | 60 FPS | 55-62 FPS (Editor) |
| 2-47 | Memory | Monitor during game | < 512MB | ~50-100MB peak |
| 2-48 | Load Time | Start game to playable | < 5 seconds | ~2-3 seconds |
| 2-49 | Stutter | Long play session (30 min) | Zero stutters | Smooth gameplay |
| 2-50 | Thermal | Run 1 hour continuously | No throttling | Stable FPS throughout |

### Regression Testing (8 tests)

| Test ID | Feature | Verification |
|---------|---------|--------------|
| 2-51 | Dice rolling | 2d6 range (2-12), distribution looks right |
| 2-52 | Chip movement | Chips move correct number of cells |
| 2-53 | Bumping | Opponent chips removed from board correctly |
| 2-54 | Win condition | Correct player declared winner |
| 2-55 | Score display | Scores update after each move |
| 2-56 | Player rotation | Turns rotate in correct order |
| 2-57 | Highlights | Valid moves highlighted correctly |
| 2-58 | Current player | Current player indicator updates |

---

## Test Execution Environment

### Hardware Requirements

**Recommended for Testing**:
- Desktop PC or Mac (Unity Editor)
- Android device (Pixel 6 or equivalent)
- iOS device (iPhone 12 or equivalent)
- Fallback: WebGL in Chrome/Firefox

### Software Requirements

- Unity 6000.2.12f1 installed
- All migrated files compiled (Phase 1 PASS)
- Test devices with latest OS updates
- Android 12+ for Android device
- iOS 14+ for iOS device

### Test Tools

- Unity Profiler (memory, frame rate)
- Device Performance Monitor
- Test Case Management (spreadsheet or tool)
- Screenshot/video recording for evidence
- Bug tracking system for issues found

---

## Execution Timeline

### Week 1: Nov 21-22 (Game Mode Testing)
- Mon Nov 21: Bump5 testing (Tests 2-1, 2-2, 2-3)
- Tue Nov 22: Krazy6 & PassTheChip (Tests 2-4 through 2-9)

### Week 2: Nov 23-24 (Game Modes & UI)
- Wed Nov 23: BumpUAnd5 & Solitary (Tests 2-10 through 2-15)
- Thu Nov 24: UI & Screen Transitions (Tests 2-16 through 2-23)

### Week 3: Nov 25-26 (Input & Edge Cases)
- Fri Nov 25: Input methods & Edge cases (Tests 2-24 through 2-39)
- Sat Nov 26: Platform testing (Tests 2-40 through 2-45)

### Week 4: Nov 27-29 (Performance & Final)
- Sun Nov 27: Performance testing (Tests 2-46 through 2-50)
- Mon Nov 28: Regression testing (Tests 2-51 through 2-58)
- Tue Nov 29: Results compilation & sign-off

**Total Test Cases**: 58  
**Estimated Execution Time**: 40-50 hours  
**Testers Required**: 1-2 QA engineers

---

## Success Criteria for Phase 2

Phase 2 PASS requires:
- ✅ All 58 tests executed
- ✅ At least 50 tests passing (86% pass rate)
- ✅ Zero CRITICAL issues blocking release
- ✅ All HIGH issues resolved or scheduled for Phase 3
- ✅ Performance metrics meet targets on at least 2 platforms
- ✅ No new regressions from Phase 1

**Blocker Criteria** (would prevent Phase 3):
- ❌ Game crash on startup or during play
- ❌ Core game mechanic broken (dice, bumping, win condition)
- ❌ UI completely unresponsive
- ❌ Save/load completely broken (if implemented)

---

## Issue Severity Matrix

| Severity | Definition | Resolution Timeline |
|----------|-----------|---------------------|
| CRITICAL | Game unplayable | Fix before Phase 2 sign-off |
| HIGH | Major feature broken | Fix before Phase 3 |
| MEDIUM | Partial feature broken | Fix by Phase 3 end |
| LOW | Minor/cosmetic | Fix by release (Phase 4) |

---

## Test Documentation

### For Each Test Case, Record:
1. **Test ID** (e.g., 2-1)
2. **Description** (what you're testing)
3. **Steps** (how to reproduce)
4. **Expected Result** (what should happen)
5. **Actual Result** (what actually happened)
6. **Status** (PASS/FAIL/SKIP)
7. **Evidence** (screenshot, video, log)
8. **Issues Found** (if any)
9. **Severity** (CRITICAL/HIGH/MEDIUM/LOW)
10. **Assigned To** (for fixes)

### Example Log Entry:
```
Test ID: 2-1
Description: Play Bump5 mode with 2 players to completion
Steps:
  1. Start game, select Bump5 mode
  2. Select 2 players
  3. Play until one player wins
  4. Verify win modal displays
Expected: Game completes successfully, winner announced
Actual: ✅ Completed as expected, winner modal displayed
Status: PASS
Evidence: Screenshot of win modal
Issues: None
```

---

## Integration with Dev Teams

### Bug Reporting Process

When a test FAILS:
1. Record test case and actual behavior
2. Take screenshot/video
3. Check editor console for errors
4. Report to appropriate team:
   - Game logic bugs → Gameplay Team
   - UI bugs → UI Team
   - Input bugs → Input System Team
   - Performance bugs → Build Team
   - Board bugs → Board Team

### Expected Response Times

- **CRITICAL**: Dev response < 4 hours
- **HIGH**: Dev response < 24 hours
- **MEDIUM**: Dev response < 48 hours
- **LOW**: Dev response < 1 week

---

## Phase 2 Sign-Off Criteria

**Phase 2 Lead**: QA Lead (Amp)

### Before Signing Off:
- [ ] All 58 tests documented with results
- [ ] All CRITICAL issues resolved
- [ ] All HIGH issues assigned to team with ETA
- [ ] Performance baseline established on 2+ platforms
- [ ] No new regressions from Phase 1
- [ ] Executable build passes all Phase 2 tests
- [ ] Test evidence (screenshots/videos) archived
- [ ] Bug report compiled and prioritized

### Sign-Off Template:
```
Phase 2 Testing Complete
Date: Nov 29, 2025
Tests Passed: X/58
Pass Rate: X%
CRITICAL Issues: X
HIGH Issues: X
Status: [APPROVED / PENDING / FAILED]
Next Phase: Phase 3 (Bug Fixes & Optimization)
```

---

## Transition to Phase 3

**Phase 3 Start**: Dec 1, 2025  
**Phase 3 Duration**: 2 weeks (Dec 1-14)  
**Phase 3 Focus**: Bug fixes, optimization, final testing

Phase 3 will address all issues identified in Phase 2 and conduct final validation before Phase 4 (Release Preparation).

---

**Document Version**: 1.0  
**Created**: Nov 16, 2025  
**Authority**: QA Lead (Amp)  
**Status**: READY FOR PHASE 2 EXECUTION
