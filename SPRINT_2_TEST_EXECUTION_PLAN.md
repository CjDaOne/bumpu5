# Sprint 2 Test Execution Plan - Days 5-6

**Objective**: Run all 32 tests, achieve 80%+ coverage, debug failures  
**Duration**: 2 days (6-8 hours)  
**Owner**: Gameplay Engineer

---

## How to Run Tests in Unity

### Step 1: Open Test Runner
```
Unity Editor → Window → Testing → Test Runner
```

### Step 2: Select Test Assembly
- Look for: `Assembly-CSharp-Tests` or `GameStateManagerTests`

### Step 3: Run All Tests
- Click "Run All" button (green play icon)
- Wait for test execution to complete

### Step 4: Check Results
- Green checkmarks ✅ = Passing tests
- Red X = Failing tests
- Blue circle = Skipped tests

---

## Test Execution Order

### Phase 1: Setup & Initialization (4 tests)
```
[1] Initialize_WithValidPlayers_SetsUpGameCorrectly
    Expected: PASS
    
[2] Initialize_WithNullPlayer_RaisesError
    Expected: PASS
    
[3] StartGame_TransitionsToRollingPhase
    Expected: PASS
    
[4] StartGame_InWrongPhase_RaisesError
    Expected: PASS
```

**If all PASS**: Proceed to Phase 2  
**If any FAIL**: Debug initialization logic in `Initialize()` and `StartGame()`

---

### Phase 2: Phase Transitions (8 tests)
```
[5] RollDice_TransitionsFromRollingToPlacing
    Expected: PASS
    
[6] RollDice_InWrongPhase_RaisesError
    Expected: PASS
    
[7] RollDice_StoresRollResult
    Expected: PASS
    
[8] PlaceChip_TransitionsFromPlacingToBumping
    Expected: PASS (or EndTurn if no bump possible)
    
[9] PlaceChip_WithInvalidCell_RaisesError
    Expected: PASS
    
[10] PlaceChip_InWrongPhase_RaisesError
     Expected: PASS
     
[11] BumpChip_TransitionsToEndTurn
     Expected: PASS (if bump is possible)
     
[12] EndTurn_RotatesPlayer
     Expected: PASS
```

**If all PASS**: Proceed to Phase 3  
**If any FAIL**: Debug phase transition logic in `RollDice()`, `PlaceChip()`, `EndTurn()`

---

### Phase 3: Event Firing (5 tests)
```
[13] OnPhaseChanged_FiresForEachTransition
     Expected: PASS
     
[14] OnDiceRolled_FiresWithCorrectValues
     Expected: PASS
     
[15] OnPlayerChanged_FiresAfterEndTurn
     Expected: PASS
     
[16] OnInvalidAction_FiresForBadMove
     Expected: PASS
     
[17] SkipBump_TransitionsToEndTurn
     Expected: PASS
```

**If all PASS**: Proceed to Phase 4  
**If any FAIL**: Debug event firing in `TransitionPhase()` or individual methods

---

### Phase 4: State Queries (5 tests)
```
[18] CanPlaceChip_ReturnsTrueForValidCell
     Expected: PASS
     
[19] CanPlaceChip_ReturnsFalseForInvalidCell
     Expected: PASS
     
[20] CanPlaceChip_ReturnsFalseForOutOfBounds
     Expected: PASS
     
[21] CanBumpChip_ReturnsFalseForEmptyCell
     Expected: PASS
     
[22] CanBumpChip_ReturnsFalseForInvalidCell
     Expected: PASS
```

**If all PASS**: Proceed to Phase 5  
**If any FAIL**: Debug validation logic in `CanPlaceChip()` and `CanBumpChip()`

---

### Phase 5: Edge Cases (5 tests)
```
[23] TurnNumber_IncrementOnEndTurn
     Expected: PASS
     
[24] LastDiceRoll_StoresRollResult
     Expected: PASS
     
[25] ConsecutiveRolls_WorksCorrectly
     Expected: PASS
     
[26] HasWon_ReturnsFalseWhenNoWinCondition
     Expected: PASS
     
[27] CanRollAgain_InitiallyFalse
     Expected: PASS
```

**If all PASS**: Proceed to Phase 6  
**If any FAIL**: Debug edge case handling in special rule methods

