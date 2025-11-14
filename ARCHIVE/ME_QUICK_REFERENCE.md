# MANAGING ENGINEER - QUICK REFERENCE GUIDE
## Daily Operations & Decision Making

**AUTHORITY**: Amp, Managing Engineer  
**UPDATED**: Nov 14, 2025  
**SCOPE**: Daily operations, escalation, triggers, approvals

---

## DAILY RESPONSIBILITIES (CHECKLIST)

### 9 AM UTC - Daily Standup
- [ ] All teams report (Completed / In-Progress / Blockers)
- [ ] Track progress on dashboard
- [ ] Identify risks immediately
- [ ] Confirm timeline targets
- [ ] 15 minutes max duration

### Throughout Day
- [ ] Monitor #blockers channel
- [ ] Review code submissions (< 4 hours)
- [ ] Approve/reject commits
- [ ] Respond to team questions (< 24 hours)
- [ ] Track daily progress

### Friday 5 PM UTC - Sprint Review
- [ ] Demo completed sprint deliverables
- [ ] Assess metrics & quality
- [ ] Review blockers & risks
- [ ] Plan next sprint activities
- [ ] 30 minutes max duration

---

## CODE REVIEW PROCESS (< 4 Hours)

### Submission Received
**Check**:
- [ ] All tests passing?
- [ ] Follows CODING_STANDARDS.md?
- [ ] 95%+ documented?
- [ ] No critical issues?

### Decision
**APPROVE IF**:
- ✅ Tests 100% passing
- ✅ Standards compliant
- ✅ Well documented
- ✅ No critical issues

**REJECT IF**:
- ❌ Tests failing
- ❌ Standards violation
- ❌ Missing documentation
- ❌ Critical issues found

### Action
- Approve → Merge immediately
- Reject → Send specific feedback, allow resubmit

---

## BLOCKER ESCALATION (< 1 Hour)

### Blocker Identified
1. Team posts in #blockers
2. ME reviews (< 1 hour)
3. ME decides (workaround or timeline change)
4. Team executes immediately

### Blocker Types

**Technical Blocker** (code issue):
- ME: Review code, provide solution
- Escalate: If security/architecture issue
- Response: < 4 hours

**Process Blocker** (external dependency):
- ME: Evaluate alternatives
- Decision: Proceed differently or delay
- Response: < 1 hour

**Resource Blocker** (missing tools/info):
- ME: Provide needed resource
- Decision: Provide workaround
- Response: < 1 hour

---

## CONDITIONAL START TRIGGERS

### Sprint 4 (Board Team) - Nov 19 (Expected)
**Trigger**: Game1_Bump5.cs + Game2_Krazy6.cs COMPLETE & TESTED  
**Evidence**: Both files in repo, 100% tests passing  
**Action**: Send "START NOW" message to Board team  
**Watch**: Game completion in daily standups  
**Contingency**: If late, start Nov 20-21 latest

### Sprint 5 (UI Team) - Nov 27 (Expected)
**Trigger**: BoardGridManager + BoardInputHandler COMPLETE & INTEGRATED  
**Evidence**: Board files in repo, integrated with game state  
**Action**: Send "START NOW" message to UI team  
**Watch**: Board team progress daily  
**Contingency**: If late, start Dec 1 latest

### Sprint 6 (Integration) - Dec 4 (Expected)
**Trigger**: UI at 75% completion  
**Evidence**: Daily standup report from UI team  
**Action**: Activate integration teams (Gameplay + UI)  
**Watch**: UI team progress daily  
**Contingency**: If late, start Dec 5 latest

### Sprint 7 (Build Team) - Dec 10 (Expected)
**Trigger**: Sprint 6 at 80% completion  
**Evidence**: Integration team progress report  
**Action**: Send "START NOW" message to Build team  
**Watch**: Integration team progress daily  
**Contingency**: If late, start Dec 11 latest

### Sprint 8 (QA Team) - Dec 17 (Expected)
**Trigger**: First builds complete (WebGL, Android, iOS)  
**Evidence**: Build team reports working builds  
**Action**: Send "START NOW" message to QA team  
**Watch**: Build team progress daily  
**Contingency**: If late, start Dec 18 latest

---

## SPRINT APPROVAL GATES

### Sprint Completion Approved IF:
- ✅ All deliverables implemented
- ✅ 100% unit test pass rate
- ✅ 95%+ documentation
- ✅ Code review approval
- ✅ No critical issues
- ✅ Performance targets met (if applicable)

### Sprint Completion Rejected IF:
- ❌ Deliverables incomplete
- ❌ Tests failing
- ❌ Critical bugs found
- ❌ Documentation missing
- ❌ Performance unacceptable

---

## GO/NO-GO DECISION (Dec 25)

### RELEASE GO Criteria
- ✅ 0 CRITICAL bugs
- ✅ All HIGH bugs fixed
- ✅ 60 FPS on modern devices
- ✅ 30+ FPS on older devices
- ✅ All 5 modes playable
- ✅ All 3 platforms working
- ✅ Documentation complete
- ✅ QA sign-off received

### RELEASE NO-GO Criteria
- ❌ CRITICAL bugs remain
- ❌ Performance unacceptable
- ❌ Platform failures
- ❌ Game breaking issues
- ❌ Data corruption risk

---

## QUALITY GATES (NON-NEGOTIABLE)

### Code Quality
- **Standard**: CODING_STANDARDS.md compliance
- **Testing**: 100% pass rate before approval
- **Documentation**: 95%+ public methods
- **Coverage**: 85%+ code coverage

