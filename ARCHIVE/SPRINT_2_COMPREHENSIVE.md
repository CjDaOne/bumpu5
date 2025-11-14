# Sprint 2: Turn Manager & Game State Machine
**Comprehensive Briefing Package**

**Sprint Duration**: Week 2 (Nov 21 - Nov 28, 2025)  
**Lead**: Gameplay Engineer Agent  
**Managing Engineer**: Amp (active oversight)  
**Status**: 🚀 READY TO LAUNCH

---

## Executive Summary

Sprint 2 builds the "nervous system" of the game—a **state machine** that orchestrates complete turn flow from start to finish. By Nov 28, a complete game will be playable end-to-end in code: Setup → Roll → Move → Bump → Win.

**Deliverables**: 5 new classes + 22+ unit tests  
**Estimated LOC**: ~1,200 lines  
**Success Criteria**: All tests passing, GameStateManager orchestrates turn flow, zero logic bugs

---

## What We're Building

### Core Concept

A **GameStateManager** that acts as the "conductor" of the entire game. It:
- Maintains complete game state at every moment
- Orchestrates turn flow through distinct phases
- Fires events that UI systems will listen to (Sprints 4-5)
- Validates all player actions before executing them
- Delegates to game modes for custom rules (Sprint 3)

### State Machine Flow

```
Setup → Rolling → DiceResult → Placing → BumpDecision
                                             ├→ WinScreen → GameOver
                                             └→ Bumping → EndTurn → Rolling (next player)
```

---

## Deliverables (In Order)

### 1. GamePhase.cs (Enum, 20 lines)

**Purpose**: Define all 9 possible game phases

```csharp
public enum GamePhase
{
    Setup,           // Game initialized, waiting for first roll
    Rolling,         // Player can roll dice
    DiceResult,      // Dice rolled, showing result
    Placing,         // Player moving chip (can move multiple times)
    BumpDecision,    // After move: is there a bump option?
    Bumping,         // Opponent chip being removed
    EndTurn,         // Turn wrapping up (check 6, doubles, etc)
    WinScreen,       // Player won
    GameOver         // Game ended
}
```

**Requirements**:
- ✅ Exactly 9 phases in order
- ✅ Clear semantic meaning
- ✅ Comments explaining each phase

**Testing**: None (enum used in other tests)

---

### 2. GameState.cs (Data Structure, 60 lines)

**Purpose**: Snapshot of the complete game at any point

```csharp
public class GameState
{
    // Game identification
    public int GameModeID { get; set; }
    public string GameModeName { get; set; }
    
    // Players & board
    public Player[] Players { get; set; }
    public BoardModel Board { get; set; }
    
    // Current game state
    public GamePhase CurrentPhase { get; set; }
    public int CurrentPlayerIndex { get; set; }
    public int[] LastDiceRoll { get; set; }
    
    // Turn tracking
    public int TurnNumber { get; set; }
    public bool CanRollAgain { get; set; }
    public bool MustBump { get; set; }
    
    // Pending actions
    public int? PendingBumpCell { get; set; }
    public Chip LastMovedChip { get; set; }
    
    // Methods
    public GameState() { }
    public void Reset() { }
    public string Serialize() { return "{}"; }
    public static GameState Deserialize(string json) { return new GameState(); }
}
```

**Requirements**:
- ✅ All properties documented with `/// <summary>`
- ✅ Nullable types for optional values (`int?` for PendingBumpCell)
- ✅ Default constructor initializes to valid state
- ✅ Reset() clears turn-specific data
- ✅ Serialize/Deserialize stubs OK for now

**Testing** (3+ tests):
- `GameState_Creation_InitializesCorrectly()`
- `GameState_Reset_ClearsTurnData()`
- `GameState_Serialize_ReturnsJSON()`

---

### 3. GameStateManager.cs (Orchestrator, 300+ lines) ⭐

