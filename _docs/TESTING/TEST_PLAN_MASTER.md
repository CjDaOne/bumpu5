# Test Plan Master

**Created**: Nov 14, 2025  
**Owner**: QA Lead  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Testing Scope

### Platforms Tested
- **WebGL** (Chrome, Firefox, Safari on desktop & mobile browser)
- **Android** (5 representative devices)
- **iOS** (4 representative devices)

### Devices List

| Platform | Device | OS | RAM | Notes |
|----------|--------|----|----|-------|
| Android | Pixel 5a | 12 | 6GB | Mid-range reference |
| Android | Galaxy S21 | 12 | 8GB | High-end flagship |
| Android | Pixel 6a | 13 | 6GB | Current mid-range |
| Android | Galaxy A51 | 11 | 4GB | Budget device |
| Android | OnePlus 9 | 11 | 8GB | Performance baseline |
| iOS | iPhone 13 | 16 | 4GB | Current standard |
| iOS | iPhone 12 | 15 | 4GB | Previous generation |
| iOS | iPhone SE | 15 | 3GB | Budget option |
| iOS | iPad Air | 16 | 4GB | Tablet (optional) |
| WebGL | Desktop (Chrome) | Windows/Mac | - | Primary browser |
| WebGL | Desktop (Firefox) | Windows/Mac | - | Secondary browser |
| WebGL | Mobile Safari | iOS | - | iOS web |
| WebGL | Chrome Mobile | Android | - | Android web |

---

## Test Scenarios by Game Mode

### Game 1: Bump 5

**Objective**: First player to score 25 points wins

**Setup Test**:
```
Step 1: Select "Game 1: Bump 5" from menu
Step 2: Verify board displays with 12 cells
Step 3: Verify all 4 player chips visible in starting positions
Step 4: Verify scoreboard shows all players at 0 points
Expected: Game ready to start
```

**Normal Turn Test**:
```
Step 1: Roll dice (click Dice button)
Step 2: Observe valid moves highlighted (light blue glow)
Step 3: Select a valid cell (valid move)
Step 4: Observe chip animates from home to selected cell (300ms)
Step 5: Verify score updates correctly (based on cell number)
Step 6: Next player's turn begins
Expected: Smooth animation, score accurate, correct player turn
```

**Bump Test**:
```
Step 1: Position Player 1 chip on cell 5
Step 2: Position Player 2 chip on cell 5
Step 3: Roll dice → valid move includes cell 5
Step 4: Select cell 5 (bump detected)
Step 5: Observe:
   - Player 1 chip stays on cell 5
   - Player 2 chip animates back to home
   - Score updates correctly
Expected: Bump executes smoothly, bumped chip returns to home
```

**Win Test**:
```
Step 1: Player scores to 24 points
Step 2: Roll dice → move reaches 25 points
Step 3: Click "Declare Win" button
Step 4: Verify:
   - "You won!" message appears
   - Option to play again or return to menu
   - Game ends correctly
Expected: Win detection accurate, game ends gracefully
```

**Edge Case - Invalid Win**:
```
Step 1: Player has 24 points (1 point away from win)
Step 2: Click "Declare Win" button prematurely
Step 3: Verify error: "Cannot declare win: need 1 more point"
Expected: Prevents false wins, provides feedback
```

**Edge Case - Multiple Bumps**:
```
Step 1: Roll dice → valid moves include multiple occupied cells
Step 2: Select any occupied cell
Step 3: Verify only the targeted chip bumps (not others)
Expected: Correct bump mechanics, no chain reactions
```

---

### Game 2: Krazy 6

**Objective**: Race around board twice (24 cells total)

**Setup Test**:
```
Step 1: Select "Game 2: Krazy 6" from menu
Step 2: Verify board displays 12 cells (will lap)
Step 3: Verify all 4 players at home
Expected: Game ready, lapping mechanic ready
```

