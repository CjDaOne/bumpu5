# TEAM DISPATCH - SPRINT 3 IMMEDIATE EXECUTION
## Game Modes Implementation (Game1-Game5)

**Issued By**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025  
**Effective Immediately**  
**Status**: 🚀 **TEAMS MOBILIZED**

---

## EXECUTIVE DISPATCH SUMMARY

All teams are proceeding with next phase regardless of calendar date. Sprint 2 sign-off complete. Sprint 3 execution begins immediately.

**Critical Path**:
- **🎮 Gameplay Team**: BEGIN Sprint 3 immediately (Game Modes 1-5)
- **🎨 UI Team**: Standby mode - review Sprint 4 requirements
- **🎲 Board Team**: Standby mode - design BoardGridManager architecture
- **⚙️ Build Team**: Standby mode - prepare build pipeline
- **🧪 QA Team**: Monitor - begin test plan documentation

---

## 🎮 GAMEPLAY ENGINEERING TEAM - SPRINT 3

**Lead**: Gameplay Engineer Agent  
**Assignment**: Implement all 5 Game Modes  
**Duration**: 7 days  
**Blockers**: None - proceed immediately

### CRITICAL REQUIREMENTS

#### **IGameMode Interface Definition**
**File**: `Assets/Scripts/Modes/IGameMode.cs`

```csharp
public interface IGameMode
{
    // Properties
    string ModeName { get; }
    string ModeLongName { get; }
    int MaxPlayers { get; }
    bool Use5InARow { get; }
    bool UseTripleDoublesPenalty { get; }
    bool Use5Plus6Safe { get; }
    
    // Win Condition
    bool CheckWinCondition(Player player, BoardModel board);
    
    // Special Rules
    void ApplySpecialRules(GameStateManager stateManager, int[] diceRoll);
    void ApplyRollModifier(ref int[] roll);
    void ApplyBumpModifier(Player bumper, Chip bumpedChip);
    
    // Initialization
    void Initialize(GameStateManager stateManager);
}
```

#### **Game 1: Bump 5 (Standard Game)**
**File**: `Assets/Scripts/Modes/Game1_Bump5.cs`

**Rules**:
- ✅ Standard 5-in-a-row to win
- ✅ Rolling a 6 loses turn
- ✅ 5+6 is a safe roll (no placement)
- ✅ Doubles allow rolling again
- ✅ Triple doubles loses turn
- ✅ Bumping opponent chips removes them from board
- ✅ First player to get 5 chips in a row wins

**Implementation Checklist**:
```
□ CheckWinCondition: Implement 5-in-a-row detection
□ ApplySpecialRules: Pass-through (no special rules)
□ ApplyRollModifier: None (standard dice)
□ ApplyBumpModifier: Standard bump (remove chip)
□ Unit Tests: 5+ tests
```

#### **Game 2: Krazy 6**
**File**: `Assets/Scripts/Modes/Game2_Krazy6.cs`

**Rules**:
- ✅ Rolling a 6 is GOOD (continue turn)
- ✅ Rolling a 6 twice = bonus movement
- ✅ 5-in-a-row to win
- ✅ Triple doubles still loses turn
- ✅ Bumping removes opponent chips

**Implementation Checklist**:
```
□ CheckWinCondition: Standard 5-in-a-row
□ ApplySpecialRules: Override 6-roll penalty (becomes bonus)
□ ApplyRollModifier: Doubles on 6-6 = extra movement
□ ApplyBumpModifier: Standard bump
□ Unit Tests: 5+ tests
```

#### **Game 3: Pass The Chip**
**File**: `Assets/Scripts/Modes/Game3_PassTheChip.cs`

**Rules**:
- ✅ No bumping - chips stay on board
- ✅ Landing on opponent's chip = swap positions
- ✅ 5-in-a-row to win
- ✅ Rolling a 6 loses turn (standard)
- ✅ Triple doubles loses turn

**Implementation Checklist**:
```
□ CheckWinCondition: 5-in-a-row
□ ApplySpecialRules: Disable bumping, enable swapping
□ ApplyBumpModifier: Swap positions instead of bump
□ Unit Tests: 5+ tests covering swap logic
```

