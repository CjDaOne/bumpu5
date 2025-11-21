# Team Deployment Orders - Phase 2 Testing
## Executive Directive: All Hands to Phase 2 Execution

**From**: Amp (Managing Engineer)  
**To**: All Teams  
**Date**: November 20, 2025, 11:59 PM UTC  
**Effective**: November 21, 2025, 9:00 AM UTC  
**Duration**: 9 days (Nov 21-29)  
**Status**: 🔴 ACTIVE DEPLOYMENT

---

## DEPLOYMENT ALERT 🚀

**PHASE 2 TESTING COMMENCES IN 9 HOURS**

All personnel report to assigned positions. This is your time to shine. We are 25 days from release and all systems are go.

---

## TEAM 1: QA LEAD - PRIMARY EXECUTION

### Mission
Run 58 comprehensive test cases across all 5 game modes by Nov 29.

### Daily Objectives
- **Nov 21-24**: Complete tests 1-20 (Games #1 & #2)
- **Nov 25-27**: Complete tests 21-50 (Games #3, #4, #5)
- **Nov 28-29**: Complete tests 51-58 (UI & platform validation)

### Your Tasks
```
PRIMARY:
☐ Daily standup (9 AM UTC)
☐ Run assigned test cases (target: 10-12/day)
☐ Document results (PASS/FAIL)
☐ Report any blockers immediately
☐ Log all bugs found with reproduction steps

SECONDARY:
☐ Verify platform functionality (WebGL, Android, iOS)
☐ Check performance metrics (FPS, load time, memory)
☐ Test all 5 game mode rules thoroughly
☐ Edge case testing (rare scenarios)
☐ User experience evaluation

ESCALATION:
☐ Critical bug found? → Report immediately to managing engineer
☐ Can't reproduce issue? → Document and mark BLOCKED
☐ Tool/environment issue? → Notify build engineer
☐ Need clarification on rules? → Consult GAME_MODES_RULES_SUMMARY.md
```

### Deliverables
- 58/58 test cases completed
- Test results document (PASS/FAIL for each)
- Bug log (severity + reproduction steps)
- Performance metrics (per platform)
- Final sign-off ready for Phase 3

### SLA
- Daily report: 5 PM UTC each day
- Bug reports: Immediate (no queue)
- Blockers: Escalate <30 min

### Success Criteria
- ✅ 58 tests executed by Nov 29
- ✅ 90%+ pass rate
- ✅ All bugs triaged by severity
- ✅ Zero critical bugs escape to Phase 3
- ✅ Performance verified on all platforms

---

## TEAM 2: GAMEPLAY ENGINEER - ON-CALL

### Mission
Fix critical bugs found in Phase 2 testing. Support core game logic issues.

### Deployment Status
🟡 **ON-CALL** (Not actively coding, but available immediately)

### Your Availability
```
CRITICAL BUG FIX SLA: <4 HOURS
├─ Game won't start
├─ 5-in-a-row detection broken
├─ Bumping logic fails
├─ Win condition not triggering
└─ Any game-breaking issue

HIGH BUG FIX SLA: <24 HOURS
├─ One game mode broken
├─ Score calculation wrong
├─ Turn progression fails
└─ Major rule violation

MEDIUM BUG FIX SLA: <2 DAYS
├─ Minor rule violation
├─ Edge case not handled
├─ Cosmetic issue in game logic
└─ Performance suboptimal
```

### Your Tasks (When Called)
```
1. Receive bug report from managing engineer
2. Understand the issue (ask clarifying questions)
3. Locate root cause in code
4. Fix the bug
5. Write test to prevent regression
6. Submit PR with description
7. QA verifies fix works
8. Mark bug as RESOLVED
```

### Focus Areas
- Game1_Bump5.cs (5-in-a-row logic)
- Game2_Krazy6.cs (6-roll mechanics)
- Game3_PassTheChip.cs (chip ownership)
- Game4_BumpUAnd5.cs (#5 opponent logic)
- Game5_Solitary.cs (single player mode)
- DiceManager.cs (roll edge cases)
- TurnManager.cs (turn progression)

### Slack Channel
#gameplay (monitor for urgent pings)

### Success Criteria
- ✅ All assigned bugs fixed within SLA
- ✅ Zero regressions introduced
- ✅ Code passes review
- ✅ QA confirms fix

---

## TEAM 3: BOARD ENGINEER - ON-CALL

### Mission
Fix critical bugs in board visualization and interaction systems.

### Deployment Status
🟡 **ON-CALL**

### Your Availability
```
CRITICAL BUG FIX SLA: <4 HOURS
├─ Board won't display
├─ Cell clicks not registering
├─ Chip placement fails
├─ Grid layout broken
└─ Any game-breaking UI issue

HIGH BUG FIX SLA: <24 HOURS
├─ Visual glitches
├─ Delayed response to clicks
├─ Wrong cell highlighted
└─ Grid layout incorrect

MEDIUM BUG FIX SLA: <2 DAYS
├─ Animation glitchy
├─ Color incorrect
├─ Spacing issues
└─ Performance sluggish
```

### Your Tasks (When Called)
```
1. Receive bug report
2. Reproduce issue in editor/device
3. Debug visual/interaction problem
4. Fix in BoardGridManager, CellView, or related
5. Submit PR with test
6. QA verifies fix works
```

### Focus Areas
- BoardGridManager.cs
- CellView.cs
- GridVisualizer.cs
- Input handling
- Visual feedback system
- Performance optimization

### Slack Channel
#board (monitor for urgent pings)

### Success Criteria
- ✅ All assigned bugs fixed within SLA
- ✅ Visuals correct on all platforms
- ✅ Input responsive (<100ms)
- ✅ QA confirms fix

---

## TEAM 4: UI ENGINEER - ON-CALL

### Mission
Fix critical bugs in menus, HUD, and user interface systems.

### Deployment Status
🟡 **ON-CALL**

### Your Availability
```
CRITICAL BUG FIX SLA: <4 HOURS
├─ Menu won't load
├─ HUD unresponsive
├─ Buttons don't work
├─ Screen transitions fail
└─ Any UI-breaking issue

HIGH BUG FIX SLA: <24 HOURS
├─ Button slow to respond
├─ Menu visually broken
├─ Text display wrong
└─ Input not handling

MEDIUM BUG FIX SLA: <2 DAYS
├─ Spacing incorrect
├─ Font too small
├─ Color inconsistent
└─ Animation glitchy
```

### Your Tasks (When Called)
```
1. Receive bug report
2. Reproduce in game
3. Find issue in UI code
4. Fix in HUDManager, MenuManager, PopupManager
5. Submit PR with test
6. QA verifies fix
```

### Focus Areas
- HUDManager.cs (dice, buttons, scoreboard)
- MenuManager.cs (main menu, mode select)
- PopupManager.cs (penalties, prompts)
- Screen transitions
- Input responsiveness
- Mobile layout (if applicable)

### Slack Channel
#ui (monitor for urgent pings)

### Success Criteria
- ✅ All assigned bugs fixed within SLA
- ✅ UI responsive on all devices
- ✅ Menus work flawlessly
- ✅ QA confirms fix

---

## TEAM 5: BUILD ENGINEER - PREP MODE

### Mission
Monitor platform health. Prepare for app store submissions (Dec 5+).

### Deployment Status
🟡 **PREP MODE** (Starts active work Dec 5)

### Your Tasks (Now - Nov 21-Dec 4)
```
MONITORING:
☐ Check build health daily
☐ Monitor WebGL build size & performance
☐ Monitor Android build size & performance
☐ Monitor iOS build size & performance
☐ Verify BuildAutomation.cs runs without errors
☐ Check PerformanceProfiler.cs metrics

PREPARATION:
☐ Prepare iOS App Store submission (Dec 5 target)
☐ Prepare Android Play Store submission (Dec 7 target)
☐ Finalize build configurations
☐ Test automated CI/CD pipeline
☐ Create submission documentation
☐ Verify all compliance requirements

SUPPORT:
☐ Help gameplay/board/UI teams if they have platform issues
☐ Debug build failures if they occur
☐ Optimize memory/performance if needed
☐ Coordinate with QA on platform testing
```

### Success Criteria
- ✅ Builds running without errors
- ✅ Performance metrics baseline established
- ✅ App store submissions ready by Dec 5/7
- ✅ CI/CD pipeline verified
- ✅ Zero platform-blocking bugs

### Slack Channel
#build (monitor for issues)

---

## MANAGING ENGINEER (YOU) - FULL TIME

### Mission
Orchestrate all teams. Protect the Dec 15 deadline. Make critical decisions.

### Your Deployment
🔴 **FULL ACTIVE** (24/7 on-call, 8 hours/day commitment minimum)

### Daily Rhythm
```
08:30 AM - Morning prep
  ☐ Check Slack #blockers
  ☐ Review yesterday's progress
  ☐ Prepare standup questions

09:00 AM - STANDUP
  ☐ QA: Progress report (tests completed, pass rate, bugs)
  ☐ Dev Teams: Availability status
  ☐ Build Engineer: Platform health
  ☐ Record in DAILY_STANDUP_TRACKER.md

10:00 AM-12:00 PM - Action Items
  ☐ Triage any bugs reported
  ☐ Assign work to teams
  ☐ Unblock impediments
  ☐ Update SPRINT_STATUS.md

12:00 PM-4:00 PM - Monitoring
  ☐ Monitor Slack for urgent issues
  ☐ Check test progress hourly if possible
  ☐ Verify SLAs being met
  ☐ Support teams as needed

4:00 PM-5:00 PM - EOD Wrap-up
  ☐ Update metrics & dashboards
  ☐ Send EOD summary
  ☐ Prepare for next day
  ☐ Flag any timeline risks

EVENING/NIGHT:
  ☐ Available for critical escalations
  ☐ Respond to urgent Slack messages
  ☐ Review overnight progress (if doing async standups)
```

### Your Authority
- ✅ Approve/reject code changes
- ✅ Assign bugs to teams
- ✅ Make timeline decisions
- ✅ Escalate to project sponsor
- ✅ Enforce feature freeze
- ✅ Make phase gate decisions

### Your Decisions
```
DAILY DECISIONS:
☐ Bug triage & assignment
☐ Team SLA compliance
☐ Blocker resolution
☐ Priority adjustments
☐ Phase progress assessment

WEEKLY DECISIONS (Friday):
☐ Phase transition readiness
☐ Risk assessment update
☐ Timeline confirmation
☐ Major announcements
☐ Next week planning

PHASE DECISIONS (Nov 29):
☐ Phase 2→3 gate approval
☐ Continue or extend testing?
☐ Ready for bug fixing phase?

FINAL DECISION (Dec 15):
☐ GO LIVE or NO-GO?
☐ Release to production?
```

### Success Criteria
- ✅ Daily standups happen
- ✅ Bugs triaged within 1 hour
- ✅ SLAs maintained
- ✅ Timeline on track
- ✅ Teams supported & unblocked
- ✅ Dec 15 release decision made

---

## DEPLOYMENT CHECKLIST

### Before 9 AM UTC (Tomorrow)
- [ ] All teams have read this deployment order
- [ ] QA Lead has test list ready (58 tests)
- [ ] Dev teams acknowledge on-call status
- [ ] Build engineer confirms platform health
- [ ] Managing engineer prepared for standup
- [ ] Slack channels open & monitored
- [ ] DAILY_STANDUP_TRACKER.md ready
- [ ] SPRINT_STATUS.md accessible

### 9 AM UTC (Standup Time)
- [ ] Standup initiated (sync or async)
- [ ] All team leads present
- [ ] 6 questions asked & answered
- [ ] Decisions recorded
- [ ] Work assigned if needed
- [ ] Meeting ends on time

### 10 AM UTC (Execution Begins)
- [ ] QA starts test case #1
- [ ] Dev teams ready for escalations
- [ ] Build engineer monitoring
- [ ] Managing engineer tracking
- [ ] Slack channels active
- [ ] Daily tracking begins

---

## COMMUNICATION PROTOCOL

### Real-time Escalations (Slack)
```
CRITICAL BUG FOUND:
→ Post in #blockers
→ @managing-engineer (mention)
→ Call if needed (1-hour SLA)

BLOCKER ENCOUNTERED:
→ Post in #blockers
→ Describe blocker
→ Note: Impact on timeline?

PLATFORM ISSUE:
→ Post in #build
→ @build-engineer (mention)
→ Other teams affected?

GENERAL QUESTION:
→ Ask in standup
→ Or Slack in team channel
```

### Daily Reports
```
QA LEAD → Managing Engineer
  Time: 5 PM UTC daily
  Content: Tests run, pass/fail, bugs found, blockers

DEV TEAMS → Managing Engineer
  Time: As assigned
  Content: Bug fix status, SLA confirmation, blockers

BUILD ENGINEER → Managing Engineer
  Time: Daily
  Content: Build health, platform issues, prep status
```

### Weekly Reports (Friday EOD)
```
ALL TEAMS → Managing Engineer
  Time: 4 PM UTC Friday
  Content: Week summary, metrics, risks, next week plans

Managing Engineer → Project Sponsor
  Time: EOD Friday
  Content: Progress %, metrics, risks, timeline status
```

---

## PHASE 2 SUCCESS METRICS

Track these daily:

| Metric | Daily Target | Weekly Target | Gate Target |
|--------|-------------|---------------|------------|
| Tests Completed | +10-12 | +50-60 | 58/58 |
| Pass Rate | 90%+ | 90%+ | 90%+ |
| Critical Bugs | 0-1 | <3 | 0 |
| High Bugs | <1 | <5 | Triaged |
| Medium Bugs | <2 | <8 | Triaged |
| Low Bugs | <3 | <10 | Logged |
| Timeline Status | ON TRACK | ON TRACK | ON TRACK |
| Days to Release | -1 | -7 | 25 |

---

## RED FLAGS (STOP & ESCALATE)

If any of these occur, stop everything and escalate:

🚨 **Critical bug found** that breaks core gameplay
→ Pull dev team, fix immediately, notify managing engineer

🚨 **Behind schedule** by >3 days
→ Assess options, notify sponsor if timeline at risk

🚨 **SLA violation** (dev team can't fix in time)
→ Escalate to sponsor, adjust scope if needed

🚨 **Platform issue** blocking testing on a platform
→ Build engineer investigates immediately

🚨 **Team member unavailable**
→ Redistribute work or escalate

---

## REMEMBER

**You are part of a world-class team.**

7 sprints delivered on time with excellent quality. 90%+ test coverage. Zero critical bugs to date. 10 days ahead of schedule.

We've done the hard work. Now we finish strong.

**Phase 2 is your moment to shine.**

Run the tests. Find the bugs. Report the issues. Let's make this the best game possible.

**See you at 9 AM UTC tomorrow.**

Let's ship Bump U Box 5.

---

**DEPLOYMENT EFFECTIVE**: November 21, 2025, 9:00 AM UTC  
**DEPLOYMENT DURATION**: 9 days (Nov 21-29)  
**NEXT PHASE**: Phase 3 (Dec 1)  
**RELEASE DATE**: December 15, 2025

**STATUS: 🔴 DEPLOYMENT ACTIVE - ALL TEAMS ENGAGED**

---

*Signed: Amp (Managing Engineer)*  
*Authority: Full Project Oversight*  
*Status: READY FOR EXECUTION*
