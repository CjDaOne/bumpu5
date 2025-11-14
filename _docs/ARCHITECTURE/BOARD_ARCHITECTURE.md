# Board Architecture

**Created**: Nov 14, 2025  
**Owner**: Board Engineer  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Overview

The Board System is the core visual and interactive layer that represents the game state on screen. It manages a 12-cell grid, cell state display, piece visualization, and user input handling. The BoardGridManager orchestrates all board operations and communicates bi-directionally with GameStateManager.

---

## Grid System

### Cell Layout (12 Cells)

The board uses a classic Pachisi/Sorry-style circular layout with 12 cells arranged in a square pattern:

```
        Cell 0   Cell 1   Cell 2
        
        Cell 11             Cell 3
        
        
        Cell 10             Cell 4
        
        Cell 9   Cell 8   Cell 7   Cell 6   Cell 5
```

**Layout Details**:
- Top row: 3 cells (0, 1, 2)
- Right column: 3 cells (3, 4, 5)
- Bottom row: 5 cells (6, 7, 8, 9, 10)
- Left column: 2 cells (11, extended down)

### Cell Positioning

**Canvas-based positioning** (RectTransform):
- Reference resolution: 1080 × 1920
- Board area: 800 × 800 pixels (centered in 1080 × 1920)
- Cell size: Approximately 120 × 120 pixels with spacing
- Spacing between cells: 20 pixels

**Cell Indices**:
```
Cell 0:  (100, 100)   - Top left
Cell 1:  (450, 100)   - Top center
Cell 2:  (800, 100)   - Top right
Cell 3:  (1000, 300)  - Right upper
Cell 4:  (1000, 650)  - Right lower
Cell 5:  (1000, 1000) - Bottom right corner
Cell 6:  (800, 1000)  - Bottom right
Cell 7:  (450, 1000)  - Bottom center
Cell 8:  (100, 1000)  - Bottom left
Cell 9:  (50, 700)    - Left lower
Cell 10: (50, 350)    - Left upper
Cell 11: (100, 100)   - Top left corner extended
```

### Board Dimensions

**Outer Dimensions**: 1020 × 1100 pixels (12 cells + borders)  
**Cell Size**: 120 × 120 pixels  
**Padding**: 10px around each cell (for highlighting, animations)  
**Total Width**: 800px (with margins)  
**Total Height**: 900px (with margins)

---

## Cell Data Structure

### Cell State Machine

Each cell has a discrete state based on occupancy:

```
State: EMPTY
├─ No chips on this cell
├─ Visual: Empty cell sprite
└─ Interaction: Selectable (if valid move)

State: HAS_CHIP_PLAYER_1
├─ One chip from Player 1
├─ Visual: Cell with Player 1 chip (red)
└─ Interaction: Selectable (if valid move)

State: HAS_CHIP_PLAYER_2
├─ One chip from Player 2
├─ Visual: Cell with Player 2 chip (blue)
└─ Interaction: Selectable (if valid move)

State: HAS_CHIP_PLAYER_3
├─ One chip from Player 3
├─ Visual: Cell with Player 3 chip (yellow)
└─ Interaction: Selectable (if valid move)

State: HAS_CHIP_PLAYER_4
├─ One chip from Player 4
├─ Visual: Cell with Player 4 chip (green)
└─ Interaction: Selectable (if valid move)

State: HAS_MULTIPLE_CHIPS
├─ Multiple chips on this cell (rare, game-specific)
├─ Visual: Cell with count badge
└─ Interaction: Non-selectable (blocked)
```

### Cell Highlight States

```
HighlightState: NONE
├─ No highlight applied
├─ Opacity: 1.0
└─ Color: Normal

HighlightState: VALID_MOVE
├─ Player can move to this cell
├─ Opacity: 0.7 (additive)
├─ Color: Light blue (#DBEAFE)
└─ Animation: Pulsing (0.6 to 0.9 opacity, 1s loop)

HighlightState: SELECTED
├─ Player has selected this cell
├─ Opacity: 1.0
├─ Color: Medium blue (#BFDBFE)
├─ Animation: None (static)
└─ Border: 3px #2563EB outline

HighlightState: INVALID_MOVE
├─ Player attempted invalid move
├─ Opacity: 1.0
├─ Color: Red flash (#FEE2E2) for 200ms, then normal
└─ Animation: Flash (once, no loop)
```

