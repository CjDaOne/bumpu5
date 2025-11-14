# Regression Testing

**Created**: Nov 14, 2025  
**Owner**: QA Lead  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Definition

**Regression Testing**: Verification that previously working features continue to work correctly after code changes, bug fixes, or new features are added.

**Purpose**: Prevent "regression" where a fix in one area breaks something in another area.

**When**: After every bug fix, before every release.

---

## Critical Path (Must Pass Every Time)

The Critical Path represents the most essential user journey. If any of these fail, the release is blocked.

```
CRITICAL PATH CHECKLIST

☐ App Launches
  └─ Game starts without crash
  └─ Main menu visible
  └─ No error messages

☐ Game Starts
  └─ Can select Game 1 from menu
  └─ Game scene loads (< 3 seconds)
  └─ Board displays with 12 cells
  └─ All 4 players visible with chips

☐ Dice Roll
  └─ Dice button is visible and clickable
  └─ Clicking Dice button rolls dice
  └─ Dice result shown (1-6)
  └─ Result completes in < 1 second

☐ Valid Move Highlighting
  └─ Valid destination cells highlight in blue
  └─ Cells pulse with animation
  └─ Only valid cells are highlighted

☐ Move Execution
  └─ Can tap a highlighted cell
  └─ Chip animates from home to selected cell
  └─ Animation smooth (300ms)
  └─ Chip lands on correct cell

☐ Turn Progression
  └─ After move, next player's turn starts
  └─ Phase indicator updates correctly
  └─ Scoreboard updates

☐ Win Condition
  └─ When conditions met, win button enabled
  └─ Can click "Declare Win"
  └─ Win screen appears with message
  └─ Can select "Play Again" or "Return to Menu"

☐ Menu Navigation
  └─ Main Menu → Game 1 → Play → Quit works
  └─ Return to Menu button works
  └─ Back/Close buttons functional
  └─ Settings menu accessible

☐ App Closes
  └─ Can quit gracefully
  └─ No crashes on exit
```

---

## Game Logic Regression Tests

### Test Suite 1: Dice Rolling

```
TEST: Dice Range
  Step 1: Roll dice 20 times
  Step 2: Record all results
  Result: All results between 1-6? ✅ PASS / ❌ FAIL

TEST: Valid Moves Calculation
  Step 1: Roll a 5
  Step 2: Verify valid moves are 5 cells away (or correct per game mode)
  Result: Correct cells highlighted? ✅ PASS / ❌ FAIL

TEST: No Duplicate Moves
  Step 1: Roll dice
  Step 2: Note valid moves
  Result: No duplicate cells in valid list? ✅ PASS / ❌ FAIL
```

### Test Suite 2: Bump Logic

```
TEST: Bump Detection
  Step 1: Place Player 1 chip on cell 5
  Step 2: Place Player 2 chip on cell 5
  Step 3: Roll and move Player 1 to cell 5
  Result: Player 2 chip bumped to home? ✅ PASS / ❌ FAIL

TEST: Own Chip Not Bumped
  Step 1: Place 2 Player 1 chips on board (if possible)
  Step 2: Try to move over own chip
  Result: Own chip cannot be bumped? ✅ PASS / ❌ FAIL

TEST: Bump Score Penalty
  Step 1: Perform a bump
  Step 2: Verify bumped player loses points (if game mode applies)
  Result: Score deducted correctly? ✅ PASS / ❌ FAIL
```

### Test Suite 3: Win Detection

```
TEST: Game 1 Win (25 points)
  Step 1: Manually set player score to 24
  Step 2: Move to earn 1 more point
  Step 3: Click "Declare Win"
  Result: Win detected and game ends? ✅ PASS / ❌ FAIL

TEST: Premature Win Prevention
  Step 1: Player has 10 points (far from win)
  Step 2: Click "Declare Win"
  Result: Error message shown, game continues? ✅ PASS / ❌ FAIL

TEST: Win Screen
  Step 1: Declare a valid win
  Step 2: Observe win screen
  Result: Shows correct winner, play again option? ✅ PASS / ❌ FAIL
```

