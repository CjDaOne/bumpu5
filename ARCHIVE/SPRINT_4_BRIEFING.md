# SPRINT 4 DETAILED BRIEFING
## Board System Implementation (Board Engineering Agent)

**Issued By**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025  
**Duration**: Immediate start, target completion within sprint cycle  
**Target**: Board grid visualization + cell interaction system

---

## OVERVIEW

Sprint 4 transforms the abstract board logic (Sprint 1) into a visual, interactive system. The BoardGridManager displays the game board on screen, handles touch/mouse input, and provides visual feedback for game state changes.

**High Level**:
1. Create BoardGridManager (orchestrator)
2. Create CellView (visual representation of one cell)
3. Implement input handling (touch/mouse)
4. Implement visual feedback (selection, valid moves)
5. Integrate with GameStateManager
6. Comprehensive testing

**Success**: Board is fully playable, responsive, and visually clear.

---

## PART 1: BOARD GRID MANAGER

**File**: `Assets/Scripts/Board/BoardGridManager.cs`  
**Type**: MonoBehaviour (UI orchestrator)  
**Purpose**: Manage board layout, cell references, and overall board state

### Class Structure

```csharp
public class BoardGridManager : MonoBehaviour
{
    // ============================================
    // REFERENCES
    // ============================================
    
    /// <summary>Reference to game state manager</summary>
    [SerializeField] private GameStateManager gameStateManager;
    
    /// <summary>Prefab for individual cell view</summary>
    [SerializeField] private GameObject cellViewPrefab;
    
    /// <summary>Container for cell views (parent transform)</summary>
    [SerializeField] private Transform cellContainer;
    
    /// <summary>Array of all cell views (indexed 0-11)</summary>
    private CellView[] cellViews;
    
    // ============================================
    // CONFIGURATION
    // ============================================
    
    /// <summary>Number of cells on the board (always 12)</summary>
    private const int CELL_COUNT = 12;
    
    /// <summary>Spacing between cells in world units</summary>
    [SerializeField] private float cellSpacing = 1.2f;
    
    /// <summary>Grid rows (typically 2 rows of 6 cells)</summary>
    [SerializeField] private int gridRows = 2;
    
    /// <summary>Grid columns (typically 6 columns)</summary>
    [SerializeField] private int gridColumns = 6;
    
    // ============================================
    // STATE TRACKING
    // ============================================
    
    /// <summary>Currently selected cell (for input handling)</summary>
    private int selectedCellIndex = -1;
    
    /// <summary>Cells that are valid moves for current turn</summary>
    private bool[] validMoveCells;
    
    // ============================================
    // INITIALIZATION
    // ============================================
    
    /// <summary>
    /// Initialize the board grid with all cells.
    /// Called from Awake() or OnEnable().
    /// </summary>
    public void Initialize()
    {
        // Create cell views
        CreateCells();
        
        // Set up input handling
        SetupInputHandling();
        
        // Register with GameStateManager
        RegisterWithGameStateManager();
    }
    
    // ============================================
    // CELL CREATION
    // ============================================
    
    /// <summary>
    /// Create all 12 cell views and populate cellViews array.
    /// </summary>
    private void CreateCells()
    {
        cellViews = new CellView[CELL_COUNT];
        
        for (int i = 0; i < CELL_COUNT; i++)
        {
            // Calculate grid position
            int row = i / gridColumns;
            int col = i % gridColumns;
            
            // Instantiate cell view
            GameObject cellGO = Instantiate(cellViewPrefab, cellContainer);
            cellGO.name = $"Cell_{i}";
            
            // Position in grid
            Vector3 position = new Vector3(col * cellSpacing, -row * cellSpacing, 0);
            cellGO.transform.localPosition = position;
            
            // Get CellView component
            CellView cellView = cellGO.GetComponent<CellView>();
            cellView.Initialize(i, this);
            
            // Store reference
            cellViews[i] = cellView;
        }
    }
    
    // ============================================
    // INPUT HANDLING
    // ============================================
    
    /// <summary>
    /// Set up touch/mouse input handling.
    /// </summary>
    private void SetupInputHandling()
    {
        // Subscribe to cell click events
        for (int i = 0; i < cellViews.Length; i++)
        {
            cellViews[i].OnCellClicked += OnCellSelected;
        }
    }
    
    /// <summary>
    /// Called when a cell is clicked/tapped.
    /// </summary>
    private void OnCellSelected(int cellIndex)
    {
        // Check if move is valid
        if (!IsValidMove(cellIndex))
        {
            ShowInvalidMoveMessage();
            return;
        }
        
        // Deselect previous
        if (selectedCellIndex >= 0)
        {
            cellViews[selectedCellIndex].SetSelected(false);
        }
        
        // Select new cell
        selectedCellIndex = cellIndex;
        cellViews[cellIndex].SetSelected(true);
        
        // Notify game state manager
        gameStateManager.OnCellSelected(cellIndex);
    }
    
    /// <summary>
    /// Check if a cell selection is a valid move.
    /// </summary>
    private bool IsValidMove(int cellIndex)
    {
        // Check if cell is within valid moves
        if (validMoveCells == null || cellIndex >= validMoveCells.Length)
            return false;
        
        return validMoveCells[cellIndex];
    }
    
    /// <summary>
    /// Show visual feedback for invalid move attempt.
    /// </summary>
    private void ShowInvalidMoveMessage()
    {
        // Could display popup, flash screen, play sound, etc.
        // Implementation depends on UI framework
    }
    
    // ============================================
    // VISUAL FEEDBACK
    // ============================================
    
    /// <summary>
    /// Highlight valid move cells for current turn.
    /// </summary>
    public void HighlightValidMoves(int[] validCellIndices)
    {
        // Initialize valid moves array
        validMoveCells = new bool[CELL_COUNT];
        
        // Mark valid cells
        foreach (int index in validCellIndices)
        {
            if (index >= 0 && index < CELL_COUNT)
            {
                validMoveCells[index] = true;
                cellViews[index].SetHighlighted(true);
            }
        }
    }
    
    /// <summary>
    /// Clear all highlighted cells.
    /// </summary>
    public void ClearHighlights()
    {
        for (int i = 0; i < cellViews.Length; i++)
        {
            cellViews[i].SetHighlighted(false);
        }
        
        validMoveCells = null;
    }
    
    /// <summary>
    /// Update visual representation of a chip placement.
    /// </summary>
    public void UpdateCellDisplay(int cellIndex, Player player)
    {
        if (cellIndex >= 0 && cellIndex < cellViews.Length)
        {
            cellViews[cellIndex].DisplayChip(player);
        }
    }
    
    /// <summary>
    /// Remove visual representation of a chip (bump).
    /// </summary>
    public void ClearCellDisplay(int cellIndex)
    {
        if (cellIndex >= 0 && cellIndex < cellViews.Length)
        {
            cellViews[cellIndex].ClearChip();
        }
    }
    
    // ============================================
    // STATE UPDATES
    // ============================================
    
    /// <summary>
    /// Called when game state changes.
    /// Update board display based on current board model.
    /// </summary>
    public void RefreshDisplay(BoardModel board)
    {
        // Update all cells based on board state
        for (int i = 0; i < CELL_COUNT; i++)
        {
            Player chipOwner = board.GetChipOwner(i);
            if (chipOwner != null)
            {
                UpdateCellDisplay(i, chipOwner);
            }
            else
            {
                ClearCellDisplay(i);
            }
        }
    }
    
    // ============================================
    // GAME STATE MANAGER INTEGRATION
    // ============================================
    
    /// <summary>
    /// Register with game state manager for state change notifications.
    /// </summary>
    private void RegisterWithGameStateManager()
    {
        // Subscribe to board change events
        if (gameStateManager != null)
        {
            // Would subscribe to board change events
            // gameStateManager.OnBoardChanged += OnBoardChanged;
        }
    }
    
    /// <summary>
    /// Called when board model changes.
    /// </summary>
    private void OnBoardChanged(BoardModel board)
    {
        RefreshDisplay(board);
    }
    
    // ============================================
    // GETTERS
    // ============================================
    
    /// <summary>Gets a specific cell view by index.</summary>
    public CellView GetCell(int index)
    {
        if (index >= 0 && index < cellViews.Length)
            return cellViews[index];
        return null;
    }
    
    /// <summary>Gets the currently selected cell index.</summary>
    public int GetSelectedCellIndex()
    {
        return selectedCellIndex;
    }
}
```

