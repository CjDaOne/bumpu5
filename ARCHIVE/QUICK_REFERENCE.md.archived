# Quick Reference Card

Fast lookup for common tasks, standards, and decisions.

---

## 🚀 I'm a New Team Member. Where Do I Start?

1. Read **README.md** (2 min overview)
2. Read **ARCHITECTURE.md** (system design)
3. Read **CODING_STANDARDS.md** (how to code)
4. Find your sprint in **SUBAGENT_TEAMS.md**
5. Read the sprint kickoff doc (e.g., SPRINT_1_KICKOFF.md)

---

## 📖 Document Map

| Document | Purpose | Audience |
|----------|---------|----------|
| README.md | Project overview & quick start | Everyone |
| ARCHITECTURE.md | System design & technical vision | Developers |
| PROJECT_PLAN.md | 8-week sprint schedule | PMs, leads |
| SUBAGENT_TEAMS.md | Team assignments & roles | All engineers |
| CODING_STANDARDS.md | C# style guide & best practices | All developers |
| SPRINT_1_KICKOFF.md | Sprint 1 detailed breakdown | Gameplay engineer |
| DECISION_LOG.md | Technical decisions & rationale | Architects, leads |
| SPRINT_STATUS.md | Real-time progress tracking | All (for daily standup) |
| ENGINEERING_MANAGER.md | Managing agent role | Amp AI agents |

---

## 💻 Code Structure (Quick Lookup)

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

### Game Modes (Sprint 3)
```
/Assets/Scripts/GameModes/
  IGameMode.cs        → Interface all modes implement
  Game1_Bump5.cs      → Mode 1
  Game2_Krazy6.cs     → Mode 2
  Game3_PassTheChip.cs → Mode 3
  Game4_BumpUAnd5.cs  → Mode 4
  Game5_Solitary.cs   → Mode 5
```

### UI (Sprint 5)
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
  ChipTests.cs            → (Sprint 2)
  BoardModelTests.cs      → 20 tests
  DiceTests.cs            → 13 tests
  TurnManagerTests.cs     → 13 tests
  GameModeTests.cs        → (Sprint 3)
```

---

## 🎯 Coding Rules (Summarized)

### Naming
- **Classes**: `PascalCase` → `BoardModel`, `TurnManager`
- **Methods**: `PascalCase` → `RollDice()`, `CheckWin()`
- **Variables**: `camelCase` → `currentPlayer`, `isActive`
- **Constants**: `UPPER_SNAKE_CASE` → `BOARD_SIZE`, `WINNING_ROW`
- **Interfaces**: `IPascalCase` → `IGameMode`, `IBoardCell`

### Documentation
```csharp
/// <summary>
/// What this method does in one sentence.
/// </summary>
/// <param name="paramName">What this parameter is</param>
/// <returns>What is returned</returns>
public void MyMethod(int paramName) { }
```

### Testing
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

## 🧪 Running Tests

In Unity Editor:
1. Window → Testing → Test Runner
2. Click "Run All" under EditMode tests
3. All tests should show ✅ Green

---

## 🔄 Git Workflow

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

## 🚨 Common Mistakes (Avoid!)

❌ **Magic Numbers** → Use `const int BOARD_SIZE = 12`  
❌ **No Comments** → Document public methods  
❌ **God Objects** → Keep classes focused (single responsibility)  
❌ **Null Checks Missing** → Always validate inputs  
❌ **Tight Coupling** → Use interfaces & dependency injection  
❌ **No Tests** → All logic must be testable  

---

## 🎮 Game Mode Rules (Quick Ref)

Each mode has different scoring & winning rules:

| Mode | Goal | Special Rule | Sprint |
|------|------|--------------|--------|
| Bump 5 | 5 in a row | Classic rules | 3 |
| Krazy 6 | 5 in a row | Double 6 quirk | 3 |
| Pass the Chip | Pass chip | Shared ownership | 3 |
| Bump U & 5 | 5 + rules | Combined mechanics | 3 |
| Solitary | 5 in a row | Single player | 3 |

Full details in Sprint 3 Kickoff.

---

## 📊 Current Status

**Sprint**: 1 / 8 (Core Logic)  
**Classes Created**: 7  
**Tests Created**: 57  
**Code Coverage**: 85%+ (estimated)  
**Blockers**: None  

See **SPRINT_STATUS.md** for detailed progress.

---

## ❓ FAQ

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

## 🔗 Key Files

```
README.md               ← Start here
ARCHITECTURE.md        ← System design
CODING_STANDARDS.md    ← How to code
SPRINT_1_KICKOFF.md    ← Current sprint details
SPRINT_STATUS.md       ← Real-time progress
DECISION_LOG.md        ← Why we made choices
```

---

## 📞 Getting Help

1. **Code question?** → Check CODING_STANDARDS.md
2. **Architecture question?** → Check ARCHITECTURE.md
3. **Why was X decided?** → Check DECISION_LOG.md
4. **What's my task?** → Check SUBAGENT_TEAMS.md + Sprint Kickoff
5. **What's the status?** → Check SPRINT_STATUS.md
6. **Still stuck?** → Post in #blockers on Slack

---

**Last Updated**: Nov 14, 2025  
**For**: All team members
