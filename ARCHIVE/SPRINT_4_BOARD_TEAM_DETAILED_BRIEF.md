# SPRINT 4 - BOARD ENGINEERING TEAM DETAILED BRIEF
## Board Visualization & Interactive System Implementation

**Issued By**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025  
**Duration**: 5 days (immediate start)  
**Team**: Board Engineering  
**Target**: Complete visual board system with 1,200+ lines production code + 22+ tests

---

## EXECUTIVE SUMMARY

Sprint 4 transforms the abstract game logic into a visual, interactive 12-cell board that players can see and interact with. This is the visual foundation that connects game logic (Sprints 1-3) to UI (Sprint 5).

**Scope**: Build all board visualization, cell interaction, chip visuals, and move highlighting.

**Deliverables**: 
- BoardGridManager (~400 lines)
- CellView (~250 lines)
- ChipVisualizer (~300 lines)
- ValidMoveHighlighter (~200 lines)
- 22+ comprehensive tests

**Success**: Board fully interactive, visually polished, integrated with game logic.

---

## PART 1: REQUIREMENTS OVERVIEW

### Board Layout (12 Cells)

The board is a circular/hexagonal arrangement of 12 cells:

```
         [0]
     [11]   [1]
   [10]       [2]
 [9]             [3]
   [8]         [4]
     [7]     [5]
        [6]
```

Each cell can contain:
- Empty (no chip)
- Player 1 chip
- Player 2 chip
- Player 3 chip (some modes)
- Player 4 chip (some modes)

### Visual Requirements

1. **Cell Display**
   - Clear, distinguishable cells on screen
   - Show player color for occupied cells
   - Show chip visual (player color + number if multi-chip)
   - Highlight valid moves (bright color)
   - Show selection state (border/glow)
   - Smooth animations on changes

2. **Interaction Points**
   - Tap/click to place chip on cell
   - Hover to show valid move preview
   - Long-press to show cell info (optional)
   - Touch targets ≥ 44px (mobile requirement)

3. **Visual Feedback**
   - Cell highlight on hover
   - Smooth placement animation
   - Bump animation (chip bounces/disappears)
   - Win animation (special effects)
   - State changes (normal → highlighted → selected)

4. **Responsive Design**
   - Works on mobile phones (various sizes)
   - Works on tablets
   - Works on desktop/web
   - Scales gracefully to screen size

---

## PART 2: FILE STRUCTURE

Create these files in Assets/Scripts/Board/:

```
Assets/Scripts/Board/
  ├── BoardGridManager.cs          (~400 lines)
  ├── CellView.cs                  (~250 lines)
  ├── ChipVisualizer.cs            (~300 lines)
  ├── ValidMoveHighlighter.cs      (~200 lines)
  └── BoardVisualConfig.cs         (~50 lines - configuration)

Assets/Scripts/Tests/
  ├── BoardGridManagerTests.cs     (~350 lines, 8+ tests)
  ├── CellViewTests.cs             (~250 lines, 6+ tests)
  ├── ChipVisualizerTests.cs       (~300 lines, 5+ tests)
  └── BoardIntegrationTests.cs     (~200 lines, 3+ integration tests)
```

---

## PART 3: BoardGridManager (400 lines)

**Purpose**: Master controller for the visual board system. Manages board layout, cell references, input handling, and state updates.

### Class Structure

```csharp
public class BoardGridManager : MonoBehaviour
{
    // INSPECTOR PROPERTIES
    [SerializeField] private Transform boardParent;
    [SerializeField] private CellView cellPrefab;
    [SerializeField] private Vector2 boardCenterPosition;
    [SerializeField] private float cellRadius; // Distance from center
    [SerializeField] private float cellSize;
    
    // INTERNAL STATE
    private CellView[] cells = new CellView[12];
    private GameStateManager gameStateManager;
    private ValidMoveHighlighter moveHighlighter;
    
    // EVENTS
    public event System.Action<int> OnCellSelected; // Cell index
    public event System.Action<CellView> OnCellHovered;
    
    // LIFECYCLE
    public void Initialize(GameStateManager stateManager);
    public void Shutdown();
    
    // BOARD SETUP
    private void CreateBoardLayout();
    private void PositionCells();
    private Vector3 GetCellPosition(int cellIndex);
    
    // INPUT HANDLING
    private void HandleCellClicked(int cellIndex);
    private void HandleCellHovered(int cellIndex);
    private void HandleCellExited(int cellIndex);
    
    // STATE UPDATES
    public void UpdateCellDisplay(int cellIndex, Player occupant);
    public void UpdateAllCells(BoardModel board);
    public void ClearBoard();
    
    // VALID MOVES
    public void ShowValidMoves(int[] validCells);
    public void ClearValidMoves();
    
    // CHIP ANIMATIONS
    public void AnimateChipPlacement(int cellIndex, Player player);
    public void AnimateChipBump(int cellIndex);
    public void AnimateWin(Player winningPlayer);
    
    // GAME STATE INTEGRATION
    private void OnGameStateChanged(GamePhase newPhase);
    private void OnChipPlaced(int cellIndex, Player player);
    private void OnChipBumped(int cellIndex);
    private void OnPlayerChanged(Player newPlayer);
}
```

