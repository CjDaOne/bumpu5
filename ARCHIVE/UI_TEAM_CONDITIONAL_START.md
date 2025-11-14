# UI ENGINEERING TEAM - CONDITIONAL START ORDER
## Sprint 5 - UI Framework & HUD

**STATUS**: 🟡 **STANDBY - CONDITIONAL AUTHORIZATION TO START**  
**Date Issued**: Nov 14, 2025  
**Authority**: Managing Engineer (Amp)  
**Estimated Start**: Nov 26-27, 2025 (when Board reaches milestone)

---

## CONDITIONAL ACTIVATION CRITERIA

**You are AUTHORIZED to begin Sprint 5 when ANY of these conditions are met**:

1. ✅ Board Team reports BoardGridManager + BoardInputHandler COMPLETE (100% tests passing, integrated)
2. ✅ You receive explicit start order from Managing Engineer
3. ✅ Nov 27 arrives (hard start date - begin regardless)

**Estimated trigger**: Nov 26-27, 2025

---

## MISSION

Implement complete UI framework including HUD elements, popup system, and game state visualization - fully responsive and integrated with board + game logic.

---

## SPRINT 5 DELIVERABLES (5-7 days once activated)

### Phase 1: HUD Foundation (Days 1-2)
**Objective**: Core HUD elements + orchestrator

```
- [ ] HUDManager.cs (MonoBehaviour)
      * Orchestrator for all HUD elements
      * Listen to game state changes
      * Update all UI elements reactively
      * Manage button states (enabled/disabled)
      
- [ ] DiceRollButton.cs (MonoBehaviour)
      * Visual button for rolling dice
      * Animation on click (spin/press)
      * Enabled only when player can roll
      * Displays dice result after roll
      
- [ ] ScoreboardDisplay.cs (MonoBehaviour)
      * Show current player names
      * Display each player's position
      * Live update per move
      * Visual highlighting of current player
      
- [ ] Unit tests: 5+ tests
      * HUDManager state synchronization
      * Button state management
      * Display updates correctly
```

**Commit by**: End of Day 2

---

### Phase 2: Action Buttons & Feedback (Days 3-4)
**Objective**: Gameplay buttons + popups

```
- [ ] BumpButton.cs (MonoBehaviour)
      * Context-sensitive button
      * Enabled only during bump opportunities
      * Confirms bump action
      * Visual feedback (glow/highlight)
      
- [ ] DeclareWinButton.cs (MonoBehaviour)
      * Enabled only when win condition met
      * Triggers win declaration
      * Confirms game end
      
- [ ] PopupManager.cs (MonoBehaviour)
      * Manages all popup display
      * Fade in/out animations
      * Stacking multiple popups
      * Auto-dismiss or button dismiss
      
- [ ] PopupPrefab system
      * Generic popup template
      * Title + message + button
      * Customizable for different types
      
- [ ] Unit tests: 5+ tests
      * Button state transitions
      * Popup display/dismiss
      * Event handling
```

**Commit by**: End of Day 4

---

### Phase 3: Screen Navigation & Config (Days 5-6)
**Objective**: Game mode selection + responsive design

```
- [ ] GameModeSelectionScreen.cs
      * Display 5 game mode options
      * Click to select mode
      * Transitions to gameplay
      * Rules preview (optional)
      
- [ ] PhaseIndicator.cs (MonoBehaviour)
      * Display current game phase
      * Visual representation (text/icon)
      * Update with state changes
      * Help text for current phase
      
- [ ] ResponsiveCanvasManager.cs
      * Handle different screen sizes
      * Adjust button sizes (≥44px on mobile)
      * Layout scaling
      * Safe area handling (notches)
      
- [ ] Unit tests: 5+ tests
      * Mode selection working
      * Phase indicator updates
      * Responsive layout
      
- [ ] Integration tests: 2+ tests
      * Game mode selection → gameplay flow
      * HUD updates per game state
```

**Commit by**: End of Day 6

---

### Phase 4: Polish & Integration (Day 7)
**Objective**: Complete integration + code review ready

```
- [ ] Full HUD state binding
      * All elements sync with GameStateManager
      * Real-time updates
      * No stale UI state
      
- [ ] Animation polish
      * Button press animations
      * Popup fade in/out
      * State transition animations
      * Smooth feel throughout
      
- [ ] Responsive design verification
      * Test on multiple screen sizes
      * Touch targets ≥44px
      * Layout stability
      * No text overflow/clipping
      
- [ ] Code cleanup & documentation
      * 95%+ method documentation
      * Consistent naming
      * No dead code
      * Standards compliance
      
- [ ] Final testing
      * All 15+ tests passing
      * No warnings/errors
      * Integration verified
      * Performance optimized
```

**Commit by**: End of Day 7

---

## TECHNICAL REQUIREMENTS

### File Structure
```
Assets/Scripts/UI/
├── HUDManager.cs (orchestrator)
├── DiceRollButton.cs (roll button)
├── BumpButton.cs (bump button)
├── DeclareWinButton.cs (win button)
├── ScoreboardDisplay.cs (score/position)
├── PhaseIndicator.cs (game phase)
├── PopupManager.cs (popup system)
├── GameModeSelectionScreen.cs (mode selection)
├── ResponsiveCanvasManager.cs (layout)
├── Prefabs/
│   ├── Popup.prefab
│   ├── Button.prefab
│   ├── HUD.prefab
│   └── Canvas.prefab
└── Tests/
    ├── HUDManagerTests.cs
    ├── PopupManagerTests.cs
    ├── ButtonTests.cs
    └── UIIntegrationTests.cs
```

