# Sprint 2 Execution Plan - Accelerated

**Status**: Active (Start Immediately)  
**Duration**: 1 week  
**Lead**: Gameplay Engineer  
**Dependency**: Sprint 1 code approved ✅  

---

## Overview

Sprint 2 implements the GameStateManager and TurnManager integration, creating a complete turn-flow state machine. This is the critical dependency for all subsequent sprints.

**Deliverables**:
- GameStateManager.cs (complete state machine)
- GamePhase enum (Rolling, Placing, Bumping, EndTurn, GameOver)
- Integration tests (turn flow, phase transitions)
- Documentation

**Lines of Code**: 500-800  
**Unit Tests**: 22+  
**Success Criteria**: End-to-end game flow testable without UI

---

## Critical Path Items

### Item 1: GamePhase Enum (2 hours)
**File**: `/Assets/Scripts/Core/GamePhase.cs`

```csharp
/// <summary>
/// Enum representing all game phases in the turn cycle.
/// </summary>
public enum GamePhase
{
    Setup,          // Initial game setup
    Rolling,        // Player rolls dice
    Placing,        // Player places chip on board
    Bumping,        // Optional: bump opponent chip
    EndTurn,        // Turn wrapping (penalties, pass chip, etc.)
    GameOver        // Win condition detected
}
```

**Definition of Done**:
- ✅ Enum created with all 6 phases
- ✅ No compiler errors
- ✅ Follows CODING_STANDARDS.md naming

---

### Item 2: GameStateManager.cs (6-8 hours)
**File**: `/Assets/Scripts/Core/GameStateManager.cs`

```csharp
/// <summary>
/// Central game state machine managing turn flow and phase transitions.
/// Orchestrates all game logic: dice rolling, chip placement, bumping, win detection.
/// Single source of truth for game state.
/// </summary>
public class GameStateManager
{
    // Core state
    private GamePhase currentPhase;
    private Player currentPlayer;
    private int[] lastDiceRoll;
    
    // References
    private BoardModel boardModel;
    private TurnManager turnManager;
    private DiceManager diceManager;
    
    // Events
    public event System.Action<GamePhase> OnPhaseChanged;
    public event System.Action<int[]> OnDiceRolled;
    public event System.Action<Player> OnPlayerChanged;
    public event System.Action<Player> OnGameWon;
    public event System.Action<string> OnInvalidAction;
    
    // Setup
    public void Initialize(Player player1, Player player2);
    public void StartGame();
    
    // Turn flow
    public void RollDice();
    public void PlaceChip(int cellIndex);
    public void BumpOpponentChip(int cellIndex);
    public void EndTurn();
    
    // State queries
    public GamePhase CurrentPhase { get; }
    public Player CurrentPlayer { get; }
    public int[] LastDiceRoll { get; }
    public bool CanPlaceChip(int cellIndex);
    public bool CanBumpChip(int cellIndex);
    
    // Win condition
    private void CheckWinCondition();
    private void ApplyGameModeRules();
    private void TransitionPhase(GamePhase newPhase);
}
```

**Implementation Requirements**:

1. **Phase Transitions** (4 hours)
   - Setup → Rolling
   - Rolling → Placing (after dice roll)
   - Placing → Bumping (optional, if valid bumps exist)
   - Bumping → EndTurn
   - EndTurn → Rolling (next player) or GameOver
   - Rolling → GameOver (immediate win detection)

2. **Event Broadcasting** (1 hour)
   - Emit OnPhaseChanged whenever phase changes
   - Emit OnDiceRolled with roll results
   - Emit OnPlayerChanged when turn switches
   - Emit OnGameWon with winner
   - Emit OnInvalidAction with reason

3. **Validation Gates** (2 hours)
   - CanPlaceChip() checks board validity + game mode rules
   - CanBumpChip() checks adjacency + opponent ownership
   - Guard against invalid state transitions

4. **Edge Cases** (1 hour)
   - Double dice roll (5+6 = safe, skip all rolls)
   - Rolling a 6 (lose turn, stay in Rolling phase)
   - No valid moves (skip to EndTurn)
   - Player wins mid-turn (immediate GameOver)

**Definition of Done**:
- ✅ All 6 phases implemented
- ✅ Phase transition logic complete
- ✅ All events firing correctly
- ✅ No compiler errors
- ✅ Follows CODING_STANDARDS.md
- ✅ Full method documentation
- ✅ No magic numbers (use constants)

---

### Item 3: Integration Tests (6-8 hours)
**File**: `/Assets/Scripts/Tests/GameStateManagerTests.cs`

**Test Categories**:

1. **Setup & Initialization** (2 tests)
   ```csharp
   [Test] public void Initialize_SetsUpGameCorrectly()
   [Test] public void StartGame_TransitionsToRollingPhase()
   ```

