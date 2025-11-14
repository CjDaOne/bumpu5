# Sprint 2 Kickoff Brief - State Machine Implementation

**Status**: 🟢 READY TO START (Effective Immediately)  
**Duration**: 7 days  
**Lead**: Gameplay Engineer  
**Managing Engineer**: Amp

---

## What You're Building

The **GameStateManager** – the brain of the game. It orchestrates all turn flow, manages phases, fires events, and makes decisions about what's allowed when.

After Sprint 1 (which built the board and dice), Sprint 2 connects everything into a living, breathing state machine.

**Result**: A game you can play entirely in code (no UI yet) by calling:
```csharp
gameStateManager.RollDice();
gameStateManager.PlaceChip(cellIndex);
gameStateManager.BumpOpponentChip(cellIndex);
gameStateManager.EndTurn();
```

---

## The Big Picture

```
You are here ↓
Sprint 1: Core Logic ✅ DONE
Sprint 2: State Machine ← YOU START NOW
Sprint 3: Game Modes (Nov 28)
Sprint 4: Board Visualization (Dec 5)
Sprint 5: UI Framework (Dec 12)
Sprint 6-8: Integration, Builds, QA
```

Sprint 2 is the **critical blocker** for all future work. Everything from Sprint 3 onward depends on GameStateManager being solid.

---

## Your Deliverables (This Sprint)

### 1. GamePhase Enum
**File**: `/Assets/Scripts/Core/GamePhase.cs`  
**Size**: ~15 lines  
**Time**: 30 minutes

```csharp
public enum GamePhase
{
    Setup,      // Game initialization
    Rolling,    // Player is rolling dice
    Placing,    // Player is placing chip on board
    Bumping,    // Player is deciding to bump (optional)
    EndTurn,    // Turn ending (apply rules, penalties, etc.)
    GameOver    // Someone won
}
```

### 2. GameStateManager Class
**File**: `/Assets/Scripts/Core/GameStateManager.cs`  
**Size**: 400-600 lines  
**Time**: 6-8 hours

This is the meat of Sprint 2. See SPRINT_2_EXECUTION_PLAN.md for full specification.

### 3. Integration Tests
**File**: `/Assets/Scripts/Tests/GameStateManagerTests.cs`  
**Size**: 300-400 lines  
**Count**: 22+ tests  
**Time**: 6-8 hours

Tests that verify:
- All phase transitions work
- Events fire at the right times
- Edge cases (6, 5+6, no moves, win) handled
- Validation gates work

### 4. Documentation
- All public methods documented (XML comments)
- Complex logic explained inline
- No TODOs without context

---

## Success Criteria (Must Hit All)

✅ GamePhase enum created  
✅ GameStateManager compiles with 0 errors  
✅ 22+ integration tests, all passing  
✅ ≥80% code coverage  
✅ 100% CODING_STANDARDS.md compliance  
✅ All public methods documented  
✅ Code review approved by ME

---

## Execution Plan (Day by Day)

### Day 1 (2 hours)
- Create GamePhase.cs enum
- Stub out GameStateManager.cs class skeleton
- Create test file structure
- Verify everything compiles
- **Checkpoint**: Framework in place, compiles cleanly

### Days 2-3 (6 hours)
- Implement `Initialize()` method (set up board, players, starting phase)
- Implement `StartGame()` method (transition to Rolling phase)
- Implement `RollDice()` method (roll, transition to Placing)
- Test these 3 methods thoroughly
- **Checkpoint**: Basic turn flow working

### Days 4-5 (8 hours)
- Implement `PlaceChip()` with validation
- Implement `BumpOpponentChip()` with validation
- Implement `EndTurn()` with next player logic
- Implement all event broadcasting
- Implement edge cases (6 = lose turn, 5+6 = skip all rolls, etc.)
- **Checkpoint**: Full turn flow working end-to-end

### Days 6-7 (6 hours)
- Write 22+ integration tests
- Debug any failures
- Verify code coverage ≥80%
- Clean up code, add documentation
- Prepare for code review
- **Checkpoint**: Ready for ME review

---

## Architecture Overview

```
GameStateManager (NEW - orchestrator)
    ├── BoardModel (Sprint 1)
    │   ├── Player, Chip, BoardCell
    │   └── Logic: 5-in-a-row, bumping, adjacency
    ├── TurnManager (Sprint 1)
    │   └── Logic: Player rotation
    ├── DiceManager (Sprint 1)
    │   └── Logic: Dice rolls, doubles, edge cases
    └── Game Modes (Sprint 3)
        └── Each mode extends GameStateManager rules
```

**Do NOT modify** Sprint 1 classes. GameStateManager orchestrates them without changing them.

---

## Key Methods to Implement

### Core Methods (MUST HAVE)

```csharp
// Setup
public void Initialize(Player player1, Player player2);
public void StartGame();

// Turn flow
public void RollDice();
public void PlaceChip(int cellIndex);
public void BumpOpponentChip(int cellIndex);
public void EndTurn();

// Queries
public bool CanPlaceChip(int cellIndex);
public bool CanBumpChip(int cellIndex);
public bool CanDeclareWin();

// State accessors
public GamePhase CurrentPhase { get; }
public Player CurrentPlayer { get; }
public int[] LastDiceRoll { get; }
```

### Event Firing (MUST HAVE)

```csharp
public event Action<GamePhase> OnPhaseChanged;      // When phase transitions
public event Action<int[]> OnDiceRolled;            // When dice roll completed
public event Action<Player> OnPlayerChanged;       // When turn switches
public event Action<Player> OnGameWon;             // When game ends
public event Action<string> OnInvalidAction;       // When bad move attempted
```

### Private Methods (Supporting)

