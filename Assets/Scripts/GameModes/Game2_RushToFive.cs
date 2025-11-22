using UnityEngine;

/// <summary>
/// Game2_RushToFive - Speed-Based Game Mode
/// 
/// Win Condition: First to place 5 chips on the board (regardless of pattern)
/// Bumping: DISABLED - bumping is not allowed in this mode
/// Penalties: Reduced - no bump penalties since bumping doesn't exist
/// Difficulty: Easy
/// 
/// This mode is all about speed and placement efficiency.
/// Players race to fill 5 cells without worrying about patterns or strategic positioning.
/// Fast-paced, beginner-friendly game mode.
/// </summary>
public class Game2_RushToFive : GameModeBase
{
    // Mode properties
    public override string ModeName => "Rush to Five";
    public override string ModeDescription => "First to place 5 chips wins. No bumping allowed. Fast-paced race to victory.";
    
    // ==================== LIFECYCLE ====================
    
    /// <summary>
    /// Initialize the game mode.
    /// </summary>
    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        Debug.Log("[Game2_RushToFive] Initialized");
    }
    
    /// <summary>
    /// Called when game starts.
    /// </summary>
    public override void OnGameStart()
    {
        base.OnGameStart();
        Debug.Log("[Game2_RushToFive] Game started - First to 5 chips wins!");
    }
    
    /// <summary>
    /// Called at the start of each player's turn.
    /// </summary>
    public override void OnTurnStart(Player currentPlayer)
    {
        base.OnTurnStart(currentPlayer);
    }
    
    // ==================== MOVE VALIDATION ====================
    
    /// <summary>
    /// Check if a move is valid in Game2_RushToFive.
    /// 
    /// Rules:
    /// - Cell must be empty (no chips placed there)
    /// - That's it - player can place on any empty cell
    /// 
    /// This is identical to Game1, but with different win condition.
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index
        if (cellIndex < 0 || cellIndex >= BoardModel.BOARD_SIZE)
        {
            Debug.LogWarning($"[Game2_RushToFive] Invalid cell index: {cellIndex}");
            return false;
        }
        
        // Check if cell is empty
        if (!IsCellEmpty(cellIndex))
        {
            Debug.Log($"[Game2_RushToFive] Cell {cellIndex} is already occupied");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Called after a chip is placed.
    /// </summary>
    public override void OnChipPlaced(Player player, int cellIndex)
    {
        base.OnChipPlaced(player, cellIndex);
        
        // Log chip placement
        int chipCount = GetChipCountForPlayer(player);
        Debug.Log($"[Game2_RushToFive] {player.PlayerName} placed chip #{chipCount}");
    }
    
    // ==================== BUMPING ====================
    
    /// <summary>
    /// Check if a bump is allowed in Game2_RushToFive.
    /// 
    /// Rules:
    /// - Bumping is NEVER allowed in this mode
    /// </summary>
    public override bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell)
    {
        // Bumping is disabled in Rush to Five
        Debug.Log("[Game2_RushToFive] Bumping is not allowed in this mode");
        return false;
    }
    
    /// <summary>
    /// Called when a bump occurs.
    /// This should never happen in Game2 since CanBump always returns false.
    /// </summary>
    public override void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        // This method should never be called in Game2
        Debug.LogWarning("[Game2_RushToFive] OnBumpOccurs called, but bumping is disabled!");
    }
    
    // ==================== WIN CONDITION ====================
    
    /// <summary>
    /// Check if a player has won in Game2_RushToFive.
    /// 
    /// Win Condition: Player has placed 5 chips on the board
    /// 
    /// Simpler than Game1 - no pattern matching needed.
    /// Just count the chips.
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        
        int chipCount = GetChipCountForPlayer(player);
        
        // Win if player has 5 or more chips on board
        if (chipCount >= 5)
        {
            Debug.Log($"[Game2_RushToFive] {player.PlayerName} has {chipCount} chips - WINS!");
            return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// Called when game ends.
    /// </summary>
    public override void OnGameEnd(Player winner)
    {
        base.OnGameEnd(winner);
        int winnerChips = GetChipCountForPlayer(winner);
        Debug.Log($"[Game2_RushToFive] Game ended! Winner: {winner.PlayerName} with {winnerChips} chips");
    }
}
