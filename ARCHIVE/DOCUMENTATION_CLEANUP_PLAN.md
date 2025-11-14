# Documentation Cleanup & Consolidation Plan

**Date**: Nov 14, 2025  
**Status**: Active Consolidation  
**Owner**: Managing Engineer (Amp)

---

## Issue Analysis

### Identified Problems

1. **Document Duplication**: Multiple documents covering the same content
   - ENGINEERING_MANAGER.md vs MANAGING_ENGINEER_DASHBOARD.md vs MANAGING_ENGINEER_OPERATIONS.md
   - SPRINT_2_EXECUTION_PLAN.md vs SPRINT_2_KICKOFF_BRIEF.md vs SPRINT_2_COMPREHENSIVE.md
   - Multiple sprint status tracking files

2. **Inconsistent Information**:
   - Sprint 2 start date: Some say Nov 14, others say Nov 21
   - Team names inconsistent (Engineering Manager vs Managing Engineer)
   - Different SLAs mentioned across documents (4-hour, 24-hour)

3. **Outdated Documents in Root**:
   - ENGINEERING_MANAGER.md (older version, now superseded)
   - MANAGING_ENGINEER_DASHBOARD.md (sprint-specific, not general framework)
   - MANAGING_ENGINEER_ACCELERATION_BRIEFING.md (one-time briefing, not active operational doc)

4. **Archive Not Cleaned**:
   - 36+ files in ARCHIVE/ still visible in root directory navigation
   - Some ARCHIVE files referenced in active docs

5. **Incomplete Transitions**:
   - SPRINT_2 has 4 different "briefing" documents
   - SPRINT_STATUS.md references old docs in ARCHIVE/
   - DOCUMENTATION_INDEX.md not updated for new MANAGING_ENGINEER_OPERATIONS.md

---

## Consolidation Strategy

### Phase 1: Establish Single Source of Truth

#### Active Framework Documents (KEEP)
```
✅ MANAGING_ENGINEER_OPERATIONS.md (Master framework - created Nov 14)
   → Contains: Teams, daily ops, sprint lifecycle, decision-making
   → Audience: All teams, future agents, stakeholders

✅ AGENT_ONBOARDING.md (Agent guide - created Nov 14)
   → Contains: 13-step onboarding, communication, first-week checklist
   → Audience: All new agents

✅ PROJECT_PLAN.md (8-week timeline)
   → Contains: Master schedule, milestones
   → Audience: Leadership, project tracking

✅ ARCHITECTURE.md (System design)
   → Contains: Module structure, patterns, design decisions
   → Audience: All engineers

✅ CODING_STANDARDS.md (Code standards)
   → Contains: Naming, testing, documentation requirements
   → Audience: All engineers (daily reference)

✅ PROJECT_QUALITY_GATES.md (Code review framework)
   → Contains: Review process, checklists, acceptance criteria
   → Audience: ME, code reviewers

✅ SUBAGENT_TEAMS.md (Team structure)
   → Contains: 5 teams, roles, responsibilities
   → Audience: All agents

✅ DECISION_LOG.md (Decision history)
   → Contains: All decisions with rationale, append-only
   → Audience: All engineers (reference)
```

#### Sprint-Specific Documents (KEEP ONE PER SPRINT)
```
✅ SPRINT_N_EXECUTION_PLAN.md (Primary source)
   → Contains: Requirements, tasks, acceptance criteria, daily breakdown
   → Replaces: SPRINT_N_KICKOFF_BRIEF.md, SPRINT_N_COMPREHENSIVE.md
   → Audience: Team executing sprint

✅ SPRINT_N_STATUS_DAY#.md (Daily standup log)
   → Contains: Daily progress, blockers, metrics
   → Audience: Team, Managing Engineer

✅ SPRINT_N_COMPREHENSIVE_REVIEW.md (Created at sprint end)
   → Contains: Retro, metrics, lessons learned
   → Created: On sprint completion
   → Audience: All teams, stakeholders
```

---

## Phase 2: Archive & Consolidate

### Documents to Archive (MOVE to ARCHIVE/)

