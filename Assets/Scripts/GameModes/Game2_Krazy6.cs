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
public class Game2_Krazy6 : GameModeBase
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    public override string ModeName => "Krazy 6";
    public override string ModeDescription => "Rolling a 6 is good luck! Get 5 chips in a row to win. Bumping allowed.";
    
    // ============================================
    // INITIALIZATION
    // ============================================
    
    /// <summary>
    /// Initialize the game mode.
    /// </summary>
    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        Debug.Log("[Game2_Krazy6] Initialized - 6s are good!");
    }
    
    /// <summary>
    /// Called when game starts.
    /// </summary>
    public override void OnGameStart()
    {
        base.OnGameStart();
        Debug.Log("[Game2_Krazy6] Game started - Rolling 6s is beneficial!");
    }
    
    /// <summary>
    /// Called at the start of each player's turn.
    /// </summary>
    public override void OnTurnStart(Player currentPlayer)
    {
        base.OnTurnStart(currentPlayer);
        // Krazy 6 - no special turn start logic
    }
    
    // ============================================
    // MOVE VALIDATION
    // ============================================
    
    /// <summary>
    /// Check if a move is valid in Krazy6.
    /// 
    /// Rules:
    /// - Cell must be empty (no chips placed there)
    /// - Player can place on any empty cell
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index
        if (cellIndex < 0 || cellIndex > 11)
        {
            Debug.LogWarning($"[Game2_Krazy6] Invalid cell index: {cellIndex}");
            return false;
        }
        
        // Check if cell is empty
        if (!IsCellEmpty(cellIndex))
        {
            Debug.Log($"[Game2_Krazy6] Cell {cellIndex} is already occupied");
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
        // No special post-placement effects in Krazy6
    }
    
    // ============================================
    // BUMPING
    // ============================================
    
    /// <summary>
    /// Check if a bump is allowed in Krazy6.
    /// 
    /// Rules (same as Bump5):
    /// - Bumping is always allowed
    /// - Can bump any opponent chip on the board
    /// - Cannot bump your own chips
    /// </summary>
    public override bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell)
    {
        // Can't bump yourself
        if (bumpingPlayer == targetPlayer)
        {
            Debug.Log("[Game2_Krazy6] Cannot bump your own chip");
            return false;
        }
        
        // Target cell must have opponent's chip
        if (!IsCellOccupiedBy(targetCell, targetPlayer))
        {
            Debug.Log($"[Game2_Krazy6] Cell {targetCell} is not occupied by target player");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Called when a bump occurs.
    /// </summary>
    public override void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        base.OnBumpOccurs(bumpingPlayer, bumpedPlayer);
        Debug.Log($"[Game2_Krazy6] {bumpingPlayer.PlayerName} bumped {bumpedPlayer.PlayerName}");
    }
    
    // ============================================
    // WIN CONDITION
    // ============================================
    
    /// <summary>
    /// Check if a player has won in Krazy6.
    /// 
    /// Win Condition: 5 chips in a row (same as Bump5)
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        
        // Use BoardModel's 5-in-a-row detection
        return gameStateManager.Board.Check5InARow(player);
    }
    
    /// <summary>
    /// Called when game ends.
    /// </summary>
    public override void OnGameEnd(Player winner)
    {
        base.OnGameEnd(winner);
        Debug.Log($"[Game2_Krazy6] Game ended! Winner: {winner.PlayerName}");
    }
}
