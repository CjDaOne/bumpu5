using System;
using UnityEngine;

/// <summary>
/// Game Mode 5: Solitary (Single-Player Puzzle Mode)
/// 
/// Rules:
/// - Single player mode
/// - Goal: Place chips to create 5-in-a-row as fast as possible
/// - Rolling: Normal dice rolls
/// - Win: Get 5 in a row
/// - Tracking: Best time, fewest rolls, etc.
/// - No opponent bumping
/// 
/// Why This Mode?
/// Puzzle mode - relax and try to achieve 5-in-a-row. Can be timed.
/// </summary>
public class Game5_Solitary : IGameMode
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    public string ModeName => "Solitary";
    public string ModeLongName => "Solitary Puzzle";
    public int MaxPlayers => 1;  // Single player only
    
    // ============================================
    // RULE CONFIGURATION PROPERTIES
    // ============================================
    
    public bool Use5InARow => true;
    public bool UseTripleDoublesPenalty => false;  // Single player - no penalty
    public bool Use5Plus6Safe => false;            // Single player - no special combos
    public bool RollingASixLosesTurn => false;     // Single player - always roll again
    public bool AllowBumping => false;             // Single player - no opponents
    
    // ============================================
    // TRACKING STATE
    // ============================================
    
    private int rollCount = 0;
    private DateTime startTime = DateTime.MinValue;
    private bool isInitialized = false;
    
    /// <summary>Gets the number of rolls made so far in this game.</summary>
    public int RollCount
    {
        get { return rollCount; }
    }
    
    /// <summary>Gets the elapsed time since game started.</summary>
    public TimeSpan ElapsedTime
    {
        get
        {
            if (!isInitialized)
                return TimeSpan.Zero;
            return DateTime.Now - startTime;
        }
    }
    
    // ============================================
    // WIN CONDITION CHECK
    // ============================================
    
    /// <summary>
    /// Checks if the player has won by getting 5 chips in a row.
    /// Single player mode - only one player to check.
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
    /// No player rotation in solitary mode.
    /// </summary>
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer)
    {
        // Single player - no special turn start logic
    }
    
    /// <summary>
    /// Called after dice are rolled.
    /// Track roll count for stats.
    /// </summary>
    public void OnDiceRolled(GameStateManager stateManager, int[] rollResult)
    {
        // Increment roll counter
        rollCount++;
    }
    
    /// <summary>
    /// Called when a chip is about to be placed.
    /// Single player - allow all placements.
    /// </summary>
    public bool CanPlaceChip(GameStateManager stateManager, int targetCell)
    {
        // Single player - allow placement
        return true;
    }
    
    /// <summary>
    /// Called when bumping is about to occur.
    /// Single player - no bumping (no opponents).
    /// </summary>
    public bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell)
    {
        // Single player - no bumping
        return false;
    }
    
    /// <summary>
    /// Called when turn ends.
    /// No player rotation in solitary - game loops for single player until win.
    /// </summary>
    public void OnTurnEnd(GameStateManager stateManager, Player currentPlayer)
    {
        // Single player - no turn rotation
    }
    
    /// <summary>
    /// Called when game starts with this mode.
    /// Initialize tracking for roll count and elapsed time.
    /// </summary>
    public void Initialize(GameStateManager stateManager)
    {
        rollCount = 0;
        startTime = DateTime.Now;
        isInitialized = true;
    }
}