**Lap Mechanics Test**:
```
Step 1: Score to 12 points (first lap)
Step 2: Verify lap counter or visual indicator
Step 3: Move beyond 12 → second lap begins
Step 4: Score to 24 points (complete two laps)
Step 5: Click "Declare Win"
Expected: Lap tracking accurate, win at 24 correct
```

**Special Rules Test** (if any in Krazy 6):
```
[Fill based on actual Krazy 6 rules]
Example: "Sixes roll again" or "Can't move backward"
```

---

### Game 3: Pass The Chip

**Objective**: Pass chip to opponent, avoid holding at end of turn

**Setup Test**:
```
Step 1: Select "Game 3: Pass The Chip"
Step 2: Verify starting player has chip visually highlighted
Step 3: Verify other players have "No Chip" indicator
Expected: Chip ownership clear, game ready
```

**Pass Mechanics Test**:
```
Step 1: Player with chip rolls dice
Step 2: Moves chip to another player's cell
Step 3: Verify chip now belongs to that player
Step 4: Next player must now move the chip
Expected: Chip passes correctly, ownership transfers
```

**Penalty Test**:
```
Step 1: Player with chip at end of turn (couldn't pass)
Step 2: Verify penalty applied (lose points or skip turn)
Step 3: Check score/turn order adjustment
Expected: Penalty logic correct, game continues properly
```

---

### Game 4: Bump U And 5

**Objective**: Combine Bump and 5-point rules

**Setup Test**:
```
Step 1: Select "Game 4: Bump U And 5"
Step 2: Verify board ready with both mechanics
Expected: Game ready for combined rules
```

**Combined Mechanics Test**:
```
Step 1: Roll dice → valid move on opponent's chip
Step 2: Move → bump occurs AND score 5 points
Step 3: Verify:
   - Opponent's chip bumped
   - Score increases by 5 (not just cell value)
Expected: Both mechanics execute together
```

**Win Test**:
```
Step 1: Score with combination of bumps and 5-point moves
Step 2: Reach win condition (game-specific threshold)
Step 3: Declare win
Expected: Win condition based on combined scoring
```

---

### Game 5: Solitary

**Objective**: Single-player puzzle mode (test move all chips home)

**Setup Test**:
```
Step 1: Select "Game 5: Solitary"
Step 2: Verify game shows 4 chips (all owned by player)
Step 3: Verify turn order is: Roll → Move chips
Expected: Single-player setup correct
```

**Move Mechanics Test**:
```
Step 1: Roll dice
Step 2: Valid moves highlight multiple cells
Step 3: Move a chip
Step 4: Roll again → next chip available
Expected: Can move all chips, turn-based correctly
```

**Win Test**:
```
Step 1: Move all 4 chips to home
Step 2: Click "Declare Win"
Step 3: Verify win message
Expected: Puzzle complete, game ends
```

**Failure Condition** (if implemented):
```
Step 1: Get into unwinnable state (optional)
Step 2: Verify feedback or restart option
Expected: Clear communication
```

---

## Platform-Specific Tests

### WebGL Tests

**Browser Compatibility**:
```
Chrome (Latest):
  Step 1: Load game in Chrome
  Step 2: Play one full round
  Step 3: Check console for errors
  Expected: No errors, smooth 60 FPS

Firefox (Latest):
  Step 1: Load game in Firefox
  Step 2: Play one full round
  Expected: Smooth performance, no issues

Safari (Latest):
  Step 1: Load game in Safari (macOS or iPad)
  Step 2: Play one full round
  Expected: Works (may be slightly slower than Chrome)
```

**Performance** (WebGL):
```
Step 1: Monitor FPS while playing (Chrome DevTools)
Step 2: Check memory usage (Task Manager or DevTools)
Step 3: Measure load time (Network tab in DevTools)

Expected:
  - FPS: 60 on desktop, 30+ on mobile browsers
  - Memory: < 300 MB
  - Load time: < 5 seconds
```