### Test Suite 4: Turn Order

```
TEST: 4-Player Rotation
  Step 1: Note Player 1 active
  Step 2: Complete turn → Player 2 active
  Step 3: Complete turn → Player 3 active
  Step 4: Complete turn → Player 4 active
  Step 5: Complete turn → Player 1 active again
  Result: Turn order correct? ✅ PASS / ❌ FAIL

TEST: Turn Skipping (if applicable)
  Step 1: Player incurs skip penalty
  Step 2: Verify player skipped in turn order
  Result: Correct players active in sequence? ✅ PASS / ❌ FAIL

TEST: AI Turn (single-player)
  Step 1: In Solitary mode, it becomes AI's turn
  Step 2: Verify AI automatically rolls and moves
  Result: AI turn executes without player input? ✅ PASS / ❌ FAIL
```

### Test Suite 5: State Transitions

```
TEST: Phase Progression
  Step 1: Rolling phase → Player rolls → Playing phase
  Step 2: Playing phase → Player moves → next player Rolling
  Result: Phases transition correctly? ✅ PASS / ❌ FAIL

TEST: Phase-Specific Actions
  Step 1: In Rolling phase, Dice button enabled
  Step 2: In Playing phase, Move buttons (cells) enabled
  Step 3: In Waiting phase, only spectator info shown
  Result: Correct buttons active per phase? ✅ PASS / ❌ FAIL
```

---

## UI Regression Tests

### Test Suite 1: Menu Navigation

```
TEST: Main Menu Buttons
  Step 1: From main menu, click each game mode (Game 1-5)
  Result: Game starts for each? ✅ PASS / ❌ FAIL

TEST: Settings Menu
  Step 1: From main menu, click Settings
  Step 2: Verify settings displayed
  Step 3: Adjust volume slider
  Step 4: Return to menu
  Result: Settings accessible and return works? ✅ PASS / ❌ FAIL

TEST: Rules/Help Screen
  Step 1: From main menu or pause, click Help/Rules
  Step 2: Read through rules
  Step 3: Close/return to menu
  Result: Rules screen displays and closes correctly? ✅ PASS / ❌ FAIL
```

### Test Suite 2: Button Responsiveness

```
TEST: Dice Button
  Step 1: In Rolling phase, click Dice button
  Step 2: During animation, try clicking Dice button again
  Result: Button disabled during action, re-enabled after? ✅ PASS / ❌ FAIL

TEST: Move Buttons (Cells)
  Step 1: Valid moves highlighted
  Step 2: Click on a highlighted cell
  Result: Immediate response, no lag? ✅ PASS / ❌ FAIL

TEST: Menu Buttons
  Step 1: From game, click Pause → Menu appears
  Step 2: Click "Resume" → Back to game
  Step 3: Click "Quit" → Return to main menu
  Result: All buttons functional? ✅ PASS / ❌ FAIL
```

### Test Suite 3: Text & Labels

```
TEST: Score Display
  Step 1: Move chip, earn points
  Step 2: Observe scoreboard updates
  Result: Scores visible and correct? ✅ PASS / ❌ FAIL

TEST: Phase Indicator
  Step 1: Roll dice → see "Rolling Phase" → "Playing Phase"
  Result: Phase text updates? ✅ PASS / ❌ FAIL

TEST: Error Messages
  Step 1: Click Dice in wrong phase → error message
  Step 2: Try invalid move → error message
  Result: Messages display and disappear correctly? ✅ PASS / ❌ FAIL
```

### Test Suite 4: Animations

```
TEST: Button Press Animation
  Step 1: Click any button
  Step 2: Observe button scales down then back up
  Result: Smooth 150ms animation? ✅ PASS / ❌ FAIL

TEST: Chip Movement Animation
  Step 1: Move a chip from cell 0 to cell 5
  Step 2: Observe chip slides smoothly
  Result: 300ms animation, no stuttering? ✅ PASS / ❌ FAIL

TEST: Valid Move Highlight Animation
  Step 1: Roll dice → valid moves glow
  Step 2: Observe pulsing effect
  Result: Smooth pulsing, not janky? ✅ PASS / ❌ FAIL
```

