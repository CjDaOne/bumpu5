# Sprint 4 Kickoff - Board System Integration & Visualization

**Sprint Duration**: Week 4 (Dec 5 - Dec 12, 2025)  
**Lead**: Board Engineer Agent  
**Dependency**: Sprint 1-3 complete & approved  
**Goal**: Create an interactive, visual 12-cell board with click/tap detection and valid move highlighting  

---

## What We're Building

The visual representation of the game board—a 12-cell grid that players interact with. This bridges the pure C# game logic (Sprints 1-3) with Unity's visual system.

By end of this sprint, the board is fully interactive: players can see cells, select chips, see valid moves, and execute moves by tapping/clicking.

---

## Core Components

### 1. BoardGridManager.cs (New)

Main manager for the entire board system. Bridges GameStateManager and visual board.

```csharp
public class BoardGridManager : MonoBehaviour
{
    [SerializeField] private Transform boardParent;
    [SerializeField] private BoardCellView cellPrefab;
    [SerializeField] private ChipView chipPrefab;
    
    private BoardCellView[] cellViews;  // Visual cells
    private GameStateManager gameStateManager;
    
    // Initialization
    public void Initialize(GameStateManager gsm);
    
    // Visual updates
    public void UpdateBoardState(GameState state);
    public void HighlightValidMoves(List<int> validCells);
    public void ClearHighlights();
    public void PlaceChip(Chip chip, int cellIndex);
    public void MoveChipVisualized(int fromCell, int toCell, float duration);
    public void BumpChipAnimated(Chip chip, Vector3 targetPos);
    
    // Input handling
    private void HandleCellClicked(int cellIndex);
    private void HandleCellDragTo(int cellIndex);
    
    // Animation
    private void PlayChipDropAnimation(int cellIndex);
    private void PlayBumpAnimation(Chip chip);
}
```

---

### 2. BoardCellView.cs (New)

Visual representation of a single cell. Handles rendering and interaction.

```csharp
public class BoardCellView : MonoBehaviour
{
    [SerializeField] private int cellIndex;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Image highlightImage;
    [SerializeField] private Text cellNumberText;
    
    // Visual state
    private Color normalColor = Color.white;
    private Color highlightColor = Color.green;
    private Color ownerColor = Color.white;  // Player-specific color
    
    // Setup
    public void SetupCell(int index);
    public void SetOwner(Player owner);
    
    // Interaction
    public void OnClicked();
    public void OnPointerEnter();
    public void OnPointerExit();
    
    // Visuals
    public void Highlight(Color highlightColor);
    public void Unhighlight();
    public void SetOwnerVisualized(Player owner);
    public void PlaceChipView(ChipView chipView);
    public void RemoveChipView();
    
    // Getters
    public int CellIndex => cellIndex;
    public bool HasChip { get; set; }
}
```

---

### 3. ChipView.cs (New)

Visual representation of a single chip. Handles visuals and animation.

```csharp
public class ChipView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int chipID;
    [SerializeField] private Color player1Color = Color.red;
    [SerializeField] private Color player2Color = Color.blue;
    
    private Chip chip;  // Reference to model
    private BoardCellView currentCell;
    private CanvasGroup canvasGroup;  // For fade animations
    
    // Setup
    public void Initialize(Chip modelChip, Player owner);
    
    // Movement
    public IEnumerator AnimateMovement(Vector3 targetPos, float duration);
    public IEnumerator AnimateBump(Vector3 bouncePos);
    public IEnumerator AnimateFadeOut();
    public IEnumerator AnimateFadeIn();
    
    // State
    public void SetActive(bool active);
    public void SetSelected(bool selected);
    
    // Getters
    public Chip ModelChip => chip;
    public BoardCellView CurrentCell => currentCell;
}
```

---

### 4. BoardInputHandler.cs (New)

Handles all user input for board interaction (click/drag/tap).

```csharp
public class BoardInputHandler : MonoBehaviour
{
    [SerializeField] private BoardGridManager boardManager;
    [SerializeField] private GraphicRaycaster raycaster;
    
    private int selectedCellIndex = -1;
    private List<int> validMoveCells;
    
    // Events for GameStateManager
    public event System.Action<int, int> OnMovementRequested;  // from, to
    public event System.Action<int> OnBumpRequested;           // target
    
    // Input processing
    private void ProcessMouseInput();
    private void ProcessTouchInput();
    
    // Click/Tap detection
    private int GetCellIndexAtPointer();
    private void SelectCell(int cellIndex);
    private void DeselectCell();
    
    // Validation
    private bool IsValidMove(int fromCell, int toCell);
}
```

---

### 5. BoardLayoutConfiguration.cs (New)

Data-driven configuration for board layout and appearance.

```csharp
[System.Serializable]
public class BoardLayoutConfiguration : ScriptableObject
{
    [Header("Cell Setup")]
    [SerializeField] public float cellRadius = 50f;
    [SerializeField] public Vector2[] cellPositions = new Vector2[12];
    [SerializeField] public int[] cellNumbers = new int[12];
    
    [Header("Colors")]
    [SerializeField] public Color player1Color = Color.red;
    [SerializeField] public Color player2Color = Color.blue;
    [SerializeField] public Color highlightColor = Color.green;
    [SerializeField] public Color validMoveColor = new Color(0, 1, 0, 0.5f);
    
    [Header("Animation")]
    [SerializeField] public float chipDropDuration = 0.3f;
    [SerializeField] public float chipBumpDuration = 0.4f;
    [SerializeField] public float moveAnimationDuration = 0.5f;
    
    // Methods to get data
    public Vector2 GetCellPosition(int cellIndex);
    public Color GetPlayerColor(int playerIndex);
}
```

