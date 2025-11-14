# Architecture Decision Log

Document all significant technical decisions and rationale here. Updated as project evolves.

---

## Decision #1: Linear Board Adjacency (Sprint 1)

**Date**: Nov 14, 2025  
**Decision Maker**: Managing Engineer  
**Status**: ACCEPTED

### Context
The board adjacency system determines how chips can move and interact on the board.

### Decision
Implement simple linear adjacency where each cell (0-11) connects to the next and previous cell in a circular fashion. Example: Cell 0 → [Cell 1, Cell 11], Cell 5 → [Cell 4, Cell 6].

### Rationale
- Simple implementation for Sprint 1 (reduces complexity)
- Can be updated in Sprint 4 when actual board layout is visualized
- Works for all game modes with minimal logic changes
- Easy to test and verify

### Trade-offs
- May not match visual board layout exactly (visual team can adjust in Sprint 4)
- Assumes circular movement pattern

### Alternative Considered
- 2D grid-based adjacency (more complex, deferred to visual phase)

### Follow-up
In Sprint 4, when board art is created, validate that this adjacency matches the visual layout. If not, update BuildAdjacencyMap() method in BoardModel.

---

## Decision #2: Pure C# Core Logic (Sprint 1)

**Date**: Nov 14, 2025  
**Decision Maker**: Managing Engineer  
**Status**: ACCEPTED

### Context
Deciding whether core game logic should depend on Unity framework classes.

### Decision
All core logic (BoardModel, DiceManager, TurnManager, etc.) uses pure C# with no Unity dependencies. This means no MonoBehaviour, no GameObject, only using UnityEngine.Debug for logging.

### Rationale
- Core logic becomes testable without Unity runtime
- Faster test execution (no Editor overhead)
- Reusable logic across platforms (WebGL, mobile, desktop)
- Clear separation of concerns (logic vs. visualization)

### Trade-offs
- Need a separate visualization layer (will be done in Sprints 4-5)
- Requires more architectural discipline to maintain separation

### Follow-up
In Sprint 5, create visualization classes that wrap core logic and update Unity GameObjects based on game state changes.

---

## Decision #3: Player Score Management (Sprint 1)

**Date**: Nov 14, 2025  
**Decision Maker**: Gameplay Engineer  
**Status**: ACCEPTED

### Context
How to handle player score changes, especially negative scores.

### Decision
- Scores can go negative as an intermediate state
- Final score clamped at 0 (cannot be negative)
- Used for tracking point deductions (penalties, bumps)
- Game mode defines scoring rules (points per action)

### Rationale
- Allows game modes to implement different point mechanics
- Simple validation prevents impossible states
- Matches physical Bump U Box rules

### Follow-up
Each game mode will specify exact point awards in Sprint 3.

---

## Decision #4: Chip Ownership & Activation (Sprint 1)

**Date**: Nov 14, 2025  
**Decision Maker**: Gameplay Engineer  
**Status**: ACCEPTED

### Context
When is a chip considered "active" and eligible for bumping/moving?

### Decision
- A chip is "active" only when on the board (CurrentCell != null)
- Chips start as "inactive" (in reserve)
- Placing a chip on board automatically activates it
- Bumping a chip removes it from board and deactivates it

### Rationale
- Matches physical Bump U Box (chips in play vs. in reserve)
- Simplifies win detection and game logic
- Clear state transitions

### Follow-up
In Sprint 3, game modes will define rules for re-placing bumped chips or new chips.

---

## Decision #5: Test Framework (Sprint 1)

**Date**: Nov 14, 2025  
**Decision Maker**: Managing Engineer  
**Status**: ACCEPTED

### Context
Choosing unit test framework for the project.

### Decision
Use NUnit framework with Unity's built-in test runner (Unity Test Framework).

### Rationale
- Industry standard for C# / Unity projects
- Good documentation and support
- Easy integration into Unity Editor
- CI/CD friendly

### Follow-up
Tests should be run as part of build pipeline. CI/CD setup in Sprint 7.

---

## Decision #6: Game State Snapshots (Sprint 2 Preview)

**Date**: Nov 14, 2025  
**Decision Maker**: Managing Engineer  
**Status**: PROPOSED

### Context
Future need for game state saving, replays, undo functionality.

### Decision
Implement GameState class that can serialize/deserialize the complete game state at any point.

### Rationale
- Enables save/load functionality
- Supports replay/undo for future features
- Allows networked multiplayer (send state over network)
- Useful for testing (set up specific board state)

### Follow-up
Implement in Sprint 2. Prioritize in Sprint 8 if time permits.

---

## Decision #7: Game Mode Interface Pattern (Sprint 3 Preview)

**Date**: Nov 14, 2025  
**Decision Maker**: Managing Engineer  
**Status**: PROPOSED

### Context
How to implement 5 different game modes with different rules.

### Decision
All modes implement IGameMode interface with core methods:
- OnRoll(Player, int[] roll)
- OnPlace(Player, int cellIndex)
- CheckWin(Player)
- GetModeRules() (returns mode-specific rules)

### Rationale
- Polymorphic behavior allows GameManager to work with any mode
- Easy to add new modes later
- Clear contract for what each mode must implement
- Allows mode-specific UI if needed

### Follow-up
Detailed interface definition will be in Sprint 3 kickoff document.

---

## Decision #8: No Networking in MVP (Sprint 1)

**Date**: Nov 14, 2025  
**Decision Maker**: Managing Engineer  
**Status**: ACCEPTED

### Context
Should the MVP support online multiplayer or local-only play?

### Decision
MVP supports local-only (2 players on same device). Networking deferred to post-release.

### Rationale
- Reduces complexity for initial release
- Can add networking later without major refactoring (thanks to GameState serialization)
- Focus on core game quality first
- Simplifies testing and QA

### Trade-off
- No cross-platform multiplayer in initial release

### Follow-up
If successful, multiplayer can be added in v2.

---

**Last Updated**: Nov 14, 2025  
**Total Decisions**: 8 (2 Accepted, 2 Proposed)
