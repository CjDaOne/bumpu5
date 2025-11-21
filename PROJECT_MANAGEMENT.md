# Bump U - Project Management & Team Structure

## Project Status: PRE-PRODUCTION → PRODUCTION-READY

**Project Manager:** Managing Engineer  
**Timeline:** 4 Phases (8-12 weeks estimated)  
**Target Release:** Production-ready build with all 5 games fully playable

---

## TEAMS & RESPONSIBILITIES

### Team 1: Core Systems (Lead: Core Infrastructure Agent)
**Deliverables:** Board, Dice, Chip logic, Game state management
- Implement 11x7 board data structure with number positions
- Create Dice system (d6, special rules for #5/#6 per game)
- Implement Chip placement, removal, collision detection
- Build GameState manager (player turns, win conditions, scoring)
- Create serialization for game saves

**Success Metrics:** 100% unit test coverage, all core systems work independently

---

### Team 2: Game Logic (Lead: Game Rules Agent)
**Deliverables:** All 5 game implementations with rule validation
- Game #1: 5-in-a-row detection (horizontal, vertical, diagonal)
- Game #2: 6-chip placement, #6 roll mechanics
- Game #3: Center #5 boxing, point calculation, "PASS THE CHIP" logic
- Game #4: Dual dice, #5 as opponent, bonus roll system
- Game #5: Solitary mode, alternating colors, fill detection
- Bumping system: Validation, phrase requirements ("BUMP U")

**Success Metrics:** All games playable with rule-complete implementations

---

### Team 3: UI/UX (Lead: Interface Agent)
**Deliverables:** Game selection, HUD, score display, win screens
- Game selection menu (all 5 games + difficulty/players)
- In-game HUD: Current player, dice result, board state
- Score tracking display (per-game specific scoring)
- Win/loss/draw screens with rule confirmations
- Mobile-responsive UI (if targeting mobile)
- Accessibility features (colorblind mode, text-to-speech for rules)

**Success Metrics:** All screens functional, <100ms input response

---

### Team 4: Audio/Polish (Lead: Polish Agent)
**Deliverables:** Audio, animations, visual feedback
- Dice roll sounds and animations
- Chip placement/bumping SFX and particle effects
- Background music (per-game themes)
- UI button interactions
- Camera work and visual transitions
- Screen shake on bumps, animations on wins

**Success Metrics:** All interactions have audio/visual feedback

---

### Team 5: Testing & QA (Lead: QA Agent)
**Deliverables:** Test coverage, bug tracking, release readiness
- Unit tests for all systems (Core, Games, Rules)
- Integration tests (multi-game scenarios)
- Rule compliance verification (all 5 games)
- Performance profiling (target: 60 FPS)
- Edge case testing (wrong bumps, tied games, invalid moves)
- Regression testing before each build

**Success Metrics:** 0 critical bugs, 95%+ test pass rate, documented known issues

---

## TECHNICAL STACK & VERSIONS

| Component | Version | Status |
|-----------|---------|--------|
| Unity Editor | 2022 LTS (2022.3.20 latest) | ✓ Required |
| C# | 9.0+ | ✓ Via Unity |
| .NET | 4.7.1 (Mono) / IL2CPP | ✓ Unity managed |
| Test Framework | UTF 1.4.3+ | ✓ Install via Package Manager |
| Input System | 1.7.0+ | ✓ Recommended for mobile |
| TextMesh Pro | Latest | ✓ Built-in |
| Physics 2D | Built-in | ✓ For collision checks |

---

## PROJECT PHASES

### Phase 1: Foundation (Weeks 1-2)
**Team 1 Focus**
- [ ] Core board system (11x7 grid, number mapping)
- [ ] Dice system (6-sided die, roll mechanics)
- [ ] Chip class (position, color, state)
- [ ] Unit tests for above
- [ ] Project structure created (Assets/Scripts/{Core,Games,UI,Audio})

**Success Gate:** Core systems tested and working in isolation

---

### Phase 2: Game Implementation (Weeks 3-5)
**Teams 1 & 2 Focus**
- [ ] Implement all 5 games using core systems
- [ ] Bumping logic with phrase validation
- [ ] Win condition detection per game
- [ ] Score calculation systems
- [ ] Integration tests between systems

**Success Gate:** All games playable in editor with debug UI

---

### Phase 3: UI & Polish (Weeks 6-8)
**Teams 3, 4, & 1 Focus**
- [ ] Main menu and game selection
- [ ] In-game HUD for all games
- [ ] Score displays and win screens
- [ ] Audio/SFX implementation
- [ ] Visual feedback (animations, particles)
- [ ] Mobile input handling if applicable

**Success Gate:** Game fully playable with all UI/audio

---

### Phase 4: Testing & Release (Weeks 9-12)
**Team 5 Focus + All Teams**
- [ ] Full QA regression testing
- [ ] Performance optimization (target 60 FPS)
- [ ] Bug fixes and edge case handling
- [ ] Build optimization (size, memory)
- [ ] Documentation and user guides
- [ ] Final production build

**Success Gate:** Zero critical bugs, all tests pass, release notes signed off

---

## CODE STANDARDS

### File Organization
```
Assets/
├── Scripts/
│   ├── Core/              (Board, Dice, Chip, GameState)
│   ├── Games/             (Game1-5 implementations)
│   ├── UI/                (Menus, HUD, Screens)
│   ├── Audio/             (SFX, Music managers)
│   └── Utilities/         (Helpers, extensions)
├── Scenes/                (Per-game scenes + menu)
├── Prefabs/               (Reusable components)
├── Audio/
├── Sprites/
└── Resources/
```

### Code Style Requirements
- **C# Version:** 9.0 (modern syntax, null coalescing)
- **Naming:** PascalCase classes/methods, camelCase fields
- **Access Modifiers:** Private by default, public only when necessary
- **Comments:** XML docs on public APIs, explain game rules inline
- **Error Handling:** TryParse for input, null checks for Unity objects
- **Tests:** All public methods tested, games tested with all rule scenarios

### Git Workflow
- Main branch: Production builds only (protected, requires review)
- Develop: Integration branch (all teams merge here)
- Feature branches: `feature/{team}/{feature-name}`
- Pull requests required, code review by 2+ engineers

---

## TRACKING & COMMUNICATION

### Task Management
- Daily standup: 15 min sync per team (or async updates to shared doc)
- Weekly sprint review: All teams present progress, blockers
- Backlog: Maintained in project tracking system
- Success metrics updated daily

### Status Dashboard
```
Phase: 1/4 (Foundation)
Core Systems:    ████████░░ 80% (Board & Dice done, Chips in-progress)
Game Logic:      ░░░░░░░░░░ 0% (Waiting for Phase 2)
UI/UX:           ░░░░░░░░░░ 0% (Waiting for Phase 2)
Audio/Polish:    ░░░░░░░░░░ 0% (Waiting for Phase 3)
QA/Testing:      ██░░░░░░░░ 10% (Framework setup)
```

### Risk Register
| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|-----------|
| Rule ambiguity across games | High | High | Weekly rules review with all teams |
| Performance on mobile | Medium | High | Early profiling, optimization pass |
| Audio licensing | Low | Medium | Use royalty-free assets only |
| Multiplayer desync | Medium | High | Implement turn-by-turn validation |

---

## SUCCESS CRITERIA (GO/NO-GO)

### Phase 1 Gate
- [ ] All core systems have >90% test coverage
- [ ] Zero high-priority bugs
- [ ] Code review approved by managing engineer

### Phase 2 Gate
- [ ] All 5 games fully playable and rule-compliant
- [ ] Integration tests pass (100%)
- [ ] Bumping system works with phrase validation

### Phase 3 Gate
- [ ] All UI responsive and bug-free
- [ ] Audio plays without errors
- [ ] FPS stable at 60+ on target hardware

### Phase 4 Gate (PRODUCTION RELEASE)
- [ ] Zero critical bugs reported
- [ ] 100% of rules verified working
- [ ] Performance: 60 FPS sustained
- [ ] All accessibility features tested
- [ ] Build size optimized
- [ ] Release notes and user guide complete

---

## NEXT STEPS (IMMEDIATE)

**Today:**
1. Create Git repository with this project structure
2. Initialize Unity 2022 LTS project
3. Assign Team Leads to each area
4. Create backlog issues for Phase 1

**This Week:**
1. Team 1 begins core systems (Board, Dice)
2. Teams 2-5 prepare detailed specifications
3. First code review passes
4. Daily standups begin

---

## COMMUNICATION CHANNELS

- **Critical Issues:** Direct escalation to managing engineer
- **Daily Updates:** Team-specific Slack/Discord channels
- **Weekly Reviews:** All-hands meeting, recorded
- **Code Reviews:** GitHub/Git platform
- **Documentation:** Shared wiki/Confluence
