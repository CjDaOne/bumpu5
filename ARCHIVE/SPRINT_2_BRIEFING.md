# Sprint 2 Briefing Document
## Turn Manager & Game State Machine
**For**: Gameplay Engineer Agent  
**From**: Managing Engineer Agent  
**Date**: Nov 14, 2025  
**Kickoff**: Nov 21, 2025 (1 week)

---

## Executive Summary

Sprint 2 builds the "nervous system" of the game—a state machine that orchestrates complete turn flow and game state. You'll connect all Sprint 1 logic into a cohesive, playable system where a complete game can be executed in code.

**Deliverables**: 5 new classes + 3 test files (22+ tests)  
**LOC Target**: ~1,200 lines  
**Duration**: 7 days (Nov 21-28)

---

## What You're Building

### Core Concept
A **GameStateManager** that acts as the "conductor" of the entire game. It:
- Maintains complete game state at every moment
- Orchestrates turn flow through distinct phases
- Fires events that UI systems will listen to (Sprint 4/5)
- Validates all player actions before executing them
- Delegates to game modes for custom rules (Sprint 3)

### Key Classes

1. **GamePhase.cs** (Enum) - 9 phases from Setup through GameOver
2. **GameState.cs** - Snapshot of complete game state
3. **GameStateManager.cs** - The orchestrator (300+ lines)
4. **TurnPhaseController.cs** - Manages phase transitions
5. **TurnManager.cs** (Enhanced) - Add turn-specific tracking

### State Machine Flow

```
Setup → Rolling → DiceResult → Placing → BumpDecision
                                            ├→ WinScreen → GameOver
                                            └→ Bumping → EndTurn → Rolling (next player)
```

---

## Requirements Breakdown

### 1. GamePhase.cs

**Purpose**: Define all possible game states.

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
- ✅ Clear semantic meaning (not generic "State1/State2")
- ✅ Comments explaining each phase

**Testing**: None (it's an enum), but will be used in tests.

---

### 2. GameState.cs

**Purpose**: Snapshot of the complete game at any point.

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
    public bool CanRollAgain { get; set; }       // Doubles or mode rule
    public bool MustBump { get; set; }           // Mode-specific rule
    
    // Pending actions
    public int? PendingBumpCell { get; set; }    // Cell to bump (null if none)
    public Chip LastMovedChip { get; set; }
    
    // Initialization
    public GameState() { }
    public void Reset() { }
    
    // Serialization (for save/load - future sprint)
    public string Serialize() { return "{}"; }
    public static GameState Deserialize(string json) { return new GameState(); }
}
```

**Requirements**:
- ✅ All public properties documented
- ✅ Optional values use nullable types (`int?` for PendingBumpCell)
- ✅ Default constructor initializes to valid state
- ✅ Reset() method clears turn-specific data without touching players/board
- ✅ Serialize/Deserialize stubs (not required to work yet)

**Testing**:
- `GameStateTests_Creation_InitializesCorrectly()`
- `GameStateTests_Reset_ClearsTurnData()`
- `GameStateTests_Serialize_ReturnsJSON()`

---

### 3. GameStateManager.cs (Core)

**Purpose**: Orchestrate the entire game flow and validate actions.

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
    public event System.Action<int, int> OnChipMoved;  // fromCell, toCell
    public event System.Action<Player> OnGameWon;
    public event System.Action<string> OnInvalidActionAttempted;
    
    // Initialization
    public void InitializeGame(IGameMode gameMode, Player[] players)
    {
        // 1. Create GameState
        // 2. Set up TurnManager with players
        // 3. Create BoardModel with players
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
        // Validate: must be in Placing phase
        // Validate: move is adjacent
        // Validate: player owns chip at fromCell
        // Execute move via boardModel
        // Update lastMovedChip
        // Fire OnChipMoved event
        // Check for win condition → transition to WinScreen or BumpDecision
    }
    
    public void DeclareBump(int targetCell)
    {
        // Validate: bump is possible at targetCell
        // Validate: must have just moved a chip
        // Execute bump via boardModel
        // Award points
        // Transition to Bumping phase
        // Check for win again
        // Transition to EndTurn
    }
    
    public void EndTurn()
    {
        // Validate: must be in EndTurn phase
        // Check for roll-again conditions (doubles, mode-specific)
        // If can roll again → return to Rolling phase
        // Else → advance player, return to Rolling
        // Fire OnPlayerChanged event
    }
    
    public void DeclareWin()
    {
        // Validate: player actually has winning condition
        // Award points
        // Transition to WinScreen then GameOver
        // Fire OnGameWon event
    }
    
    // State queries (for UI to check)
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

**Requirements**:
- ✅ All public methods documented with `/// <summary>`
- ✅ All phase transitions use `TransitionToPhase()` (centralized)
- ✅ All invalid actions call `OnInvalidActionAttempted(reason)`
- ✅ Events fire BEFORE state change (so UI sees old state)
- ✅ No direct board/turn manipulation; use methods
- ✅ IGameMode is called for win condition checks

