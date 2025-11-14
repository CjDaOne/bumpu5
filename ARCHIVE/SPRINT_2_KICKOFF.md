# Sprint 2 Kickoff - Turn Manager & Game State Machine

**Sprint Duration**: Week 2 (Nov 21 - Nov 28, 2025)  
**Lead**: Gameplay Engineer Agent  
**Dependency**: Sprint 1 complete & approved  
**Goal**: Build complete turn flow with game state machine (rolling → placing → bumping → end-turn)  

---

## What We're Building

The "nervous system" of the game—a state machine that orchestrates the entire turn flow and maintains game state throughout a complete game. This connects all Sprint 1 logic into a cohesive, playable system.

By end of this sprint, a complete game can be played in code: dice rolls → moves → bumps → win detection.

---

## Deliverables (In Order)

### 1. Game State Enum (Foundational)

#### GamePhase.cs
```csharp
public enum GamePhase
{
    Setup,           // Game initialized, waiting for first player
    Rolling,         // Player rolling dice
    DiceResult,      // Waiting for player to move after roll
    Placing,         // Player moving chip on board
    BumpDecision,    // Player deciding whether to bump
    Bumping,         // Opponent chip being bumped
    EndTurn,         // Turn wrapping up
    WinScreen,       // Someone won
    GameOver         // Game complete
}
```

---

### 2. Game State Data Structure

#### GameState.cs (Enhanced from Sprint 1 preview)
Snapshot of the complete game at any point.

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
    public bool CanRollAgain { get; set; }  // Doubles or special case
    public bool MustBump { get; set; }      // Mode-specific rule
    
    // Pending actions
    public int? PendingBumpCell { get; set; }  // Cell to bump (null if none)
    public Chip LastMovedChip { get; set; }
    
    // Serialization for save/load
    public string Serialize() { }
    public static GameState Deserialize(string json) { }
}
```

---

### 3. Game State Manager (Core)

#### GameStateManager.cs
Orchestrates the entire game flow. This is the "conductor" of the game.

```csharp
public class GameStateManager
{
    private GameState gameState;
    private TurnManager turnManager;
    private DiceManager diceManager;
    private BoardModel board;
    private IGameMode currentGameMode;
    
    // Events (for UI to listen)
    public event System.Action<GamePhase> OnPhaseChanged;
    public event System.Action<Player> OnPlayerChanged;
    public event System.Action<int[]> OnDiceRolled;
    public event System.Action<bool> OnGameWon;
    public event System.Action<string> OnInvalidActionAttempted;
    
    // Initialization
    public void InitializeGame(IGameMode gameMode, Player[] players);
    
    // Turn flow methods
    public void RollDice();
    public void MovementChip(int fromCell, int toCell);
    public void DeclareBump(int targetCell);
    public void EndTurn();
    public void DeclareWin();
    
    // State queries
    public GamePhase GetCurrentPhase();
    public Player GetCurrentPlayer();
    public bool IsValidMove(int fromCell, int toCell);
    public bool CanBumpTarget(int targetCell);
    public bool HasWon(Player player);
    
    // Validation
    private void ValidatePhaseForAction(GamePhase requiredPhase);
    private void TransitionToPhase(GamePhase nextPhase);
}
```

---

### 4. Turn Phase Controller

#### TurnPhaseController.cs
Manages the transitions and logic for each phase.

```csharp
public class TurnPhaseController
{
    // Rolling phase
    public void StartRollingPhase(Player currentPlayer);
    public void CompleteRoll(int[] diceRoll);
    
    // Placement phase
    public void StartPlacingPhase();
    public bool ExecuteMove(int fromCell, int toCell);
    public void CompleteMove();
    
    // Bump phase
    public void StartBumpDecisionPhase();
    public bool AttemptBump(int targetCell);
    public void SkipBump();
    
    // End turn
    public void CompleteTurn();
    public Player GetNextPlayer();
}
```

---

### 5. Updated TurnManager (Sprint 2)

#### TurnManager.cs (Enhanced)
Add turn-specific state beyond just player rotation.

```csharp
public class TurnManager
{
    // ... existing code ...
    
    // NEW - Turn state
    private int turnsCompleted;
    private bool canRollAgain;
    private bool justBumped;
    private int consecutiveDoublesCount;
    
    public int TurnsCompleted => turnsCompleted;
    public bool CanRollAgain { get; set; }
    public bool JustBumped { get; set; }
    public int ConsecutiveDoublesCount => consecutiveDoublesCount;
    
    // NEW - Turn lifecycle
    public void StartNewTurn();
    public void CompleteTurn();
    public void RecordDouble();
    public void ResetDoubleCount();
    public void RecordBump();
}
```

---

## State Machine Diagram

```
                    [Setup]
                      ↓
    ┌───────────────────────────────────┐
    ↓                                   ↓
[Rolling] ←──── Player rolls dice     [GameOver]
    ↓                                   ↑
