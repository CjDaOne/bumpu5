# BOARD ENGINEERING TEAM - SPRINT 4 ARCHITECTURE PHASE
## Board System Integration Preparation - BEGIN NOW

**From**: Amp (Managing Engineer)  
**To**: Board Engineer Agent  
**Date**: Nov 14, 2025  
**Authority**: BEGIN ARCHITECTURE PHASE NOW  
**Target Completion**: Design complete by Nov 21 (when Sprint 3 finishes)  
**Formal Execution**: Sprint 4 starts Dec 5

---

## MISSION OVERVIEW

Design complete board system architecture to be implemented Dec 5-12. This is foundational work that enables:
- Game modes to be visualized and playable
- UI to have interactive board
- Animations to display gameplay

**Current Status**: Design phase (parallel with Gameplay Team's Sprint 3)

---

## ARCHITECTURE DESIGN TASKS

### Task 1: BoardGridManager Architecture
**File to Design**: `Assets/Scripts/Board/BoardGridManager.cs`

**Purpose**: Orchestrator for 12-cell board layout, state management, and display

**Design Considerations**:
```
Questions to answer in design doc:

1. Cell Management
   - How to initialize 12 cells?
   - How to reference cells (by index? by position?)
   - How to store cell state (occupied, by which player)?

2. Layout
   - 12 cells in a specific arrangement
   - How to position visually? (prefabs, positioning)
   - How to scale for different screen sizes?

3. Chip Placement
   - How to place chips on cells?
   - How to track chip position?
   - How to remove chips (home/bump)?

4. State Synchronization
   - How to sync board visuals with GameStateManager?
   - When does GameStateManager tell BoardGridManager to update?
   - Two-way communication needed?

5. Performance
   - Cache cell references?
   - Update strategy (OnDemand vs Polling)?
```

**Deliverable**: Architecture design document with:
- Class structure diagram
- Property/method list
- Initialization sequence
- Update flow diagram
- Integration points with GameStateManager

**Target**: Complete by Nov 18

---

### Task 2: Cell Interaction System
**Files to Design**: `Assets/Scripts/Board/BoardCell.cs` (view), `Assets/Scripts/Board/BoardInputHandler.cs`

**Purpose**: Enable clicking/tapping cells to execute moves

**Design Considerations**:
```
Cell View Component:
- Visual representation of single cell
- Highlight state (valid move? selected? occupied?)
- Animation triggers (chip drop, bump animation)
- Touch/click detection

Input Handler:
- Detect tap/click on cells
- Filter valid moves based on game state
- Send move command to GameStateManager
- Provide visual feedback (highlight, animation)

Valid Move Highlighting:
- When is valid move set computed?
- How to highlight which cells are valid?
- How to update highlights during turn?
- How to clear highlights?
```

**Deliverable**: Design document with:
- Cell prefab structure
- Input flow diagram
- Valid move filtering logic
- Visual feedback state machine
- Integration with GameStateManager

**Target**: Complete by Nov 18

---

### Task 3: Valid Move Generation Algorithm
**Purpose**: Translate GameStateManager moves into visual board highlighting

**Design Considerations**:
```
Algorithm:
1. GameStateManager computes valid moves (as list of cells)
2. Board displays these as highlighted cells
3. Player taps highlighted cell
4. Board sends move back to GameStateManager

Questions:
- How does GameStateManager expose valid moves?
- How does Board request valid moves?
- How often does Board update highlights?
- What happens if no valid moves exist?
```

**Deliverable**: Algorithm design with:
- Move computation flow
- Highlight update sequence
- Edge cases (no moves, forced move, etc.)
- Visual feedback strategy

**Target**: Complete by Nov 19

---

### Task 4: Chip Animation System
**Purpose**: Visual feedback for game actions

**Animations to Design**:
1. **Chip Drop**: Chip falls from top to land on cell
2. **Chip Slide**: Chip moves from one cell to another
3. **Chip Bump**: Chip is bumped (animation to show removal)
4. **Chip Home**: Chip moves to home area (end state)

**Design Considerations**:
```
- Animation duration (should feel responsive, not slow)
- Animation triggers (when does GameStateManager say "play this animation"?)
- Sequencing (can multiple animations play simultaneously?)
- Performance (is animator component used? Or manual lerp?)
- Feedback (should animation block further input? Or allow queuing?)
```

**Deliverable**: Animation design document with:
- Each animation sequence
- Trigger conditions
- Duration specifications
- Performance impact analysis
- Integration with game state

**Target**: Complete by Nov 19

---

### Task 5: Board-to-GameState Integration Strategy
**Purpose**: Define how Board system and GameStateManager communicate

**Integration Points**:
```
1. Game Initialization
   - GameStateManager creates game
   - Board listens for game started event
   - Board displays initial state

2. Turn Execution
   - Player clicks cell (Board detects)
   - Board sends move to GameStateManager
   - GameStateManager validates & executes
   - GameStateManager notifies Board of result
   - Board updates display

3. State Updates
   - Chip placement
   - Chip removal (bump, home)
   - Turn transitions
   - Game end
```

**Deliverable**: Integration design with:
- Event flow diagram
- Method signatures for Board ↔ GameStateManager
- State synchronization strategy
- Error handling (invalid clicks, etc.)

**Target**: Complete by Nov 20

---

### Task 6: Visual Design & Prefab Structure
**Purpose**: Finalize how board looks and is organized in hierarchy

**Design Considerations**:
```
Scene Hierarchy:
- BoardContainer (root)
  - GridLayout (positions cells)
    - Cell_0 (prefab instance)
    - Cell_1 (prefab instance)
    - ... (12 total)
  - ChipsContainer (holds chip visuals)
    - Chip_P1_0 (player 1, chip 0)
    - Chip_P2_0 (player 2, chip 0)
    - ... (up to 4 players × 4 chips)
  - AnimationLayer (for animations)

Cell Prefab Structure:
- Image component (cell background)
- Text component (optional: cell number)
- Button component (click detection)
- Highlight Image (for valid move display)
- Animators (for animations)

Chip Prefab Structure:
- Image component (chip visual)
- RectTransform (positioning)
- Animator (for animations)
```

**Deliverable**: Prefab design with:
- Scene hierarchy diagram
- Prefab component lists
- Data structure for chip tracking
- Visual feedback states
- Scaling/responsive design strategy

**Target**: Complete by Nov 20

---

## DESIGN DOCUMENT TEMPLATE

For each design task above, create a document with:

```markdown
## [Task Name]

### Purpose
[One sentence describing why this exists]

### Design Overview
[High-level explanation of how it works]

### Key Components
- Component 1: [Purpose]
- Component 2: [Purpose]

### Data Flow
[Diagram or description of information flow]

### Integration Points
- [System A] → how it connects
- [System B] → how it connects

### Edge Cases
- [Edge case 1]: [How handled]
- [Edge case 2]: [How handled]

### Performance Considerations
- [Concern 1]: [Solution]
- [Concern 2]: [Solution]

### Dependencies
- GameStateManager (for game state)
- [Other systems]

### Open Questions
- [Question 1]
- [Question 2]

### Implementation Notes
- [Note 1]
- [Note 2]
```

---

## REFERENCE TO SPRINT 3

You don't need game modes implemented yet, but you should:
- [ ] Review SPRINT_3_DETAILED_BRIEFING.md to understand game rules
- [ ] Understand what "valid moves" means in context of each mode
- [ ] Note any mode-specific board behaviors needed

By Nov 21, you'll have confirmed game mode implementations. Your design should be ready to integrate with them Dec 5.

---

## DAILY PROGRESS REPORTING

Report at 9 AM UTC standup:
- ✅ Completed since yesterday
- 🔄 In progress today
- 🚫 Blockers
- 📈 % complete
- 📋 Design documents ready for review

**Example Day 1**:
> ✅ Completed: None (kickoff day)  
> 🔄 In Progress: BoardGridManager architecture design  
> 🚫 Blockers: None  
> 📈 Progress: 10%  
> 📋 Design docs: 0/6 ready

---

## SUCCESS CRITERIA

✅ **Complete architecture design** for all components  
✅ **Design documents** for all 6 tasks  
✅ **Clear integration plan** with GameStateManager  
✅ **Performance strategy** defined  
✅ **Prefab structure** documented  
✅ **Ready to implement** Dec 5 (no design gaps)

---

## MANAGING ENGINEER SUPPORT

I'm available for:
- Architecture clarifications
- GameStateManager integration questions
- Design review & feedback
- Blocker resolution

Contact: Direct message for urgent, #board for updates

---

## TIMELINE

| Date | Phase | Target |
|------|-------|--------|
| Nov 14-15 | Design start | Task 1-2 begin |
| Nov 16-17 | Design progress | Task 3-4 progress |
| Nov 18-19 | Design completion | Task 5-6 complete |
| Nov 20 | Design review | Final refinements |
| Nov 21 | Ready for execution | Sprint 3 complete, Sprint 4 ready |
| Dec 5 | Sprint 4 kickoff | Begin implementation |

---

## NEXT: FORMAL SPRINT 4 EXECUTION

When Sprint 3 completes (Nov 21), I will issue TEAM_DISPATCH_SPRINT4_BOARD_EXECUTE.md with:
- Detailed implementation tasks
- Code structure specifications
- Unit test requirements
- Code review criteria

Your design work now ensures Sprint 4 execution is smooth and on-schedule.

---

**Status**: BEGIN DESIGN PHASE NOW  
**Authority**: FULL ARCHITECTURE OWNERSHIP  
**Deadline**: Design complete Nov 21  
**Impact**: Enables Sprint 4 execution & downstream sprints

**You're authorized to proceed. Begin immediately. Design thoroughly. Deliver on time.**

---

**From**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025

---

*End of Dispatch*
