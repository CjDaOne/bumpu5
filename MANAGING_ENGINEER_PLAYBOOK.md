# Managing Engineer Playbook
## Day-to-Day Operations & Decision Framework

**For**: You (Managing Engineer)  
**Date**: November 20, 2025  
**Phase**: Sprint 8 - Phase 2 Testing (Nov 21-29)  
**Next Milestone**: Dec 15 Release Decision

---

## YOUR JOB (IN A NUTSHELL)

You have ONE job over the next 25 days: **Protect the Dec 15 Release Date**

This means:
1. **Monitor progress daily** (15-min standup)
2. **Unblock teams immediately** (critical issues <1 hour)
3. **Enforce feature freeze** (no new features, bug fixes only)
4. **Triage bugs ruthlessly** (Critical/High/Medium/Low)
5. **Make final GO/NO-GO call** (Dec 15)

---

## DAILY STANDUP AGENDA (15 MIN)

**Time**: 9 AM UTC  
**Attendees**: QA Lead + Team Leads (dev teams on-call)  
**Format**: Async (Slack) or sync (Zoom), your choice

### Your Questions (In This Order)

**1. What did you accomplish yesterday?** (2 min)
- QA: X test cases completed, Y passed, Z issues found
- Dev Teams: Bugs fixed, SLA met?

**2. What are you doing today?** (2 min)
- QA: Next test cases (list them)
- Dev Teams: Bug priorities (if any)

**3. Do you have blockers?** (3 min)
- Listen for critical issues
- Flag for immediate action
- Record in SPRINT_STATUS.md

**4. How does progress look vs. plan?** (2 min)
- On track / At risk / Blocked?
- Any timeline adjustments needed?
- Recording: QA progress % toward 58 tests

**5. Any platform/environment issues?** (2 min)
- Build health?
- Device testing coverage?
- Submission readiness?

**6. What's needed from me (managing engineer)?** (4 min)
- Unblock decisions
- Escalations
- Resource requests

### Standup Notes Template

```
DATE: Nov 21, 2025
PHASE: 2 (Testing)
DURATION: 25 days until release

QA Progress:
- Tests completed: 12/58 (21%)
- Tests passed: 12/12 (100%)
- Issues found: 2 (1 Medium, 1 Low)

Dev Status:
- Team 1 (Gameplay): On-call, 0 bugs assigned
- Team 2 (Board): On-call, 0 bugs assigned
- Team 3 (UI): On-call, 0 bugs assigned
- Team 4 (Build): On-call, 0 bugs assigned

Blockers: NONE
Timeline: ON TRACK (25 days to Dec 15)
Decision: Continue to Phase 2 completion
```

---

## BUG TRIAGE FRAMEWORK

**When a bug is reported**, ask these questions:

### Question 1: What's the Severity?
```
CRITICAL → Game-breaking, unplayable, data loss
HIGH     → Major feature broken, workaround exists
MEDIUM   → Feature degraded, cosmetic, minor issue
LOW      → Nice-to-have fix, polish, rare edge case
```

### Question 2: What's the Impact?
```
All Players     → Affects everyone (CRITICAL/HIGH)
Some Players    → Specific device/platform (HIGH/MEDIUM)
Rare Edge Case  → <1% of usage (MEDIUM/LOW)
```

### Question 3: What's the Timeline?
```
CRITICAL → Fix now (<4 hours)
HIGH     → Fix ASAP (<24 hours)
MEDIUM   → Fix this week (<2 days)
LOW      → Fix if time permits (before Dec 15, ok if deferred)
```

### Decision Matrix

| Severity | Impact | Timeline | Action |
|----------|--------|----------|--------|
| CRITICAL | All | Now | STOP everything, fix immediately |
| HIGH | All | <24h | Assign dev team today, report daily |
| HIGH | Some | <24h | Assign dev team, lower priority |
| MEDIUM | Any | <2d | Queue for dev team, schedule fix |
| LOW | Any | <1w | Backlog (defer if needed) |

### Example Triage Session

```
BUG REPORT: Game 1 (Bump 5) doesn't detect 5-in-a-row correctly

Step 1: Severity
- User can't win the game → CRITICAL

Step 2: Impact
- Affects all players on all platforms → ALL PLAYERS

Step 3: Timeline
- Breaks core gameplay → FIX NOW (<4 hours)

DECISION: CRITICAL
ACTION: Pull dev team off standby, fix immediately, verify with QA
```

---

## DAILY TODO (WHEN YOU WAKE UP)

