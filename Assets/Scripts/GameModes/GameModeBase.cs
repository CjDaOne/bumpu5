using UnityEngine;

/// <summary>
/// GameModeBase Abstract Class
/// 
/// Abstract base class that implements common game mode functionality.
/// All game modes inherit from this class to ensure consistent interface implementation.
/// 
/// Provides:
/// - Common state management (gameStateManager, gameState references)
/// - Default implementations of optional interface methods
/// - Utility methods for common operations
/// 
/// Game modes override:
/// - ModeName and ModeDescription properties
/// - IsValidMove() - to define which cells are legal
/// - CanBump() - to define if/when bumping is allowed
/// - CheckWinCondition() - to define how a player wins
/// - OnBumpOccurs() - to apply mode-specific penalties
/// </summary>
public abstract class GameModeBase : IGameMode
{
    // Protected state references accessible to derived classes
    protected GameStateManager gameStateManager;
    protected GameState gameState;
    
    /// <summary>
    /// Display name of this game mode.
    /// Must be overridden in each derived class.
    /// </summary>
    public abstract string ModeName { get; }
    
    /// <summary>
    /// Description of how this game mode plays.
    /// Must be overridden in each derived class.
    /// </summary>
    public abstract string ModeDescription { get; }
    
    /// <summary>
    /// Initialize this mode with the game state manager.
    /// Called when mode is selected.
    /// 
    /// Derived classes should call base.Initialize() and then
    /// perform any mode-specific initialization.
    /// </summary>
    public virtual void Initialize(GameStateManager gsm)
    {
        gameStateManager = gsm;
        gameState = gsm.GetGameState();
        
        // Log initialization for debugging
        Debug.Log($"[GameMode] {ModeName} initialized");
    }
    
    /// <summary>
    /// Called when the game starts.
    /// Default implementation does nothing.
    /// Override in derived classes for mode-specific startup logic.
    /// </summary>
    public virtual void OnGameStart()
    {
        Debug.Log($"[GameMode] {ModeName} game started");
    }
    
    /// <summary>
    /// Called at the beginning of each player's turn.
    /// Default implementation does nothing.
    /// Override in derived classes for mode-specific turn logic.
    /// </summary>
    public virtual void OnTurnStart(Player currentPlayer)
    {
        Debug.Log($"[GameMode] {ModeName} turn started for {currentPlayer.PlayerName}");
    }
    
    /// <summary>
    /// Check if a move is valid.
    /// Must be overridden in each derived class to implement mode-specific rules.
    /// </summary>
    public abstract bool IsValidMove(Player player, int cellIndex);
    
    /// <summary>
    /// Called after a chip is placed.
    /// Default implementation does nothing.
    /// Override in derived classes for post-placement logic.
    /// </summary>
    public virtual void OnChipPlaced(Player player, int cellIndex)
    {
        Debug.Log($"[GameMode] {ModeName} chip placed by {player.PlayerName} at cell {cellIndex}");
    }
    
    /// <summary>
    /// Check if bumping is allowed in this mode.
    /// Must be overridden in each derived class to define bumping rules.
    /// </summary>
    public abstract bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell);
    
    /// <summary>
    /// Called when a bump occurs.
    /// Default implementation does nothing.
    /// Override in derived classes to apply mode-specific bump penalties.
    /// </summary>
    public virtual void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        Debug.Log($"[GameMode] {ModeName} bump occurred: {bumpingPlayer.PlayerName} bumped {bumpedPlayer.PlayerName}");
    }
    
    /// <summary>
    /// Check if a player has won.
    /// Must be overridden in each derived class to implement mode-specific win condition.
    /// </summary>
    public abstract bool CheckWinCondition(Player player);
    
    /// <summary>
    /// Called when the game ends.
    /// Default implementation does nothing.
    /// Override in derived classes for end-game logic.
    /// </summary>
    public virtual void OnGameEnd(Player winner)
    {
        Debug.Log($"[GameMode] {ModeName} game ended. Winner: {winner.PlayerName}");
    }
    
    // ==================== UTILITY METHODS ====================
    
    /// <summary>
    /// Check if a cell is empty (no chips placed there).
    /// Helper method used by most game modes' IsValidMove() implementations.
    /// </summary>
    protected bool IsCellEmpty(int cellIndex)
    {
        if (gameState == null || gameState.Board == null)
            return false;
            
        BoardCell cell = gameState.Board.GetCell(cellIndex);
        return cell != null && !cell.IsOccupied;
    }
    
    /// <summary>
    /// Check if a cell is occupied by a specific player.
    /// Helper method for bump checks and ownership validation.
    /// </summary>
    protected bool IsCellOccupiedBy(int cellIndex, Player player)
    {
        if (gameState == null || gameState.Board == null)
            return false;
            
        BoardCell cell = gameState.Board.GetCell(cellIndex);
        return cell != null && cell.IsOccupied && cell.OccupiedBy == player;
    }
    
    /// <summary>
    /// Get a specific board cell.
    /// Helper method for accessing board state safely.
    /// </summary>
    protected BoardCell GetCell(int cellIndex)
    {
        if (gameState == null || gameState.Board == null)
            return null;
            
        return gameState.Board.GetCell(cellIndex);
    }
    
    /// <summary>
    /// Get all cells occupied by a player.
    /// Helper method for win condition checks (e.g., checking 5-in-a-row).
    /// </summary>
    protected int[] GetCellsOccupiedBy(Player player)
    {
        int[] occupiedCells = new int[12];
        int count = 0;
        
        for (int i = 0; i < 12; i++)
        {
            if (IsCellOccupiedBy(i, player))
            {
                occupiedCells[count] = i;
                count++;
            }
        }
        
        // Return only the relevant portion of the array
        System.Array.Resize(ref occupiedCells, count);
        return occupiedCells;
    }
    
    /// <summary>
    /// Count chips placed by a player on the board.
    /// Helper method for win condition checks (e.g., Game2 checking for 5 chips).
    /// </summary>
    protected int GetChipCountForPlayer(Player player)
    {
        int count = 0;
        for (int i = 0; i < 12; i++)
        {
            if (IsCellOccupiedBy(i, player))
                count++;
        }
        return count;
    }
    
    /// <summary>
    /// Check if a player has 5 chips in a row (horizontal, vertical, or diagonal).
    /// Helper method used by Game1 and other modes that check for 5-in-a-row.
    /// </summary>
    protected bool HasFiveInARow(Player player)
    {
        int[] playerCells = GetCellsOccupiedBy(player);
        
        // Check all possible 5-in-a-row patterns
        // This is a simplified check - in production, would use a more efficient algorithm
        
        // Horizontal patterns (example for 12-cell board)
        // Would need to know board layout to implement properly
        // For now, this is a placeholder that derived classes can override or extend
        
        return false; // Placeholder
    }
}
