# Sprint 2 Quick Start Guide
**For**: Gameplay Engineer Agent  
**From**: Managing Engineer  
**Date**: Nov 14, 2025 (Prep for Nov 21 kickoff)

---

## TL;DR - What You're Building

You're building the "nervous system" of the game: a **GameStateManager** that orchestrates the entire turn flow from start to finish.

**5 classes** + **3 test files** + **22+ tests**

Think of it like a traffic controller:
- Rolling phase → Placing phase → Bumping phase → End turn
- Each phase validates actions before allowing them
- Events fire so UI can react (lights up buttons, shows animations)
- Game modes inject custom rules

---

## The 5 Classes You'll Create

### 1. GamePhase.cs (20 lines)
Just an enum with 9 phases:
```csharp
Rolling → DiceResult → Placing → BumpDecision → Bumping → EndTurn
                          ↓
                      WinScreen → GameOver
```

### 2. GameState.cs (60 lines)
A data class that snapshots the entire game state:
- Current phase, current player, last dice roll
- Turn number, can roll again?, last moved chip
- Serialize/deserialize stubs

### 3. GameStateManager.cs (~300 lines) ⭐
The main orchestrator. Methods like:
- `RollDice()` - validates and executes roll
- `MoveChip()` - validates and executes move
- `DeclareBump()` - validates and executes bump
- `EndTurn()` - wraps up turn, checks for roll-again
- Events: `OnPhaseChanged`, `OnDiceRolled`, `OnChipMoved`, `OnGameWon`, etc.

### 4. TurnPhaseController.cs (~150 lines)
Lighter class that delegates to GameStateManager. Methods like:
- `StartRollingPhase()` - setup for roll
- `ExecuteMove()` - calls GameStateManager.MoveChip()
- `AttemptBump()` - calls GameStateManager.DeclareBump()

### 5. TurnManager.cs (UPDATED)
Add ~80 lines to existing class:
- `turnsCompleted` - track turn count
- `consecutiveDoublesCount` - detect triple-double
- `StartNewTurn()` / `CompleteTurn()` - lifecycle methods
- `RecordDouble()` / `ResetDoubleCount()` - double tracking

---

## Key Design Principles

### 1. Validate Everything
```csharp
public void RollDice()
{
    // ALWAYS validate first
    if (gameState.CurrentPhase != GamePhase.Rolling)
        throw new InvalidOperationException("Not in Rolling phase");
    
    // Then execute
    int[] roll = diceManager.RollTwoDice();
    gameState.LastDiceRoll = roll;
    
    // Then fire events
    OnDiceRolled?.Invoke(roll);
    TransitionToPhase(GamePhase.Placing);
}
```

### 2. Fire Events Everywhere
UI will listen to events and react:
```csharp
OnPhaseChanged?.Invoke(GamePhase.Placing);  // UI highlights valid cells
OnDiceRolled?.Invoke(roll);                 // UI animates dice
OnChipMoved?.Invoke(fromCell, toCell);      // UI moves chip sprite
OnPlayerChanged?.Invoke(nextPlayer);        // UI shows current player
```

### 3. Delegate to Game Modes
Don't hardcode win conditions. Ask the mode:
```csharp
if (currentGameMode.CheckWin(currentPlayer, board))
{
    TransitionToPhase(GamePhase.WinScreen);
    OnGameWon?.Invoke(currentPlayer);
}
```

### 4. Use GamePhase Enum Everywhere
No magic strings or numbers:
```csharp
// ✅ GOOD
if (gameState.CurrentPhase == GamePhase.Rolling) { }

// ❌ BAD
if (gameState.CurrentPhase == 0) { }
if (phaseName == "rolling") { }
```

---

## The Flow (State Machine)

```
[Setup] (game initializes)
   ↓
[Rolling] ← Player clicks "Roll Dice" button
   ↓
[DiceResult] ← Dice animation plays
   ↓
[Placing] ← Valid cells highlight, player taps one
   ↓
   Can bump? ─→ Yes → [BumpDecision] ← Player chooses to bump or not
   ↓              ↓
   No          [Bumping] ← Chip removed, points awarded
   ↓              ↓
   └─────────→ [EndTurn] ← Check for double, mode rules
                  ↓
              Can roll again? ← Doubles? Mode rule?
              ↓
              Yes → [Rolling] (same player rolls again)
              No → [Rolling] (next player's turn)
```

---

## Testing Requirements

### GameStateManagerTests.cs (10+ tests)
```
✓ InitializeGame_SetsupPlayers_ReturnsTrue
✓ RollDice_TransitionsToPlacingPhase
✓ MoveChip_WithValidMove_TransitionsCorrectly
✓ MoveChip_WithInvalidMove_FiresErrorEvent
✓ DeclareBump_RemovesChip_AwardsPoints
✓ EndTurn_RotatesToNextPlayer
✓ EndTurn_WithDouble_AllowsRollAgain
✓ HasWon_ChecksGameModeWinCondition
✓ InvalidAction_InWrongPhase_FiresErrorEvent
✓ AllEventsAreFired_WhenActionsExecute
```