### 1. Check Overnight Updates (10 min)
- [ ] Read Slack #blockers channel
- [ ] Check SPRINT_STATUS.md for updates
- [ ] Review any overnight bugs reported
- [ ] Triage critical issues (if any)

### 2. Prepare for Standup (5 min)
- [ ] Review yesterday's standup notes
- [ ] Prepare any decisions/announcements
- [ ] Flag any known issues to discuss
- [ ] Estimate today's test completion rate

### 3. Attend Standup (15 min)
- [ ] Join 9 AM UTC standup
- [ ] Ask the 6 questions above
- [ ] Make decisions on blockers
- [ ] Record notes

### 4. Take Any Necessary Actions (30-60 min)
- [ ] Assign bugs to dev teams (if needed)
- [ ] Unblock any critical issues
- [ ] Update SPRINT_STATUS.md
- [ ] Notify teams of any changes

### 5. Monitor Progress (throughout day)
- [ ] Check QA test progress (post test results in Slack)
- [ ] Verify dev teams are meeting SLAs
- [ ] Respond to critical escalations within 1 hour
- [ ] Update daily bug log

### 6. End-of-Day Check (5 min)
- [ ] Update SPRINT_STATUS.md with day's progress
- [ ] Log any issues for next day
- [ ] Calculate days until release (countdown)
- [ ] Send brief end-of-day summary to team

---

## WEEKLY CHECK-IN (FRIDAYS)

**Time**: 4 PM UTC (end of week)  
**Attendees**: All team leads + stakeholders  
**Duration**: 30-45 min

### Agenda

1. **Progress Snapshot** (5 min)
   - % complete (target: 20%/week for 5 weeks)
   - Tests passed vs. total (58 expected for Phase 2)
   - Critical bugs found (track trend)

2. **Risk Assessment** (5 min)
   - Any new risks emerged?
   - Timeline at risk? (track countdown to Dec 15)
   - Resource issues?

3. **Team Updates** (15 min)
   - QA: How are Phase 2 tests going?
   - Dev: Any patterns in bugs?
   - Build: Submission prep ready?

4. **Decisions** (5 min)
   - Any timeline adjustments?
   - Phase transition greenlight?
   - Major blockers?

5. **Next Week Goals** (5 min)
   - What should happen next week?
   - Any team member changes?
   - Prepare for next phase?

---

## DECISION FLOWCHART

Use this when major decisions need to be made:

```
DECISION NEEDED
    ↓
Is it about critical bugs?
    YES → Use Bug Triage Framework (above)
    NO → Is it about timeline/schedule?
        YES → Consider: Cost/Quality/Timeline trade-off
        NO → Is it about features/scope?
            YES → Answer: Does it impact Dec 15 release?
                YES → DEFER until after Dec 15
                NO → PROCEED (if time exists)
            NO → Escalate to project sponsor
```

---

## CRITICAL DATES CALENDAR

### This Week (Nov 21-24)
- **Nov 21**: Phase 2 testing BEGINS
- **Nov 22**: First test results review
- **Nov 24**: Mid-week progress check

### Next Week (Nov 25-29)
- **Nov 25**: Weekly review
- **Nov 27**: Projected Phase 2 90% complete
- **Nov 29**: Phase 2 completion target

### December (Dec 1-15)
- **Dec 1**: Phase 3 (bug fixes) BEGINS
- **Dec 5**: iOS App Store submission
- **Dec 7**: Android Play Store submission
- **Dec 10**: Final testing + submission checks
- **Dec 14**: Final QA sign-off
- **Dec 15**: GO/NO-GO DECISION + RELEASE

---

## RED FLAGS (WHEN TO ESCALATE)

Stop everything and escalate to project sponsor if:

1. **Critical Bug Found** that affects gameplay
   - Action: Triage immediately, assign dev team
   - Escalate if: Can't be fixed in <4 hours

2. **Timeline at Risk** (more than 2-3 days behind)
   - Action: Reassess Phase 3 duration
   - Escalate if: Dec 15 release date at risk

3. **Platform Submission Issue** (rejection from app store)
   - Action: Understand requirement, plan resubmission
   - Escalate if: Resubmission causes >1 week delay