```csharp
private void TransitionPhase(GamePhase newPhase);
private void CheckWinCondition();
private void ApplyEndTurnRules();
private void ValidateState(string context);
```

---

## Critical Logic to Handle

### Phase Transitions
```
Setup → Rolling (StartGame)
Rolling → Placing (after RollDice, unless 6 or 5+6)
Rolling → Rolling (if rolled 6, stay in Rolling)
Placing → Bumping (if valid bumps exist)
Placing → EndTurn (if no bumps or player chooses not to bump)
Bumping → EndTurn
EndTurn → Rolling (next player) or GameOver (if player won)
Any → GameOver (if player has 5 in a row)
```

### Edge Cases
```
Rolled 6: Stay in Rolling, lose turn effect handled in Sprint 3
Rolled 5+6: Skip all rolling, go directly to EndTurn
No valid moves: Skip Placing phase, go to Bumping
Player wins: Immediate GameOver regardless of current phase
Invalid move: Fire OnInvalidAction event, stay in current phase
```

---

## Testing Strategy

### What to Test
- ✅ Each phase transition
- ✅ Events fire with correct data
- ✅ Validation gates work
- ✅ Edge cases (6, 5+6, no moves, win detection)
- ✅ Multiple listeners receive events
- ✅ State queries return correct values
- ✅ Invalid actions rejected cleanly

### Test Names (Pattern: MethodName_Condition_Result)

```csharp
[Test] public void RollDice_TransitionsToPlacing();
[Test] public void RollDice_With6_StaysInRollingPhase();
[Test] public void PlaceChip_WithValidCell_Succeeds();
[Test] public void PlaceChip_WithOwnChip_Fails();
[Test] public void Check5InARow_DetectedAutomatically();
```

### Mock/Stubs

Use real Board/Turn/Dice classes (not mocks) - they're already tested in Sprint 1. GameStateManager just orchestrates them.

---

## Code Standards (Non-Negotiable)

Every line of code must follow **CODING_STANDARDS.md**:

- ✅ PascalCase for classes/methods
- ✅ camelCase for private fields
- ✅ UPPER_SNAKE_CASE for constants
- ✅ XML docs on all public methods
- ✅ Guard clauses, not nested ifs
- ✅ No magic numbers
- ✅ Null checks on entry
- ✅ No compiler warnings

Example:

```csharp
/// <summary>
/// Rolls two dice and transitions to Placing phase (if valid).
/// Handles edge case of 5+6 (safe) = skip all rolls.
/// </summary>
public void RollDice()
{
    if (CurrentPhase != GamePhase.Rolling)
        throw new InvalidOperationException("Can only roll during Rolling phase");
    
    int[] roll = diceManager.RollTwoDice();
    OnDiceRolled?.Invoke(roll);
    
    // Edge case: 5+6 is safe, skip to EndTurn
    if (diceManager.IsSafe5Plus6)
    {
        TransitionPhase(GamePhase.EndTurn);
        return;
    }
    
    // Normal case: go to Placing
    TransitionPhase(GamePhase.Placing);
}
```

---

## Support & Escalation

### If You Get Stuck
1. Check SPRINT_2_EXECUTION_PLAN.md (detailed spec)
2. Review Sprint 1 code (patterns to follow)
3. Check CODING_STANDARDS.md (style questions)
4. Escalate to ME immediately (don't wait)

### Blocker SLA
- Report blocker: ASAP (don't wait)
- ME response: Within 4 hours (weekday)
- Resolution: Clear path provided within 24 hours

---

## Submission Checklist (Before Code Review)

Before you mark Sprint 2 complete, verify:

- [ ] GamePhase.cs created, compiles
- [ ] GameStateManager.cs complete (all methods)
- [ ] 22+ tests written, all passing
- [ ] Code coverage ≥80%
- [ ] 0 compiler errors, 0 warnings
- [ ] All public methods documented (XML comments)
- [ ] No magic numbers (use constants)
- [ ] Guard clauses used (not nested ifs)
- [ ] All edge cases handled (6, 5+6, no moves, win)
- [ ] Follows CODING_STANDARDS.md 100%
- [ ] Git commits clean (one logical change per commit)

Once all checked: Create submission and notify ME for code review.

---

## What's Next (After Approval)

Once ME approves:

1. Code merged to main branch
2. Sprint 3 can begin (Game Modes)
3. Board/UI engineers can review GameStateManager
4. Full game structure in place

---

## Quick Links

- 📄 **SPRINT_2_EXECUTION_PLAN.md** – Full technical spec (22+ tests detailed)
- 📄 **CODING_STANDARDS.md** – Code style enforced
- 📄 **PROJECT_QUALITY_GATES.md** – Code review checklist
- 📄 **ARCHITECTURE.md** – System design reference
- 🔗 **GitHub**: https://github.com/CjDaOne/bumpu5

---

## Timeline at a Glance

| Day | Focus | Deliverable |
|-----|-------|------------|
| 1 | Setup & skeleton | GamePhase enum + framework compiles |
| 2-3 | Core methods | RollDice, PlaceChip, EndTurn working |
| 4-5 | Complete logic | All phases, events, edge cases |
| 6-7 | Testing & polish | 22+ tests passing, ready for review |
| 8 | Code review | ME approves, merge to main |

---

## You've Got This

Sprint 1 was solid. You have all the building blocks. Sprint 2 is orchestration – bringing it all together.

This is the **critical path** for the project. Everything downstream depends on GameStateManager being rock-solid.

The execution plan is detailed. The code patterns are set. The standards are clear.

Execute with confidence. Ask for help early if you need it.

---

**Sprint 2 Start**: Immediately (Nov 14, 2025)  
**Target Completion**: Nov 21, 2025  
**Owner**: Gameplay Engineer  
**Managing Engineer**: Amp  
**Status**: 🟢 READY TO GO

