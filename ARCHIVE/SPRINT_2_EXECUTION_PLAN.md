# Sprint 2 Execution Plan - State Machine & Game Flow Control

**Sprint Duration**: Immediate activation (5 calendar days)  
**Lead**: Gameplay Engineer Agent  
**Critical Path**: Yes (blocks all downstream sprints)  
**Dependency**: Sprint 1 ✅ (Core Logic complete)  

---

## Sprint Goal

Implement complete GameStateManager with state machine logic to coordinate all gameplay phases (Rolling → Placing → Bumping → End Turn), enabling the game to flow correctly and serve as the orchestrator for all game events.

---

## Deliverables Overview

| Deliverable | Lines of Code | Unit Tests | Est. Hours | Owner |
|-------------|----------------|-----------|-----------|-------|
| GamePhase enum | ~40 | 8 | 0.5h | Gameplay Eng |
| GameStateManager core | ~600 | 30+ | 12h | Gameplay Eng |
| State transition logic | ~250 | 15+ | 4h | Gameplay Eng |
| Integration tests | ~400 | 25+ | 6h | Gameplay Eng |
| Documentation | ~100 | - | 2h | Gameplay Eng |
| Code review & fixes | - | - | 2h | ME (Amp) |
| **TOTAL** | **~1,400** | **78+** | **27h** | |

---

## Daily Breakdown

### **DAY 1: GamePhase Enum & Scaffolding**

**Goal**: Establish enum and core class structure  
**Hours**: 2h

#### Task 1.1: GamePhase Enum
**File**: `Assets/Scripts/Game/GamePhase.cs` (NEW)

```csharp
public enum GamePhase
{
    Idle = 0,           // Before game starts
    RollDice = 1,       // Player rolls dice
    MoveChip = 2,       // Player places chip on board
    BumpOpponent = 3,   // Optional: bump opponent chip
    EndTurn = 4,        // Clean up turn, switch to next player
    GameWon = 5,        // Game has been won
    GameOver = 6        // Final state
}
```

**Acceptance Criteria**:
- [ ] Enum compiles without errors
- [ ] All 7 phases clearly named & ordered
- [ ] Documentation comment on each value
- [ ] Test: Enum values match specification
- [ ] Test: Cannot have missing/duplicate phases

**Commit Message**: `feat: Add GamePhase enum for state machine`

---

#### Task 1.2: GameStateManager Scaffold
**File**: `Assets/Scripts/Managers/GameStateManager.cs` (NEW)

Basic skeleton:

```csharp
public class GameStateManager : MonoBehaviour
{
    // State
    private GameState currentGameState;
    private GamePhase currentPhase;
    private TurnManager turnManager;
    
    // References
    private GameState gameState;
    private DiceManager diceManager;
    private BoardModel boardModel;
    
    // Events
    public event System.Action<GamePhase> OnPhaseChanged;
    public event System.Action<Player> OnPlayerChanged;
    public event System.Action<int[]> OnDiceRolled;
    public event System.Action<Player> OnGameWon;
    public event System.Action<string> OnInvalidAction;
    
    // Initialization
    public void Initialize(GameState gameState, TurnManager tm, DiceManager dm, BoardModel bm);
    
    // Phase queries
    public GamePhase GetCurrentPhase();
    public Player GetCurrentPlayer();
    public bool CanRollDice();
    public bool CanPlaceChip(int cellIndex);
    public bool CanBumpChip(int targetCell);
    
    // Main state machine loop
    private void UpdatePhase();
    public void ProcessAction(GameAction action);
}
```

**Acceptance Criteria**:
- [ ] Compiles without errors
- [ ] All public methods declared
- [ ] Event signatures correct
- [ ] Constructor/Initialize pattern established
- [ ] Ready for Task 2 implementation

**Commit Message**: `feat: Add GameStateManager skeleton with event system`

---

### **DAY 2: Phase Transition Logic & Core Flows**

**Goal**: Implement state machine transitions and game flow  
**Hours**: 6h

#### Task 2.1: Phase Transition System
**File**: Update `GameStateManager.cs`

Implement the phase transition logic:

