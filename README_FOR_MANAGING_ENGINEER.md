# README for Managing Engineer
## Everything You Need to Know (Right Now)

**Date**: November 20, 2025  
**Status**: 87.5% COMPLETE - On Track for Dec 15 Release  
**Your Role**: Protect the release date + manage team

---

## THE SITUATION (2 MIN READ)

Your team has built an amazing game. Here's what's been done:

✅ **Complete**: All 8 sprints delivered (core logic, UI, builds)  
✅ **Quality**: 90%+ test coverage, zero critical bugs  
✅ **Timeline**: 10 days ahead of original schedule  
🔄 **Current**: Phase 2 Testing starts tomorrow (Nov 21)  
⏳ **Next**: Phase 3 (Dec 1-14), then Release Decision (Dec 15)

---

## YOUR JOB (1 MIN)

**Tl;dr**: Don't let anything delay Dec 15.

**Specifically**:
1. **Daily standup** (15 min, 9 AM UTC) - check progress
2. **Bug triage** (as needed) - CRITICAL <4h, HIGH <24h
3. **Unblock teams** (immediate) - respond to escalations in <1 hour
4. **Enforce freeze** - no new features, bug fixes only
5. **Make GO/NO-GO call** (Dec 15) - release or delay

---

## YOUR MUST-READ DOCUMENTS (IN ORDER)

### Right Now (30 seconds each)
1. **This file** ← You're reading it
2. **MANAGING_ENGINEER_PLAYBOOK.md** ← Your daily operations guide

### Before Tomorrow (5 min)
3. **MANAGING_ENGINEER_STATUS_REPORT.md** ← Full project status

### Before First Standup (5 min)
4. **CURRENT_STATUS_INDEX.md** ← Team navigation guide
5. **SPRINT_STATUS.md** ← Real-time progress tracker

### Reference (whenever needed)
- **GAME_MODES_RULES_SUMMARY.md** ← Understand the 5 games
- **_docs/STANDARDS/CODING_STANDARDS.md** ← Code quality rules
- **_docs/ARCHITECTURE/ARCHITECTURE.md** ← System design

---

## QUICK START (TODAY)

