# Bump U Box - Multi-Platform Game Project

A digital adaptation of the classic Bump U Box game for Android, iOS, and Web Browser (WebGL).

---

## 📋 Project Overview

**Goal**: Create a polished, multi-platform version of Bump U Box using Unity.

**Platforms**: 
- 📱 Android (Google Play)
- 🍎 iOS (App Store)
- 🌐 Web Browser (WebGL)

**Tech Stack**: 
- Unity 2022 LTS
- C# with VS Code
- Pure C# core logic (testable, no Unity dependencies)

---

## 📚 Documentation

Start here for quick orientation:

1. **ENGINEERING_MANAGER.md** - Managing agent role & responsibilities
2. **ARCHITECTURE.md** - Complete technical architecture & systems
3. **PROJECT_PLAN.md** - 8-week sprint schedule
4. **SUBAGENT_TEAMS.md** - Team structure & responsibilities
5. **CODING_STANDARDS.md** - C# style guide, naming, documentation
6. **SPRINT_1_KICKOFF.md** - Sprint 1 detailed breakdown
7. **DECISION_LOG.md** - Technical decisions & rationale
8. **SPRINT_STATUS.md** - Real-time progress tracking

---

## 🏗️ Project Structure

```
/Assets
  /Scripts              # All game code
    /Core              # Game logic (pure C#, no Unity deps)
    /Board             # Board interaction & visualization
    /GameModes         # 5 game mode implementations
    /UI                # HUD, menus, popups
    /Managers          # GameManager, scene management
    /Platform          # Input handlers, device optimization
    /Tests             # Unit tests (NUnit)
  /Prefabs             # Reusable UI/game components
  /Scenes              # Unity scenes
  /Art                 # Board, chips, UI art
  /Audio               # Sound effects, music
  /Resources           # Data files, configs

/ARCHITECTURE.md       # System design & layout
/PROJECT_PLAN.md       # Sprint breakdown
/SUBAGENT_TEAMS.md     # Team assignments
/CODING_STANDARDS.md   # Style guide
/DECISION_LOG.md       # Technical decisions
/SPRINT_STATUS.md      # Progress tracking
```

---

## 🎮 Game Systems

### Core Gameplay
- **Dice Manager**: Handles 1-die and 2-die rolls with edge cases
- **Turn Manager**: Player rotation and turn flow
- **Board Model**: 12-cell grid, chip placement, 5-in-a-row detection
- **Game State Machine**: Full game state tracking

### Game Modes (5 Total)
1. **Bump 5** - Classic: First to 5 in a row wins
2. **Krazy 6** - Double 6 with special rules
3. **Pass the Chip** - Shared chip mechanic
4. **Bump U & 5** - Combined rules variant
5. **Solitary** - Single-player challenge mode

### UI/UX
- Main menu & mode selection
- HUD (dice, buttons, scoreboard)
- Game instructions & rules
- Settings (volume, etc.)
- Popup prompts (penalties, chip passing)

### Platform Support
- **Mobile**: Touch input, safe areas (notch), optimized FPS
- **Web**: Mouse & touch, canvas scaling, IL2CPP
- **Desktop**: Keyboard & mouse support (optional)

---

## 📅 Sprint Schedule

