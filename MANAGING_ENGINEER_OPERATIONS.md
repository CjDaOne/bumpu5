# Managing Engineer Operations Framework

**Role**: Managing Engineer Agent  
**Authority**: Full project coordination, team management, decision-making  
**Effective Date**: Nov 14, 2025  
**Last Updated**: Nov 14, 2025

---

## I. Team Structure & Activation

### Five Subagent Teams (On-Demand)

| Team | Lead Focus | Primary Scope |
|------|-----------|---------------|
| **Gameplay Engineering** | Core game logic, rules, game modes (Game1-5) | `Assets/Scripts/Game/`, `Assets/Scripts/Managers/` |
| **UI/UX Engineering** | Interface, menus, HUD, popups, screens | `Assets/Scripts/UI/`, `Assets/Prefabs/UI/` |
| **Board & Interaction** | Board visuals, cells, animations, tap detection | `Assets/Scripts/Board/`, `Assets/Prefabs/Board/` |
| **Build & Platform** | Multi-platform builds, optimization, CI/CD | Build settings, deployment configs |
| **QA & Testing** | Playtesting, bug triage, release validation | Test plans, bug reports, release notes |

### Activation Protocol

Teams are generated **as needed** for specific sprint tasks:
1. Managing Engineer defines sprint goals
2. Teams are instantiated with clear deliverables
3. Teams work in parallel on assigned features
4. Daily standups capture progress/blockers
5. Integration happens at sprint review

---

## II. Daily Operations & Workflows

### A. Sprint Kickoff (Managing Engineer Responsibility)

**Input**: Previous sprint retro + product backlog  
**Output**: Sprint plan document + team assignments

```
1. Define Sprint Goal (1-3 sentences)
2. Decompose into Stories per Team
3. Assign Story Points & Acceptance Criteria
4. Identify Cross-Team Dependencies
5. Schedule Daily Standups (time + participants)
6. Document in SPRINT_N_EXECUTION_PLAN.md
```

### B. Daily Standup Protocol

**Time**: 15 minutes  
**Participants**: Managing Engineer + all active teams  
**Format**:

Each team reports:
- ✅ **Completed**: What shipped since last standup
- 🔄 **In Progress**: What's being worked on now
- 🚫 **Blockers**: Any impediments (need managing engineer help)

**Managing Engineer Action Items**:
- Unblock teams immediately (escalate if needed)
- Adjust sprint plan if scope threatens delivery
- Document blockers in SPRINT_N_STATUS.md

### C. Code Review & Quality Gate

**Trigger**: When a team completes a task  
**Reviewer**: Managing Engineer (+ peer review if high-risk)

**Review Checklist**:
- [ ] Code follows CODING_STANDARDS.md
- [ ] Tests written (target 80%+ coverage)
- [ ] Documentation updated (inline comments, README)
- [ ] No console errors/warnings
- [ ] Builds successfully
- [ ] Acceptance criteria met

**Decision**: LGTM (approve) → Merge to develop  
**Outcome**: NEEDS WORK → Team fixes + resubmit

### D. Integration Testing

**Trigger**: When multiple teams' code converges  
**Responsibility**: Board team (owner) + impacted teams

**Protocol**:
1. Board team integrates feature from other teams
2. Test on all target platforms (WebGL, Android, iOS)
3. Log integration bugs to issue tracker
4. Manage dependency updates carefully

---

## III. Decision-Making Framework

### Authority Levels

| Decision Type | Authority | Documentation |
|---------------|-----------|---------------|
| Sprint scope / team assignments | Managing Engineer | SPRINT_N_EXECUTION_PLAN.md |
| Architecture / design patterns | Managing Engineer | ARCHITECTURE.md + decision record |
| Code style violations | Managing Engineer | CODING_STANDARDS.md + PR comment |
| Performance / optimization | Managing Engineer | DECISION_LOG.md + benchmarks |
| Bug severity / triage | Managing Engineer | Bug report + priority label |
| Release readiness | Managing Engineer | QA sign-off + checklist |

