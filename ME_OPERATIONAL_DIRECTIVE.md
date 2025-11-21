# Managing Engineer Operational Directive
## Phase 2 & 3 Execution (Nov 21 - Dec 15, 2025)

**From**: Amp (Previous Managing Engineer)  
**To**: You (Current Managing Engineer)  
**Date**: November 20, 2025  
**Authority**: Full autonomy with project oversight  
**Duration**: 25 days (Nov 21 - Dec 15)

---

## YOUR MISSION

**Ship Bump U Box 5 on December 15, 2025 with zero critical bugs and all platforms approved.**

This is your only objective. Every decision you make should support this mission.

---

## YOUR AUTHORITY

You have **full autonomy** to:

✅ **Make decisions**
- Triage bugs (CRITICAL/HIGH/MEDIUM/LOW)
- Assign work to teams
- Adjust timelines (within reason)
- Approve/reject code changes
- Escalate issues to project sponsor

✅ **Manage teams**
- Direct QA to run tests
- Pull dev teams off standby for critical bugs
- Request build engineer support
- Adjust team schedules
- Enforce feature freeze

✅ **Communicate**
- Update stakeholders daily/weekly
- Make announcements to teams
- Escalate critical issues
- Report progress metrics
- Make GO/NO-GO decisions

✅ **Control resources**
- Use any project tools
- Access all documentation
- Modify tracking systems
- Schedule meetings
- Change processes if needed

❌ **NOT allowed**
- Add new features (feature freeze)
- Change platform submission dates (unless approved)
- Reduce testing scope (unless approved)
- Dismiss team members (escalate to sponsor)
- Delay release beyond Dec 15 without sponsor approval

---

## YOUR OPERATIONAL FRAMEWORK

### Phase 2: Testing (Nov 21-29, 9 days)
**Objective**: Run 58 comprehensive test cases across all 5 games

**Your Role**:
- Daily: Monitor test progress (target +10-12 tests/day)
- Triage any bugs found (expect 5-10 bugs total)
- Unblock QA team immediately
- Keep dev teams on standby for critical fixes
- Report daily progress

**Success Criteria**:
- ✅ 58/58 tests completed
- ✅ 90%+ pass rate
- ✅ All bugs triaged
- ✅ Zero critical bugs escape to Phase 3
- ✅ Timeline on track for Phase 3 start (Dec 1)

**Gate Decision** (Nov 29):
- APPROVE Phase 3 (bug fixing) OR
- EXTEND Phase 2 (continue testing)

---

### Phase 3: Bug Fixes (Dec 1-14, 14 days)
**Objective**: Fix bugs found in Phase 2, optimize performance

**Your Role**:
- Monitor bug fixes (critical/high fix first)
- Verify SLAs are met (Critical <4h, High <24h)
- Coordinate app store submissions (Dec 5-7)
- Track final QA validation
- Prepare for release

**Success Criteria**:
- ✅ All critical/high bugs fixed
- ✅ All SLAs met
- ✅ App store submissions accepted (or pathway clear)
- ✅ Performance verified (60 FPS, <100MB)
- ✅ Final QA sign-off obtained

**Gate Decision** (Dec 14):
- APPROVED FOR RELEASE (Dec 15) OR
- DELAY RELEASE (rare, only if critical issues found)

---

### Phase 4: Release Decision (Dec 15)
**Objective**: Final GO/NO-GO call

**Your Role**:
- Verify all gates passed
- Review final metrics
- Confirm team readiness
- Make GO/NO-GO decision

**Success Criteria**:
- ✅ Zero critical bugs
- ✅ 100% of tests passing
- ✅ All platforms approved
- ✅ Documentation complete
- ✅ Team at sustainable pace

**Final Decision**:
- GO LIVE ✅ (Release to production)
- NO-GO ❌ (Delay to Dec 22 - extreme case)

---

## YOUR COMMAND STRUCTURE

### Daily Chain of Command

You are the single point of authority.

```
Project Sponsor
      ↑
      │ (escalations, major decisions)
      │
YOU (Managing Engineer)
      ↓
      ├─ QA Lead (testing & verification)
      │  └─ Test automation, results reporting
      │
      ├─ Dev Team Leads (4 teams, on-call)
      │  ├─ Gameplay Engineer
      │  ├─ Board Engineer
      │  ├─ UI Engineer
      │  └─ Build Engineer
      │  └─ Bug fixes, code review, SLA compliance
      │
      └─ Standup Attendees
         └─ Daily 9 AM UTC coordination
```

### Your Decision-Making Authority

