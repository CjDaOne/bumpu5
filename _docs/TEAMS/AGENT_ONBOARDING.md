# Agent Onboarding Guide

**For All Subagents & Future Project Members**

**Created**: Nov 14, 2025  
**Audience**: Subagent Teams, AI Agents, Future Developers  
**Status**: Active

---

## Welcome to Bump U

You are joining a disciplined, well-documented game development project. This guide explains how we work together.

### Quick Facts

- **Project**: Bump U - A multi-platform dice/board game
- **Tech Stack**: Unity (C#), WebGL, Android, iOS
- **Management Model**: Managing Engineer + 5 Specialized Subagent Teams
- **Documentation**: Everything is documented; use it
- **Timeline**: 8 sprints across 10 weeks (Nov 14 - Jan 14)

---

## Step 1: Read These Documents (In Order)

### 5 Minutes
1. [QUICK_REFERENCE.md](./QUICK_REFERENCE.md) - Project snapshot
2. [README.md](./README.md) - Project description

### 15 Minutes
3. [ARCHITECTURE.md](./ARCHITECTURE.md) - System design overview
4. [SUBAGENT_TEAMS.md](./SUBAGENT_TEAMS.md) - Your team & peers

### 30 Minutes
5. **[MANAGING_ENGINEER_OPERATIONS.md](./MANAGING_ENGINEER_OPERATIONS.md)** - How we work (CRITICAL)
6. [CODING_STANDARDS.md](./CODING_STANDARDS.md) - Code style & quality

### Context (As Needed)
- Current sprint: SPRINT_N_COMPREHENSIVE.md (assigned by team lead)
- Decisions: [DECISION_LOG.md](./DECISION_LOG.md)
- Status: [SPRINT_STATUS.md](./SPRINT_STATUS.md)

---

## Step 2: Understand Your Team

You are assigned to ONE of these teams:

| Team | Lead | Focus |
|------|------|-------|
| **Gameplay Engineering** | Gameplay Lead | Core game logic, rules, game modes |
| **UI/UX Engineering** | UI Lead | Menus, HUD, popups, screens |
| **Board & Interaction** | Board Lead | Board visuals, cells, animations |
| **Build & Platform** | Build Lead | WebGL, Android, iOS builds |
| **QA & Testing** | QA Lead | Testing, playtesting, bug triage |

Your team lead will:
- Assign you specific stories
- Review your code
- Help unblock you
- Report progress to Managing Engineer

---

## Step 3: How We Work

### Daily Standup (15 min)

You report to your team lead:
- ✅ What did you complete?
- 🔄 What are you working on?
- 🚫 Are you blocked?

If you're blocked, say it immediately. The Managing Engineer unblocks you.

### Code Review & Submission

When you finish a task:
1. Write tests (80%+ coverage target)
2. Add comments explaining complex code
3. Update documentation if needed
4. Submit for review

The Managing Engineer (or your lead) will:
- Check code quality
- Verify acceptance criteria met
- Request changes if needed
- Approve & merge

### Sprint Cadence

- **Monday**: Sprint kickoff, get your tasks
- **Tue-Thu**: Daily standups, code reviews, development
- **Friday**: Sprint review, demo completed work, retrospective

---

## Step 4: File Organization

### Your Sprint

```
SPRINT_N_EXECUTION_PLAN.md      ← Your sprint goals & stories
SPRINT_N_STATUS_DAY#.md         ← Daily updates (you report here)
SPRINT_N_COMPREHENSIVE.md       ← Detailed requirements
```

### Your Code

**Gameplay Team**: `Assets/Scripts/Game/` + `Assets/Scripts/Managers/`  
**UI Team**: `Assets/Scripts/UI/` + `Assets/Prefabs/UI/`  
**Board Team**: `Assets/Scripts/Board/` + `Assets/Prefabs/Board/`  
**Build Team**: Unity build settings + deployment configs  
**QA Team**: Test plans + bug reports

### Reference Documents

```
QUICK_REFERENCE.md              ← 2-min overview (start here)
CODING_STANDARDS.md             ← Code style (bookmark this)
ARCHITECTURE.md                 ← System design
DECISION_LOG.md                 ← Why we made certain choices
MANAGING_ENGINEER_OPERATIONS.md ← How we coordinate
```

---

## Step 5: Communication

### When You Need Help

**Blocked by another team?**  
→ Tell your team lead in standup  
→ Managing Engineer resolves it

**Unclear on requirements?**  
→ Ask in standup  
→ Clarification documented in SPRINT_N_STATUS_DAY#.md

**Found a bug in existing code?**  
→ Document in DECISION_LOG.md or SPRINT_STATUS.md  
→ Alert Managing Engineer

### When to Escalate to Managing Engineer

- You're blocked (other team's work needed)
- Requirements are ambiguous
- Need architectural decision
- Code review has been waiting > 2 hours
- You think sprint scope is wrong

---

## Step 6: Code Quality

### Non-Negotiable Standards

- **No console errors** - Ever
- **No console warnings** - Unless documented
- **Tests required** - 80%+ coverage for logic, 60%+ for UI
- **Documentation** - All public methods have comments
- **Naming** - Clear, follows CODING_STANDARDS.md
- **No hardcoding** - Use configs, managers, etc.

### Before Submitting

- [ ] Code compiles with zero warnings
- [ ] All unit tests pass
- [ ] New tests written for new code
- [ ] Methods documented (XML comments)
- [ ] Complex logic has inline comments
- [ ] Acceptance criteria all met

---

## Step 7: Decision-Making

### You Have Authority Over

