# Board Architecture
**Created**: Nov 14, 2025  
**Owner**: Board Engineer  
**Status**: ACTIVE

---

## Overview

The Board System is a Unity-based 12-cell interactive grid that synchronizes with the game logic (GameStateManager). It handles cell state visualization, chip placement, valid move highlighting, and input detection for all platforms.

---

## Grid System

### Cell Layout (12-Cell Linear)
```
[1] [2] [3] [4] [5] [6]
[7] [8] [9] [10] [11] [12]
```

### Dimensions
- **Cell Count**: 12 cells (indices 0-11 or 1-12)
- **Cell Size**: 100×100px (configurable)
- **Spacing**: 8px gap between cells
- **Board Total**: 612×212px (calculated: 6×100 + 5×8, 2×100 + 1×8)

### Cell Positions (World Coordinates)
- **Cell 1**: (0, 0)
- **Cell 2**: (108, 0)
- **Cell 3**: (216, 0)
- **Cell 4**: (324, 0)
- **Cell 5**: (432, 0)
- **Cell 6**: (540, 0)
- **Cell 7**: (0, 108)
- **Cell 8**: (108, 108)
- **Cell 9**: (216, 108)
- **Cell 10**: (324, 108)
- **Cell 11**: (432, 108)
- **Cell 12**: (540, 108)

### Board Origin
- Centered on parent canvas
- Anchored at top-left of grid
- Scales with canvas resolution

---

## Cell Data Model

### Cell State (Per Cell)
```csharp
enum CellState {
  Empty = 0,
  Player1_Chip = 1,
  Player2_Chip = 2,
  Player3_Chip = 3,
  Player4_Chip = 4
}
```

### Cell Component Data
- **Index**: 0-11 (cell position in grid)
- **Owner**: Which player's chip (if any)
- **Highlighted**: Is this cell a valid move?
- **Selected**: Is this cell currently selected?
- **Occupied**: Bool (owned by any player)

### Valid Move Calculation
```
Input: Current player position, dice roll value
Process:
  1. Calculate target = current_position + roll
  2. If target > 12: Cannot move this chip (too far)
  3. If target in [1-12]: Valid
  4. Check if target occupied: 
     - If own chip: Invalid (can't land on self)
     - If opponent chip: Valid (bump)
  5. Mark target as valid move
Output: List of valid cells to highlight
```

---

## BoardGridManager (Orchestrator)

### Responsibilities
- Initialize 12 cell prefabs
- Maintain cell references (array or list)
- Update cell visuals based on GameStateManager events
- Handle valid move highlighting
- Route tap/click input to game logic
- Manage chip placement animations

### Key Methods
```csharp
// Initialization
void Initialize()
void SpawnCells()

// Visual Updates
void UpdateCellState(int cellIndex, CellState newState)
void HighlightValidMoves(List<int> validCells)
void ClearHighlights()

// Chip Management
void PlaceChip(int cellIndex, int playerId)
void RemoveChip(int cellIndex)
void MoveChip(int fromCell, int toCell)

// Input
void OnCellTapped(int cellIndex)

// Animations
void AnimateChipMove(int fromCell, int toCell, float duration)
void AnimateChipBump(int targetCell)
```

### Events Subscribed To
```
GameStateManager.OnGameStateChanged
  └─ Update all cell visuals

GameStateManager.OnMoveMade
  └─ Animate chip movement

GameStateManager.OnBumpOccurred
  └─ Animate bump effect
```

---

## Integration with GameStateManager

### State Synchronization
1. Game starts: BoardGridManager listens to GameStateManager
2. GameStateManager broadcasts current board state
3. BoardGridManager receives state, initializes cell visuals
4. On each move: GameStateManager updates state, broadcasts
5. BoardGridManager receives update, animates transition

### Move Validation Flow
```
Player taps cell → BoardGridManager.OnCellTapped(index)
  ├─ Check: Is this a valid move?
  ├─ If NO: Show invalid feedback, return
  └─ If YES: Call GameStateManager.ExecuteMove(index)
      ├─ GameStateManager validates game logic
      ├─ Updates board state
      ├─ Broadcasts OnMoveMade event
      └─ BoardGridManager receives & animates
```

### Board Update on Action
1. Player rolls dice: GameStateManager.RollDice()
2. GameState updates available moves
3. GameStateManager broadcasts OnDiceRolled
4. BoardGridManager receives event
5. BoardGridManager calls GetValidMoves() from GameStateManager
6. Highlights valid cells
7. Player taps cell → move executed (above flow)

---

## Cell Prefab Structure

### Prefab Hierarchy
```
CellView (Prefab)
├─ SpriteRenderer (background, cell visual)
├─ CanvasGroup (for fade/opacity during highlight)
├─ Animator (for highlight animation)
├─ BoxCollider2D (for tap detection)
├─ ChipView (child, instantiated when chip placed)
│  ├─ SpriteRenderer (chip visual)
│  ├─ Animator (chip animations)
│  └─ ParticleSystem (optional: bump effect)
└─ HighlightVisual (child, shows when valid move)
   ├─ Image (glowing ring or background tint)
   └─ Animator (pulse animation)
```

### CellView Component
```csharp
public class CellView : MonoBehaviour {
  public int CellIndex { get; set; }
  
  private SpriteRenderer cellBg;
  private CanvasGroup canvasGroup;
  private Animator animator;
  private ChipView occupiedChip;
  private Image highlightImage;
  
  public void SetState(CellState state)
  public void Highlight()
  public void UnHighlight()
  public bool IsOccupied()
  public ChipView GetChip()
}
```