**Input** (WebGL):
```
Step 1: Test on desktop with mouse
  - Hover effects work
  - Click detection accurate
  - No double-click issues

Step 2: Test on mobile browser (iPad/Android tablet)
  - Touch detection works
  - No lag between tap and action
  - Multi-touch ignored
  
Expected: All input responsive
```

---

### Android Tests

**Device Compatibility** (5 devices):
```
For each device:
  Step 1: Install APK/AAB
  Step 2: Launch app
  Step 3: Play 2-3 rounds
  Step 4: Check app stability (logcat for errors)
  
Expected: App runs without crashes on all devices
```

**Performance** (Android):
```
Using Android Profiler:
  Step 1: Record FPS during gameplay
  Step 2: Monitor memory (Heap)
  Step 3: Check CPU usage
  
Expected:
  - Modern devices: 60 FPS stable
  - Older devices: 30 FPS locked (no fluctuation)
  - Memory: < 400 MB peak
  - CPU: < 80% sustained
```

**Input** (Android):
```
Step 1: Test tap detection on each device
Step 2: Test with different finger sizes (thumb vs pointer)
Step 3: Verify 56px buttons are comfortable to tap
Step 4: Check double-tap debouncing

Expected: Responsive, no accidental taps
```

**Safe Area** (Android):
```
Step 1: Check on phones with notches (Pixel 3a+)
Step 2: Check on phones with gesture buttons (system navigation)
Step 3: Verify HUD elements not hidden

Expected: All UI visible, not obscured
```

---

### iOS Tests

**Device Compatibility** (4 devices):
```
For each device (via TestFlight):
  Step 1: Install via TestFlight
  Step 2: Launch app
  Step 3: Play 2-3 rounds
  Step 4: Check crash reports in Xcode
  
Expected: App runs stably on all devices
```

**Performance** (iOS):
```
Using Xcode Instruments:
  Step 1: Profile Memory (Allocations)
  Step 2: Profile CPU (Sampler)
  Step 3: Measure FPS (Core Animation)
  
Expected:
  - iPhone 12+: 60 FPS
  - iPhone SE: 30-45 FPS
  - Memory: < 400 MB
  - No sustained high CPU
```

**Safe Area** (iOS):
```
Device: iPhone 12/13+ (with notch)
  Step 1: Run in portrait
  Step 2: Verify HUD doesn't overlap notch
  Step 3: Verify buttons above home indicator
  
Device: iPhone SE (no notch)
  Step 1: Verify UI doesn't have wasted space
  Step 2: Layout should scale properly

Expected: Proper safe area handling on both
```

**Input** (iOS):
```
Step 1: Test tap detection
Step 2: Test on different screen sizes (iPhone SE vs 13 Pro Max)
Step 3: Verify touch feedback/haptics (if implemented)

Expected: Responsive input, correct targeting
```

---

## Bug Severity Definitions

### CRITICAL (Blocks Release)
- **Definition**: Game unplayable or crashes immediately
- **Examples**:
  - Game crashes on startup
  - Infinite loop / freeze
  - Unwinnable state (no valid moves)
  - Score doesn't update (game logic broken)
  - Can't progress to next turn
- **Fix Deadline**: Immediate (same day)
- **Testing**: Regression test after fix

### HIGH (Blocks Feature)
- **Definition**: Feature broken, game playable but incomplete
- **Examples**:
  - Dice button doesn't work
  - Bump logic incorrect (bumps wrong chip)
  - Win condition never triggers
  - Score calculates wrong
  - Menu navigation broken
- **Fix Deadline**: 24 hours
- **Testing**: Re-test affected feature

### MEDIUM (Should Fix)
- **Definition**: Impacts user experience but not critical
- **Examples**:
  - Animation stutters
  - UI misaligned on some devices
  - Button feedback missing
  - Highlights appear wrong color
  - Text too small/unreadable
- **Fix Deadline**: 48 hours
- **Testing**: Re-test affected UI