```
⬜ ENGINEERING_MANAGER.md
   → Reason: Superseded by MANAGING_ENGINEER_OPERATIONS.md
   → Action: Move to ARCHIVE/

⬜ MANAGING_ENGINEER_DASHBOARD.md
   → Reason: Sprint-specific, should be in sprint docs not root
   → Action: Move to ARCHIVE/

⬜ MANAGING_ENGINEER_ACCELERATION_BRIEFING.md
   → Reason: One-time briefing from Nov 14, historical value only
   → Action: Move to ARCHIVE/

⬜ SPRINT_2_KICKOFF_BRIEF.md
   → Reason: Duplicate of SPRINT_2_EXECUTION_PLAN.md
   → Action: Move to ARCHIVE/

⬜ SPRINT_2_COMPREHENSIVE_SUMMARY.md
   → Reason: Duplicate of SPRINT_2_EXECUTION_PLAN.md
   → Action: Move to ARCHIVE/ OR consolidate into EXECUTION_PLAN.md

⬜ SPRINT_2_CODE_REVIEW_CHECKLIST.md
   → Reason: Covered by PROJECT_QUALITY_GATES.md
   → Action: Move to ARCHIVE/

⬜ SPRINT_2_TEAM_DASHBOARD.md
   → Reason: Real-time status belongs in SPRINT_STATUS.md
   → Action: Move to ARCHIVE/

⬜ SPRINT_2_STATUS_DAY4.md
   → Reason: Keep only current active sprint's daily statuses
   → Action: Archive completed sprint days
```

### Documents to Update (MODIFY IN PLACE)

```
🔄 DOCUMENTATION_INDEX.md
   → Add: Link to MANAGING_ENGINEER_OPERATIONS.md
   → Add: Link to AGENT_ONBOARDING.md
   → Update: Remove references to archived docs
   → Clarify: Which docs are active vs archived

🔄 SPRINT_STATUS.md
   → Update: Reference MANAGING_ENGINEER_OPERATIONS.md for frameworks
   → Clarify: This tracks real-time sprint status only
   → Remove: References to archived docs
   → Add: Current sprint details only

🔄 REAL_TIME_PROJECT_DASHBOARD.md
   → Update: Link to MANAGING_ENGINEER_OPERATIONS.md
   → Clarify: High-level project view (burndown, velocity, metrics)
   → Remove: Duplicate team role information
   → Keep: Live metrics, blockers, risk assessment
```

### Documents to Keep As-Is (NO CHANGE)

```
✅ README.md (Project overview - correct)
✅ PROJECT_PLAN.md (Master schedule - correct)
✅ QUICK_REFERENCE.md (2-min overview - correct)
✅ PROJECT_STATUS_UPDATE_NOV14.md (Status snapshot - current)
✅ TIMELINE_UPDATE_NOV14.md (Timeline analysis - current)
✅ SPRINT_1_REVIEW.md (Sprint 1 completion - historical)
✅ SPRINT_2_EXECUTION_PLAN.md (Current sprint - correct)
✅ SPRINT_2_STATUS_DAY1.md (Active daily tracking - correct)
✅ SPRINT_3_COMPREHENSIVE.md (Sprint briefing - correct)
✅ SPRINT_4_KICKOFF.md (Sprint briefing - correct)
✅ SPRINT_5_KICKOFF.md (Sprint briefing - correct)
```

---

## Phase 3: Create Clean Directory Structure

### Root Level (Post-Cleanup)

