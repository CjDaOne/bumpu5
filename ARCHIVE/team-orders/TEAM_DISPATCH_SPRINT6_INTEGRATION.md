# INTEGRATION TEAM - SPRINT 6 EXECUTION ORDER
## Multi-Mode Integration & Full Game Loop - EXECUTE NOW

**From**: Amp (Managing Engineer)  
**To**: Integration Engineer Agent  
**Date**: Nov 21, 2025  
**Authority**: FULL EXECUTION - BEGIN IMMEDIATELY  
**Target Completion**: Dec 5, 2025  
**Mission**: Integrate all systems into complete playable game

---

## SPRINT 6 EXECUTION AUTHORITY

**Status**: ✅ **EXECUTE NOW**  
**Scope**: Complete game integration  
**Timeline**: Nov 21-Dec 5 (15 days)  
**Quality Gate**: Full game playable end-to-end, zero critical bugs  
**Authority**: Managing Engineer - Full mobilization order  
**Trigger Events**: Sprint 3 complete (Nov 21), Sprint 4-5 progress

---

## MISSION OVERVIEW

Integrate all components (Gameplay logic, Board system, UI/HUD) into seamless playable experience.

**Starting State** (Nov 21):
- ✅ Game logic complete (5 modes + game state manager)
- ✅ Board system complete (BoardGridManager, animations)
- 🔄 UI system in progress (will be ready Dec 5)

**Ending State** (Dec 5):
- ✅ Full game playable from menu to victory screen
- ✅ All 5 game modes work end-to-end
- ✅ All menus functional
- ✅ All animations coordinated
- ✅ Cross-system communication seamless

---

## DELIVERABLES (5 TOTAL)

### 1. GameFlowManager.cs (Main Orchestrator)
**Target**: Nov 21-23  
**Lines**: ~400-500  

**Functionality**:
- Coordinate gameplay, board, UI, state
- Manage game phases (Menu → Mode Selection → Gameplay → End)
- Handle transitions between screens
- Manage game lifecycle
- Error handling & recovery

**Interface Requirements**:
```csharp
public class GameFlowManager : MonoBehaviour
{
    public void ShowMainMenu();
    public void ShowModeSelection();
    public void StartNewGame(GameMode mode, int playerCount);
    public void ResumeGame(GameState savedState);
    public void PauseGame();
    public void ResumeFromPause();
    public void EndGame(Player winner);
    public void ReturnToMainMenu();
    public void ExitGame();
    
    public event System.Action OnGameStarted;
    public event System.Action OnGamePaused;
    public event System.Action OnGameEnded;
}
```

**Key Responsibilities**:
- Initialize GameStateManager for new games
- Connect board input to game state
- Display UI popups at correct times
- Manage pause/resume logic
- Save/load game state
- Handle errors gracefully

**Tests**: 8 unit tests
- Game initialization test
- Screen transition test
- Pause/resume test
- Game end test
- Error recovery test
- Save/load test
- Multi-game sequence test
- Edge case handling test

---

### 2. InputCoordinationSystem.cs (Input Router)
**Target**: Nov 23-24  
**Lines**: ~200-250  

**Functionality**:
- Route input to correct handler based on game state
- Board input when playing game
- UI input (buttons) always available
- Menu input when in menus
- Pause button always available
- Disable input during animations

**Interface Requirements**:
```csharp
public class InputCoordinationSystem : MonoBehaviour
{
    public void EnableGameplayInput();
    public void DisableGameplayInput();
    public void EnableMenuInput();
    public void DisableMenuInput();
    public void BlockAllInput(float duration);
    
    private void RouteTouchInput(Vector2 screenPos);
    private void RouteClickInput(Vector2 screenPos);
}
```

**Input Priorities**:
1. Pause button (always available)
2. Popup buttons (when popup open)
3. Gameplay input (when in game)
4. Menu input (when in menu)

**Tests**: 4 unit tests
- Input routing test
- Priority handling test
- Animation blocking test
- State transition test

---

### 3. GameStateSync.cs (State Synchronization)
**Target**: Nov 24-25  
**Lines**: ~250-300  

**Functionality**:
- Listen to GameStateManager changes
- Update board visuals
- Update UI/HUD
- Trigger animations
- Trigger popups
- Handle turn transitions