**Key Design Points**:

1. **Validation First**: Every action validates before executing
   ```csharp
   public void RollDice()
   {
       ValidatePhaseForAction(GamePhase.Rolling);  // Throws or returns if wrong phase
       // ... then execute
   }
   ```

2. **Events**: Fire events so UI can react
   ```csharp
   OnDiceRolled?.Invoke(diceResult);  // UI animates dice
   OnPhaseChanged?.Invoke(GamePhase.Placing);  // UI highlights valid cells
   ```

3. **Game Mode Integration**: Call IGameMode for custom rules
   ```csharp
   if (currentGameMode.CheckWin(currentPlayer, board))
   {
       TransitionToPhase(GamePhase.WinScreen);
   }
   ```

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

### 4. TurnPhaseController.cs

**Purpose**: Encapsulate phase-specific logic (lighter than GameStateManager).

```csharp
public class TurnPhaseController
{
    private GameStateManager gameStateManager;
    
    // Rolling phase
    public void StartRollingPhase(Player currentPlayer)
    {
        // Notify game state
        // Reset roll data
    }
    
    public int[] CompleteRoll(int[] diceRoll)
    {
        // Validate roll
        // Store in game state
        // Return roll for display
    }
    
    // Placing phase
    public void StartPlacingPhase()
    {
        // Calculate valid moves based on dice sum
        // Light up cells in UI (via events)
    }
    
    public bool ExecuteMove(int fromCell, int toCell)
    {
        // Delegate to GameStateManager.MoveChip()
        // Return true if move succeeded
    }
    
    // Bump phase
    public void StartBumpDecisionPhase()
    {
        // Check if any bumps possible
        // If none, skip to EndTurn
        // If possible, show bump options
    }
    
    public bool AttemptBump(int targetCell)
    {
        // Delegate to GameStateManager.DeclareBump()
        // Return true if bump succeeded
    }
    
    // Turn end
    public void CompleteTurn()
    {
        // Delegate to GameStateManager.EndTurn()
        // Reset phase-specific state
    }
}
```

**Requirements**:
- ✅ All methods documented
- ✅ Delegation to GameStateManager (no duplication)
- ✅ Clear phase naming (StartXPhase, CompleteX)
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

### 5. TurnManager.cs (Enhanced from Sprint 1)

**Purpose**: Add turn-specific tracking beyond just player rotation.

Add these to the existing TurnManager:

```csharp
public class TurnManager
{
    // ... existing code (Setup, CurrentPlayer, etc) ...
    
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
        // Reset turn-specific state
        canRollAgain = false;
        justBumped = false;
    }
    
    public void CompleteTurn()
    {
        // Increment turn counter
        turnsCompleted++;
        AdvanceTurn();  // Move to next player
        StartNewTurn();
    }
    
    public void RecordDouble()
    {
        // Increment double count
        // If >= 3, lose turn (standard board game rule)
        consecutiveDoublesCount++;
    }
    
    public void ResetDoubleCount()
    {
        // Clear double count at turn end
        consecutiveDoublesCount = 0;
    }
    
    public void RecordBump()
    {
        // Mark that a bump occurred
        justBumped = true;
    }
}
```

**Requirements**:
- ✅ All new properties documented
- ✅ StartNewTurn() resets state, doesn't change player
- ✅ CompleteTurn() increments counter AND advances player
- ✅ RecordDouble() handles triple-double check (3+ doubles lose turn)
- ✅ Backward compatible with Sprint 1 code

**Testing** (5+ tests):
- `StartNewTurn_ResetsState()`
- `CompleteTurn_IncrementsTurnCounter()`
- `RecordDouble_IncreasesDoubleCount()`
- `RecordDouble_TripleDouble_LosesTurn()`
- `ResetDoubleCount_ClearsCount()`

