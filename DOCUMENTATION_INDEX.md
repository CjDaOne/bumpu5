# Documentation Index
**Master Guide to Project Documentation**

**Last Updated**: Nov 14, 2025  
**Project**: Bump U - Multi-Platform Box Game  
**Status**: Active

---

## 📋 Quick Navigation

### Active Documentation (Use These)

**Project Overview & Planning**
- [README.md](./README.md) - Project overview & description
- [PROJECT_PLAN.md](./PROJECT_PLAN.md) - 8-week timeline & milestones
- [TIMELINE_UPDATE_NOV14.md](./TIMELINE_UPDATE_NOV14.md) - Current status & timeline analysis

**Real-Time Tracking**
- [SPRINT_STATUS.md](./SPRINT_STATUS.md) - Current sprint progress (updated daily)
- [ARCHITECTURE.md](./ARCHITECTURE.md) - System design & architecture

**Standards & References**
- [CODING_STANDARDS.md](./CODING_STANDARDS.md) - C# code standards (ACTIVE - use daily)
- [QUICK_REFERENCE.md](./QUICK_REFERENCE.md) - 2-minute project overview
- [ENGINEERING_MANAGER.md](./ENGINEERING_MANAGER.md) - Managing Engineer role definition

**Team Structure**
- [SUBAGENT_TEAMS.md](./SUBAGENT_TEAMS.md) - Team assignments & responsibilities

**Decision Log**
- [DECISION_LOG.md](./DECISION_LOG.md) - Architectural decisions & rationale

---

## 🚀 Sprint Documentation

### Sprint 1: Core Logic Foundation
**Status**: ✅ COMPLETE

- [SPRINT_1_REVIEW.md](./SPRINT_1_REVIEW.md) - Code review & approval

**Archived**: Initial kickoff docs moved to ARCHIVE/

---

### Sprint 2: State Machine & Turn Flow
**Status**: 🟡 READY (Kickoff Nov 21)

- [SPRINT_2_COMPREHENSIVE.md](./SPRINT_2_COMPREHENSIVE.md) - **PRIMARY** - Complete briefing
  - All requirements in one document
  - Use this for implementation
  - Daily reference during sprint

**Deliverables**: GameStateManager + 4 classes, 22+ tests  
**Key Resource**: SPRINT_2_COMPREHENSIVE.md

---

### Sprint 3: Game Modes Architecture
**Status**: 📋 READY (Kickoff Nov 28)

- [SPRINT_3_COMPREHENSIVE.md](./SPRINT_3_COMPREHENSIVE.md) - **PRIMARY** - Complete briefing
  - 5 game mode specifications
  - IGameMode interface definition
  - GameModeFactory pattern
  - 40+ unit tests required

**Deliverables**: 5 modes + factory, 40+ tests  
**Key Resource**: SPRINT_3_COMPREHENSIVE.md

---

### Sprint 4: Board Visualization
**Status**: 📋 PLANNED (Kickoff Dec 5)

- [SPRINT_4_KICKOFF.md](./SPRINT_4_KICKOFF.md) - Board system implementation

**Deliverables**: BoardGridManager, cell interactions, animations  
**Key Resource**: SPRINT_4_KICKOFF.md

---

### Sprint 5: UI Framework & HUD
**Status**: 📋 PLANNED (Kickoff Dec 12)

- [SPRINT_5_KICKOFF.md](./SPRINT_5_KICKOFF.md) - UI system implementation

**Deliverables**: HUD, buttons, popups, menus  
**Key Resource**: SPRINT_5_KICKOFF.md

---

## 📁 File Organization

### Root Level (Active Documents)

```
/home/cjnf/Bump U/
├── DOCUMENTATION_INDEX.md (YOU ARE HERE)
├── README.md
├── PROJECT_PLAN.md
├── PROJECT_STATUS_UPDATE_NOV14.md
├── TIMELINE_UPDATE_NOV14.md
├── SPRINT_STATUS.md
├── SPRINT_1_REVIEW.md
├── SPRINT_2_COMPREHENSIVE.md ⭐
├── SPRINT_3_COMPREHENSIVE.md ⭐
├── SPRINT_4_KICKOFF.md
├── SPRINT_5_KICKOFF.md
├── ARCHITECTURE.md
├── CODING_STANDARDS.md
├── QUICK_REFERENCE.md
├── ENGINEERING_MANAGER.md
├── SUBAGENT_TEAMS.md
├── DECISION_LOG.md
├── ARCHIVE/ (old documents)
└── _docs/ (additional reference docs)
```

### ARCHIVE/ Folder (Outdated - Reference Only)