```csharp
private void TransitionToPhase(GamePhase newPhase)
{
    // Validate transition
    if (!IsValidTransition(currentPhase, newPhase))
    {
        Debug.LogError($"Invalid phase transition: {currentPhase} → {newPhase}");
        OnInvalidAction?.Invoke("Invalid game phase transition");
        return;
    }
    
    // Exit current phase
    OnPhaseExit(currentPhase);
    
    // Update state
    currentPhase = newPhase;
    
    // Enter new phase
    OnPhaseEnter(currentPhase);
    
    // Notify listeners
    OnPhaseChanged?.Invoke(currentPhase);
}

private bool IsValidTransition(GamePhase from, GamePhase to)
{
    // Define valid transitions
    // RollDice → MoveChip (after dice rolled)
    // MoveChip → BumpOpponent (optional)
    // BumpOpponent → EndTurn (or skip if not bumping)
    // MoveChip → EndTurn (if no bump)
    // EndTurn → RollDice (next player)
    // Any phase → GameWon (if winning condition met)
    // GameWon → GameOver (final)
    
    return allowedTransitions[from].Contains(to);
}
```

**Acceptance Criteria**:
- [ ] Valid transitions table implemented
- [ ] Invalid transitions blocked with error message
- [ ] OnPhaseEnter/Exit hooks callable
- [ ] Event fires on transition
- [ ] Test: Valid transitions succeed
- [ ] Test: Invalid transitions fail gracefully
- [ ] Test: Transition order enforced

**Commit Message**: `feat: Implement phase transition logic with validation`

---

#### Task 2.2: RollDice Phase Handler
**File**: Update `GameStateManager.cs`

Handle dice roll phase:

```csharp
public void RollDice()
{
    if (currentPhase != GamePhase.RollDice)
    {
        OnInvalidAction?.Invoke("Cannot roll dice in " + currentPhase);
        return;
    }
    
    // Roll dice
    int[] roll = diceManager.Roll();
    OnDiceRolled?.Invoke(roll);
    
    // Update game state
    gameState.lastDiceRoll = roll;
    
    // Transition to next phase
    TransitionToPhase(GamePhase.MoveChip);
}
```

**Acceptance Criteria**:
- [ ] Only works in RollDice phase
- [ ] Calls DiceManager.Roll()
- [ ] Updates gameState with result
- [ ] Fires OnDiceRolled event
- [ ] Auto-transitions to MoveChip
- [ ] Test: RollDice in correct phase succeeds
- [ ] Test: RollDice in wrong phase fails

**Commit Message**: `feat: Implement RollDice phase with DiceManager integration`

---

#### Task 2.3: MoveChip Phase Handler
**File**: Update `GameStateManager.cs`

Handle chip placement:

```csharp
public void PlaceChip(int cellIndex)
{
    if (currentPhase != GamePhase.MoveChip)
    {
        OnInvalidAction?.Invoke("Cannot place chip in " + currentPhase);
        return;
    }
    
    Player currentPlayer = GetCurrentPlayer();
    int[] lastRoll = gameState.lastDiceRoll;
    
    // Validate move using BoardModel
    if (!boardModel.IsValidMove(currentPlayer, cellIndex, lastRoll))
    {
        OnInvalidAction?.Invoke("Invalid move for this roll");
        return;
    }
    
    // Execute move
    boardModel.PlaceChip(currentPlayer, cellIndex);
    gameState.board = boardModel.GetBoardState();
    
    // Transition
    TransitionToPhase(GamePhase.BumpOpponent);
}
```

**Acceptance Criteria**:
- [ ] Only works in MoveChip phase
- [ ] Validates move via BoardModel
- [ ] Calls BoardModel.PlaceChip()
- [ ] Updates gameState.board
- [ ] Transitions to BumpOpponent
- [ ] Test: Valid move succeeds
- [ ] Test: Invalid move rejected

**Commit Message**: `feat: Implement MoveChip phase with validation`

---

#### Task 2.4: BumpOpponent Phase Handler
**File**: Update `GameStateManager.cs`

Handle optional bump:

