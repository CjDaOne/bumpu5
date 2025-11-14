# SPRINT 3 BRIEFING: Gameplay Engineering Team

**TO**: Gameplay Engineer Agent  
**FROM**: Managing Engineer  
**DATE**: Nov 14, 2025  
**SPRINT**: Week 3 (Nov 28 - Dec 5, 2025)  
**PRIORITY**: CRITICAL

---

## Mission

Implement **all 5 game modes** as IGameMode classes with complete logic, scoring, and unit tests. Each mode is independently testable and integrates with GameStateManager via polymorphism.

By end of sprint: **All 5 modes playable, fully tested, production-ready.**

---

## What You're Building

| Mode | Name | Win Condition | Special Feature |
|------|------|---------------|-----------------|
| Game1 | Bump 5 | 5 in a row | Standard bumping, doubles roll again |
| Game2 | Krazy 6 | 5 in a row | Double-6 = -50 pts, lose turn |
| Game3 | Pass the Chip | Move special chip 5 times | Shared chip, +10 pts for special moves |
| Game4 | Bump U & 5 | 5 in a row OR 200 pts | 2 chips/turn, forced bumping, double-5 = -25 |
| Game5 | Solitary | 5 in a row (1 player) | No bumping, turn bonuses |

---

## Your Responsibilities

### Primary Deliverables

1. **Five Game Mode Classes** (one per file)
   - Each implements `IGameMode` interface
   - All public methods documented (XML)
   - All constants defined (no magic numbers)
   - Follows CODING_STANDARDS.md

2. **GameModeFactory Class**
   - Static factory for creating modes by ID or name
   - GetAllModes() for UI mode selection
   - Proper error handling

3. **40+ Unit Tests** (8-10 per mode)
   - All tests passing
   - NUnit framework
   - Edge cases covered
   - 80%+ code coverage target

### Secondary Deliverables

- Rules text for each mode (GetRulesText() method)
- Integration tests with GameStateManager
- Documentation of mode logic and state transitions

---

## Technical Specification

### IGameMode Interface (Already Created in Sprint 2)

You'll implement these methods for each mode:

```csharp
/// <summary>
/// Name of the game mode displayed in UI
/// </summary>
public string ModeName { get; }

/// <summary>
/// Unique ID for factory/serialization
/// </summary>
public int ModeID { get; }

/// <summary>
/// Determines if the specified player has won
/// </summary>
public bool CheckWin(Player player, BoardModel board);

/// <summary>
/// Whether to roll dice again (doubles, bonuses, etc.)
/// </summary>
public bool CanRollAgain(int[] diceRoll);

/// <summary>
/// Whether bumping is allowed in this situation
/// </summary>
public bool IsBumpAllowed(Player attacker, int targetCell, BoardModel board);

/// <summary>
/// Returns list of valid move destinations for this turn
/// </summary>
public List<int> GetValidMoves(Player player, int[] diceRoll, BoardModel board);

/// <summary>
/// Called when a chip moves (for scoring, tracking, etc.)
/// </summary>
public void OnChipMoved(Player player, int fromCell, int toCell);

/// <summary>
/// Called when dice are rolled (for penalties, bonuses, etc.)
/// </summary>
public void OnDiceRolled(Player player, int[] diceRoll);

/// <summary>
/// Complete rules text for this mode (for help/instructions)
/// </summary>
public string GetRulesText();
```

### Per-Mode Implementation Details

See `SPRINT_3_KICKOFF.md` for specific implementations for each mode.

---

## Code Requirements

### Naming & Structure
- File: `GameX_ModeName.cs` (e.g., `Game1_Bump5.cs`)
- Class: `public class GameX_ModeName : IGameMode`
- Location: `/Assets/Scripts/GameModes/`

### Documentation
- XML summary on every public method
- Inline comments for complex logic
- No code without purpose

### Constants (UPPER_SNAKE_CASE)
```csharp
private const int WIN_THRESHOLD = 5;
private const int BUMP_POINTS = 10;
private const int DOUBLE_6_PENALTY = -50;
```

### Testing
- Test file: `/Assets/Scripts/Tests/GameX_ModeName Tests.cs`
- Naming: `MethodName_Condition_ExpectedResult`
- Mock Player and BoardModel as needed
- Use [SetUp] for test initialization

---

## Acceptance Criteria

✅ **Must Have**
- All 5 modes implemented and working
- 40+ tests passing (8-10 per mode)
- No code style violations
- All public methods documented
- Zero magic numbers
- Factory working for all modes

✅ **Should Have**
- 80%+ code coverage
- Edge case tests
- Integration tests with GameStateManager
- Clear rules text

⚠️ **Nice to Have**
- Performance optimizations
- Additional test coverage
- Refactoring for clarity

---

## Sprint Milestones

| Date | Milestone | Status |
|------|-----------|--------|
| Nov 28 | Kickoff + Game1 skeleton | 🎯 |
| Nov 29 | Game1 complete + Game2 start | 🎯 |
| Nov 30 | Game2 complete + Game3 start | 🎯 |
| Dec 1 | Game3 complete + Game4 start | 🎯 |
| Dec 2 | Game4 complete + Game5 start | 🎯 |
| Dec 3 | Game5 + Factory complete | 🎯 |
| Dec 4 | All tests passing, code review | 🎯 |
| Dec 5 | Final review + sign-off | ✅ |

---

## How You'll Know You're Done

