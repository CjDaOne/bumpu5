# Sprint 2 Quick Reference Card

**Sprint**: Sprint 2 - State Machine & Game Flow Control  
**Lead**: Gameplay Engineer  
**Managing Engineer**: Amp  
**Duration**: Nov 14-19, 2025 (5 days, no date constraints)  

---

## Your Mission (1 Sentence)

Build GameStateManager—the state machine that orchestrates all gameplay phases and validates every game action.

---

## Deliverables at a Glance

| Component | Est. LOC | Tests | File |
|-----------|----------|-------|------|
| GamePhase enum | 40 | 8 | `Assets/Scripts/Game/GamePhase.cs` |
| GameStateManager | 600+ | 30+ | `Assets/Scripts/Managers/GameStateManager.cs` |
| Integration tests | 400 | 25+ | `Assets/Scripts/Tests/GameStateManagerIntegrationTests.cs` |
| Unit tests | 100 | 8 | `Assets/Scripts/Tests/GamePhaseTests.cs` |
| **TOTAL** | **~1,400** | **78+** | - |

**Success = All merged + approved**

---

## 5-Day Plan (One Line Per Day)

| Day | Focus | Output | Hours |
|-----|-------|--------|-------|
| 1 | Enum + scaffold | Compiling code | 2h |
| 2 | Phase logic | 5 phases working | 6h |
| 3 | Win & end game | Full game loop | 6h |
| 4 | Tests + integration | 78+ passing tests | 6h |
| 5 | Code review + docs | Approved & merged | 4h |

---

## Core Architecture (One Picture)

```
GameStateManager (Orchestrator)
├─ Events (PhaseChanged, PlayerChanged, DiceRolled, GameWon, InvalidAction)
├─ Phase Transitions (validated)
│  ├─ Idle → RollDice
│  ├─ RollDice → MoveChip
│  ├─ MoveChip → BumpOpponent
│  ├─ BumpOpponent → EndTurn
│  ├─ EndTurn → RollDice (next player)
│  └─ Any Phase → GameWon → GameOver
├─ State Machine (no invalid transitions)
└─ Dependencies (TurnManager, DiceManager, BoardModel, IGameMode)
```

---

## Key Files You Need

**Reference These**:
- 📋 `SPRINT_2_EXECUTION_PLAN.md` - Full task breakdown
- 📋 `SPRINT_2_TEAM_DISPATCH.md` - Your assignment (read first)
- 📋 `CODING_STANDARDS.md` - Style guide (compliance required)
- 📋 `ARCHITECTURE.md` - System design context
- 📋 `DECISION_LOG.md` - Why past decisions were made

**Update as You Go**:
- 📋 `SPRINT_2_DAILY_STANDUP_LOG.md` - Daily standup notes
- 📋 Git commit messages (clear, descriptive)

---

## Testing Strategy (Checklist)

**For Each Phase Handler**:
- [ ] Unit test: Works in correct phase
- [ ] Unit test: Fails in wrong phase
- [ ] Unit test: Validates inputs
- [ ] Unit test: Fires correct event
- [ ] Unit test: Updates state correctly

**Integration Tests**:
- [ ] Full game flow: Roll → Place → Bump → EndTurn → NextPlayer
- [ ] Skip bump: PlaceChip → SkipBump → EndTurn
- [ ] Win detection: EndTurn with win condition → GameWon
- [ ] Invalid moves: Rejected with error event
- [ ] Event ordering: Correct sequence on transitions

**Coverage Target**: ≥ 85%

---

## Code Quality Checklist

