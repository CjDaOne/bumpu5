# Sprint 3 Task Allocation & Execution Plan

**Sprint Duration**: Week 3 (Nov 28 - Dec 5, 2025)  
**Managing Engineer**: AI Engineering Manager  
**Sprint Lead**: Gameplay Engineer Agent  
**Status**: ACTIVE BRIEFING

---

## Executive Summary

Sprint 3 focuses exclusively on **implementing all 5 game modes** with complete logic, scoring, and win conditions. This is a single-team sprint (Gameplay Team) with clear deliverables and measurable success criteria.

All modes must:
- Implement the `IGameMode` interface
- Include comprehensive unit tests (40+ total)
- Follow CODING_STANDARDS.md
- Integrate seamlessly with GameStateManager (from Sprint 2)

---

## Team Assignment: Gameplay Engineering Team

**Lead**: Gameplay Engineer Agent  
**Team Members**: Gameplay Engineer Agent + QA support  
**Scope**: All 5 game mode implementations + factory + tests  
**Dependencies**: Sprint 1 & 2 complete and approved

---

## Task Breakdown

### Task 1: IGameMode Interface Verification
**Assignee**: Gameplay Engineer Agent  
**Estimated Hours**: 2 hours  
**Success Criteria**:
- [ ] IGameMode interface exists and is properly documented
- [ ] All required methods are defined
- [ ] Interface supports polymorphic access from GameStateManager

**Checklist**:
- Verify interface location: `/Assets/Scripts/GameModes/IGameMode.cs`
- Document all methods with XML summaries
- Add examples of usage

**Dependencies**: None (created in Sprint 2 prep)

---

### Task 2: Game1_Bump5 Implementation
**Assignee**: Gameplay Engineer Agent  
**Estimated Hours**: 6 hours  
**File**: `/Assets/Scripts/GameModes/Game1_Bump5.cs`

**Deliverables**:
- [x] Class skeleton created
- [ ] All IGameMode methods implemented
- [ ] Scoring system working (Bump: +10, Win: +100)
- [ ] Win condition: 5 in a row
- [ ] Roll-again logic: doubles only
- [ ] Bump permission: standard allow
- [ ] Safe roll handling: 5+6 marked

**Methods to Implement**:
```csharp
public string ModeName { get; }
public int ModeID { get; }
public bool CheckWin(Player player, BoardModel board)
public bool CanRollAgain(int[] diceRoll)
public bool IsBumpAllowed(Player attacker, int targetCell, BoardModel board)
public List<int> GetValidMoves(Player player, int[] diceRoll, BoardModel board)
public void OnChipMoved(Player player, int fromCell, int toCell)
public void OnDiceRolled(Player player, int[] diceRoll)
public string GetRulesText()
```

**Tests Required** (8-10 tests):
- CheckWin_With5InARow_ReturnsTrue()
- CheckWin_With4InARow_ReturnsFalse()
- CanRollAgain_WithDouble_ReturnsTrue()
- CanRollAgain_WithSingleRoll_ReturnsFalse()
- IsBumpAllowed_WithOpponentChip_ReturnsTrue()
- IsBumpAllowed_WithOwnChip_ReturnsFalse()
- GetValidMoves_ReturnsAdjacentCells()
- GetRulesText_ReturnsNonEmpty()

---

### Task 3: Game2_Krazy6 Implementation
**Assignee**: Gameplay Engineer Agent  
**Estimated Hours**: 7 hours  
**File**: `/Assets/Scripts/GameModes/Game2_Krazy6.cs`

**Deliverables**:
- [ ] All IGameMode methods implemented
- [ ] Double-6 penalty: -50 points, lose turn
- [ ] Win condition: 5 in a row (same as Bump5)
- [ ] Scoring: Bump: +10, Win: +100, Double-6: -50
- [ ] Roll-again: doubles except double-6

**Special Logic**:
- On dice roll with 6+6: Subtract 50 from player score
- Do NOT roll again on double-6 (unlike other doubles)

**Tests Required** (8-10 tests):
- OnDiceRolled_With6And6_Deducts50Points()
- OnDiceRolled_With6And6_ScoreNeverBelowZero()
- CanRollAgain_With6And6_ReturnsFalse()
- CanRollAgain_WithOtherDouble_ReturnsTrue()
- CheckWin_With5InARow_ReturnsTrue()
- IsBumpAllowed_ReturnsTrue()
- GetRulesText_MentionsDouble6Penalty()

---

### Task 4: Game3_PassTheChip Implementation
**Assignee**: Gameplay Engineer Agent  
**Estimated Hours**: 8 hours  
**File**: `/Assets/Scripts/GameModes/Game3_PassTheChip.cs`