---

### Phase 6: Integration Tests (3 tests)
```
[28] FullGameFlow_InitializeToRolling
     Expected: PASS
     
[29] FullGameFlow_RollingToPlacingToEndTurn
     Expected: PASS
     
[30] GetValidMoves_ReturnsListOfCells
     Expected: PASS
```

**If all PASS**: All tests complete ✅

---

## Coverage Verification

After tests pass, verify coverage:

```
Unity Editor:
- Window → Analysis → Code Coverage
- Enable "Gather Code Coverage"
- Re-run all tests
- Check coverage percentage in Coverage report
```

**Target**: 80%+ coverage  
**Actual**: Will be measured after execution

### Coverage Expected to Cover:
- ✅ All public methods (100%)
- ✅ All event firing (100%)
- ✅ Phase transitions (100%)
- ✅ Null checks (95%+)
- ✅ Helper methods (100%)
- ✅ Edge case logic (95%+)

---

## Common Test Failures & Fixes

### Failure: "NUnit not found"
**Cause**: Test Runner assembly not recognized  
**Fix**: Ensure test file is in `/Assets/Scripts/Tests/` folder

### Failure: "GameStateManager not found"
**Cause**: Missing `using` statements  
**Fix**: Verify both files are in correct directories

### Failure: "Phase transition test fails"
**Cause**: Actual phase ≠ expected phase  
**Fix**: Check `TransitionPhase()` logic in GameStateManager

### Failure: "Event not firing"
**Cause**: Event subscriber added after firing  
**Fix**: Verify event listener is registered BEFORE action that fires it

### Failure: "Player rotation incorrect"
**Cause**: TurnManager.AdvanceTurn() not called  
**Fix**: Verify `EndTurn()` calls `turnManager.AdvanceTurn()`

---

## Debug Checklist

If tests fail, use this systematic approach:

- [ ] Check that all files compile (0 errors)
- [ ] Verify test file is in `/Assets/Scripts/Tests/`
- [ ] Verify GameStateManager is in `/Assets/Scripts/Core/`
- [ ] Check that `SetUp()` method initializes properly
- [ ] Verify all dependencies exist (Player, BoardModel, TurnManager, DiceManager)
- [ ] Use `Debug.Log()` to trace method execution
- [ ] Check for null reference exceptions
- [ ] Verify event subscribers are registered in correct order
- [ ] Ensure phase transitions are called in correct sequence

---

## Performance Verification

After tests pass, verify performance:

```csharp
// Add simple performance check
[Test]
public void RollDice_Performance_UnderThreshold()
{
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    
    stateManager.Initialize(player1, player2);
    stateManager.StartGame();
    for (int i = 0; i < 100; i++)
    {
        stateManager.RollDice();
        stateManager.EndTurn();
    }
    
    stopwatch.Stop();
    Assert.Less(stopwatch.ElapsedMilliseconds, 500); // 100 rolls in < 500ms
}
```

**Expected**: < 5ms per roll, < 50ms per full turn

---

## Day 5 Schedule

- **2 hours**: Run all tests, verify passing
- **2 hours**: Debug any failures
- **1 hour**: Measure coverage
- **1 hour**: Fix coverage gaps (if needed)

**Target**: All 32 tests passing by end of day 5

---

## Day 6 Schedule

- **2 hours**: Re-run tests for stability
- **2 hours**: Performance verification
- **2 hours**: Final refinements & documentation
- **1 hour**: Prepare for code review

**Target**: 100% test pass rate, 80%+ coverage

---

## Success Criteria

✅ All 32 tests passing  
✅ 80%+ code coverage  
✅ 0 compiler errors/warnings  
✅ < 5ms per operation  
✅ No flaky tests (run twice with same results)  
✅ All assertions meaningful  
✅ No console spam  

---

## Final Submission (Day 7)

Before submitting for code review:

- [ ] All 32 tests pass
- [ ] Coverage ≥ 80%
- [ ] No compiler warnings
- [ ] All methods documented
- [ ] No magic numbers
- [ ] Guard clauses present
- [ ] Events properly null-checked
- [ ] Run tests 2+ times (prove they're not flaky)

**Then**: Submit for ME review (Amp)

---

**Ready to execute Days 5-6 testing phase?**