---

## Integration with Sprint 1

All Sprint 1 classes remain **unchanged**. Sprint 2 uses them:

- **Player**: GameStateManager queries current player, adds score
- **Chip**: GameStateManager moves chips, checks ownership
- **BoardCell**: GameStateManager checks/validates cell state
- **BoardModel**: GameStateManager executes moves, bumps, win checks
- **DiceManager**: GameStateManager requests rolls
- **TurnManager**: GameStateManager rotates players

No modifications to Sprint 1 code. Backward compatible.

---

## Edge Cases You Must Handle

### Dice & Doubles
- ✅ Rolling a double (any pair): Player gets another roll
- ✅ Rolling 5+6 "safe": Can bump, but no bump penalty for this roll
- ✅ Rolling single 6: Lose turn (handled by mode, but tracked here)
- ✅ Three doubles in a row: Lose turn (GameStateManager must detect)

### Bump Mechanics
- ✅ Can only bump opponent's chips (not own)
- ✅ Can only bump if chip is on adjacent cell
- ✅ Must have just moved a chip to bump
- ✅ Cannot bump off-board chips

### Win Conditions
- ✅ Most modes: 5 in a row
- ✅ Some modes: Score threshold (Game 4)
- ✅ Single player mode: Special bonuses
- ✅ Win is checked AFTER every move and bump

### Invalid Actions
- ✅ Rolling when not in Rolling phase → OnInvalidActionAttempted
- ✅ Moving when no chips available → Error
- ✅ Moving to non-adjacent cell → Error
- ✅ Bumping own chips → Error
- ✅ Declaring win without condition → Error

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

Expected: 22+ tests, 100% pass rate

---

## Performance Targets

- State transitions: < 1ms
- Turn validation: < 5ms
- Complete turn flow: < 50ms
- Memory: GameState should be <1KB

---

## Code Organization

```
Assets/Scripts/Core/
├── GamePhase.cs                 (NEW - enum, 20 lines)
├── GameState.cs                 (NEW - 60 lines)
├── GameStateManager.cs          (NEW - 300+ lines)
├── TurnPhaseController.cs       (NEW - 150+ lines)
├── TurnManager.cs               (MODIFIED - add 80 lines)
└── [existing Sprint 1 classes]  (UNCHANGED)

Assets/Scripts/Tests/
├── GameStateManagerTests.cs     (NEW - 200+ lines)
├── TurnPhaseControllerTests.cs  (NEW - 150+ lines)
├── TurnManagerEnhancedTests.cs  (NEW - 100+ lines)
└── [existing Sprint 1 tests]    (UNCHANGED)
```

---

## Acceptance Criteria

Before marking Sprint 2 complete:

- ✅ All 5 new classes created with full documentation
- ✅ All 22+ unit tests pass
- ✅ Zero logic bugs in state transitions
- ✅ Code follows CODING_STANDARDS.md
- ✅ All method parameters/returns documented
- ✅ No circular dependencies
- ✅ No magic numbers (use GamePhase enum)
- ✅ Integration with Sprint 1 verified
- ✅ Event system properly declared and fired
- ✅ GameStateManager passes code review

---

## Success Checklist

- [ ] GamePhase.cs created with 9 phases
- [ ] GameState.cs created with all properties
- [ ] GameStateManager.cs handles all turn flow
- [ ] TurnPhaseController.cs delegates to GameStateManager
- [ ] TurnManager.cs enhanced with turn tracking
- [ ] All events properly declared and documented
- [ ] All 22+ tests pass
- [ ] No console errors or warnings
- [ ] Code reviewed and approved by Managing Engineer
- [ ] Backward compatible with Sprint 1

---

## Questions? Issues?

This briefing covers the requirements. If you encounter issues:
1. Document the blocker
2. Note the specific edge case
3. Escalate to Managing Engineer

---

## Next: Sprint 3 Preview

After Sprint 2 is approved, you'll implement 5 game modes that plug into this state machine.

Each mode implements IGameMode and defines:
- Custom scoring
- Custom win conditions
- Custom bump mechanics
- Roll-again rules specific to that mode

GameStateManager will call IGameMode methods to delegate mode-specific behavior.

---

**Prepared By**: Managing Engineer Agent  
**Date**: Nov 14, 2025  
**Status**: Ready for Sprint 2 Kickoff (Nov 21)
