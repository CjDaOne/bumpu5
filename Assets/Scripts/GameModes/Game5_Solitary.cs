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
public class Game5_Solitary : GameModeBase
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    public override string ModeName => "Solitary";
    public override string ModeDescription => "Single-player puzzle mode. Place chips to get 5 in a row as fast as possible. Track your time and roll count!";
    
    // ============================================
    // TRACKING STATE
    // ============================================
    
    private int rollCount = 0;
    private DateTime startTime = DateTime.MinValue;
    
    /// <summary>Gets the number of rolls made so far in this game.</summary>
    public int RollCount => rollCount;
    
    /// <summary>Gets the elapsed time since game started.</summary>
    public TimeSpan ElapsedTime
    {
        get
        {
            if (startTime == DateTime.MinValue)
                return TimeSpan.Zero;
            return DateTime.Now - startTime;
        }
    }
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>
    /// Initialize the game mode.
    /// Track roll count and elapsed time.
    /// </summary>
    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        rollCount = 0;
        startTime = DateTime.Now;
        Debug.Log("[Game5_Solitary] Initialized - Single player mode");
    }
    
    /// <summary>
    /// Called when game starts.
    /// </summary>
    public override void OnGameStart()
    {
        base.OnGameStart();
        Debug.Log("[Game5_Solitary] Game started - Race against time!");
    }
    
    /// <summary>
    /// Called at the start of each turn.
    /// </summary>
    public override void OnTurnStart(Player currentPlayer)
    {
        base.OnTurnStart(currentPlayer);
    }
    
    // ============================================
    // MOVE VALIDATION
    // ============================================
    
    /// <summary>
    /// Check if a move is valid in Game5_Solitary.
    /// 
    /// Rules (same as Bump5):
    /// - Cell must be empty (no chips placed there)
    /// - Player can place on any empty cell
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index
        if (cellIndex < 0 || cellIndex > 11)
        {
            Debug.LogWarning($"[Game5_Solitary] Invalid cell index: {cellIndex}");
            return false;
        }
        
        // Check if cell is empty
        if (!IsCellEmpty(cellIndex))
        {
            Debug.Log($"[Game5_Solitary] Cell {cellIndex} is already occupied");
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
        // No special post-placement effects in solitary mode
    }
    
    // ============================================
    // BUMPING (DISABLED IN SOLITARY)
    // ============================================
    
    /// <summary>
    /// Check if a bump is allowed in Game5_Solitary.
    /// 
    /// Rules: Bumping is never allowed (single player mode).
    /// </summary>
    public override bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell)
    {
        // Single player - bumping not allowed
        return false;
    }
    
    /// <summary>
    /// Called when a bump occurs (should never happen in solitary).
    /// </summary>
    public override void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        base.OnBumpOccurs(bumpingPlayer, bumpedPlayer);
        Debug.LogWarning("[Game5_Solitary] Bump occurred in single-player mode (should not happen)");
    }
    
    // ============================================
    // WIN CONDITION
    // ============================================
    
    /// <summary>
    /// Check if a player has won in Game5_Solitary.
    /// 
    /// Win Condition: 5 chips in a row (same as Bump5)
    /// 
    /// Board layout (3x4 grid):
    ///   0   1   2   3
    ///   4   5   6   7
    ///   8   9  10  11
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        if (gameState == null || gameState.Board == null)
            return false;
        
        int[] playerCells = GetCellsOccupiedBy(player);
        
        if (playerCells.Length < 5)
            return false; // Can't have 5-in-a-row with less than 5 chips
        
        // Check all possible 5-in-a-row patterns
        if (CheckHorizontalWin(playerCells)) return true;
        if (CheckVerticalWin(playerCells)) return true;
        if (CheckDiagonalWinLR(playerCells)) return true;
        if (CheckDiagonalWinRL(playerCells)) return true;
        
        return false;
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
    /// Log final statistics.
    /// </summary>
    public override void OnGameEnd(Player winner)
    {
        base.OnGameEnd(winner);
        Debug.Log($"[Game5_Solitary] Game ended! Rolls: {rollCount}, Time: {ElapsedTime.TotalSeconds:F2}s");
    }
}