Contains superseded documentation from previous sessions:
- Session-specific prep documents
- Duplicate sprint documents (consolidated into COMPREHENSIVE.md files)
- Day-specific tracking sheets (replaced by real-time SPRINT_STATUS.md)
- Project initialization documents (one-time setup)

**Do not use ARCHIVE/ documents for current work.**

---

## 🎯 How to Use This Documentation

### For Sprint 2 Implementation (Nov 21-28)

1. **Start**: Read [SPRINT_2_COMPREHENSIVE.md](./SPRINT_2_COMPREHENSIVE.md)
2. **Reference**: Daily use of [CODING_STANDARDS.md](./CODING_STANDARDS.md)
3. **Track**: Update [SPRINT_STATUS.md](./SPRINT_STATUS.md) daily
4. **Verify**: Use success criteria in COMPREHENSIVE.md

### For Sprint 3 Implementation (Nov 28-Dec 5)

1. **Start**: Read [SPRINT_3_COMPREHENSIVE.md](./SPRINT_3_COMPREHENSIVE.md)
2. **Reference**: Daily use of [CODING_STANDARDS.md](./CODING_STANDARDS.md)
3. **Track**: Update [SPRINT_STATUS.md](./SPRINT_STATUS.md) daily
4. **Verify**: Use success criteria in COMPREHENSIVE.md

### For Project Overview

1. **Quick**: [QUICK_REFERENCE.md](./QUICK_REFERENCE.md) (2 min read)
2. **Detailed**: [README.md](./README.md) (5 min read)
3. **Technical**: [ARCHITECTURE.md](./ARCHITECTURE.md) (10 min read)

### For Standards & Quality

- [CODING_STANDARDS.md](./CODING_STANDARDS.md) - **Active daily reference**
- [ENGINEERING_MANAGER.md](./ENGINEERING_MANAGER.md) - ME responsibilities
- [DECISION_LOG.md](./DECISION_LOG.md) - Why we made certain choices

---

## 📊 Document Relationships

```
PROJECT_PLAN.md (master timeline)
    ↓
TIMELINE_UPDATE_NOV14.md (current analysis)
    ↓
SPRINT_STATUS.md (real-time tracking)
    ↓
SPRINT_X_COMPREHENSIVE.md (sprint-specific details)
    └── Uses standards from CODING_STANDARDS.md
    └── Architecture from ARCHITECTURE.md
    └── Team assignments from SUBAGENT_TEAMS.md

QUICK_REFERENCE.md (executive summary)
    └── Links to detailed docs as needed
```

---

## 🔄 Documentation Maintenance

### Updated Regularly (Check Before Work)
- [SPRINT_STATUS.md](./SPRINT_STATUS.md) - Updated daily during active sprint
- [DECISION_LOG.md](./DECISION_LOG.md) - Updated as decisions made
- [PROJECT_STATUS_UPDATE_NOV14.md](./PROJECT_STATUS_UPDATE_NOV14.md) - Status reviews

### Updated Weekly (Friday Reviews)
- [TIMELINE_UPDATE_NOV14.md](./TIMELINE_UPDATE_NOV14.md) - Progress analysis
- [PROJECT_PLAN.md](./PROJECT_PLAN.md) - If timeline adjustments needed

### Snapshot Documents (One-Time Per Sprint)
- SPRINT_X_COMPREHENSIVE.md - Created at sprint prep, static during sprint
- SPRINT_X_REVIEW.md - Created at sprint completion, static review

---

## 📌 Key Documents by Role

### Gameplay Engineer (Implementation)
**Primary Documents**:
1. [SPRINT_2_COMPREHENSIVE.md](./SPRINT_2_COMPREHENSIVE.md) (Current sprint)
2. [CODING_STANDARDS.md](./CODING_STANDARDS.md) (Daily reference)
3. [ARCHITECTURE.md](./ARCHITECTURE.md) (System overview)
4. [SPRINT_STATUS.md](./SPRINT_STATUS.md) (Daily check-in)

### Managing Engineer (Oversight)
**Primary Documents**:
1. [SPRINT_STATUS.md](./SPRINT_STATUS.md) (Real-time tracking)
2. [TIMELINE_UPDATE_NOV14.md](./TIMELINE_UPDATE_NOV14.md) (Progress analysis)
3. [SPRINT_2_COMPREHENSIVE.md](./SPRINT_2_COMPREHENSIVE.md) (Current sprint details)
4. [DECISION_LOG.md](./DECISION_LOG.md) (Architecture decisions)

