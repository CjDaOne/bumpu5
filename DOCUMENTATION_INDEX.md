# Documentation Index - Active Documents Only
**Master Guide to Project Documentation**

**Last Updated**: Nov 14, 2025 (Post-Cleanup)  
**Project**: Bump U - Multi-Platform Box Game  
**Status**: ✅ Consolidated & Synchronized

---

## 📌 Quick Start

### For New Agents (First Time)
1. Read: **AGENT_ONBOARDING.md** (20 min)
2. Read: **QUICK_REFERENCE.md** (2 min)
3. Bookmark: **CODING_STANDARDS.md** (daily reference)

### For Active Sprint Work
1. Read: **SPRINT_N_COMPREHENSIVE.md** (sprint-specific)
2. Reference: **CODING_STANDARDS.md** (daily)
3. Check: **SPRINT_STATUS.md** (real-time)

### For Project Overview
1. Read: **README.md** (5 min)
2. Read: **QUICK_REFERENCE.md** (2 min)
3. Check: **PROJECT_PLAN.md** (timeline & milestones)

---

## 📚 Active Documentation (22 Essential Files)

### TIER 1: Read First (All Agents)

| File | Purpose | Audience | Update Frequency |
|------|---------|----------|------------------|
| **AGENT_ONBOARDING.md** | 13-step agent orientation | All agents | Per-project |
| **QUICK_REFERENCE.md** | 2-minute project overview | All | Rarely |
| **README.md** | Project description & setup | All | Rarely |

### TIER 2: Framework & Standards (Daily Reference)

| File | Purpose | Audience | Update Frequency |
|------|---------|----------|------------------|
| **MANAGING_ENGINEER_OPERATIONS.md** | ⭐ Master operations framework | ME, all teams | Per-project |
| **CODING_STANDARDS.md** | Code style & quality standards | All engineers | Per-project |
| **ARCHITECTURE.md** | System design & module structure | All engineers | Rarely |
| **PROJECT_QUALITY_GATES.md** | Code review framework & checklist | ME, reviewers | Rarely |
| **SUBAGENT_TEAMS.md** | Team structure & responsibilities | All | Per-project |
| **DECISION_LOG.md** | Decision history (append-only) | All | Daily during work |

### TIER 3: Project Tracking

| File | Purpose | Audience | Update Frequency |
|------|---------|----------|------------------|
| **PROJECT_PLAN.md** | 8-week timeline & milestones | ME, leadership | Weekly |
| **SPRINT_STATUS.md** | Real-time sprint progress | ME, team | Daily (active sprint) |
| **REAL_TIME_PROJECT_DASHBOARD.md** | Live metrics & burndown | ME, leadership | Weekly |

### TIER 4: Sprint Documentation

#### Completed Sprints
| File | Sprint | Purpose | Status |
|------|--------|---------|--------|
| **SPRINT_1_REVIEW.md** | Sprint 1 | Code review & approval | ✅ Complete |

#### Active/Upcoming Sprints
| File | Sprint | Purpose | Status |
|------|--------|---------|--------|
| **SPRINT_2_COMPREHENSIVE.md** | Sprint 2 | ⭐ Primary briefing | 🟡 Ready Nov 21 |
| **SPRINT_3_COMPREHENSIVE.md** | Sprint 3 | ⭐ Primary briefing | 🟡 Ready Nov 28 |
| **SPRINT_4_KICKOFF.md** | Sprint 4 | Briefing & kickoff | 📋 Planned Dec 5 |
| **SPRINT_5_KICKOFF.md** | Sprint 5 | Briefing & kickoff | 📋 Planned Dec 12 |

### TIER 5: Reference & Status

| File | Purpose | Audience | Update Frequency |
|------|---------|----------|------------------|
| **DOCUMENTATION_INDEX.md** | This file - navigation guide | All | Per-cleanup |
| **FINAL_DOCUMENTATION_COMPLETE.md** | Nov 14 completion summary | All | Historical |
| **EXEC_SUMMARY_NOV14.md** | Executive summary | Leadership | Historical |

---

## 🗂️ File Organization

### Root Level (Active Documents Only)

```
/home/cjnf/Bump U/
├── AGENT_ONBOARDING.md ← START HERE (first time)
├── MANAGING_ENGINEER_OPERATIONS.md ← MASTER framework
│
├── Framework & Standards (Bookmark these)
├── CODING_STANDARDS.md ← Daily code reference
├── ARCHITECTURE.md
├── PROJECT_QUALITY_GATES.md
├── DECISION_LOG.md (append-only)
├── SUBAGENT_TEAMS.md
│
├── Project Overview
├── README.md
├── QUICK_REFERENCE.md
│
├── Project Tracking (Update regularly)
├── PROJECT_PLAN.md (master timeline)
├── SPRINT_STATUS.md (real-time)
├── REAL_TIME_PROJECT_DASHBOARD.md (weekly)
│
├── Sprint Documentation
├── SPRINT_1_REVIEW.md ✅
├── SPRINT_2_COMPREHENSIVE.md ← Primary sprint brief
├── SPRINT_3_COMPREHENSIVE.md
├── SPRINT_4_KICKOFF.md
├── SPRINT_5_KICKOFF.md
│
├── Reference
├── DOCUMENTATION_INDEX.md (you are here)
├── FINAL_DOCUMENTATION_COMPLETE.md (Nov 14 summary)
├── EXEC_SUMMARY_NOV14.md (executive summary)
│
└── ARCHIVE/ (historical - don't use)
    └── (50+ archived docs from previous sessions)
```

