using UnityEngine;

/// <summary>
/// Game Mode 2: Krazy 6 (Modified Rules)
/// 
/// Rules:
/// - Win: 5 chips in a row (same as Bump5)
/// - Rolling a 6: GOOD ROLL! (don't lose turn, bonus)
/// - Double 6s: Extra bonus movement
/// - 5+6: Still a safe roll (but 6 is good, so different interpretation)
/// - Doubles: Roll again (like Bump5)
/// - Triple doubles: Still lose turn
/// - Bumping: Yes, remove opponent chip
/// - First to 5-in-a-row wins
/// 
/// Key Difference from Bump5:
/// Rolling a 6 is actually good luck - you get to place AND get a bonus.
/// </summary>
public class Game2_Krazy6 : IGameMode
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    public string ModeName => "Krazy6";
    public string ModeLongName => "Krazy 6";
    public int MaxPlayers => 2;
    
    // ============================================
    // RULE CONFIGURATION PROPERTIES
    // ============================================
    
    public bool Use5InARow => true;
    public bool UseTripleDoublesPenalty => true;
    public bool Use5Plus6Safe => true;
    public bool RollingASixLosesTurn => false;  // KEY DIFFERENCE from Bump5
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
    /// No special logic needed for this hook.
    /// </summary>
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer)
    {
        // Krazy 6 - no special initialization
    }
    
    /// <summary>
    /// Called after dice are rolled.
    /// Krazy 6: Check if we rolled any 6s and apply bonuses.
    /// </summary>
    public void OnDiceRolled(GameStateManager stateManager, int[] rollResult)
    {
        if (rollResult == null || rollResult.Length < 2)
            return;
        
        // Check if we rolled any 6s
        bool hasSingleSix = (rollResult[0] == 6 && rollResult[1] != 6) || 
                           (rollResult[1] == 6 && rollResult[0] != 6);
        bool hasDouble6 = (rollResult[0] == 6 && rollResult[1] == 6);
        
        // Krazy 6 benefits from rolling 6s - no turn loss
        // The GameStateManager will not apply turn loss due to RollingASixLosesTurn property
        
        // Note: Bonus movement/tokens would be applied here if tracked
        if (hasDouble6)
        {
            // Double 6 could grant extra movement or bonus tokens
            // Implementation would depend on GameStateManager support
        }
        else if (hasSingleSix)
        {
            // Single 6 grants bonus
            // Implementation would depend on GameStateManager support
        }
    }
    
    /// <summary>
    /// Called when a chip is about to be placed.
    /// Krazy 6 - allows all placements.
    /// </summary>
    public bool CanPlaceChip(GameStateManager stateManager, int targetCell)
    {
        // Krazy 6 - allow placement
        return true;
    }
    
    /// <summary>
    /// Called when bumping is about to occur.
    /// Krazy 6 still allows bumping like Bump5.
    /// </summary>
    public bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell)
    {
        // Krazy 6 - standard bump behavior
        return true;
    }
    
    /// <summary>
    /// Called when turn ends.
    /// No special logic needed for this hook.
    /// </summary>
    public void OnTurnEnd(GameStateManager stateManager, Player currentPlayer)
    {
        // Krazy 6 - no special end-of-turn logic
    }
    
    /// <summary>
    /// Called when game starts with this mode.
    /// Initialize the game state if needed.
    /// </summary>
    public void Initialize(GameStateManager stateManager)
    {
        // Krazy 6 - no special initialization
    }
}