```csharp
public void BumpChip(int targetCell)
{
    if (currentPhase != GamePhase.BumpOpponent)
    {
        OnInvalidAction?.Invoke("Cannot bump in " + currentPhase);
        return;
    }
    
    Player currentPlayer = GetCurrentPlayer();
    
    // Validate bump (opponent chip exists, is bumpable)
    if (!boardModel.CanBump(currentPlayer, targetCell))
    {
        OnInvalidAction?.Invoke("Cannot bump that cell");
        return;
    }
    
    // Execute bump
    boardModel.BumpChip(currentPlayer, targetCell);
    gameState.board = boardModel.GetBoardState();
    
    // Transition
    TransitionToPhase(GamePhase.EndTurn);
}

public void SkipBump()
{
    if (currentPhase != GamePhase.BumpOpponent)
    {
        OnInvalidAction?.Invoke("Cannot skip bump in " + currentPhase);
        return;
    }
    
    // Skip bump, go to EndTurn
    TransitionToPhase(GamePhase.EndTurn);
}
```

**Acceptance Criteria**:
- [ ] BumpChip only works in BumpOpponent phase
- [ ] SkipBump only works in BumpOpponent phase
- [ ] Validates bump target
- [ ] Calls BoardModel.BumpChip()
- [ ] Transitions to EndTurn
- [ ] Test: Valid bump succeeds
- [ ] Test: Invalid bump rejected
- [ ] Test: SkipBump works

**Commit Message**: `feat: Implement BumpOpponent phase with optional skip`

---

### **DAY 3: Turn Management & Win Detection**

**Goal**: End turn, switch players, detect win conditions  
**Hours**: 6h

#### Task 3.1: EndTurn Phase Handler
**File**: Update `GameStateManager.cs`

Handle end of turn:

```csharp
public void EndTurn()
{
    if (currentPhase != GamePhase.EndTurn)
    {
        OnInvalidAction?.Invoke("Cannot end turn in " + currentPhase);
        return;
    }
    
    // Check for win condition
    if (CheckWinCondition())
    {
        TransitionToPhase(GamePhase.GameWon);
        return;
    }
    
    // Switch to next player
    Player nextPlayer = turnManager.NextPlayer();
    OnPlayerChanged?.Invoke(nextPlayer);
    
    // Reset for next turn
    gameState.lastDiceRoll = null;
    gameState.lastBump = null;
    
    // Go back to roll
    TransitionToPhase(GamePhase.RollDice);
}
```

**Acceptance Criteria**:
- [ ] Only works in EndTurn phase
- [ ] Checks win condition first
- [ ] Switches player via TurnManager
- [ ] Resets roll/bump state
- [ ] Fires OnPlayerChanged event
- [ ] Transitions to RollDice (or GameWon)
- [ ] Test: EndTurn with no winner advances turn
- [ ] Test: EndTurn with winner goes to GameWon

**Commit Message**: `feat: Implement EndTurn phase with player switching`

---

#### Task 3.2: Win Detection System
**File**: Update `GameStateManager.cs`

Implement win condition checking:

```csharp
private bool CheckWinCondition()
{
    Player currentPlayer = GetCurrentPlayer();
    
    // Mode-specific win check
    IGameMode gameMode = GetCurrentGameMode();
    bool hasWon = gameMode.CheckWinCondition(currentPlayer, gameState);
    
    if (hasWon)
    {
        gameState.winner = currentPlayer;
        return true;
    }
    
    return false;
}

public void DeclareWin(Player declarer)
{
    // Allow player to declare they've won
    if (currentPhase == GamePhase.RollDice || currentPhase == GamePhase.MoveChip)
    {
        if (CheckWinCondition() && GetCurrentPlayer() == declarer)
        {
            TransitionToPhase(GamePhase.GameWon);
        }
        else
        {
            OnInvalidAction?.Invoke("Cannot declare win yet");
        }
    }
}
```

**Acceptance Criteria**:
- [ ] Calls IGameMode.CheckWinCondition()
- [ ] DeclareWin validates player & condition
- [ ] Sets gameState.winner
- [ ] Transitions to GameWon phase
- [ ] Test: Win detected correctly
- [ ] Test: False win declaration rejected
- [ ] Test: Valid declarations accepted

**Commit Message**: `feat: Implement win detection with mode-specific checks`

---

#### Task 3.3: GameWon & GameOver Phases
**File**: Update `GameStateManager.cs`

