# Bump U Box - Multi-Platform Game Project

A digital adaptation of the classic Bump U Box game for Android, iOS, and Web Browser (WebGL).

---

## ⚡ Quick Start (2 minutes)

**For a new team member?** Start here:

1. Read this Quick Start section (you are here - 2 min)
2. Check **Code Structure Reference** below (code layout)
3. Read **CODING_STANDARDS.md** (naming & testing rules)
4. Find your sprint in **SUBAGENT_TEAMS.md**
5. Read the sprint kickoff doc (e.g., SPRINT_2_COMPREHENSIVE.md)

### Current Status
- **Sprint**: 1 / 8 (Core Logic) ✅ Complete
- **Classes Created**: 7 (all approved)
- **Tests Created**: 57 (all passing)
- **Code Coverage**: 85%+ (exceeds target)
- **Blockers**: None
- **Next Sprint**: Sprint 2 kicks off Nov 21, 2025

### Tech Stack
- **Engine**: Unity 2022 LTS
- **Language**: C#
- **Platforms**: WebGL, Android, iOS
- **Testing**: NUnit (57 tests)

### Five Game Modes
| Mode | Goal | Special Rule |
|------|------|--------------|
| Bump 5 | 5 in a row | Classic rules |
| Krazy 6 | 5 in a row | Double 6 quirk |
| Pass the Chip | Pass chip | Shared ownership |
| Bump U & 5 | 5 + rules | Combined mechanics |
| Solitary | 5 in a row | Single player |

---

## 📚 Full Overview (10 minutes)

### 📋 Project Overview

**Goal**: Create a polished, multi-platform version of Bump U Box using Unity.

**Platforms**: 
- 📱 Android (Google Play)
- 🍎 iOS (App Store)
- 🌐 Web Browser (WebGL)

**Core Principles**:
- Modular game modes with shared interface
- Clear separation: UI, game state, board logic
- Test-driven logic for all game rules
- Data-driven board configuration
- Touch-friendly, responsive UI

---

### 🏗️ Project Structure

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

### 🎮 Game Systems

#### Core Gameplay
- **Dice Manager**: Handles 1-die and 2-die rolls with edge cases
- **Turn Manager**: Player rotation and turn flow
- **Board Model**: 12-cell grid, chip placement, 5-in-a-row detection
- **Game State Machine**: Full game state tracking

#### Board System
- **Grid Manager**: 12-cell grid with circular layout
- **Cell Metadata**: Ownership, piece placement
- **Interaction Layer**: Tap/drag detection

#### UI/UX
- Main menu & mode selection
- HUD (dice, buttons, scoreboard)
- Game instructions & rules
- Settings (volume, etc.)
- Popup prompts (penalties, chip passing)

#### Platform Support
- **Mobile**: Touch input, safe areas (notch), optimized FPS
- **Web**: Mouse & touch, canvas scaling, IL2CPP
- **Desktop**: Keyboard & mouse support (optional)

---

### 📅 Sprint Schedule

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

**Total Duration**: 8 weeks to release-ready (Nov 14 - Jan 9, 2026)

---

### 🚀 Getting Started by Role

#### For Developers
1. **Read ARCHITECTURE.md** for system overview
2. **Read CODING_STANDARDS.md** for style guide
3. **Claim a sprint task** from SUBAGENT_TEAMS.md
4. **Read relevant sprint kickoff** (e.g., SPRINT_2_COMPREHENSIVE.md)
5. **Check SPRINT_STATUS.md** for current progress

#### For QA
1. **Read PROJECT_PLAN.md** for timeline
2. **Read SPRINT_STATUS.md** for current milestone
3. **Wait for Sprint 5** to begin testing
4. **Use SUBAGENT_TEAMS.md** to understand QA role

