# Sprint 2 Quick Reference Card
**For**: Gameplay Engineer Agent  
**Use During**: Implementation (Nov 21-28)

---

## The 5 Classes At a Glance

### 1️⃣ GamePhase.cs
**What**: Enum with 9 phases  
**Lines**: ~20  
**Complexity**: Low

```csharp
public enum GamePhase
{
    Setup, Rolling, DiceResult, Placing, 
    BumpDecision, Bumping, EndTurn, WinScreen, GameOver
}
```

---

### 2️⃣ GameState.cs
**What**: Game state snapshot  
**Lines**: ~60  
**Complexity**: Low

**Properties needed**:
- `int GameModeID`, `string GameModeName`
- `Player[] Players`, `BoardModel Board`
- `GamePhase CurrentPhase`, `int CurrentPlayerIndex`, `int[] LastDiceRoll`
- `int TurnNumber`, `bool CanRollAgain`, `bool MustBump`
- `int? PendingBumpCell`, `Chip LastMovedChip`
- `Reset()`, `Serialize()`, `Deserialize(json)` methods

---

### 3️⃣ GameStateManager.cs ⭐
**What**: Core orchestrator  
**Lines**: ~300+  
**Complexity**: HIGH

**Key Pattern**: Validate → Execute → Fire Events → Transition

**Public Methods**:
- `void InitializeGame(IGameMode, Player[])`
- `void RollDice()`
- `void MoveChip(int, int)`
- `void DeclareBump(int)`
- `void EndTurn()`
- `void DeclareWin()`
- `GamePhase GetCurrentPhase()`
- `Player GetCurrentPlayer()`
- `bool IsValidMove(int, int)`
- `bool CanBumpTarget(int)`
- `bool HasWon(Player)`
- `List<int> GetValidMoves(int)`

**Events** (must declare):
- `event Action<GamePhase> OnPhaseChanged`
- `event Action<Player> OnPlayerChanged`
- `event Action<int[]> OnDiceRolled`
- `event Action<int, int> OnChipMoved`
- `event Action<Player> OnGameWon`
- `event Action<string> OnInvalidActionAttempted`

**Helper Methods**:
- `private void ValidatePhaseForAction(GamePhase)`
- `private void TransitionToPhase(GamePhase)`
- `private void CheckWinCondition()`

---

### 4️⃣ TurnPhaseController.cs
**What**: Phase coordinator (delegates to GameStateManager)  
**Lines**: ~150  
**Complexity**: Medium

**Public Methods**:
- `void StartRollingPhase(Player)`
- `int[] CompleteRoll(int[])`
- `void StartPlacingPhase()`
- `bool ExecuteMove(int, int)` ← calls GameStateManager.MoveChip()
- `void CompleteMove()`
- `void StartBumpDecisionPhase()`
- `bool AttemptBump(int)` ← calls GameStateManager.DeclareBump()
- `void SkipBump()`
- `void CompleteTurn()` ← calls GameStateManager.EndTurn()
- `Player GetNextPlayer()`

---

### 5️⃣ TurnManager.cs (Enhancement)
**What**: Add turn tracking to existing class  
**Lines**: Add ~80 lines  
**Complexity**: Low

**New Fields**:
```csharp
private int turnsCompleted;
private bool canRollAgain;
private bool justBumped;
private int consecutiveDoublesCount;
```

**New Methods**:
- `void StartNewTurn()` - reset turn state
- `void CompleteTurn()` - increment counter, advance player
- `void RecordDouble()` - track doubles, handle triple-double
- `void ResetDoubleCount()` - clear double count
- `void RecordBump()` - mark bump occurred

---

## Critical Edge Cases

### ✅ Doubles
```
Rolling matching pair → CanRollAgain = true
3+ consecutive doubles → Lose turn immediately
```

### ✅ Bumping
```
Only opponent chips (not own)
Only adjacent cells
Only after just moving
Not off-board
```

### ✅ Winning
```
Check AFTER every move
Check AFTER every bump
Use IGameMode.CheckWin() (delegate)
Not hardcoded to 5-in-a-row
```

### ✅ Invalid Actions
```
Wrong phase → OnInvalidActionAttempted
Empty cell → OnInvalidActionAttempted
Non-adjacent move → OnInvalidActionAttempted
Own chip bump → OnInvalidActionAttempted
```

---

## Testing Checklist

