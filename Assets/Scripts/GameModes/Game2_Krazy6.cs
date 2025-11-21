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
    /// <summary>
    /// Check if a move is valid in Krazy6.
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index against dynamic board size
        if (cellIndex < 0 || cellIndex >= gameStateManager.Board.BOARD_SIZE)
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
    /// Check if a player has won in Krazy6.
    /// Win Condition: 6 chips on the board (and roll a 6 - simplified to just 6 chips for now).
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        
        // Check for 6 chips on the board
        int chipCount = GetChipCountForPlayer(player);
        return chipCount >= 6;
    }

    /// <summary>
    /// Custom roll logic for Krazy 6.
    /// </summary>
    public override bool IsLoseTurnRoll(int[] roll)
    {
        if (roll == null || roll.Length == 0) return false;
        
        bool hasSix = (roll[0] == 6 || (roll.Length > 1 && roll[1] == 6));
        int chipCount = GetChipCountForPlayer(gameStateManager.CurrentPlayer); // This might be tricky if CurrentPlayer is not set yet? 
        // GameStateManager calls IsLoseTurnRoll inside RollDice, where CurrentPlayer IS valid.
        
        // Rule 1: A #6 is required to start putting chips on the board.
        if (chipCount == 0 && !hasSix)
        {
            Debug.Log("[Game2_Krazy6] Need a 6 to start!");
            return true; // Lose turn
        }
        
        // Rule 2: If <= 5 chips and roll 6, BUMPED by 6 (penalty).
        if (chipCount <= 5 && hasSix)
        {
            Debug.Log("[Game2_Krazy6] Bumped by the 6! Lose turn and chip.");
            return true; // Lose turn (and chip removal handled in OnDiceRolled)
        }
        
        // Rule 3: If > 5 chips and roll 6, GOOD ROLL (Bonus).
        if (chipCount > 5 && hasSix)
        {
            Debug.Log("[Game2_Krazy6] Good 6! Bonus turn.");
            return false; // Don't lose turn
        }
        
        return false;
    }

    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        if (gameStateManager != null)
        {
            gameStateManager.OnDiceRolled += HandleDiceRoll;
        }
        Debug.Log("[Game2_Krazy6] Initialized - 6s are tricky!");
    }
    
    public override void OnGameEnd(Player winner)
    {
        base.OnGameEnd(winner);
        if (gameStateManager != null)
        {
            gameStateManager.OnDiceRolled -= HandleDiceRoll;
        }
        Debug.Log($"[Game2_Krazy6] Game ended! Winner: {winner.PlayerName}");
    }

    private void HandleDiceRoll(int[] roll)
    {
        if (roll == null) return;
        
        bool hasSix = (roll[0] == 6 || (roll.Length > 1 && roll[1] == 6));
        Player currentPlayer = gameStateManager.CurrentPlayer;
        int chipCount = GetChipCountForPlayer(currentPlayer);
        
        // Rule: If <= 5 chips and roll 6, remove a chip.
        if (chipCount > 0 && chipCount <= 5 && hasSix)
        {
            // Remove a chip. We'll remove the first one we find for now.
            // Ideally we'd let the user choose, but that requires UI/Phase support.
            int[] occupied = GetCellsOccupiedBy(currentPlayer);
            if (occupied.Length > 0)
            {
                int cellToRemove = occupied[occupied.Length - 1]; // Remove last one (highest index) as a heuristic
                BoardCell cell = GetCell(cellToRemove);
                if (cell != null)
                {
                    cell.Clear();
                    Debug.Log($"[Game2_Krazy6] Penalty: Removed chip at {cellToRemove}");
                }
            }
        }
    }
}
