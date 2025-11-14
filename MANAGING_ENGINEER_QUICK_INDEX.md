# Managing Engineer Quick Index

**Purpose**: One-stop reference for all project documents  
**Updated**: Nov 14, 2025  
**For**: Amp (Managing Engineer) - My reference guide

---

## Today's Status (At a Glance)

```
Sprint 1: ✅ COMPLETE (7 classes, 57 tests, 85%+ coverage)
Sprint 2: 🟡 ACTIVE (execution begins immediately)
Status:   🟢 ON TRACK FOR JAN 9 LAUNCH
Blockers: 0
Risk:     LOW
Team:     5/5 engineers ready
```

---

## Documents I Need (By Responsibility)

### 🔍 Daily Monitoring (What I Check Every Day)

**Blocker Detection**:
- REAL_TIME_PROJECT_DASHBOARD.md (check "Blockers" section)
- Any reported in daily standup thread

**Progress Tracking**:
- REAL_TIME_PROJECT_DASHBOARD.md (update Sprint 2 progress %)
- Check against SPRINT_2_EXECUTION_PLAN.md (day-by-day timeline)

**Team Status**:
- MANAGING_ENGINEER_DASHBOARD.md (my status reference)
- REAL_TIME_PROJECT_DASHBOARD.md (team section)

### 👀 Code Review (What I Verify When Code Arrives)

**Checklist**:
- PROJECT_QUALITY_GATES.md (all mandatory items)
- CODING_STANDARDS.md (naming, documentation)
- 0 errors, 0 warnings, ≥80% coverage, all tests pass

**Approval Decision**:
- If all checks: ✅ APPROVE & MERGE
- If issues: ✗ REQUEST REVISIONS (specific feedback)
- If blocker: 🚨 ESCALATE (design discussion needed)

### 📊 Weekly Review (What I Do on Friday)

**Preparation**:
- REAL_TIME_PROJECT_DASHBOARD.md (update all metrics)
- MANAGING_ENGINEER_DASHBOARD.md (prepare agenda)
- Check SPRINT_2_EXECUTION_PLAN.md (vs. actual progress)

**During Review**:
- Progress: % complete vs. plan
- Quality: Coverage, standards compliance
- Risks: Any new issues?
- Next: Readiness for Sprint 3

**After Review**:
- Update dashboard with new metrics
- Document decisions
- Brief team on next steps

### 🚨 Blocker Response (What I Do When Issues Arise)

**Protocol** (in MANAGING_ENGINEER_DASHBOARD.md):
1. Acknowledge immediately
2. Understand root cause (2 hours)
3. Provide solution or escalation (4 hours max)
4. Document resolution (same day)

**Escalation Criteria**:
- Compilation deadlock → Technical design review
- Logic deadlock → Architecture discussion
- Resource unavailable → Leadership decision
- Quality deviation → Scope/timeline review

---

## Key Documents (Quick Links to Content)

### 📋 Sprint 2 (Active)

**For Execution**:
```
Read: SPRINT_2_EXECUTION_PLAN.md
      ↓ (daily progress check)
```

**For Communication**:
```
Reference: SPRINT_2_KICKOFF_BRIEF.md (what engineer received)
          SPRINT_2_EXECUTION_COMMAND.md (authority briefing)
```

**For Quality Verification**:
```
Check against: PROJECT_QUALITY_GATES.md
              CODING_STANDARDS.md
```

### 📊 Project Management

**Real-Time Tracking**:
```
Update: REAL_TIME_PROJECT_DASHBOARD.md (daily)
        ↓ (progress %, team status, risks)
```

**My Operations**:
```
Reference: MANAGING_ENGINEER_DASHBOARD.md (my role, decisions)
          MANAGING_ENGINEER_ACCELERATION_BRIEFING.md (authority, governance)
```

### 📚 Standards & Architecture

**Code Quality**:
```
Enforce: CODING_STANDARDS.md (mandatory, no exceptions)
        PROJECT_QUALITY_GATES.md (review checklist)
```

**System Design**:
```
Reference: ARCHITECTURE.md (system organization)
          PROJECT_PLAN.md (8-week timeline, overall strategy)
```

---

## Decision Matrix (When to Do What)

| Situation | Document | Action | SLA |
|-----------|----------|--------|-----|
| Code submitted | PROJECT_QUALITY_GATES.md | Review & approve | 24h |
| Blocker reported | MANAGING_ENGINEER_DASHBOARD.md | Assess & respond | 4h |
| Week ending | REAL_TIME_PROJECT_DASHBOARD.md | Update metrics & review | Friday |
| Question asked | Any sprint document | Answer from spec | 24h |
| Issue found | PROJECT_QUALITY_GATES.md | Request revision | 24h |
| Sprint ready | SPRINT_N_KICKOFF.md | Approve & merge | Immediate |
| Risk identified | REAL_TIME_PROJECT_DASHBOARD.md | Document & escalate | 4h |
| Timeline slip | MANAGING_ENGINEER_DASHBOARD.md | Assess & escalate | 24h |