### Escalation Path

```
Team Lead can't resolve → Managing Engineer reviews
↓
If ambiguous → Document decision + rationale in DECISION_LOG.md
↓
If blocking 2+ teams → All-hands discussion documented in DECISION_LOG.md
↓
Managing Engineer makes final call (communicated to all teams)
```

### Record Everything

Every decision is recorded:
- **What**: The decision/change
- **Why**: Rationale & constraints
- **Impact**: Affected teams/scope
- **Owner**: Who's responsible
- **Date**: When decided

*File*: `DECISION_LOG.md` (append-only)

---

## IV. Code Review Standards

### When Submitted for Review

Team submits PR/branch with:
1. **Description**: What changed & why
2. **Acceptance Criteria**: All items marked ✅
3. **Test Results**: Coverage report attached
4. **Screenshots/Videos**: If UI-related

### Managing Engineer Review

**Fast-track** (approve in 1-2 hours):
- Bug fixes (clear, minimal scope)
- Documentation updates
- Refactoring with tests

**Standard** (review thoroughly):
- New features (architecture, design)
- Cross-team integration points
- Performance-critical code

**Block & Request Changes**:
- Violates standards (naming, structure)
- Missing tests or documentation
- Doesn't meet acceptance criteria
- Performance regression detected

---

## V. Sprint Lifecycle

### Day 1: Planning

- [ ] Managing Engineer defines sprint goal
- [ ] Teams receive story cards + acceptance criteria
- [ ] Dependencies mapped in sprint doc
- [ ] First standup scheduled

### Days 2-4: Execution

- [ ] Daily standups (identify blockers immediately)
- [ ] Teams submit code for review
- [ ] Managing Engineer unblocks teams
- [ ] Mid-sprint checkpoint (update SPRINT_N_STATUS.md)

### Day 5: Review & Retro