### Key Responsibilities

1. **Board Creation**
   - Instantiate 12 cell prefabs in circular layout
   - Position cells correctly
   - Set up references

2. **Input Management**
   - Detect which cell was clicked/tapped
   - Handle hover effects
   - Prevent invalid selections

3. **State Display**
   - Update cell visuals when board changes
   - Show/hide chips as placed/removed
   - Highlight valid moves
   - Show current player indicator

4. **Event Integration**
   - Listen to GameStateManager events
   - Update board on phase changes
   - React to chip placement/bumping
   - Trigger animations

### Implementation Details

**Cell Layout**:
```csharp
private Vector3 GetCellPosition(int cellIndex)
{
    float angle = (cellIndex / 12f) * 360f;
    float radians = angle * Mathf.Deg2Rad;
    
    float x = boardCenterPosition.x + Mathf.Cos(radians) * cellRadius;
    float y = boardCenterPosition.y + Mathf.Sin(radians) * cellRadius;
    
    return new Vector3(x, y, 0);
}
```

**Event Listening**:
```csharp
public void Initialize(GameStateManager stateManager)
{
    gameStateManager = stateManager;
    gameStateManager.OnPhaseChanged += OnGameStateChanged;
    gameStateManager.OnChipPlaced += OnChipPlaced;
    gameStateManager.OnChipBumped += OnChipBumped;
    
    CreateBoardLayout();
}
```

---

## PART 4: CellView (250 lines)

**Purpose**: Individual cell representation. Handles visual states, interaction, and animation.

### Class Structure

```csharp
public class CellView : MonoBehaviour
{
    // PROPERTIES
    public int CellIndex { get; private set; }
    public bool IsOccupied { get; private set; }
    public Player Occupant { get; private set; }
    
    // VISUAL COMPONENTS
    [SerializeField] private Image cellBackground;
    [SerializeField] private Image occupantIndicator;
    [SerializeField] private Text chipCountDisplay;
    [SerializeField] private Button clickArea;
    [SerializeField] private CanvasGroup canvasGroup;
    
    // STATE
    private CellState currentState = CellState.Empty;
    private bool isHighlighted = false;
    private bool isSelected = false;
    
    // EVENTS
    public event System.Action<CellView> OnClicked;
    public event System.Action<CellView> OnHovered;
    public event System.Action<CellView> OnExited;
    
    // INITIALIZATION
    public void Initialize(int cellIndex);
    
    // STATE MANAGEMENT
    public void SetEmpty();
    public void SetOccupied(Player player);
    public void SetHighlighted(bool highlighted);
    public void SetSelected(bool selected);
    
    // VISUAL UPDATES
    private void UpdateDisplay();
    private void UpdateColor();
    private void UpdateChipDisplay();
    
    // ANIMATIONS
    public void AnimatePlacement();
    public void AnimateBump();
    public void AnimateSelection();
    
    // INTERACTION
    private void OnPointerClick(PointerEventData eventData);
    private void OnPointerEnter(PointerEventData eventData);
    private void OnPointerExit(PointerEventData eventData);
}

public enum CellState
{
    Empty,
    Occupied,
    Highlighted,
    Selected
}
```

### Visual States

Each cell has these visual states:

1. **Empty**: No chip, normal appearance
2. **Occupied**: Shows player chip in player color
3. **Highlighted**: Shows valid move target (bright highlight)
4. **Selected**: Shows currently selected target (border/glow)

### Color Configuration

```csharp
// In BoardVisualConfig.cs
public class BoardVisualConfig
{
    public Color ColorEmpty = Color.gray;
    public Color ColorPlayer1 = Color.red;
    public Color ColorPlayer2 = Color.blue;
    public Color ColorPlayer3 = Color.green;
    public Color ColorPlayer4 = Color.yellow;
    public Color ColorHighlight = Color.white;
    public Color ColorSelected = new Color(1f, 1f, 0f); // Yellow glow
}
```

