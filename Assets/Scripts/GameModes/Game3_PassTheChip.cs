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
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index
        if (cellIndex < 0 || cellIndex > 11)
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
    /// Called after a chip is placed.
    /// </summary>
    public override void OnChipPlaced(Player player, int cellIndex)
    {
        base.OnChipPlaced(player, cellIndex);
        // No special post-placement effects
    }
    
    // ============================================
    // BUMPING (SWAPPING)
    // ============================================
    
    /// <summary>
    /// Check if a "bump" (swap) is allowed in PassTheChip.
    /// 
    /// Rules:
    /// - Swapping is allowed instead of bumping
    /// - Can swap with any opponent chip
    /// - Cannot swap your own chips
    /// </summary>
    public override bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell)
    {
        // In PassTheChip, we swap instead of bump
        // Same validation as bump, but different action
        
        // Can't swap with yourself
        if (bumpingPlayer == targetPlayer)
        {
            Debug.Log("[Game3_PassTheChip] Cannot swap your own chip");
            return false;
        }
        
        // Target cell must have opponent's chip
        if (!IsCellOccupiedBy(targetCell, targetPlayer))
        {
            Debug.Log($"[Game3_PassTheChip] Cell {targetCell} is not occupied by target player");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Called when a "bump" (swap) occurs.
    /// In PassTheChip, we swap positions instead of removing the chip.
    /// </summary>
    public override void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        base.OnBumpOccurs(bumpingPlayer, bumpedPlayer);
        
        // Note: The actual swap logic would be handled by GameStateManager
        // This hook just logs the action
        Debug.Log($"[Game3_PassTheChip] {bumpingPlayer.PlayerName} swapped with {bumpedPlayer.PlayerName}");
    }
    
    // ============================================
    // WIN CONDITION
    // ============================================
    
    /// <summary>
    /// Check if a player has won in PassTheChip.
    /// 
    /// Win Condition: 5 chips in a row
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
        Debug.Log($"[Game3_PassTheChip] Game ended! Winner: {winner.PlayerName}");
    }
}