---

## PART 2: CELL VIEW

**File**: `Assets/Scripts/Board/CellView.cs`  
**Type**: MonoBehaviour (visual representation of one cell)  
**Purpose**: Display and interact with a single board cell

### Class Structure

```csharp
public class CellView : MonoBehaviour
{
    // ============================================
    // REFERENCES
    // ============================================
    
    /// <summary>Reference to cell image/sprite renderer</summary>
    [SerializeField] private SpriteRenderer cellRenderer;
    
    /// <summary>Reference to chip display (child object)</summary>
    [SerializeField] private GameObject chipDisplay;
    
    /// <summary>Reference to highlight overlay</summary>
    [SerializeField] private GameObject highlightOverlay;
    
    /// <summary>Button for click detection</summary>
    private Button cellButton;
    
    // ============================================
    // STATE
    // ============================================
    
    /// <summary>Index of this cell on the board</summary>
    private int cellIndex;
    
    /// <summary>Reference to board grid manager</summary>
    private BoardGridManager boardManager;
    
    /// <summary>Currently displayed chip player</summary>
    private Player currentChipPlayer;
    
    /// <summary>Is this cell currently highlighted</summary>
    private bool isHighlighted = false;
    
    /// <summary>Is this cell currently selected</summary>
    private bool isSelected = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    /// <summary>Event fired when cell is clicked</summary>
    public event System.Action<int> OnCellClicked;
    
    // ============================================
    // COLORS
    // ============================================
    
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private Color selectedColor = Color.cyan;
    
    // ============================================
    // INITIALIZATION
    // ============================================
    
    /// <summary>
    /// Initialize this cell view.
    /// </summary>
    public void Initialize(int index, BoardGridManager manager)
    {
        cellIndex = index;
        boardManager = manager;
        
        // Setup click detection
        cellButton = GetComponent<Button>();
        if (cellButton != null)
        {
            cellButton.onClick.AddListener(OnCellClicked_Internal);
        }
        
        // Initial state
        SetHighlighted(false);
        SetSelected(false);
    }
    
    // ============================================
    // CLICK HANDLING
    // ============================================
    
    /// <summary>
    /// Internal click handler. Fires OnCellClicked event.
    /// </summary>
    private void OnCellClicked_Internal()
    {
        OnCellClicked?.Invoke(cellIndex);
    }
    
    // ============================================
    // VISUAL FEEDBACK
    // ============================================
    
    /// <summary>
    /// Set cell highlight state.
    /// </summary>
    public void SetHighlighted(bool highlighted)
    {
        isHighlighted = highlighted;
        
        if (highlighted)
        {
            cellRenderer.color = highlightColor;
            if (highlightOverlay != null)
                highlightOverlay.SetActive(true);
        }
        else
        {
            cellRenderer.color = normalColor;
            if (highlightOverlay != null)
                highlightOverlay.SetActive(false);
        }
    }
    
    /// <summary>
    /// Set cell selection state.
    /// </summary>
    public void SetSelected(bool selected)
    {
        isSelected = selected;
        
        if (selected)
        {
            cellRenderer.color = selectedColor;
        }
        else if (!isHighlighted)
        {
            cellRenderer.color = normalColor;
        }
    }
    
    /// <summary>
    /// Display a chip on this cell.
    /// </summary>
    public void DisplayChip(Player player)
    {
        if (chipDisplay != null)
        {
            currentChipPlayer = player;
            
            // Set chip color based on player
            Image chipImage = chipDisplay.GetComponent<Image>();
            if (chipImage != null)
            {
                chipImage.color = GetPlayerColor(player);
            }
            
            chipDisplay.SetActive(true);
        }
    }
    
    /// <summary>
    /// Remove chip display from this cell.
    /// </summary>
    public void ClearChip()
    {
        currentChipPlayer = null;
        if (chipDisplay != null)
        {
            chipDisplay.SetActive(false);
        }
    }
    
    /// <summary>
    /// Get color for a player's chip.
    /// </summary>
    private Color GetPlayerColor(Player player)
    {
        if (player == null)
            return Color.white;
        
        // Return player-specific color
        // Could be: Player 1 = red, Player 2 = blue, etc.
        return player.PlayerID == 1 ? Color.red : Color.blue;
    }
    
    // ============================================
    // GETTERS
    // ============================================
    
    /// <summary>Gets the cell index.</summary>
    public int GetCellIndex()
    {
        return cellIndex;
    }
    
    /// <summary>Gets the current chip owner.</summary>
    public Player GetChipOwner()
    {
        return currentChipPlayer;
    }
    
    /// <summary>Is this cell highlighted.</summary>
    public bool IsHighlighted()
    {
        return isHighlighted;
    }
    
    /// <summary>Is this cell selected.</summary>
    public bool IsSelected()
    {
        return isSelected;
    }
}
```