#### For Project Managers
1. **Check SPRINT_STATUS.md** for real-time progress
2. **Review DECISION_LOG.md** for architectural choices
3. **Read PROJECT_PLAN.md** for high-level timeline
4. **Monitor SUBAGENT_TEAMS.md** for team blockers

---

## 💻 Code Structure Reference

### Core Logic (Pure C#, No Unity Dependencies)
```
/Assets/Scripts/Core/
  Player.cs           → Player state (score, chips)
  Chip.cs             → Individual game piece
  BoardCell.cs        → Single cell on 12-cell board
  BoardModel.cs       → Full board logic (moves, bumps, 5-in-a-row)
  DiceManager.cs      → Dice rolls & edge cases
  TurnManager.cs      → Player rotation
```

### Game Modes (Sprint 3+)
```
/Assets/Scripts/GameModes/
  IGameMode.cs        → Interface all modes implement
  Game1_Bump5.cs      → Mode 1
  Game2_Krazy6.cs     → Mode 2
  Game3_PassTheChip.cs → Mode 3
  Game4_BumpUAnd5.cs  → Mode 4
  Game5_Solitary.cs   → Mode 5
```

### UI (Sprint 5+)
```
/Assets/Scripts/UI/
  HUDManager.cs       → Dice button, BUMP button, scoreboard
  PopupManager.cs     → PENALTY, PASS THE CHIP, TAKE IT OFF
  MenuManager.cs      → Main menu, mode select
```

### Tests
```
/Assets/Scripts/Tests/
  PlayerTests.cs          → 11 tests
  BoardModelTests.cs      → 20 tests
  DiceTests.cs            → 13 tests
  TurnManagerTests.cs     → 13 tests
  GameModeTests.cs        → (Sprint 3+)
```

---

## 🎯 Coding Rules (Quick Reference)

### Naming Conventions
- **Classes**: `PascalCase` → `BoardModel`, `TurnManager`
- **Methods**: `PascalCase` → `RollDice()`, `CheckWin()`
- **Variables**: `camelCase` → `currentPlayer`, `isActive`
- **Constants**: `UPPER_SNAKE_CASE` → `BOARD_SIZE`, `WINNING_ROW`
- **Interfaces**: `IPascalCase` → `IGameMode`, `IBoardCell`

### Documentation Template
```csharp
/// <summary>
/// What this method does in one sentence.
/// </summary>
/// <param name="paramName">What this parameter is</param>
/// <returns>What is returned</returns>
public void MyMethod(int paramName) { }
```

### Testing Pattern
- Filename: `ClassNameTests.cs`
- Method naming: `MethodName_Condition_ExpectedResult()`
- Example: `RollDice_Always_Returns1To6()`

### No Magic Numbers
```csharp
// Bad
if (cells.Length == 12) { }

// Good
private const int BOARD_SIZE = 12;
if (cells.Length == BOARD_SIZE) { }
```

---

### 🚨 Common Mistakes (Avoid!)

❌ **Magic Numbers** → Use `const int BOARD_SIZE = 12`  
❌ **No Comments** → Document public methods  
❌ **God Objects** → Keep classes focused (single responsibility)  
❌ **Null Checks Missing** → Always validate inputs  
❌ **Tight Coupling** → Use interfaces & dependency injection  
❌ **No Tests** → All logic must be testable

See **CODING_STANDARDS.md** for comprehensive guidelines.

---

## 🧪 Testing

### Running Unit Tests
In Unity Editor:
1. Window → Testing → Test Runner
2. Click "Run All" under EditMode tests
3. All tests should show ✅ Green
4. Verify 57+ tests pass (57/57 expected)

### Git Workflow
```bash
# Create feature branch
git checkout -b feature/board-grid-manager

# Commit with good messages
git commit -m "[Sprint 4] Implement BoardGridManager

- Add 12-cell grid layout
- Create clickable Cell prefab
- Implement valid move highlighting"

# Push and create PR
git push origin feature/board-grid-manager
```