---

## Platform Regression Tests

### WebGL Regression

```
TEST: Browser Load
  Step 1: Visit game URL
  Step 2: Page loads
  Step 3: Click Play
  Result: Game loads < 5 seconds, plays smoothly? ✅ PASS / ❌ FAIL

TEST: Frame Rate (FPS)
  Step 1: Open Chrome DevTools → Performance tab
  Step 2: Record 30 seconds of gameplay
  Step 3: Check FPS graph
  Result: 60 FPS average or stable 30 FPS? ✅ PASS / ❌ FAIL

TEST: Mouse Input
  Step 1: Play full game with mouse
  Step 2: Test hover effects
  Step 3: Test click detection
  Result: All input works, no double-clicks? ✅ PASS / ❌ FAIL

TEST: No Console Errors
  Step 1: Open DevTools Console
  Step 2: Play full game
  Result: No red errors in console? ✅ PASS / ❌ FAIL
```

### Android Regression

```
TEST: App Launch
  Step 1: Tap app icon
  Step 2: App opens
  Result: < 3 second launch time, no crash? ✅ PASS / ❌ FAIL

TEST: Frame Rate
  Step 1: Open Android Profiler
  Step 2: Play full game
  Step 3: Monitor FPS
  Result: 60 FPS on modern device, 30 FPS stable on older? ✅ PASS / ❌ FAIL

TEST: Memory Usage
  Step 1: Monitor Heap in Profiler
  Step 2: Play for 10 minutes
  Result: Memory < 400 MB, no memory leaks? ✅ PASS / ❌ FAIL

TEST: Touch Input
  Step 1: Play with touch
  Step 2: Test tap responsiveness
  Step 3: Test multi-touch (two fingers)
  Result: Touch responsive, multi-touch ignored? ✅ PASS / ❌ FAIL

TEST: Safe Area
  Step 1: On phone with notch, play full game
  Result: UI not hidden by notch, all buttons visible? ✅ PASS / ❌ FAIL
```

### iOS Regression

```
TEST: App Launch
  Step 1: Open from home screen or TestFlight
  Result: < 3 second launch, no crash? ✅ PASS / ❌ FAIL

TEST: Frame Rate
  Step 1: Use Xcode Instruments → Core Animation
  Step 2: Play full game
  Result: Smooth 60 FPS on iPhone 12+, 30 FPS on older? ✅ PASS / ❌ FAIL

TEST: Memory Usage
  Step 1: Monitor Memory in Instruments
  Step 2: Play for 10 minutes
  Result: Memory < 400 MB, stable? ✅ PASS / ❌ FAIL

TEST: Touch Input
  Step 1: Play with touch
  Step 2: Test responsiveness
  Result: No lag, accurate cell taps? ✅ PASS / ❌ FAIL

TEST: Safe Area (Notch)
  Step 1: On iPhone 12+, play full game
  Result: HUD doesn't overlap notch, home indicator area clear? ✅ PASS / ❌ FAIL

TEST: Safe Area (No Notch)
  Step 1: On iPhone SE, play full game
  Result: UI doesn't have wasted space, scales properly? ✅ PASS / ❌ FAIL
```

---

## Performance Regression Tests

### Test Suite: Performance Benchmarks

```
TEST: Load Time
  Step 1: Start app
  Step 2: Time until menu visible
  Target: < 3 seconds
  Result: ✅ PASS / ❌ FAIL [time: ___ sec]

TEST: Scene Load
  Step 1: Start game
  Step 2: Time until board visible
  Target: < 2 seconds
  Result: ✅ PASS / ❌ FAIL [time: ___ sec]

TEST: Animation Smoothness
  Step 1: Play 5 games
  Step 2: Watch for stuttering
  Target: No visible stuttering
  Result: ✅ PASS / ❌ FAIL

TEST: Sustained FPS
  Step 1: Play 30 minutes continuously
  Step 2: Monitor FPS throughout
  Target: FPS stable (no drops > 5 FPS)
  Result: ✅ PASS / ❌ FAIL

TEST: Memory Stability
  Step 1: Play 30 minutes continuously
  Step 2: Monitor memory heap
  Target: No continuous growth, GC handled
  Result: ✅ PASS / ❌ FAIL

TEST: Battery Impact
  Step 1: Play on battery (iOS/Android)
  Step 2: Note battery usage
  Target: Reasonable drain (not excessive)
  Result: ✅ PASS / ❌ FAIL [drain: _% per 10 min]
```