1. **All 5 mode classes exist** and compile without errors
2. **Every mode implements IGameMode** with all required methods
3. **40+ unit tests pass** (0 failures)
4. **Code review passed** (Managing Engineer sign-off)
5. **No console warnings/errors** during testing
6. **Each mode integrates** with existing GameStateManager (no breaking changes)
7. **Documentation complete**: Rules text, method summaries, etc.

---

## Key Rules & Guidelines

### Game Rules (From SPRINT_3_KICKOFF.md)

**Game1_Bump5**:
- Win: 5 in a row
- Scoring: Bump +10, Win +100
- Doubles roll again
- 5+6 is "safe"

**Game2_Krazy6**:
- Win: 5 in a row
- Double-6: -50 pts, lose turn
- Other doubles: roll again

**Game3_PassTheChip**:
- Win: move special chip 5 times
- Special chip move: +10 pts
- Regular move: +5 pts
- Special chip can't be bumped

**Game4_BumpUAnd5**:
- Win: 5 in a row OR 200+ points
- Move 2 chips per turn (or 1)
- Forced bumping (if able, must bump)
- Double-5: -25 pts, no roll again

**Game5_Solitary**:
- Single player vs. board
- Win: 5 in a row
- No bumping (opponent chips are obstacles)
- Turn bonuses: <20 turns +500, 20-30 +250

### Code Style (From CODING_STANDARDS.md)
- Follow all naming conventions strictly
- Document public methods with XML
- Use guard clauses (early returns)
- Avoid magic numbers
- No deeply nested conditionals

---

## Dependencies & Blockers

### Assumes Complete (Sprint 1 & 2)
- [x] IGameMode interface exists
- [x] Player class with scoring
- [x] BoardModel with basic methods
- [x] GameStateManager can call mode methods

### Will Use From Sprint 2
```csharp
// These should already exist:
BoardModel.Check5InARow(player)  // For win detection
BoardModel.CanBump(player, cell)  // For bump validation
Player.AddScore(points)
Player.RemoveScore(points)
```

### If Something is Missing
1. Report to Managing Engineer immediately
2. Provide: what's missing, why you need it, impact
3. I'll coordinate with other teams or create it

---

## Communication

### Report Status Daily (Async)
```
[Sprint 3] Daily Update - Nov 28

✅ Completed:
- Game1_Bump5 skeleton created
- All methods stubbed out

🔄 In Progress:
- Implementing CheckWin() and CanRollAgain()

🚫 Blockers:
- None
```

### Escalation
1. Hit a blocker? Report immediately
2. Unsure about a rule? Ask in this thread
3. Code review feedback? Acknowledge and incorporate

### Review Requests
- Tag @Managing Engineer in your response when ready
- Include: scope, test results, coverage %

---

## Testing Strategy

### Unit Test Approach
1. **Create mocks** for Player and BoardModel
2. **Setup test fixture** in [SetUp] method
3. **Test each mode method** independently
4. **Edge cases**: doubles, penalties, boundaries
5. **Run full test suite** before final submission

### Example Test (Game1_Bump5)
```csharp
[Test]
public void CheckWin_With5InARow_ReturnsTrue()
{
    // Arrange
    var mode = new Game1_Bump5();
    var mockBoard = MockBoardWith5InARow(player);
    
    // Act
    bool result = mode.CheckWin(player, mockBoard);
    
    // Assert
    Assert.IsTrue(result);
}
```

### Running Tests
```bash
# In Unity Editor
Window → General → Test Runner
Click "Run All"

# Or from command line
unity -runTests -testCategory="Game1"
```

---

## Success Looks Like

By Dec 5 at EOD:

- ✅ All 5 mode files created and committed
- ✅ GameModeFactory complete and tested
- ✅ 40+ tests passing (0 failures)
- ✅ Code review approved
- ✅ Zero console errors/warnings
- ✅ Each mode playable through GameStateManager
- ✅ Documentation complete (rules text + method docs)
- ✅ Ready for Sprint 4 (Board visualization)

---

## Resources

📄 **SPRINT_3_KICKOFF.md** - Full game mode specifications  
📄 **SPRINT_3_TASK_ALLOCATION.md** - Detailed task breakdown  
📄 **CODING_STANDARDS.md** - Code style requirements  
📄 **SUBAGENT_TEAMS.md** - Team structure & responsibilities  
📄 **PROJECT_PLAN.md** - Overall project timeline  

---

## Final Notes

- **You own this sprint.** Full autonomy within scope.
- **Communication is key.** Daily updates, blockers immediately.
- **Quality matters.** This logic is the foundation for all future sprints.
- **Testing isn't optional.** 40+ tests, 80%+ coverage is non-negotiable.
- **I'm here to support.** Blockers, questions, clarifications—ask.

**Let's ship Sprint 3 strong.**

---

**Briefing Issued**: Nov 14, 2025  
**Sprint Start**: Nov 28, 2025  
**Expected Completion**: Dec 5, 2025  
**Managing Engineer**: AI Engineering Manager

---

## Sign-Off

**Gameplay Engineer Agent**: Please acknowledge receipt and readiness.

```
☐ Reviewed SPRINT_3_KICKOFF.md
☐ Reviewed SPRINT_3_TASK_ALLOCATION.md
☐ Reviewed CODING_STANDARDS.md
☐ Understand all 5 mode specifications
☐ Ready to begin Nov 28
☐ Confirm no blockers/questions

Target Start Date: Nov 28, 2025
Estimated Completion: Dec 5, 2025
```