---

## PART 3: INPUT HANDLING

**File**: `Assets/Scripts/Board/CellInputHandler.cs`  
**Type**: MonoBehaviour  
**Purpose**: Handle touch/mouse input across the entire board

### Features

```csharp
public class CellInputHandler : MonoBehaviour
{
    [SerializeField] private BoardGridManager boardManager;
    [SerializeField] private LayerMask cellLayer;
    
    private Camera mainCamera;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }
    
    private void Update()
    {
        // Handle mouse/touch input
        if (Input.GetMouseButtonDown(0))
        {
            HandleCellSelection();
        }
    }
    
    /// <summary>
    /// Handle cell selection from mouse/touch.
    /// </summary>
    private void HandleCellSelection()
    {
        // Cast ray from camera through click position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        // Check if ray hits a cell
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, cellLayer))
        {
            // Get CellView from hit object
            CellView cellView = hit.collider.GetComponent<CellView>();
            if (cellView != null)
            {
                // Notify board manager
                int cellIndex = cellView.GetCellIndex();
                boardManager.OnCellSelected(cellIndex);
            }
        }
    }
}
```

---

## PART 4: INTEGRATION WITH GAMESTATE MANAGER

The BoardGridManager must integrate with GameStateManager:

### Required Integration Points

**1. Board Change Events**:
```csharp
// In GameStateManager:
public event System.Action<BoardModel> OnBoardChanged;

// In BoardGridManager:
private void RegisterWithGameStateManager()
{
    gameStateManager.OnBoardChanged += OnBoardChanged;
}

private void OnBoardChanged(BoardModel board)
{
    RefreshDisplay(board);
}
```

