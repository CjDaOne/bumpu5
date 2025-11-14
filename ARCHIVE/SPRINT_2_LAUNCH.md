# Sprint 2 Launch - Team Briefing
**Date**: Nov 14, 2025 (Evening)  
**Kickoff Date**: Nov 21, 2025  
**Sprint Duration**: 7 days (Nov 21-28)  
**Managing Engineer**: Active oversight  
**Status**: 🚀 READY TO LAUNCH

---

## Sprint Overview

Sprint 2 builds the "nervous system" of the game—a **state machine** that orchestrates the entire turn flow from start to finish. This sprint delivers 5 new core classes and 22+ unit tests that enable complete game-flow playability.

### Key Success Metric
**By Nov 28, 2025, a complete game can be played end-to-end via code**: Setup → Roll → Move → Bump → Win

---

## Team Assignments

### Team 1: Gameplay Engineering (Primary)
**Lead**: Gameplay Engineer Agent  
**Dependencies**: Sprint 1 (approved ✅)  
**Duration**: 7 days

**Deliverables**:
1. GamePhase.cs (20 lines) - Enum with 9 phases
2. GameState.cs (60 lines) - Game state snapshot
3. GameStateManager.cs (300+ lines) - Core orchestrator ⭐
4. TurnPhaseController.cs (150+ lines) - Phase transitions
5. TurnManager.cs (Enhanced) - Add 80 lines to existing class

**Test Requirements** (22+ tests):
- GameStateManagerTests.cs (10+ tests)
- TurnPhaseControllerTests.cs (7+ tests)
- TurnManagerEnhancedTests.cs (5+ tests)

**Definition of Done**:
- ✅ All 5 classes created with full documentation
- ✅ All 22+ unit tests passing
- ✅ CODING_STANDARDS.md compliance verified
- ✅ No circular dependencies
- ✅ Integration with Sprint 1 verified
- ✅ Code review approval (Managing Engineer)

---

## Critical Path Dependencies

```
Sprint 1 (COMPLETE) ✅
        ↓
Sprint 2 (Gameplay) ← YOU ARE HERE
        ↓
Sprint 3 (Game Modes - depends on Sprint 2)
        ↓
Sprint 4 (Board System - depends on Sprint 3)
        ↓
Sprint 5 (UI/HUD - depends on Sprint 4)
```

**BLOCKER STATUS**: None identified. Sprint 1 complete and approved.

---

## Gameplay Team - Detailed Briefing

### Class 1: GamePhase.cs (Enum)
**Purpose**: Define all 9 possible game phases  
**Estimated LOC**: 20 lines  
**Complexity**: Low

```csharp
public enum GamePhase
{
    Setup,           // Game initialized
    Rolling,         // Player rolling dice
    DiceResult,      // Dice shown, result displayed
    Placing,         // Player moving chip
    BumpDecision,    // After move: can bump?
    Bumping,         // Opponent chip being removed
    EndTurn,         // Turn wrapping up
    WinScreen,       // Player won
    GameOver         // Game complete
}
```