```
/home/cjnf/Bump U/
├── DOCUMENTATION_INDEX.md (Master navigation - UPDATED)
│
├── CORE FRAMEWORK (Must read first)
├── README.md
├── QUICK_REFERENCE.md
├── AGENT_ONBOARDING.md ← NEW
├── MANAGING_ENGINEER_OPERATIONS.md ← NEW
│
├── STANDARDS & ARCHITECTURE (Daily reference)
├── CODING_STANDARDS.md
├── ARCHITECTURE.md
├── PROJECT_QUALITY_GATES.md
├── DECISION_LOG.md
├── SUBAGENT_TEAMS.md
│
├── PROJECT TRACKING
├── PROJECT_PLAN.md
├── PROJECT_STATUS_UPDATE_NOV14.md
├── SPRINT_STATUS.md
├── REAL_TIME_PROJECT_DASHBOARD.md
│
├── SPRINT DOCUMENTATION (Active)
├── SPRINT_1_REVIEW.md
├── SPRINT_2_EXECUTION_PLAN.md
├── SPRINT_2_STATUS_DAY1.md
├── SPRINT_3_COMPREHENSIVE.md
├── SPRINT_4_KICKOFF.md
├── SPRINT_5_KICKOFF.md
│
└── ARCHIVE/ (Historical - don't use)
    ├── ENGINEERING_MANAGER.md
    ├── MANAGING_ENGINEER_DASHBOARD.md
    ├── MANAGING_ENGINEER_ACCELERATION_BRIEFING.md
    ├── SPRINT_2_KICKOFF_BRIEF.md
    ├── SPRINT_2_COMPREHENSIVE_SUMMARY.md
    ├── SPRINT_2_CODE_REVIEW_CHECKLIST.md
    ├── ... (and all previous archived files)
```

---

## Phase 4: Consistency Fixes

### Sprint 2 Information (Fix Inconsistencies)

**Issue**: Documents disagree on Sprint 2 start date  
**Resolution**:
- MANAGING_ENGINEER_OPERATIONS.md: "Nov 14 immediate start (accelerated)"
- PROJECT_PLAN.md: Originally Nov 21
- SPRINT_STATUS.md: Both dates mentioned
- REAL_TIME_PROJECT_DASHBOARD.md: Says "Nov 14"

**Fix**: 
- Update PROJECT_PLAN.md to reflect acceleration decision
- Update SPRINT_STATUS.md to be clear: "Nov 21 originally planned, Nov 14 accelerated start" 
- Document decision in DECISION_LOG.md: "Sprint 2 accelerated from Nov 21 to Nov 14"

### SLA Consistency

**Issue**: Different SLAs mentioned in different docs  
**Current**:
- MANAGING_ENGINEER_DASHBOARD.md: "4-hour blocker SLA"
- MANAGING_ENGINEER_OPERATIONS.md: "Immediate response required"
- PROJECT_QUALITY_GATES.md: "24-hour review SLA"

**Fix**:
- MANAGING_ENGINEER_OPERATIONS.md is the standard (use this)
- Update other docs to reference it
- SLAs:
  - Blockers: < 1 hour acknowledgment, resolution within 4 hours
  - Code review: Within 24 hours
  - Architecture questions: Within 24 hours

### Team Terminology

**Issue**: "Engineering Manager" vs "Managing Engineer"  
**Fix**:
- Standard term: "Managing Engineer" (Amp role)
- Use consistently across all docs
- Update ENGINEERING_MANAGER.md title to note it's archived

---

## Phase 5: Documentation Maintenance Plan

### What Gets Updated When

```
MANAGING_ENGINEER_OPERATIONS.md
  → Updated: Once per project (this is now frozen, master framework)
  → Only if: Major operational change needed
  
AGENT_ONBOARDING.md
  → Updated: When adding new team structures or processes
  → Only if: Onboarding changes needed
  
SPRINT_N_EXECUTION_PLAN.md
  → Created: At sprint kickoff
  → Frozen: During sprint execution
  → Reviewed: At sprint completion
  
SPRINT_N_STATUS_DAY#.md
  → Updated: Daily during active sprint
  → Contains: Standup notes, blockers, progress
  → Archived: When sprint ends
  
DECISION_LOG.md
  → Updated: Append-only (add decisions as made)
  → Format: Date, Decision, Rationale, Impact, Owner
  → Never deleted or edited (historical record)
  
PROJECT_PLAN.md
  → Updated: Weekly if schedule changes
  → Review: Every Friday with stakeholders
  
REAL_TIME_PROJECT_DASHBOARD.md
  → Updated: Weekly (Friday reviews)
  → Contains: Burndown, velocity, blockers, risks
  
DOCUMENTATION_INDEX.md
  → Updated: When major doc changes made
  → Contains: Navigation and freshness status
```

---

## Phase 6: Cross-Reference Cleanup

### Links to Update

