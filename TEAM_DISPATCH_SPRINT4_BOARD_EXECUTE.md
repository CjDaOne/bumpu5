# BOARD ENGINEERING TEAM - SPRINT 4 EXECUTION ORDER
## Board System Implementation - EXECUTE NOW

**From**: Amp (Managing Engineer)  
**To**: Board Engineer Agent  
**Date**: Nov 14, 2025  
**Authority**: FULL EXECUTION - BEGIN IMMEDIATELY  
**Target Completion**: Nov 20, 2025  
**Mission**: Complete board system integration (BoardGridManager, cell interactions, animations)

---

## SPRINT 4 EXECUTION AUTHORITY

**Status**: ✅ **EXECUTE NOW**  
**Scope**: 8 core deliverables  
**Timeline**: Nov 14-20 (7 days, accelerated)  
**Quality Gate**: All tests passing, code review approved  
**Authority**: Managing Engineer - Full mobilization order

---

## DELIVERABLES (8 TOTAL)

### 1. BoardGridManager.cs (Core Implementation)
**Target**: Nov 15-16  
**Lines**: ~300-400  

**Functionality**:
- Initialize 12-cell grid
- Cell state management (occupied, by which player)
- Chip placement & removal
- State synchronization with GameStateManager
- Valid move highlighting
- Cell reference tracking

**Interface Requirements**:
```csharp
public class BoardGridManager : MonoBehaviour
{
    public void InitializeBoard(GameState gameState);
    public void PlaceChip(int cellIndex, Player player);
    public void RemoveChip(int cellIndex);
    public void HighlightValidMoves(List<int> validCellIndices);
    public void ClearHighlights();
    public void UpdateBoardFromGameState(GameState state);
    public List<BoardCell> GetCells();
    public BoardCell GetCellAtIndex(int index);
}
```

**Dependencies**:
- GameStateManager (state provider)
- BoardCell component
- IGameMode interface (for mode-specific rules)

**Tests**: 5 unit tests
- Initialization test
- Chip placement test
- Valid move highlighting test
- State synchronization test
- Integration test with GameStateManager

---

### 2. BoardCell.cs (Cell Visual Component)
**Target**: Nov 16-17  
**Lines**: ~150-200  

**Functionality**:
- Visual representation of single cell
- Highlight state management (valid, selected, occupied)
- Animation trigger system
- Touch/click detection
- Cell index tracking

**Interface Requirements**:
```csharp
public class BoardCell : MonoBehaviour
{
    public int CellIndex { get; set; }
    public void Highlight(Color highlightColor);
    public void Unhighlight();
    public void PlaceChip(Chip chip);
    public void RemoveChip();
    public void TriggerAnimation(AnimationType animType);
    public event System.Action<int> OnCellClicked;
}
```

**Tests**: 3 unit tests
- Visual state test
- Click detection test
- Animation trigger test

---

### 3. BoardInputHandler.cs (Input Management)
**Target**: Nov 16-17  
**Lines**: ~200-250  

**Functionality**:
- Detect cell clicks/taps
- Validate moves against GameStateManager
- Send move commands
- Visual feedback management
- Input blocking during animations

**Interface Requirements**:
```csharp
public class BoardInputHandler : MonoBehaviour
{
    public void EnableInput();
    public void DisableInput();
    public void HandleCellClick(int cellIndex);
    public event System.Action<int> OnMoveRequested;
}
```

**Tests**: 3 unit tests
- Valid move filtering test
- Input blocking test
- Move command test

---

### 4. ChipAnimationSystem.cs (Animation Controller)
**Target**: Nov 17-18  
**Lines**: ~250-300  

**Functionality**:
- Chip drop animation (fall from top)
- Chip slide animation (move between cells)
- Chip bump animation (removal feedback)
- Chip home animation (end state)
- Animation sequencing & queuing
- Performance optimization

**Interface Requirements**:
```csharp
public class ChipAnimationSystem : MonoBehaviour
{
    public void AnimateChipDrop(Chip chip, Transform targetCell);
    public void AnimateChipSlide(Chip chip, Transform fromCell, Transform toCell);
    public void AnimateChipBump(Chip chip);
    public void AnimateChipHome(Chip chip);
    public void StopAllAnimations();
    public event System.Action OnAnimationComplete;
}
```

**Specifications**:
- Drop: 0.5s, easing curve
- Slide: 0.3s, smooth interpolation
- Bump: 0.4s, visual feedback
- Home: 0.6s, celebratory animation
- No frame drops during animations