- How you implement your assigned tasks
- Technical approach (if it meets standards)
- Code structure in your team's scope

### You Escalate To Your Team Lead

- Need a different approach than spec'd
- Estimation was wrong (task too big/small)
- Need input from another team

### Managing Engineer Decides

- Cross-team architecture
- Sprint scope changes
- Performance/optimization tradeoffs
- Release readiness

All decisions are documented in [DECISION_LOG.md](./DECISION_LOG.md).

---

## Step 8: Typical Day

**9:00 AM - Daily Standup (15 min)**
- Your team lead leads standup
- Report: completed, in progress, blockers
- Any blockers? Managing Engineer helps immediately

**9:15 AM - 12:30 PM - Development**
- Write code following CODING_STANDARDS.md
- Ask questions in standup if unclear
- Commit frequently (small commits)

**12:30 PM - 1:30 PM - Lunch**

**1:30 PM - 5:00 PM - Continue Development + Code Reviews**
- Keep coding your assigned task
- Review submitted code from peers
- Update progress in SPRINT_STATUS_DAY#.md

**5:00 PM - EOD**
- Commit your work for the day
- Update status doc if significant progress

---

## Step 9: What NOT to Do

❌ **Do NOT**:
- Skip code review (always review others' work)
- Commit without tests
- Ignore console warnings
- Work on unassigned tasks
- Skip standup or hide blockers
- Hardcode values (use configs)
- Write code that violates CODING_STANDARDS.md
- Change sprint scope without Managing Engineer approval
- Merge your own code (always need review)
- Ignore documentation in ARCHITECTURE.md or DECISION_LOG.md

---

## Step 10: Success Criteria

You are successful when:

✅ **Your assigned tasks are complete**  
✅ **Your code passes code review without major revisions**  
✅ **Your code has 80%+ test coverage (logic) or 60%+ (UI)**  
✅ **Your code is deployed and working**  
✅ **You communicate blockers immediately**  
✅ **You follow CODING_STANDARDS.md consistently**  
✅ **You document your decisions (why you built it that way)**  

---

## Step 11: Your Team Lead Is Your First Point of Contact

Your team lead:
- Assigns you tasks
- Reviews your code
- Helps you learn the codebase
- Escalates blockers to Managing Engineer
- Tracks your progress

**You don't talk directly to Managing Engineer unless escalated.**

---

## Step 12: Vocabulary You'll Hear

| Term | Meaning |
|------|---------|
| **Sprint** | 5 calendar days of focused work (Mon-Fri) |
| **Story** | A feature or task to complete |
| **Acceptance Criteria** | What defines "done" for a story |
| **Blocker** | Something preventing you from moving forward |
| **Carry-over** | Task didn't finish, moves to next sprint |
| **Merge** | Code is reviewed & combined into main branch |
| **Code Review** | Someone checks your code before merging |
| **LGTM** | "Looks Good To Me" - code approved |
| **Needs Work** | Code review failed, fix and resubmit |

---

## Step 13: Emergency Contacts

**Your Team Lead**: Assigned at sprint kickoff  
**Managing Engineer**: @Amp (escalation only)  
**Project Owner**: Escalated from Managing Engineer  

---

## First Week Checklist

- [ ] Read all 5 onboarding docs above (Step 1)
- [ ] Understand your team's role (Step 2)
- [ ] Review CODING_STANDARDS.md daily (Step 6)
- [ ] Attend daily standup (Step 8)
- [ ] Ask questions - don't be stuck
- [ ] Get your first task assigned
- [ ] Set up your local dev environment
- [ ] Make your first code commit
- [ ] Pass code review (might need revisions)
- [ ] See your code merged into develop

---

## Questions You Might Have

### "Where do I get my tasks?"
From your team lead at sprint kickoff. Check SPRINT_N_EXECUTION_PLAN.md for full sprint scope.

### "How do I run the project locally?"
See [README.md](./README.md) - Development Setup section.

### "What coding style should I use?"
Follow [CODING_STANDARDS.md](./CODING_STANDARDS.md) exactly. No exceptions.

### "How do I know if my code is good?"
If it:
- Compiles with zero warnings
- Has 80%+ test coverage
- Passes code review
- Meets acceptance criteria
- Follows CODING_STANDARDS.md

Then it's good.

### "What if I'm blocked?"
1. Tell your team lead immediately in standup
2. Managing Engineer helps unblock you same day
3. Never stay blocked > 1 hour

### "What if I disagree with a decision?"
1. Discuss with your team lead
2. If still unclear, escalate to Managing Engineer
3. Managing Engineer decides (documented in DECISION_LOG.md)
4. Everyone implements the decision

---

## Key Reminders

✅ **Documentation is truth** - Always check docs before asking  
✅ **Communicate early** - Don't hide problems  
✅ **Code quality matters** - No shortcuts on standards  
✅ **Tests matter** - Every feature needs tests  
✅ **Respect deadlines** - Sprints are 5 days, focused  
✅ **Help your team** - Code review others' work  
✅ **Learn from decisions** - Check DECISION_LOG.md for context  

---

## Next Steps

1. **Right now**: Finish reading all 5 onboarding docs
2. **Next**: Read your current sprint's COMPREHENSIVE.md
3. **Tomorrow**: Attend standup, get your first task
4. **Then**: Start coding!

---

## Welcome to the Team

You're joining a well-run, documented, professional project. We're building something great, and we're doing it right.

Ask questions. Follow standards. Help your team. Deliver on time.

Let's build Bump U.

---

**Document Owner**: Managing Engineer (Amp)  
**Last Updated**: Nov 14, 2025  
**Status**: Active - All new agents must read this on day 1