### Interaction Handling

```csharp
private void OnPointerClick(PointerEventData eventData)
{
    OnClicked?.Invoke(this);
    AnimateSelection();
}

private void OnPointerEnter(PointerEventData eventData)
{
    OnHovered?.Invoke(this);
    SetHighlighted(true);
}

private void OnPointerExit(PointerEventData eventData)
{
    OnExited?.Invoke(this);
    SetHighlighted(false);
}
```

---

## PART 5: ChipVisualizer (300 lines)

**Purpose**: Render chips visually on cells with animations.

### Class Structure

```csharp
public class ChipVisualizer : MonoBehaviour
{
    // PREFABS & ASSETS
    [SerializeField] private GameObject chipPrefab;
    [SerializeField] private Material[] playerMaterials;
    [SerializeField] private AnimationCurve placementCurve;
    [SerializeField] private AnimationCurve bumpCurve;
    
    // INTERNAL STATE
    private Dictionary<int, GameObject> chipObjects = new Dictionary<int, GameObject>();
    private Dictionary<int, ChipAnimator> animators = new Dictionary<int, ChipAnimator>();
    
    // CONFIGURATION
    [SerializeField] private float placementDuration = 0.3f;
    [SerializeField] private float bumpDuration = 0.4f;
    [SerializeField] private float bumpHeight = 1f;
    
    // EVENTS
    public event System.Action<int> OnChipPlacementComplete;
    public event System.Action<int> OnChipBumpComplete;
    
    // CHIP MANAGEMENT
    public void PlaceChip(int cellIndex, Player player);
    public void RemoveChip(int cellIndex);
    public void UpdateChipAppearance(int cellIndex, Player player);
    
    // ANIMATIONS
    public void AnimateChipPlacement(int cellIndex);
    public void AnimateChipBump(int cellIndex);
    
    // UTILITY
    public GameObject GetChipAtCell(int cellIndex);
    public void Clear();
}

public class ChipAnimator
{
    public GameObject chipObject;
    public Player player;
    public Transform originalPosition;
}
```

### Chip Placement Animation

```csharp
public void AnimateChipPlacement(int cellIndex)
{
    if (!chipObjects.ContainsKey(cellIndex))
        return;
    
    GameObject chip = chipObjects[cellIndex];
    Vector3 targetPos = chip.transform.position;
    Vector3 startPos = targetPos + Vector3.up * 2f; // Drop from above
    
    float elapsed = 0f;
    while (elapsed < placementDuration)
    {
        elapsed += Time.deltaTime;
        float t = elapsed / placementDuration;
        
        // Use animation curve for easing
        t = placementCurve.Evaluate(t);
        
        chip.transform.position = Vector3.Lerp(startPos, targetPos, t);
        yield return null;
    }
    
    chip.transform.position = targetPos;
    OnChipPlacementComplete?.Invoke(cellIndex);
}
```

### Bump Animation

```csharp
public void AnimateChipBump(int cellIndex)
{
    if (!chipObjects.ContainsKey(cellIndex))
        return;
    
    GameObject chip = chipObjects[cellIndex];
    Vector3 startPos = chip.transform.position;
    
    float elapsed = 0f;
    while (elapsed < bumpDuration)
    {
        elapsed += Time.deltaTime;
        float t = elapsed / bumpDuration;
        
        // Bounce up then disappear
        float height = Mathf.Sin(t * Mathf.PI) * bumpHeight;
        float alpha = Mathf.Lerp(1f, 0f, t);
        
        chip.transform.position = startPos + Vector3.up * height;
        
        // Update alpha
        Color color = chip.GetComponent<Image>().color;
        color.a = alpha;
        chip.GetComponent<Image>().color = color;
        
        yield return null;
    }
    
    RemoveChip(cellIndex);
    OnChipBumpComplete?.Invoke(cellIndex);
}
```

---

## PART 6: ValidMoveHighlighter (200 lines)

**Purpose**: Calculate and display valid moves based on current dice roll and board state.

### Class Structure

```csharp
public class ValidMoveHighlighter : MonoBehaviour
{
    // DEPENDENCIES
    private GameStateManager gameStateManager;
    private BoardGridManager boardManager;
    
    // STATE
    private int[] validMoves = new int[0];
    private int currentDiceRoll = 0;
    
    // EVENTS
    public event System.Action<int[]> OnValidMovesUpdated;
    
    // INITIALIZATION
    public void Initialize(GameStateManager stateManager, BoardGridManager boardManager);
    
    // VALID MOVE CALCULATION
    public int[] CalculateValidMoves(int fromCell, int diceRoll);
    public int[] CalculateValidMoves(Player player, int diceRoll);
    
    // DISPLAY
    public void ShowValidMoves(int[] cells);
    public void ClearValidMoves();
    
    // GAME STATE INTEGRATION
    private void OnPhaseChanged(GamePhase newPhase);
    private void OnDiceRolled(int[] dice);
}
```