**2. Turn State Events**:
```csharp
// Notify board of valid moves for current turn
public void OnTurnStarted(int[] validCellIndices)
{
    boardGridManager.HighlightValidMoves(validCellIndices);
}

// Clear highlights on turn end
public void OnTurnEnded()
{
    boardGridManager.ClearHighlights();
}
```

**3. Cell Selection**:
```csharp
// Board notifies game state of cell selection
public void OnCellSelected(int cellIndex)
{
    gameStateManager.ProcessCellSelection(cellIndex);
}
```

---

## PART 5: TESTING STRATEGY

### Unit Tests (BoardGridManagerTests.cs)
```csharp
[Test] public void CreateCells_GeneratesAllTwelveCells()
[Test] public void InitializeCell_SetsCorrectIndex()
[Test] public void HighlightValidMoves_MarksCorrectCells()
[Test] public void ClearHighlights_RemovesAllHighlighting()
[Test] public void UpdateCellDisplay_ShowsChip()
[Test] public void ClearCellDisplay_RemovesChip()
[Test] public void IsValidMove_ReturnsTrueForValidCell()
[Test] public void IsValidMove_ReturnsFalseForInvalidCell()
[Test] public void GetCell_ReturnsCorrectCellView()
[Test] public void OnCellSelected_UpdatesSelectedIndex()
```

### Cell View Tests (CellViewTests.cs)
```csharp
[Test] public void Initialize_SetsCorrectIndex()
[Test] public void SetHighlighted_ChangesColor()
[Test] public void SetSelected_ChangesColor()
[Test] public void DisplayChip_ShowsChipDisplay()
[Test] public void ClearChip_HidesChipDisplay()
[Test] public void OnCellClicked_FiresEvent()
[Test] public void GetChipOwner_ReturnsCorrectPlayer()
```

### Integration Tests (BoardIntegrationTests.cs)
```csharp
[Test] public void BoardGridManager_IntegratesWithGameStateManager()
[Test] public void CellSelection_UpdatesGameState()
[Test] public void BoardChanges_UpdateDisplay()
[Test] public void ValidMoves_HighlightedCorrectly()
[Test] public void InvalidMove_ShowsFeedback()
```

---

## DELIVERABLES CHECKLIST

- [ ] BoardGridManager.cs (complete grid management)
- [ ] CellView.cs (individual cell representation)
- [ ] CellInputHandler.cs (input handling)
- [ ] Integration with GameStateManager events
- [ ] BoardGridManagerTests.cs (10+ unit tests)
- [ ] CellViewTests.cs (7+ unit tests)
- [ ] BoardIntegrationTests.cs (5+ integration tests)
- [ ] All tests passing (100%)
- [ ] Code review approved

**Target**: 1,200+ lines production code + 400+ lines tests

---

## SUCCESS CRITERIA

✅ Board displays all 12 cells  
✅ Cells respond to click/touch input  
✅ Valid moves are highlighted  
✅ Selected cells change appearance  
✅ Chip placement updates display  
✅ Chip removal (bumps) update display  
✅ No lag or performance issues  
✅ Integrates with GameStateManager  
✅ 22+ unit tests with 100% pass rate  
✅ Code review approved  

---

## TIMELINE

**Day 1**: BoardGridManager + CellView  
**Day 2**: Input handling + integration  
**Day 3**: Visual feedback system  
**Day 4**: Comprehensive testing  
**Day 5**: Code review & polish  

---

**READY TO BEGIN SPRINT 4?**

All information provided. All requirements clear. Proceed with implementation.

**Contact Managing Engineer with any questions: Amp**

---

*End of Sprint 4 Briefing*
