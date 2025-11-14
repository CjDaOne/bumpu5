using UnityEngine;

/// <summary>
/// Game5_SurvivalMode - Points-Based Long-Form Game Mode
/// 
/// Win Condition: First to accumulate 50 points
/// Scoring: +5 points per chip placed, +10 points per successful bump, -10 points for penalties
/// Bumping: Enabled - critical to scoring strategy
/// Penalties: Severe (-10 points for strategic penalties)
/// Difficulty: Hard - long-form gameplay, strategic point management
/// 
/// This mode extends gameplay significantly:
/// - Longer game duration (more turns to reach 50 points)
/// - Points-based win condition adds complexity
/// - Bumping becomes critical for scoring
/// - Risk/reward: Do you bump to score, or place to score?
/// - Penalties can swing momentum dramatically
/// </summary>
public class Game5_SurvivalMode : GameModeBase
{
    // Mode properties
    public override string ModeName => "Survival Mode";
    public override string ModeDescription => "Reach 50 points to win. +5 per chip, +10 per bump, -10 for penalty. Long-form strategic mode.";
    
    // Scoring configuration
    private const int POINTS_PER_CHIP = 5;
    private const int POINTS_PER_BUMP = 10;
    private const int PENALTY_POINTS = -10;
    private const int WIN_THRESHOLD = 50;
    
    // Player scores (indexed by player)
    private int[] playerScores;
    
    // ==================== LIFECYCLE ====================
    
    /// <summary>
    /// Initialize the game mode.
    /// </summary>
    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        
        // Initialize score tracking for all players
        if (gameState != null && gameState.Players != null)
        {
            playerScores = new int[gameState.Players.Length];
            for (int i = 0; i < playerScores.Length; i++)
            {
                playerScores[i] = 0;
            }
        }
        
        Debug.Log("[Game5_SurvivalMode] Initialized - First to 50 points wins!");
    }
    
    /// <summary>
    /// Called when game starts.
    /// </summary>
    public override void OnGameStart()
    {
        base.OnGameStart();
        
        // Reset all scores to 0
        if (playerScores != null)
        {
            for (int i = 0; i < playerScores.Length; i++)
            {
                playerScores[i] = 0;
            }
        }
        
        Debug.Log("[Game5_SurvivalMode] Game started - Scores reset to 0");
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
    /// Check if a move is valid in Game5_SurvivalMode.
    /// 
    /// Rules:
    /// - Cell must be empty
    /// - Player can place on any empty cell
    /// </summary>
    public override bool IsValidMove(Player player, int cellIndex)
    {
        // Validate cell index
        if (cellIndex < 0 || cellIndex > 11)
        {
            Debug.LogWarning($"[Game5_SurvivalMode] Invalid cell index: {cellIndex}");
            return false;
        }
        
        // Check if cell is empty
        if (!IsCellEmpty(cellIndex))
        {
            Debug.Log($"[Game5_SurvivalMode] Cell {cellIndex} is already occupied");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Called after a chip is placed.
    /// Award points to the player.
    /// </summary>
    public override void OnChipPlaced(Player player, int cellIndex)
    {
        base.OnChipPlaced(player, cellIndex);
        
        // Award points for chip placement
        AddPoints(player, POINTS_PER_CHIP);
        Debug.Log($"[Game5_SurvivalMode] {player.PlayerName} placed chip: +{POINTS_PER_CHIP} points (total: {GetPlayerScore(player)})");
    }
    
    // ==================== BUMPING ====================
    
    /// <summary>
    /// Check if a bump is allowed in Game5_SurvivalMode.
    /// 
    /// Rules:
    /// - Bumping is always allowed
    /// - Can bump any opponent chip
    /// - Bumping awards points
    /// </summary>
    public override bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell)
    {
        // Can't bump yourself
        if (bumpingPlayer == targetPlayer)
        {
            Debug.Log("[Game5_SurvivalMode] Cannot bump your own chip");
            return false;
        }
        
        // Target cell must have opponent's chip
        if (!IsCellOccupiedBy(targetCell, targetPlayer))
        {
            Debug.Log($"[Game5_SurvivalMode] Cell {targetCell} is not occupied by target player");
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// Called when a bump occurs.
    /// Award points to bumping player, penalize bumped player.
    /// </summary>
    public override void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        base.OnBumpOccurs(bumpingPlayer, bumpedPlayer);
        
        // Award points to bumper
        AddPoints(bumpingPlayer, POINTS_PER_BUMP);
        
        // Penalize bumped player
        AddPoints(bumpedPlayer, PENALTY_POINTS);
        
        Debug.Log($"[Game5_SurvivalMode] {bumpingPlayer.PlayerName} bumped {bumpedPlayer.PlayerName}");
        Debug.Log($"  {bumpingPlayer.PlayerName}: +{POINTS_PER_BUMP} (total: {GetPlayerScore(bumpingPlayer)})");
        Debug.Log($"  {bumpedPlayer.PlayerName}: {PENALTY_POINTS} (total: {GetPlayerScore(bumpedPlayer)})");
    }
    
    // ==================== WIN CONDITION ====================
    
    /// <summary>
    /// Check if a player has won in Game5_SurvivalMode.
    /// 
    /// Win Condition: Player has reached 50 points
    /// </summary>
    public override bool CheckWinCondition(Player player)
    {
        int score = GetPlayerScore(player);
        
        if (score >= WIN_THRESHOLD)
        {
            Debug.Log($"[Game5_SurvivalMode] {player.PlayerName} reached {score} points - WINS!");
            return true;
        }
        
        return false;
    }
    
    // ==================== SCORE MANAGEMENT ====================
    
    /// <summary>
    /// Add points to a player's score.
    /// </summary>
    private void AddPoints(Player player, int points)
    {
        if (playerScores == null || gameState == null || gameState.Players == null)
            return;
        
        // Find the player's index
        for (int i = 0; i < gameState.Players.Length; i++)
        {
            if (gameState.Players[i] == player)
            {
                playerScores[i] += points;
                break;
            }
        }
    }
    
    /// <summary>
    /// Get a player's current score.
    /// </summary>
    private int GetPlayerScore(Player player)
    {
        if (playerScores == null || gameState == null || gameState.Players == null)
            return 0;
        
        // Find the player's index
        for (int i = 0; i < gameState.Players.Length; i++)
        {
            if (gameState.Players[i] == player)
            {
                return playerScores[i];
            }
        }
        
        return 0;
    }
    
    /// <summary>
    /// Called when game ends.
    /// </summary>
    public override void OnGameEnd(Player winner)
    {
        base.OnGameEnd(winner);
        int winnerScore = GetPlayerScore(winner);
        Debug.Log($"[Game5_SurvivalMode] Game ended! Winner: {winner.PlayerName} with {winnerScore} points");
    }
}
