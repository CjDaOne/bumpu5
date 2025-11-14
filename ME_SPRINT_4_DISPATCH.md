# MANAGING ENGINEER SPRINT 4 DISPATCH
## Board Engineering Team - Immediate Activation

**Issued By**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025  
**Authority**: Executive Dispatch  
**Status**: 🚀 **TEAM ACTIVATED**

---

## EXECUTIVE ORDER

**Sprint 3 (Game Modes) is complete and approved.**

**Sprint 4 (Board System) is now ACTIVE.**

Board Engineering team is mobilized immediately to implement the visual board system and cell interaction layer. This team works in parallel with any remaining work, and prepares the foundation for UI integration in Sprint 5.

---

## TEAM ASSIGNMENT

**Team**: Board Engineering  
**Sprint**: Sprint 4 - Board System Implementation  
**Duration**: Immediate start, complete within sprint cycle  
**Deliverables**: 1,200+ lines production code + 22+ tests

---

## SPRINT 4 SCOPE

### Overview
Sprint 4 transforms the abstract board logic into a visual, interactive system:
- Display 12-cell board on screen
- Handle touch/mouse input
- Highlight valid moves
- Show chip placement/removal
- Visual feedback system

### Deliverables
1. **BoardGridManager** (~400 lines)
   - Orchestrate board display
   - Manage cell references
   - Handle cell input
   - Update visuals based on game state

2. **CellView** (~250 lines)
   - Individual cell representation
   - Highlight/selection states
   - Chip display
   - Color coding by player

3. **CellInputHandler** (~100 lines)
   - Touch/mouse input handling
   - Raycast to cells
   - Selection event firing

4. **Integration Layer** (~150 lines)
   - Connect with GameStateManager
   - Subscribe to board change events
   - Handle turn flow updates

5. **Test Suite** (~400 lines)
   - Unit tests (17+)
   - Integration tests (5+)
   - 100% pass rate target

---

## DETAILED TASKS

### TASK 1: Create BoardGridManager
**Time**: 6 hours  
**File**: `Assets/Scripts/Board/BoardGridManager.cs`  

**Requirements**:
- [ ] Initialize and create 12 cell views
- [ ] Grid layout (2 rows × 6 columns)
- [ ] Handle cell input/clicks
- [ ] Highlight valid move cells
- [ ] Update cell displays based on board state
- [ ] Track selected cell
- [ ] Integrate with GameStateManager
- [ ] 100% documented

**Key Methods**:
- `Initialize()` - Set up board
- `CreateCells()` - Instantiate all 12 cells
- `HighlightValidMoves(int[])` - Show valid moves
- `ClearHighlights()` - Remove highlights
- `UpdateCellDisplay(int, Player)` - Show chip
- `ClearCellDisplay(int)` - Remove chip
- `RefreshDisplay(BoardModel)` - Update from board state
- `OnCellSelected(int)` - Handle cell click

**Success Criteria**:
- Compiles with no errors
- Creates 12 cells correctly positioned
- Handles cell selection
- Updates display from board state

---

### TASK 2: Create CellView
**Time**: 4 hours  
**File**: `Assets/Scripts/Board/CellView.cs`  

**Requirements**:
- [ ] Individual cell representation
- [ ] Click/tap detection
- [ ] Highlight and selection states
- [ ] Chip display and color coding
- [ ] Fire OnCellClicked event
- [ ] 100% documented

**Key Methods**:
- `Initialize(int, BoardGridManager)` - Set up cell
- `SetHighlighted(bool)` - Show/hide highlight
- `SetSelected(bool)` - Show/hide selection
- `DisplayChip(Player)` - Show chip
- `ClearChip()` - Hide chip
- `OnCellClicked_Internal()` - Handle click

**Visual States**:
- Normal: White background
- Highlighted: Yellow background
- Selected: Cyan background
- Chip color: Player 1 = red, Player 2 = blue

**Success Criteria**:
- Compiles with no errors
- Responds to clicks
- Displays chips correctly
- Color changes work

