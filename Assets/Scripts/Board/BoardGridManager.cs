using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// BoardGridManager - Master controller for the visual board system.
/// 
/// Responsibilities:
/// - Create and manage cell GameObjects in grid layout
/// - Handle cell input/interaction
/// - Update visual board state based on game logic
/// - Display valid move highlighting
/// - Trigger chip placement and bump animations
/// - Integrate with GameStateManager events
/// 
/// Architecture:
/// - Creates CellView prefabs at runtime
/// - Listens to GameStateManager for game state changes
/// - Delegates chip visuals to ChipVisualizer
/// - Delegates move calculation to ValidMoveHighlighter
/// </summary>
public class BoardGridManager : MonoBehaviour
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField] 
    private Transform boardParent;
    
    [SerializeField] 
    private CellView cellPrefab;
    
    [SerializeField] 
    private Vector2 boardCenterPosition = Vector2.zero;
    
    [SerializeField] 
    private float cellRadius = 3f;
    
    [SerializeField] 
    private float cellSize = 0.8f;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private CellView[] cells = new CellView[BoardModel.BOARD_SIZE];
    private GameStateManager gameStateManager;
    private ChipVisualizer chipVisualizer;
    private ValidMoveHighlighter moveHighlighter;
    private int[] currentValidMoves = new int[0];
    private bool isInitialized = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    /// <summary>Fired when player clicks a cell</summary>
    public event System.Action<int> OnCellSelected;
    
    /// <summary>Fired when player hovers over a cell</summary>
    public event System.Action<int> OnCellHovered;
    
    /// <summary>Fired when player exits hover on a cell</summary>
    public event System.Action<int> OnCellExited;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public CellView[] Cells => cells;
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>
    /// Initialize the board system with game state reference.
    /// Creates all cell visuals and subscribes to game state events.
    /// </summary>
    public void Initialize(GameStateManager stateManager)
    {
        if (stateManager == null)
        {
            Debug.LogError("BoardGridManager.Initialize: gameStateManager is null");
            return;
        }
        
        gameStateManager = stateManager;
        
        // Create child components
        if (chipVisualizer == null)
            chipVisualizer = gameObject.AddComponent<ChipVisualizer>();
        chipVisualizer.Initialize();
        
        if (moveHighlighter == null)
            moveHighlighter = gameObject.AddComponent<ValidMoveHighlighter>();
        moveHighlighter.Initialize(stateManager, this);
        
        // Create board layout
        CreateBoardLayout();
        
        // Subscribe to game state events
        SubscribeToGameStateEvents();
        
        isInitialized = true;
        Debug.Log("BoardGridManager initialized successfully");
    }
    
    /// <summary>Cleanup on shutdown</summary>
    public void Shutdown()
    {
        if (gameStateManager != null)
        {
            gameStateManager.OnPhaseChanged -= OnGameStatePhaseChanged;
            gameStateManager.OnDiceRolled -= OnGameStateDiceRolled;
            gameStateManager.OnChipPlaced -= OnGameStateChipPlaced;
            gameStateManager.OnChipBumped -= OnGameStateChipBumped;
            gameStateManager.OnPlayerChanged -= OnGameStatePlayerChanged;
            gameStateManager.OnGameWon -= OnGameStateGameWon;
        }
        
        if (chipVisualizer != null)
            chipVisualizer.Clear();
        
        isInitialized = false;
    }
    
    // ============================================
    // BOARD CREATION
    // ============================================
    
    /// <summary>Create all cell GameObjects in 5x5 grid layout</summary>
    private void CreateBoardLayout()
    {
        if (cellPrefab == null)
        {
            Debug.LogError("BoardGridManager: cellPrefab is not assigned");
            return;
        }
        
        int boardSize = BoardModel.BOARD_SIZE;
        // Resize cells array if needed
        if (cells.Length != boardSize)
        {
            cells = new CellView[boardSize];
        }
        
        for (int i = 0; i < boardSize; i++)
        {
            // Instantiate cell prefab
            CellView cell = Instantiate(cellPrefab, boardParent);
            cells[i] = cell;
            
            // Initialize cell
            cell.Initialize(i);
            
            // Position cell in grid layout
            Vector3 cellPosition = GetCellPosition(i);
            cell.transform.localPosition = cellPosition;
            
            // Subscribe to cell events
            // Capture index for closure
            int index = i;
            cell.OnClicked += (c) => HandleCellClicked(index);
            cell.OnHovered += (c) => HandleCellHovered(index);
            cell.OnExited += (c) => HandleCellExited(index);
            
            // Debug.Log($"Created cell {i} at position {cellPosition}");
        }
    }
    
    /// <summary>Calculate world position of a cell in 5x5 grid layout</summary>
    private Vector3 GetCellPosition(int cellIndex)
    {
        int boardSize = BoardModel.BOARD_SIZE;
        if (cellIndex < 0 || cellIndex >= boardSize)
            return Vector3.zero;
            
        int gridSize = 5; // Hardcoded for now, or get from BoardModel
        int row = cellIndex / gridSize;
        int col = cellIndex % gridSize;
        
        // Center the grid
        // Offset: (gridSize - 1) * cellSize / 2
        float offset = (gridSize - 1) * cellSize / 2f;
        
        float x = (col * cellSize) - offset + boardCenterPosition.x;
        float y = (row * -cellSize) + offset + boardCenterPosition.y; // -y for rows going down
        
        return new Vector3(x, y, 0);
    }
    
    // ============================================
    // INPUT HANDLING
    // ============================================
    
    /// <summary>Called when player clicks a cell</summary>
    private void HandleCellClicked(int cellIndex)
    {
        if (!isInitialized)
            return;
        
        // Trigger event
        OnCellSelected?.Invoke(cellIndex);
        
        // Attempt to place chip at this cell
        if (gameStateManager.CanPlaceChip(cellIndex))
        {
            gameStateManager.PlaceChip(cellIndex);
            Debug.Log($"Chip placed on cell {cellIndex}");
        }
        else
        {
            Debug.Log($"Invalid move: cannot place chip on cell {cellIndex}");
        }
    }
    
    /// <summary>Called when player hovers over a cell</summary>
    private void HandleCellHovered(int cellIndex)
    {
        if (!isInitialized)
            return;
        
        OnCellHovered?.Invoke(cellIndex);
        
        // Highlight this cell if it's a valid move
        if (IsValidMove(cellIndex))
        {
            cells[cellIndex].SetHighlighted(true);
        }
    }
    
    /// <summary>Called when player exits hover on a cell</summary>
    private void HandleCellExited(int cellIndex)
    {
        if (!isInitialized)
            return;
        
        OnCellExited?.Invoke(cellIndex);
        
        // Unhighlight
        cells[cellIndex].SetHighlighted(false);
    }
    
    /// <summary>Check if a cell is a valid move target</summary>
    private bool IsValidMove(int cellIndex)
    {
        if (currentValidMoves == null || currentValidMoves.Length == 0)
            return false;
        
        foreach (int validCell in currentValidMoves)
        {
            if (validCell == cellIndex)
                return true;
        }
        
        return false;
    }
    
    // ============================================
    // STATE UPDATES
    // ============================================
    
    /// <summary>Update display of a single cell based on current board state</summary>
    public void UpdateCellDisplay(int cellIndex, Player occupant)
    {
        if (cells == null || cellIndex < 0 || cellIndex >= cells.Length)
            return;
        
        if (cells[cellIndex] == null)
            return;
        
        if (occupant == null)
        {
            cells[cellIndex].SetEmpty();
        }
        else
        {
            cells[cellIndex].SetOccupied(occupant);
            chipVisualizer.PlaceChip(cellIndex, occupant);
        }
    }
    
    /// <summary>Update all cells based on board model state</summary>
    public void UpdateAllCells(BoardModel board)
    {
        if (board == null)
            return;
        
        int boardSize = BoardModel.BOARD_SIZE;
        for (int i = 0; i < boardSize; i++)
        {
            BoardCell boardCell = board.GetCell(i);
            if (boardCell != null)
            {
                UpdateCellDisplay(i, boardCell.Owner);
            }
        }
    }
    
    /// <summary>Clear all cells (no occupants)</summary>
    public void ClearBoard()
    {
        if (cells == null) return;
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] != null)
            {
                cells[i].SetEmpty();
            }
        }
        chipVisualizer.Clear();
    }
    
    // ============================================
    // VALID MOVES DISPLAY
    // ============================================
    
    /// <summary>Display valid moves on the board</summary>
    public void ShowValidMoves(int[] validCells)
    {
        // Clear previous highlighting
        ClearValidMoves();
        
        if (validCells == null || validCells.Length == 0)
            return;
        
        currentValidMoves = validCells;
        
        // Highlight each valid cell
        foreach (int cellIndex in validCells)
        {
            if (cellIndex >= 0 && cellIndex < cells.Length && cells[cellIndex] != null)
            {
                cells[cellIndex].SetHighlighted(true);
            }
        }
    }
    
    /// <summary>Clear valid move highlighting</summary>
    public void ClearValidMoves()
    {
        if (cells == null) return;
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i] != null)
            {
                cells[i].SetHighlighted(false);
            }
        }
        currentValidMoves = new int[0];
    }
    
    // ============================================
    // ANIMATIONS
    // ============================================
    
    /// <summary>Animate chip placement on a cell</summary>
    public void AnimateChipPlacement(int cellIndex, Player player)
    {
        if (cells == null || cellIndex < 0 || cellIndex >= cells.Length)
            return;
        
        if (chipVisualizer != null)
        {
            chipVisualizer.AnimateChipPlacement(cellIndex);
        }
    }
    
    /// <summary>Animate chip being bumped from a cell</summary>
    public void AnimateChipBump(int cellIndex)
    {
        if (cells == null || cellIndex < 0 || cellIndex >= cells.Length)
            return;
        
        if (chipVisualizer != null)
        {
            chipVisualizer.AnimateChipBump(cellIndex);
        }
    }
    
    /// <summary>Animate win condition (celebration effects)</summary>
    public void AnimateWin(Player winningPlayer)
    {
        if (winningPlayer == null)
            return;
        
        Debug.Log($"Win animation for {winningPlayer.PlayerName}");
        // TODO: Trigger win celebration effects
    }
    
    // ============================================
    // GAME STATE INTEGRATION
    // ============================================
    
    /// <summary>Subscribe to all GameStateManager events</summary>
    private void SubscribeToGameStateEvents()
    {
        if (gameStateManager == null)
            return;
        
        gameStateManager.OnPhaseChanged += OnGameStatePhaseChanged;
        gameStateManager.OnDiceRolled += OnGameStateDiceRolled;
        gameStateManager.OnChipPlaced += OnGameStateChipPlaced;
        gameStateManager.OnChipBumped += OnGameStateChipBumped;
        gameStateManager.OnPlayerChanged += OnGameStatePlayerChanged;
        gameStateManager.OnGameWon += OnGameStateGameWon;
    }
    
    /// <summary>Handler for phase change events</summary>
    private void OnGameStatePhaseChanged(GamePhase newPhase)
    {
        Debug.Log($"BoardGridManager: Phase changed to {newPhase}");
        
        // Clear valid moves on phase change
        if (newPhase != GamePhase.Placing)
        {
            ClearValidMoves();
        }
    }
    
    /// <summary>Handler for dice rolled events</summary>
    private void OnGameStateDiceRolled(int[] dice)
    {
        if (dice == null || dice.Length < 2)
            return;
        
        int diceTotal = dice[0] + dice[1];
        Debug.Log($"BoardGridManager: Dice rolled {dice[0]} + {dice[1]} = {diceTotal}");
        
        // Calculate and show valid moves
        if (moveHighlighter != null && gameStateManager != null)
        {
            Player currentPlayer = gameStateManager.CurrentPlayer;
            if (currentPlayer != null)
            {
                int[] validMoves = moveHighlighter.CalculateValidMoves(currentPlayer, diceTotal);
                ShowValidMoves(validMoves);
            }
        }
    }
    
    /// <summary>Handler for chip placed events</summary>
    private void OnGameStateChipPlaced(Player player, int cellIndex)
    {
        Debug.Log($"BoardGridManager: Chip placed on cell {cellIndex} by {player.PlayerName}");
        
        UpdateCellDisplay(cellIndex, player);
        AnimateChipPlacement(cellIndex, player);
    }
    
    /// <summary>Handler for chip bumped events</summary>
    private void OnGameStateChipBumped(int cellIndex)
    {
        Debug.Log($"BoardGridManager: Chip bumped from cell {cellIndex}");
        
        AnimateChipBump(cellIndex);
        UpdateCellDisplay(cellIndex, null);
    }
    
    /// <summary>Handler for player changed events</summary>
    private void OnGameStatePlayerChanged(Player newPlayer)
    {
        Debug.Log($"BoardGridManager: Current player changed to {newPlayer.PlayerName}");
        // TODO: Update UI to show current player
    }
    
    /// <summary>Handler for game won events</summary>
    private void OnGameStateGameWon(Player winner)
    {
        Debug.Log($"BoardGridManager: Game won by {winner.PlayerName}");
        AnimateWin(winner);
    }
}