---

## Board Layout Design

### Recommended 12-Cell Circular Layout
```
           Cell 0
      11 ◄─────► 1
    10            2
   9   ┌────┐     3
   │   │BOARD│   │
   8   └────┘     4
    7            5
      6  ◄─────►  5
```

**Implementation Note**: Create board art asset showing 12 cells arranged in a circle (or chosen layout). The `BoardLayoutConfiguration` ScriptableObject will define the exact positions.

---

## File Structure (Created in Sprint 4)

```
/Assets/Scripts/Board/
  BoardGridManager.cs           (NEW - orchestrator)
  BoardCellView.cs              (NEW - single cell visual)
  ChipView.cs                   (NEW - chip visual)
  BoardInputHandler.cs          (NEW - input processing)
  BoardLayoutConfiguration.cs   (NEW - data config)

/Assets/Prefabs/Board/
  Cell.prefab                   (NEW - BoardCellView prefab)
  Chip.prefab                   (NEW - ChipView prefab)
  Board.prefab                  (NEW - complete board)

/Assets/Scenes/
  Gameplay.unity                (NEW - main game scene)

/Assets/Art/Board/
  board-background.png          (To be created by artist)
  cell-indicator.png            (To be created by artist)
  grid-overlay.png              (To be created by artist)

/Assets/Resources/
  BoardLayout.asset             (ScriptableObject config)
```

---

## Interaction Flow

```
Player taps cell
    ↓
BoardInputHandler detects input
    ↓
Get cell index at pointer
    ↓
Call GameStateManager.ValidateMove(fromCell, toCell)
    ↓
If valid: Request move
    ↓
GameStateManager updates GameState
    ↓
BoardGridManager listens to GameState change
    ↓
Animate chip movement
    ↓
Update cell visuals
```

---

## Unit Tests Required

### BoardCellViewTests.cs
```
- SetupCell_SetsCorrectIndex()
- Highlight_ChangesColor()
- Unhighlight_RestoresColor()
- PlaceChipView_UpdatesHasChip()
- SetOwner_ChangesOwnerColor()
```

### ChipViewTests.cs
```
- Initialize_SetsCorrectPlayer()
- SetActive_TogglesVisibility()
- SetSelected_HighlightsChip()
- AnimateMovement_ReachesTargetPosition()
- AnimateBump_ReturnsToStartPosition()
```

### BoardInputHandlerTests.cs
```
- GetCellIndexAtPointer_WithValidCell_ReturnsIndex()
- GetCellIndexAtPointer_WithInvalidPointer_ReturnsNegative()
- SelectCell_WithValidCell_SetsSelection()
- IsValidMove_WithValidMove_ReturnsTrue()
- OnMovementRequested_FiredCorrectly()
```

### BoardGridManagerTests.cs
```
- Initialize_CreatesAllCells()
- PlaceChip_PositionsChipCorrectly()
- HighlightValidMoves_HighlightsCorrectCells()
- UpdateBoardState_ReflectsGameState()
- MoveChipVisualized_AnimatesCorrectly()
```

---

## Integration Points

### With Sprints 1-3 (Game Logic)
- BoardGridManager listens to GameStateManager events
- Board state is queried from GameState object
- No modification to core logic needed

### With Sprint 5 (UI)
- HUDManager will have references to BoardGridManager
- Input events may be routed through UIManager
- Disable/enable board based on game phase

### With Sprint 7 (Mobile)
- BoardInputHandler adapted for touch (already supports)
- Safe area margins applied to board
- Resolution scaling handled by Canvas

---

## Success Criteria

✅ All 12 cells visible and positioned correctly  
✅ Chips can be placed and moved visually  
✅ Valid moves highlighted when selected  
✅ Click/tap detection working accurately  
✅ Smooth animations (not jarring)  
✅ All 15+ unit tests pass  
✅ Code follows CODING_STANDARDS.md  
✅ Full documentation  
✅ Board responsive to GameStateManager changes  
✅ No logic errors (logic stays in Sprints 1-3)  

---

## Performance Targets

- Cell click detection: < 1ms
- Move animation: Smooth (60 FPS)
- Board state update: < 10ms
- Memory: Minimal (reuse cells for all games)

---

## Artist Assets Needed

**Board Art**:
- board-background.png (main board image)
- cell-indicator.png (highlight overlay for cells)
- grid-overlay.png (optional grid visual)

**Chip Art**:
- chip-red.png (Player 1 chip)
- chip-blue.png (Player 2 chip)

**UI for Board**:
- selection-ring.png (indicate selected cell)
- highlight-glow.png (valid move indicator)

---

## Next Sprint Preview (Sprint 5)

Sprint 5 will add the HUD and menus that interact with this board:
- Dice roll button (shows board after roll)
- BUMP button (highlight bumpable cells)
- Valid moves list
- Mode-specific UI elements

---

**Sprint Start Date**: Dec 5, 2025  
**Estimated Completion**: Dec 12, 2025  
**Owner**: Board Engineer Agent  
**Dependency**: Sprint 1-3 approved ✅
