using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Game1_Bump5 - The Classic Game Mode
/// 
/// Win Condition: Get 5 chips in a row (horizontal, vertical, or diagonal)
/// Bumping: Enabled - bump opponent chips off the board
/// Penalties: Standard - bumped players lose the chip
/// Difficulty: Medium
/// 
/// This is the core game mode - the one that appears in most physical Bump5 games.
/// Players take turns placing chips, trying to form 5 in a row while bumping
/// opponent pieces when strategic.
/// </summary>
public class Game1_Bump5 : GameModeBase
{
    // Mode properties
    public override string ModeName => "Bump 5";
    public override string ModeDescription => "Get 5 chips in a row to win. Bump opponent chips off the board. Standard rules apply.";
    
    // ==================== LIFECYCLE ====================
    
    /// <summary>
    /// Initialize the game mode.
    /// </summary>
    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        Debug.Log("[Game1_Bump5] Initialized");
    }
    
    /// <summary>
    /// Called when game starts.
    /// </summary>
    public override void OnGameStart()
    {
        base.OnGameStart();
        Debug.Log("[Game1_Bump5] Game started");
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
    /// Check if a move is valid in Game1_Bump5.
    /// 
    /// Rules:
    /// - Cell must be empty (no chips placed there)
    /// - That's it - player can place on any empty cell
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index against dynamic board size
        if (cellIndex < 0 || cellIndex >= BoardModel.BOARD_SIZE)
        {
            Debug.LogWarning($"[Game1_Bump5] Invalid cell index: {cellIndex}");
            return false;
        }

        // Check if cell is empty
        if (!IsCellEmpty(cellIndex))
        {
            Debug.Log($"[Game1_Bump5] Cell {cellIndex} is already occupied");
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
        
        // In Game1, no special post-placement effects
        // Just log it for debugging
    }
    
    // ==================== BUMPING ====================
    
    /// <summary>
    /// Check if a bump is allowed in Game1_Bump5.
    /// 
    /// Rules:
    /// - Bumping is always allowed
    /// - Can bump any opponent chip on the board
    /// - Cannot bump your own chips
    /// </summary>
    public override bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell)
    {
        // Can't bump yourself
        if (bumpingPlayer == targetPlayer)
        {
            Debug.Log("[Game1_Bump5] Cannot bump your own chip");
            return false;
        }
        
        // Target cell must have opponent's chip
        if (!IsCellOccupiedBy(targetCell, targetPlayer))
        {
            Debug.Log($"[Game1_Bump5] Cell {targetCell} is not occupied by target player");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Called when a bump occurs.
    /// Applies Game1-specific penalties.
    /// </summary>
    public override void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        base.OnBumpOccurs(bumpingPlayer, bumpedPlayer);
        
        // In Game1, the bump penalty is simply:
        // - Bumped chip is removed from the board (handled by GameStateManager)
        // - No additional score penalties
        
        Debug.Log($"[Game1_Bump5] {bumpingPlayer.PlayerName} bumped {bumpedPlayer.PlayerName}");
    }
    
    // ==================== WIN CONDITION ====================
    
    /// <summary>
    /// Check if a player has won in Game1_Bump5.
    /// 
    /// Win Condition: 5 chips in a row (any direction)
    /// 
    /// Patterns to check:
    /// - Horizontal: 5 chips in a row across the board
    /// - Vertical: 5 chips in a column
    /// - Diagonal: 5 chips diagonally
    /// 
    /// This requires knowing the board layout.
    /// Assuming a 3x4 grid (12 cells total):
    /// 
    ///   0   1   2   3
    ///   4   5   6   7
    ///   8   9  10  11
    /// </summary>
    /// <summary>
    /// Check if a player has won in Game1_Bump5.
    /// 
    /// Win Condition: 5 chips in a row (any direction)
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        
        int boardSize = BoardModel.BOARD_SIZE; // Use BoardModel's 5-in-a-row detection which handles the 5x5 grid
        return gameStateManager.Board.Check5InARow(player);
    }
    
    /// <summary>
    /// Called when game ends.
    /// </summary>
    public override void OnGameEnd(Player winner)
    {
        base.OnGameEnd(winner);
        Debug.Log($"[Game1_Bump5] Game ended! Winner: {winner.PlayerName}");
    }
}
