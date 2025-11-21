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
public class Game4_BumpUAnd5 : GameModeBase
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    public override string ModeName => "Bump U & 5";
    public override string ModeDescription => "Hybrid mode combining Bump5 bumping mechanics with Krazy6 favorable rolling rules. Win by getting 5 chips in a row.";
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>
    /// Initialize the game mode.
    /// </summary>
    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        Debug.Log("[Game4_BumpUAnd5] Initialized - Hybrid mode active");
    }
    
    /// <summary>
    /// Called when game starts.
    /// </summary>
    public override void OnGameStart()
    {
        base.OnGameStart();
        Debug.Log("[Game4_BumpUAnd5] Game started - Rolling 6s is beneficial!");
    }
    
    /// <summary>
    /// Called at the start of each player's turn.
    /// </summary>
    public override void OnTurnStart(Player currentPlayer)
    {
        base.OnTurnStart(currentPlayer);
    }
    
    // ============================================
    // MOVE VALIDATION
    // ============================================
    
    /// <summary>
    /// Check if a move is valid in Game4_BumpUAnd5.
    /// 
    /// Rules (same as Bump5):
    /// - Cell must be empty (no chips placed there)
    /// - Player can place on any empty cell
    /// </summary>
    /// <summary>
    /// Check if a move is valid in Game4_BumpUAnd5.
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index against dynamic board size
        if (cellIndex < 0 || cellIndex >= BoardModel.BOARD_SIZE)
        {
            Debug.LogWarning($"[Game4_BumpUAnd5] Invalid cell index: {cellIndex}");
            return false;
        }
        
        // Check if cell is empty
        if (!IsCellEmpty(cellIndex))
        {
            Debug.Log($"[Game4_BumpUAnd5] Cell {cellIndex} is already occupied");
            return false;
        }
        
        return true;
    }

    /// <summary>
    /// Check if a player has won in Game4_BumpUAnd5.
    /// Win Condition: 5 chips on the game board in any order.
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        
        int chipCount = GetChipCountForPlayer(player);
        return chipCount >= 5;
    }
    
    /// <summary>
    /// Check for horizontal 5-in-a-row.
    /// </summary>
    private bool CheckHorizontalWin(int[] playerCells)
    {
        if (Contains(playerCells, 0) && Contains(playerCells, 1) && Contains(playerCells, 2) && Contains(playerCells, 3))
            return true;
        
        if (Contains(playerCells, 4) && Contains(playerCells, 5) && Contains(playerCells, 6) && Contains(playerCells, 7))
            return true;
        
        if (Contains(playerCells, 8) && Contains(playerCells, 9) && Contains(playerCells, 10) && Contains(playerCells, 11))
            return true;
        
        return false;
    }
    
    /// <summary>
    /// Check for vertical 5-in-a-row.
    /// </summary>
    private bool CheckVerticalWin(int[] playerCells)
    {
        if (Contains(playerCells, 0) && Contains(playerCells, 4) && Contains(playerCells, 8))
            return true;
        
        if (Contains(playerCells, 1) && Contains(playerCells, 5) && Contains(playerCells, 9))
            return true;
        
        if (Contains(playerCells, 2) && Contains(playerCells, 6) && Contains(playerCells, 10))
            return true;
        
        if (Contains(playerCells, 3) && Contains(playerCells, 7) && Contains(playerCells, 11))
            return true;
        
        return false;
    }
    
    /// <summary>
    /// Check for diagonal 5-in-a-row (top-left to bottom-right).
    /// </summary>
    private bool CheckDiagonalWinLR(int[] playerCells)
    {
        if (Contains(playerCells, 0) && Contains(playerCells, 5) && Contains(playerCells, 10))
            return true;
        
        if (Contains(playerCells, 1) && Contains(playerCells, 6) && Contains(playerCells, 11))
            return true;
        
        return false;
    }
    
    /// <summary>
    /// Check for diagonal 5-in-a-row (top-right to bottom-left).
    /// </summary>
    private bool CheckDiagonalWinRL(int[] playerCells)
    {
        if (Contains(playerCells, 3) && Contains(playerCells, 6) && Contains(playerCells, 9))
            return true;
        
        if (Contains(playerCells, 2) && Contains(playerCells, 5) && Contains(playerCells, 8))
            return true;
        
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
        Debug.Log($"[Game4_BumpUAnd5] Game ended! Winner: {winner.PlayerName}");
    }
}
