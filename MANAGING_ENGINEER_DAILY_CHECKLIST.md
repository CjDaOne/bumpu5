# Managing Engineer Daily Checklist
## Nov 21 - Dec 15, 2025 (Phase 2 & 3)

Use this checklist every single day. Print it out.

---

## BEFORE 9 AM UTC (Morning Prep - 30 min)

### Check Communications (5 min)
- [ ] Read Slack #blockers channel (any overnight escalations?)
- [ ] Check email for urgent messages
- [ ] Review GitHub issues (new bugs reported?)
- [ ] Check if SPRINT_STATUS.md was updated overnight

### Review Yesterday (5 min)
- [ ] Read yesterday's standup notes
- [ ] Check if bugs were fixed as promised
- [ ] Verify SLA compliance (Critical <4h, High <24h?)
- [ ] Note any patterns in bugs found

### Prepare for Standup (10 min)
- [ ] Open DAILY_STANDUP_TRACKER.md
- [ ] Fill in [DATE] placeholders for today
- [ ] Review yesterday's metrics (test count, pass rate, bugs)
- [ ] Calculate expected progress for today (should be +10-12 tests)
- [ ] Prepare any announcements or decisions needed
- [ ] Flag any known issues to discuss

### Prepare Meeting (5 min)
- [ ] Open Zoom/Teams meeting link
- [ ] Send standup reminder to team (if needed)
- [ ] Have these ready: Playbook, SPRINT_STATUS.md, bug triage framework
- [ ] Set timer for 15 minutes

### Equipment Check (5 min)
- [ ] Quiet location secured
- [ ] Microphone working
- [ ] Camera working (if video call)
- [ ] Internet stable
- [ ] Phone on silent

---

## 9 AM UTC (Daily Standup - 15 min)

### Standup Execution

**1. Welcome & Agenda** (1 min)
- [ ] Greet team
- [ ] State: "Let's do our daily standup. I'll ask 6 questions."
- [ ] Confirm attendees (QA Lead, Dev Leads, Build Engineer)

**2. Ask 6 Questions** (12 min)
Follow the template in DAILY_STANDUP_TRACKER.md:

- [ ] **Q1: What did you accomplish yesterday?** (2 min)
  - Listen for: test count, bugs fixed, SLAs met
  - Flag: Any missed SLAs?

- [ ] **Q2: What are you doing today?** (2 min)
  - Listen for: next test cases, bug priorities
  - Flag: Realistic estimate?

- [ ] **Q3: Do you have blockers?** (3 min)
  - Listen for: Any impediments?
  - Flag: Anything I can solve immediately?

- [ ] **Q4: How does progress look vs. plan?** (2 min)
  - Listen for: On track, at risk, or blocked?
  - Flag: Timeline still OK?

- [ ] **Q5: Any platform/environment issues?** (1 min)
  - Listen for: Build health, tool issues
  - Flag: Any platform concerns?

- [ ] **Q6: What's needed from me?** (2 min)
  - Listen for: Blockers only I can resolve
  - Flag: Quick decisions needed?

**3. Close** (2 min)
- [ ] Summarize key points
- [ ] List any action items
- [ ] Confirm: "See everyone tomorrow at 9 AM UTC"
- [ ] End meeting on time

### Standup Recording
- [ ] Fill in DAILY_STANDUP_TRACKER.md with all answers
- [ ] Note any decisions made
- [ ] Log any new bugs found
- [ ] Update progress metrics

---

## 10 AM - 12 PM (Post-Standup Actions - 2 hours)

### Process Blockers (15 min)
- [ ] Any critical issues mentioned? **YES / NO**
- [ ] If YES:
  - [ ] Understand the blocker
  - [ ] Decide: Can I solve it? Need escalation? Need team decision?
  - [ ] Take action immediately
  - [ ] Notify team of resolution
  - [ ] Update SPRINT_STATUS.md

### Triage Any Bugs (15 min)
- [ ] New bugs reported in standup? **YES / NO**
- [ ] For each bug:
  - [ ] Classify severity (CRITICAL / HIGH / MEDIUM / LOW)
  - [ ] Assign to dev team (with SLA)
  - [ ] Create GitHub issue (if not exists)
  - [ ] Notify assignee
  - [ ] Log in bug tracker

### Code Review (15 min) - If applicable
- [ ] Any PRs waiting for review? **YES / NO**
- [ ] If YES:
  - [ ] Review code against CODING_STANDARDS.md
  - [ ] Check tests are included
  - [ ] Approve or request changes
  - [ ] Merge if approved

### Update Tracking (15 min)
- [ ] Update PROJECT_AT_A_GLANCE.txt with latest metrics
- [ ] Update SPRINT_STATUS.md with daily progress
- [ ] Note: Tests completed, pass rate, bugs by severity
- [ ] Check: Timeline still on track? (Days to Dec 15 countdown)