---

### TASK 3: Create CellInputHandler
**Time**: 2 hours  
**File**: `Assets/Scripts/Board/CellInputHandler.cs`  

**Requirements**:
- [ ] Detect mouse/touch input
- [ ] Raycast to cell layer
- [ ] Identify which cell was clicked
- [ ] Notify BoardGridManager
- [ ] 100% documented

**Key Methods**:
- `Update()` - Check for input
- `HandleCellSelection()` - Process click

**Success Criteria**:
- Detects clicks on cells
- Correctly identifies cell index
- Notifies board manager

---

### TASK 4: GameStateManager Integration
**Time**: 4 hours  

**Requirements**:
- [ ] Verify integration points exist in GameStateManager
- [ ] Connect board change events
- [ ] Connect turn state events
- [ ] Handle cell selection flow
- [ ] Verify no modifications needed to Sprint 2 code
- [ ] Document integration

**Integration Points**:
1. **Board Change Events**
   ```csharp
   gameStateManager.OnBoardChanged += RefreshDisplay;
   ```

2. **Turn State Events**
   ```csharp
   gameStateManager.OnTurnStarted += OnTurnStarted;
   gameStateManager.OnTurnEnded += OnTurnEnded;
   ```

3. **Cell Selection**
   ```csharp
   boardManager.OnCellSelected(cellIndex);
   // → gameStateManager.ProcessCellSelection(cellIndex);
   ```

**Success Criteria**:
- No modifications to GameStateManager needed
- Board updates when state changes
- Cell selections update game state

---

### TASK 5: Unit Test Suite - BoardGridManager
**Time**: 6 hours  
**File**: `Assets/Scripts/Tests/BoardGridManagerTests.cs`  

**Test Coverage** (10+ tests):
- [ ] CreateCells generates 12 cells
- [ ] Cells positioned correctly
- [ ] HighlightValidMoves marks correct cells
- [ ] ClearHighlights removes all highlights
- [ ] UpdateCellDisplay shows chip
- [ ] ClearCellDisplay hides chip
- [ ] IsValidMove returns correct result
- [ ] GetCell returns correct cell view
- [ ] OnCellSelected updates selected index
- [ ] RefreshDisplay updates from board

**Test Organization**:
```
Setup
├─ Cell Creation Tests (3)
├─ Valid Move Tests (3)
├─ Display Update Tests (2)
├─ Input Handling Tests (2)
└─ State Management Tests (1)
```

---

### TASK 6: Unit Test Suite - CellView
**Time**: 4 hours  
**File**: `Assets/Scripts/Tests/CellViewTests.cs`  

**Test Coverage** (7+ tests):
- [ ] Initialize sets correct index
- [ ] SetHighlighted changes color
- [ ] SetSelected changes color
- [ ] DisplayChip shows chip display
- [ ] ClearChip hides chip display
- [ ] OnCellClicked fires event
- [ ] GetChipOwner returns correct player

---

### TASK 7: Integration Tests
**Time**: 4 hours  
**File**: `Assets/Scripts/Tests/BoardIntegrationTests.cs`  

**Test Coverage** (5+ tests):
- [ ] BoardGridManager integrates with GameStateManager
- [ ] Cell selection updates game state
- [ ] Board changes update display
- [ ] Valid moves highlighted correctly
- [ ] Invalid move shows feedback

---

### TASK 8: Code Review Preparation
**Time**: 2 hours  

**Checklist**:
- [ ] All code follows naming standards
- [ ] 100% documentation on public methods
- [ ] No magic numbers (use constants)
- [ ] Error handling complete
- [ ] No circular dependencies
- [ ] All tests pass (100%)
- [ ] Integration verified
- [ ] Ready for review

---

## TEAM STANDUP TEMPLATE

**Daily Standup (9 AM UTC)**

