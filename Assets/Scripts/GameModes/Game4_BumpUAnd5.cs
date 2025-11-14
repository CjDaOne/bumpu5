using UnityEngine;

/// <summary>
/// Game Mode 4: Bump U & 5 (Hybrid Mode)
/// 
/// Combines Bump5 + Krazy6:
/// - Rolling a 6: GOOD (from Krazy6)
/// - Double 6s: Extra bonus movement (from Krazy6)
/// - 5+6: Safe roll (from Bump5)
/// - Doubles: Roll again
/// - Triple doubles: Lose turn
/// - Bumping: Remove opponent chip (from Bump5)
/// - Win: 5 in a row
/// 
/// Why This Mode?
/// Players who like both modes can play them together!
/// Hybrid game combining the favorable rolling rules from Krazy6 with the bumping mechanics of Bump5.
/// </summary>
public class Game4_BumpUAnd5 : IGameMode
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    public string ModeName => "BumpUAnd5";
    public string ModeLongName => "Bump U & 5";
    public int MaxPlayers => 2;
    
    // ============================================
    // RULE CONFIGURATION PROPERTIES
    // ============================================
    
    public bool Use5InARow => true;
    public bool UseTripleDoublesPenalty => true;
    public bool Use5Plus6Safe => true;
    public bool RollingASixLosesTurn => false;  // From Krazy6 - rolling 6 is GOOD
    public bool AllowBumping => true;           // From Bump5 - bumping enabled
    
    // ============================================
    // WIN CONDITION CHECK
    // ============================================
    
    /// <summary>
    /// Checks if the given player has won by getting 5 chips in a row.
    /// Hybrid mode uses standard 5-in-a-row win condition.
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
    /// No special initialization needed.
    /// </summary>
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer)
    {
        // Hybrid mode - no special turn start logic
    }
    
    /// <summary>
    /// Called after dice are rolled.
    /// Hybrid mode: Apply Krazy6's rolling bonus logic.
    /// Rolling a 6 is good (no turn loss), similar to Krazy6.
    /// </summary>
    public void OnDiceRolled(GameStateManager stateManager, int[] rollResult)
    {
        if (rollResult == null || rollResult.Length < 2)
            return;
        
        // Check if we rolled any 6s
        bool hasSingleSix = (rollResult[0] == 6 && rollResult[1] != 6) || 
                           (rollResult[1] == 6 && rollResult[0] != 6);
        bool hasDouble6 = (rollResult[0] == 6 && rollResult[1] == 6);
        
        // Hybrid mode benefits from rolling 6s (like Krazy6)
        // No turn loss due to RollingASixLosesTurn property
        
        // Note: Bonus movement/tokens would be applied here if tracked
        if (hasDouble6)
        {
            // Double 6 grants extra bonus
        }
        else if (hasSingleSix)
        {
            // Single 6 grants bonus
        }
    }
    
    /// <summary>
    /// Called when a chip is about to be placed.
    /// Hybrid mode - allows all placements.
    /// </summary>
    public bool CanPlaceChip(GameStateManager stateManager, int targetCell)
    {
        // Hybrid mode - allow placement
        return true;
    }
    
    /// <summary>
    /// Called when bumping is about to occur.
    /// Hybrid mode: Use Bump5's bump logic (remove opponent chip).
    /// </summary>
    public bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell)
    {
        // Hybrid mode - standard bump behavior from Bump5
        return true;
    }
    
    /// <summary>
    /// Called when turn ends.
    /// No special end-of-turn logic needed.
    /// </summary>
    public void OnTurnEnd(GameStateManager stateManager, Player currentPlayer)
    {
        // Hybrid mode - no special end-of-turn logic
    }
    
    /// <summary>
    /// Called when game starts with this mode.
    /// Initialize the game state if needed.
    /// </summary>
    public void Initialize(GameStateManager stateManager)
    {
        // Hybrid mode - no special initialization
    }
}
