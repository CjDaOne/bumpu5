# Bump U Box - Sprint & Milestone Plan

## Overview

**Total Duration**: ~8 weeks (8 sprints × 1 week each)  
**Teams**: Gameplay Engineers, UI Engineers, Build Engineer, QA  
**Delivery**: Android, iOS, WebGL

---

## Sprint Schedule

### **Sprint 1: Core Game Logic Foundation** (Week 1)
**Goal**: Build testable, non-visual game logic  
**Deliverables**:
- BoardCell, Chip, Player classes (pure C#)
- Board adjacency detection & 5-in-a-row logic
- Dice roll system (1-die, 2-die, all edge cases)
- Unit tests for all logic (80%+ coverage)

**Owner**: Gameplay Engineer  
**Success Criteria**: Game is 100% playable in code (no UI needed yet)

---

### **Sprint 2: Turn Manager & Game State** (Week 2)
**Goal**: Implement turn flow and game state machine  
**Deliverables**:
- Turn Manager (player rotation, turn phases)
- Game State Machine (setup → rolling → placing → bumping → end-turn)
- Dice edge case handling (6 = lose turn, 5+6 = safe, etc.)
- Integration tests for turn flow

**Owner**: Gameplay Engineer  
**Success Criteria**: Turn flow tested end-to-end

---

### **Sprint 3: Game Modes Architecture** (Week 3)
**Goal**: Define IGameMode interface and implement all 5 modes  
**Deliverables**:
- IGameMode interface
- Game1_Bump5.cs (complete implementation)
- Game2_Krazy6.cs (complete implementation)
- Game3_PassTheChip.cs (complete implementation)
- Game4_BumpUAnd5.cs (complete implementation)
- Game5_Solitary.cs (complete implementation)
- Mode-specific rule tests

**Owner**: Gameplay Engineer  
**Success Criteria**: All 5 modes are fully logic-complete and testable

---

### **Sprint 4: Board System Integration** (Week 4)
**Goal**: Build the interactive board in Unity  
**Deliverables**:
- Board Grid Manager (12-cell layout)
- Cell prefabs (clickable, highlightable)
- Chip placement & visual representation
- Valid move highlighting
- Tap/click detection → move execution
- Board scene with proper hierarchy

**Owner**: Board Engineer / Gameplay Engineer  
**Success Criteria**: Board is fully interactive; moves execute correctly

---

### **Sprint 5: UI Framework & HUD** (Week 5)
**Goal**: Build complete UI layer  
**Deliverables**:
- Dice Roll Button + animation
- BUMP Button (context-sensitive)
- Declare Win Button
- Scoreboard display (live player scores)
- Popup system (PENALTY, PASS THE CHIP, TAKE IT OFF)
- Game instructions overlay
- Game mode selection screen

**Owner**: UI Engineer  
**Success Criteria**: All UI functional and responsive to game state

---

### **Sprint 6: Multi-Mode Integration & Menu System** (Week 6)
**Goal**: Connect all systems; polish gameplay loop  
**Deliverables**:
- Main Menu scene
- Mode selection → mode-specific setup
- Rules/Instructions screen
- Settings menu (volume, language, etc.)
- Resume / Restart / Back to Menu flow
- Pause functionality

**Owner**: UI Engineer + Gameplay Engineer  
**Success Criteria**: Full gameplay loop works for all 5 modes

---

### **Sprint 7: Platform Builds & Optimization** (Week 7)
**Goal**: Export to WebGL, Android, iOS  
**Deliverables**:
- WebGL build (optimized, IL2CPP)
- Android build (APK + Play Store ready)
- iOS build (Xcode project)
- Input handlers for each platform
- Safe area implementation (iOS notch)
- Performance profiling & optimization

**Owner**: Build Engineer + Platform Engineer  
**Success Criteria**: Builds work on actual devices

---

### **Sprint 8: QA, Playtesting & Polish** (Week 8)
**Goal**: Final testing, bug fixes, release preparation  
**Deliverables**:
- Comprehensive playtest (all modes, all platforms)
- Bug fixes & edge case handling
- Performance fine-tuning
- Release notes & documentation
- App Store / Play Store submissions (metadata, screenshots)

**Owner**: QA Lead + All Engineers  
**Success Criteria**: No critical bugs; ready for public release

---

## Milestones & Gates

| Milestone | Sprint | Gate Criteria |
|-----------|--------|---------------|
| **Logic Complete** | 1–3 | All game rules tested, 80%+ coverage |
| **Board Playable** | 4 | Board interactive, moves execute |
| **UI Functional** | 5 | All HUD elements respond correctly |
| **Full Loop** | 6 | One complete game playable end-to-end |
| **Build Ready** | 7 | Exports work on actual devices |
| **Release Ready** | 8 | All platforms QA'd, zero critical bugs |

---

## Team Structure

### Gameplay Engineer (Core + Board)
- Core game logic (dice, turns, rules)
- Game mode implementations
- Board system integration
- Game state machine

### UI Engineer
- HUD, menus, popups
- Canvas/layout setup
- Button interactions
- Settings & options

### Build Engineer
- WebGL, Android, iOS exports
- Build optimization
- Platform-specific tweaks
- CI/CD setup

### QA Lead
- Test plan execution
- Bug reporting & triage
- Playtest coordination
- Final sign-off

---

## Communication & Standup

**Daily Standup**: 15 min  
**Weekly Review**: Friday 30 min (sprint review, next sprint planning)  
**Blockers**: Reported immediately on #engineering Slack  
**Documentation**: All decisions logged in GitHub Issues/PRs  

---

## Success Criteria (End of Project)

✅ All 5 game modes fully playable  
✅ Zero critical bugs on all platforms  
✅ 60 FPS on modern devices, 30 FPS floor on older devices  
✅ Touch & mouse input both functional  
✅ App Store & Play Store approved  
✅ Complete documentation & onboarding guide  
✅ 80%+ unit test coverage for game logic  

---

**Last Updated**: Nov 14, 2025  
**Status**: Ready for team assignment