---

## BoardGridManager

### Responsibilities

1. **Initialization**
   - Create 12 cell prefab instances
   - Position cells in grid layout
   - Initialize cell states to EMPTY
   - Set up cell references array

2. **State Synchronization**
   - Receive cell state updates from GameStateManager
   - Update visual representation immediately
   - Trigger animations on state changes

3. **Input Handling**
   - Detect taps/clicks on cells
   - Validate moves against GameStateManager
   - Highlight valid moves
   - Provide feedback on invalid moves

4. **Animation Management**
   - Animate chip movement between cells
   - Animate bump effects
   - Animate win celebration
   - Queue and manage animations

5. **Event Broadcasting**
   - Notify GameStateManager of cell selection
   - Report tap detection results
   - Confirm animation completion

### Class Structure

```csharp
public class BoardGridManager : MonoBehaviour
{
    // References
    [SerializeField] private CellView[] cells = new CellView[12];
    private GameStateManager gameStateManager;
    
    // State
    private BoardState currentBoardState;
    private List<int> validMoveCells;
    
    // Events
    public event System.Action<int> OnCellSelected;
    public event System.Action<int, int> OnMoveExecuted;
    
    // Lifecycle
    public void Initialize(GameStateManager gsm);
    public void UpdateBoardState(BoardState newState);
    
    // Input
    public void OnCellTapped(int cellIndex);
    public void SetValidMoves(List<int> validCells);
    
    // Animation
    public void AnimateMoveChip(int fromCell, int toCell);
    public void AnimateBump(int targetCell);
    public void AnimateWin(int winningPlayer);
    
    // Queries
    public BoardState GetCurrentState();
    public bool IsCellEmpty(int cellIndex);
    public int GetChipPlayer(int cellIndex);
}
```

---

## Cell Prefab Structure

```
CellView (GameObject)
│
├─ BackgroundImage (Image)
│  ├─ Color: White (#FFFFFF)
│  ├─ Sprite: Cell_Empty
│  └─ RectTransform: 120 × 120
│
├─ ChipImage (Image - conditional, child of BackgroundImage)
│  ├─ Color: Player color (red, blue, etc.)
│  ├─ Sprite: Chip sprite
│  ├─ RectTransform: 100 × 100 (centered)
│  └─ Initially: Disabled (shows when cell has chip)
│
├─ HighlightOverlay (Image - conditional)
│  ├─ Color: Highlight color (light blue, etc.)
│  ├─ Opacity: 0.5 (additive blending)
│  ├─ RectTransform: 130 × 130 (slightly larger for glow)
│  └─ Initially: Disabled (shows for valid moves)
│
├─ SelectionBorder (Image)
│  ├─ Color: Blue (#2563EB)
│  ├─ Sprite: Border (3px width)
│  ├─ RectTransform: 130 × 130
│  └─ Initially: Disabled (shows when selected)
│
├─ Button (Button component)
│  ├─ Interactable: true (updated by BoardGridManager)
│  └─ OnClick: CellView.OnTapped()
│
└─ CellView (MonoBehaviour script)
   ├─ cellIndex: int
   ├─ currentState: CellState
   ├─ OnTapped(): void
   ├─ SetState(CellState): void
   ├─ SetHighlight(HighlightState): void
   └─ AnimateChipArrival(): IEnumerator
```

---

## Integration with GameStateManager

### Data Flow

```
Player taps cell
    ↓
CellView.OnTapped(cellIndex)
    ↓
BoardGridManager.OnCellTapped(cellIndex)
    ↓
GameStateManager.ValidateMove(cellIndex)
    ↓
GameStateManager updates BoardState
    ↓
BoardGridManager.UpdateBoardState(newState)
    ↓
Update all cell visuals + animations
    ↓
Feedback to player (success or error)
```

### Event Integration

```csharp
// BoardGridManager subscribes to GameStateManager
GameStateManager.OnGameStateChanged += UpdateBoardDisplay;
GameStateManager.OnMoveExecuted += AnimateMoveAndUpdate;
GameStateManager.OnBumpDetected += AnimateBumpEffect;
GameStateManager.OnValidMovesChanged += HighlightValidMoves;
GameStateManager.OnPhaseChanged += UpdateInteractivity;

// BoardGridManager notifies GameStateManager
OnCellSelected += GameStateManager.ProcessMoveAttempt;
OnMoveAnimationComplete += GameStateManager.ConfirmMoveExecution;
```