### GameStateManagerTests.cs (10+ tests)
- [ ] InitializeGame_SetsupPlayers_ReturnsTrue
- [ ] RollDice_TransitionsToPlacingPhase
- [ ] MoveChip_WithValidMove_TransitionsCorrectly
- [ ] MoveChip_WithInvalidMove_FiresErrorEvent
- [ ] DeclareBump_RemovesChip_AwardsPoints
- [ ] DeclareBump_WithInvalidTarget_Fails
- [ ] EndTurn_RotatesToNextPlayer
- [ ] EndTurn_WithDouble_AllowsRollAgain
- [ ] HasWon_ChecksGameModeWinCondition
- [ ] InvalidAction_InWrongPhase_FiresErrorEvent

### TurnPhaseControllerTests.cs (7+ tests)
- [ ] StartRollingPhase_SetsCurrentPhase
- [ ] CompleteRoll_TransitionsToPlacing
- [ ] ExecuteMove_WithValidMove_ReturnsTrue
- [ ] ExecuteMove_WithInvalidMove_ReturnsFalse
- [ ] StartBumpDecisionPhase_HighlightsBumpableCells
- [ ] AttemptBump_RemovesChip
- [ ] CompleteTurn_RotatesPlayer

### TurnManagerEnhancedTests.cs (5+ tests)
- [ ] StartNewTurn_ResetsState
- [ ] CompleteTurn_IncrementsTurnCounter
- [ ] RecordDouble_IncreasesDoubleCount
- [ ] RecordDouble_TripleDouble_LosesTurn
- [ ] ResetDoubleCount_ClearsCount

---

## Code Standards Checklist

### Before Submitting Each Class
- [ ] PascalCase class names
- [ ] camelCase private fields
- [ ] PascalCase public properties
- [ ] All public methods have `/// <summary>` docs
- [ ] All parameters documented with `/// <param>`
- [ ] All return types documented with `/// <returns>`
- [ ] No magic numbers (use constants or enums)
- [ ] No circular dependencies

### Before Final Submission
- [ ] All 5 classes created
- [ ] All 22+ tests passing
- [ ] No console errors/warnings
- [ ] Integration with Sprint 1 verified
- [ ] Events properly declared and firing
- [ ] Edge cases tested
- [ ] Ready for code review

---

## Daily Standup Template

Copy/paste each day:

```
✅ Completed since last standup:
- [List completed items]

🔄 In progress:
- [List current work]

🚫 Blockers:
- [Any blockers or None]

Notes:
- [Any other notes]
```

---

## Quick Command Reference

### Run All Tests
```
Window → TextTest Runner → Run All Tests
```

### Expected Results
```
79 tests total (57 Sprint 1 + 22 Sprint 2)
100% pass rate required
```

### Push to GitHub
```bash
git add Assets/Scripts/
git commit -m "[Sprint 2] Description of what was added/fixed"
git push origin main
```

---

## Key Files to Reference

| File | Purpose | When to Read |
|------|---------|--------------|
| SPRINT_2_BRIEFING.md | Detailed requirements | During implementation |
| SPRINT_2_LAUNCH.md | Team briefing | At kickoff |
| CODING_STANDARDS.md | Code formatting | Before submitting code |
| SPRINT_1_REVIEW.md | What was approved | Reference for patterns |
| SPRINT_2_KICKOFF.md | Architecture | When designing |

---

## Success = Done

When you can answer YES to all:
- ✅ All 5 classes created?
- ✅ All 22+ tests passing?
- ✅ No console errors?
- ✅ CODING_STANDARDS.md compliant?
- ✅ Integration with Sprint 1 verified?
- ✅ Code ready for review?

**If YES to all** → Sprint 2 complete!

---

## Escalation Path

**If stuck**:
1. Review error message carefully
2. Check SPRINT_2_BRIEFING.md for specification
3. Check SPRINT_2_LAUNCH.md for edge cases
4. Report in daily standup with specific issue
5. Managing Engineer (Amp) will help resolve

---

## Timeline

| Date | What |
|------|------|
| Nov 21 | Kickoff, read docs |
| Nov 22-23 | GamePhase, GameState |
| Nov 24-25 | GameStateManager (core) |
| Nov 26 | TurnPhaseController, TurnManager |
| Nov 27 | Testing & debugging |
| Nov 28 | Final review & approval |

---

**Last Updated**: Nov 14, 2025  
**Use During**: Sprint 2 (Nov 21-28, 2025)  
**Print This Out!** It's your quick reference during implementation.