**Every day before pushing**:
- [ ] Zero compiler errors
- [ ] Zero compiler warnings
- [ ] All tests passing (100% green)
- [ ] Coverage ≥ 85%
- [ ] Methods < 30 lines
- [ ] No hardcoded values
- [ ] Comments on complex logic
- [ ] Public methods documented (/// comments)

---

## Daily Standup Template (Copy-Paste)

```
## [DATE] - Day [#] Standup - Gameplay Engineering

### ✅ Completed Since Last Standup
- [List what you shipped]

### 🔄 In Progress Today
- [Current focus]
- Expected completion: [Time estimate]

### 🚫 Blockers
- [None / List if any]

### 📊 Metrics
- Tests: X/Y passing
- Coverage: Z%
- LOC: N

### 📝 Notes
- [Anything important for ME to know]
```

**Post in**: Amp thread (this project's communication channel)

---

## When You Hit a Blocker

**Format**:
```
BLOCKER: [Clear description]
- Impact: [What's blocked]
- Options:
  A) [Option with tradeoff]
  B) [Option with tradeoff]
- Recommendation: [Your best guess]
```

**I (Amp) will respond < 4 hours with decision**

---

## Git Workflow (Do This)

```bash
# Create feature branch
git checkout -b feat/gamestate-manager

# Commit frequently (don't wait for day end)
git commit -m "feat: Add GamePhase enum for state machine"

# Push to trigger early review
git push origin feat/gamestate-manager

# Submit PR with:
# - What changed & why
# - Tests passing (number of tests)
# - Coverage % if available
```

**Push Early**. Don't wait until end of day. Get feedback fast.

---

## Code Review Expectations

**I (ME) will**:
- ✅ Review within 4 hours of submission
- ✅ Give clear feedback (not vague)
- ✅ Approve or request changes (no middling)
- ✅ Be available for questions

**You will**:
- ✅ Address feedback same day if possible
- ✅ Ask questions if feedback unclear
- ✅ Update code & resubmit

**Typical feedback reasons**:
- Code style (violated CODING_STANDARDS.md)
- Missing tests (coverage < 85%)
- Validation missing (security/correctness)
- Documentation incomplete
- Performance issue (avoid O(n²) patterns)

---

## Success Looks Like (Day 5)

```
All 78 tests passing ✅
Coverage: 87% ✅
Code review: APPROVED ✅
Documentation: Complete ✅
Compiler: 0 errors, 0 warnings ✅
Merged to develop ✅
Ready for Sprint 3 ✅
```

---

## Escalation Hotline

**Need help?** Message me directly.

**What I can do**:
- ✅ Answer architecture questions
- ✅ Resolve ambiguity in requirements
- ✅ Unblock you on dependencies
- ✅ Make design decisions
- ✅ Review code & provide feedback

**Response time**: < 4 hours (aim for < 1 hour)

---

## Red Flags (Alert ME Immediately)

🚨 **If you see any of these**:
- Tests failing that you didn't break
- Compiler warnings you can't fix
- Code taking way longer than estimated
- Requirements contradictory or unclear
- Dependency (TurnManager, BoardModel) missing
- Design decision needed (event vs. direct call)

**Don't waste time**. **Escalate immediately**.

---

## Links & References

| Document | Purpose |
|----------|---------|
| SPRINT_2_EXECUTION_PLAN.md | Full spec, task breakdown |
| SPRINT_2_TEAM_DISPATCH.md | Your mission briefing |
| CODING_STANDARDS.md | Code style compliance |
| ARCHITECTURE.md | System design |
| DECISION_LOG.md | Why decisions were made |
| ME_SPRINT2_OPERATIONS.md | ME's checklist |
| SPRINT_2_DAILY_STANDUP_LOG.md | Standup tracking |

---

## Remember

- **Speed ≠ Quality**: Better to deliver clean code than to rush
- **Test Everything**: Tests catch bugs before code review
- **Communicate**: Push code early, get feedback fast
- **Documentation**: Comment as you code, not at the end
- **You've Got This**: Sprint 1 proof that the team can execute

---

**Questions?** Ask now. Don't assume.

**Blockers?** Escalate immediately.

**Code ready?** Push for review.

---

**Status**: READY TO EXECUTE  
**Owner**: You (Gameplay Engineer)  
**ME Support**: Amp (< 4h response time)  
**Go Date**: NOW