---

## Regression Testing Schedule

### Pre-Bug Fix
- Quick sanity test: Critical path only (~5 min)

### Post-Bug Fix (Minor Changes)
- Critical path full (~15 min)
- Affected feature full test suite (~20 min)
- Total: 35 min per fix

### Post-Bug Fix (Major Changes)
- Critical path full (~15 min)
- Game logic suite (~30 min)
- UI suite (~20 min)
- Platform suite (~30 min)
- Performance suite (~15 min)
- Total: 110 min per major fix

### Daily (Before Release)
- Critical path full (~15 min)
- All test suites (~120 min)
- Performance benchmarks (~20 min)
- Total: 155 min (~2.5 hours)

### Weekly (Before Submission)
- Complete regression suite: 4-5 hours
- All platforms: 8-10 hours (parallel with multiple devices)

---

## Regression Testing Checklist

### Pre-Release (Final Gate)

```
☐ CRITICAL PATH
  ☐ App launches without crash
  ☐ Main menu loads
  ☐ Game starts (all 5 modes)
  ☐ Dice rolls
  ☐ Moves execute
  ☐ Win condition works
  ☐ Can quit gracefully

☐ GAME LOGIC
  ☐ Dice rolls 1-6 only
  ☐ Valid moves calculated correctly
  ☐ Bump logic works
  ☐ Score updates
  ☐ Turn order correct
  ☐ Win detection accurate

☐ UI & PRESENTATION
  ☐ Menus responsive
  ☐ Buttons clickable
  ☐ Text displays correctly
  ☐ Animations smooth
  ☐ No layout issues
  ☐ Scoreboard updates

☐ PLATFORM: WebGL
  ☐ Loads < 5 seconds
  ☐ 60 FPS stable
  ☐ Mouse input works
  ☐ No console errors
  ☐ Responsive on mobile browser

☐ PLATFORM: Android
  ☐ Launches < 3 seconds
  ☐ 60 FPS modern, 30 FPS older
  ☐ Memory < 400 MB
  ☐ Touch responsive
  ☐ Safe area respected
  ☐ No crashes

☐ PLATFORM: iOS
  ☐ Launches < 3 seconds
  ☐ 60 FPS modern, 30 FPS older
  ☐ Memory < 400 MB
  ☐ Touch responsive
  ☐ Safe area respected (notch)
  ☐ No crashes

☐ PERFORMANCE
  ☐ Load time < 3 sec
  ☐ FPS stable 60 or locked 30
  ☐ Memory stable over time
  ☐ No battery drain spike
  ☐ Smooth animations
```

---

## Reporting

### Daily Regression Report

```
DATE: Nov 29, 2025
BUILD: v1.0-release-candidate-1

CRITICAL PATH: ✅ PASS
├─ All steps verified
├─ Platform: Android, iOS, WebGL
└─ Duration: 15 minutes

AFFECTED TEST SUITES: ✅ PASS
├─ Game Logic (Bump Logic fix)
  ├─ Bump detection: PASS
  ├─ Score penalty: PASS
  └─ Bump animation: PASS

PERFORMANCE: ✅ PASS
├─ FPS: 60 stable
├─ Memory: 350 MB (under limit)
└─ Load time: 2.1 sec

RESULT: ✅ ALL REGRESSION TESTS PASS
No new issues introduced.
Safe to proceed.
```

---

## Related Documents

- TEST_PLAN_MASTER.md
- QA_PROCESS.md
- SPRINT_8_QA_PLANNING.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Release Management