**Purpose**: Control entire game flow, validate actions, fire events

#### Public API

```csharp
public class GameStateManager : MonoBehaviour
{
    private GameState gameState;
    private TurnManager turnManager;
    private DiceManager diceManager;
    private BoardModel board;
    private IGameMode currentGameMode;
    
    // Events (for UI/Board to listen)
    public event System.Action<GamePhase> OnPhaseChanged;
    public event System.Action<Player> OnPlayerChanged;
    public event System.Action<int[]> OnDiceRolled;
    public event System.Action<int, int> OnChipMoved;
    public event System.Action<Player> OnGameWon;
    public event System.Action<string> OnInvalidActionAttempted;
    
    // Initialization
    public void InitializeGame(IGameMode gameMode, Player[] players)
    {
        // 1. Create GameState
        // 2. Set up TurnManager
        // 3. Create BoardModel
        // 4. Store game mode
        // 5. Fire OnPhaseChanged(GamePhase.Setup)
    }
    
    // Turn flow actions
    public void RollDice()
    {
        // Validate: must be in Rolling phase
        // Roll dice via diceManager
        // Store result in gameState
        // Fire OnDiceRolled event
        // Transition to DiceResult phase
    }
    
    public void MoveChip(int fromCell, int toCell)
    {
        // Validate: in Placing phase, owns chip, legal move
        // Execute move via boardModel
        // Update lastMovedChip
        // Fire OnChipMoved event
        // Check for win → WinScreen or BumpDecision
    }
    
    public void DeclareBump(int targetCell)
    {
        // Validate: bump possible, just moved
        // Execute bump via boardModel
        // Award points
        // Transition to Bumping
        // Check for win again
        // Transition to EndTurn
    }
    
    public void EndTurn()
    {
        // Validate: in EndTurn phase
        // Check for roll-again conditions
        // If can roll → Rolling phase
        // Else → advance player, Rolling phase
        // Fire OnPlayerChanged event
    }
    
    public void DeclareWin()
    {
        // Validate: player has winning condition
        // Award points
        // Transition to WinScreen then GameOver
        // Fire OnGameWon event
    }
    
    // State queries
    public GamePhase GetCurrentPhase() => gameState.CurrentPhase;
    public Player GetCurrentPlayer() => turnManager.CurrentPlayer;
    public bool IsValidMove(int fromCell, int toCell) { }
    public bool CanBumpTarget(int targetCell) { }
    public bool HasWon(Player player) { }
    public List<int> GetValidMoves(int fromCell) { }
    
    // Internal
    private void ValidatePhaseForAction(GamePhase requiredPhase) { }
    private void TransitionToPhase(GamePhase nextPhase) { }
    private void CheckWinCondition() { }
}
```

**Key Design Principles**:
1. **Validate First**: Every action validates before executing
2. **Fire Events**: Events allow UI to react without coupling
3. **Delegate to Game Modes**: Use `IGameMode.CheckWin()`, not hardcoded logic
4. **Centralize Transitions**: Always use `TransitionToPhase()` method

**Requirements**:
- ✅ All public methods fully documented
- ✅ All phase transitions use `ValidatePhaseForAction()`
- ✅ All invalid actions call `OnInvalidActionAttempted(reason)`
- ✅ Events fire BEFORE state change
- ✅ No direct board/turn manipulation
- ✅ IGameMode called for win condition checks

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

### 4. TurnPhaseController.cs (Phase Coordinator, 150 lines)

**Purpose**: Manage phase-specific logic (delegates to GameStateManager)

```csharp
public class TurnPhaseController
{
    private GameStateManager gameStateManager;
    
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
}
```

**Key Design**: Delegates to `GameStateManager` (no duplication)

**Requirements**:
- ✅ All methods documented
- ✅ Methods delegate to GameStateManager
- ✅ Clear naming: `StartXPhase`, `CompleteX`, `AttemptX`
- ✅ Return types indicate success/failure