---

## Highlight States

### Highlight Types
1. **None**: Cell not highlighted, normal appearance
2. **Valid Move**: Cell is a valid move (bright outline, pulse animation)
3. **Selected**: Cell is currently selected (solid color, scale up)
4. **Invalid**: Cell was tapped but invalid (flash red briefly)

### Valid Move Visual
- **Color**: Bright green (#43A047) or primary blue (#1E88E5)
- **Animation**: Pulse (scale 1.0 → 1.1 → 1.0), duration 600ms, loop
- **Opacity**: 80% (visible but not overwhelming)
- **Effect**: Glow ring around cell border

### Selected Cell Visual
- **Color**: Bright yellow or primary color
- **Animation**: Scale slightly larger (1.05x)
- **Border**: 3px outline
- **Effect**: Solid, not pulsing

### Invalid Feedback
- **Color**: Red (#E53935)
- **Animation**: Flash twice (0ms → 1.0 opacity → 100ms → 0.3 opacity → 100ms → 1.0)
- **Duration**: 200ms total
- **Sound**: Negative beep (optional)

---

## Chip Visuals & Placement

### Chip Appearance
- **Size**: 80×80px (fits in 100px cell with 10px margin)
- **Color**: Player-specific color (Player 1 red, Player 2 blue, etc.)
- **Border**: 2px dark outline for depth
- **Shadow**: Subtle shadow for elevation effect

### Chip States
- **Idle**: Normal appearance
- **Moving**: Animated along path
- **Bumped**: Eject animation, exit board
- **Exiting**: Move off board animation

### Chip Placement Logic
```
When cell.SetOccupied(playerId):
  1. Check if chip already in cell
  2. If yes: Error (shouldn't happen)
  3. If no: Instantiate ChipView prefab
  4. Position chip center in cell
  5. Set player color
  6. Add to cell's transform (as child)
```

---

## Material & Sprite Requirements

### Cell Sprites
- **Empty Cell**: Light gray background (#F5F5F5)
- **Highlighted Cell**: Bright color (#43A047 or #1E88E5)
- **Selected Cell**: Bold color, high contrast
- **Border**: 1px gray (#BDBDBD)

### Chip Sprites
- **Player 1**: Red (#D32F2F)
- **Player 2**: Blue (#1976D2)
- **Player 3**: Green (#388E3C)
- **Player 4**: Orange (#F57C00)

### Materials
- **Default**: Standard Unity sprite material
- **Highlight**: Additive material (glowing effect)
- **Disabled**: Grayscale material (dim appearance)

---

## Animation Clips

### Highlight Enter Animation
- **Name**: `Cell_HighlightEnter`
- **Duration**: 250ms
- **Action**: Scale up slightly, fade in glow
- **Target**: Highlight visual image

### Highlight Exit Animation
- **Name**: `Cell_HighlightExit`
- **Duration**: 250ms
- **Action**: Scale down, fade out glow
- **Target**: Highlight visual image

### Chip Move Animation
- **Name**: `Chip_Move`
- **Duration**: 300ms
- **Path**: Straight line from source cell to target cell
- **Easing**: Ease-out (smooth landing)

### Chip Bump Animation
- **Name**: `Chip_Bump`
- **Duration**: 400ms
- **Path**: Eject from board (increase size, move off-screen)
- **Effect**: Particle burst on impact
- **Sound**: Bump sound effect

### Pulse Animation (Idle)
- **Name**: `Highlight_Pulse`
- **Duration**: 600ms
- **Action**: Scale 1.0 → 1.1 → 1.0, loop
- **Target**: Highlight ring image
- **Loop**: Yes, continuous

---

## Component Relationships

### Dependencies
```
BoardGridManager
├─ Knows about: GameStateManager
├─ Controls: CellView array
├─ Listens to: GameStateManager events
└─ Publishes: OnCellTapped event

CellView
├─ Knows about: ChipView (if occupied)
├─ Controls: SpriteRenderer, Animator, BoxCollider
├─ Listens to: BoardGridManager calls
└─ Publishes: OnTapped event

ChipView
├─ Knows about: Player ID, cell index
├─ Controls: Sprite, Animator, Particle effects
└─ Publishes: Animation completion events
```

### Communication Pattern
```
User Input (tap)
  ↓
CellView.OnTapped()
  ↓
BoardGridManager.OnCellTapped(cellIndex)
  ↓
GameStateManager.ExecuteMove(cellIndex)
  ↓
GameStateManager broadcasts OnMoveMade
  ↓
BoardGridManager receives & animates
  ↓
ChipView animate movement
  ↓
Animation completes
```

---

## Scalability & Responsiveness

### Canvas Scaling
- Design resolution: 1920×1080
- Board scales with canvas
- Safe area: 10% margins
- Mobile: Board size reduced, touch targets remain >= 44px

### Cell Responsiveness
- Cells scale proportionally with board
- Minimum cell size: 60×60px (mobile)
- Maximum cell size: 120×120px (large displays)
- Touch target always >= 44px

---

## Related Documents
- INPUT_HANDLING.md
- ASSET_INTEGRATION.md
- SPRINT_4_KICKOFF.md

---

**Status**: Complete - Production Ready