### Testing
- **Unit Tests**: 200+ tests by end
- **Pass Rate**: 100% before code review
- **Coverage**: 85%+ branches tested
- **Edge Cases**: All covered

### Performance
- **Modern**: 60 FPS target
- **Older**: 30 FPS minimum
- **Memory**: Acceptable per platform
- **Startup**: < 5 seconds

---

## TEAM STATUS QUICK VIEW

| Team | Status | Next Milestone | Watch |
|------|--------|-----------------|-------|
| Gameplay | 🔴 Active | Game1 complete Nov 16 | Daily commits |
| Board | 🟡 Standby | Trigger Nov 19 | Game1+2 progress |
| UI | 🟡 Standby | Trigger Nov 27 | Board integration |
| Build | 🟡 Standby | Trigger Dec 10 | Integration progress |
| QA | 🟡 Standby | Trigger Dec 17 | Build completion |

---

## COMMUNICATION CHANNELS

### Team Channels
- #gameplay - Gameplay team updates
- #board - Board team updates
- #ui - UI team updates
- #build - Build team updates
- #qa - QA team updates
- #general - Overall project status

### Critical Channels
- #blockers - CRITICAL issues (escalation)
- Daily standup - 9 AM UTC (all teams)
- Sprint review - Friday 5 PM UTC (all teams)

---

## DECISION AUTHORITY MATRIX

| Decision | Authority | Timeline | Notes |
|----------|-----------|----------|-------|
| Code approval | ME | < 4 hours | Required for merge |
| Blocker resolution | ME | < 1 hour | Final decision |
| Sprint go/no-go | ME | At sprint end | Sign-off required |
| Trigger activation | ME | Immediately | When conditions met |
| Timeline adjustment | ME | < 24 hours | If needed |
| Quality gate enforcement | ME | Continuous | Non-negotiable |

---

## ESCALATION QUICK REFERENCE

### Level 1: Team Lead
- Attempts to resolve issue
- Escalates if unresolved in 2 hours

### Level 2: ME (#blockers)
- Reviews issue
- Decides on solution
- Response: < 1 hour
- Decision is final

### Level 3: Executive
- Not needed (ME has full authority)
- ME authority covers all decisions

---

## QUICK DECISION FLOWCHART

```
Issue Identified
    ↓
Is it code quality?
  ├─ YES → Send for rewrite (< 24 hours)
  └─ NO → Continue
    ↓
Is it a blocker?
  ├─ YES → 1-hour resolution window
  └─ NO → Continue
    ↓
Is it timeline critical?
  ├─ YES → Immediate decision
  └─ NO → 24-hour decision window
    ↓
ISSUE RESOLVED
```

---

## KEY METRICS TO TRACK DAILY

### Code Metrics
- [ ] Daily commits from all active teams
- [ ] Test pass rate (target: 100%)
- [ ] Code submission quality
- [ ] Code review approval rate

### Schedule Metrics
- [ ] Deliverables on track
- [ ] Team velocity stable
- [ ] Blockers: 0
- [ ] Risk level: LOW

### Quality Metrics
- [ ] Documentation complete
- [ ] Standards compliance
- [ ] Test coverage (target: 85%+)
- [ ] No critical issues

---

## WEEKLY REVIEW CHECKLIST

### Friday 5 PM UTC Review
- [ ] Sprint metrics reviewed
- [ ] All deliverables checked
- [ ] Quality gates confirmed
- [ ] Blockers resolved
- [ ] Next sprint confirmed
- [ ] Team morale confirmed

---

## TIMELINE REFERENCE

```
Nov 14-21: SPRINT 3 (Active)
Nov 19-26: SPRINT 4 (Conditional)
Nov 27-Dec 4: SPRINT 5 (Conditional)
Dec 4-11: SPRINT 6 (Concurrent)
Dec 10-18: SPRINT 7 (Concurrent)
Dec 17-25: SPRINT 8 (Concurrent)
```

**Release Date**: Dec 25, 2025 🎯

---

## EMERGENCY CONTACTS

**If Critical Issue**:
1. Team posts in #blockers
2. ME reviews (you)
3. ME decides (you)
4. Team executes immediately

**If Timeline at Risk**:
1. Assess impact
2. Determine mitigation
3. Adjust timeline if needed
4. Inform all teams

**If Quality Gate Breach**:
1. Identify issue
2. Reject code submission
3. Send feedback to team
4. Require rewrite/fix

---

## TONIGHT'S TODO (Nov 14)

- [ ] Create standup meeting link (Zoom/Discord)
- [ ] Send calendar invites to all teams
- [ ] Verify all dispatch orders are sent
- [ ] Prepare standup agenda
- [ ] Confirm Gameplay team readiness
- [ ] Review GitHub commits

---

## TOMORROW'S TODO (Nov 15)

### Morning (Before 9 AM UTC)
- [ ] Prepare standup room/link
- [ ] Review any overnight commits
- [ ] Check #blockers channel
- [ ] Prepare standup notes

### Standup (9 AM UTC)
- [ ] Conduct 15-minute standup
- [ ] Record progress
- [ ] Identify risks
- [ ] Confirm next steps

### Afternoon/Evening
- [ ] Review Game1 skeleton submission
- [ ] Provide feedback or approval
- [ ] Monitor team progress
- [ ] Update dashboard

---

**QUICK REFERENCE GUIDE**  
**Authority**: Amp, Managing Engineer  
**Status**: 🟢 OPERATIONAL  

**Keep this handy for daily operations.**