---

## My Authority (What I Can/Cannot Do)

### ✅ I CAN (Full Authority)
- Approve/reject code (quality gates)
- Merge to main branch
- Enforce quality standards
- Approve sprint completion
- Make architectural decisions
- Provide design guidance

### ❌ I CANNOT (Requires Escalation)
- Change sprint scope
- Extend deadline beyond Jan 9
- Deviate from quality standards
- Add team members
- Change project strategy

---

## Contact Quick Links

**File Locations** (all in `/home/cjnf/Bump U/`):
- SPRINT_2_EXECUTION_PLAN.md
- PROJECT_QUALITY_GATES.md
- REAL_TIME_PROJECT_DASHBOARD.md
- MANAGING_ENGINEER_DASHBOARD.md
- CODING_STANDARDS.md
- ARCHITECTURE.md

**GitHub**:
- https://github.com/CjDaOne/bumpu5
- Branch: main
- All documentation committed

**Team**:
- Gameplay Engineer: Sprint 2 (active)
- Board Engineer: Sprint 4 (ready)
- UI Engineer: Sprint 5 (ready)
- Build Engineer: Sprint 7 (ready)
- QA Lead: Sprint 8 (ready)

---

## Success Metrics (Track These)

### Sprint 2 Metrics
- Code coverage: ≥80% (target)
- Tests: 22+ passing (all)
- Compilation: 0 errors, 0 warnings
- Standards: 100% compliance
- Timeline: On schedule

### Project Metrics
- Overall: 12.5% complete (1/8 sprints)
- Timeline: On track for Jan 9
- Blockers: 0 identified
- Risk: LOW
- Team readiness: 100%

---

## Daily Checklist (Copy & Use)

```
□ Check for new blocker reports
□ Verify code quality standards if new submission
□ Update REAL_TIME_PROJECT_DASHBOARD.md progress
□ Answer any standing questions (24h SLA)
□ Confirm no new risks identified
□ Monitor timeline vs. plan (SPRINT_2_EXECUTION_PLAN.md)
□ If Friday: Prepare for weekly review
```

---

## Weekly Checklist (Friday)

```
□ Gather all metrics from this week
□ Update REAL_TIME_PROJECT_DASHBOARD.md
□ Review progress vs. SPRINT_2_EXECUTION_PLAN.md
□ Assess any risks identified
□ Prepare agenda for team review
□ Run sprint review meeting
□ Document decisions in MANAGING_ENGINEER_DASHBOARD.md
□ Brief team on next steps
□ Update MANAGING_ENGINEER_ACCELERATION_BRIEFING.md if needed
```

---

## Escalation Triggers (When to Flag)

🚨 **Immediate** (Report within 1 hour):
- Compilation deadlock
- Test failure indicating design issue
- Code quality catastrophic

🔴 **High** (Report within 4 hours):
- Blocker preventing progress
- Resource unavailable
- Timeline risk identified

🟡 **Medium** (Report within 24 hours):
- Quality standards deviation
- New architectural issues
- Team concerns

---

## My Response SLA (Commit to These)

| Request Type | SLA | Status |
|--------------|-----|--------|
| Blocker report | 4 hours | ✅ Committed |
| Code review | 24 hours | ✅ Committed |
| Architecture question | 24 hours | ✅ Committed |
| Bug fix priority | 24 hours | ✅ Committed |
| Emergency issue | 2 hours | ✅ Committed |

---

## Key Numbers to Remember

- Sprints total: 8
- Current sprint: 2
- Teams: 5 engineers
- Completion target: Jan 9, 2026
- Test coverage required: 80%+
- Code standards: 100% compliance
- Blocker response: 4-hour SLA
- Code review: 24-hour SLA

---

## If You Forget Something...

**"What should I check daily?"**
→ REAL_TIME_PROJECT_DASHBOARD.md

**"Can I approve this code?"**
→ PROJECT_QUALITY_GATES.md (run checklist)

**"What's my authority level?"**
→ MANAGING_ENGINEER_DASHBOARD.md (authority section)

**"How do I handle a blocker?"**
→ MANAGING_ENGINEER_DASHBOARD.md (blocker protocol)

**"What's the sprint plan?"**
→ SPRINT_2_EXECUTION_PLAN.md

**"What's the timeline?"**
→ REAL_TIME_PROJECT_DASHBOARD.md or PROJECT_PLAN.md

**"What are the standards?"**
→ CODING_STANDARDS.md or PROJECT_QUALITY_GATES.md

---

## Status (Right Now)

✅ Authority: Granted & Active  
✅ Documentation: Complete  
✅ Team: Ready  
✅ Blockers: 0  
✅ Timeline: On track  
🟡 Sprint 2: Ready to execute  

**Ready to proceed. Monitoring daily. All systems operational.**

---

**Last Updated**: Nov 14, 2025  
**For**: Amp (Managing Engineer)  
**Status**: Active & Current  
**Next Review**: Tomorrow morning