Handle end-of-game states:

```csharp
private void OnPhaseEnter_GameWon()
{
    Player winner = gameState.winner;
    OnGameWon?.Invoke(winner);
    
    // Auto-transition after delay (for UI to show won message)
    StartCoroutine(AutoTransitionToGameOver(3f));
}

private IEnumerator AutoTransitionToGameOver(float delay)
{
    yield return new WaitForSeconds(delay);
    TransitionToPhase(GamePhase.GameOver);
}

private void OnPhaseEnter_GameOver()
{
    // Game is now complete
    // UI should show final score
    // Player can return to menu
}
```

**Acceptance Criteria**:
- [ ] GameWon fires OnGameWon event
- [ ] GameWon auto-transitions to GameOver
- [ ] GameOver is terminal state
- [ ] Test: GameWon → GameOver transition
- [ ] Test: GameOver cannot transition further

**Commit Message**: `feat: Implement GameWon and GameOver terminal phases`

---

### **DAY 4: Integration Tests & Cross-System Verification**

**Goal**: Verify state machine works with Sprint 1 classes  
**Hours**: 6h

#### Task 4.1: Integration Test Suite
**File**: `Assets/Scripts/Tests/GameStateManagerIntegrationTests.cs` (NEW)

```csharp
[TestFixture]
public class GameStateManagerIntegrationTests
{
    private GameStateManager gsm;
    private GameState gameState;
    private TurnManager turnManager;
    private DiceManager diceManager;
    private BoardModel boardModel;
    
    [SetUp]
    public void Setup()
    {
        // Initialize all Sprint 1 components
        gameState = new GameState();
        turnManager = new TurnManager(new[] { new Player(1, "P1"), new Player(2, "P2") });
        diceManager = new DiceManager();
        boardModel = new BoardModel();
        
        gsm = gameObject.AddComponent<GameStateManager>();
        gsm.Initialize(gameState, turnManager, diceManager, boardModel);
    }
    
    [Test]
    public void FullGameFlow_ValidMoves_CompletesSuccessfully()
    {
        // RollDice phase
        Assert.AreEqual(GamePhase.RollDice, gsm.GetCurrentPhase());
        gsm.RollDice();
        Assert.AreEqual(GamePhase.MoveChip, gsm.GetCurrentPhase());
        
        // MoveChip phase
        gsm.PlaceChip(1);
        Assert.AreEqual(GamePhase.BumpOpponent, gsm.GetCurrentPhase());
        
        // Skip bump
        gsm.SkipBump();
        Assert.AreEqual(GamePhase.EndTurn, gsm.GetCurrentPhase());
        
        // End turn
        gsm.EndTurn();
        Assert.AreEqual(GamePhase.RollDice, gsm.GetCurrentPhase());
    }
    
    [Test]
    public void InvalidTransition_Blocked()
    {
        // Try to place chip in RollDice phase (invalid)
        Assert.Throws<InvalidOperationException>(() => gsm.PlaceChip(1));
    }
    
    // ... 20+ more tests
}
```

**Test Coverage** (25+ tests):
- [ ] Test: Full valid game flow
- [ ] Test: Phase transitions validated
- [ ] Test: Invalid transitions blocked
- [ ] Test: Event firing on transitions
- [ ] Test: Player switching works
- [ ] Test: Dice roll generates valid moves
- [ ] Test: Win detection accurate
- [ ] Test: Bump validation works
- [ ] Test: Skip bump works
- [ ] Test: State consistency maintained

**Acceptance Criteria**:
- [ ] All 25+ tests pass
- [ ] Coverage > 85%
- [ ] Integration with Sprint 1 verified
- [ ] No state inconsistencies
- [ ] Blocking behavior tested

**Commit Message**: `test: Add 25+ integration tests for GameStateManager`

---

#### Task 4.2: Unit Tests for GamePhase Enum
**File**: `Assets/Scripts/Tests/GamePhaseTests.cs` (NEW)

