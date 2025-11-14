# Sprint 2 Team Dispatch - Gameplay Engineering

**From**: Managing Engineer (Amp)  
**To**: Gameplay Engineer Agent  
**Date**: Nov 14, 2025  
**Sprint**: Sprint 2 - State Machine & Game Flow Control  
**Status**: URGENT - EXECUTE IMMEDIATELY

---

## Mission Brief

You are cleared to begin Sprint 2 execution immediately. This is the critical path for the entire project. No date constraints—move at full speed until complete.

**Critical Path**: GameStateManager blocks all downstream work (Sprints 3-8). Every hour counts.

---

## Your Assignment

### Primary Responsibility
Implement the complete GameStateManager state machine that orchestrates all gameplay phases and events.

### Deliverables
- GamePhase enum (7 phases)
- GameStateManager class (600+ lines)
- 78+ unit & integration tests
- Full documentation
- Code review approval

### Success Metric
**All code merged to `develop` with ME approval before proceeding to Sprint 3**

---

## 5-Day Execution Plan

### Day 1: GamePhase Enum & Scaffolding (2 hours)
**Tasks**:
1. Create `Assets/Scripts/Game/GamePhase.cs` with enum definition
2. Create `Assets/Scripts/Managers/GameStateManager.cs` skeleton
3. Declare all events, properties, methods
4. **Commit**: `feat: Add GamePhase enum for state machine`
5. **Commit**: `feat: Add GameStateManager skeleton with event system`

**Deliverables Due**: Compiling code, ready for Day 2

---

### Day 2: Phase Logic & Transitions (6 hours)
**Tasks**:
1. Implement `TransitionToPhase()` with validation
2. Implement `IsValidTransition()` with full transition table
3. Implement `RollDice()` phase handler
4. Implement `PlaceChip()` phase handler
5. Implement `BumpChip()` + `SkipBump()` handlers
6. Write unit tests for each method
7. **Commit**: `feat: Implement phase transition logic with validation`
8. **Commit**: `feat: Implement RollDice phase with DiceManager integration`
9. **Commit**: `feat: Implement MoveChip phase with validation`
10. **Commit**: `feat: Implement BumpOpponent phase with optional skip`

**Deliverables Due**: 5 playable phases, 20+ passing tests

---

### Day 3: Win & End Game Logic (6 hours)
**Tasks**:
1. Implement `EndTurn()` phase handler with player switching
2. Implement `CheckWinCondition()` with mode-specific checking
3. Implement `DeclareWin()` for manual win declarations
4. Implement GameWon & GameOver phase handlers
5. Write unit tests (15+ tests)
6. **Commit**: `feat: Implement EndTurn phase with player switching`
7. **Commit**: `feat: Implement win detection with mode-specific checks`
8. **Commit**: `feat: Implement GameWon and GameOver terminal phases`

**Deliverables Due**: Complete game loop (start → end), 25+ passing tests

---

### Day 4: Integration & Validation (6 hours)
**Tasks**:
1. Write full integration test suite (25+ tests covering full game flow)
2. Write GamePhase enum unit tests (8 tests)
3. Verify all 78+ tests pass
4. Verify coverage ≥ 85%
5. Test integration with Sprint 1 classes (Player, Chip, BoardCell, etc.)
6. Fix any issues found
7. **Commit**: `test: Add 25+ integration tests for GameStateManager`
8. **Commit**: `test: Add unit tests for GamePhase enum`

**Deliverables Due**: All tests passing, coverage ≥ 85%, no blockers

---

### Day 5: Code Review & Documentation (4 hours)
**Tasks**:
1. Clean up code, add inline documentation
2. Ensure CODING_STANDARDS.md compliance
3. Verify zero compiler warnings
4. Create comprehensive class documentation
5. Update ARCHITECTURE.md with state machine diagram
6. Wait for Managing Engineer code review
7. Fix any review feedback
8. **Commit**: `docs: Add comprehensive GameStateManager documentation`
9. **Commit**: `chore: Code review approved - Sprint 2 ready`

**Deliverables Due**: Approved, merged code + documentation

---

## Key References

📋 **Full Plan**: `/home/cjnf/Bump U/SPRINT_2_EXECUTION_PLAN.md`  
📋 **Coding Standards**: `/home/cjnf/Bump U/CODING_STANDARDS.md`  
📋 **Architecture**: `/home/cjnf/Bump U/ARCHITECTURE.md`  
📋 **Decision Log**: `/home/cjnf/Bump U/DECISION_LOG.md`  

---

## Important Constraints & Guidelines

### Code Quality (Non-negotiable)
- ✅ Zero compiler errors/warnings
- ✅ 78+ unit/integration tests
- ✅ Coverage ≥ 85%
- ✅ Full documentation for all public methods
- ✅ Event-driven architecture (use events, not tight coupling)

### Architecture Requirements
- GameStateManager must NOT directly call UI code
- GameStateManager must NOT directly instantiate game modes
- Use events to communicate state changes
- Keep state transitions in validation table
- All validation must fail gracefully with OnInvalidAction event

### Testing Strategy
- Unit tests for each phase handler
- Integration tests for full game flow
- Use mock/fake objects for dependencies (BoardModel, TurnManager, etc.)
- Test happy path AND error cases
- Coverage calculator must show ≥ 85%

### Code Style (From CODING_STANDARDS.md)
- C# naming conventions (PascalCase for public, camelCase for private)
- 2-space indentation (not tabs)
- XML documentation comments on all public members
- Meaningful variable names (avoid abbrev., be explicit)
- Methods < 30 lines where possible

---

## Daily Standups

**Time**: Each morning (async if needed)  
**Format**: Email or thread message with:
- ✅ Completed since yesterday
- 🔄 Working on today
- 🚫 Blockers (if any)

**Managing Engineer** (Amp) will:
- Respond within 4 hours to blockers
- Review code once submitted
- Unblock you immediately
- Keep project on track

---

## How to Escalate Blockers

If you hit a blocker:

1. **Document it**: What's blocked, why, impact
2. **Propose options**: A) Workaround, B) Design change, C) Clarification needed
3. **Message Managing Engineer**: Clear, concise escalation
4. **Wait < 4 hours for response**: ME will unblock or redirect

**Example escalation**:
```
BLOCKER: IGameMode interface not defined
- Blocks: CheckWinCondition() implementation
- Impact: Can't complete win detection (Day 3 task)

Options:
A) Define IGameMode interface now (15 min)
B) Move to Sprint 3, assume basic interface
C) Hardcode for now, refactor later

Recommendation: Option A (cleanest)
```

---

## Success = Project Success

This sprint is the foundation for everything that follows. Sprints 3-8 all depend on GameStateManager working correctly.

**Quality over speed**. Better to deliver clean, well-tested code that passes review than to rush and need rework.

---

## Resources Available

- **Codebase**: `/home/cjnf/Bump U/` (full access)
- **Sprint 1 Code**: All in `Assets/Scripts/` (Player, Chip, BoardCell, DiceManager, TurnManager, etc.)
- **Git**: Push commits early and often
- **Documentation**: Update as you go, don't wait for Day 5
- **Managing Engineer**: Available for questions, escalations, code review

---

## GO

You're cleared to start immediately. Hit the ground running on Day 1 tasks.

Current sprint momentum is strong (Sprint 1 at 100%, all systems green). Keep it up.

Questions? Ask. Blockers? Escalate. Code ready? Push for review.

Let's build this.

---

**From**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025  
**Status**: DISPATCH AUTHORIZED - BEGIN EXECUTION
