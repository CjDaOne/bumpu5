# Managing Engineer Sprint 2 Operations Checklist

**Role**: Managing Engineer (Amp)  
**Sprint**: Sprint 2 - State Machine & Game Flow Control  
**Duration**: Nov 14-19, 2025  
**Team**: Gameplay Engineer (1 agent)  
**Status**: ACTIVE

---

## Daily Operations (Mon-Fri)

### Morning (Check-in)
- [ ] **08:00 UTC**: Review overnight progress on GitHub/email
- [ ] **08:15 UTC**: Check for blockers in daily standup
- [ ] **08:30 UTC**: Respond to any escalations (< 4h SLA)
- [ ] **Update**: SPRINT_2_DAILY_STANDUP_LOG.md with status

### Mid-Day (Review)
- [ ] **12:00 UTC**: Check for code submissions (if any)
- [ ] **12:15 UTC**: Quick code review pass
- [ ] **12:30 UTC**: Provide feedback (within 4 hours of submission)
- [ ] **Document**: Any issues in code review section

### Evening (Wrap-Up)
- [ ] **18:00 UTC**: Final check for day's progress
- [ ] **18:15 UTC**: Update REAL_TIME_PROJECT_DASHBOARD.md
- [ ] **18:30 UTC**: Flag any emerging risks
- [ ] **Log**: Day summary in standup log

---

## Sprint 2 Code Review Checklist

**When team submits code**, review for:

### Code Quality Standards
- [ ] Follows CODING_STANDARDS.md (naming, indentation, comments)
- [ ] Zero compiler errors/warnings
- [ ] Methods < 30 lines where possible
- [ ] Clear, meaningful variable names (no abbreviations)
- [ ] XML documentation comments on all public members

### Architecture & Design
- [ ] Event-driven (not tight coupling)
- [ ] Proper dependency injection (GameStateManager init pattern)
- [ ] No direct UI dependencies
- [ ] State transitions validated
- [ ] No memory leaks (events unsubscribed)

### Testing & Coverage
- [ ] Tests written for all public methods
- [ ] Unit tests for logic
- [ ] Integration tests for phase transitions
- [ ] Coverage ≥ 85%
- [ ] Happy path AND error cases tested
- [ ] All tests passing (green CI/CD)

### Functional Correctness
- [ ] Acceptance criteria met (all checkboxes)
- [ ] Works with Sprint 1 classes (Player, Chip, etc.)
- [ ] State machine transitions correct
- [ ] Win detection accurate
- [ ] Events fire correctly

### Documentation
- [ ] Class-level doc explaining purpose
- [ ] Method signatures documented
- [ ] Complex logic explained inline
- [ ] README/ARCHITECTURE updated if needed

---

## Code Review Decision Tree

```
CODE SUBMITTED
    ↓
┌─ CHECK: Quality Standards Met?
│   ├─ NO → "Needs Work" (detail issues)
│   └─ YES ↓
├─ CHECK: Architecture Sound?
│   ├─ NO → "Needs Work" (suggest refactoring)
│   └─ YES ↓
├─ CHECK: Tests Comprehensive?
│   ├─ NO → "Needs Work" (request more tests)
│   └─ YES ↓
├─ CHECK: Functional Correct?
│   ├─ NO → "Needs Work" (describe bug)
│   └─ YES ↓
└─ CHECK: Documented?
    ├─ NO → "Needs Work" (add docs)
    └─ YES → "LGTM" ✅ APPROVED
```

---

## Sprint 2 Specific Review Points

### Day 1-2 Review (Enum + Scaffolding)

**Checklist**:
- [ ] GamePhase enum has all 7 values
- [ ] Values are unique (0-6)
- [ ] GameStateManager compiles
- [ ] All event declarations correct
- [ ] All method signatures match plan

**Expected Issues**: Unlikely (straightforward enum)

**Turnaround**: 1-2 hours

---

### Day 2-3 Review (Phase Logic)

**Checklist**:
- [ ] TransitionToPhase() validates transitions
- [ ] IsValidTransition() table complete & correct
- [ ] RollDice() calls DiceManager correctly
- [ ] PlaceChip() validates via BoardModel
- [ ] BumpChip() / SkipBump() work correctly
- [ ] Tests cover all paths (happy + error)
- [ ] No compiler warnings

