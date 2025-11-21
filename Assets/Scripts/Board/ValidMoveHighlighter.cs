using UnityEngine;

/// <summary>
/// ValidMoveHighlighter - Calculates and displays valid moves on the board.
/// 
/// Responsibilities:
/// - Calculate valid moves from current player position
/// - Take into account dice roll result
/// - Check board rules (can't move to self, can bump opponents, etc.)
/// - Integrate with IGameMode rules
/// - Display valid moves on board
/// 
/// The highlighter uses the game mode's rules to determine what moves are valid
/// and tells the board manager which cells should be highlighted.
/// </summary>
public class ValidMoveHighlighter : MonoBehaviour
{
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private GameStateManager gameStateManager;
    private BoardGridManager boardManager;
    private int[] currentValidMoves = new int[0];
    private bool isInitialized = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    public event System.Action<int[]> OnValidMovesUpdated;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public int[] CurrentValidMoves => currentValidMoves;
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize the valid move highlighter</summary>
    public void Initialize(GameStateManager stateManager, BoardGridManager boardMgr)
    {
        if (stateManager == null || boardMgr == null)
        {
            Debug.LogError("ValidMoveHighlighter.Initialize: Required dependencies are null");
            return;
        }
        
        gameStateManager = stateManager;
        boardManager = boardMgr;
        
        // Subscribe to game state events
        gameStateManager.OnPhaseChanged += OnGamePhaseChanged;
        gameStateManager.OnDiceRolled += OnGameDiceRolled;
        
        isInitialized = true;
        Debug.Log("ValidMoveHighlighter initialized");
    }
    
    // ============================================
    // VALID MOVE CALCULATION
    // ============================================
    
    /// <summary>Calculate valid moves from a starting cell with given dice roll</summary>
    public int[] CalculateValidMoves(int fromCell, int diceRoll)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return new int[0];
        
        // Simple movement: move forward diceRoll cells (modulo 12)
        int targetCell = (fromCell + diceRoll) % 12;
        
        // Validate target
        if (IsValidTarget(targetCell))
        {
            return new int[] { targetCell };
        }
        
        return new int[0];
    }
    
    /// <summary>Calculate valid moves from current player's position</summary>
    public int[] CalculateValidMoves(Player player, int diceRoll)
    {
        if (player == null || gameStateManager == null || gameStateManager.Board == null)
            return new int[0];
        
        // Find player's current chip position
        int playerPosition = FindPlayerPosition(player);
        if (playerPosition < 0)
        {
            Debug.LogWarning($"Could not find position for player {player.PlayerName}");
            return new int[0];
        }
        
        return CalculateValidMoves(playerPosition, diceRoll);
    }
    
    /// <summary>Find board position of player's chip</summary>
    private int FindPlayerPosition(Player player)
    {
        BoardModel board = gameStateManager.Board;
        if (board == null)
            return -1;
        
        // Linear search for player's chip
        for (int i = 0; i < 12; i++)
        {
            BoardCell cell = board.Cells[i];
            if (cell != null && cell.Owner == player)
            {
                return i;
            }
        }
        
        return -1;
    }
    
    /// <summary>Check if a target cell is valid for placement</summary>
    private bool IsValidTarget(int cellIndex)
    {
        // Validate bounds
        if (cellIndex < 0 || cellIndex >= 12)
            return false;
        
        BoardModel board = gameStateManager.Board;
        if (board == null)
            return false;
        
        BoardCell targetCell = board.Cells[cellIndex];
        if (targetCell == null)
            return false;
        
        // Get current player
        Player currentPlayer = gameStateManager.CurrentPlayer;
        if (currentPlayer == null)
            return false;
        
        // Can't place on own chip
        if (targetCell.Owner == currentPlayer)
            return false;
        
        // If empty, always valid
        if (targetCell.Occupant == null)
            return true;
        
        // Target has opponent chip
        // Check if bumping is allowed in current game mode
        IGameMode gameMode = gameStateManager.CurrentGameMode;
        if (gameMode != null && gameMode.CanBump(currentPlayer, cellIndex))
            return true;
        
        // Bumping not allowed in this mode
        return false;
    }
    
    // ============================================
    // DISPLAY
    // ============================================
    
    /// <summary>Display given cells as valid moves</summary>
    public void ShowValidMoves(int[] cells)
    {
        if (boardManager == null)
            return;
        
        currentValidMoves = cells ?? new int[0];
        boardManager.ShowValidMoves(currentValidMoves);
        
        OnValidMovesUpdated?.Invoke(currentValidMoves);
    }
    
    /// <summary>Clear valid move display</summary>
    public void ClearValidMoves()
    {
        if (boardManager == null)
            return;
        
        boardManager.ClearValidMoves();
        currentValidMoves = new int[0];
    }
    
    // ============================================
    // GAME STATE INTEGRATION
    // ============================================
    
    /// <summary>Handler for phase change events</summary>
    private void OnGamePhaseChanged(GamePhase newPhase)
    {
        // Only show valid moves during Placing phase
        if (newPhase != GamePhase.Placing)
        {
            ClearValidMoves();
        }
    }
    
    /// <summary>Handler for dice rolled events</summary>
    private void OnGameDiceRolled(int[] dice)
    {
        if (gameStateManager == null || gameStateManager.CurrentGameMode == null)
            return;
        
        if (dice == null || dice.Length < 2)
            return;
        
        int diceTotal = dice[0] + dice[1];
        
        // Calculate valid moves from current player's position
        Player currentPlayer = gameStateManager.CurrentPlayer;
        if (currentPlayer != null)
        {
            int[] validMoves = CalculateValidMoves(currentPlayer, diceTotal);
            ShowValidMoves(validMoves);
        }
    }
}
