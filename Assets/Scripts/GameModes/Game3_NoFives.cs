using UnityEngine;

/// <summary>
/// Game3_NoFives - Reverse-Win Strategic Mode
/// 
/// Win Condition: Force opponent to create 5-in-a-row (they lose if they do)
/// Bumping: Enabled - bump opponent chips strategically
/// Penalties: Increased - dangerous positions incur penalties
/// Difficulty: Hard - requires strategic planning
/// 
/// This is a reverse-win mode where creating 5-in-a-row is BAD.
/// Players try to force their opponent into creating 5-in-a-row.
/// Highly strategic - experienced players mode.
/// </summary>
public class Game3_NoFives : GameModeBase
{
    // Mode properties
    public override string ModeName => "No Fives";
    public override string ModeDescription => "Force opponent to create 5 in a row - they lose if they do! Bumping enabled. Strategic mode.";
    
    // ==================== LIFECYCLE ====================
    
    /// <summary>
    /// Initialize the game mode.
    /// </summary>
    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        Debug.Log("[Game3_NoFives] Initialized");
    }
    
    /// <summary>
    /// Called when game starts.
    /// </summary>
    public override void OnGameStart()
    {
        base.OnGameStart();
        Debug.Log("[Game3_NoFives] Game started - Force opponent into 5-in-a-row!");
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
    /// Check if a move is valid in Game3_NoFives.
    /// 
    /// Rules:
    /// - Cell must be empty
    /// - Player CANNOT place chip if it would create 5-in-a-row for them
    ///   (because that would mean they lose!)
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index
        if (cellIndex < 0 || cellIndex > 11)
        {
            Debug.LogWarning($"[Game3_NoFives] Invalid cell index: {cellIndex}");
            return false;
        }
        
        // Check if cell is empty
        if (!IsCellEmpty(cellIndex))
        {
            Debug.Log($"[Game3_NoFives] Cell {cellIndex} is already occupied");
            return false;
        }
        
        // Check if placing here would create 5-in-a-row for this player
        // If so, REJECT the move (they can't place it)
        if (WouldCreateFiveInARow(player, cellIndex))
        {
            Debug.Log($"[Game3_NoFives] {player.PlayerName} cannot place at {cellIndex} - would create 5-in-a-row!");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Helper method: Check if placing a chip would create 5-in-a-row.
    /// </summary>
    private bool WouldCreateFiveInARow(Player player, int cellIndex)
    {
        // Get all cells currently occupied by this player
        int[] playerCells = GetCellsOccupiedBy(player);
        
        // We would need to simulate adding cellIndex and check for 5-in-a-row
        // For now, this is a simplified placeholder
        // In production, would need full win detection logic
        
        return false; // Placeholder
    }
    
    /// <summary>
    /// Called after a chip is placed.
    /// </summary>
    public override void OnChipPlaced(Player player, int cellIndex)
    {
        base.OnChipPlaced(player, cellIndex);
        Debug.Log($"[Game3_NoFives] {player.PlayerName} placed chip at {cellIndex}");
    }
    
    // ==================== BUMPING ====================
    
    /// <summary>
    /// Check if a bump is allowed in Game3_NoFives.
    /// 
    /// Rules:
    /// - Bumping is always allowed
    /// - Can bump any opponent chip
    /// - Used strategically to prevent opponent from winning
    /// </summary>
    public override bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell)
    {
        // Can't bump yourself
        if (bumpingPlayer == targetPlayer)
        {
            Debug.Log("[Game3_NoFives] Cannot bump your own chip");
            return false;
        }
        
        // Target cell must have opponent's chip
        if (!IsCellOccupiedBy(targetCell, targetPlayer))
        {
            Debug.Log($"[Game3_NoFives] Cell {targetCell} is not occupied by target player");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Called when a bump occurs.
    /// Applies Game3-specific effects.
    /// </summary>
    public override void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        base.OnBumpOccurs(bumpingPlayer, bumpedPlayer);
        
        // In Game3, bump removes the opponent's chip
        // This is strategic - might prevent opponent from winning
        Debug.Log($"[Game3_NoFives] {bumpingPlayer.PlayerName} bumped {bumpedPlayer.PlayerName}");
    }
    
    // ==================== WIN CONDITION ====================
    
    /// <summary>
    /// Check if a player has won in Game3_NoFives.
    /// 
    /// Win Condition: OPPONENT has created 5-in-a-row
    /// (So if player1 creates 5-in-a-row, player2 wins)
    /// 
    /// This requires checking if opponent has 5-in-a-row.
    /// The "win" is actually when the other player loses.
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        if (gameState == null || gameState.Board == null)
            return false;
        
        // Get the other player (opponent)
        Player opponent = GetOpponentPlayer(player);
        if (opponent == null)
            return false;
        
        // Check if OPPONENT has 5-in-a-row
        // If so, this player (current player) WINS
        if (OpponentHasFiveInARow(opponent))
        {
            Debug.Log($"[Game3_NoFives] {player.PlayerName} wins! {opponent.PlayerName} created 5-in-a-row!");
            return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// Helper: Get the opponent player (the other player).
    /// </summary>
    private Player GetOpponentPlayer(Player player)
    {
        if (gameState == null || gameState.Players == null)
            return null;
        
        // Assuming 2-player game, return the other player
        foreach (Player p in gameState.Players)
        {
            if (p != player)
                return p;
        }
        
        return null;
    }
    
    /// <summary>
    /// Helper: Check if opponent has 5-in-a-row.
    /// </summary>
    private bool OpponentHasFiveInARow(Player opponent)
    {
        int[] opponentCells = GetCellsOccupiedBy(opponent);
        
        if (opponentCells.Length < 5)
            return false;
        
        // Check all possible 5-in-a-row patterns
        if (CheckHorizontalWin(opponentCells)) return true;
        if (CheckVerticalWin(opponentCells)) return true;
        if (CheckDiagonalWinLR(opponentCells)) return true;
        if (CheckDiagonalWinRL(opponentCells)) return true;
        
        return false;
    }
    
    /// <summary>
    /// Check for horizontal 5-in-a-row.
    /// </summary>
    private bool CheckHorizontalWin(int[] cells)
    {
        if (Contains(cells, 0) && Contains(cells, 1) && Contains(cells, 2) && Contains(cells, 3))
            return true;
        if (Contains(cells, 4) && Contains(cells, 5) && Contains(cells, 6) && Contains(cells, 7))
            return true;
        if (Contains(cells, 8) && Contains(cells, 9) && Contains(cells, 10) && Contains(cells, 11))
            return true;
        return false;
    }
    
    /// <summary>
    /// Check for vertical 5-in-a-row.
    /// </summary>
    private bool CheckVerticalWin(int[] cells)
    {
        if (Contains(cells, 0) && Contains(cells, 4) && Contains(cells, 8))
            return true;
        if (Contains(cells, 1) && Contains(cells, 5) && Contains(cells, 9))
            return true;
        if (Contains(cells, 2) && Contains(cells, 6) && Contains(cells, 10))
            return true;
        if (Contains(cells, 3) && Contains(cells, 7) && Contains(cells, 11))
            return true;
        return false;
    }
    
    /// <summary>
    /// Check for diagonal 5-in-a-row (left-right).
    /// </summary>
    private bool CheckDiagonalWinLR(int[] cells)
    {
        if (Contains(cells, 0) && Contains(cells, 5) && Contains(cells, 10))
            return true;
        if (Contains(cells, 1) && Contains(cells, 6) && Contains(cells, 11))
            return true;
        return false;
    }
    
    /// <summary>
    /// Check for diagonal 5-in-a-row (right-left).
    /// </summary>
    private bool CheckDiagonalWinRL(int[] cells)
    {
        if (Contains(cells, 3) && Contains(cells, 6) && Contains(cells, 9))
            return true;
        if (Contains(cells, 2) && Contains(cells, 5) && Contains(cells, 8))
            return true;
        return false;
    }
    
    /// <summary>
    /// Helper: Check if array contains value.
    /// </summary>
    private bool Contains(int[] array, int value)
    {
        foreach (int item in array)
        {
            if (item == value)
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
        Debug.Log($"[Game3_NoFives] Game ended! Winner: {winner.PlayerName} (opponent created 5-in-a-row)");
    }
}