### Branch Naming
- `feature/system-name` → New features
- `bugfix/issue-name` → Bug fixes
- `hotfix/critical-issue` → Production critical fixes

---

## 📋 Core Game Concepts

### The Board
- 12 cells numbered 0-11
- Cells form a circular layout
- Each player owns some cells
- 5 in a row = WIN

### The Chips
- Each player has multiple chips (exact count per mode)
- Active = on the board
- Inactive = in reserve
- Bumping removes a chip from the board

### Turn Flow
- Player rolls dice
- Moves chip to adjacent cell
- May bump opponent's chip
- Score updates
- Turn passes to opponent

### Win Condition
- 5 of your chips in adjacent cells = WIN
- Specific to each game mode

---

## ❓ Frequently Asked Questions

**Q: Where's the game visual?**  
A: Sprint 4. Core logic first (testable), then visuals.

**Q: Can I play it yet?**  
A: No. It's all code logic. Playable in Sprint 6.

**Q: What if I find a bug?**  
A: Create issue with: Reproduction steps, Expected vs. Actual, Severity.

**Q: What's the timeline?**  
A: 8 weeks total. See PROJECT_PLAN.md.

**Q: Can I change architecture?**  
A: Document it in DECISION_LOG.md and get managing engineer approval.

**Q: What's the code quality bar?**  
A: 80%+ test coverage, no critical bugs, full documentation.

---

## 📝 Code Quality Standards

✅ **Naming**: PascalCase classes, camelCase variables  
✅ **Documentation**: 100% of public methods documented  
✅ **Testing**: 80%+ unit test coverage target  
✅ **Performance**: 60 FPS modern, 30 FPS minimum  
✅ **Code Review**: All PRs require managing engineer sign-off  

See **CODING_STANDARDS.md** for full details.

---

## 📊 Key Metrics

| Metric | Current | Target |
|--------|---------|--------|
| Sprints Complete | 1 / 8 | 8 / 8 |
| Code Coverage | 85%+ | 80%+ |
| Platforms Ready | 0 / 3 | 3 / 3 |
| Game Modes | 0 / 5 | 5 / 5 |
| Documentation | Complete | Complete |
| Zero Critical Bugs | 0 | 0 |

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

## 📱 Platform-Specific Details

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

## 📞 Communication

- **Daily Standups**: 15 min (blockers focus)
- **Weekly Sprint Reviews**: Fridays
- **Slack Channels**:
  - `#gameplay` - Core logic discussions
  - `#ui` - UI/UX updates
  - `#build` - Build & platform issues
  - `#qa` - Testing updates
  - `#blockers` - Cross-team impediments
  - `#general` - Announcements

---

## 👥 Team Structure

- **Managing Engineer**: Amp (overall coordination)
- **Gameplay Engineer**: Agent (Sprints 1-3, core logic + modes)
- **Board Engineer**: Agent (Sprint 4, visualization)
- **UI Engineer**: Agent (Sprint 5-6, interface)
- **Build Engineer**: Agent (Sprint 7, multi-platform)
- **QA Lead**: Agent (Sprint 8, testing)

Full details: **SUBAGENT_TEAMS.md**

---

## 🔗 Quick Links

| Link | Purpose |
|------|---------|
| **ARCHITECTURE.md** | Complete technical design |
| **CODING_STANDARDS.md** | C# style guide |
| **PROJECT_PLAN.md** | 8-week timeline |
| **SUBAGENT_TEAMS.md** | Team structure |
| **SPRINT_STATUS.md** | Real-time progress |
| **DECISION_LOG.md** | Architectural decisions |
| **DOCUMENTATION_INDEX.md** | Navigation guide |

---

## 📄 License

(To be defined)

---

**Project Start Date**: Nov 14, 2025  
**Target Release Date**: January 9, 2026  
**Status**: 🟢 Sprint 1 Complete, Sprint 2 Ready for Nov 21 Kickoff
