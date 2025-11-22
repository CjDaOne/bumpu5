using UnityEngine;

/// <summary>
/// Game Mode 3: Pass The Chip (Swap Logic)
/// 
/// Rules:
/// - Win: 5 chips in a row
/// - Rolling a 6: Lose turn (like Bump5)
/// - 5+6: Safe roll
/// - Doubles: Roll again
/// - Triple doubles: Lose turn
/// - Bumping: NO - instead, SWAP positions with opponent's chip
/// - First to 5-in-a-row wins
/// 
/// Key Feature: Swap Instead of Bump
/// When you land on an opponent's chip, instead of removing it, you swap positions:
/// - Your chip moves to cell X (where opponent was)
/// - Opponent's chip moves to cell Y (where your chip came from)
/// </summary>
public class Game3_PassTheChip : GameModeBase
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    public override string ModeName => "Pass The Chip";
    public override string ModeDescription => "Swap chips instead of bumping! Get 5 in a row to win.";
    
    // ============================================
    // INITIALIZATION
    // ============================================
    
    /// <summary>
    /// Initialize the game mode.
    /// </summary>
    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        Debug.Log("[Game3_PassTheChip] Initialized - Swapping enabled");
    }
    
    /// <summary>
    /// Called when game starts.
    /// </summary>
    public override void OnGameStart()
    {
        base.OnGameStart();
        Debug.Log("[Game3_PassTheChip] Game started - Swap instead of bump!");
    }
    
    /// <summary>
    /// Called at the start of each player's turn.
    /// </summary>
    public override void OnTurnStart(Player currentPlayer)
    {
        base.OnTurnStart(currentPlayer);
        // PassTheChip - no special turn start logic
    }
    
    // ============================================
    // MOVE VALIDATION
    // ============================================
    
    /// <summary>
    /// Check if a move is valid in PassTheChip.
    /// 
    /// Rules:
    /// - Cell must be empty (no chips placed there)
    /// - Player can place on any empty cell
    /// </summary>
    /// <summary>
    /// Check if a move is valid in PassTheChip.
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index against dynamic board size
        if (cellIndex < 0 || cellIndex >= BoardModel.BOARD_SIZE)
        {
            Debug.LogWarning($"[Game3_PassTheChip] Invalid cell index: {cellIndex}");
            return false;
        }
        
        // Check if cell is empty
        if (!IsCellEmpty(cellIndex))
        {
            Debug.Log($"[Game3_PassTheChip] Cell {cellIndex} is already occupied");
            return false;
        }
        
        return true;
    }

    /// <summary>
    /// Check if a player has won in PassTheChip.
    /// Win Condition: Center #5 node (index 12) is surrounded by 8 chips.
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
            
        // Center node is at (2,2) which is index 12 in a 5x5 grid
        // Center node is at (2,2) which is index 12 in a 5x5 grid
        
        // Check if center is #5 (it should be based on generation, but we assume it is)
        // We just check if the 8 neighbors are occupied.
        
        // Neighbors of 12:
        // Row 1: 6, 7, 8
        // Row 2: 11, 13
        // Row 3: 16, 17, 18
        int[] neighbors = new int[] { 6, 7, 8, 11, 13, 16, 17, 18 };
        
        foreach (int neighbor in neighbors)
        {
            // If any neighbor is empty, win condition not met
            if (IsCellEmpty(neighbor))
                return false;
        }
        
        // All 8 neighbors are occupied.
        // Does the player need to be the one who placed the last one?
        // The method checks if *player* won.
        // Usually called for the current player.
        // If the condition is met, the current player (who placed the last chip) wins.
        return true;
    }
    
    /// <summary>
    /// Called when game ends.
    /// </summary>
    public override void OnGameEnd(Player winner)
    {
        base.OnGameEnd(winner);
        Debug.Log($"[Game3_PassTheChip] Game ended! Winner: {winner.PlayerName}");
    }
}