2. **Phase Transitions** (8 tests)
   ```csharp
   [Test] public void RollDice_TransitionsFromRollingToPlacing()
   [Test] public void PlaceChip_TransitionsFromPlacingToBumping()
   [Test] public void BumpChip_TransitionsFromBumpingToEndTurn()
   [Test] public void EndTurn_TransitionsFromEndTurnToRolling()
   [Test] public void RollDice_With6_StaysInRollingPhase()
   [Test] public void RollDice_With5Plus6_SkipsAllRolls()
   [Test] public void NoValidMoves_SkipsToPlacinggBumpingToEndTurn()
   [Test] public void WinCondition_ImmediatelyTransitionsToGameOver()
   ```

3. **Event Firing** (6 tests)
   ```csharp
   [Test] public void OnPhaseChanged_FiresForEachTransition()
   [Test] public void OnDiceRolled_FiresWithCorrectValues()
   [Test] public void OnPlayerChanged_FiresAfterEndTurn()
   [Test] public void OnGameWon_FiresWithWinner()
   [Test] public void OnInvalidAction_FiresForBadMove()
   [Test] public void MultipleListeners_AllReceiveEvents()
   ```

4. **State Queries** (4 tests)
   ```csharp
   [Test] public void CanPlaceChip_ReturnsTrueForValidCell()
   [Test] public void CanPlaceChip_ReturnsFalseForOwnChip()
   [Test] public void CanBumpChip_ReturnsTrueForOpponentChip()
   [Test] public void CanBumpChip_ReturnsFalseForEmptyCell()
   ```

5. **Edge Cases** (4 tests)
   ```csharp
   [Test] public void DoubleRoll_AllowsReroll()
   [Test] public void LoseTurn_SkipsToNextPlayer()
   [Test] public void NoChips_SkipsPlacingPhase()
   [Test] public void Win5InARow_DetectedAutomatically()
   ```

**Definition of Done**:
- ✅ 22+ tests implemented
- ✅ 100% pass rate
- ✅ Test names follow MethodName_Condition_Result pattern
- ✅ Full SetUp/TearDown for isolation
- ✅ No console errors/warnings

---

## Daily Breakdown

### Day 1 (2 hours)
- Create GamePhase.cs enum
- Stub out GameStateManager.cs with class skeleton
- Verify compilation
- **Deliverable**: GamePhase enum ready, GameStateManager compiles

### Day 2-3 (6 hours)
- Implement Initialize() and StartGame()
- Implement RollDice() with phase transition
- Implement PlaceChip() with validation
- Implement BumpOpponentChip()
- Implement EndTurn()
- **Deliverable**: All 5 core methods functional

### Day 4 (4 hours)
- Implement all phase transition logic
- Implement all event broadcasting
- Add null checks and error handling
- **Deliverable**: Full state machine working

### Day 5-6 (6 hours)
- Write all 22+ integration tests
- Debug test failures
- Refine edge case handling
- **Deliverable**: All tests passing

### Day 7 (2 hours)
- Code review vs standards
- Final bug fixes
- Prepare for code review submission
- **Deliverable**: Ready for ME review

---

## Code Review Checklist

Before submitting for review, verify:

- [ ] All methods documented (XML comments)
- [ ] All constants used (no magic numbers)
- [ ] Guard clauses on entry (null checks)
- [ ] Events fire correctly and are null-checked
- [ ] Phase transitions are defensive (validate state)
- [ ] No compiler warnings
- [ ] Test coverage ≥ 80%
- [ ] Naming follows CODING_STANDARDS.md
- [ ] No nested conditionals (guard clauses instead)
- [ ] Performance: no allocations in hot paths

---

## Integration With Sprint 1

GameStateManager depends on:
- ✅ Player.cs (already complete)
- ✅ BoardModel.cs (already complete)
- ✅ TurnManager.cs (already complete)
- ✅ DiceManager.cs (already complete)

These should NOT be modified. GameStateManager orchestrates them.

---

## Success Metrics

| Metric | Target | Definition |
|--------|--------|------------|
| Code Coverage | 85%+ | Statements covered by tests |
| Test Pass Rate | 100% | All 22+ tests pass |
| Compilation | 0 errors | No compiler issues |
| Standards | 100% | Follows CODING_STANDARDS.md |
| Documentation | 100% | All public methods documented |
| Code Review | Approved | Passes ME review |

---

## Next Steps (Sprint 3 Prep)

Once Sprint 2 is complete:
1. GameStateManager available for game modes to use
2. Game1_Bump5 will inherit from IGameMode and use GameStateManager
3. All 5 game modes will follow same pattern
4. Sprint 3 can parallelize game mode implementations

---

## Support & Escalation

### Blockers (Report Immediately)
- Compilation errors that don't resolve
- Logic deadlock in state machine
- Test failures that indicate design flaw

### 24-Hour SLA
- Code review completed within 24 hours
- Questions answered within 24 hours
- Blockers escalated to ME immediately

---

**Start Date**: Immediately  
**Target Completion**: 7 days  
**Owner**: Gameplay Engineer  
**Managing Engineer**: Amp  
**Status**: 🟢 Ready to begin