### Now (5 min)
- [ ] Read this file (you're almost done)
- [ ] Read MANAGING_ENGINEER_PLAYBOOK.md (your playbook for 25 days)

### Tonight (10 min)
- [ ] Skim MANAGING_ENGINEER_STATUS_REPORT.md (know the status)
- [ ] Review CURRENT_STATUS_INDEX.md (understand team roles)

### Tomorrow Morning (9 AM UTC)
- [ ] Join daily standup
- [ ] Ask the 6 questions in the Playbook
- [ ] Note any blockers
- [ ] Make any necessary decisions

### This Week
- [ ] Monitor Phase 2 progress (target: 58 tests by Nov 29)
- [ ] Triage any bugs found
- [ ] Keep teams on-track
- [ ] Friday: Weekly review meeting

---

## CRITICAL INFORMATION

### Timeline
```
TODAY (Nov 20)     → Project is 87.5% done
NOV 21-29          → Phase 2 Testing (58 test cases)
DEC 1-14           → Phase 3 (bug fixes + optimization)
DEC 15             → GO/NO-GO DECISION (FINAL)
DEC 15+            → RELEASE (if GO)
```

### Team Assignments
- **QA Lead**: Execute Phase 2 tests (reports daily)
- **Dev Teams** (4): On-call for critical bugs (SLA <4 hours)
- **Build Engineer**: Prep for app store submissions (Dec 5 start)
- **You**: Coordinate, unblock, protect timeline

### Key Metrics (Current)
- **Code Coverage**: 90%+ (target 80%) ✅ EXCEEDS
- **Test Pass Rate**: 100% (Phase 1: 78/78) ✅ PERFECT
- **Critical Bugs**: 0 ✅ ZERO
- **Timeline**: 10 days ahead ✅ AHEAD OF SCHEDULE

---

## YOUR DAILY CHECKLIST (EVERY MORNING)

```
☐ Check Slack #blockers for overnight issues
☐ Read SPRINT_STATUS.md overnight updates
☐ Prepare for 9 AM UTC standup
☐ Check: Any bugs reported? Any escalations?
☐ Triage bugs (if any) using the Triage Framework
☐ Assign critical bugs to dev teams
☐ Monitor progress throughout day
☐ Update SPRINT_STATUS.md with day's results
☐ Respond to escalations <1 hour SLA
☐ EOD: Send brief summary to team
```

---

## BUG TRIAGE QUICK REFERENCE

When a bug is reported:

**CRITICAL** → Game-breaking, can't play  
→ Fix NOW (<4 hours), pull dev team off standby

**HIGH** → Major feature broken but workaround exists  
→ Fix ASAP (<24 hours), assign dev team today

**MEDIUM** → Feature degraded, cosmetic issue  
→ Fix this week (<2 days), queue for dev team

**LOW** → Rare edge case, nice-to-have  
→ Fix if time permits, can defer to post-release

---

## PHASE 2 TESTING PLAN (Nov 21-29)

58 test cases across 5 games:

- **Game 1 (Bump 5)**: 10 tests
- **Game 2 (Krazy 6)**: 10 tests
- **Game 3 (Pass the Chip)**: 12 tests
- **Game 4 (Bump U & 5)**: 12 tests
- **Game 5 (Solitary)**: 8 tests
- **UI/Input**: 6 tests

**Target**: 100% complete by Nov 29  
**Expected**: 90%+ pass rate  
**Outcome**: List of bugs for Phase 3 fixing

---

## DECISION FRAMEWORK

### When You Need to Make a Decision

Ask yourself:

1. **Is it a bug triage?** → Use Triage Framework (above)
2. **Is it about timeline?** → Escalate if >3 days impact
3. **Is it about features?** → Answer: "Does it affect Dec 15?" → If yes, DEFER
4. **Is it about people?** → Reassign work, keep moving

---

## ESCALATION PATHS

**Critical Bug Fix** (Need dev team to work ASAP)  
→ Slack #blockers → Dev Team Lead → Fix starts immediately

**Timeline Concern** (Might miss deadline)  
→ Email/message managing engineer → Assess options → Escalate if needed

**Resource Issue** (Not enough people)  
→ Direct message managing engineer → Redistribute work → Escalate if can't solve

**Stakeholder Pressure** (New feature request, timeline change)  
→ Escalate to project sponsor → Follow their direction → Document decision

---

## RED FLAGS (STOP EVERYTHING & ESCALATE)

🚨 **Critical bug found** that breaks core gameplay  
🚨 **Timeline at risk** by >3 days (behind schedule)  
🚨 **Platform rejection** from app stores (can't resubmit in time)  
🚨 **Key team member** becomes unavailable (can't complete work)  
🚨 **Scope creep** - stakeholder requesting new features  

**Action**: Stop everything, escalate to project sponsor immediately

---

## WEEKLY RHYTHM

**Monday**: Start week with standup  
**Tuesday-Thursday**: Daily standups + monitor progress  
**Friday 4 PM**: Weekly review meeting with all team leads  
**Friday Evening**: Send weekly report, plan next week  
**Weekend**: OFF (no work unless critical emergency)

---

## PHASE GATES (YOUR SIGN-OFFS)

### Phase 1→2 (Already Done ✅)
- 78/78 compatibility tests passed
- Zero critical bugs
- Go to Phase 2

### Phase 2→3 (Nov 29-30)
- 58/58 tests completed
- 90%+ pass rate
- Bugs triaged
- **Your Decision**: GO → Phase 3

### Phase 3→4 (Dec 14-15)
- All bugs fixed
- Final QA passed
- Submissions accepted
- **Your Decision**: APPROVED FOR RELEASE (or defer)

---

## CONTACT INFO

**QA Lead** (Phase 2 executor):
- Reports daily in standup
- Runs all 58 tests
- Contact: Slack, daily standup

**Dev Team Leads** (On-call):
- Available for critical bugs
- SLA: <4 hours for CRITICAL
- Contact: Slack #blockers

**Build Engineer** (Starts Dec 5):
- Prepares app store submissions
- Contact: Slack #build

**Your Direct Report**: None (independent coordinator)

---

## SUCCESS DEFINITION

You'll know you succeeded when:

- ✅ Dec 15 comes and you can confidently say "GO"
- ✅ Game releases with zero critical bugs
- ✅ All 5 game modes fully playable
- ✅ All platforms approved
- ✅ Team didn't burn out (sustainable pace)

---

## KEY DOCUMENTS LOCATION

| Document | Purpose | Read When |
|----------|---------|-----------|
| MANAGING_ENGINEER_PLAYBOOK.md | Daily ops | Every morning (key reference) |
| MANAGING_ENGINEER_STATUS_REPORT.md | Full status | Tonight (understand situation) |
| CURRENT_STATUS_INDEX.md | Team nav | Tomorrow (before standup) |
| SPRINT_STATUS.md | Real-time progress | Daily (track updates) |
| GAME_MODES_RULES_SUMMARY.md | Game rules | When testing game logic |
| _docs/STANDARDS/CODING_STANDARDS.md | Code rules | When reviewing pull requests |
| _docs/ARCHITECTURE/ARCHITECTURE.md | System design | When understanding complex issues |

---

## FINAL CHECKLIST (BEFORE TOMORROW)

- [ ] Read MANAGING_ENGINEER_PLAYBOOK.md (your playbook)
- [ ] Read MANAGING_ENGINEER_STATUS_REPORT.md (know the status)
- [ ] Understand bug triage framework
- [ ] Know the 9 AM UTC standup time + format
- [ ] Have Slack/email ready for communications
- [ ] Confirm QA Lead and Dev Team Leads will attend standup
- [ ] Prepare for Phase 2 testing to begin
- [ ] Bookmark SPRINT_STATUS.md (you'll check it daily)

---

## ONE MORE THING

The team that built this has done excellent work. They've already delivered 87.5% of the project ahead of schedule with high quality.

**Your job is not to find problems.** They'll find those (Phase 2 testing).

**Your job is to help them solve problems fast** and **keep everyone focused on Dec 15**.

Don't micromanage. Trust them. Support them. Unblock them. Protect the release date.

**You've got this. Now go read the Playbook.** 🚀

---

**Questions?** Check MANAGING_ENGINEER_PLAYBOOK.md first (it has answers).

**Got it?** Great. See you at 9 AM UTC tomorrow for standup.

---

*Prepared by: Amp*  
*Date: November 20, 2025, 8:30 PM UTC*  
*Status: READY FOR PHASE 2*