### TurnPhaseControllerTests.cs (7+ tests)
```
✓ StartRollingPhase_SetsCurrentPhase
✓ ExecuteMove_WithValidMove_ReturnsTrue
✓ ExecuteMove_WithInvalidMove_ReturnsFalse
✓ AttemptBump_RemovesChip
✓ AttemptBump_AwardsPoints
✓ CompleteTurn_RotatesPlayer
✓ StartBumpDecisionPhase_HighlightsBumpableCells
```

### TurnManagerEnhancedTests.cs (5+ tests)
```
✓ StartNewTurn_ResetsState
✓ CompleteTurn_IncrementsTurnCounter
✓ RecordDouble_IncreasesDoubleCount
✓ RecordDouble_TripleDouble_LosesTurn
✓ ResetDoubleCount_ClearsCount
```

**Total**: 22+ tests

---

## Edge Cases to Handle

### ✅ Doubles Logic
- Any matching pair → can roll again
- Need to track count (3 doubles = lose turn)
- 5+6 is NOT a double, but it's "safe" (mode handles)

### ✅ Bumping Rules
- Can only bump opponent's chips
- Must be adjacent
- Must have just moved
- Can't bump own chips

### ✅ Win Detection
- Happens AFTER every move
- Happens AFTER every bump
- Uses IGameMode.CheckWin() (not hardcoded)
- Game 4 has score threshold (not just 5-in-a-row)

### ✅ Invalid Actions
- Rolling in wrong phase → fire OnInvalidActionAttempted
- Moving from empty cell → error
- Bumping empty cell → error
- Declaring win when no win → error

---

## What NOT to Do

❌ Don't modify Sprint 1 classes  
❌ Don't hardcode win conditions  
❌ Don't use magic numbers (use GamePhase enum)  
❌ Don't forget to validate before executing  
❌ Don't forget to fire events  
❌ Don't skip edge case tests  

---

## Checklist Before Marking Sprint 2 Done

- [ ] GamePhase.cs created (9 phases)
- [ ] GameState.cs created (all properties)
- [ ] GameStateManager.cs created (300+ lines, all methods documented)
- [ ] TurnPhaseController.cs created (delegates to GameStateManager)
- [ ] TurnManager.cs enhanced (add turn tracking)
- [ ] All 22+ unit tests created
- [ ] All 22+ tests passing
- [ ] No console errors/warnings
- [ ] All methods have /// comments
- [ ] Events properly declared and documented
- [ ] Integration with Sprint 1 verified
- [ ] Code review completed (Managing Engineer)

---

## Key Files to Read

1. **SPRINT_2_BRIEFING.md** ← Detailed requirements (you are here)
2. **SPRINT_2_KICKOFF.md** ← Overview and architecture
3. **CODING_STANDARDS.md** ← How to format code
4. **ARCHITECTURE.md** ← System design
5. **SPRINT_1_REVIEW.md** ← What was approved in Sprint 1

---

## Code Template (DRY Setup)

Here's the general structure for GameStateManager:

```csharp
public class GameStateManager
{
    // Fields
    private GameState gameState;
    private TurnManager turnManager;
    private DiceManager diceManager;
    private BoardModel board;
    private IGameMode currentGameMode;
    
    // Events
    public event System.Action<GamePhase> OnPhaseChanged;
    public event System.Action<int[]> OnDiceRolled;
    // ... more events ...
    
    // Action: Validate → Execute → Fire Events → Transition
    public void RollDice()
    {
        ValidatePhaseForAction(GamePhase.Rolling);  // Step 1
        int[] roll = diceManager.RollTwoDice();     // Step 2
        gameState.LastDiceRoll = roll;              // Step 3
        OnDiceRolled?.Invoke(roll);                 // Step 4
        TransitionToPhase(GamePhase.DiceResult);    // Step 5
    }
    
    // Helper: Centralized phase validation
    private void ValidatePhaseForAction(GamePhase requiredPhase)
    {
        if (gameState.CurrentPhase != requiredPhase)
        {
            string error = $"Action requires {requiredPhase} phase, " +
                          $"currently in {gameState.CurrentPhase}";
            OnInvalidActionAttempted?.Invoke(error);
            throw new InvalidOperationException(error);
        }
    }
    
    // Helper: Centralized phase transition
    private void TransitionToPhase(GamePhase nextPhase)
    {
        gameState.CurrentPhase = nextPhase;
        OnPhaseChanged?.Invoke(nextPhase);
    }
}
```

---

## Meeting Prep

For the Nov 21 kickoff meeting:

1. **Read** SPRINT_2_BRIEFING.md (this document)
2. **Review** state machine diagram (above)
3. **Understand** the 5 classes at a high level
4. **Note** any questions about requirements
5. **Prepare** to discuss testing strategy

---

## Success Metrics

By end of Sprint 2 (Nov 28):

- ✅ 22+ tests passing
- ✅ GameStateManager fully functional
- ✅ Turn flow works end-to-end
- ✅ Can play a complete game from start to finish
- ✅ Code approved by Managing Engineer

---

**Good luck!** This is the critical system that ties everything together. Quality work here makes Sprints 3+ much easier.

---

**Prepared By**: Managing Engineer Agent  
**Date**: Nov 14, 2025  
**Kickoff**: Nov 21, 2025 (7 days away)