**Interface Requirements**:
```csharp
public class GameStateSync : MonoBehaviour
{
    public void OnGameStateChanged(GameState newState);
    public void OnPlayerTurnChanged(Player player);
    public void OnPhaseChanged(GamePhase phase);
    public void OnChipPlaced(Chip chip, int cellIndex);
    public void OnChipBumped(Chip chip);
    public void OnGameEnded(Player winner);
}
```

**Sync Points**:
- Player turn → Update HUD (highlight current player)
- Dice roll → Show result, enable buttons
- Chip placed → Animate placement, update board
- Win condition → Show popup, end game
- Phase change → Update UI buttons

**Tests**: 6 unit tests
- State change reaction test
- Turn transition test
- Phase change test
- Visual update test
- Animation trigger test
- Popup trigger test

---

### 4. PauseResumeSystem.cs (Game Control)
**Target**: Nov 25-26  
**Lines**: ~150-200  

**Functionality**:
- Pause game at any time
- Save game state on pause
- Show pause menu (Resume / Main Menu / Settings)
- Resume game
- Handle pause during animations
- Prevent actions during pause

**Interface Requirements**:
```csharp
public class PauseResumeSystem : MonoBehaviour
{
    public void PauseGame();
    public void ResumeGame();
    public void ShowPauseMenu();
    public void HidePauseMenu();
    
    public event System.Action<bool> OnGamePaused;
}
```

**Features**:
- Can pause anytime (including during animations)
- Pause menu appears on top
- Game state frozen (input disabled)
- Resume returns to exact game state
- Can return to main menu from pause
- Settings accessible from pause

**Tests**: 4 unit tests
- Pause functionality test
- Resume functionality test
- State preservation test
- Menu accessibility test

---

### 5. Integration Tests (IntegrationTestSuite.cs)
**Target**: Nov 26-Dec 5  
**Tests**: 20+ comprehensive integration tests

**Test Coverage**:
- **Game Flow Tests** (5 tests)
  - [ ] Menu → Mode Selection → Gameplay flow
  - [ ] Complete game playable start to finish
  - [ ] Game end triggers correctly
  - [ ] Menu return works
  - [ ] Exit works cleanly

- **Gameplay Tests** (8 tests)
  - [ ] Bump5 mode full game
  - [ ] Krazy6 mode full game
  - [ ] PassTheChip mode full game
  - [ ] BumpUAnd5 mode full game
  - [ ] Solitary mode full game
  - [ ] Multiple games in sequence
  - [ ] Win detection works
  - [ ] Edge cases handled

- **UI Integration Tests** (4 tests)
  - [ ] HUD updates during gameplay
  - [ ] Popups appear at correct times
  - [ ] Menu navigation works
  - [ ] UI responsive to game state

- **Board Integration Tests** (3 tests)
  - [ ] Board updates with game state
  - [ ] Animations trigger correctly
  - [ ] Input blocked during animations

- **System Integration Tests** (2 tests)
  - [ ] Pause/resume preserves state
  - [ ] Save/load works correctly

---

## DAILY SPRINT SCHEDULE

### Days 1-2 (Nov 21-22)
- [ ] Sprint 6 kickoff meeting
- [ ] Receive Sprint 4-5 status updates
- [ ] Set up development branch
- [ ] Begin GameFlowManager skeleton
- **Standup**: Team oriented, integration approach clear

### Days 3-4 (Nov 23-24)
- [ ] GameFlowManager 60% complete
- [ ] InputCoordinationSystem started
- [ ] Coordinate with Board & UI teams
- **Standup**: GameFlow at 60%, Input started

### Days 5-6 (Nov 25-26)
- [ ] GameFlowManager complete & tested
- [ ] InputCoordinationSystem complete & tested
- [ ] GameStateSync started (50%)
- **Standup**: GameFlow & Input done, Sync started

### Days 7-8 (Nov 27-28)
- [ ] GameStateSync 80% complete
- [ ] PauseResumeSystem started (50%)
- [ ] Basic game flow working
- [ ] Testing started
- **Standup**: Sync & Pause started, basic flow working

