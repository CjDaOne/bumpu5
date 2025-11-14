using UnityEngine;

/// <summary>
/// Game Mode 1: Bump 5 in a Row (Standard Game)
/// 
/// Rules:
/// - Win: 5 chips in a row (horizontally or vertically on 12-cell board)
/// - Rolling a 6: LOSE TURN (no placement)
/// - 5+6 combo: SAFE ROLL (no placement, no turn loss)
/// - Doubles: Get to place AND roll again
/// - Triple doubles: Lose turn (penalty)
/// - Bumping: Remove opponent chip from board
/// - First to 5-in-a-row wins game
/// </summary>
public class Game1_Bump5 : IGameMode
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    public string ModeName => "Bump5";
    public string ModeLongName => "Bump 5 in a Row";
    public int MaxPlayers => 2;
    
    // ============================================
    // RULE CONFIGURATION PROPERTIES
    // ============================================
    
    public bool Use5InARow => true;
    public bool UseTripleDoublesPenalty => true;
    public bool Use5Plus6Safe => true;
    public bool RollingASixLosesTurn => true;
    public bool AllowBumping => true;
    
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
    /// No special logic needed for Bump5 in this hook.
    /// </summary>
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer)
    {
        // Standard game - no special initialization
    }
    
    /// <summary>
    /// Called after dice are rolled.
    /// Standard game - no special roll processing.
    /// </summary>
    public void OnDiceRolled(GameStateManager stateManager, int[] rollResult)
    {
        // Standard game - no special roll processing
    }
    
    /// <summary>
    /// Called when a chip is about to be placed.
    /// Standard game - allows all placements.
    /// </summary>
    public bool CanPlaceChip(GameStateManager stateManager, int targetCell)
    {
        // Standard game - allow placement
        return true;
    }
    
    /// <summary>
    /// Called when bumping is about to occur.
    /// Standard bump: remove opponent chip and allow placement.
    /// </summary>
    public bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell)
    {
        // Standard bump: remove opponent chip
        // Return true to allow GameStateManager to execute bump
        return true;
    }
    
    /// <summary>
    /// Called when turn ends.
    /// No special logic needed for Bump5 in this hook.
    /// </summary>
    public void OnTurnEnd(GameStateManager stateManager, Player currentPlayer)
    {
        // Standard game - no special end-of-turn logic
    }
    
    /// <summary>
    /// Called when game starts with this mode.
    /// Initialize the game state if needed.
    /// </summary>
    public void Initialize(GameStateManager stateManager)
    {
        // Standard game - no special initialization
    }
}