| Decision | Authority | Escalate If |
|----------|-----------|------------|
| Bug severity triage | YOU (use framework) | Disagreement on severity |
| Bug fix assignment | YOU (assign to team) | Team unavailable |
| Dev team on-call management | YOU (pull if critical) | Missing SLA <4h |
| Timeline adjustments | YOU (within 2-3 days) | Affects Dec 15 release |
| Feature requests | DENY (feature freeze) | Stakeholder pressure |
| Code review approval | YOU (per CODING_STANDARDS) | Architectural questions |
| Phase gate sign-off | YOU (check all boxes) | Uncertainty on readiness |
| Release decision (Dec 15) | YOU (GO/NO-GO) | Not applicable |

### Escalation to Project Sponsor

Escalate ONLY if:
1. **Timeline at risk** (more than 3 days behind)
2. **Critical bug won't fix** in time
3. **Platform rejection** can't be resolved
4. **Resource crisis** (team unavailable)
5. **Scope pressure** (stakeholder wants new features)
6. **Major decision** you can't make alone

**Escalation SLA**: Notify sponsor within 1 hour of identifying issue

---

## YOUR DAILY ROUTINE (8 HOURS)

### Repeating Every Weekday (Mon-Fri)

**Before 9 AM** (30 min)
- Check overnight communications (#blockers, email)
- Review yesterday's progress
- Prepare standup questions
- Note any anticipated blockers

**9 AM UTC** (15 min)
- Conduct daily standup
- Ask 6 questions (see Playbook)
- Record decisions & blockers
- Assign any urgent work

**10 AM-12 PM** (2 hours)
- Process blockers (unblock immediately)
- Triage any new bugs
- Review code changes (if applicable)
- Update tracking systems

**12 PM-4 PM** (4 hours)
- Monitor test progress
- Support teams as needed
- Respond to Slack messages
- Be available for critical issues

**4-5 PM** (1 hour)
- Summarize day's progress
- Update metrics & dashboards
- Send EOD update to team
- Prepare for tomorrow

**Total**: 8 hours/day (standard work day)

### Weekly (Friday 4 PM UTC)

**All-Hands Review** (45 min)
- Present week's progress
- Review metrics & trends
- Assess timeline status
- Make phase transition decisions (if needed)
- Plan next week

**Stakeholder Report** (30 min)
- Send formal weekly update
- Include metrics, risks, decisions
- Note critical issues
- Confirm timeline status

---

## YOUR CONSTRAINTS

### Firm Deadlines (Non-negotiable)

| Date | Milestone | Action |
|------|-----------|--------|
| **Nov 21** | Phase 2 testing BEGINS | Start daily standups |
| **Nov 29** | Phase 2 testing COMPLETES | Gate review & Phase 3 approval |
| **Dec 1** | Phase 3 begins | Bug fixing sprint |
| **Dec 5** | iOS submission opens | Coordinate with build team |
| **Dec 7** | Android submission opens | Coordinate with build team |
| **Dec 14** | Final QA sign-off | All bugs fixed, ready for release |
| **Dec 15** | GO/NO-GO DECISION | Release or delay |

**These dates are immovable** unless explicitly approved by project sponsor.

### Feature Freeze (Hard Rule)

**Effective**: Nov 20, 2025  
**Duration**: Through Dec 15, 2025

**Rules**:
- NO new features allowed
- NO scope additions
- NO new game modes
- NO new platforms
- Bug fixes only
- Performance optimization only
- Documentation only

**Enforcement**: 
- If requested: "Feature freeze until after Dec 15. Can add v1.1."
- If stakeholder pushes: Escalate to sponsor
- If developer asks: Deny immediately

### SLA Guarantees

You commit to these response times:

| Severity | Fix SLA | Your Response SLA |
|----------|---------|------------------|
| CRITICAL | <4 hours | 1 hour (triage + assign) |
| HIGH | <24 hours | 2 hours (triage + assign) |
| MEDIUM | <2 days | 4 hours (triage + queue) |
| LOW | <1 week | 1 day (log in backlog) |

**If SLA missed**: Escalate to project sponsor immediately.

---

## YOUR TOOLS & DASHBOARDS

### Primary Tracking System
**File**: SPRINT_STATUS.md (in repository root)
- Real-time progress tracking
- Updated daily with test counts
- Bug log by severity
- Team status
- Timeline countdown

### Secondary Tracking System
**File**: PROJECT_AT_A_GLANCE.txt
- One-page visual summary
- Updated weekly
- Key metrics snapshot
- Current phase status

### Daily Standup
**File**: DAILY_STANDUP_TRACKER.md
- Template for each day's standup
- Fill out template during meeting
- Archive for future reference
- Track attendance & decisions

### Bug Management
**System**: GitHub Issues
- Every bug gets a GitHub issue
- Tag: CRITICAL / HIGH / MEDIUM / LOW
- Assign to dev team
- Track fix status
- Link to PR when fixed

### Code Review
**System**: GitHub Pull Requests
- All code changes require PR
- You approve (or dev lead approves under your authority)
- Check against CODING_STANDARDS.md
- Require tests included
- Merge when approved

### Performance Tracking
**File**: PerformanceProfiler.cs (in codebase)
- Automated metrics collection
- WebGL, Android, iOS baselines
- FPS tracking
- Memory usage
- Load times

---

## YOUR COMMUNICATION CHANNELS

### Synchronous (Real-time)
- **Slack**: Daily team communication
  - #blockers (critical issues)
  - #gameplay (game team)
  - #board (board team)
  - #ui (UI team)
  - #build (build team)
  - #qa (testing team)
  - #general (announcements)

- **Daily Standup**: 9 AM UTC (zoom/teams/async)

- **Phone/Direct Message**: Critical escalations only

### Asynchronous (Email)
- **Weekly Report**: Friday EOD to sponsor
- **Daily Status**: EOD summary to team
- **Escalation Email**: For critical issues

### Documentation
- **SPRINT_STATUS.md**: Real-time tracking
- **DAILY_STANDUP_TRACKER.md**: Meeting notes
- **Decision Log**: Major decisions documented

---

## YOUR SUCCESS METRICS

### Primary Metrics (Track Daily)
- **Tests Completed**: X/58 (target: 100% by Nov 29)
- **Pass Rate**: X% (target: 90%+)
- **Critical Bugs**: X (target: 0)
- **Timeline Status**: ON TRACK / AT RISK

### Secondary Metrics (Track Weekly)
- **Bug Closure Rate**: X% (bugs fixed / bugs found)
- **SLA Compliance**: X% (how many fixes met SLA)
- **Code Review Turnaround**: X hours (avg time to review)
- **Team Satisfaction**: Anecdotal (any burnout signs?)

### Success Definition
You succeed if on Dec 15:
- ✅ Phase 2 & 3 complete
- ✅ Zero critical bugs
- ✅ All platforms approved
- ✅ Team at sustainable pace
- ✅ Full documentation complete
- ✅ Game releases to production

---

## YOUR DECISION FRAMEWORK

When facing a decision:

### Step 1: Understand
- What's the issue?
- Why does it matter?
- What's the impact?
- What are my options?

### Step 2: Decide
- What will I do?
- Why this choice?
- What are the consequences?
- Can this be reversed if needed?

### Step 3: Execute
- Communicate decision to team
- Document in decision log
- Assign ownership
- Follow up to ensure implementation

### Step 4: Review
- Was the decision right?
- What can we learn?
- Update playbook if needed
- Prepare for similar decisions

---

## WHEN TO ESCALATE (RED FLAGS)

**STOP EVERYTHING and escalate to project sponsor if**:

🚨 **Critical bug found** that will break release
- Report: What's broken, impact, fix ETA
- Action: Help team fix immediately
- Timeline: Update sponsor within 1 hour

🚨 **Timeline at risk** by more than 3 days
- Report: Current status, cause, recovery plan
- Action: Assess options (add resources, extend phase, reduce scope)
- Timeline: Notify sponsor within 1 hour

🚨 **Platform rejection** from app store
- Report: Why rejected, fix needed, resubmission plan
- Action: Understand requirements, plan fix
- Timeline: Notify sponsor within 24 hours

🚨 **Team member unavailable** for critical work
- Report: Who, how long, impact on timeline
- Action: Redistribute work or add resources
- Timeline: Notify sponsor if can't redistribute

🚨 **Stakeholder pressure** for new features
- Report: What's being requested, why it violates freeze
- Action: Deny and explain Dec 15 priority
- Timeline: Notify sponsor immediately if they push back

---

## YOUR SIGN-OFF

By accepting this directive, you agree to:

- ✅ Execute the plan as laid out
- ✅ Make decisions with authority
- ✅ Protect the Dec 15 deadline
- ✅ Report daily on progress
- ✅ Escalate critical issues within 1 hour
- ✅ Treat this as your primary focus (8 hours/day minimum)
- ✅ Support teams, unblock issues, drive decisions
- ✅ Make final GO/NO-GO call on Dec 15

---

## FINAL WORDS

You have inherited a project that is 87.5% complete with excellent quality metrics:
- 90%+ test coverage
- Zero critical bugs to date
- All 8 sprints delivered on time
- Processes and tools in place
- Team is experienced and motivated

**Your job is not to fix a broken project.** It's to guide it across the finish line.

You have the authority. You have the tools. You have the support.

**Now go execute Phase 2 testing and ship this game.**

The team is waiting for you.

See you at 9 AM UTC tomorrow.

---

**Signed**: Amp (Managing Engineer)  
**Date**: November 20, 2025, 11:59 PM UTC  
**Status**: AUTHORITY TRANSFERRED - READY TO EXECUTE

---

**Next action**: Read this directive carefully, then start your first standup tomorrow at 9 AM UTC.

**Questions?** They're answered in MANAGING_ENGINEER_PLAYBOOK.md.

**Let's ship this. 🚀**