### Days 9-11 (Nov 29-Dec 1)
- [ ] GameStateSync complete & tested
- [ ] PauseResumeSystem complete & tested
- [ ] Integration tests started (10/20 passing)
- [ ] All game modes playable
- **Standup**: All components done, integration testing

### Days 12-14 (Dec 2-4)
- [ ] All 20+ integration tests complete
- [ ] End-to-end game playable
- [ ] All features working
- [ ] Code review with Managing Engineer
- **Standup**: All tests passing, ready for review

### Day 15 (Dec 5)
- [ ] Final code review sign-off
- [ ] All systems integrated & working
- [ ] Formal approval
- [ ] Ready for Sprint 7
- **Standup**: Sprint complete, all systems go

---

## QUALITY STANDARDS

**Code Standards**: CODING_STANDARDS.md compliance required
- ✅ Proper naming conventions
- ✅ 100% documentation (public methods)
- ✅ Error handling for edge cases
- ✅ Proper dependency injection

**Testing Standards**:
- ✅ Unit tests for all public methods
- ✅ 80%+ code coverage minimum
- ✅ Integration tests covering all flows
- ✅ All tests passing before review

**Performance Standards**:
- ✅ 60 FPS target during gameplay
- ✅ 30 FPS minimum on older devices
- ✅ < 100ms response to input
- ✅ No frame drops during transitions
- ✅ Smooth animations throughout

**Functionality Standards**:
- ✅ All 5 game modes fully playable
- ✅ All menus accessible & functional
- ✅ Pause/resume works perfectly
- ✅ Cross-system sync seamless
- ✅ Error recovery graceful

---

## DEPENDENCIES & COORDINATION

**What You Need From Others**:
- ✅ Sprint 3: Game logic complete (Nov 21)
- ✅ Sprint 4: Board system complete (Nov 20)
- 🔄 Sprint 5: UI system (will be complete Dec 5)

**Board Team Coordination**:
- Confirm board layout (Nov 20)
- Verify animation triggers (Nov 22)
- Test board input routing (Nov 23)

**UI Team Coordination**:
- Confirm screen transitions (Nov 22)
- Verify popup triggers (Nov 24)
- Test HUD updates (Nov 25)

**Gameplay Team Coordination**:
- Confirm game rules (Nov 21)
- Verify game state changes (Nov 22)
- Test all game modes (Nov 23+)

---

## STANDUP REPORTING TEMPLATE

**Report at 9 AM UTC daily**:

```
✅ COMPLETED SINCE YESTERDAY:
- [Component status]
- [Integration progress]

🔄 IN PROGRESS TODAY:
- [Current work]
- [Coordination needed]

🚫 BLOCKERS:
- [If any, with impact]

📈 PROGRESS: X%

📋 READY FOR REVIEW:
- [What's working]
- [What needs polish]
```

---

## SUCCESS CRITERIA

✅ **All deliverables complete** (5/5)  
✅ **All unit tests passing** (15+ tests, 100% pass rate)  
✅ **All integration tests passing** (20+ tests, 100% pass rate)  
✅ **All 5 game modes fully playable**  
✅ **All menus functional**  
✅ **Pause/resume works**  
✅ **Cross-system sync seamless**  
✅ **Performance targets met** (60/30 FPS)  
✅ **Zero critical bugs** at sign-off  
✅ **Code review approved** by Managing Engineer  
✅ **Formal sign-off** by Dec 5

---

## RESOURCES & SUPPORT

**Managing Engineer** (Amp):
- Integration guidance: immediate response
- Code review: < 2 hours turnaround
- Blocker resolution: < 1 hour
- Team coordination: daily standups

**Communication**:
- #blockers for cross-team issues
- Direct message for urgent integration questions
- Daily standup for status updates

---

## GO/NO-GO DECISION

**Current**: 🟢 **GO** - Dependencies on track, team ready, no blockers

**Status**: Begin immediately. Coordinate with teams. Integrate thoroughly. Test extensively.

**Authority**: Full integration team mobilization - EXECUTE NOW

---

**From**: Amp (Managing Engineer)  
**Date**: Nov 21, 2025, 6:00 PM UTC  
**Authority**: FULL EXECUTION ORDER

---

*End of Dispatch*