### Valid Move Calculation Logic

```csharp
public int[] CalculateValidMoves(int fromCell, int diceRoll)
{
    if (fromCell < 0 || fromCell >= 12 || diceRoll <= 0)
        return new int[0];
    
    // Simple: move forward diceRoll cells (modulo 12)
    int targetCell = (fromCell + diceRoll) % 12;
    
    // Check if target is valid (may depend on game rules)
    if (IsValidTarget(targetCell))
    {
        return new int[] { targetCell };
    }
    
    return new int[0];
}

private bool IsValidTarget(int cellIndex)
{
    // Rules validation:
    // - Can't move to cell outside board
    // - Can move to empty cell
    // - Can move to opponent cell (for bumping)
    // - Can't move to own cell
    
    if (cellIndex < 0 || cellIndex >= 12)
        return false;
    
    Player currentPlayer = gameStateManager.CurrentPlayer;
    Player cellOccupant = gameStateManager.Board.GetCell(cellIndex).Occupant;
    
    if (cellOccupant == null)
        return true; // Empty cell always valid
    
    if (cellOccupant == currentPlayer)
        return false; // Can't bump own chip
    
    // Check if bumping is allowed in current game mode
    IGameMode mode = gameStateManager.CurrentGameMode;
    return mode.AllowBumping;
}
```

---

## PART 7: Integration with GameStateManager

### Event Subscriptions

BoardGridManager listens to:
1. **OnPhaseChanged**: Update valid move display based on phase
2. **OnDiceRolled**: Calculate and show valid moves
3. **OnChipPlaced**: Update board visuals, trigger animation
4. **OnChipBumped**: Trigger bump animation
5. **OnPlayerChanged**: Update current player indicator
6. **OnGameWon**: Trigger win animation

### Event Triggering

BoardGridManager triggers:
1. **OnCellSelected**: When player clicks a cell
   - GameStateManager responds with PlaceChip() if valid

### Integration Example

```csharp
// In BoardGridManager.Initialize()
gameStateManager.OnDiceRolled += (dice) =>
{
    // Calculate valid moves from current player position
    int[] validCells = moveHighlighter.CalculateValidMoves(
        gameStateManager.CurrentPlayer,
        dice[0] + dice[1]
    );
    
    ShowValidMoves(validCells);
};

gameStateManager.OnChipPlaced += (cellIndex, player) =>
{
    AnimateChipPlacement(cellIndex, player);
    chipVisualizer.PlaceChip(cellIndex, player);
    UpdateCellDisplay(cellIndex, player);
};

gameStateManager.OnChipBumped += (cellIndex) =>
{
    AnimateChipBump(cellIndex);
    chipVisualizer.AnimateChipBump(cellIndex);
};
```

---

## PART 8: Testing Strategy (22+ tests)

### Test Files

**BoardGridManagerTests.cs** (8+ tests)
```
✅ TestBoardInitialization
✅ TestCellCreation
✅ TestCellPositioning
✅ TestInputHandling
✅ TestStateUpdates
✅ TestEventDispatching
✅ TestAnimationTriggering
✅ TestValidMoveHighlighting
```

**CellViewTests.cs** (6+ tests)
```
✅ TestCellInitialization
✅ TestStateTransitions
✅ TestVisualUpdates
✅ TestInteractionHandling
✅ TestAnimations
✅ TestColorConfiguration
```

**ChipVisualizerTests.cs** (5+ tests)
```
✅ TestChipPlacement
✅ TestChipRemoval
✅ TestPlacementAnimation
✅ TestBumpAnimation
✅ TestMultipleChips
```

**BoardIntegrationTests.cs** (3+ integration tests)
```
✅ TestBoardWithGameStateManager
✅ TestFullGameFlow
✅ TestBumpingWithAnimation
```

### Test Example