```
STANDUP REPORT - [Date]

✅ COMPLETED YESTERDAY:
- [Task name and completion status]

🔄 IN PROGRESS TODAY:
- [Current task]
- [Any blockers]

🚫 BLOCKERS:
- [Any blocking issues]
- [Help needed?]

📊 PROGRESS:
- Overall completion: X%
- Tests passing: Y/Z
```

---

## QUALITY GATES

### Acceptance Criteria
- ✅ All 12 cells display correctly
- ✅ Cell input detection works
- ✅ Valid moves highlight properly
- ✅ Chip placement updates display
- ✅ Chip removal (bumps) work
- ✅ No lag/performance issues
- ✅ All 22+ tests pass
- ✅ Code review approved
- ✅ Integration with GameStateManager verified
- ✅ Zero critical bugs

### Code Quality Standards
- ✅ PascalCase class names
- ✅ camelCase method/property names
- ✅ 100% public method documentation
- ✅ No magic numbers
- ✅ Error handling complete
- ✅ Organized file structure

---

## SUCCESS METRICS

| Metric | Target | Status |
|--------|--------|--------|
| Production LOC | 1,200+ | 🔄 |
| Unit Tests | 17+ | 🔄 |
| Integration Tests | 5+ | 🔄 |
| Test Pass Rate | 100% | 🔄 |
| Code Coverage | 80%+ | 🔄 |
| Documentation | 100% | 🔄 |

---

## CRITICAL PATH

```
Sprint 3 ✅ COMPLETE (signed off Nov 14)
    ↓
Sprint 4 🚀 ACTIVE (Nov 14 - kickoff)
├─ Day 1-2: BoardGridManager + CellView
├─ Day 3: Input handling + integration
├─ Day 4: Visual feedback + testing
└─ Day 5: Code review + sign-off
    ↓
Sprint 5 📅 QUEUED (UI Framework)
```

---

## MANAGING ENGINEER SUPPORT

**During Sprint 4**:
- ✅ Daily standup (9 AM UTC)
- ✅ Code review (< 4 hours)
- ✅ Blocker resolution (< 1 hour)
- ✅ Architecture guidance (available 24/6)

**Communication Channels**:
- `#board` - Team updates
- `#blockers` - Issues needing escalation
- Direct message for urgent items

---

## NEXT STEPS (For Board Team)

### Immediate
1. [ ] Read SPRINT_4_BRIEFING.md (complete requirements)
2. [ ] Review BoardGridManager specification
3. [ ] Review CellView specification
4. [ ] Identify any questions/clarifications

### Today
1. [ ] Begin BoardGridManager implementation
2. [ ] Create cell prefab (if needed)
3. [ ] Set up test structure

### This Sprint
1. [ ] Implement all board system components
2. [ ] Comprehensive testing (22+ tests)
3. [ ] Integration with GameStateManager
4. [ ] Code review and approval

---

## RESOURCES PROVIDED

- **SPRINT_4_BRIEFING.md** - Complete technical requirements
- **Project folder** - All code structure in place
- **Sprint 3 code** - Reference for integration patterns
- **Managing Engineer** - 24/6 support, < 1 hour blocker response

---

## EXPECTATIONS

### Code Quality
- Production-ready code
- 100% documentation
- Comprehensive testing
- Zero critical bugs

### Timeline
- Start immediately
- Daily standup reports
- Weekly progress tracking
- Completion within sprint cycle

### Communication
- Daily standup (mandatory)
- Blocker escalation (immediate)
- Progress updates (daily)
- Code reviews (as needed)

---

## AUTHORITY & APPROVAL

**Dispatch Authority**: Amp (Managing Engineer)  
**Approval Date**: Nov 14, 2025  
**Status**: ✅ **TEAM ACTIVATED & MOBILIZED**

---

## CONCLUSION

**Board Engineering team is authorized and mobilized for Sprint 4.**

All requirements clear. All resources provided. Full support from Managing Engineer.

**Begin implementation immediately.**

---

**Contact**: Amp (Managing Engineer)  
**Channel**: #board  
**Response Time**: < 1 hour for blockers

---

*End of Sprint 4 Dispatch Order*
