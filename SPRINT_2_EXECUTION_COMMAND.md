# Sprint 2 EXECUTION COMMAND

**STATUS: ACTIVE 🟢**  
**AUTHORITY: ACCELERATED EXECUTION GRANTED**  
**EFFECTIVE**: Immediately (Nov 14, 2025)  
**OWNER**: Gameplay Engineer  
**OVERSEER**: Amp (Managing Engineer)

---

## MISSION STATEMENT

Implement **GameStateManager.cs** – the central game state machine that orchestrates all turn flow, phase transitions, and game logic.

**Impact**: This is the critical dependency for all remaining work (Sprints 3-8). Sprint 2 completion unblocks the entire project.

**Success Definition**: GameStateManager compiles, all 22+ tests pass, 80%+ coverage, 100% standards compliant.

---

## EXECUTION TIMELINE

| Day | Task | Hours | Deliverable |
|-----|------|-------|-------------|
| 1 | Setup + enum | 2h | GamePhase.cs compiles |
| 2-3 | Core methods | 6h | RollDice, PlaceChip, EndTurn working |
| 4-5 | Complete logic | 8h | All phases, events, edge cases |
| 6-7 | Testing + polish | 6h | 22+ tests passing |
| 8 | Code review | - | ME approval |

**Total**: ~22 hours development + 24 hour code review = 8 days (fits 1-week sprint perfectly)

---

## DELIVERABLES (MUST HAVE ALL)

### 1. GamePhase.cs
```csharp
public enum GamePhase
{
    Setup,      // Initial game setup
    Rolling,    // Player rolls dice
    Placing,    // Player places chip
    Bumping,    // Optional bump phase
    EndTurn,    // Turn ending
    GameOver    // Win condition
}
```
**File**: `/Assets/Scripts/Core/GamePhase.cs`  
**Lines**: ~15  
**Tests**: N/A (enum, no logic)

### 2. GameStateManager.cs
**File**: `/Assets/Scripts/Core/GameStateManager.cs`  
**Lines**: 400-600  
**Classes**: 1 (GameStateManager : MonoBehaviour)  
**Methods**: 13 public + supporting private  

**Public Interface**:
```csharp
// Setup
void Initialize(Player player1, Player player2)
void StartGame()

// Turn Flow
void RollDice()
void PlaceChip(int cellIndex)
void BumpOpponentChip(int cellIndex)
void EndTurn()

// State Queries
bool CanPlaceChip(int cellIndex)
bool CanBumpChip(int cellIndex)
bool CanDeclareWin()

// Properties
GamePhase CurrentPhase { get; }
Player CurrentPlayer { get; }
int[] LastDiceRoll { get; }

// Events
event Action<GamePhase> OnPhaseChanged
event Action<int[]> OnDiceRolled
event Action<Player> OnPlayerChanged
event Action<Player> OnGameWon
event Action<string> OnInvalidAction
```

### 3. GameStateManagerTests.cs
**File**: `/Assets/Scripts/Tests/GameStateManagerTests.cs`  
**Count**: 22+ unit tests  
**Coverage**: ≥80% code coverage  
**Categories**:
- Setup & initialization (2 tests)
- Phase transitions (8 tests)
- Event firing (6 tests)
- State queries (4 tests)
- Edge cases (4+ tests)

**Test Pattern**: `MethodName_Condition_Result`

Example:
```csharp
[Test] public void RollDice_TransitionsFromRollingToPlacing() { }
[Test] public void RollDice_With6_StaysInRollingPhase() { }
[Test] public void PlaceChip_WithValidCell_Succeeds() { }
```

---

## PRE-EXECUTION CHECKLIST

Before you start, verify:

- [ ] Sprint 1 code is in `/Assets/Scripts/Core/`
  - Player.cs ✅
  - Chip.cs ✅
  - BoardCell.cs ✅
  - BoardModel.cs ✅
  - DiceManager.cs ✅
  - TurnManager.cs ✅