4. **Team Member Unavailable** (can't continue work)
   - Action: Redistribute workload
   - Escalate if: Not enough people to complete phase

5. **Scope Creep** (new features requested)
   - Action: DENY (feature freeze in effect)
   - Escalate if: Stakeholder pushing back

---

## PHASE GATES (SIGN-OFF CHECKLIST)

You must approve each phase transition. Use this checklist:

### Phase 1→2 Gate (ALREADY PASSED ✅)
- [x] 78/78 compatibility tests passed
- [x] Zero critical bugs
- [x] All code reviews approved
- [x] Ready for Phase 2

### Phase 2→3 Gate (Nov 29-30)
- [ ] 58/58 Phase 2 tests completed
- [ ] 90%+ tests passed
- [ ] All bugs triaged
- [ ] Timeline still on track
- [ ] Sign-off: Ready for Phase 3 (bug fixing)

### Phase 3→4 Gate (Dec 14-15)
- [ ] All bugs fixed or deferred
- [ ] Final QA validation passed
- [ ] App store submissions accepted
- [ ] Zero critical bugs remaining
- [ ] Performance targets verified
- [ ] Sign-off: APPROVED FOR RELEASE

---

## TOOLS & DASHBOARDS

### Primary Dashboard: SPRINT_STATUS.md
- Real-time progress tracking
- Bug counts by severity
- Test completion rate
- Team status (on-track/at-risk/blocked)
- **Update**: Daily (by EOD)

### Secondary Dashboard: CURRENT_STATUS_INDEX.md
- Quick team navigation
- Role-specific instructions
- Document locations
- **Update**: As needed (when status changes)

### Bug Tracking: GitHub Issues
- All bugs logged here
- Tags: Critical, High, Medium, Low
- Assigned to dev teams
- **Update**: Real-time (as reported)

### Test Tracking: QATests.cs (code)
- 127+ unit tests
- Run in Unity Test Framework
- Expected: 100% pass rate
- **Execute**: Daily (automated or manual)

---

## KEY CONTACTS & ESCALATION

### Your Supporting Team
- **QA Lead** (Phase 2 executor)
  - Reports to you daily
  - Runs 58 test cases
  - Contacts: Daily standup, Slack

- **Dev Team Leads** (4 people, on-call)
  - Available for critical bugs only
  - SLA: Critical <4h, High <24h
  - Contacts: Slack #blockers, direct message

- **Build Engineer** (platform submissions)
  - Starts work Dec 5
  - Prepares for app store submissions
  - Contact: Email or Slack #build

### Escalation Levels

**Level 1 - You Can Decide**:
- Bug triage (use framework)
- Test scheduling
- Team assignments
- Minor timeline adjustments (<2 days)

**Level 2 - Need Stakeholder Approval**:
- Major timeline changes (>3 days)
- Scope creep requests
- Feature deferrals
- Budget/resource changes

**Level 3 - Release Decision**:
- Dec 15 GO/NO-GO call
- Only you can make this

---

## COMMUNICATION TEMPLATES

### Critical Bug Alert (Send Immediately)

```
CRITICAL BUG ALERT

System: [Game Mode / Feature]
Severity: CRITICAL
Impact: [Affects all players / Specific platform / Rare case]
Description: [What's broken]

ACTION TAKEN:
- Assigned to: [Dev Team]
- Expected fix time: [<4 hours]
- QA will verify: [Time]

Next update: [Time] UTC
```

### Daily Standup Summary (Send 9 AM)

```
STANDUP SUMMARY - [DATE]

PROGRESS:
- Phase 2 Tests: X/58 completed (Y% pass rate)
- Bugs Found: X total (Critical/High/Medium/Low)
- Timeline: ON TRACK (Z days to Dec 15)

BLOCKERS: None / [List if any]

TODAY'S FOCUS: [What's priority today]

DECISION NEEDED: Yes/No [If yes, describe]
```

### Weekly Report (Send Friday 4 PM)

```
WEEKLY REVIEW - Week of [DATE]

HIGHLIGHTS:
- [Major achievement 1]
- [Major achievement 2]

METRICS:
- Tests completed: X/58 (Y%)
- Pass rate: Z%
- Bugs by severity: Critical X, High Y, Medium Z

RISKS:
- [Any risks emerged?]

TIMELINE STATUS: ON TRACK / AT RISK / BLOCKED
- Days until Dec 15: X
- Current phase completion: Y%

NEXT WEEK:
- [Key goal 1]
- [Key goal 2]

DECISION REQUIRED: Yes/No
```

---

## COMMON SCENARIOS & YOUR RESPONSE

### Scenario 1: Test Failure (Common)

**Situation**: "We found Game 1 doesn't detect diagonal wins correctly"

**Your Response**:
1. **Triage**: Is it reproducible? On all platforms? → Probably CRITICAL
2. **Escalate**: Pull dev team off standby
3. **Track**: Assign to Gameplay Engineer with <4h SLA
4. **Monitor**: Request fix + QA re-test within 4 hours
5. **Log**: Update bug tracker and SPRINT_STATUS.md

**Outcome**: Bug fixed and verified before Phase 2 completes

---

### Scenario 2: Behind Schedule (Risk)

**Situation**: "We're only 30% done with Phase 2, should be 50%"

**Your Response**:
1. **Assess**: How many days behind? (currently on track, but if this happens)
2. **Investigate**: Why are we slow? (test complexity? tools? people?)
3. **Plan**: Can we add QA resources? Run tests in parallel?
4. **Decide**: Can we still meet Nov 29 deadline?
5. **Escalate**: If not, notify stakeholder + adjust Phase 3

**Outcome**: Get back on track or adjust timeline

---

### Scenario 3: App Store Rejection (Late)

**Situation**: "iOS submission rejected by Apple on Dec 8"

**Your Response**:
1. **Understand**: Why was it rejected? (content rating? permissions? API?)
2. **Fix**: Plan resubmission (usually <1 week)
3. **Reassess**: Can we still release Dec 15?
4. **Decide**: Go Dec 15 without iOS? Or delay to Dec 22?
5. **Escalate**: If delay needed, notify stakeholder ASAP

**Outcome**: Minimize release delay, proceed on best timeline

---

### Scenario 4: Stakeholder Requests New Feature

**Situation**: "Can we add a custom chip color feature by Dec 15?"

**Your Response**:
1. **No**. Feature freeze is in effect (Nov 20)
2. **Reason**: 25 days to release, every day counts
3. **Defer**: Add to post-release roadmap (v1.1)
4. **Offer**: We can add this Jan 2026 (immediately after)
5. **Enforce**: Escalate to project sponsor if they push back

**Outcome**: Feature freeze maintained, release date protected

---

## METRICS TO TRACK DAILY

Create a simple spreadsheet or document tracking these:

```
DATE    | TESTS DONE | PASS RATE | CRITICAL BUGS | HIGH BUGS | TIMELINE STATUS | DAYS TO RELEASE
Nov 21  | 12/58      | 100%      | 0             | 1         | ON TRACK        | 24
Nov 22  | 24/58      | 100%      | 0             | 1         | ON TRACK        | 23
Nov 23  | 36/58      | 95%       | 0             | 2         | SLIGHT RISK     | 22
```

**Target Pattern**:
- Tests completed: +10-12 tests/day
- Pass rate: 90%+
- Bug trend: Should decrease by end of Phase 2
- Timeline: ON TRACK throughout

---

## WHEN TO WORK LONGER HOURS

Only extend hours if:

1. **Critical bug prevents release** (unlikely)
   - Everyone works overtime to fix
   - Use as last resort only

2. **App store submissions failing** (Dec 5-10)
   - Extended hours to resubmit quickly
   - Usually short duration (1-2 days)

Otherwise:
- **Don't** ask teams to work weekends
- **Do** maintain sustainable pace
- **Remember**: Quality > Speed (bugs cost more than extra days)

---

## YOUR CHECKLIST (SAVE THIS)

**Every Morning**:
- [ ] Check #blockers Slack
- [ ] Read yesterday's standup notes
- [ ] Prepare for 9 AM standup
- [ ] Check SPRINT_STATUS.md for overnight updates

**Every Day**:
- [ ] Attend standup (9 AM UTC)
- [ ] Triage any reported bugs
- [ ] Monitor team progress
- [ ] Respond to escalations <1 hour

**Every Friday**:
- [ ] Weekly review meeting (4 PM UTC)
- [ ] Update metrics spreadsheet
- [ ] Send weekly report
- [ ] Plan next week

**Every Week**:
- [ ] Review SPRINT_STATUS.md progress
- [ ] Update risk register
- [ ] Assess timeline (still 25 days to Dec 15?)
- [ ] Prepare for next phase

---

## FINAL THOUGHT

You've got this. The team has delivered 87.5% of the project on time with high quality. The last 12.5% is testing and bug fixing—the team knows how to do this.

**Your job**: Keep them focused, unblock issues, and protect the Dec 15 deadline.

**You'll know you succeeded when**: Dec 15 comes and you say "GO" with confidence.

---

**Good luck, Managing Engineer.**

**Let's ship this game.**

---

*Prepared by: Amp*  
*Date: November 20, 2025*  
*Status: Ready to Execute Phase 2*