#### **Game 4: Bump U & 5**
**File**: `Assets/Scripts/Modes/Game4_BumpUAnd5.cs`

**Rules**:
- ✅ Combine Bump5 + Krazy6
- ✅ Rolling 6 = good (no penalty)
- ✅ Double 6s = extra bonus roll
- ✅ Bumping removes chips
- ✅ 5-in-a-row to win

**Implementation Checklist**:
```
□ CheckWinCondition: 5-in-a-row
□ ApplySpecialRules: Hybrid rules (Game1 + Game2)
□ ApplyRollModifier: Game 2 logic
□ ApplyBumpModifier: Game 1 logic
□ Unit Tests: 8+ tests (combinations)
```

#### **Game 5: Solitary**
**File**: `Assets/Scripts/Modes/Game5_Solitary.cs`

**Rules**:
- ✅ Single player mode
- ✅ Place chips to reach 5-in-a-row
- ✅ Each turn: roll dice, place chip, declare win
- ✅ No opponent bumping
- ✅ Scoreboard tracks best time/fewest rolls

**Implementation Checklist**:
```
□ CheckWinCondition: 5-in-a-row (single player)
□ ApplySpecialRules: Single player mode
□ Initialize: Reset timer/roll counter
□ Unit Tests: 5+ tests
□ Features: Time tracking, roll counter
```

### DELIVERABLES CHECKLIST

#### Code Files (5 Game Mode Classes)
- [ ] Assets/Scripts/Modes/IGameMode.cs (interface definition)
- [ ] Assets/Scripts/Modes/Game1_Bump5.cs (complete implementation)
- [ ] Assets/Scripts/Modes/Game2_Krazy6.cs (complete implementation)
- [ ] Assets/Scripts/Modes/Game3_PassTheChip.cs (complete implementation)
- [ ] Assets/Scripts/Modes/Game4_BumpUAnd5.cs (complete implementation)
- [ ] Assets/Scripts/Modes/Game5_Solitary.cs (complete implementation)
- [ ] Assets/Scripts/Modes/GameModeFactory.cs (helper/registry)

#### Test Files
- [ ] Assets/Scripts/Tests/GameModeTests.cs (40+ tests)
  - [ ] 5+ Game1 tests
  - [ ] 5+ Game2 tests
  - [ ] 5+ Game3 tests
  - [ ] 8+ Game4 tests (combinations)
  - [ ] 5+ Game5 tests
  - [ ] 7+ IGameMode interface tests

#### Documentation
- [ ] SPRINT_3_IMPLEMENTATION.md (detailed progress)
- [ ] Game Mode Rules Documentation
- [ ] API Documentation for IGameMode

### CODE QUALITY STANDARDS

**Requirements** (from CODING_STANDARDS.md):
- ✅ 100% public methods documented with /// comments
- ✅ Clear variable names (no abbreviations)
- ✅ Single Responsibility Principle
- ✅ No duplicate code
- ✅ Follows existing code patterns from Sprint 1-2
- ✅ Each game mode extends IGameMode interface
- ✅ All special rules tested independently

### TESTING REQUIREMENTS

**Unit Test Coverage**:
- ✅ Every game mode has minimum 5 tests
- ✅ Test win condition detection
- ✅ Test special rules application
- ✅ Test integration with GameStateManager
- ✅ Test edge cases (e.g., Game3 swap logic)
- ✅ Target: 80%+ coverage

**Test Examples**:
```csharp
// Game1_Bump5 Tests
- Test5InARowDetection()
- TestStandardBumpRemovesChip()
- TestRolling6LosesTurn()
- TestDoublesAllowRollAgain()
- TestTripleDoublesLoseTurn()

// Game2_Krazy6 Tests
- TestRolling6GivesBonus()
- TestDouble6ExtraMovement()
- TestWinDetectionStandard()
- TestTripleDoublesPenalty()

// Game3_PassTheChip Tests
- TestSwapLogicOnLanding()
- TestNoBumpingInThisMode()
- TestWinDetection()
- TestSwapProperlySwapsPositions()
- TestSwapEventFiring()
```

### MILESTONE GATES