### Integration Points
```
UI (Sprint 5) ← Board (Sprint 4) ← Gameplay (Sprint 3)
├─ Gameplay: Emit state updates
├─ Board: Route input, show visuals
├─ UI: Display state, accept user input
└─ Feedback loop: State → UI update → User input → State
```

---

## DEPENDENCIES

✅ Sprint 3 complete (Game modes)  
✅ Sprint 4 complete (Board system)  
✅ GameStateManager available  
✅ All game logic stable & tested  
✅ Board team coordination on #ui channel  

---

## TESTING STRATEGY

### Unit Tests (15+)
```
HUDManager: 3 tests
  - State synchronization
  - Element visibility control
  - Event handling

DiceRollButton: 2 tests
  - Button enabled/disabled state
  - Click handling

BumpButton: 2 tests
  - Context-sensitive enabling
  - Action triggering

PopupManager: 3 tests
  - Popup display/dismiss
  - Stacking multiple popups
  - Auto-dismiss timing

GameModeSelectionScreen: 2 tests
  - Mode selection
  - Transition to gameplay

ResponsiveCanvasManager: 2 tests
  - Layout scaling
  - Safe area handling

Integration: 3 tests
  - Full HUD workflow
  - Button → game state
  - Popup → user input
```

---

## QUALITY GATES

| Gate | Requirement | Status |
|------|-------------|--------|
| Unit Tests | 15+ tests, 100% passing | [ ] |
| Documentation | 95% public methods | [ ] |
| Performance | UI update < 16ms (60 FPS) | [ ] |
| Responsive | Works on all screen sizes | [ ] |
| Touch Targets | ≥44px buttons on mobile | [ ] |
| Integration | Syncs with GameStateManager | [ ] |

---

## PREPARATION WORK (Do NOW)

While waiting for conditional start:

1. **Read Requirements**:
   - [ ] Re-read this document
   - [ ] Review Board system deliverables (Sprint 4)
   - [ ] Study GameStateManager state structure
   - [ ] Understand game flow & states

2. **Design Work**:
   - [ ] Sketch HUD layout
   - [ ] Design button placement
   - [ ] Plan popup styles
   - [ ] Design responsive breakpoints

3. **Architecture Planning**:
   - [ ] How will HUDManager listen to state?
   - [ ] Event system design (delegates/events)
   - [ ] Popup stack implementation
   - [ ] Responsive layout approach

4. **Code Skeleton**:
   - [ ] Create empty HUDManager.cs
   - [ ] Create empty button classes
   - [ ] Setup responsive canvas structure
   - [ ] Create test class files

5. **Research & Learning**:
   - [ ] Canvas scaling techniques
   - [ ] Animation curve best practices
   - [ ] Mobile touch target standards
   - [ ] Safe area handling (iOS notch)

---

## SUCCESS CRITERIA

**Sprint 5 complete when**:
1. ✅ All HUD elements rendered & functional
2. ✅ All buttons respond to game state correctly
3. ✅ Popup system working for all scenarios
4. ✅ Game mode selection integrated
5. ✅ Responsive on all screen sizes
6. ✅ Touch targets ≥44px on mobile
7. ✅ 15+ unit tests 100% passing
8. ✅ 95%+ documentation
9. ✅ Code review approval
10. ✅ Ready for next sprint (menu integration)

---

## TIMELINE (Estimated)

```
Nov 26-27: Receive start authorization
  └─ Condition: Board integration complete

Nov 27-28 (Days 1-2): HUDManager + DiceRollButton
  └─ Commit by Nov 28

Nov 29-30 (Days 3-4): BumpButton + PopupManager
  └─ Commit by Nov 30

Dec 1-2 (Days 5-6): Mode Selection + Responsive Design
  └─ Commit by Dec 2

Dec 3-4 (Day 7): Integration + Code Review
  └─ Final commit Dec 3
  └─ Code review Dec 4

TARGET: SPRINT 5 COMPLETE BY DEC 4 ✅
```

---

## MANAGING ENGINEER OVERSIGHT

**Once you start**:
- Daily code review (< 4 hours)
- Attend daily standup (9 AM UTC)
- Weekly sprint review (Friday 5 PM UTC)
- Escalation available < 1 hour for blockers

---

## FINAL INSTRUCTIONS

**NOW (Nov 14-26)**:
1. ✅ Read this document thoroughly
2. ✅ Do all preparation & design work
3. ✅ Prepare code skeleton
4. ✅ Get ready to start immediately when signaled

**When conditional activation triggers (Nov 26-27)**:
1. ✅ You'll receive explicit "START NOW" message
2. ✅ Immediately begin Phase 1
3. ✅ Attend daily standup at 9 AM UTC
4. ✅ Commit daily
5. ✅ Coordinate with Board team

**You are ON CALL and ready to execute immediately upon signal.**

---

**Assignment Issued**: Nov 14, 2025  
**Authority**: Amp, Managing Engineer  
**Status**: CONDITIONAL - AWAITING TRIGGER

Stand by for activation signal around Nov 26-27, 2025.

