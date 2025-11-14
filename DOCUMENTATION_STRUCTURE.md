# Documentation Structure & Organization
## Root Directory Cleanup - Nov 14, 2025

---

## ROOT LEVEL FILES (Essential Only)

### Core Project Documentation
- README.md (project overview)
- PROJECT_PLAN.md (original timeline & scope)
- ARCHITECTURE.md (system design)
- CODING_STANDARDS.md (dev guidelines)

### Current Status (Always Updated)
- SPRINT_STATUS.md (real-time sprint tracking) ✅ KEEP
- PROJECT_STATUS_CURRENT.md (overall project health) → KEEP

### Active Operational Docs (Current Phase)
- ME_ACCELERATION_ORDER_ALL_TEAMS.md (active order) ✅ KEEP
- ME_DAILY_STANDUP_TEMPLATE.md (coordination) ✅ KEEP
- TEAM_DISPATCH_SPRINT4_BOARD_PREP.md (Board team) ✅ KEEP
- TEAM_DISPATCH_SPRINT5_UI_PREP.md (UI team) ✅ KEEP

---

## ARCHIVE/ (Remove from Root)

### Obsolete Dispatch Orders
- DEPLOYMENT_ORDER_ISSUED_NOV14.md → ARCHIVE
- DEPLOYMENT_COMPLETE_SUMMARY.md → ARCHIVE
- EXECUTION_ACTIVE_NOW.md → ARCHIVE
- ME_COMMAND_BRIEF.md → ARCHIVE
- ME_COMMAND_CENTER_LIVE.md → ARCHIVE
- ME_DEPLOYMENT_ORDER_TEAMS_PROCEED.md → ARCHIVE
- ME_DEPLOYMENT_SUMMARY_NOV14.md → ARCHIVE
- ME_DOCUMENTATION_CLEANUP_BRIEFING.md → ARCHIVE
- ME_EXECUTION_MANIFEST.md → ARCHIVE
- ME_EXECUTION_STATUS_REALTIME.md → ARCHIVE
- ME_IMMEDIATE_DEPLOYMENT_EXECUTION_NOV14.md → ARCHIVE
- ME_PARALLEL_ACCELERATION_ORDER.md → ARCHIVE
- ME_TEAM_COORDINATION_LIVE.md → ARCHIVE
- ME_TEAM_DISPATCH_EXECUTION_NOV14.md → ARCHIVE

### Obsolete Documentation
- DOCUMENTATION_CLEANED.md → ARCHIVE
- DOCUMENTATION_CLEANUP_PLAN.md → ARCHIVE
- MANAGING_ENGINEER_ACTIVE_DASHBOARD.md → ARCHIVE
- TEAM_DISPATCH_DOCUMENTATION_CLEANUP.md → ARCHIVE

### Kept (Historical Reference)
- ME_SPRINT3_COMPLETION_ORDER.md → Keep in root (recent, important)
- ME_SPRINT4_DESIGN_COMPLETION.md → Keep in root (recent, important)
- ME_SPRINT5_DESIGN_COMPLETION.md → Keep in root (recent, important)
- ME_SPRINTS_3_5_COMPLETION_SUMMARY.md → Keep in root (recent, important)

---

## DOCS/ or _DOCS/ (Organized Documentation)

Create a logical folder structure:

