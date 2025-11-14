# Sprint 2 Code Review Checklist - Day 7

**Submission Date**: End of Day 7  
**Reviewer**: Managing Engineer (Amp)  
**Review SLA**: Within 24 hours

---

## Pre-Submission Checklist (Engineer to Complete)

Before submitting for ME review, verify all items below:

### Code Quality
- [ ] All 32 tests pass (100% pass rate)
- [ ] Code coverage ≥ 80%
- [ ] 0 compiler errors
- [ ] 0 compiler warnings
- [ ] No #pragma directives suppressing warnings
- [ ] No console.log or Debug.LogError spam

### Naming Standards (CODING_STANDARDS.md)
- [ ] Classes use PascalCase: `GameStateManager`, `GamePhase`
- [ ] Methods use PascalCase: `RollDice()`, `PlaceChip()`
- [ ] Properties use PascalCase: `CurrentPhase`, `CurrentPlayer`
- [ ] Local variables use camelCase: `currentPhase`, `lastDiceRoll`
- [ ] Constants use UPPER_SNAKE_CASE: `MAX_CONSECUTIVE_DOUBLES`, `BOARD_SIZE`
- [ ] Enums use PascalCase: `GamePhase`

### Documentation
- [ ] All public classes documented (summary)
- [ ] All public methods documented (summary + params + returns)
- [ ] All public properties documented
- [ ] All enums have comments
- [ ] No TODO or FIXME comments in code
- [ ] Documentation is accurate and clear

### Guard Clauses
- [ ] All null inputs validated at method entry
- [ ] All invalid phase transitions caught
- [ ] All out-of-bounds cell accesses prevented
- [ ] All divide-by-zero prevented
- [ ] All invalid state combinations prevented

### No Magic Numbers
- [ ] All hard-coded values replaced with constants
- [ ] Constants defined at class level
- [ ] Constants follow naming standards
- [ ] Constants are documented

Example:
```csharp
// ❌ Bad
if (roll[0] == 6) { }

// ✅ Good
private const int LOSE_TURN_ROLL = 6;
if (roll[0] == LOSE_TURN_ROLL) { }
```

### Events
- [ ] All 5 events declared at class level
- [ ] Event names describe action (e.g., OnPhaseChanged)
- [ ] Events use `Action` or `Action<T>` delegates
- [ ] All events are null-checked before invoking
- [ ] Events fire at correct times
- [ ] Event parameters are meaningful

Example:
```csharp
// ✅ Good
OnPhaseChanged?.Invoke(newPhase);

// ❌ Bad (no null check)
OnPhaseChanged(newPhase); // Could throw NullReferenceException
```

### Integration
- [ ] GameStateManager uses all Sprint 1 classes correctly
- [ ] No Sprint 1 classes were modified
- [ ] All dependencies are properly initialized
- [ ] No circular dependencies
- [ ] All external APIs are used as intended

### Tests
- [ ] Test file names follow pattern: `{Class}Tests.cs`
- [ ] Test method names follow pattern: `MethodName_Condition_Result()`
- [ ] All tests have proper SetUp and TearDown
- [ ] Tests are isolated (no side effects between tests)
- [ ] Tests are deterministic (same input = same output)
- [ ] All assertions are meaningful
- [ ] No test interdependencies

### Performance
- [ ] No allocations in hot paths
- [ ] No unnecessary string concatenation
- [ ] No excessive list/array creation
- [ ] Phase transitions complete in < 1ms
- [ ] Turn operations complete in < 50ms

### Edge Cases
- [ ] ✅ 5+6 safe combination handled
- [ ] ✅ Rolling 6 lose turn handled
- [ ] ✅ Consecutive doubles tracked (max 3)
- [ ] ✅ Null player validation
- [ ] ✅ Invalid phase transitions blocked
- [ ] ✅ Out-of-bounds cells prevented
- [ ] ✅ Can't move to own chips
- [ ] ✅ Win detection after moves

---

## Code Review Sections (ME to Verify)

### Section 1: Architecture & Design
**Reviewer**: Amp

- [ ] Single Responsibility Principle (GameStateManager handles state only)
- [ ] Dependency Injection (dependencies passed in, not created)
- [ ] Event-driven architecture (loose coupling via events)
- [ ] No tight coupling to specific implementations
- [ ] Extensible for game modes (Sprint 3)

### Section 2: Code Quality
**Reviewer**: Amp

- [ ] No code duplication
- [ ] Methods are focused and small
- [ ] No nested conditionals (guard clauses used)
- [ ] No "god" classes
- [ ] Clear code flow
- [ ] Comments explain "why", not "what"

Example:
```csharp
// ❌ Bad (comment repeats code)
// Check if roll is double
if (roll[0] == roll[1]) { }

// ✅ Good (comment explains reason)
// Doubles allow reroll in standard rules
if (roll[0] == roll[1]) { }
```

### Section 3: Testing Coverage
**Reviewer**: Amp