- [ ] Test infrastructure in place
  - `/Assets/Scripts/Tests/` exists
  - NUnit available
  - Can run tests locally

- [ ] Development environment
  - Visual Studio / Rider open
  - C# compiler ready
  - Git on main branch, up to date

- [ ] Documentation ready
  - SPRINT_2_EXECUTION_PLAN.md open
  - CODING_STANDARDS.md bookmarked
  - PROJECT_QUALITY_GATES.md available

---

## CRITICAL SUCCESS FACTORS

### Code Quality (Non-Negotiable)
✅ 0 compiler errors (fail if > 0)  
✅ 0 compiler warnings (fail if > 0)  
✅ 100% CODING_STANDARDS.md compliance (fail if < 100%)  
✅ 80%+ test coverage (fail if < 80%)  
✅ All tests passing (fail if any fail)  
✅ Full method documentation (fail if incomplete)  

### Architecture (Non-Negotiable)
✅ No modifications to Sprint 1 classes  
✅ All dependencies injected (not hard-coded)  
✅ Events properly null-checked  
✅ Guard clauses instead of nested ifs  
✅ Single responsibility principle  

### Testing (Non-Negotiable)
✅ 22+ tests implemented  
✅ All tests deterministic (no flaky tests)  
✅ Edge cases covered (6, 5+6, no moves, win)  
✅ Test names clear and descriptive  
✅ Mock/stub external dependencies  

---

## KEY LOGIC TO IMPLEMENT

### Phase Transitions
```
Setup → Rolling (StartGame)
Rolling → Placing (after RollDice, unless 6 or 5+6)
Rolling → Rolling (if rolled 6, stay in Rolling)
Placing → Bumping (if valid bumps exist) OR EndTurn (if not)
Bumping → EndTurn
EndTurn → Rolling (next player) OR GameOver (if won)
Any → GameOver (if 5 in a row detected)
```

### Edge Cases (Must Handle)
```
Rolled 6: Stay in Rolling phase (lose turn effect in Sprint 3)
Rolled 5+6: Skip all rolling, go to EndTurn immediately
No valid moves: Skip Placing, go to Bumping or EndTurn
Player wins: Immediate transition to GameOver
Invalid move: Fire OnInvalidAction event, stay in current phase
Null references: Throw ArgumentNullException with param name
```

### Event Broadcasting (Must Fire)
```
OnPhaseChanged: When CurrentPhase changes
OnDiceRolled: When dice roll completes
OnPlayerChanged: When turn advances to next player
OnGameWon: When someone reaches 5 in a row
OnInvalidAction: When invalid move attempted
```

---

## SUBMISSION PROCESS

### When Ready
1. Verify all checklist items ✅
2. Run tests locally (all passing)
3. Check coverage (≥80%)
4. Verify no warnings/errors
5. Review code vs. CODING_STANDARDS.md
6. Commit to git with atomic changes

### Submission Format
```
Create PR/submission with:
- Brief description of implementation
- Files modified (GamePhase.cs, GameStateManager.cs, GameStateManagerTests.cs)
- Test summary (22 tests, 85% coverage)
- Any design decisions made
- Known limitations (if any)
```

### Code Review SLA
- Submitted: Day 7
- Reviewed: Within 24 hours (Day 8)
- Approved: If all checks pass
- Merged: Within 1 hour of approval

---

## SUPPORT & ESCALATION