**Day 1-2: IGameMode Interface + Game1**
- ✅ IGameMode.cs complete
- ✅ Game1_Bump5.cs complete & tested
- ✅ 10+ tests passing

**Day 3-4: Game2 & Game3**
- ✅ Game2_Krazy6.cs complete & tested
- ✅ Game3_PassTheChip.cs complete & tested
- ✅ 20+ tests passing

**Day 5-6: Game4 & Game5**
- ✅ Game4_BumpUAnd5.cs complete & tested
- ✅ Game5_Solitary.cs complete & tested
- ✅ 35+ tests passing

**Day 7: Polish & Code Review**
- ✅ All 40+ tests passing
- ✅ Code review by Managing Engineer
- ✅ Final documentation

### INTEGRATION POINTS

**With Sprint 1-2**:
- GameStateManager.Initialize() with IGameMode parameter
- GameStateManager.ApplyGameModeRules() hook
- BoardModel.Check5InARow() used by all modes

**With Sprint 3 (Future)**:
- GameModeFactory.CreateGameMode(int modeNumber)
- UI passing selected game mode to GameStateManager

### SUCCESS CRITERIA

✅ All 5 game modes fully playable end-to-end  
✅ 40+ unit tests with 100% pass rate  
✅ IGameMode interface flexible for future modes  
✅ Zero critical bugs in mode-specific logic  
✅ All code reviewed and approved  
✅ Complete documentation for each mode  

---

## 🎨 UI ENGINEERING TEAM - SPRINT 5 PREP

**Lead**: UI Engineer Agent  
**Assignment**: Prepare for Sprint 5 (HUD & UI Framework)  
**Duration**: Standby + Design Phase  

### IMMEDIATE ACTIONS (This Week)

1. **Review Sprint 4 Requirements**
   - Read SPRINT_4_KICKOFF.md
   - Understand board integration points
   - Plan HUD layout for board

2. **Design HUD Layout**
   - Dice Roll Button placement (top-left or center?)
   - BUMP Button placement (context-sensitive)
   - Declare Win Button placement
   - Scoreboard position (top/bottom?)
   - Phase indicator location

3. **Design Popup System**
   - PENALTY popup template
   - PASS THE CHIP popup template
   - TAKE IT OFF popup template
   - Modal/non-modal decision

4. **Create Design Documents**
   - HUD Wireframe (paper or Figma)
   - Button State Machine (enabled/disabled by phase)
   - Canvas hierarchy plan
   - Responsive design approach

### FILES TO CREATE (Sprint 5)
- HUDManager.cs
- DiceRollButton.cs (with animation)
- BumpButton.cs (context-sensitive)
- DeclareWinButton.cs
- ScoreboardDisplay.cs
- PopupManager.cs
- PopupPrefab + variants
- GameModeSelectionScreen.cs
- PhaseIndicator.cs

### SPRINT 5 START DATE
**Dec 12, 2025** (after Sprint 4 board integration complete)

---

## 🎲 BOARD ENGINEERING TEAM - SPRINT 4 PREP

**Lead**: Board Engineer Agent  
**Assignment**: Prepare for Sprint 4 (Board System Integration)  
**Duration**: Standby + Architecture Phase  

### IMMEDIATE ACTIONS (This Week)

1. **Design BoardGridManager**
   - 12-cell board layout (which pattern? circular? square?)
   - Cell dimensions & spacing
   - Chip visual representation
   - Prefab hierarchy

2. **Plan Cell Interaction System**
   - Cell prefab structure
   - Clickable collider setup
   - Highlight system (valid moves)
   - Tap detection for mobile

3. **Design Chip Visuals**
   - Player1 chip appearance
   - Player2 chip appearance
   - State indicators (selected, hovered)
   - Animations (drop, bump, move)

4. **Create Architecture Document**
   - BoardGridManager class design
   - BoardCellView & ChipView prefab structure
   - BoardInputHandler design
   - Animation system plan

### FILES TO CREATE (Sprint 4)
- BoardGridManager.cs
- BoardCellView.cs
- ChipView.cs
- BoardInputHandler.cs
- BoardLayoutConfiguration.cs
- ValidMoveHighlighter.cs

### SPRINT 4 START DATE
**Dec 5, 2025** (after Sprint 3 game modes complete)