### LOW (Nice to Fix)
- **Definition**: Minor issues, polish
- **Examples**:
  - Text typo
  - Spacing 2px off
  - Animation 50ms timing
  - Button hover state subtle
  - Sound effect volume slightly loud
- **Fix Deadline**: After release (v1.0.1)
- **Testing**: Manual verification

---

## Pass/Fail Criteria

### Baseline (Must Pass)

- ✅ All 5 game modes playable end-to-end
- ✅ No critical bugs (crashes, logic errors)
- ✅ No high-severity bugs blocking release
- ✅ Game playable on all target platforms
- ✅ FPS target met (60 stable or capped 30)
- ✅ Memory under limits
- ✅ All platforms: < 2 HIGH severity bugs max

### Success Criteria

- ✅ Zero critical bugs found in final test
- ✅ All game logic verified correct
- ✅ UI responsive on all devices
- ✅ Load times < 5 seconds
- ✅ Safe areas respected (mobile)
- ✅ All animations smooth
- ✅ Ready for launch ✅

### No-Go Criteria (Stop Release)

- ❌ Any critical bug still present
- ❌ > 3 HIGH severity bugs remaining
- ❌ FPS consistently below target
- ❌ Any platform untested
- ❌ Crashes on startup
- ❌ Memory exceeds limits

---

## Test Data & Environment

### Test Accounts
- Not needed for MVP (offline single-player)

### Test Data
- No save data in v1.0 (stateless)
- Use default starting conditions

### Test Environment
- WebGL: Chrome DevTools (desktop simulation)
- Android: Android Emulator + 5 real devices
- iOS: Simulator + 4 real devices via TestFlight

### Tools
- **Android**: Android Studio Profiler, logcat
- **iOS**: Xcode Instruments, Console
- **WebGL**: Chrome DevTools Performance tab
- **General**: Microsoft Excel (test log spreadsheet)

---

## Test Execution Schedule

### Week 1: Component Testing
- Each game mode, each platform
- ~ 40 scenarios

### Week 2: Integration Testing
- Full game flow (menu → gameplay → end)
- Multi-mode transitions
- Settings persistence
- ~ 20 scenarios

### Week 3: Platform Stress Testing
- Performance under load
- Sustained gameplay (30+ min)
- Device-specific edge cases
- ~ 15 scenarios

### Week 4: Regression Testing
- Verify bug fixes
- Re-test all critical paths
- Final sign-off
- ~ 25 scenarios

---

## Test Reporting

### Daily Log
```
Date: Nov 20, 2025
Device: Pixel 5a (Android 12)
Tester: QA1

Test: Game 1 - Bump 5, Normal Turn
Status: PASS
Notes: Chip animated correctly, score updated

Test: Game 2 - Krazy 6, Win Declaration
Status: FAIL - BUG #47
Notes: Win modal doesn't close after "Play Again"
Severity: HIGH
```

### Bug Report Template
```
Title: [Brief description]
Severity: [CRITICAL/HIGH/MEDIUM/LOW]
Platform: [WebGL/Android/iOS]
Device: [Device model & OS version]
Game Mode: [Game 1-5]
Reproducibility: [Always/Sometimes/Rarely]

Steps to Reproduce:
1. [Step 1]
2. [Step 2]
3. [Step 3]

Expected Result: [What should happen]
Actual Result: [What does happen]

Screenshot/Video: [Link or embedded]
```

### Weekly Summary Report
```
Week of: Nov 20, 2025
Total Tests Run: 120
Pass Rate: 95%
Bugs Found: 5 (1 CRITICAL, 2 HIGH, 2 MEDIUM)
Bugs Fixed: 3
Bugs Remaining: 2

Status: PROCEED (1 HIGH bug being worked)
Next Week: Platform stress testing
```

---

## Related Documents

- QA_PROCESS.md
- REGRESSION_TESTING.md
- SPRINT_8_QA_PLANNING.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Testing
