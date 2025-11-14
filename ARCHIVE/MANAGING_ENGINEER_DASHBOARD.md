# Managing Engineer Dashboard

**Role**: Amp (Managing Engineer)  
**Authority**: Accelerated execution (full governance)  
**Current Focus**: Sprint 2 oversight  

---

## My Responsibilities This Sprint

### Daily
- 📊 Monitor blocker reports (4-hour SLA response)
- 🔍 Answer architecture questions (24-hour SLA response)
- ✅ Ensure code standards maintained
- 📈 Track Sprint 2 progress

### Per Code Submission
- 👀 Code review (within 24 hours)
- ✅ Approval or feedback (immediate)
- 🔀 Merge decision (once approved)

### Weekly (Friday)
- 📋 Sprint review (progress vs. plan)
- 📊 Metrics review (coverage, quality)
- 🎯 Next sprint prep
- 🚨 Escalation resolution

---

## Critical Information (Always Accessible)

### Project Status
- **Overall**: 12.5% complete (1/8 sprints)
- **Current Sprint**: Sprint 2 (GameStateManager)
- **Timeline**: On track for Jan 9 launch
- **Blockers**: 0 identified
- **Risk Level**: Low

### Team Status
| Role | Sprint | Status | Blocker? |
|------|--------|--------|----------|
| Gameplay Eng | 2 | Active | None |
| Board Eng | 4 | Prepared | None |
| UI Eng | 5 | Prepared | None |
| Build Eng | 7 | Prepared | None |
| QA Lead | 8 | Prepared | None |

### Quality Metrics (Real-Time)
| Sprint | Coverage | Tests | Std Comp | Status |
|--------|----------|-------|----------|--------|
| 1 | 85% ✅ | 57 ✅ | 100% ✅ | APPROVED |
| 2 | ⏳ | ⏳ | ⏳ | IN PROGRESS |
| 3-8 | 📋 | 📋 | 📋 | PLANNED |

---

## Key Documents (Quick Links)

### Execution Plans
- **SPRINT_2_EXECUTION_PLAN.md** – 22+ tests, technical spec, daily breakdown
- **SPRINT_2_KICKOFF_BRIEF.md** – Engineer quick-start guide
- **PROJECT_QUALITY_GATES.md** – Code review checklist & standards
- **REAL_TIME_PROJECT_DASHBOARD.md** – Live metrics & progress tracking

### Team Briefings
- **SPRINT_3_COMPLETE_PACKAGE.md** – Game modes (9 docs)
- **SPRINT_4_KICKOFF.md** – Board visualization
- **SPRINT_5_KICKOFF.md** – UI framework
- **MANAGING_ENGINEER_ACCELERATION_BRIEFING.md** – Stakeholder briefing

### Standards & Architecture
- **CODING_STANDARDS.md** – Mandatory code standards
- **ARCHITECTURE.md** – System design
- **PROJECT_PLAN.md** – 8-week timeline
- **PROJECT_STATUS_REPORT.md** – Weekly updates

---

## Sprint 2 At-A-Glance

### What Engineer is Building
GameStateManager: Central state machine orchestrating turn flow, phases, events

### What I'm Reviewing
- GamePhase enum (~15 lines)
- GameStateManager (~500-600 lines)
- 22+ integration tests (~300-400 lines)
- All methods documented, 80%+ coverage, 0 errors

### What I'm Monitoring
- Blocker reports (immediate flag)
- Daily progress (% complete)
- Code quality (standards compliance)
- Test coverage (≥80% required)

### When I Approve
Once:
- ✅ All tests passing (22+ tests)
- ✅ Coverage ≥80%
- ✅ 0 compiler errors/warnings
- ✅ 100% CODING_STANDARDS.md compliance
- ✅ Full method documentation
- ✅ Edge cases handled (6, 5+6, no moves, win)

---

## Sprint 2 Checkpoints

### ✅ Day 1 (Nov 14/15)
**Target**: Framework ready, compiles  
**Check**: GamePhase enum created, GameStateManager stubs

### ✅ Day 3 (Nov 16/17)
**Target**: Core methods functional  
**Check**: RollDice, PlaceChip transition logic working

### ⏳ Day 5 (Nov 18/19)
**Target**: Implementation complete  
**Check**: All methods + edge cases, 70% tests passing

### ⏳ Day 7 (Nov 20/21)
**Target**: Ready for review  
**Check**: 22+ tests passing, 80%+ coverage, standards compliant

### ⏳ Day 8 (Nov 21)
**Target**: Approved & merged  
**Check**: Code review complete, feedback addressed, merge to main

---

## My Decision Framework