In DOCUMENTATION_INDEX.md:
- Remove: References to archived SPRINT_2_KICKOFF_BRIEF.md
- Add: Link to MANAGING_ENGINEER_OPERATIONS.md in "Managing Engineer" section
- Add: Link to AGENT_ONBOARDING.md in "Getting Started" section
- Update: All active sprint doc links

In SPRINT_STATUS.md:
- Remove: References to ARCHIVE/SPRINT_2_STATUS_DAY4.md
- Update: Links to current sprint docs only
- Add: Link to MANAGING_ENGINEER_OPERATIONS.md for framework reference

In active sprint EXECUTION_PLAN docs:
- Add: Reference to MANAGING_ENGINEER_OPERATIONS.md for team/ME coordination
- Add: Reference to AGENT_ONBOARDING.md for new agent joins

---

## Cleanup Execution Checklist

### Step 1: Move Files to Archive
- [ ] Move ENGINEERING_MANAGER.md → ARCHIVE/
- [ ] Move MANAGING_ENGINEER_DASHBOARD.md → ARCHIVE/
- [ ] Move MANAGING_ENGINEER_ACCELERATION_BRIEFING.md → ARCHIVE/
- [ ] Move SPRINT_2_KICKOFF_BRIEF.md → ARCHIVE/
- [ ] Move SPRINT_2_COMPREHENSIVE_SUMMARY.md → ARCHIVE/
- [ ] Move SPRINT_2_CODE_REVIEW_CHECKLIST.md → ARCHIVE/
- [ ] Move SPRINT_2_TEAM_DASHBOARD.md → ARCHIVE/
- [ ] Move SPRINT_2_STATUS_DAY4.md → ARCHIVE/

### Step 2: Update Core Documents
- [ ] Update DOCUMENTATION_INDEX.md (remove archived refs, add new docs)
- [ ] Update SPRINT_STATUS.md (clean references)
- [ ] Update PROJECT_PLAN.md (reflect Sprint 2 acceleration)
- [ ] Update REAL_TIME_PROJECT_DASHBOARD.md (consolidate info)

### Step 3: Fix Consistency
- [ ] Update DECISION_LOG.md: Add "Sprint 2 acceleration decision"
- [ ] Verify all SLA references point to MANAGING_ENGINEER_OPERATIONS.md
- [ ] Verify terminology: "Managing Engineer" (not "Engineering Manager")

### Step 4: Verification
- [ ] DOCUMENTATION_INDEX.md is current and complete
- [ ] All active docs in root are actually active
- [ ] All ARCHIVE/ docs moved out of root view
- [ ] No dead links in active documents
- [ ] No conflicting information in overlapping docs

---

## Results (Post-Cleanup)

### What Changes

**Before**: 50+ markdown files with duplication, confusion about which is current  
**After**: ~22 active files, clear organization, no duplication

**Before**: New agents read 5 different "briefing" documents per sprint  
**After**: New agents read 1 EXECUTION_PLAN.md + reference docs

**Before**: Multiple sources of truth (dashboard, briefing, execution plan all different)  
**After**: Single MANAGING_ENGINEER_OPERATIONS.md + sprint-specific EXECUTION_PLAN.md

### Benefits

✅ **Clarity**: No ambiguity about which doc to read  
✅ **Efficiency**: Agents spend less time searching, more time building  
✅ **Maintainability**: Changes made in one place, not five  
✅ **Scalability**: Easy to add new sprints/teams with clear template  
✅ **Professionalism**: Clean, organized, documented project  

---

## Timeline

- **Immediate** (Nov 14): Move files to ARCHIVE/
- **Within 1 hour**: Update DOCUMENTATION_INDEX.md, SPRINT_STATUS.md
- **Within 2 hours**: Fix consistency issues (dates, SLAs, terminology)
- **Within 4 hours**: Verify all links, run final check
- **Ongoing**: Maintain per "Maintenance Plan" above

---

## Owner & Authority

**Executed By**: Amp (Managing Engineer)  
**Approved By**: Project Standards  
**Status**: Active (execution in progress)  
**Next Review**: Friday, Nov 22 (post-Sprint 2 checkpoint)

---

**Start Date**: Nov 14, 2025  
**Target Complete**: Nov 14, 2025 (same day)  
**Status**: IN PROGRESS