---

## ⚙️ BUILD ENGINEERING TEAM - SPRINT 7 PREP

**Lead**: Build Engineer Agent  
**Assignment**: Prepare for Sprint 7 (Platform Builds)  
**Duration**: Research & Preparation  

### IMMEDIATE ACTIONS (This Week)

1. **WebGL Build Pipeline**
   - Verify IL2CPP configuration
   - Check compression settings
   - Document build size targets
   - Performance profiling tools

2. **Android Configuration**
   - Target API level research
   - APK signing process review
   - Play Store submission requirements
   - Performance targets (30 FPS minimum)

3. **iOS Configuration**
   - Xcode export setup
   - Provisioning profile requirements
   - Safe area implementation plan
   - Performance targets (30 FPS minimum)

4. **Create Build Checklist**
   - Build command documentation
   - Platform-specific settings
   - Optimization procedures
   - Testing procedures per platform

### SPRINT 7 START DATE
**Dec 26, 2025** (after Sprint 6 full game loop complete)

---

## 🧪 QA ENGINEERING TEAM - MONITORING

**Lead**: QA Lead Agent  
**Assignment**: Test Plan Documentation  
**Duration**: Ongoing preparation  

### IMMEDIATE ACTIONS (This Week)

1. **Create Test Plan Document**
   - All 5 game modes test scenarios
   - Platform-specific test matrix
   - Device/browser test list
   - Edge case catalog

2. **Define Bug Severity Levels**
   - Critical (game-breaking)
   - Major (feature broken)
   - Minor (cosmetic)
   - Nice-to-have (enhancements)

3. **Prepare Test Tracking**
   - Bug report template
   - Test result tracking spreadsheet
   - Regression test checklist
   - Platform-specific issues

4. **Monitor Sprint 3 Execution**
   - Attend daily standups
   - Review game mode implementations
   - Flag potential issues
   - Prepare edge case scenarios

### SPRINT 8 START DATE
**Jan 2, 2026** (after Sprint 7 platform builds complete)

---

## DAILY STANDUP SCHEDULE

**Time**: 9 AM UTC (starting tomorrow)  
**Duration**: 15 minutes  
**Participants**: All teams  

### Standup Format
Each team reports:
- ✅ Completed since last standup
- 🔄 In progress
- 🚫 Blockers (if any)
- 📅 Today's plan

### Escalation
- Blockers reported to Managing Engineer immediately
- Managing Engineer available for guidance
- Final decisions made by Managing Engineer

---

## COMMUNICATION CHANNELS

- **#gameplay**: Gameplay team updates
- **#ui**: UI team updates
- **#board**: Board team updates
- **#build**: Build team updates
- **#qa**: QA team updates
- **#blockers**: Cross-team issues
- **#general**: Overall project status

---

## WEEKLY SPRINT REVIEW

**Time**: Friday 5 PM UTC  
**Duration**: 30 minutes  

### Agenda
1. Demo of completed work (10 min)
2. Team standups (10 min)
3. Next sprint planning (10 min)

---

## PROJECT CRITICAL PATH

```
Sprint 1 ✅ (Complete)
Sprint 2 ✅ (Complete - Just Signed Off)
    ↓
Sprint 3 🚀 (ACTIVE - Gameplay Team)
    ↓
Sprint 4 (Dec 5 - Board Team)
    ↓
Sprint 5 (Dec 12 - UI Team)
    ↓
Sprint 6 (Dec 19 - Integration)
    ↓
Sprint 7 (Dec 26 - Builds)
    ↓
Sprint 8 (Jan 2 - QA)
```

---

## MANAGING ENGINEER OVERSIGHT

**Role**: Active management & unblocking  
**Responsibilities**:
- Daily standup monitoring
- Code review (all commits)
- Architecture decisions
- Cross-team coordination
- Issue escalation

**Response Time**:
- Blockers: < 1 hour
- Code review: < 4 hours
- General questions: < 24 hours

---

**TEAMS: PROCEED WITH FULL EXECUTION**

This dispatch supersedes any calendar constraints. All teams proceed as assigned, regardless of date.

**Status**: 🚀 **DISPATCH AUTHORIZED**

---

*End of Dispatch Order*