**Testing** (7+ tests):
- `StartRollingPhase_SetsCurrentPhase()`
- `CompleteRoll_TransitionsToPlacing()`
- `ExecuteMove_WithValidMove_ReturnsTrue()`
- `ExecuteMove_WithInvalidMove_ReturnsFalse()`
- `StartBumpDecisionPhase_HighlightsBumpableCells()`
- `AttemptBump_RemovesChip()`
- `CompleteTurn_RotatesPlayer()`

---

### 5. TurnManager.cs (Enhancement, +80 lines)

**Purpose**: Add turn-specific tracking to existing Sprint 1 class

Add these to the existing TurnManager:

```csharp
public class TurnManager
{
    // ... existing code ...
    
    // NEW - Turn state tracking
    private int turnsCompleted;
    private bool canRollAgain;
    private bool justBumped;
    private int consecutiveDoublesCount;
    
    // NEW - Properties
    public int TurnsCompleted => turnsCompleted;
    public bool CanRollAgain { get; set; }
    public bool JustBumped { get; set; }
    public int ConsecutiveDoublesCount => consecutiveDoublesCount;
    
    // NEW - Turn lifecycle
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
}
```

**Requirements**:
- ✅ All new properties documented
- ✅ StartNewTurn() resets state, doesn't change player
- ✅ CompleteTurn() increments counter AND advances player
- ✅ RecordDouble() handles triple-double check
- ✅ Backward compatible with Sprint 1 code

**Testing** (5+ tests):
- `StartNewTurn_ResetsState()`
- `CompleteTurn_IncrementsTurnCounter()`
- `RecordDouble_IncreasesDoubleCount()`
- `RecordDouble_TripleDouble_LosesTurn()`
- `ResetDoubleCount_ClearsCount()`

---

## Edge Cases to Handle

### ✅ Doubles Logic
- Rolling any matching pair → player gets another roll
- Track consecutive doubles count
- 3 doubles in a row → Lose turn (standard board game rule)
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
- Must be checked AFTER every move and bump
- Must use `IGameMode.CheckWin()`, not hardcoded logic

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

## File Structure

```
/Assets/Scripts/Core/
  GamePhase.cs                    (NEW - enum)
  GameState.cs                    (NEW - state snapshot)
  GameStateManager.cs             (NEW - orchestrator)
  TurnPhaseController.cs          (NEW - phase transitions)
  TurnManager.cs                  (UPDATED - add turn state)

/Assets/Scripts/Tests/
  GameStateManagerTests.cs        (NEW - 10+ tests)
  GamePhaseControllerTests.cs     (NEW - 7+ tests)
  TurnManagerEnhancedTests.cs     (NEW - 5+ tests)
```

---

## Testing Strategy

### Test Files to Create

1. **GameStateManagerTests.cs** (10+ tests)
   - Initialization, turn flow, phase transitions
   - All public methods
   - Error conditions

2. **TurnPhaseControllerTests.cs** (7+ tests)
   - Phase-specific logic
   - Delegation to GameStateManager
   - Valid/invalid transitions

3. **TurnManagerEnhancedTests.cs** (5+ tests)
   - New turn tracking features
   - Double counting
   - Backward compatibility with Sprint 1

### Test Execution

Run via: **Window > TextTest Runner** in Unity Editor

Expected: 22+ new tests + 57 Sprint 1 tests = 79 total tests, 100% pass rate

---

## Code Standards Checklist

Before submitting for review:

- [ ] All classes follow PascalCase naming
- [ ] All public methods have `/// <summary>` documentation
- [ ] All parameters and return types documented
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

## Success Criteria

Before marking Sprint 2 complete:

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

**Prepared By**: Managing Engineer (Amp)  
**Date**: Nov 14, 2025  
**Status**: 🚀 Ready for Nov 21 Kickoff  
**Managing Engineer**: Active oversight
