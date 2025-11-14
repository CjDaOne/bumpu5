using UnityEngine;

/// <summary>
/// GameStateSyncManager - Ensures all systems stay synchronized with GameStateManager.
/// 
/// Responsibilities:
/// - Watch for divergence between systems
/// - Trigger reconciliation if needed
/// - Validate state consistency
/// - Log state changes for debugging
/// - Prevent race conditions
/// </summary>
public class GameStateSyncManager : MonoBehaviour
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField]
    private GameStateManager gameStateManager;
    
    [SerializeField]
    private BoardGridManager boardGridManager;
    
    [SerializeField]
    private bool enableValidation = true;
    
    [SerializeField]
    private float validationInterval = 1.0f;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private bool isInitialized = false;
    private float lastValidationTime = 0;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize state synchronization manager</summary>
    public void Initialize(GameStateManager stateManager, BoardGridManager boardManager)
    {
        gameStateManager = stateManager;
        boardGridManager = boardManager;
        
        if (gameStateManager == null || boardGridManager == null)
        {
            Debug.LogError("GameStateSyncManager: Required dependencies are null");
            return;
        }
        
        // Subscribe to game state events
        gameStateManager.OnChipPlaced += OnChipPlaced;
        gameStateManager.OnChipBumped += OnChipBumped;
        gameStateManager.OnPhaseChanged += OnPhaseChanged;
        
        isInitialized = true;
        Debug.Log("GameStateSyncManager initialized");
    }
    
    /// <summary>Update synchronization state</summary>
    public void Update()
    {
        if (!isInitialized || !enableValidation)
            return;
        
        // Periodic validation
        if (Time.time - lastValidationTime >= validationInterval)
        {
            ValidateStateConsistency();
            lastValidationTime = Time.time;
        }
    }
    
    // ============================================
    // STATE SYNCHRONIZATION
    // ============================================
    
    /// <summary>Validate that all systems are in sync</summary>
    private void ValidateStateConsistency()
    {
        if (gameStateManager == null || boardGridManager == null)
            return;
        
        // Check board cell state vs game state
        BoardModel board = gameStateManager.Board;
        if (board == null)
            return;
        
        for (int i = 0; i < 12; i++)
        {
            BoardCell boardCell = board.GetCell(i);
            CellView cellView = boardGridManager.Cells[i];
            
            if (boardCell == null || cellView == null)
                continue;
            
            // Verify occupant matches
            if ((boardCell.Occupant == null) != !cellView.IsOccupied)
            {
                Debug.LogWarning($"State mismatch at cell {i}: BoardCell occupant={boardCell.Occupant != null}, CellView occupied={cellView.IsOccupied}");
                // Reconcile: update cell view to match board state
                boardGridManager.UpdateCellDisplay(i, boardCell.Occupant);
            }
            
            if (boardCell.Occupant != null && cellView.Occupant != boardCell.Occupant)
            {
                Debug.LogWarning($"Occupant mismatch at cell {i}");
                // Reconcile
                boardGridManager.UpdateCellDisplay(i, boardCell.Occupant);
            }
        }
    }
    
    /// <summary>Handler for chip placed event - ensure board updates</summary>
    private void OnChipPlaced(int cellIndex, Player player)
    {
        // Verify the cell was actually updated
        if (boardGridManager.Cells[cellIndex].Occupant != player)
        {
            Debug.LogWarning($"Chip placement sync issue at cell {cellIndex}");
            boardGridManager.UpdateCellDisplay(cellIndex, player);
        }
    }
    
    /// <summary>Handler for chip bumped event - ensure cell is cleared</summary>
    private void OnChipBumped(int cellIndex)
    {
        // Verify the cell was actually cleared
        if (boardGridManager.Cells[cellIndex].IsOccupied)
        {
            Debug.LogWarning($"Chip bump sync issue at cell {cellIndex}");
            boardGridManager.UpdateCellDisplay(cellIndex, null);
        }
    }
    
    /// <summary>Handler for phase change - update all affected systems</summary>
    private void OnPhaseChanged(GamePhase newPhase)
    {
        // Ensure board input is updated for new phase
        if (newPhase == GamePhase.Placing)
        {
            // Calculate and show valid moves
            Player currentPlayer = gameStateManager.CurrentPlayer;
            if (currentPlayer != null)
            {
                // Board system will handle valid move display through its own event handlers
                Debug.Log($"[GameStateSyncManager] Phase changed to Placing - valid moves will be calculated");
            }
        }
        else if (newPhase == GamePhase.RollingDice)
        {
            // Clear valid move display
            boardGridManager.ClearValidMoves();
        }
    }
    
    // ============================================
    // PUBLIC INTERFACE
    // ============================================
    
    /// <summary>Force full state reconciliation</summary>
    public void ReconcileAllStates()
    {
        Debug.Log("[GameStateSyncManager] Reconciling all states");
        ValidateStateConsistency();
    }
    
    /// <summary>Enable or disable validation</summary>
    public void SetValidationEnabled(bool enabled)
    {
        enableValidation = enabled;
    }
}