```csharp
[TestFixture]
public class GamePhaseTests
{
    [Test]
    public void AllPhasesHaveUniqueValues()
    {
        var phases = System.Enum.GetValues(typeof(GamePhase));
        var values = new HashSet<int>();
        
        foreach (int phase in phases)
        {
            Assert.IsTrue(values.Add(phase), $"Duplicate phase value: {phase}");
        }
    }
    
    [Test]
    public void IdlePhaseIsFirst()
    {
        Assert.AreEqual(0, (int)GamePhase.Idle);
    }
    
    // ... 8 more tests
}
```

**Acceptance Criteria**:
- [ ] All 8 tests pass
- [ ] Coverage = 100%

**Commit Message**: `test: Add unit tests for GamePhase enum`

---

### **DAY 5: Code Review, Documentation, Polish**

**Goal**: Review, fix issues, finalize Sprint 2  
**Hours**: 4h

#### Task 5.1: Code Review (Managing Engineer)
**Action**: ME (Amp) reviews all GameStateManager code

**Review Checklist**:
- [ ] Follows CODING_STANDARDS.md
- [ ] No compiler warnings
- [ ] All public methods documented
- [ ] Event naming consistent
- [ ] State consistency maintained
- [ ] Performance acceptable
- [ ] No memory leaks (events unsubscribed)
- [ ] Tests comprehensive (25+)
- [ ] Coverage ≥ 85%

**Outcome**: Approved or "Needs Changes" with feedback

**Commit Message** (if approved): `chore: Code review approved - Sprint 2 ready`

---

#### Task 5.2: Documentation
**Files to Update**:
- [ ] `Assets/Scripts/Managers/GameStateManager.cs` - Full inline docs
- [ ] `ARCHITECTURE.md` - State machine diagram
- [ ] `CODING_STANDARDS.md` - Add event naming convention pattern

**Documentation Includes**:
- [ ] Class-level doc explaining state machine
- [ ] Each phase documented
- [ ] Event signatures documented
- [ ] State transition diagram (ASCII or image)
- [ ] Example usage of GameStateManager

**Commit Message**: `docs: Add comprehensive GameStateManager documentation`

---

#### Task 5.3: Final Checks
**Checklist**:
- [ ] Code compiles with 0 errors/warnings
- [ ] All 78+ tests passing
- [ ] Code coverage ≥ 85%
- [ ] Integration with Sprint 1 verified
- [ ] No blockers for Sprint 3
- [ ] Documentation complete
- [ ] All commits logged with meaningful messages

---

## Success Criteria (Sprint 2 Complete)

✅ GamePhase enum complete (7 phases defined)  
✅ GameStateManager implemented (600+ lines)  
✅ State transitions validated (all paths tested)  
✅ Win detection working (mode-specific)  
✅ 78+ unit/integration tests passing  
✅ Test coverage ≥ 85%  
✅ Code review approved  
✅ Full documentation  
✅ Zero blockers identified  
✅ Code follows CODING_STANDARDS.md  

---

## Files Delivered (Sprint 2)

```
Assets/Scripts/Game/
  └─ GamePhase.cs (NEW - 40 lines)

Assets/Scripts/Managers/
  └─ GameStateManager.cs (NEW - 600+ lines)

Assets/Scripts/Tests/
  ├─ GameStateManagerIntegrationTests.cs (NEW - 400 lines, 25+ tests)
  └─ GamePhaseTests.cs (NEW - 100 lines, 8 tests)

Documentation/
  ├─ ARCHITECTURE.md (UPDATED - state machine diagram)
  └─ CODING_STANDARDS.md (UPDATED - event patterns)
```

---

## Dependencies & Next Steps

**Blocks**: Sprint 3 (Game Modes - requires GameStateManager)  
**Blocked By**: Sprint 1 ✅ (Core Logic - COMPLETE)

**After Sprint 2 Approval**:
1. Gameplay Team enters Sprint 3 (Nov 28)
2. Board Team reviews code for Sprint 4 prep
3. UI Team reviews code for Sprint 5 prep

---

## Owner & Status

**Owner**: Gameplay Engineer Agent  
**Status**: READY TO EXECUTE  
**Start Date**: Immediate  
**Estimated End**: 5 calendar days  
**Managing Engineer**: Amp (oversight, code review, blocker resolution)

---

**Last Updated**: Nov 14, 2025  
**Document Status**: ACTIVE - Execution begins immediately