---

## Valid Move Highlighting

### Process

1. **Dice Roll Complete**
   - GameStateManager calculates valid destination cells
   - Broadcasts `OnValidMovesChanged` event
   
2. **HighlightValidMoves()**
   - BoardGridManager receives valid cell indices
   - Sets each valid cell to `HighlightState.VALID_MOVE`
   - Enables pulsing animation on each cell
   
3. **Visual Feedback**
   - Valid cells glow light blue with pulsing animation
   - All other cells remain normal
   
4. **Player Selects Cell**
   - If valid: Execute move, animate chip movement
   - If invalid: Flash red, show error message
   
5. **Move Completes**
   - Clear all highlights
   - Update cell states
   - Next player's turn

---

## Chip Animation System

### Movement Animation

**Duration**: 300ms  
**Easing**: EaseInOutCubic  
**Process**:
1. Start position: Source cell center
2. End position: Destination cell center
3. Path: Linear (straight line)
4. Completion: Snap to destination, confirm to GameStateManager

### Bump Animation

**Duration**: 200ms  
**Easing**: EaseOutBounce  
**Process**:
1. Target chip scales up (1.0 → 1.15) for 100ms
2. Target chip moves slightly (jump back)
3. Shrink back to 1.0 over 100ms
4. Trigger GameStateManager to move bumped chip to home

### Win Animation

**Duration**: 1000ms  
**Easing**: Custom spring curve  
**Process**:
1. All chips on winning player scale up (1.0 → 1.3)
2. Spin animation (360° rotation) over 800ms
3. Confetti particle effect (if supported)
4. Show "You Won!" modal after animation

---

## Prefab Hierarchy (Scene)

```
Canvas (ScreenSpace - Overlay)
│
└─ BoardPanel (Panel, centered)
   │
   ├─ BoardBackground (Image)
   │  ├─ Sprite: Board_Background (or color)
   │  └─ RectTransform: 900 × 1000
   │
   ├─ GridContainer (empty transform)
   │  └─ RectTransform: Anchor to center
   │
   │  ├─ Cell_0 (CellView prefab instance)
   │  ├─ Cell_1 (CellView prefab instance)
   │  ├─ Cell_2 (CellView prefab instance)
   │  ├─ Cell_3 (CellView prefab instance)
   │  ├─ Cell_4 (CellView prefab instance)
   │  ├─ Cell_5 (CellView prefab instance)
   │  ├─ Cell_6 (CellView prefab instance)
   │  ├─ Cell_7 (CellView prefab instance)
   │  ├─ Cell_8 (CellView prefab instance)
   │  ├─ Cell_9 (CellView prefab instance)
   │  ├─ Cell_10 (CellView prefab instance)
   │  └─ Cell_11 (CellView prefab instance)
   │
   ├─ AnimationLayer (empty transform, for temp animations)
   │  └─ Dynamic floating chips during animation
   │
   └─ ParticleEffects (empty transform)
      ├─ BumpEffectPool (ParticleSystem)
      └─ WinEffectPool (ParticleSystem)
```

---

## Component Relationships

### MonoBehaviours
1. **BoardGridManager** - Master orchestrator
2. **CellView** - Individual cell behavior
3. **ChipAnimator** - Handles movement/bump animations
4. **BoardInputHandler** - Tap detection layer

### Communication Flow
```
CellView (tapped) 
→ BoardGridManager.OnCellTapped()
→ GameStateManager.ProcessMove()
→ GameStateManager.OnMoveExecuted event
→ BoardGridManager.AnimateMoveChip()
→ ChipAnimator.PlayAnimation()
→ GameStateManager.ConfirmExecution()
```

---

## Testing Checklist

- [ ] All 12 cells render correctly
- [ ] Cells position in correct layout
- [ ] Cell states update visually
- [ ] Valid move highlighting works
- [ ] Tap detection registers correctly
- [ ] Invalid moves show feedback
- [ ] Movement animation smooth (300ms)
- [ ] Bump animation plays correctly
- [ ] Animations can be interrupted
- [ ] Safe area handling on mobile
- [ ] Board scales properly on all devices
- [ ] No performance issues (60 FPS)

---

## Related Documents

- INPUT_HANDLING.md
- ASSET_INTEGRATION.md
- SPRINT_4_BOARD_PREP.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Implementation