**Tests**: 4 unit tests
- Drop animation test
- Slide animation test
- Bump animation test
- Animation sequencing test

---

### 5. GameState-Board Integration (BoardGameStateListener.cs)
**Target**: Nov 18-19  
**Lines**: ~200-250  

**Functionality**:
- Listen to GameStateManager events
- Update board visuals on state changes
- Sync chip positions
- Handle game phase transitions
- Coordinate animations with game logic

**Interface Requirements**:
```csharp
public class BoardGameStateListener : MonoBehaviour
{
    public void OnGameStateChanged(GameState newState);
    public void OnChipPlaced(Chip chip, int cellIndex);
    public void OnChipBumped(Chip chip);
    public void OnChipHome(Chip chip);
    public void OnTurnChanged(Player newPlayer);
    public void OnGameEnded(Player winner);
}
```

**Tests**: 3 unit tests
- State change reaction test
- Chip placement sync test
- Game phase transition test

---

### 6. BoardPrefabStructure.prefab (Scene Setup)
**Target**: Nov 19  

**Hierarchy**:
```
BoardContainer
├── GridLayout (12 cells)
│   ├── Cell_0 (prefab)
│   ├── Cell_1 (prefab)
│   └── ... (12 total)
├── ChipsContainer
│   ├── Chip_P1_0
│   ├── Chip_P1_1
│   └── ... (4 players × 4 chips = 16 total)
└── AnimationLayer
```

**Prefab Design**:
- Cell prefab: Image, Button, HighlightImage, Animator
- Chip prefab: Image, RectTransform, Animator
- Responsive scaling for mobile/tablet/desktop

---

### 7. Board Integration Tests (BoardIntegrationTests.cs)
**Target**: Nov 19-20  
**Tests**: 8 integration tests

**Test Coverage**:
- Full game mode playability
- Board + GameStateManager sync
- Multi-player game flow
- Animation + Input timing
- Edge case handling

**Critical Path Tests**:
- [ ] Can play full Bump5 game on board
- [ ] Can play full PassTheChip game on board
- [ ] All animations trigger correctly
- [ ] No visual glitches during gameplay
- [ ] Input responsive & accurate

---

### 8. Code Review & Sign-Off
**Target**: Nov 20  

**Review Criteria**:
- ✅ All 300+ lines production code comply with CODING_STANDARDS.md
- ✅ All unit tests passing (100% pass rate)
- ✅ Code coverage 80%+
- ✅ Performance targets met (60 FPS on modern, 30 minimum)
- ✅ Integration with GameStateManager verified
- ✅ No critical issues identified
- ✅ All deliverables complete

---

## DAILY SPRINT SCHEDULE

### Day 1 (Nov 14)
- [ ] Sprint 4 kickoff meeting
- [ ] Review architecture design docs
- [ ] Set up development branch
- [ ] Begin BoardGridManager skeleton
- **Standup Report**: Team oriented, architecture understood, Day 1 tasks clear

### Day 2 (Nov 15)
- [ ] Complete BoardGridManager (60% done)
- [ ] Begin BoardCell.cs
- [ ] Unit tests for GridManager started
- **Standup Report**: GridManager at 60%, Cell.cs started, no blockers

### Day 3 (Nov 16)
- [ ] BoardGridManager complete & tested
- [ ] BoardCell complete & tested
- [ ] Begin BoardInputHandler & AnimationSystem
- **Standup Report**: GridManager & Cell complete, Input/Animation started

### Day 4 (Nov 17)
- [ ] BoardInputHandler complete & tested
- [ ] ChipAnimationSystem complete (60%)
- [ ] AnimationSystem integration
- **Standup Report**: Input handler done, Animation at 60%

### Day 5 (Nov 18)
- [ ] ChipAnimationSystem complete & tested
- [ ] BoardGameStateListener complete (70%)
- [ ] Integration testing started
- **Standup Report**: Animation done, Integration starting

### Day 6 (Nov 19)
- [ ] BoardGameStateListener complete & tested
- [ ] Prefab structure setup
- [ ] Integration tests (all 8 complete)
- **Standup Report**: All components done, integration tests in progress

### Day 7 (Nov 20)
- [ ] All integration tests passing
- [ ] Code review (managing engineer)
- [ ] Final adjustments & sign-off
- [ ] Documentation complete
- **Standup Report**: Ready for formal review, all tests passing