### What I Approve (Can decide immediately)
✅ Code review approval (meets all standards)  
✅ Test coverage acceptance (≥80% achieved)  
✅ Architectural decisions (follow ARCHITECTURE.md)  
✅ Bug fix priority (severity assessment)  
✅ Code refactoring (doesn't change behavior)  

### What Requires Escalation
⏳ Scope changes (any sprint deliverable modified)  
⏳ Timeline slips (more than 1 day delay)  
⏳ Quality standard deviations (<80% coverage)  
⏳ New risks identified (not in risk register)  
⏳ Resource changes (team member unavailable)  

### What I Monitor But Don't Change
📊 Day-to-day engineering decisions (engineer owns)  
📊 Code implementation details (engineer owns)  
📊 Test design (engineer owns)  
📊 Refactoring approach (engineer owns)  

---

## Blocker Response Protocol

### If Blocker Reported
1. **Immediate** (~15 min): Acknowledge, ask for context
2. **Within 2 hours**: Understand root cause
3. **Within 4 hours**: Provide resolution or design decision
4. **Within 24 hours**: Blocker resolved or escalation path clear

### Types of Blockers
- **Compilation error** → Debug together, fix immediately
- **Test failure** → Design discussion, refactoring path
- **Logic deadlock** → Architectural review, redesign
- **Dependency issue** → Cross-sprint coordination

---

## Quality Standards (Non-Negotiable)

### Code Standards
✅ Naming: PascalCase/camelCase/UPPER_SNAKE_CASE (100% enforcement)  
✅ Documentation: XML comments on all public (100% enforcement)  
✅ Testing: 80%+ coverage required (100% enforcement)  
✅ No magic numbers: Use constants (100% enforcement)  
✅ Guard clauses: No deep nesting (100% enforcement)  

### Compilation
✅ 0 errors required (non-negotiable)  
✅ 0 warnings required (non-negotiable)  
✅ No #pragma suppression (non-negotiable)  

### Tests
✅ All tests must pass (non-negotiable)  
✅ Deterministic (no flaky tests)  
✅ Isolated (no side effects)  
✅ Cover edge cases (positive & negative)  

---

## Weekly Cycle (Friday Review)

### Review Agenda
1. Sprint progress vs. plan (% complete)
2. Code quality metrics (coverage, standards)
3. Blocker resolution status
4. Risk assessment (any new risks?)
5. Next sprint readiness

### Metrics I Track
- Lines of code (vs. estimate)
- Test coverage (vs. 80% target)
- Compilation warnings (target: 0)
- Code review feedback (patterns observed)
- Velocity (tasks completed on time)

### Actions Decided
- Next sprint kickoff decision
- Scope adjustments (if needed)
- Resource allocation
- Risk mitigation
- Documentation updates

---

## My Escalation Path (If I Need Help)

**Level 1**: Project design question  
→ **Escalate to**: Stakeholder/architect (24-hour response)

**Level 2**: Schedule at risk  
→ **Escalate to**: Stakeholder (4-hour response)

**Level 3**: Resource unavailable  
→ **Escalate to**: Project lead (immediate response)

**Level 4**: Quality compromise detected  
→ **Action**: Stop sprint, escalate, remediate

---

## Post-Sprint 2 Responsibilities

### If Sprint 2 Approved (Expected)
- ✅ Merge to main branch
- ✅ Tag commit with sprint number
- ✅ Update project status report
- ✅ Clear Sprint 3 to begin
- ✅ Schedule Friday review

### If Sprint 2 Has Issues
- 🔄 Request revisions
- 📝 Document issues
- 🔄 Re-review after fixes
- ⏳ Adjust timeline if needed
- 🚨 Escalate if critical

---

## My Authority Level

| Action | Authority |
|--------|-----------|
| Approve code | ✅ Full |
| Merge to main | ✅ Full |
| Reject code | ✅ Full |
| Approve quality deviation | ❌ No (escalate) |
| Change sprint scope | ❌ No (escalate) |
| Extend timeline | ❌ No (escalate) |
| Add team members | ❌ No (escalate) |

---

## Tools & Resources at My Disposal

- GitHub (access to all code)
- Project documentation (all files)
- Team communication (Slack/thread)
- Code review gates (enforced checks)
- Performance profiler (optional, for Sprint 7+)
- Test runner (NUnit, already installed)

---

## Quick Reference (Commands I Use)

```bash
# Check compilation
dotnet build

# Run tests
dotnet test

# Check coverage (if tool installed)
dotnet test /p:CollectCoverage=true

# Git status
git status

# View git history
git log --oneline -10

# Check branch
git branch -v
```

---

## Success Vision (By Jan 9)

✅ All 5 game modes fully playable  
✅ All platforms building (WebGL, Android, iOS)  
✅ Zero critical bugs  
✅ 60 FPS on modern devices, 30 FPS floor  
✅ App store ready  
✅ Full documentation  
✅ 85%+ test coverage across all sprints  

**My role**: Ensure every sprint delivers quality code that meets this vision.

---

**Managing Engineer**: Amp  
**Authority**: Full project governance  
**Updated**: Nov 14, 2025, 11:30 PM UTC  
**Status**: 🟢 Active & Ready