---

## 🎯 How to Use This Documentation

### Scenario 1: New Agent Joining Project

**Time Commitment**: 1 hour

1. **Read** AGENT_ONBOARDING.md (20 min)
   - Context, history, getting started
   
2. **Read** QUICK_REFERENCE.md (2 min)
   - Project at a glance
   
3. **Read** README.md (5 min)
   - Full project description
   
4. **Review** MANAGING_ENGINEER_OPERATIONS.md (20 min)
   - How teams work, communication, standards
   
5. **Bookmark** CODING_STANDARDS.md
   - Daily code reference (5 min per day of coding)

### Scenario 2: Starting a New Sprint

**Time Commitment**: 30 minutes

1. **Read** SPRINT_N_COMPREHENSIVE.md (primary source)
   - All requirements, deliverables, acceptance criteria
   - This is your single source of truth
   
2. **Bookmark** CODING_STANDARDS.md (daily)
   - Code style & quality requirements
   
3. **Bookmark** SPRINT_STATUS.md (daily)
   - Check progress, blockers, updates
   
4. **Bookmark** PROJECT_QUALITY_GATES.md
   - Code review standards (reference when submitting code)

### Scenario 3: Daily Work During Sprint

**Daily Workflow**:
1. Check SPRINT_STATUS.md (2 min) - blockers, updates
2. Code using CODING_STANDARDS.md (reference as needed)
3. Submit code and check PROJECT_QUALITY_GATES.md
4. Update SPRINT_STATUS.md with progress (5 min)

### Scenario 4: Questions About System Architecture

1. **Technical Design**: ARCHITECTURE.md
2. **Why a Decision was Made**: DECISION_LOG.md
3. **Code Standards**: CODING_STANDARDS.md

### Scenario 5: Project Status Report (For Leadership)

1. **Quick**: EXEC_SUMMARY_NOV14.md (2 min)
2. **Timeline**: PROJECT_PLAN.md (5 min)
3. **Current Status**: REAL_TIME_PROJECT_DASHBOARD.md (3 min)
4. **Detailed**: Individual SPRINT_N_COMPREHENSIVE.md files

---

## 📖 Document Relationships

### Operations & Process
```
MANAGING_ENGINEER_OPERATIONS.md (MASTER)
  ├─ Team structure (SUBAGENT_TEAMS.md)
  ├─ Code standards (CODING_STANDARDS.md)
  ├─ Code reviews (PROJECT_QUALITY_GATES.md)
  └─ Decision-making (DECISION_LOG.md)
```

### Planning & Tracking
```
PROJECT_PLAN.md (Master Timeline)
  ↓
  Sprint Kickoff (SPRINT_N_COMPREHENSIVE.md)
  ↓
  Daily Execution (SPRINT_STATUS.md + daily work)
  ↓
  Weekly Review (REAL_TIME_PROJECT_DASHBOARD.md)
  ↓
  Sprint Completion (historical archive)
```

### Code Development
```
CODING_STANDARDS.md (How to code)
  ↓
  Write code daily
  ↓
  PROJECT_QUALITY_GATES.md (Code review)
  ↓
  DECISION_LOG.md (Record decisions)
```

---

## 🚀 Key Documents by Role

### For Gameplay Engineer (Sprint 2-3)
**Start Here**:
- AGENT_ONBOARDING.md (orientation)
- SPRINT_2_COMPREHENSIVE.md (sprint requirements)
- CODING_STANDARDS.md (daily reference)

**Reference**:
- ARCHITECTURE.md (system design)
- MANAGING_ENGINEER_OPERATIONS.md (team coordination)
- PROJECT_QUALITY_GATES.md (code review standards)

### For Managing Engineer
**Daily**:
- SPRINT_STATUS.md (real-time tracking)
- PROJECT_QUALITY_GATES.md (code reviews)
- MANAGING_ENGINEER_OPERATIONS.md (operations)

**Weekly**:
- REAL_TIME_PROJECT_DASHBOARD.md (metrics)
- PROJECT_PLAN.md (timeline review)
- DECISION_LOG.md (decisions made)

### For Project Leadership
**Overview**:
- QUICK_REFERENCE.md (2-min status)
- EXEC_SUMMARY_NOV14.md (detailed status)
- PROJECT_PLAN.md (timeline)

**Details**:
- REAL_TIME_PROJECT_DASHBOARD.md (metrics)
- SPRINT_N_COMPREHENSIVE.md (sprint details)

---

## ✅ Documentation Status

### By Category

