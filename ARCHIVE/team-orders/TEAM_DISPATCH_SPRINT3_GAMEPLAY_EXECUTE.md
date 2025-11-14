# GAMEPLAY TEAM - SPRINT 3 EXECUTION DISPATCH
## Game Modes Implementation - EXECUTE NOW

**From**: Amp (Managing Engineer)  
**To**: Gameplay Engineer Agent  
**Date**: Nov 14, 2025  
**Authority**: IMMEDIATE EXECUTION ORDER  
**Deadline**: Nov 21, 2025 (7 days)

---

## MISSION CRITICAL

Implement 5 complete game modes + interface + 40+ unit tests to unlock downstream sprint work (Board Integration, UI, etc.).

**This is the critical path blocker. All other teams waiting on completion.**

---

## EXECUTION CHECKLIST

### PHASE 1: Interface Definition (Day 1-2)
- [ ] Create file: `Assets/Scripts/Modes/IGameMode.cs`
- [ ] Implement interface with all required methods:
  - `ModeName`, `ModeLongName`, `MaxPlayers` (properties)
  - `Use5InARow`, `UseTripleDoublesPenalty`, `Use5Plus6Safe`, `RollingASixLosesTurn`, `AllowBumping` (rule flags)
  - `CheckWinCondition(Player, BoardModel)` - returns bool
  - `OnTurnStart(GameStateManager, Player)` - void
  - `OnDiceRoll(GameStateManager, Player, int dice)` - void
  - `OnChipPlacement(GameStateManager, Player, BoardCell)` - void
  - `OnGameEnd(GameStateManager)` - void