| Sprint | Focus | Duration | Owner |
|--------|-------|----------|-------|
| 1 | Core Logic (pure C#) | Week 1 | Gameplay Engineer |
| 2 | Game State Machine | Week 2 | Gameplay Engineer |
| 3 | Game Modes (5 modes) | Week 3 | Gameplay Engineer |
| 4 | Board Visualization | Week 4 | Board Engineer |
| 5 | UI & HUD | Week 5 | UI Engineer |
| 6 | Menu System & Integration | Week 6 | UI Engineer |
| 7 | Multi-Platform Builds | Week 7 | Build Engineer |
| 8 | QA & Playtesting | Week 8 | QA Lead |

**Total Duration**: 8 weeks to release-ready

---

## 🚀 Getting Started

### For Developers

1. **Read ARCHITECTURE.md** for system overview
2. **Read CODING_STANDARDS.md** for style guide
3. **Claim a sprint task** from SUBAGENT_TEAMS.md
4. **Read relevant sprint kickoff** (e.g., SPRINT_1_KICKOFF.md)
5. **Check SPRINT_STATUS.md** for current progress

### For QA

1. **Read PROJECT_PLAN.md** for timeline
2. **Read SPRINT_STATUS.md** for current milestone
3. **Wait for Sprint 5** to begin testing
4. **Use SUBAGENT_TEAMS.md** to understand QA role

### For Project Managers

1. **Check SPRINT_STATUS.md** for real-time progress
2. **Review DECISION_LOG.md** for architectural choices
3. **Read PROJECT_PLAN.md** for high-level timeline
4. **Monitor SUBAGENT_TEAMS.md** for team blockers

---

## 📝 Code Quality Standards

✅ **Naming**: PascalCase classes, camelCase variables  
✅ **Documentation**: 100% of public methods documented  
✅ **Testing**: 80%+ unit test coverage target  
✅ **Performance**: 60 FPS modern, 30 FPS minimum  
✅ **Code Review**: All PRs require managing engineer sign-off  

See **CODING_STANDARDS.md** for full details.

---

## 🧪 Testing

### Unit Tests
- Run in Unity Test Framework
- 57+ test cases in Sprint 1 alone
- Target coverage: 80%+

### Manual QA
- Playtest all 5 modes on all platforms
- Test edge cases (illegal bumps, win declarations, etc.)
- Device testing (multiple phones, browsers)

See **PROJECT_PLAN.md** for detailed QA schedule (Sprint 8).

---

## 📱 Platform-Specific

### WebGL
- IL2CPP backend for performance
- ASTC/ETC2 texture compression
- 512-1024 MB memory allocation
- Mouse + touch support

### Android
- Google Play Console submission
- Safe areas (no hardcoded unsafe zones)
- FPS limiter (30 FPS for battery)
- Portrait orientation lock

### iOS
- App Store Connect submission
- iPhone notch safe area support
- FPS limiter (30 FPS for battery)
- Portrait orientation lock

---

## 🔧 Development Workflow

### Creating a Feature
1. Create branch: `feature/system-name`
2. Implement following **CODING_STANDARDS.md**
3. Write unit tests (80%+ coverage)
4. Submit PR with description
5. Managing engineer reviews & approves
6. Merge to `develop` branch

### Bug Fixes
1. Create branch: `bugfix/issue-name`
2. Fix and test thoroughly
3. Submit PR with bug description & fix
4. Merge after review

### Hotfixes (Production)
1. Create branch: `hotfix/issue-name`
2. Fix critical issues only
3. Fast-track review & merge

---

## 📊 Key Metrics

| Metric | Current | Target |
|--------|---------|--------|
| Sprints Complete | 0 / 8 | 8 / 8 |
| Code Coverage | N/A | 80%+ |
| Platforms Ready | 0 / 3 | 3 / 3 |
| Game Modes | 0 / 5 | 5 / 5 |
| Documentation | Complete | Complete |
| Zero Critical Bugs | N/A | Release Target |

---

## 🎯 Success Criteria

Project is complete when:
- ✅ All 5 game modes fully playable
- ✅ Zero critical bugs on all platforms
- ✅ 60 FPS on modern devices, 30 FPS floor
- ✅ All platforms approved (Play Store, App Store)
- ✅ 80%+ unit test coverage
- ✅ Complete documentation

---

## 📞 Communication

- **Daily Standups**: TBD (set with team)
- **Weekly Sprint Reviews**: Fridays
- **Slack**:
  - `#gameplay` - Core logic
  - `#ui` - UI/UX updates
  - `#build` - Build & platform
  - `#qa` - Testing updates
  - `#blockers` - Cross-team issues
  - `#general` - Announcements

---

## 📄 License

(To be defined)

---

## 👥 Team

- **Managing Engineer**: You (this agent)
- **Gameplay Engineer**: Agent (assigned Sprint 1+)
- **UI Engineer**: Agent (assigned Sprint 5+)
- **Board Engineer**: Agent (assigned Sprint 4+)
- **Build Engineer**: Agent (assigned Sprint 7+)
- **QA Lead**: Agent (assigned Sprint 8+)

---

## 🔗 Quick Links

- **Architecture**: [ARCHITECTURE.md](ARCHITECTURE.md)
- **Sprint Plan**: [PROJECT_PLAN.md](PROJECT_PLAN.md)
- **Teams**: [SUBAGENT_TEAMS.md](SUBAGENT_TEAMS.md)
- **Coding Standards**: [CODING_STANDARDS.md](CODING_STANDARDS.md)
- **Current Status**: [SPRINT_STATUS.md](SPRINT_STATUS.md)
- **Decisions**: [DECISION_LOG.md](DECISION_LOG.md)

---

**Project Start Date**: Nov 14, 2025  
**Target Release Date**: Early 2026  
**Status**: 🟢 Ready for Sprint 1 Kickoff
