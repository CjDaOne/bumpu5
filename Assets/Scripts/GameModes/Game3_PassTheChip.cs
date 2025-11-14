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
public class Game3_PassTheChip : IGameMode
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    public string ModeName => "PassTheChip";
    public string ModeLongName => "Pass The Chip";
    public int MaxPlayers => 2;
    
    // ============================================
    // RULE CONFIGURATION PROPERTIES
    // ============================================
    
    public bool Use5InARow => true;
    public bool UseTripleDoublesPenalty => true;
    public bool Use5Plus6Safe => true;
    public bool RollingASixLosesTurn => true;
    public bool AllowBumping => false;  // No bumping in this mode - swap instead
    
    // Track the last chip movement for swap calculations
    private int lastChipSourceCell = -1;
    private Player lastChipMovingPlayer = null;
    
    // ============================================
    // WIN CONDITION CHECK
    // ============================================
    
    /// <summary>
    /// Checks if the given player has won by getting 5 chips in a row.
    /// </summary>
    public bool CheckWinCondition(Player player, BoardModel board)
    {
        if (player == null || board == null)
            return false;
        
        // Delegate to BoardModel's 5-in-a-row detection
        return board.Check5InARow(player);
    }
    
    // ============================================
    // SPECIAL RULES HOOKS
    // ============================================
    
    /// <summary>
    /// Called at the start of each turn.
    /// Reset chip tracking for this turn.
    /// </summary>
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer)
    {
        // Reset tracking for new turn
        lastChipSourceCell = -1;
        lastChipMovingPlayer = null;
    }
    
    /// <summary>
    /// Called after dice are rolled.
    /// No special roll processing needed.
    /// </summary>
    public void OnDiceRolled(GameStateManager stateManager, int[] rollResult)
    {
        // No special roll processing
    }
    
    /// <summary>
    /// Called when a chip is about to be placed.
    /// Track where the chip is coming from for swap logic.
    /// </summary>
    public bool CanPlaceChip(GameStateManager stateManager, int targetCell)
    {
        // Store the current chip position before moving
        // Note: This would need coordination with GameStateManager to know source cell
        // For now, allow the placement
        return true;
    }
    
    /// <summary>
    /// Called when bumping is about to occur.
    /// Instead of removing opponent chip, swap positions.
    /// </summary>
    public bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell)
    {
        if (stateManager == null || bumperPlayer == null || lastChipSourceCell < 0)
            return false;
        
        // Implement swap logic
        // Note: This would need BoardModel support for GetChipAt, RemoveChip, PlaceChip
        // For now, return false to indicate we handled the bump (mode-specific)
        return false;  // Return false = "I handled it, don't do standard bump"
    }
    
    /// <summary>
    /// Called when turn ends.
    /// Clean up tracking.
    /// </summary>
    public void OnTurnEnd(GameStateManager stateManager, Player currentPlayer)
    {
        // Clean up tracking
        lastChipSourceCell = -1;
        lastChipMovingPlayer = null;
    }
    
    /// <summary>
    /// Called when game starts with this mode.
    /// Initialize tracking state.
    /// </summary>
    public void Initialize(GameStateManager stateManager)
    {
        // Initialize tracking
        lastChipSourceCell = -1;
        lastChipMovingPlayer = null;
    }
    
    /// <summary>
    /// Helper method to execute chip swap.
    /// Swaps positions between two players' chips.
    /// </summary>
    private void SwapChips(Player player1, int cell1, Player player2, int cell2, BoardModel board)
    {
        if (board == null)
            return;
        
        // This would need BoardModel support:
        // 1. Remove chip from cell2 (opponent's chip)
        // 2. Place player1's chip at cell2
        // 3. Place opponent's chip at cell1
        // 
        // Pseudocode:
        // Chip opponentChip = board.GetChipAt(cell2);
        // board.RemoveChip(cell2);
        // board.PlaceChip(cell2, player1Chip);
        // board.PlaceChip(cell1, opponentChip);
    }
}
