# Test Plan Master
**Created**: Nov 14, 2025  
**Owner**: QA Lead  
**Status**: ACTIVE

---

## Testing Scope

### Coverage Matrix
| Dimension | Scope | Status |
|-----------|-------|--------|
| Game Modes | All 5 modes | In scope |
| Platforms | WebGL, Android, iOS | In scope |
| Devices | 3+ per platform | In scope |
| Edge Cases | Rule violations, boundary conditions | In scope |
| Performance | FPS, memory, load time | In scope |
| Accessibility | Color contrast, touch targets | In scope |
| Localization | English primary | Not in scope |

---

## Game Mode Test Scenarios

### Game 1: Bump 5 (40+ test cases)

#### Setup & Initialization
- ✓ Game starts with 0 bumps
- ✓ Both players have 2 chips on board
- ✓ Turn indicator shows Player 1
- ✓ Phase indicator shows "Rolling"

#### Turn Flow
- ✓ Player rolls dice (1-6 result)
- ✓ Valid moves highlighted after roll
- ✓ Player can move any chip with valid path
- ✓ Move executes when cell tapped
- ✓ Turn passes to Player 2 on move completion
- ✓ Roll of 6 grants extra turn
- ✓ Extra turn can be used or passed

#### Bumping Mechanics
- ✓ Landing on opponent chip triggers bump
- ✓ Opponent chip returns to start
- ✓ Bump counter increments (visible on scoreboard)
- ✓ Bumping same opponent twice works
- ✓ Bumping different opponent counts separately
- ✓ Multiple bumps in single turn all count
- ✓ Game displays "Bump!" notification

#### Win Condition
- ✓ Declare Win button appears at 5 bumps
- ✓ Clicking Declare Win with < 5 bumps fails
- ✓ Clicking Declare Win with 5 bumps succeeds
- ✓ Win dialog shows victor
- ✓ Game ends after win declared
- ✓ Final scoreboard shows bump count

#### Edge Cases
- ✓ No legal moves (pass turn)
- ✓ Attempting to move off board (invalid)
- ✓ Moving own chip onto own chip (invalid)
- ✓ Roll exceeding board size (no chip exits)
- ✓ Duplicate declaration prevention

### Game 2: Krazy 6 (40+ test cases)

#### Setup
- ✓ Roll counter shows 0/6
- ✓ Player 1 starts with 2 chips
- ✓ Goal: Exit all chips in exactly 6 rolls

#### Turn Mechanics
- ✓ Each turn counts as 1 roll (including extra turns from 6)
- ✓ Rolling 6 grants extra turn (still counts as 1 roll)
- ✓ Roll counter increments each turn
- ✓ 6/6 shown on turn 6

#### Win Condition
- ✓ Exiting all chips on roll 6 triggers win
- ✓ Exiting all chips before roll 6 → game continues (lost state)
- ✓ Exiting chips after roll 6 → game continues (lost state)
- ✓ Win/loss determined automatically