**Expected Issues**:
- Missing validation?
- Incomplete transition table?
- Missing error handling?

**Turnaround**: 2-4 hours

---

### Day 3-4 Review (Win Detection)

**Checklist**:
- [ ] EndTurn() switches players correctly
- [ ] CheckWinCondition() calls IGameMode interface
- [ ] DeclareWin() validates properly
- [ ] GameWon → GameOver transition works
- [ ] Events fire in correct order
- [ ] Tests verify win detection logic

**Expected Issues**:
- IGameMode interface not defined? (escalate or define placeholder)
- Win condition timing wrong?
- State not updating correctly?

**Turnaround**: 2-4 hours

---

### Day 4-5 Review (Tests + Integration)

**Checklist**:
- [ ] Full game flow tests (25+) pass
- [ ] GamePhase enum tests (8) pass
- [ ] Total 78+ tests pass
- [ ] Coverage report shows ≥ 85%
- [ ] Integration with Sprint 1 verified
- [ ] No memory leaks (test teardown clean)
- [ ] Performance acceptable (no O(n²) loops)

**Expected Issues**:
- Coverage < 85%? (request additional tests)
- Tests failing? (code must be fixed)
- Flaky tests? (request stabilization)

**Turnaround**: 4-6 hours

---

### Day 5 Review (Final)

**Checklist**:
- [ ] All feedback from previous reviews addressed
- [ ] Documentation complete
- [ ] Zero compiler warnings
- [ ] All tests passing
- [ ] Code follows all standards
- [ ] Ready to merge

**Expected Issues**: None (all addressed by now)

**Turnaround**: 1-2 hours

**Outcome**: APPROVED for merge to `develop`

---

## Blocker Resolution Protocol

### If Team Hits a Blocker

**Response Time**: < 4 hours (SLA)

**Resolution Steps**:
1. **Acknowledge**: Confirm I received the escalation
2. **Clarify**: Ask questions to understand fully
3. **Options**: Propose 2-3 ways forward
4. **Decide**: Make decision based on impact
5. **Document**: Log in DECISION_LOG.md

**Example Blocker** (from team):
```
BLOCKER: IGameMode interface not defined
- Blocks: CheckWinCondition() implementation  
- Impact: Can't finish Day 3 task
- Options:
  A) Define interface now (15 min work)
  B) Assume basic interface, continue
  C) Use mock for now, refactor in Sprint 3

Recommendation: Option A (cleanest)
```

**My Response**:
```
DECISION: Option A approved
REASONING: Interface is needed for Sprint 3 anyway; better to do now
NEXT STEPS: Define IGameMode in GameState.cs, test with basic implementation
DOCUMENTATION: Update ARCHITECTURE.md with interface diagram
TIMELINE: 15 min work, back on track for Day 3
```

---

## Integration Testing

### Verify Sprint 2 Integrates with Sprint 1

**Test Checklist**:
- [ ] GameStateManager initializes with Sprint 1 classes
  - Player (from Sprint 1)
  - Chip (from Sprint 1)
  - BoardCell (from Sprint 1)
  - TurnManager (from Sprint 1)
  - DiceManager (from Sprint 1)
  - BoardModel (from Sprint 1)
- [ ] GameStateManager fires events correctly
- [ ] State transitions don't break on Sprint 1 data
- [ ] No memory corruption or null reference exceptions
- [ ] Performance acceptable with all Sprint 1 classes loaded

**Verification**: Run integration tests as written by team

---

## Documentation Review

### Ensure Deliverables Have Full Docs

**Files to Check**:
- [ ] GamePhase.cs - Class & enum member docs
- [ ] GameStateManager.cs - Class doc + all public methods
- [ ] ARCHITECTURE.md - State machine diagram added
- [ ] DECISION_LOG.md - Any new decisions logged