- [ ] Setup & Initialization: 4+ tests ✅
- [ ] Phase Transitions: 8+ tests ✅
- [ ] Event Firing: 5+ tests ✅
- [ ] State Queries: 5+ tests ✅
- [ ] Edge Cases: 5+ tests ✅
- [ ] Integration: 3+ tests ✅
- [ ] Total: 30+ tests ✅
- [ ] Coverage: 80%+ ✅

### Section 4: Standards Compliance
**Reviewer**: Amp

**CODING_STANDARDS.md Checklist**:
- [ ] Naming: 100% compliant
- [ ] Documentation: 100% compliant
- [ ] Testing: 100% compliant (80%+ coverage)
- [ ] Guard clauses: Everywhere needed
- [ ] No magic numbers: All constants used
- [ ] No deep nesting: Guard clauses used

**Compilation**:
- [ ] 0 errors
- [ ] 0 warnings
- [ ] No suppressions

### Section 5: Functional Correctness
**Reviewer**: Amp

**Core Methods**:
- [ ] Initialize() sets up game correctly
- [ ] StartGame() transitions to Rolling phase
- [ ] RollDice() rolls correctly and handles edge cases
- [ ] PlaceChip() transitions correctly
- [ ] BumpOpponentChip() executes bump
- [ ] SkipBump() advances turn
- [ ] EndTurn() rotates players

**State Queries**:
- [ ] CanPlaceChip() validates placement
- [ ] CanBumpChip() validates bumping
- [ ] GetValidMoves() returns valid cells
- [ ] HasWon() detects 5-in-a-row

**Edge Cases**:
- [ ] 5+6 "safe" skips movement
- [ ] Rolling 6 loses turn
- [ ] Consecutive doubles tracked
- [ ] Win detected immediately

**Events**:
- [ ] OnPhaseChanged fires on transition
- [ ] OnDiceRolled fires with roll results
- [ ] OnPlayerChanged fires after rotation
- [ ] OnGameWon fires with winner
- [ ] OnInvalidAction fires with error message

### Section 6: Integration
**Reviewer**: Amp

- [ ] TurnManager.AdvanceTurn() called correctly
- [ ] BoardModel methods called correctly
- [ ] DiceManager.RollTwoDice() used
- [ ] Player references valid
- [ ] No Sprint 1 classes modified
- [ ] All dependencies properly initialized

---

## Approval Criteria

### For ME Approval ✅
**All of the following must be true**:

1. ✅ All 32 tests pass
2. ✅ Code coverage ≥ 80%
3. ✅ 0 compiler errors
4. ✅ 0 compiler warnings
5. ✅ 100% CODING_STANDARDS.md compliance
6. ✅ All public methods documented
7. ✅ No magic numbers
8. ✅ All edge cases handled
9. ✅ Events properly integrated
10. ✅ Code review checklist completed

**If all above are true**: **APPROVED** ✅  
**If any above is false**: **REQUEST REVISIONS** 🔄

---

## Feedback Process

### If Approved
1. ME merges to main branch
2. Tag commit with sprint number
3. Update project status
4. Clear sprint 3 to begin
5. Document in DECISION_LOG.md if any changes made

### If Changes Requested
1. ME lists specific issues
2. Engineer makes revisions
3. Engineer re-submits for review
4. ME re-reviews and approves/requests more changes
5. Cycle repeats until approved

---

## Post-Approval Actions

Once approved:

- [ ] Merge to main branch
- [ ] Tag commit: `sprint-2-complete`
- [ ] Update SPRINT_STATUS.md
- [ ] Update PROJECT_STATUS_REPORT.md
- [ ] Close all related GitHub issues
- [ ] Notify team of completion
- [ ] Begin Sprint 3 preparation

---

## Review Timeline

| Phase | Time | Owner |
|-------|------|-------|
| Engineer submission | EOD Day 7 | Gameplay Engineer |
| ME initial review | Day 8 (morning) | Amp |
| Feedback to engineer | Day 8 (morning) | Amp |
| Engineer revisions | Day 8 | Gameplay Engineer |
| Final approval | Day 8-9 | Amp |
| Merge to main | Day 9 | Amp |

**Target**: Approved and merged by EOD Day 8

---

## Escalation Criteria

Escalate to stakeholder if:

- [ ] Code review takes > 24 hours
- [ ] Revisions needed > 2 rounds
- [ ] Multiple compilation errors
- [ ] Test coverage < 70%
- [ ] Critical edge cases missed
- [ ] Architecture questions arise

**Escalation Path**: Managing Engineer → Project Lead → Stakeholder

---

## Success Metrics

**For Sprint 2**:

| Metric | Target | Status |
|--------|--------|--------|
| Tests Passing | 100% | To verify |
| Code Coverage | 80%+ | To measure |
| Compilation | 0 errors | To verify |
| Standards | 100% | To verify |
| Documentation | 100% | To verify |
| Code Review | Approved | To complete |

**All metrics must be achieved** for sprint completion.

---

## Reviewer Sign-Off

```
Managing Engineer: Amp
Code Review Date: ____________
Approval Status: [ ] APPROVED [ ] REVISIONS NEEDED
Comments:
_________________________________________________________________
_________________________________________________________________
```

---

**Next**: Submit on Day 7 EOD for review