---

## QUALITY STANDARDS

**Code Standards**: CODING_STANDARDS.md compliance required
- ✅ Proper naming conventions
- ✅ 100% documentation (public methods)
- ✅ Error handling for edge cases
- ✅ No hardcoded values (use constants)
- ✅ Proper dependency injection

**Testing Standards**:
- ✅ Unit tests for all public methods
- ✅ 80%+ code coverage minimum
- ✅ All tests passing before code review
- ✅ Integration tests verify end-to-end flow

**Performance Standards**:
- ✅ 60 FPS target on modern devices
- ✅ 30 FPS minimum on older devices
- ✅ No frame drops during animations
- ✅ Memory usage < 50MB for board system
- ✅ Animation latency < 16ms (1 frame)

---

## DEPENDENCIES & INTEGRATION

**What You Need From Others**:
- ✅ GameStateManager (from Sprint 2) - Available
- ✅ Game mode implementations (from Sprint 3) - Will be available Nov 21
- ✅ UI layout plan (from Sprint 5) - Available for layout coordination

**What Depends On You**:
- Sprint 5 (UI Framework) - Needs board layout specs
- Sprint 6 (Integration) - Needs working board
- Downstream teams - All depend on working board system

**Coordination Points**:
- Nov 18: Share board layout specs with UI team
- Nov 21: Board system ready for Spring 6 integration
- Dec 5: Ready for full integration testing with all systems

---

## STANDUP REPORTING TEMPLATE

**Report at 9 AM UTC daily**:

```
✅ COMPLETED SINCE YESTERDAY:
- [Task 1 status]
- [Task 2 status]

🔄 IN PROGRESS TODAY:
- [Task 1]
- [Task 2]

🚫 BLOCKERS:
- [If any]

📈 PROGRESS: X%

📋 DELIVERABLES READY FOR REVIEW:
- [Which docs/code ready]
```

---

## SUCCESS CRITERIA

✅ **All deliverables complete** (8/8)  
✅ **All unit tests passing** (18+ tests, 100% pass rate)  
✅ **Code coverage 80%+**  
✅ **Integration tests passing** (8/8)  
✅ **Board playable with all 5 game modes**  
✅ **Performance targets met** (60/30 FPS)  
✅ **Zero critical bugs** identified in review  
✅ **Code review approved** by Managing Engineer  
✅ **Formal sign-off** by Nov 20

---

## RESOURCES & SUPPORT

**Managing Engineer** (Amp):
- Code review: < 2 hours turnaround
- Architecture questions: immediate response
- Blocker resolution: < 1 hour
- Contact: Direct message for urgent, #board for updates

**Reference Materials**:
- CODING_STANDARDS.md
- SPRINT_3_DETAILED_BRIEFING.md (game mode specs)
- TEAM_DISPATCH_SPRINT4_BOARD_PREP.md (architecture designs)
- GameStateManager.cs (in codebase)

---

## TIMELINE AT A GLANCE

| Date | Day | Target | Status |
|------|-----|--------|--------|
| Nov 14 | 1 | Kickoff, architecture review | Start |
| Nov 15 | 2 | GridManager & Cell started | Progress |
| Nov 16 | 3 | GridManager & Cell complete | Progress |
| Nov 17 | 4 | Input & Animation started | Progress |
| Nov 18 | 5 | All components in progress | Progress |
| Nov 19 | 6 | All components complete, integration started | Progress |
| Nov 20 | 7 | Code review & sign-off | Complete |

---

## AUTHORITY & AUTHORITY CHAIN

**This is a formal execution order.**

- **Authority**: Managing Engineer (Amp)
- **Start**: Nov 14, 2025 (NOW)
- **Deadline**: Nov 20, 2025 (hard date)
- **Escalation**: Any blockers reported within 1 hour

**You are authorized to:**
- Use any libraries already in project
- Modify design docs if necessary (with explanation)
- Request Managing Engineer support immediately
- Make implementation decisions within scope

---

## GO/NO-GO DECISION

**Current**: 🟢 **GO** - All dependencies met, team ready, no blockers identified

**Status**: Begin immediately. Execute thoroughly. Maintain quality. Deliver on schedule.

**Authority**: Full board engineering team mobilization - EXECUTE NOW

---

**From**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025, 6:00 PM UTC  
**Authority**: FULL EXECUTION ORDER

---

*End of Dispatch*