**Standards**:
- [ ] XML documentation comments (/// <summary>)
- [ ] All public methods documented
- [ ] Complex logic has inline comments
- [ ] Architecture decisions justified

---

## Sprint 2 Sign-Off Criteria (Day 5)

### Release Approval Gate

**Before Sprint 2 is considered COMPLETE**, verify:

- [ ] **Code Quality**: 100% standards compliance
- [ ] **Test Coverage**: ≥ 85% measured
- [ ] **Tests Passing**: All 78+ pass (green)
- [ ] **Compilation**: Zero errors/warnings
- [ ] **Documentation**: Complete & clear
- [ ] **Integration**: Works with Sprint 1 ✅
- [ ] **Performance**: No regression
- [ ] **Code Review**: ME approved ✅
- [ ] **No Blockers**: None outstanding

**Approval Signature**:
```
ME (Amp) approves Sprint 2 as COMPLETE

Date: [TBD]
Status: Ready for Sprint 3
Next: Board Team begins Sprint 4 prep review
```

---

## Expected Issues & Quick Resolutions

| Issue | Cause | Resolution | Time |
|-------|-------|-----------|------|
| IGameMode not defined | Enum refers to interface | Define placeholder interface in GameState.cs | 15m |
| Tests failing on state transition | Transition table incomplete | Verify all paths in IsValidTransition() | 30m |
| Coverage < 85% | Missing test scenarios | Add edge case tests (invalid moves, etc.) | 1h |
| Memory leak on event subscription | Events not unsubscribed | Add OnDestroy cleanup | 20m |
| Compiler warning: "unused field" | Code cleanup | Remove or use the field | 10m |

---

## Daily Status Updates

### Standup Log Updates (Daily)

**I will update**:
- [ ] SPRINT_2_DAILY_STANDUP_LOG.md with day's standup
- [ ] REAL_TIME_PROJECT_DASHBOARD.md with progress %
- [ ] Code review feedback (if submitted)
- [ ] Any blockers & resolutions

**Team will provide**:
- [ ] What completed
- [ ] What in progress
- [ ] Any blockers (escalate immediately)

---

## Handoff to Sprint 3

### When Sprint 2 Complete

**Checklist**:
- [ ] All code merged to `develop`
- [ ] All tests passing in CI/CD
- [ ] Tag: `release/sprint-2-complete`
- [ ] Create Sprint 3 execution plan (prepare board team)
- [ ] Brief board team on GameStateManager architecture
- [ ] Sprint 3 kickoff document sent

**Timeline**: Day 5, end of business → Sprint 3 ready for Day 6

---

## Communication Log

### Sprint 2 Communication

**Daily Standups**: Will be logged in SPRINT_2_DAILY_STANDUP_LOG.md  
**Code Reviews**: Will be logged with feedback & decisions  
**Decisions**: Will be logged in DECISION_LOG.md  
**Issues**: Will be tracked with resolution status

---

## Timeline & Milestones

| Date | Milestone | Status |
|------|-----------|--------|
| Nov 14 (Day 1) | Enum + Scaffolding | 🔴 IN PROGRESS |
| Nov 15 (Day 2) | Phase Logic | 🟡 Scheduled |
| Nov 16 (Day 3) | Win Detection | 🟡 Scheduled |
| Nov 17 (Day 4) | Integration Tests | 🟡 Scheduled |
| Nov 18 (Day 5) | Code Review + Docs | 🟡 Scheduled |
| Nov 19 (End) | **SPRINT 2 COMPLETE** | ⏳ Target |

---

## Success Metrics

**Sprint 2 is successful when**:

✅ All 78+ tests passing  
✅ Coverage ≥ 85%  
✅ Code review approved (ME sign-off)  
✅ Documentation complete  
✅ Zero compiler warnings  
✅ Integrated with Sprint 1  
✅ Merged to `develop`  
✅ No blockers for Sprint 3  

---

## Notes

- **Gameplay Engineer** is the expert on this code; I'm here to unblock and review
- **Trust but verify**: Good code quality means fewer surprises later
- **Escalate early**: If something doesn't fit the plan, flag it immediately
- **No crunch**: Work is sustainable; focus on quality over speed

---

**Owner**: Amp (Managing Engineer)  
**Status**: ACTIVE  
**Last Updated**: Nov 14, 2025  
**Next Review**: Daily during sprint

