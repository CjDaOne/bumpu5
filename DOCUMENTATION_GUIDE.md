# Documentation Organization Guide

## 📁 Structure Overview

All project documentation is organized in the `_docs/` folder for easy navigation and maintenance.

### Root-Level Files (Minimal Set)

**Project Overview**
- `README.md` - Project introduction and quick start
- `SPRINT_STATUS.md` - Real-time sprint progress (updated daily)

**Game Reference**
- `GAME_MODES_RULES_SUMMARY.md` - Quick reference for all 5 game modes

**Operational Files** (Active sprints only)
- `TEAM_DISPATCH_SPRINT*.md` - Current sprint team assignments
- `ME_*.md` - Managing engineer operational notes (rotate out after sprint)

### Documentation Folders (`_docs/`)

```
_docs/
├── SPRINTS/                    # Sprint documentation
├── ARCHITECTURE/               # System design & component specs
├── GAME_DESIGN/               # Game mechanics & requirements
├── STANDARDS/                 # Code & communication standards
├── TESTING/                   # Test strategy & plans
├── TEAMS/                     # Team structure & roles
├── UI/                        # UI design specifications
├── PLANNING/                  # Project planning docs
└── INDEX.md                   # Master reference guide
```

## 🔍 How to Find Things

| Question | Location |
|----------|----------|
| What's the current sprint status? | `SPRINT_STATUS.md` (root) |
| How do the game modes work? | `_docs/GAME_DESIGN/GAME_MODES_RULES_SUMMARY.md` |
| What's my sprint assignment? | `TEAM_DISPATCH_SPRINT*.md` (root) |
| System architecture overview? | `_docs/ARCHITECTURE/ARCHITECTURE.md` |
| Board system design? | `_docs/ARCHITECTURE/BOARD_ARCHITECTURE.md` |
| Coding standards? | `_docs/STANDARDS/CODING_STANDARDS.md` |
| Test coverage plan? | `_docs/TESTING/TEST_PLAN_MASTER.md` |
| Complete doc index? | `_docs/INDEX.md` |

## 📋 Maintenance Rules

1. **Root-level files**: Keep only current operational docs
   - Active TEAM_DISPATCH files
   - SPRINT_STATUS.md (updated daily)
   - GAME_MODES_RULES_SUMMARY.md (team reference)
   - README.md (project overview)

2. **Historical files**: Move to `ARCHIVE/` after sprint completion
   - Completed TEAM_DISPATCH files
   - Sprint completion docs
   - Superseded versions

3. **New documentation**: Add to appropriate `_docs/` subfolder
   - Always link from `_docs/INDEX.md`
   - Update this guide if creating new folder

## 🗂️ File Rotation

At sprint completion:
1. Archive old TEAM_DISPATCH files
2. Update SPRINT_STATUS.md with metrics
3. Add sprint summary to `_docs/SPRINTS/`
4. Clean up ME_*.md files if superseded

---

**Last Updated**: Nov 14, 2025  
**Version**: 1.0