### Stakeholder Communication (15 min)
- [ ] Do I need to notify project sponsor? **YES / NO**
- [ ] If YES: Send brief update with:
  - [ ] Daily progress (tests completed, pass rate)
  - [ ] Any critical issues
  - [ ] Timeline status
  - [ ] Next milestone date
- [ ] If NO: Log in daily notes

### Prepare Next Actions (15 min)
- [ ] What's the priority for rest of day?
- [ ] Any decisions pending?
- [ ] Any team members need individual check-ins?
- [ ] Anything preventing Phase 2 completion by Nov 29?

---

## 12 PM - 4 PM (Monitoring & Support - 4 hours)

### Monitor Progress (Throughout day)
- [ ] Check Slack #qa channel for test updates
- [ ] Check Slack #blockers for any escalations
- [ ] Verify dev teams are meeting SLAs on bug fixes
- [ ] Look for patterns in bugs (same system failing repeatedly?)

### Be Available (Critical SLA)
- [ ] Respond to Slack messages <15 min (if possible)
- [ ] Critical issues: Respond <1 hour
- [ ] If escalation needed: Contact project sponsor immediately
- [ ] If decision needed: Make it same day (don't delay)

### Support Teams
- [ ] Check if anyone needs help
- [ ] Unblock any impediments
- [ ] Answer any questions
- [ ] Help with problem-solving if asked
- [ ] Don't micromanage - just support

### Track Metrics (Hourly if possible)
- [ ] ~12 PM: Check morning test progress
- [ ] ~2 PM: Check afternoon test progress
- [ ] ~4 PM: Check end-of-day progress
- [ ] Compare to daily target (should be ~10-12 tests/day)

### Emergency Response
- [ ] Critical bug found? → Triage immediately
- [ ] SLA violation? → Escalate immediately
- [ ] Timeline at risk? → Assess impact, notify stakeholder
- [ ] Team issue? → Address immediately

---

## 4 PM - 5 PM (EOD Wrap-up - 1 hour)

### Daily Status Update
- [ ] Update DAILY_STANDUP_TRACKER.md "EOD UPDATE" section
- [ ] Note what went well today
- [ ] Note challenges encountered
- [ ] Note what we learned
- [ ] Note tomorrow's priority

### Update Metrics Dashboard
- [ ] Tests completed today: [X]
- [ ] Pass rate: [X]%
- [ ] Bugs found: [X] (by severity)
- [ ] Bugs fixed: [X]
- [ ] Days remaining: [X]
- [ ] Timeline status: [ON TRACK / AT RISK]

### Review Tomorrow
- [ ] What needs to happen tomorrow?
- [ ] Any pre-work needed?
- [ ] Any prep for standup?
- [ ] Any team communication needed?

### Send EOD Summary (Optional but recommended)
- [ ] Send Slack message to team:
  ```
  📊 EOD UPDATE [DATE]
  ✅ Tests completed today: X/58
  ✅ Pass rate: X%
  ⚠️  Bugs found: X (list by severity)
  🎯 Timeline: ON TRACK / AT RISK
  📅 Tomorrow: [Brief description]
  ```

### Personal Debrief (5 min)
- [ ] Note any decisions I made today
- [ ] Note any issues I escalated
- [ ] Note any patterns observed
- [ ] Update my risk register
- [ ] Prepare for tomorrow

### Log Time
- [ ] Total time spent today: [X hours]
- [ ] Stood up 1 standup: [9 AM UTC]
- [ ] Processed [X] blockers
- [ ] Triaged [X] bugs
- [ ] Made [X] decisions

---

## WEEKLY (FRIDAY 4 PM UTC - 45 min)

### Friday All-Hands Review

**Before Meeting (30 min)**
- [ ] Prepare presentation:
  - [ ] Week's progress (tests completed, pass rate)
  - [ ] Bugs found & fixed (trends)
  - [ ] Timeline status (days to Dec 15)
  - [ ] Risk assessment (any new risks?)
- [ ] Review SPRINT_STATUS.md
- [ ] Prepare decisions needed from team

**During Meeting (45 min)**
- [ ] Progress snapshot (5 min)
- [ ] QA status (10 min)
- [ ] Dev team updates (10 min)
- [ ] Build team updates (5 min)
- [ ] Risk assessment (5 min)
- [ ] Next week planning (5 min)
- [ ] Q&A (5 min)

**After Meeting (15 min)**
- [ ] Summarize decisions made
- [ ] Update action items
- [ ] Send meeting notes to team
- [ ] Update SPRINT_STATUS.md

### Weekly Metrics Report
- [ ] Send stakeholder weekly report with:
  - [ ] % complete (Phase 2: X/58 tests)
  - [ ] Pass rate
  - [ ] Bugs by severity
  - [ ] Critical issues (if any)
  - [ ] Timeline status
  - [ ] Risk level
  - [ ] Next week priorities

---

## END OF PHASE (Phase Transition Gates)

### Phase 2→3 Gate (Nov 29, Friday)
- [ ] Verify 58/58 tests completed
- [ ] Verify 90%+ pass rate
- [ ] Triage all bugs found
- [ ] Review bug list with team
- [ ] **DECISION**: Approve Phase 3 (bug fixing) start
- [ ] Update SPRINT_STATUS.md with Phase 2 results
- [ ] Send Phase 3 kickoff message to teams

### Phase 3→4 Gate (Dec 14, Sunday)
- [ ] Verify all bugs triaged or fixed
- [ ] Verify final QA sign-off
- [ ] Verify app store submissions accepted
- [ ] Verify performance targets met
- [ ] Review documentation completeness
- [ ] **DECISION**: Ready for Dec 15 GO/NO-GO?

### Final Gate (Dec 15, Monday - GO/NO-GO)
- [ ] Review final checklist
- [ ] Verify zero critical bugs
- [ ] Verify all tests passing
- [ ] Verify platforms approved
- [ ] Verify team ready
- [ ] **FINAL DECISION**: GO LIVE ✅ or NO-GO ❌

---

## WEEKLY TEMPLATE (Fill Every Friday)

```
WEEK OF [DATE]

PROGRESS:
- Phase: [Phase #]
- Tests completed: X/58 ([Y]%)
- Pass rate: X%
- Bugs found: X (Critical X, High X, Medium X, Low X)
- Bugs fixed: X

METRICS:
- Timeline: ON TRACK / AT RISK
- Days to release: X
- Critical blockers: 0 / [List]
- High severity issues: X
- SLAs met: [Y]%

KEY ACHIEVEMENTS:
- [Achievement 1]
- [Achievement 2]
- [Achievement 3]

CHALLENGES:
- [Challenge 1]
- [Challenge 2]

RISKS:
- [Risk 1]
- [Risk 2]

NEXT WEEK PRIORITIES:
1. [Priority 1]
2. [Priority 2]
3. [Priority 3]

DECISIONS NEEDED:
- [Decision 1?]
- [Decision 2?]
```

---

## EMERGENCY RESPONSE CHECKLIST

### Critical Bug Found
- [ ] Receive report
- [ ] Triage: Severity = CRITICAL
- [ ] Assign to best dev (usually same game engineer)
- [ ] SLA: Fix within 4 hours
- [ ] Notify: QA to monitor fix + retest
- [ ] Track: Check status hourly
- [ ] Verify: QA confirms fix works
- [ ] Update: Log in bug tracker
- [ ] Communicate: Notify stakeholder if major

### SLA Violation (Dev team missed deadline)
- [ ] Identify which bug
- [ ] Identify which dev team
- [ ] Understand: Why did it take longer?
- [ ] Assess: Is timeline at risk?
- [ ] Support: Can I help them complete it?
- [ ] Escalate: If impact is high
- [ ] Document: Log in SPRINT_STATUS.md
- [ ] Follow-up: Check next day to prevent repeat

### Behind Schedule (Tests behind target)
- [ ] Calculate: How many days behind?
- [ ] Assess: Can we catch up? (target +10-12 tests/day)
- [ ] Options:
  - [ ] Add QA resources
  - [ ] Run tests in parallel
  - [ ] Extend Phase 2 deadline
  - [ ] Reduce test scope (only test critical paths)
- [ ] Decide: Which option?
- [ ] Escalate: If timeline truly at risk

### Team Member Unavailable
- [ ] Understand: Who's unavailable? For how long?
- [ ] Assess: Impact on critical work?
- [ ] Redistribute: Reassign their work to others
- [ ] Escalate: If can't redistribute
- [ ] Update: Adjust timeline if needed
- [ ] Support: Help team manage workload

### App Store Submission Rejected
- [ ] Understand: Why was it rejected?
- [ ] Timeline: How long to resubmit?
- [ ] Assess: Does this delay Dec 15 release?
- [ ] Plan: Path to resubmission
- [ ] Options:
  - [ ] Fix and resubmit (if <1 week)
  - [ ] Launch without platform
  - [ ] Delay release to Dec 22
- [ ] Escalate: To project sponsor
- [ ] Communicate: Inform team & stakeholders

---

## PRINT THIS & USE DAILY

This checklist should become your daily routine:
- **Before 9 AM**: 30 min prep
- **9 AM**: 15 min standup
- **10 AM-12 PM**: 2 hours action items
- **12 PM-4 PM**: 4 hours monitoring
- **4-5 PM**: 1 hour EOD wrap-up
- **Total**: 8 hours/day (standard work day)

**Total time commitment: 25 days × 8 hours = 200 hours (One full month)**

You've got this. Let's ship this game.

---

**Last Updated**: November 20, 2025  
**Valid**: Nov 21 - Dec 15, 2025  
**Status**: READY TO EXECUTE