- [ ] All code merged (or documented as carry-over)
- [ ] Demo of completed features
- [ ] QA sign-off on quality
- [ ] Team retrospective (what went well, what didn't)
- [ ] Document lessons in SPRINT_N_COMPREHENSIVE.md

### Post-Sprint

- [ ] Archive sprint docs
- [ ] Plan next sprint with retro learnings
- [ ] Update PROJECT_STATUS_UPDATE.md with progress

---

## VI. Communication Standards

### Synchronous (Standups & Meetings)

- **Daily Standup**: 15 min (blockers only)
- **Sprint Review**: Friday, 30 min (demos + retro)
- **Ad-hoc Escalation**: < 1 hour response time

### Asynchronous (Documentation)

All decisions, blockers, and changes documented in:
- `SPRINT_N_EXECUTION_PLAN.md` (scope)
- `SPRINT_N_STATUS.md` (daily progress)
- `DECISION_LOG.md` (all decisions)
- `SPRINT_N_COMPREHENSIVE.md` (retro)
- Inline code comments (context for future agents)

### Future Agent Onboarding

When a new agent joins:
1. Read: `SUBAGENT_TEAMS.md` (team structure)
2. Read: This document (operations framework)
3. Read: `CODING_STANDARDS.md` (code style)
4. Read: Latest `SPRINT_N_EXECUTION_PLAN.md` (current sprint)
5. Read: `DECISION_LOG.md` (why decisions were made)
6. Attend: Next daily standup (get context)

---

## VII. Quality Gates & Standards

### Code Quality

- **Linter**: No warnings (ESLint/Roslyn standards)
- **Tests**: 80%+ coverage for logic, 60%+ for UI
- **Documentation**: All public methods documented
- **Performance**: Builds & deploys in < 5 min

### Release Readiness

Before shipping to app stores:
- [ ] Zero critical bugs
- [ ] All 5 game modes playable end-to-end
- [ ] All platforms tested (WebGL, Android, iOS)
- [ ] QA sign-off complete
- [ ] Release notes reviewed
- [ ] App store metadata reviewed

---

## VIII. Managing Engineer Responsibilities

### Daily
- [ ] Run standup (15 min)
- [ ] Review submitted code (2-3 hours)
- [ ] Unblock teams (immediate)
- [ ] Update sprint status doc

### Weekly
- [ ] Sprint review & retro (Friday)
- [ ] Plan next sprint (Friday afternoon)
- [ ] Review architectural decisions
- [ ] Update project dashboard

### Per-Sprint
- [ ] Generate team assignments
- [ ] Define sprint goals
- [ ] Track burndown & velocity
- [ ] Document lessons learned

---

## IX. File Organization & Naming

### Sprint Documentation

```
SPRINT_N_EXECUTION_PLAN.md    ← Scope, stories, acceptance criteria
SPRINT_N_STATUS_DAY#.md       ← Daily standups (append-only)
SPRINT_N_COMPREHENSIVE.md     ← Retro, metrics, lessons learned
```

### Decision Records

```
DECISION_LOG.md               ← Append-only, all decisions with rationale
ARCHITECTURE.md               ← System design, module structure
CODING_STANDARDS.md           ← Style, naming, patterns
```

### Project Artifacts

```
PROJECT_STATUS_UPDATE.md      ← High-level progress (updated weekly)
REAL_TIME_PROJECT_DASHBOARD.md ← Burndown, velocity, blockers
DELIVERABLES_MANIFEST.txt     ← What's shipped, what's pending
```

---

## X. Transition Between Sprints

### End-of-Sprint Checklist

- [ ] All code merged to `develop`
- [ ] Tests passing (CI/CD green)
- [ ] No critical bugs open
- [ ] Sprint metrics recorded
- [ ] Retrospective notes documented
- [ ] Next sprint goals defined
- [ ] Teams briefed on next sprint

### Carry-Over Management

If story doesn't finish:
1. Document why (blocker, scope creep, estimation miss)
2. Mark as "carry-over" to next sprint
3. Note any technical debt or cleanup needed
4. Adjust team velocity estimation for next sprint

---

## XI. How Subagents Interact With Managing Engineer

### Request Pattern (For Teams)

```
**From**: [Team Name]
**Subject**: [Task ID] - [Description]

We need:
- [Blocker/Decision/Approval needed]

Impact:
- [Who else is affected]

Options:
1. [Option A with tradeoffs]
2. [Option B with tradeoffs]

Recommendation: Option X because [rationale]
```

### Managing Engineer Response

```
**Decision**: [Approved/Needs revision/Escalating]
**Reasoning**: [Why this decision]
**Next Steps**: [What team does now]
**Documentation**: [File to update]
```

---

## XII. Current Sprint Info

**Current Sprint**: [Initialized on Nov 14, 2025]  
**Sprint Lead**: Managing Engineer Agent  
**Teams Active**: [To be assigned per sprint]  
**Sprint Duration**: 5 calendar days (Mon-Fri)  
**Next Kickoff**: [Scheduled]

---

## Appendix: Quick Reference for Teams

### When to Escalate to Managing Engineer

- 🚫 Blocked by another team (not in your scope)
- ❓ Architectural decision needed (affects other teams)
- ⏰ Sprint scope threatened (estimation was wrong)
- 🔄 Code review blocked (feedback unclear)
- 📋 Need to change acceptance criteria (scope creep)

### When Managing Engineer Escalates to You

- **Code Review FAILED**: Fix and resubmit
- **Blocker Identified**: Help other team or adjust plan
- **Integration Issue**: Collaborate with impacted teams
- **Decision Made**: Implement as directed in DECISION_LOG.md

---

**Document Owner**: Managing Engineer Agent  
**Last Reviewed**: Nov 14, 2025  
**Next Review**: End of Sprint 1  
**Status**: Active - All teams must follow this framework