**Requirements**:
- [ ] Exactly 9 phases in semantic order
- [ ] Each phase has 1-2 line comment
- [ ] No additional logic (it's an enum)

**Testing**: None (enums don't need unit tests, but will be used in other tests)

---

### Class 2: GameState.cs (Data Structure)
**Purpose**: Snapshot of complete game state at any moment  
**Estimated LOC**: 60 lines  
**Complexity**: Low

**Required Properties**:
- Game identification: `GameModeID`, `GameModeName`
- Players & board: `Player[] Players`, `BoardModel Board`
- Current state: `GamePhase CurrentPhase`, `int CurrentPlayerIndex`, `int[] LastDiceRoll`
- Turn tracking: `int TurnNumber`, `bool CanRollAgain`, `bool MustBump`
- Pending actions: `int? PendingBumpCell`, `Chip LastMovedChip`
- Serialization: `Serialize()`, `Deserialize(json)` (stubs OK for now)

**Requirements**:
- [ ] All properties documented with `/// <summary>`
- [ ] Default constructor initializes to valid state
- [ ] `Reset()` method clears turn-specific data
- [ ] Serialize/Deserialize stubs (not required to work yet)

**Testing** (3 tests):
- `GameState_Creation_InitializesCorrectly()`
- `GameState_Reset_ClearsTurnData()`
- `GameState_Serialize_ReturnsJSON()`

---

### Class 3: GameStateManager.cs (Core Orchestrator) ⭐
**Purpose**: Control entire game flow, validate actions, fire events  
**Estimated LOC**: 300+ lines  
**Complexity**: High - This is the critical piece

**Core Methods**:

#### 1. Initialization
```csharp
public void InitializeGame(IGameMode gameMode, Player[] players)
{
    // 1. Create GameState
    // 2. Setup TurnManager with players
    // 3. Create BoardModel with players
    // 4. Store game mode reference
    // 5. Transition to Setup phase
}
```

#### 2. Action Methods (Validate → Execute → Fire Events → Transition)
```csharp
public void RollDice()
{
    // Validate: must be in Rolling phase
    // Execute: roll dice via diceManager
    // Fire: OnDiceRolled event
    // Transition: to DiceResult phase
}

public void MoveChip(int fromCell, int toCell)
{
    // Validate: in Placing phase, player owns chip, move is legal
    // Execute: move chip via boardModel
    // Fire: OnChipMoved event
    // Check: win condition
    // Transition: to BumpDecision or WinScreen
}

public void DeclareBump(int targetCell)
{
    // Validate: bump possible, have just moved
    // Execute: remove opponent chip, award points
    // Fire: OnBumpTriggered event
    // Transition: to EndTurn
}

public void EndTurn()
{
    // Validate: in EndTurn phase
    // Check: can roll again (doubles, mode rules)
    // Transition: to Rolling (same or next player)
    // Fire: OnPlayerChanged event
}

public void DeclareWin()
{
    // Validate: player has winning condition
    // Execute: award points
    // Fire: OnGameWon event
    // Transition: to GameOver
}
```

#### 3. Query Methods
```csharp
public GamePhase GetCurrentPhase() { }
public Player GetCurrentPlayer() { }
public bool IsValidMove(int fromCell, int toCell) { }
public bool CanBumpTarget(int targetCell) { }
public bool HasWon(Player player) { }
public List<int> GetValidMoves(int fromCell) { }
```

#### 4. Helper Methods
```csharp
private void ValidatePhaseForAction(GamePhase requiredPhase)
{
    if (gameState.CurrentPhase != requiredPhase)
        OnInvalidActionAttempted?.Invoke($"Wrong phase");
}

private void TransitionToPhase(GamePhase nextPhase)
{
    gameState.CurrentPhase = nextPhase;
    OnPhaseChanged?.Invoke(nextPhase);
}

private void CheckWinCondition()
{
    if (currentGameMode.CheckWin(GetCurrentPlayer(), board))
    {
        TransitionToPhase(GamePhase.WinScreen);
    }
}
```

**Events** (Must be declared and documented):
```csharp
public event System.Action<GamePhase> OnPhaseChanged;
public event System.Action<Player> OnPlayerChanged;
public event System.Action<int[]> OnDiceRolled;
public event System.Action<int, int> OnChipMoved;  // fromCell, toCell
public event System.Action<Player> OnGameWon;
public event System.Action<string> OnInvalidActionAttempted;
```

**Key Design Principles**:
1. **Validate First**: Every action validates before executing
2. **Fire Events**: Events allow UI to react without coupling
3. **Delegate to Game Modes**: Use `IGameMode.CheckWin()`, not hardcoded logic
4. **Centralize Transitions**: Always use `TransitionToPhase()` method

**Requirements**:
- [ ] All public methods fully documented with `/// <summary>`
- [ ] All phase transitions use `ValidatePhaseForAction()` first
- [ ] All invalid actions call `OnInvalidActionAttempted(reason)`
- [ ] Events fire BEFORE state change (so UI sees transition)
- [ ] No direct board/turn manipulation; use class methods
- [ ] IGameMode called for win condition checks

**Testing** (10+ tests):
- `InitializeGame_SetsupPlayers_ReturnsTrue()`
- `RollDice_TransitionsToPlacingPhase()`
- `MoveChip_WithValidMove_TransitionsCorrectly()`
- `MoveChip_WithInvalidMove_FiresErrorEvent()`
- `DeclareBump_RemovesChip_AwardsPoints()`
- `DeclareBump_WithInvalidTarget_Fails()`
- `EndTurn_RotatesToNextPlayer()`
- `EndTurn_WithDouble_AllowsRollAgain()`
- `HasWon_ChecksGameModeWinCondition()`
- `InvalidAction_InWrongPhase_FiresErrorEvent()`

---

### Class 4: TurnPhaseController.cs (Phase Coordinator)
**Purpose**: Manage phase-specific logic, lighter than GameStateManager  
**Estimated LOC**: 150 lines  
**Complexity**: Medium

**Methods**:
```csharp
// Rolling phase
public void StartRollingPhase(Player currentPlayer) { }
public int[] CompleteRoll(int[] diceRoll) { }

// Placing phase
public void StartPlacingPhase() { }
public bool ExecuteMove(int fromCell, int toCell) { }
public void CompleteMove() { }

// Bump phase
public void StartBumpDecisionPhase() { }
public bool AttemptBump(int targetCell) { }
public void SkipBump() { }

// Turn end
public void CompleteTurn() { }
public Player GetNextPlayer() { }
```

**Key Design**: Delegates to `GameStateManager` (no duplication)

**Requirements**:
- [ ] All methods documented
- [ ] Methods delegate to GameStateManager (don't duplicate logic)
- [ ] Clear naming: `StartXPhase`, `CompleteX`, `AttemptX`
- [ ] Return types indicate success/failure

**Testing** (7+ tests):
- `StartRollingPhase_SetsCurrentPhase()`
- `CompleteRoll_TransitionsToPlacing()`
- `ExecuteMove_WithValidMove_ReturnsTrue()`
- `ExecuteMove_WithInvalidMove_ReturnsFalse()`
- `StartBumpDecisionPhase_HighlightsBumpableCells()`
- `AttemptBump_RemovesChip()`
- `CompleteTurn_RotatesPlayer()`

---

### Class 5: TurnManager.cs (Enhancement)
**Purpose**: Add turn-specific tracking to existing Sprint 1 class  
**Estimated LOC**: 80 new lines (add to existing class)  
**Complexity**: Low

**New Fields**:
```csharp
private int turnsCompleted;
private bool canRollAgain;
private bool justBumped;
private int consecutiveDoublesCount;
```

**New Properties**:
```csharp
public int TurnsCompleted => turnsCompleted;
public bool CanRollAgain { get; set; }
public bool JustBumped { get; set; }
public int ConsecutiveDoublesCount => consecutiveDoublesCount;
```

**New Methods**:
```csharp
public void StartNewTurn()
{
    canRollAgain = false;
    justBumped = false;
}

public void CompleteTurn()
{
    turnsCompleted++;
    AdvanceTurn();  // Existing method
    StartNewTurn();
}

public void RecordDouble()
{
    consecutiveDoublesCount++;
    if (consecutiveDoublesCount >= 3)
    {
        // Handle triple-double rule (lose turn)
    }
}

public void ResetDoubleCount()
{
    consecutiveDoublesCount = 0;
}

public void RecordBump()
{
    justBumped = true;
}
```

**Requirements**:
- [ ] All new properties documented
- [ ] Backward compatible with Sprint 1 code (no breaking changes)
- [ ] `CompleteTurn()` increments counter AND advances player
- [ ] `RecordDouble()` handles triple-double logic

**Testing** (5+ tests):
- `StartNewTurn_ResetsState()`
- `CompleteTurn_IncrementsTurnCounter()`
- `RecordDouble_IncreasesDoubleCount()`
- `RecordDouble_TripleDouble_LosesTurn()`
- `ResetDoubleCount_ClearsCount()`

---

## Edge Cases You MUST Handle

### ✅ Doubles Logic
- Rolling any matching pair → player gets another roll
- Need to track consecutive doubles count
- **3 doubles in a row** → Lose turn (standard board game rule)
- 5+6 "safe" combination is NOT a double (mode-specific handling)

### ✅ Bumping Rules
- Can only bump **opponent's** chips (not own)
- Can only bump if chip is on **adjacent** cell
- Must have **just moved** a chip to bump
- Cannot bump chips that are off-board

### ✅ Win Conditions
- Most modes: 5 in a row
- Some modes (Game 4): Score threshold
- Single-player mode (Game 5): Special bonuses
- **Must be checked AFTER every move and bump**
- **Must use `IGameMode.CheckWin()`, not hardcoded logic**

### ✅ Invalid Actions
- Rolling when not in Rolling phase → `OnInvalidActionAttempted`
- Moving from empty cell → Error with message
- Moving to non-adjacent cell → Error with message
- Bumping own chips → Error with message
- Bumping when no bump possible → Error with message
- Declaring win without meeting condition → Error with message

---

## Integration with Sprint 1

**Sprint 1 classes remain UNCHANGED**. Sprint 2 uses them:

| Sprint 1 Class | How Sprint 2 Uses It |
|---|---|
| Player | Query current player, add score, check chip ownership |
| Chip | Move chips, verify ownership, track last moved chip |
| BoardCell | Check cell state, validate placement |
| BoardModel | Execute moves, execute bumps, check win |
| DiceManager | Roll dice when requested |
| TurnManager | Rotate players, track turn count, manage doubles |

**No modifications to Sprint 1 code. Fully backward compatible.**

---

## Testing Strategy

### File Organization
```
Assets/Scripts/Tests/
├── GameStateManagerTests.cs      (NEW - 10+ tests)
├── TurnPhaseControllerTests.cs   (NEW - 7+ tests)
├── TurnManagerEnhancedTests.cs   (NEW - 5+ tests)
└── [existing Sprint 1 tests]     (UNCHANGED)
```

### Running Tests
**In Unity Editor**: Window → TextTest Runner → Run All Tests

**Expected**: 22+ new tests + 57 Sprint 1 tests = 79 total tests, 100% pass rate

### Test Execution Timeline
- **Day 1-4**: Implement classes while writing tests
- **Day 5-6**: Execute tests, fix failures
- **Day 7**: Final integration test and code review

---

## Code Standards Checklist

Before submitting for review:
- [ ] All classes follow PascalCase naming
- [ ] All public methods have `/// <summary>` documentation
- [ ] All parameters and return types documented with `/// <param>` and `/// <returns>`
- [ ] No magic numbers (use `GamePhase` enum, constants)
- [ ] No circular dependencies
- [ ] All events properly declared and fire at correct time
- [ ] Integration with Sprint 1 verified (run all 79 tests)
- [ ] No console errors or warnings
- [ ] Code follows CODING_STANDARDS.md exactly

---

## Performance Targets

- **State transitions**: < 1ms
- **Turn validation**: < 5ms
- **Complete turn flow**: < 50ms
- **Memory**: GameState < 1KB

All targets should be easily achievable with clean code architecture.

---

## Daily Standup Format

Each day, report:
- ✅ Completed since last standup
- 🔄 In progress
- 🚫 Blockers (if any)

**Example**:
```
✅ GamePhase.cs created & tested
✅ GameState.cs created with all properties
🔄 Working on GameStateManager (currently at 150 lines)
🚫 None
```

---

## Key Dates

| Date | Event |
|------|-------|
| Nov 21 (Thu) | Sprint 2 Kickoff, review requirements |
| Nov 22-24 | Implementation phase (classes) |
| Nov 25-26 | Testing phase (execute tests, fix bugs) |
| Nov 27 | Final integration & edge case review |
| Nov 28 (Thu) | Sprint 2 complete, ready for code review |

---

## Code Review Criteria

Managing Engineer will verify:
1. ✅ All 5 classes created with correct structure
2. ✅ All 22+ tests passing
3. ✅ Code follows CODING_STANDARDS.md
4. ✅ No circular dependencies or tight coupling
5. ✅ Integration with Sprint 1 verified
6. ✅ Events properly implemented
7. ✅ Edge cases handled (doubles, bumps, win)
8. ✅ Performance acceptable
9. ✅ Documentation complete

**Expected Outcome**: APPROVED ✅ (or specific feedback for revision)

---

## Success Criteria for Sprint 2 Complete

- ✅ GamePhase.cs created (9 phases)
- ✅ GameState.cs created (all properties, properly initialized)
- ✅ GameStateManager.cs created (300+ lines, orchestrates turn flow)
- ✅ TurnPhaseController.cs created (delegates to GameStateManager)
- ✅ TurnManager.cs enhanced (add 80 lines, backward compatible)
- ✅ All 22+ unit tests created and passing
- ✅ All 57 Sprint 1 tests still passing (no regressions)
- ✅ No console errors or warnings
- ✅ Code reviewed and approved by Managing Engineer
- ✅ Complete game playable from code: Setup → Roll → Move → Bump → Win

---

## What's Next: Sprint 3 Preview

Once Sprint 2 is approved, Sprint 3 will implement the 5 game modes:

1. Game1_Bump5 (basic 5-in-a-row)
2. Game2_Krazy6 (anything goes with 6)
3. Game3_PassTheChip (team play)
4. Game4_BumpUAnd5 (score-based win)
5. Game5_Solitary (single-player with bonuses)

Each mode implements `IGameMode` interface and defines:
- Custom scoring rules
- Custom win conditions
- Custom bump mechanics
- Roll-again rules specific to that mode

GameStateManager will call IGameMode methods to delegate mode-specific behavior.

---

## Questions & Blockers

If you encounter issues:
1. **Document** the blocker clearly
2. **Note** the specific edge case or error
3. **Escalate** to Managing Engineer with detailed context
4. **Continue** with other work while waiting for resolution

**Managing Engineer** is actively monitoring and available for:
- Architecture clarifications
- Design questions
- Dependency resolution
- Code review feedback

---

## Summary

**What**: Build state machine that orchestrates complete game turn flow  
**How**: 5 classes (GamePhase, GameState, GameStateManager, TurnPhaseController, TurnManager+)  
**Tests**: 22+ unit tests across 3 test files  
**Duration**: 7 days (Nov 21-28)  
**Quality**: CODING_STANDARDS.md compliant, 100% test pass rate, code review approved  
**Success**: Complete game playable from code by Nov 28

---

**Document Created**: Nov 14, 2025  
**Status**: Ready for Nov 21 Kickoff  
**Managing Engineer**: Active oversight  
**Teams**: Gameplay Engineering (Primary), All other teams on standby

**Let's build the nervous system of Bump U!**