[DiceResult] ←── Validate roll        [WinScreen]
    ↓                (6 = lose turn)    ↑
[Placing] ←──── Show valid moves      [Bumping]
    ↓                                   ↑
[BumpDecision] ← Check 5-in-a-row?   [EndTurn]
    ├─→ Yes: [WinScreen]                ↑
    └─→ No:  [Bumping] (optional)       ↓
                  ↓                  [Rolling] (next player)
            [EndTurn]
                  ↓
            Next player
```

---

## Unit Tests Required

### Test File: GameStateManagerTests.cs
```
- InitializeGame_SetsupPlayers_ReturnsTrue()
- RollDice_TransitionsToPlacingPhase()
- MoveChip_WithValidMove_TransitionsCorrectly()
- MoveChip_WithInvalidMove_RaisesError()
- DeclareBump_ValidatesOwnership()
- EndTurn_RotatesToNextPlayer()
- EndTurn_With6_AllowsRollAgain()
- EndTurn_WithDouble_AllowsRollAgain()
- HasWon_ChecksGameModeWinCondition()
- InvalidAction_InWrongPhase_RaisesError()
```

### Test File: GamePhaseControllerTests.cs
```
- StartRollingPhase_SetsCurrentPhase()
- CompleteRoll_TransitionsToPlacing()
- ExecuteMove_WithValidMove_ReturnsTrue()
- ExecuteMove_WithBumpChip_NotifiesEvent()
- StartBumpDecisionPhase_WhenChipBumpable()
- SkipBump_AdvancesToEndTurn()
- CompleteTurn_RotatesPlayer()
```

### Test File: TurnManagerEnhancedTests.cs
```
- StartNewTurn_ResetsState()
- CompleteTurn_IncrementsTurnCounter()
- RecordDouble_IncreasesDoubleCount()
- ResetDoubleCount_ClearsCount()
- RecordBump_SetsBumpFlag()
```

---

## Edge Cases to Handle

### Dice & Doubles
- Rolling a double (any pair) → Player gets another roll
- Rolling 5+6 combination → "Safe" (no bump possible) - special case
- Rolling 6 with single die → Lose turn immediately
- Three doubles in a row → Lose turn (standard board game rule)

### Bump Mechanics
- Can only bump opponent's chips
- Must be adjacent to target
- Must have a chip to move
- Cannot bump own chips

### Win Conditions
- 5 in a row (primary)
- Game mode specific variations (Game 4 has different rule)
- Must not be the result of being forced to move

### Invalid Actions
- Rolling when not in Rolling phase
- Moving when no chip available
- Moving to non-adjacent cell
- Bumping when no bump possible
- Declaring win when no 5-in-a-row exists

---

## File Structure (Created in Sprint 2)

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

## Integration Points

### With Sprint 1 Classes
- GameStateManager uses TurnManager, DiceManager, BoardModel, Players
- All existing classes remain unchanged (backward compatible)
- No modifications to Sprint 1 logic

### With Sprint 3 (Preview)
- GameStateManager will call IGameMode.OnRoll(), OnPlace(), CheckWin()
- Game modes will implement their own victory conditions
- Different modes have different turn flows (some skip bumping, etc.)

### With Sprint 4 (Preview)
- Board visualization will listen to GameStateManager events
- UI will respond to phase changes
- Events drive all visual updates

---

## Success Criteria

✅ GameStateManager orchestrates complete turn flow  
✅ State machine handles all phases correctly  
✅ All 22+ unit tests pass  
✅ Zero logic bugs in state transitions  
✅ Code follows CODING_STANDARDS.md  
✅ Full documentation (method summaries)  
✅ Events properly fired for UI to listen  
✅ No circular dependencies  

---

## Review Checklist

Before this sprint is complete:
- [ ] All new classes follow PascalCase naming
- [ ] All public methods documented with /// comments
- [ ] No magic numbers (all phase logic uses enums)
- [ ] GameStateManager is testable (dependency injection ready)
- [ ] All 22+ tests pass
- [ ] Code reviewed by Managing Engineer
- [ ] No console errors/warnings
- [ ] Integration with Sprint 1 classes verified
- [ ] Events are properly declared and fired
- [ ] Edge cases (doubles, bumps, win) all handled

---

## Performance Targets

- State transitions: < 1ms
- Turn validation: < 5ms
- Complete turn flow: < 50ms
- Memory: Minimal (GameState reusable)

---

## Next Sprint Preview (Sprint 3)

Sprint 3 will implement the 5 game modes that plug into this state machine.

Each mode will:
- Implement IGameMode interface
- Define custom scoring rules
- Define custom win conditions
- Define custom bump mechanics (some modes skip bumping)
- Hook into GameStateManager events

---

**Sprint Start Date**: Nov 21, 2025  
**Estimated Completion**: Nov 28, 2025  
**Owner**: Gameplay Engineer Agent  
**Dependency**: Sprint 1 approved ✅