- [ ] Full XML documentation (/// comments)
- [ ] Copy from SPRINT_3_DETAILED_BRIEFING.md (lines 34-150+)
- [ ] Commit to git

**Target**: End of Day 2

---

### PHASE 2: Game Mode Implementations (Day 2-5)

#### Game 1: Bump5 (Standard Game)
**File**: `Assets/Scripts/Modes/Game1_Bump5.cs`

```csharp
public class Game1_Bump5 : IGameMode
{
    // Properties
    public string ModeName => "Bump5";
    public string ModeLongName => "Bump 5 in a Row";
    public int MaxPlayers => 4;
    
    // Rules
    public bool Use5InARow => true;
    public bool UseTripleDoublesPenalty => true;
    public bool Use5Plus6Safe => true;
    public bool RollingASixLosesTurn => false;
    public bool AllowBumping => true;
    
    // Win condition: 5 in a row on board
    public bool CheckWinCondition(Player player, BoardModel board)
    {
        // Check if player has 5 consecutive chips on board
        // Implementation: iterate through board cells, find sequences
        return HasFiveInARow(player, board);
    }
    
    // Hooks
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer) { }
    public void OnDiceRoll(GameStateManager stateManager, Player player, int dice) { }
    public void OnChipPlacement(GameStateManager stateManager, Player player, BoardCell cell) { }
    public void OnGameEnd(GameStateManager stateManager) { }
}
```

**Key Rules**:
- Win condition: 5 chips in a row (horizontal, vertical, or diagonal)
- Triple doubles lose turn
- 5+6 is a safe roll
- Bumping allowed
- Standard dice rules apply

**Target**: End of Day 3

---

#### Game 2: Krazy6
**File**: `Assets/Scripts/Modes/Game2_Krazy6.cs`

```csharp
public class Game2_Krazy6 : IGameMode
{
    public string ModeName => "Krazy6";
    public string ModeLongName => "Krazy 6";
    public int MaxPlayers => 4;
    
    public bool Use5InARow => false;
    public bool UseTripleDoublesPenalty => false;
    public bool Use5Plus6Safe => false;
    public bool RollingASixLosesTurn => false;
    public bool AllowBumping => true;
    
    // Win condition: Get all 4 chips home
    public bool CheckWinCondition(Player player, BoardModel board)
    {
        return AllChipsHome(player, board);
    }
    
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer) { }
    public void OnDiceRoll(GameStateManager stateManager, Player player, int dice) { }
    public void OnChipPlacement(GameStateManager stateManager, Player player, BoardCell cell) { }
    public void OnGameEnd(GameStateManager stateManager) { }
}
```

**Key Rules**:
- Win condition: All 4 chips home (no 5-in-a-row requirement)
- NO triple doubles penalty
- 5+6 NOT a safe roll
- Standard bumping rules
- Faster game than Bump5

**Target**: End of Day 4

---

#### Game 3: PassTheChip
**File**: `Assets/Scripts/Modes/Game3_PassTheChip.cs`

```csharp
public class Game3_PassTheChip : IGameMode
{
    public string ModeName => "PassTheChip";
    public string ModeLongName => "Pass the Chip";
    public int MaxPlayers => 4;
    
    public bool Use5InARow => false;
    public bool UseTripleDoublesPenalty => true;
    public bool Use5Plus6Safe => true;
    public bool RollingASixLosesTurn => false;
    public bool AllowBumping => false;  // KEY DIFFERENCE: No bumping
    
    // Win condition: All chips home
    public bool CheckWinCondition(Player player, BoardModel board)
    {
        return AllChipsHome(player, board);
    }
    
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer) { }
    public void OnDiceRoll(GameStateManager stateManager, Player player, int dice) { }
    public void OnChipPlacement(GameStateManager stateManager, Player player, BoardCell cell) 
    {
        // When placing chip, check if landing on occupied cell -> SWAP instead of bump
        // Swap: exchange positions with opponent chip
    }
    public void OnGameEnd(GameStateManager stateManager) { }
}
```

**Key Rules**:
- NO bumping (chips can't be removed)
- When landing on opponent chip: SWAP positions instead
- Triple doubles penalty active
- 5+6 safe roll
- Win: All chips home

**Target**: End of Day 4

---

#### Game 4: BumpUAnd5
**File**: `Assets/Scripts/Modes/Game4_BumpUAnd5.cs`

```csharp
public class Game4_BumpUAnd5 : IGameMode
{
    public string ModeName => "BumpUAnd5";
    public string ModeLongName => "Bump U and 5";
    public int MaxPlayers => 4;
    
    public bool Use5InARow => true;
    public bool UseTripleDoublesPenalty => true;
    public bool Use5Plus6Safe => true;
    public bool RollingASixLosesTurn => false;
    public bool AllowBumping => true;
    
    // Win condition: Get 5 in a row OR all chips home (whichever first)
    public bool CheckWinCondition(Player player, BoardModel board)
    {
        return HasFiveInARow(player, board) || AllChipsHome(player, board);
    }
    
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer) { }
    public void OnDiceRoll(GameStateManager stateManager, Player player, int dice) { }
    public void OnChipPlacement(GameStateManager stateManager, Player player, BoardCell cell) { }
    public void OnGameEnd(GameStateManager stateManager) { }
}
```

**Key Rules**:
- HYBRID: Win with either 5-in-a-row OR all chips home (whichever comes first)
- Full bumping allowed
- Triple doubles penalty
- 5+6 safe roll
- Strategic blend of speed + positioning

**Target**: End of Day 5

---

#### Game 5: Solitary
**File**: `Assets/Scripts/Modes/Game5_Solitary.cs`

```csharp
public class Game5_Solitary : IGameMode
{
    public string ModeName => "Solitary";
    public string ModeLongName => "Solitary";
    public int MaxPlayers => 1;  // SINGLE PLAYER
    
    public bool Use5InARow => true;
    public bool UseTripleDoublesPenalty => false;
    public bool Use5Plus6Safe => true;
    public bool RollingASixLosesTurn => false;
    public bool AllowBumping => false;  // No bumping against self
    
    // Win condition: Get 5 in a row (puzzle/challenge mode)
    public bool CheckWinCondition(Player player, BoardModel board)
    {
        return HasFiveInARow(player, board);
    }
    
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer) { }
    public void OnDiceRoll(GameStateManager stateManager, Player player, int dice) { }
    public void OnChipPlacement(GameStateManager stateManager, Player player, BoardCell cell) { }
    public void OnGameEnd(GameStateManager stateManager) { }
}
```

**Key Rules**:
- Single player only (puzzle/challenge mode)
- Goal: Get 5 in a row without time limit
- No opponents to bump against
- NO triple doubles penalty (single player)
- 5+6 safe roll
- Practice/learning mode

**Target**: End of Day 5

---

### PHASE 3: Unit Tests (Day 5-6)

**File**: `Assets/Scripts/Tests/GameModeTests.cs`

Create 40+ unit tests covering:

```csharp
[TestFixture]
public class GameModeTests
{
    // Test Game1_Bump5 (10 tests)
    [Test] public void Game1_WinCondition_FiveInARow_ReturnsTrue() { }
    [Test] public void Game1_RuleProperty_Use5InARow_IsTrue() { }
    [Test] public void Game1_RuleProperty_AllowBumping_IsTrue() { }
    [Test] public void Game1_RuleProperty_Use5Plus6Safe_IsTrue() { }
    [Test] public void Game1_RuleProperty_UseTripleDoublesPenalty_IsTrue() { }
    [Test] public void Game1_MaxPlayers_Returns4() { }
    [Test] public void Game1_ModeName_ReturnsBump5() { }
    [Test] public void Game1_WinCondition_LessThan5InARow_ReturnsFalse() { }
    [Test] public void Game1_WinCondition_DiagonalFive_ReturnsTrue() { }
    [Test] public void Game1_WinCondition_VerticalFive_ReturnsTrue() { }
    
    // Test Game2_Krazy6 (8 tests)
    [Test] public void Game2_WinCondition_AllChipsHome_ReturnsTrue() { }
    [Test] public void Game2_RuleProperty_Use5InARow_IsFalse() { }
    [Test] public void Game2_RuleProperty_UseTripleDoublesPenalty_IsFalse() { }
    [Test] public void Game2_RuleProperty_AllowBumping_IsTrue() { }
    [Test] public void Game2_MaxPlayers_Returns4() { }
    [Test] public void Game2_WinCondition_OneChipHome_ReturnsFalse() { }
    [Test] public void Game2_WinCondition_ThreeChipsHome_ReturnsFalse() { }
    [Test] public void Game2_ModeName_ReturnsKrazy6() { }
    
    // Test Game3_PassTheChip (8 tests)
    [Test] public void Game3_WinCondition_AllChipsHome_ReturnsTrue() { }
    [Test] public void Game3_RuleProperty_AllowBumping_IsFalse() { }
    [Test] public void Game3_RuleProperty_Use5Plus6Safe_IsTrue() { }
    [Test] public void Game3_OnChipPlacement_LandingOnOpponent_Swaps() { }
    [Test] public void Game3_MaxPlayers_Returns4() { }
    [Test] public void Game3_WinCondition_PartialHome_ReturnsFalse() { }
    [Test] public void Game3_ModeName_ReturnsPassTheChip() { }
    [Test] public void Game3_RuleProperty_UseTripleDoublesPenalty_IsTrue() { }
    
    // Test Game4_BumpUAnd5 (8 tests)
    [Test] public void Game4_WinCondition_FiveInARow_ReturnsTrue() { }
    [Test] public void Game4_WinCondition_AllChipsHome_ReturnsTrue() { }
    [Test] public void Game4_WinCondition_Either_ReturnsTrueForBoth() { }
    [Test] public void Game4_RuleProperty_AllowBumping_IsTrue() { }
    [Test] public void Game4_RuleProperty_Use5InARow_IsTrue() { }
    [Test] public void Game4_MaxPlayers_Returns4() { }
    [Test] public void Game4_ModeName_ReturnsBumpUAnd5() { }
    [Test] public void Game4_WinCondition_Neither_ReturnsFalse() { }
    
    // Test Game5_Solitary (6 tests)
    [Test] public void Game5_MaxPlayers_Returns1() { }
    [Test] public void Game5_WinCondition_FiveInARow_ReturnsTrue() { }
    [Test] public void Game5_RuleProperty_AllowBumping_IsFalse() { }
    [Test] public void Game5_RuleProperty_Use5InARow_IsTrue() { }
    [Test] public void Game5_ModeName_ReturnsSolitary() { }
    [Test] public void Game5_WinCondition_LessThanFive_ReturnsFalse() { }
}
```

**Target Coverage**: 80%+ of all game mode code

**Target**: End of Day 6

---

### PHASE 4: Code Review & Sign-Off (Day 7)

- [ ] All files committed to git
- [ ] All 40+ tests passing (100% pass rate)
- [ ] 80%+ test coverage achieved
- [ ] All XML documentation complete
- [ ] CODING_STANDARDS.md compliance verified
- [ ] Code review submission to Managing Engineer

**Submission checklist**:
```
Files ready for review:
- Assets/Scripts/Modes/IGameMode.cs
- Assets/Scripts/Modes/Game1_Bump5.cs
- Assets/Scripts/Modes/Game2_Krazy6.cs
- Assets/Scripts/Modes/Game3_PassTheChip.cs
- Assets/Scripts/Modes/Game4_BumpUAnd5.cs
- Assets/Scripts/Modes/Game5_Solitary.cs
- Assets/Scripts/Tests/GameModeTests.cs

Tests: 40+ passing, 100% pass rate
Coverage: 80%+
Documentation: 100%
Standards: 100% compliant
```

**Target**: Nov 21, 2025

---

## DAILY PROGRESS REPORTING

**Every day at 9 AM UTC standup report**:
- ✅ Completed since yesterday
- 🔄 In progress today
- 🚫 Blockers (if any)
- 📈 % complete

Example Day 1 report:
> ✅ Completed: None (kickoff day)  
> 🔄 In Progress: IGameMode interface design  
> 🚫 Blockers: None  
> 📈 Progress: 5%

---

## STANDARDS & QUALITY GATES

✅ **Code Standards**: Follow CODING_STANDARDS.md
- All public methods documented with /// comments
- Consistent naming (PascalCase classes, camelCase methods/variables)
- Proper error handling
- No warnings

✅ **Test Standards**: 
- 40+ tests minimum
- 100% pass rate required
- 80%+ code coverage minimum
- Each test has descriptive name (Arrange-Act-Assert pattern)

✅ **Documentation**:
- Every public method has XML docs
- Every class has summary
- Every property has summary
- Win conditions documented in comments

---

## REFERENCE MATERIALS

**Full specifications**: SPRINT_3_DETAILED_BRIEFING.md (800+ lines)
- Detailed game mode rules (lines 250-600+)
- Win condition logic (lines 600-700+)
- Integration points (lines 700-800+)
- Testing strategy (lines 800+)

**Architecture reference**: SPRINT_2_FINAL_SIGNOFF.md
- GameStateManager.cs approved implementation
- Code quality standards that we exceeded
- Test patterns to follow

**Coding standards**: CODING_STANDARDS.md
- Must follow for all code
- Pre-code-review checklist

---

## SUCCESS CRITERIA

✅ **All 5 game modes fully implemented**
✅ **40+ unit tests, 100% passing**
✅ **80%+ test coverage**
✅ **100% code documentation**
✅ **CODING_STANDARDS.md compliance**
✅ **Code review approved by Managing Engineer**
✅ **Zero critical bugs**

---

## NEXT STEPS AFTER COMPLETION

Upon approval (Nov 21):
- Board Team triggers Sprint 4 kickoff
- UI Team can finalize design based on confirmed game modes
- Project accelerates through Sprints 4-8

---

## MANAGING ENGINEER SUPPORT

**You are fully authorized to proceed. I am here for**:
- Clarifications on game mode rules
- Code review (< 4 hours after commit)
- Blocker resolution (< 1 hour)
- Questions on architecture

**Contact**: Direct message for urgent, #gameplay for general updates

---

## AUTHORITY & PRIORITY

🚀 **PRIORITY**: CRITICAL PATH  
✅ **AUTHORITY**: FULL EXECUTION  
📋 **DEADLINE**: Nov 21, 2025  
🎯 **IMPACT**: Unblocks 4 downstream teams

**You have everything needed. Begin immediately. Report daily. Deliver on time.**

---

**From**: Amp (Managing Engineer)  
**Status**: ACTIVE EXECUTION  
**Date**: Nov 14, 2025

---

*End of Dispatch*
