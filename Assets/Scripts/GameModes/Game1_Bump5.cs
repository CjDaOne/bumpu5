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
        // Validate cell index
        if (cellIndex < 0 || cellIndex > 11)
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
    public override bool CheckWinCondition(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        
        int[] playerCells = GetCellsOccupiedBy(player);
        
        if (playerCells.Length < 5)
            return false; // Can't have 5-in-a-row with less than 5 chips
        
        // Check all possible 5-in-a-row patterns
        // Horizontal patterns
        if (CheckHorizontalWin(playerCells)) return true;
        
        // Vertical patterns
        if (CheckVerticalWin(playerCells)) return true;
        
        // Diagonal patterns (top-left to bottom-right)
        if (CheckDiagonalWinLR(playerCells)) return true;
        
        // Diagonal patterns (top-right to bottom-left)
        if (CheckDiagonalWinRL(playerCells)) return true;
        
        return false;
    }
    
    /// <summary>
    /// Check for horizontal 5-in-a-row.
    /// Board layout (3x4 grid):
    ///   0   1   2   3
    ///   4   5   6   7
    ///   8   9  10  11
    /// </summary>
    private bool CheckHorizontalWin(int[] playerCells)
    {
        // Check each row for 5 in a row
        // Row 1: Check patterns starting from 0, 1
        if (Contains(playerCells, 0) && Contains(playerCells, 1) && Contains(playerCells, 2) && Contains(playerCells, 3))
            return true; // Pattern: 0-1-2-3
        
        // Row 2: Check patterns starting from 4, 5
        if (Contains(playerCells, 4) && Contains(playerCells, 5) && Contains(playerCells, 6) && Contains(playerCells, 7))
            return true; // Pattern: 4-5-6-7
        
        // Row 3: Check patterns starting from 8, 9
        if (Contains(playerCells, 8) && Contains(playerCells, 9) && Contains(playerCells, 10) && Contains(playerCells, 11))
            return true; // Pattern: 8-9-10-11
        
        return false;
    }
    
    /// <summary>
    /// Check for vertical 5-in-a-row.
    /// Board layout (3x4 grid):
    ///   0   1   2   3
    ///   4   5   6   7
    ///   8   9  10  11
    /// </summary>
    private bool CheckVerticalWin(int[] playerCells)
    {
        // Column 1: 0, 4, 8
        if (Contains(playerCells, 0) && Contains(playerCells, 4) && Contains(playerCells, 8))
            return true;
        
        // Column 2: 1, 5, 9
        if (Contains(playerCells, 1) && Contains(playerCells, 5) && Contains(playerCells, 9))
            return true;
        
        // Column 3: 2, 6, 10
        if (Contains(playerCells, 2) && Contains(playerCells, 6) && Contains(playerCells, 10))
            return true;
        
        // Column 4: 3, 7, 11
        if (Contains(playerCells, 3) && Contains(playerCells, 7) && Contains(playerCells, 11))
            return true;
        
        return false;
    }
    
    /// <summary>
    /// Check for diagonal 5-in-a-row (top-left to bottom-right).
    /// Board layout (3x4 grid):
    ///   0   1   2   3
    ///   4   5   6   7
    ///   8   9  10  11
    /// </summary>
    private bool CheckDiagonalWinLR(int[] playerCells)
    {
        // Diagonal 1: 0, 5, 10
        if (Contains(playerCells, 0) && Contains(playerCells, 5) && Contains(playerCells, 10))
            return true;
        
        // Diagonal 2: 1, 6, 11
        if (Contains(playerCells, 1) && Contains(playerCells, 6) && Contains(playerCells, 11))
            return true;
        
        // Diagonal 3: 4, 9
        // (Only 2 cells, not a 5-in-a-row)
        
        return false;
    }
    
    /// <summary>
    /// Check for diagonal 5-in-a-row (top-right to bottom-left).
    /// Board layout (3x4 grid):
    ///   0   1   2   3
    ///   4   5   6   7
    ///   8   9  10  11
    /// </summary>
    private bool CheckDiagonalWinRL(int[] playerCells)
    {
        // Diagonal 1: 3, 6, 9
        if (Contains(playerCells, 3) && Contains(playerCells, 6) && Contains(playerCells, 9))
            return true;
        
        // Diagonal 2: 2, 5, 8
        if (Contains(playerCells, 2) && Contains(playerCells, 5) && Contains(playerCells, 8))
            return true;
        
        // Diagonal 3: 1, 4
        // (Only 2 cells, not a 5-in-a-row)
        
        return false;
    }
    
    /// <summary>
    /// Helper: Check if an array contains a value.
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
        Debug.Log($"[Game1_Bump5] Game ended! Winner: {winner.PlayerName}");
    }
}