### Project Leadership (Executive View)
**Primary Documents**:
1. [QUICK_REFERENCE.md](./QUICK_REFERENCE.md) (2-min overview)
2. [PROJECT_STATUS_UPDATE_NOV14.md](./PROJECT_STATUS_UPDATE_NOV14.md) (Status)
3. [PROJECT_PLAN.md](./PROJECT_PLAN.md) (Timeline)
4. [TIMELINE_UPDATE_NOV14.md](./TIMELINE_UPDATE_NOV14.md) (Progress)

---

## ✅ Documentation Standards

### All Documentation Must Include

- **Date Created/Updated**: When was it created
- **Status**: Current state (✅ Complete, 🟡 In Progress, 📋 Planned)
- **Owner**: Who maintains it
- **Purpose**: What is this document for
- **Contents**: What information it contains
- **Related Docs**: Links to related documents

### Format Standards

- **Markdown** (.md files) - All documentation
- **Clear Headers** - H1, H2, H3 hierarchy
- **Code Blocks** - Syntax highlighted where relevant
- **Links** - Cross-references to related docs
- **Tables** - For structured information (timelines, metrics)
- **Lists** - Bulleted for readability

---

## 🚀 Getting Started

### First Time?

1. Read [QUICK_REFERENCE.md](./QUICK_REFERENCE.md) (2 min)
2. Read [README.md](./README.md) (5 min)
3. Review [ARCHITECTURE.md](./ARCHITECTURE.md) (10 min)
4. Check [CODING_STANDARDS.md](./CODING_STANDARDS.md) (bookmark for reference)

### Starting a Sprint?

1. Read the SPRINT_X_COMPREHENSIVE.md for your sprint
2. Bookmark [CODING_STANDARDS.md](./CODING_STANDARDS.md)
3. Bookmark [SPRINT_STATUS.md](./SPRINT_STATUS.md) for daily updates
4. Review success criteria in COMPREHENSIVE.md

### Daily Work?

1. Update [SPRINT_STATUS.md](./SPRINT_STATUS.md) with progress
2. Reference [CODING_STANDARDS.md](./CODING_STANDARDS.md) for standards
3. Check [SPRINT_STATUS.md](./SPRINT_STATUS.md) for blockers
4. Refer to active SPRINT_X_COMPREHENSIVE.md for requirements

---

## 🔗 External Links

- **GitHub Repository**: https://github.com/CjDaOne/bumpu5
- **Amp Thread**: https://ampcode.com/threads/T-998a6b8f-b236-46c3-93bb-de8e5df608f8

---

## 📝 How to Report Issues or Questions

**For Blockers During Implementation**:
1. Document in [SPRINT_STATUS.md](./SPRINT_STATUS.md)
2. Create GitHub issue if needed
3. Escalate to Managing Engineer (Amp)

**For Documentation Issues**:
1. Note the specific section
2. Suggest improvement
3. Submit to Managing Engineer for update

**For Architecture Questions**:
1. Check [ARCHITECTURE.md](./ARCHITECTURE.md)
2. Check [DECISION_LOG.md](./DECISION_LOG.md)
3. Escalate if clarification needed

---

## 📈 Documentation Completeness

| Area | Status | Coverage |
|------|--------|----------|
| Project Overview | ✅ Complete | 100% |
| Architecture | ✅ Complete | 100% |
| Code Standards | ✅ Complete | 100% |
| Sprint 1 | ✅ Complete | 100% |
| Sprint 2 | ✅ Complete | 100% |
| Sprint 3 | ✅ Complete | 100% |
| Sprint 4-5 | 🟡 In Progress | Kickoff docs ready |
| Sprint 6-8 | 📋 Planned | To be created |

---

## 🎯 Summary

**Active Documents You'll Use**:
- ⭐ [SPRINT_2_COMPREHENSIVE.md](./SPRINT_2_COMPREHENSIVE.md) - Current sprint briefing
- ⭐ [CODING_STANDARDS.md](./CODING_STANDARDS.md) - Daily code reference
- ⭐ [SPRINT_STATUS.md](./SPRINT_STATUS.md) - Real-time progress tracking
- ⭐ [ARCHITECTURE.md](./ARCHITECTURE.md) - System design reference

**Archive**: Old docs moved to ARCHIVE/ folder (reference only, don't use)

**Structure**: Clean, organized, easy to navigate

**Next Steps**:
1. Bookmark the starred documents above
2. Use SPRINT_2_COMPREHENSIVE.md for Sprint 2 starting Nov 21
3. Update SPRINT_STATUS.md daily
4. Check TIMELINE_UPDATE_NOV14.md weekly for progress analysis

---

**Prepared By**: Managing Engineer (Amp)  
**Purpose**: Master documentation navigation guide  
**Status**: ✅ Active and Current