| Category | Files | Completeness | Status |
|----------|-------|--------------|--------|
| **Framework** | 3 | 100% | ✅ Complete |
| **Standards** | 6 | 100% | ✅ Complete |
| **Planning** | 3 | 100% | ✅ Complete |
| **Sprint 1** | 1 | 100% | ✅ Complete |
| **Sprint 2** | 1 | 100% | ✅ Ready |
| **Sprint 3** | 1 | 100% | ✅ Ready |
| **Sprint 4-5** | 2 | 100% | ✅ Ready |
| **Reference** | 4 | 100% | ✅ Complete |

**Total Active Documents**: 22 files  
**Total Archived (Historical)**: 50+ files in ARCHIVE/  
**Consolidation Status**: ✅ Clean, organized, no duplication

---

## 🔄 Document Maintenance Rules

### What Gets Updated & When

| Document | Update Pattern | Trigger |
|----------|---|---|
| CODING_STANDARDS.md | Rarely | Major standard change |
| ARCHITECTURE.md | Rarely | System redesign |
| MANAGING_ENGINEER_OPERATIONS.md | Rarely | Process change |
| PROJECT_PLAN.md | Weekly | Timeline adjustments |
| DECISION_LOG.md | Daily | Decisions made |
| SPRINT_N_COMPREHENSIVE.md | On creation | Created at sprint kickoff, frozen during sprint |
| SPRINT_STATUS.md | Daily | During active sprint |
| REAL_TIME_PROJECT_DASHBOARD.md | Weekly | Friday reviews |

### ARCHIVE/ Folder Rules

- **Never use ARCHIVE/ documents** for current work
- Move files here **on sprint completion**
- Keep for **historical reference** only
- Named with `.archived` suffix for clarity

---

## 🚨 What NOT to Do

❌ **Don't create parallel briefing documents**  
→ One SPRINT_N_COMPREHENSIVE.md per sprint (single source of truth)

❌ **Don't keep daily standup files in root**  
→ Archive when sprint ends (keep current sprint only)

❌ **Don't reference archived documents**  
→ Use active documents, check DECISION_LOG for historical context

❌ **Don't update DOCUMENTATION_INDEX without cleanup**  
→ Only update when documents change

❌ **Don't duplicate framework information**  
→ MANAGING_ENGINEER_OPERATIONS.md is the authority

---

## 🔗 External Links

- **GitHub Repository**: https://github.com/CjDaOne/bumpu5
- **Project Thread**: https://ampcode.com/threads/T-f971c422-4dfd-4dc7-ad2d-c627278dd675

---

## 📋 How to Report Issues

### Documentation Issue?
1. Note the specific section/file
2. Flag in SPRINT_STATUS.md
3. Escalate to Managing Engineer

### Conflicting Information?
1. Check DECISION_LOG.md (why decision was made)
2. Check MANAGING_ENGINEER_OPERATIONS.md (source of truth)
3. Escalate if unresolved

### Need to Update a Document?
1. Check ownership (file header)
2. Propose change to Managing Engineer
3. Implementation follow document rules above

---

## 📈 Project Completion Status

| Component | Status | Completion |
|-----------|--------|-----------|
| Core Framework | ✅ Complete | 100% |
| Sprint 1 | ✅ Complete | 100% |
| Sprint 2 | 🟡 Ready | 0% (starts Nov 21) |
| Sprint 3 | 🟡 Ready | 0% (starts Nov 28) |
| Sprint 4-5 | 📋 Planned | 0% (Dec 5-19) |
| Sprint 6-8 | 📋 Planned | 0% (Dec 19-Jan 9) |

**Overall Documentation**: ✅ 100% Complete  
**Overall Code**: ✅ Sprint 1 Complete, Sprints 2-8 Planned

---

## 🎯 Navigation Summary

### Bookmark These (Most Used)
- ⭐ CODING_STANDARDS.md - Code reference (daily)
- ⭐ SPRINT_N_COMPREHENSIVE.md - Current sprint (daily)
- ⭐ SPRINT_STATUS.md - Real-time progress (daily)
- ⭐ MANAGING_ENGINEER_OPERATIONS.md - Operations (reference)

### For One-Time Reference
- QUICK_REFERENCE.md - Project overview (once)
- AGENT_ONBOARDING.md - Agent orientation (once)
- ARCHITECTURE.md - System design (reference as needed)

### For Leadership/Reports
- EXEC_SUMMARY_NOV14.md - Executive summary
- PROJECT_PLAN.md - Timeline & milestones
- REAL_TIME_PROJECT_DASHBOARD.md - Live metrics

---

## ✨ Post-Cleanup Summary

**What Changed**:
- Removed 8 duplicate/archived documents
- Consolidated 4 project status documents
- Archived obsolete briefings
- Created single source of truth per topic

**What Stayed**:
- All critical framework documents
- All sprint briefings (single file per sprint)
- All code standards
- All tracking dashboards

**Result**:
- ✅ No conflicting information
- ✅ No document duplication
- ✅ Clean, organized structure
- ✅ All documents in sync

---

**Document Owner**: Managing Engineer (Amp)  
**Last Updated**: Nov 14, 2025 (Post-Cleanup)  
**Status**: ✅ Active & Current  
**Review Schedule**: Weekly (Friday)