```csharp
[Test]
public void TestBoardInitialization()
{
    // Arrange
    var boardManager = new BoardGridManager();
    var gameState = new GameStateManager();
    
    // Act
    boardManager.Initialize(gameState);
    
    // Assert
    Assert.AreEqual(12, boardManager.Cells.Length);
    Assert.IsTrue(boardManager.IsInitialized);
}

[Test]
public void TestValidMoveCalculation()
{
    // Arrange
    var highlighter = new ValidMoveHighlighter();
    highlighter.Initialize(gameState, boardManager);
    
    // Act
    int[] validMoves = highlighter.CalculateValidMoves(0, 3);
    
    // Assert
    Assert.AreEqual(1, validMoves.Length);
    Assert.AreEqual(3, validMoves[0]);
}
```

---

## PART 9: Timeline & Milestones

### Day 1-2: Foundation (400 lines)
- ✅ Create BoardGridManager class structure
- ✅ Implement cell layout algorithm
- ✅ Create cell instantiation logic
- ✅ Wire up event subscriptions
- ✅ 4+ unit tests

### Day 2-3: Cell Visualization (250 lines)
- ✅ Create CellView class
- ✅ Implement state management
- ✅ Create color configuration
- ✅ Implement interaction handlers
- ✅ 6+ unit tests

### Day 3-4: Chip System (300 lines)
- ✅ Create ChipVisualizer class
- ✅ Implement chip placement
- ✅ Create placement animation
- ✅ Create bump animation
- ✅ 5+ unit tests

### Day 4-5: Valid Moves (200 lines)
- ✅ Create ValidMoveHighlighter class
- ✅ Implement move calculation
- ✅ Implement highlighting logic
- ✅ Create integration with board manager
- ✅ 7+ integration tests

### Day 5: Code Review & Sign-Off
- ✅ All code review standards met
- ✅ All tests passing
- ✅ Code documented
- ✅ Integration verified
- ✅ Ready for Sprint 5 UI

---

## PART 10: Success Criteria

### Functionality
- ✅ All 12 cells visible and interactive
- ✅ Chips display correctly with player colors
- ✅ Valid moves highlight correctly
- ✅ Placement animation plays smoothly
- ✅ Bump animation plays smoothly
- ✅ Board state syncs with GameStateManager

### Quality
- ✅ 1,200+ lines production code
- ✅ 22+ passing unit tests
- ✅ 85%+ code coverage
- ✅ 95%+ documentation
- ✅ Zero critical issues

### Performance
- ✅ Board layout renders in < 100ms
- ✅ Animations run at 60 FPS
- ✅ Input response < 50ms
- ✅ Memory usage minimal
- ✅ No frame stutters

### Integration
- ✅ Works with GameStateManager
- ✅ Works with all 5 game modes
- ✅ Ready for UI team in Sprint 5
- ✅ Ready for final integration in Sprint 6

---

## PART 11: Dependencies & Blockers

### Dependencies
- ✅ GameStateManager (Sprint 2 - ready)
- ✅ BoardModel (Sprint 1 - ready)
- ✅ Player class (Sprint 1 - ready)
- ✅ IGameMode interface (Sprint 3 - ready)

### Potential Blockers
None identified. All upstream dependencies complete.

---

## PART 12: Questions & Support

**Questions for Managing Engineer**:
- Board visual style (pixel art, flat, 3D)?
- Animation preference (smooth, snappy)?
- Color scheme per game mode?
- Mobile optimization priority?

**Support Available**:
- Managing Engineer: Code review & decisions
- Gameplay Team: Game logic questions
- QA Team: Testing coordination

---

## DELIVERABLES SUMMARY

### Code Files
- BoardGridManager.cs (~400 lines)
- CellView.cs (~250 lines)
- ChipVisualizer.cs (~300 lines)
- ValidMoveHighlighter.cs (~200 lines)
- BoardVisualConfig.cs (~50 lines)

### Test Files
- BoardGridManagerTests.cs (~350 lines)
- CellViewTests.cs (~250 lines)
- ChipVisualizerTests.cs (~300 lines)
- BoardIntegrationTests.cs (~200 lines)

### Total
- **Production Code**: ~1,200 lines
- **Test Code**: ~1,100 lines
- **Documentation**: 95%+ inline

---

## FINAL NOTES

This is comprehensive board visualization that will form the visual foundation for the game. Quality, documentation, and testing are critical.

All code must meet CODING_STANDARDS.md requirements.

All code must pass Managing Engineer review before merge.

**Target Completion**: 5 days (by Nov 19, 2025)

**Next Gate**: Sprint 5 begins - UI team integrates with board visuals

---

**Issued By**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025, 4:00 PM UTC  
**Authority**: Executive Dispatch  
**Status**: 🚀 **TEAM ACTIVATED**

---

*End of Sprint 4 Board Team Brief*