**Deliverables**:
- [ ] Special chip tracking system
- [ ] Chip ownership tracking
- [ ] Win condition: moved special chip 5 times
- [ ] Scoring: Regular move: +5, Special chip: +10, Hold bonus: +1/turn
- [ ] Cannot bump special chip (or bumping resets possession)
- [ ] Roll-again: doubles (standard)

**Special State Management**:
- Track which chip is "special" (designate one during init)
- Track how many times current player has moved special chip
- Track chip owner (who currently possesses it)
- Reset count when chip changes hands

**Tests Required** (8-10 tests):
- OnChipMoved_SpecialChip_Awards10Points()
- OnChipMoved_RegularChip_Awards5Points()
- CheckWin_With5SpecialChipMoves_ReturnsTrue()
- CheckWin_With4SpecialChipMoves_ReturnsFalse()
- SpecialChip_CantBeBumped_ReturnsFalse()
- GetRulesText_ExplainsSpecialChip()

---

### Task 5: Game4_BumpUAnd5 Implementation
**Assignee**: Gameplay Engineer Agent  
**Estimated Hours**: 9 hours  
**File**: `/Assets/Scripts/GameModes/Game4_BumpUAnd5.cs`

**Deliverables**:
- [ ] Win condition: 5 in a row OR 200+ points
- [ ] Move 2 chips per turn (or 1 if available)
- [ ] Forced bumping: Must bump if able
- [ ] Scoring: Move: +5, Bump: +15, Double-5: -25
- [ ] Double-5: -25 points, NO roll again (unique rule)

**Special Logic**:
- Track chips moved this turn (max 2)
- When player can bump, bump is mandatory
- Double-5 rolls: deduct 25 points, don't roll again
- Allow selection of which chip to move

**Tests Required** (8-10 tests):
- CheckWin_With5InARow_ReturnsTrue()
- CheckWin_With200PlusScore_ReturnsTrue()
- CheckWin_With4InARowAnd150Score_ReturnsFalse()
- OnDiceRolled_With5And5_Deducts25()
- CanRollAgain_With5And5_ReturnsFalse()
- IsBumpAllowed_WhenCanBump_ReturnsTrue()
- GetChipsMovedThisTurn_TracksCorrectly()

---

### Task 6: Game5_Solitary Implementation
**Assignee**: Gameplay Engineer Agent  
**Estimated Hours**: 8 hours  
**File**: `/Assets/Scripts/GameModes/Game5_Solitary.cs`

