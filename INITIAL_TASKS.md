# Bump U - Initial Tasks & Quick Start

## PHASE 1: FOUNDATION (Week 1-2) - TEAM 1 FOCUS

### Sprint 1.1: Project Setup (Days 1-2)

#### Managing Engineer Tasks
- [ ] Create Git repository with proper .gitignore for Unity
- [ ] Set up branch protection rules (main requires 2 reviews)
- [ ] Create issue template for bugs/features
- [ ] Initialize project tracking system
- [ ] Schedule daily standups at [TIME]

#### All Teams Tasks
- [ ] Read PROJECT_MANAGEMENT.md and TEAM_SPECIFICATIONS.md
- [ ] Confirm team membership and roles
- [ ] Set up development environment (Unity 2022 LTS)
- [ ] Clone repository and create feature branches

---

### Sprint 1.2: Core Systems - Board (Days 3-5)

#### Team 1 Tasks (Core Infrastructure Agent)

**1. Board System Implementation**
```
Tasks:
- [ ] Create Assets/Scripts/Core/ folder
- [ ] Create GameBoard.cs class
- [ ] Implement 11x7 grid data structure
- [ ] Map board positions to numbers (1-6 pattern from PDF)
- [ ] Create unit tests: GameBoardTests.cs
  - [ ] Test GetChip() at valid positions
  - [ ] Test PlaceChip() and placement validation
  - [ ] Test RemoveChip()
  - [ ] Test GetBoardNumber() for all positions
  - [ ] Test GetEmptyPositions()
  - [ ] Test GetPositionsByNumber(int)
  
Success Criteria:
- All 15 tests pass
- Code review approved
- 100% method coverage
```

**2. Dice System Implementation**
```
Tasks:
- [ ] Create GameDice.cs class
- [ ] Implement Roll() method (1-6)
- [ ] Implement RollBoth() method (tuple)
- [ ] Add roll history tracking
- [ ] Create unit tests: GameDiceTests.cs
  - [ ] Test single roll range
  - [ ] Test both roll range
  - [ ] Test roll distribution (statistically random)
  - [ ] Test deterministic seeding for tests
  
Success Criteria:
- All 10 tests pass
- Roll results logged for debugging
```

**3. Chip System Implementation**
```
Tasks:
- [ ] Create Chip.cs class
- [ ] Define ChipColor enum (Red, Blue)
- [ ] Add chip state properties
- [ ] Create unit tests: ChipTests.cs
  
Success Criteria:
- Chip creation and state changes work
- 5+ unit tests
```

**Code Quality Checklist:**
- [ ] All code has XML documentation comments
- [ ] No compiler warnings
- [ ] Code follows naming conventions (PascalCase)
- [ ] No magic numbers (use constants)
- [ ] All public methods tested

**Code Review Checklist:**
- [ ] Code review requested from managing engineer
- [ ] All comments addressed
- [ ] Approved before merge to develop branch

---

### Sprint 1.3: Game State & Integration (Days 6-10)

#### Team 1 Tasks (Continuation)

**1. Game State Manager**
```
Tasks:
- [ ] Create GameStateManager.cs
- [ ] Define GamePhase enum
- [ ] Define GameResult enum
- [ ] Implement phase transitions
- [ ] Create event system (OnPhaseChanged, OnPlayerChanged)
- [ ] Create unit tests: GameStateManagerTests.cs (30+ tests)
  - [ ] Test turn advancement
  - [ ] Test phase transitions
  - [ ] Test player switching
  - [ ] Test event firing
  
Success Criteria:
- State machine diagram documented
- All transitions tested
- Events fire correctly
```

**2. Player System**
```
Tasks:
- [ ] Create Player.cs class
- [ ] Track player color, chips on board, score
- [ ] Create PlayerTests.cs
  
Success Criteria:
- Player state properly managed
```

**3. Game Manager Facade**
```
Tasks:
- [ ] Create GameManager.cs (singleton)
- [ ] Coordinate Board, Dice, GameState
- [ ] Expose simple API for Games & UI
  - [ ] StartGame(GameType)
  - [ ] RollDice()
  - [ ] PlaceChip(row, col)
  - [ ] BumpChip(row, col)
  - [ ] GetGameStatus()
  
Success Criteria:
- All core systems working together
- Integration tests pass (Board + Dice + State)
```