```
_docs/
├── STANDARDS/
│   ├── CODING_STANDARDS.md
│   ├── UI_STANDARDS.md
│   └── COMMUNICATION_TEMPLATES.md
│
├── ARCHITECTURE/
│   ├── ARCHITECTURE.md
│   ├── BOARD_ARCHITECTURE.md
│   ├── HUD_ARCHITECTURE.md
│   └── ASSET_INTEGRATION.md
│
├── SPRINTS/
│   ├── SPRINT_1_REVIEW.md
│   ├── SPRINT_2_FINAL_SIGNOFF.md
│   ├── SPRINT_3_DETAILED_BRIEFING.md
│   ├── SPRINT_STATUS.md
│   └── SPRINT_COMPLETION_STATUS.md
│
├── GAME_DESIGN/
│   ├── GAME_MODES_RULES_SUMMARY.md
│   ├── PLATFORM_REQUIREMENTS.md
│   ├── INPUT_HANDLING.md
│   ├── BUILD_PIPELINE.md
│   └── APP_STORE_REQUIREMENTS.md
│
├── UI/
│   ├── UI_DESIGN_SYSTEM.md
│   ├── HUD_ARCHITECTURE.md
│   └── UI_STANDARDS.md
│
├── TEAMS/
│   ├── TEAM_DEPLOYMENT_CARDS.md
│   ├── SUBAGENT_TEAMS.md
│   ├── MANAGING_ENGINEER_INDEX.md
│   └── AGENT_ONBOARDING.md
│
├── TESTING/
│   └── TEST_PLAN_MASTER.md
│
└── PLANNING/
    ├── PROJECT_PLAN.md
    ├── DECISION_LOG.md
    └── DELIVERABLES_MANIFEST.txt
```

---

## FINAL ROOT STRUCTURE (Post-Cleanup)

**Essential Files Only (20 files max)**:

1. README.md
2. PROJECT_PLAN.md
3. ARCHITECTURE.md
4. CODING_STANDARDS.md
5. SPRINT_STATUS.md
6. PROJECT_STATUS_CURRENT.md
7. ME_ACCELERATION_ORDER_ALL_TEAMS.md
8. ME_DAILY_STANDUP_TEMPLATE.md
9. ME_SPRINT3_COMPLETION_ORDER.md
10. ME_SPRINT4_DESIGN_COMPLETION.md
11. ME_SPRINT5_DESIGN_COMPLETION.md
12. ME_SPRINTS_3_5_COMPLETION_SUMMARY.md
13. TEAM_DISPATCH_SPRINT3_GAMEPLAY_EXECUTE.md
14. TEAM_DISPATCH_SPRINT4_BOARD_PREP.md
15. TEAM_DISPATCH_SPRINT5_UI_PREP.md
16. TEAM_DISPATCH_SPRINT7_BUILD_PREP.md
17. TEAM_DISPATCH_SPRINT8_QA_PLANNING.md
18. GAME_MODES_RULES_SUMMARY.md
19. TEST_PLAN_MASTER.md
20. .gitignore, Assembly files, etc.

---

## CLEANUP COMMANDS

```bash
# Move obsolete files to ARCHIVE
mv DEPLOYMENT_*.md ARCHIVE/
mv EXECUTION_ACTIVE_NOW.md ARCHIVE/
mv ME_COMMAND_*.md ARCHIVE/
mv ME_DEPLOYMENT_*.md ARCHIVE/
mv ME_DOCUMENTATION_*.md ARCHIVE/
mv ME_EXECUTION_*.md ARCHIVE/
mv ME_IMMEDIATE_*.md ARCHIVE/
mv ME_PARALLEL_*.md ARCHIVE/
mv ME_TEAM_COORDINATION_*.md ARCHIVE/
mv ME_TEAM_DISPATCH_EXECUTION_*.md ARCHIVE/
mv DOCUMENTATION_CLEAN*.md ARCHIVE/
mv MANAGING_ENGINEER_ACTIVE_*.md ARCHIVE/
mv TEAM_DISPATCH_DOCUMENTATION_*.md ARCHIVE/

# Organized files can stay in root (only 20 essential ones)
```

---

## Benefits

✅ Root directory: Clean & minimal (only current operational docs)  
✅ _docs/ folder: Organized by purpose (standards, architecture, sprints, etc.)  
✅ ARCHIVE/: All historical/obsolete files  
✅ Easier navigation: Team members know where to find what  
✅ Git cleaner: Fewer files clutter the workspace  

---

## Action Items

1. Create _docs/ folder structure
2. Move documentation files to organized subfolders
3. Keep only 20 essential files in root
4. Move obsolete files to ARCHIVE
5. Update .gitignore to exclude _docs/generated files (if applicable)
6. Commit cleanup with "docs: Reorganize documentation structure"

---

**Status**: CLEANUP PLAN READY  
**Next Step**: Execute cleanup (move files + commit)