**Deliverables**:
- [ ] Single player vs. board
- [ ] Pre-placed opponent chips (fixed, can't be bumped)
- [ ] Win condition: 5 in a row
- [ ] Scoring: Move: +1, Clear path: +5, Bonuses: <20 turns: +500, 20-30: +250
- [ ] No bumping (opponent chips are obstacles)
- [ ] Turn tracking for bonus calculation
- [ ] Roll-again: doubles (standard)

**Special Logic**:
- Initialize board with opponent chips in specific positions
- IsBumpAllowed always returns false
- Track turn count from game start
- Award bonuses at win detection

**Tests Required** (8-10 tests):
- IsBumpAllowed_AlwaysReturnsFalse()
- CheckWin_With5InARow_And19Turns_Awards500Bonus()
- CheckWin_With5InARow_And25Turns_Awards250Bonus()
- CheckWin_With5InARow_And31Turns_AwardsNoBonus()
- OnTurnEnd_IncrementsTurnCount()
- GetRulesText_ExplainsObstacles()

---

### Task 7: GameModeFactory Implementation
**Assignee**: Gameplay Engineer Agent  
**Estimated Hours**: 3 hours  
**File**: `/Assets/Scripts/GameModes/GameModeFactory.cs`

**Deliverables**:
- [ ] Factory class with static methods
- [ ] CreateGameMode(int modeID) method
- [ ] CreateGameMode(string modeName) method
- [ ] GetAllModes() method returns list of all 5 modes
- [ ] Proper error handling for unknown modes/IDs

**Tests Required**:
- CreateGameMode_WithValidID_ReturnsCorrectMode()
- CreateGameMode_WithValidName_ReturnsCorrectMode()
- CreateGameMode_WithInvalidID_ThrowsException()
- GetAllModes_ReturnsFiveModes()

---

### Task 8: Unit Tests for All Modes
**Assignee**: Gameplay Engineer Agent  
**Estimated Hours**: 12 hours  
**Files**:
- `/Assets/Scripts/Tests/Game1_Bump5Tests.cs` (8-10 tests)
- `/Assets/Scripts/Tests/Game2_Krazy6Tests.cs` (8-10 tests)
- `/Assets/Scripts/Tests/Game3_PassTheChipTests.cs` (8-10 tests)
- `/Assets/Scripts/Tests/Game4_BumpUAnd5Tests.cs` (8-10 tests)
- `/Assets/Scripts/Tests/Game5_SolitaryTests.cs` (8-10 tests)

**Test Requirements**:
- All tests use NUnit framework
- Follow naming: `MethodName_Condition_ExpectedResult`
- Minimum 40 tests total (8+ per mode)
- Mock Player and BoardModel as needed
- Test edge cases for each mode
- Target 80%+ code coverage

**Test Execution**:
```bash
# Run all tests
Unity Editor → Window → General → Test Runner → Run All
# or from command line
unity -runTests -testCategory="Sprint3"
```

---

### Task 9: Code Review & Documentation Pass
**Assignee**: Managing Engineer  
**Estimated Hours**: 4 hours

**Review Checklist**:
- [ ] All 5 classes follow naming conventions (GameX_ModeName)
- [ ] All public methods have XML documentation
- [ ] No magic numbers (all constants defined)
- [ ] All 40+ tests pass with no failures
- [ ] No console errors or warnings
- [ ] Code follows CODING_STANDARDS.md exactly
- [ ] Each mode has clear, complete rules text
- [ ] Factory tested and working
- [ ] Edge cases handled properly
- [ ] Code is production-ready

**Review Process**:
1. Running all unit tests
2. Static code analysis (check for warnings)
3. Documentation verification
4. Edge case testing
5. Integration test with GameStateManager

---

## Definition of Done (Sprint 3 Completion)

All the following must be true:

✅ **Implementation**
- All 5 game modes fully implemented
- All methods in IGameMode interface implemented per mode
- GameModeFactory working and tested

✅ **Testing**
- 40+ unit tests written and passing
- 80%+ code coverage achieved
- All edge cases tested
- Zero test failures

✅ **Quality**
- All code follows CODING_STANDARDS.md
- All public methods documented with XML
- No console errors or warnings
- No magic numbers (all as constants)

✅ **Documentation**
- Each mode has GetRulesText() with clear instructions
- File structure matches spec
- Mode descriptions complete

✅ **Integration**
- Each mode tested with GameStateManager
- Factory tested end-to-end
- Mode polymorphism working correctly

✅ **Review & Sign-off**
- Code reviewed by Managing Engineer
- All feedback addressed
- Ready for Sprint 4 integration

---

## Communication & Checkpoints

### Daily Standup (async via thread)
The Gameplay Engineer Agent will report:
- ✅ Completed since last standup
- 🔄 In progress
- 🚫 Blockers (if any)

### Sprint Checkpoint Dates

**Day 1 (Nov 28)**: 
- IGameMode interface verified
- Game1_Bump5 skeleton created

**Day 2 (Nov 29)**:
- Game1_Bump5 complete and tested
- Game2_Krazy6 in progress

**Day 3 (Nov 30)**:
- Game2_Krazy6 complete and tested
- Game3_PassTheChip in progress

**Day 4 (Dec 1)**:
- Game3_PassTheChip complete and tested
- Game4_BumpUAnd5 in progress

**Day 5 (Dec 2)**:
- Game4_BumpUAnd5 complete and tested
- Game5_Solitary in progress

**Day 6 (Dec 3)**:
- Game5_Solitary complete and tested
- GameModeFactory complete

**Day 7 (Dec 4)**:
- All 40+ tests passing
- Code review in progress

**Sprint Completion (Dec 5)**:
- All feedback incorporated
- Final review pass
- Ready for Sprint 4

---

## Success Metrics

| Metric | Target | Status |
|--------|--------|--------|
| Game modes implemented | 5/5 | ⬜ |
| Unit tests passing | 40+ | ⬜ |
| Code coverage | 80%+ | ⬜ |
| Documentation complete | 100% | ⬜ |
| Code review passed | Yes | ⬜ |
| Zero critical bugs | Yes | ⬜ |

---

## Risk Assessment

| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|-----------|
| Complex logic bugs | Medium | High | Comprehensive testing, edge case review |
| State tracking issues | Medium | High | Careful initialization, logging, debugging |
| Integration with GameStateManager | Low | High | Early testing with Sprint 2 code |
| Documentation gaps | Low | Medium | Mandatory XML summaries, rules text review |

---

## Next Sprint Preview (Sprint 4)

Sprint 4 will create the **Board visualization system** that displays this logic:
- BoardGridManager with 12-cell layout
- Cell prefabs and visual feedback
- Chip visuals and placement
- Valid move highlighting
- Tap/click detection

Sprint 3's game logic will be ready for Sprint 4 to build the UI around it.

---

**Briefing Complete**: Nov 14, 2025  
**Sprint Start**: Nov 28, 2025  
**Expected Completion**: Dec 5, 2025  
**Approved By**: Managing Engineer