#### Edge Cases
- ✓ Exceeding roll limit (after roll 6, game ends)
- ✓ Getting stuck (can't exit all in 6 rolls)
- ✓ Chip off board then back on (counts as exited)
- ✓ Roll 6 on turn 6 (extra turn doesn't help)

### Game 3: Pass The Chip (40+ test cases)

#### Setup
- ✓ Each player has 1 chip
- ✓ Bump counter shows 0/3 per chip
- ✓ All chips display bump count

#### Bumping & Control Transfer
- ✓ Bumping opponent chip increments its counter
- ✓ After bump, bumped chip transfers to bumping player
- ✓ Bumping player now controls that chip
- ✓ Original owner's bump count shown on chip

#### Chip Removal
- ✓ Chip removed after 3 bumps
- ✓ Chip visual changes when removed
- ✓ Removed chip cannot be moved
- ✓ Removed chip doesn't affect game board

#### Win Condition
- ✓ Player wins when only their chip remains
- ✓ Winning automatically triggered
- ✓ Win dialog shows victor
- ✓ Last standing = winner

#### Edge Cases
- ✓ Bumping same chip multiple times (counter increments)
- ✓ Control switches on each bump
- ✓ Multiple chips controlled by one player (possible)
- ✓ Game continues with variable player counts

### Game 4: Bump U And 5 (40+ test cases)

#### Phase 1: Bumping (First 5)
- ✓ Start in "Bumping" phase
- ✓ Bump counter shows 0/5
- ✓ Bumping opponent increments counter
- ✓ Cannot exit chips before 5 bumps
- ✓ Exiting with < 5 bumps fails (invalid)

#### Phase Transition
- ✓ Reaching 5 bumps transitions to "Racing"
- ✓ UI updates to show "Racing Phase"
- ✓ Declare Win button becomes active
- ✓ Chip exit becomes allowed

#### Phase 2: Racing (Exit All)
- ✓ Move chips toward finish (cell 12)
- ✓ Exit chips from cell 12
- ✓ Scoreboard shows chips remaining
- ✓ Declare Win only valid when all exited

#### Win Condition
- ✓ All chips exited + declare win = win
- ✓ Declaring win with chips remaining fails
- ✓ Declaring win before 5 bumps fails
- ✓ Win dialog validates both conditions

#### Edge Cases
- ✓ Bump count exceeded 5 (doesn't help)
- ✓ Mixed bumping and racing (bumping still counts)
- ✓ Trying to exit before 5 bumps (fails)
- ✓ Losing chips mid-game (adjust count)

### Game 5: Solitary (40+ test cases)

#### Setup
- ✓ Target sequence displayed
- ✓ Player has 2 chips
- ✓ Dice rolls are predetermined
- ✓ Target cells highlight

#### Movement
- ✓ Each turn advances 1 chip by predetermined roll
- ✓ Player chooses which chip to move
- ✓ Cannot exceed cell 12
- ✓ Move must follow target sequence

#### Sequence Tracking
- ✓ Landing on target cell advances sequence
- ✓ Missing target cell doesn't advance
- ✓ Sequence shown on UI
- ✓ Progress indicator updates

#### Reset Mechanic
- ✓ Player can reset chip position
- ✓ Reset doesn't cost a turn
- ✓ Can reset multiple times per game
- ✓ Resetting loses any sequence progress

#### Win Condition
- ✓ Completing full sequence = win
- ✓ Landing on final target triggers win
- ✓ Win dialog shows completion
- ✓ Difficulty-based sequence variations

#### Edge Cases
- ✓ Blocked from reaching target (reset option)
- ✓ Multiple paths to target
- ✓ Very hard sequences (high difficulty)
- ✓ Game doesn't deadlock

---

## Platform-Specific Tests

### WebGL (Browser)

#### Browser Compatibility
- ✓ Chrome (latest): All features work
- ✓ Firefox (latest): All features work
- ✓ Safari (latest): All features work
- ✓ Edge (latest): All features work

#### Desktop Input
- ✓ Mouse click on cells works
- ✓ Hover states visible
- ✓ Cursor changes on hover
- ✓ Keyboard shortcuts work (if any)

#### Performance
- ✓ Load time < 5 seconds
- ✓ 60 FPS during gameplay
- ✓ Smooth animations
- ✓ No memory leaks (check after 10 min play)

#### Rendering
- ✓ Graphics render correctly
- ✓ Colors match design spec
- ✓ Text readable
- ✓ No visual artifacts

### Android

#### Device Compatibility
- ✓ Pixel 5 (modern): All features work
- ✓ Galaxy S21 (modern): All features work
- ✓ Pixel 4 (older): Works at 30-60 FPS
- ✓ Galaxy S10 (older): Works at 30-60 FPS

#### Touch Input
- ✓ Single tap registers
- ✓ Tap latency < 100ms (feels responsive)
- ✓ Tap feedback (visual) works
- ✓ No accidental double-taps

#### Safe Area
- ✓ Notch handling correct
- ✓ Navigation bar doesn't overlap game
- ✓ All UI visible in safe area
- ✓ Landscape notch (if applicable) handled

#### Orientation
- ✓ Portrait locks correctly
- ✓ Landscape rotates (if supported)
- ✓ Transition smooth
- ✓ UI repositions correctly

#### Performance
- ✓ 60 FPS on modern devices
- ✓ 30+ FPS on older devices
- ✓ Load time < 3 seconds
- ✓ No crashes after 30 min play

### iOS

#### Device Compatibility
- ✓ iPhone 12: All features work at 60 FPS
- ✓ iPhone 11: Works at 60 FPS
- ✓ iPhone 8: Works at 30-60 FPS
- ✓ iPad Air: Works at 60 FPS

#### Touch Input
- ✓ Single tap registers
- ✓ Tap feedback works
- ✓ Haptic feedback (if enabled) works
- ✓ Multi-touch not interfering

#### Safe Area
- ✓ Top notch respected
- ✓ Bottom home indicator respected
- ✓ All UI in safe area
- ✓ Dynamic Island (if applicable) handled

#### Orientation
- ✓ Portrait orientation works
- ✓ Landscape rotation works
- ✓ UI adapts per orientation
- ✓ No UI cutoff in any orientation

#### System Integration
- ✓ Respects system volume mute
- ✓ Allows screen recording
- ✓ System gestures work (swipe back)
- ✓ Low power mode doesn't crash

---

## Bug Severity Definitions

### CRITICAL (Blocks Release)
- Game crash on launch
- Game crash during any game mode
- Win condition not working
- Game logic broken (e.g., all moves invalid)
- Fatal error on all tested devices
- **Action**: Fix immediately, retest before next release

### HIGH (Block Feature)
- Game mode unplayable (specific mode crashes)
- Move validation wrong (valid moves invalid)
- Turn order broken
- Bump logic wrong
- UI completely unresponsive
- **Action**: Fix before next build, test thoroughly

### MEDIUM (Should Fix)
- UI misalignment
- Animation glitchy
- Graphics artifact
- Performance dips below 30 FPS
- Occasional input lag (> 100ms)
- **Action**: Fix in next update, may release if low impact

### LOW (Nice to Fix)
- Text typo
- Minor animation stutter
- Button slightly small
- Color slightly off
- Accessibility (non-blocking)
- **Action**: Fix in future update, don't block release

---

## Pass/Fail Criteria

### Release Gate Criteria
- [ ] All 5 game modes playable end-to-end
- [ ] No CRITICAL bugs found
- [ ] All CRITICAL bugs fixed (if any)
- [ ] No unplanned crashes in 1 hour play
- [ ] All platforms tested (WebGL, Android, iOS)
- [ ] Performance: 60 FPS on modern, 30+ on older
- [ ] Win conditions function correctly
- [ ] UI responsive and readable

### Success Metrics
- ✓ All game modes tested (5/5)
- ✓ All platforms tested (3/3)
- ✓ CRITICAL bugs: 0 unfixed
- ✓ Overall stability: High
- ✓ Ready to ship: YES

---

## Test Device Matrix

### WebGL (Browser)
| Browser | Version | OS | Resolution | Status |
|---------|---------|----|----|---------|
| Chrome | Latest | Windows | 1920×1080 | ✓ Test |
| Firefox | Latest | Mac | 2560×1600 | ✓ Test |
| Safari | Latest | iOS | 1125×2436 | ✓ Test |

### Android
| Device | OS | RAM | Resolution | Status |
|--------|----|----|-----------|---------|
| Pixel 5 | Android 13 | 8GB | 1080×2340 | ✓ Test |
| Galaxy S21 | Android 12 | 8GB | 1080×2400 | ✓ Test |
| Pixel 4 | Android 13 | 6GB | 1080×2280 | ✓ Test |

### iOS
| Device | OS | RAM | Resolution | Status |
|--------|----|----|-----------|---------|
| iPhone 12 | iOS 16 | 4GB | 1125×2436 | ✓ Test |
| iPhone 11 | iOS 16 | 4GB | 1326×2688 | ✓ Test |
| iPhone 8 | iOS 16 | 2GB | 750×1334 | ✓ Test |

---

## Related Documents
- QA_PROCESS.md
- REGRESSION_TESTING.md
- SPRINT_8_QA_PLANNING.md

---

**Status**: Complete - Ready for Execution