**4. Serialization System**
```
Tasks:
- [ ] Create save/load system for game state
- [ ] Create SaveDataTests.cs
- [ ] Test serialization roundtrip

Success Criteria:
- Game can be saved and loaded
```

---

### PHASE 1 COMPLETION GATE (End of Week 2)

**Before proceeding to Phase 2:**

- [ ] All core systems unit tests: 100% pass
- [ ] Code coverage: >95%
- [ ] Zero high-priority bugs
- [ ] Code review approval by managing engineer
- [ ] Team standup confirms no blockers
- [ ] Develop branch is stable and deployable

**Demo Checklist:**
```
Show the managing engineer:
1. GameBoard can place/remove chips on all positions
2. Dice rolls 1-6 unpredictably
3. GameStateManager handles turn progression
4. All 40+ unit tests pass
5. No compiler errors/warnings
6. Core systems can be tested independently in editor
```

---

## IMMEDIATE ACTIONS (TODAY)

### For Managing Engineer
1. Review and approve PROJECT_MANAGEMENT.md
2. Create repository with this structure:
   ```
   .gitignore (Unity template)
   README.md
   AGENTS.md
   PROJECT_MANAGEMENT.md
   TEAM_SPECIFICATIONS.md
   INITIAL_TASKS.md (this file)
   Assets/
     Scripts/
       Core/           (Team 1)
       Games/          (Team 2)
       UI/             (Team 3)
       Audio/          (Team 4)
       Tests/          (Team 5)
   ```
3. Create GitHub Issues for each sprint task
4. Schedule daily standup meeting
5. Invite Team Leads

### For All Teams
1. Read all documentation (30 min)
2. Install Unity 2022 LTS (if not present)
3. Clone repository
4. Create feature branch: `feature/{teamname}/{task}`
5. Set up IDE (Visual Studio Code, Visual Studio, or Rider)
6. Attend standup briefing

### For Team 1 (Core Systems)
1. Create Assets/Scripts/Core/ folder structure
2. Start GameBoard.cs implementation
3. Create GameBoardTests.cs (tests first mentality)
4. First pull request ready for review by EOD Day 3

---

## ACCEPTANCE CRITERIA TEMPLATE (Use for all tasks)

```
## Task: [Task Name]

### Requirements
- [ ] Requirement 1
- [ ] Requirement 2
- [ ] Requirement 3

### Testing
- [ ] Unit tests written (XX test cases)
- [ ] All tests passing
- [ ] Code coverage >95%
- [ ] Integration tested with related systems

### Code Quality
- [ ] XML documentation complete
- [ ] No compiler warnings
- [ ] Code review approved
- [ ] Merged to develop branch

### Success Metrics
- Metric 1: [Target]
- Metric 2: [Target]
```

---

## BLOCKING ISSUES TO RESOLVE

**Before Phase 2 can start:**
1. Core systems must be 100% complete and tested
2. No critical bugs in develop branch
3. Team 2 must finalize game rule specifications
4. Managing engineer approval on architecture

---

## COMMUNICATION CADENCE

- **Daily:** 15-min standup (async ok if timezone issues)
- **Every 2 days:** Team 1 check-in with managing engineer
- **Weekly:** All-hands sprint review (Friday EOD)
- **As-needed:** Critical blocker escalations

---

## TRACKING PROGRESS

### Weekly Status Report Template
```
Week 1 Status:
- [ ] Sprints on track
- [ ] Blockers: (none / list)
- [ ] Code coverage: X%
- [ ] Test pass rate: X%
- [ ] Critical bugs: X
- [ ] Next week priorities: (list)
```

---

## REFERENCE DOCUMENTS
- PROJECT_MANAGEMENT.md - Overall plan and timeline
- TEAM_SPECIFICATIONS.md - Detailed API contracts
- AGENTS.md - Game rules and conventions
- INITIAL_TASKS.md - This file