### If You Get Stuck
1. Check SPRINT_2_EXECUTION_PLAN.md (detailed spec)
2. Review Sprint 1 code (patterns to follow)
3. Check CODING_STANDARDS.md (style guidelines)
4. Ask ME immediately (don't wait)

### Blocker Report (Use This Format)
```
BLOCKER REPORT
Date/Time: [timestamp]
Issue: [What's blocked]
Severity: Critical / Major / Minor
Context: [What I was doing when blocked]
Help Needed: [What would unblock this]
```

### ME Response SLA
- Blocker reported: Immediately flag
- ME response: Within 4 hours (weekday)
- Resolution: Clear path within 24 hours

---

## QUALITY GATES (Before Approval)

### Code Review Checklist (All Required ✅)

**Naming & Convention**
- [ ] Classes: PascalCase
- [ ] Methods: PascalCase verbs
- [ ] Properties: PascalCase
- [ ] Private fields: camelCase
- [ ] Constants: UPPER_SNAKE_CASE
- [ ] Interfaces: I prefix

**Documentation**
- [ ] All public classes documented
- [ ] All public methods documented (param, return)
- [ ] Complex logic has inline comments
- [ ] No TODOs without context

**Code Quality**
- [ ] No compiler errors
- [ ] No compiler warnings
- [ ] No magic numbers
- [ ] Guard clauses, not nested ifs
- [ ] Methods ≤ 50 lines
- [ ] Single responsibility

**Error Handling**
- [ ] Null checks on entry (ArgumentNullException)
- [ ] Invalid state detection
- [ ] Debug logging (info/warning/error appropriate)
- [ ] No silent failures

**Testing**
- [ ] 22+ tests implemented
- [ ] All tests pass
- [ ] ≥80% coverage achieved
- [ ] Edge cases tested
- [ ] Deterministic (no flaky)

**Performance**
- [ ] No allocations in Update()
- [ ] No expensive calculations per-frame
- [ ] Reasonable algorithm complexity

**Git**
- [ ] Commit format: [Sprint 2] Description
- [ ] Atomic commits (one logical change each)
- [ ] Clean history (no merge commits)

---

## SUCCESS METRICS (Track These)

| Metric | Target | How to Verify |
|--------|--------|---------------|
| Compilation | 0 errors | `dotnet build` |
| Warnings | 0 | `dotnet build` (check output) |
| Tests | 22+ pass | `dotnet test` |
| Coverage | ≥80% | Test runner or code coverage tool |
| Standards | 100% | Manual review vs. CODING_STANDARDS.md |
| Documentation | 100% | All public methods have XML comments |
| Edge cases | All handled | Code review verification |

---

## AFTER APPROVAL (Next Steps)

1. ✅ Code merged to main branch
2. ✅ Commit tagged with sprint number
3. ✅ Project status updated
4. ✅ Sprint 3 unblocked (Game modes)
5. ✅ Sprint 3 kickoff can begin

---

## THIS IS CRITICAL PATH

Everything downstream depends on this:

```
Sprint 2 ← Blocker for Sprint 3
Sprint 3 ← Blocker for Sprint 4
Sprint 4 ← Blocker for Sprint 5
Sprint 5 ← Blocker for Sprint 6
Sprint 6-8 ← All blocked until Sprint 2 complete
```

No pressure, but... **this is the most important thing right now.**

---

## QUICK LINKS

- 📄 SPRINT_2_EXECUTION_PLAN.md (Full technical spec, 22 tests detailed)
- 📄 SPRINT_2_KICKOFF_BRIEF.md (Day-by-day breakdown)
- 📄 CODING_STANDARDS.md (Standards reference)
- 📄 PROJECT_QUALITY_GATES.md (Code review checklist)
- 📄 ARCHITECTURE.md (System design)
- 📄 README.md (Project overview)

---

## FINAL WORDS

You have:
- ✅ Clear specifications
- ✅ Detailed execution plan
- ✅ Code patterns to follow
- ✅ Quality gates defined
- ✅ Full support from ME
- ✅ No blockers identified

**Execute with confidence.**

Ask for help early. No stupid questions. Escalate blockers immediately.

This sprint is the foundation for everything that follows. Make it solid.

---

**COMMAND STATUS**: 🟢 ACTIVE  
**AUTHORITY**: Accelerated execution approved  
**EXECUTION BEGINS**: Immediately (Nov 14, 2025)  
**TARGET COMPLETION**: Nov 20-21, 2025  
**OWNER**: Gameplay Engineer  
**SUPPORT**: Amp (Managing Engineer) - 24/7 available  

**GO BUILD SOMETHING GREAT.**

