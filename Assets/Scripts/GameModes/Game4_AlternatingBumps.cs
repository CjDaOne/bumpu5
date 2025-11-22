using UnityEngine;

/// <summary>
/// Game4_AlternatingBumps - Tactical Bumping Game Mode
/// 
/// Win Condition: Get 5 chips in a row (same as Game1)
/// Bumping: CONTROLLED - players take turns being the "bumper"
/// Penalties: Variable - depends on who is allowed to bump
/// Difficulty: Medium-Hard - requires tactical planning
/// 
/// This mode adds a twist to the classic game:
/// - Players alternate who has the right to bump
/// - When it's your turn to bump, you're the only one who can bump
/// - When it's not your turn, you cannot bump (even if beneficial)
/// - Adds strategy layer: Is this the right time to use your bump?
/// </summary>
public class Game4_AlternatingBumps : GameModeBase
{
    // Mode properties
    public override string ModeName => "Alternating Bumps";
    public override string ModeDescription => "Get 5 in a row to win. Bumping alternates between players each turn. Tactical mode.";
    
    // Game state tracking
    private Player bumpingPlayer;  // Who currently has bump rights
    private int bumpTurnCounter;   // Track which turn
    
    // ==================== LIFECYCLE ====================
    
    /// <summary>
    /// Initialize the game mode.
    /// </summary>
    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        bumpingPlayer = null;
        bumpTurnCounter = 0;
        Debug.Log("[Game4_AlternatingBumps] Initialized");
    }
    
    /// <summary>
    /// Called when game starts.
    /// </summary>
    public override void OnGameStart()
    {
        base.OnGameStart();
        
        // Start with first player having bump rights
        if (gameStateManager != null && gameStateManager.Players != null && gameStateManager.Players.Count > 0)
        {
            bumpingPlayer = gameStateManager.Players[0];
            bumpTurnCounter = 0;
        }
        
        Debug.Log($"[Game4_AlternatingBumps] Game started - {bumpingPlayer?.PlayerName} has initial bump rights");
    }
    
    /// <summary>
    /// Called at the start of each player's turn.
    /// </summary>
    public override void OnTurnStart(Player currentPlayer)
    {
        base.OnTurnStart(currentPlayer);
        
        // If the current player is different from bumping player, rotate bump rights
        if (currentPlayer != bumpingPlayer)
        {
            bumpingPlayer = currentPlayer;
            Debug.Log($"[Game4_AlternatingBumps] Bump rights now belong to {bumpingPlayer?.PlayerName}");
        }
    }
    
    // ==================== MOVE VALIDATION ====================
    
    /// <summary>
    /// Check if a move is valid in Game4_AlternatingBumps.
    /// 
    /// Rules:
    /// - Cell must be empty
    /// - Player can place on any empty cell
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index
        if (cellIndex < 0 || cellIndex >= BoardModel.BOARD_SIZE)
        {
            Debug.LogWarning($"[Game4_AlternatingBumps] Invalid cell index: {cellIndex}");
            return false;
        }
        
        // Check if cell is empty
        if (!IsCellEmpty(cellIndex))
        {
            Debug.Log($"[Game4_AlternatingBumps] Cell {cellIndex} is already occupied");
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
        Debug.Log($"[Game4_AlternatingBumps] {player.PlayerName} placed chip at {cellIndex}");
    }
    
    // ==================== BUMPING ====================
    
    /// <summary>
    /// Check if a bump is allowed in Game4_AlternatingBumps.
    /// 
    /// Rules:
    /// - Only the current bumping player can bump
    /// - The other player CANNOT bump, even if strategic
    /// - Can bump any opponent chip
    /// </summary>
    public override bool CanBump(Player bumpingPlayer_param, Player targetPlayer, int targetCell)
    {
        // Only the designated bumping player can bump
        if (bumpingPlayer_param != bumpingPlayer)
        {
            Debug.Log($"[Game4_AlternatingBumps] {bumpingPlayer_param.PlayerName} cannot bump - not their bump turn");
            return false;
        }
        
        // Can't bump yourself
        if (bumpingPlayer_param == targetPlayer)
        {
            Debug.Log("[Game4_AlternatingBumps] Cannot bump your own chip");
            return false;
        }
        
        // Target cell must have opponent's chip
        if (!IsCellOccupiedBy(targetCell, targetPlayer))
        {
            Debug.Log($"[Game4_AlternatingBumps] Cell {targetCell} is not occupied by target player");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Called when a bump occurs.
    /// Applies Game4-specific effects.
    /// </summary>
    public override void OnBumpOccurs(Player bumpingPlayer_param, Player bumpedPlayer)
    {
        base.OnBumpOccurs(bumpingPlayer_param, bumpedPlayer);
        Debug.Log($"[Game4_AlternatingBumps] {bumpingPlayer_param.PlayerName} bumped {bumpedPlayer.PlayerName}");
    }
    
    // ==================== WIN CONDITION ====================
    
    /// <summary>
    /// Check if a player has won in Game4_AlternatingBumps.
    /// 
    /// Win Condition: 5 chips in a row (same as Game1)
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        
        return gameStateManager.Board.Check5InARow(player);
    }
    
    /// <summary>
    /// Check for 5-in-a-row.
    /// </summary>
    private bool CheckHorizontalWin(int[] playerCells) => false; // Deprecated
    private bool CheckVerticalWin(int[] playerCells) => false; // Deprecated
    private bool CheckDiagonalWinLR(int[] playerCells) => false; // Deprecated
    private bool CheckDiagonalWinRL(int[] playerCells) => false; // Deprecated
    
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
        Debug.Log($"[Game4_AlternatingBumps] Game ended! Winner: {winner.PlayerName}");
    }
}
