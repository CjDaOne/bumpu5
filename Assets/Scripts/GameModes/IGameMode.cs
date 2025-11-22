/// <summary>
/// IGameMode Interface
/// 
/// Contract that all game modes must implement.
/// Defines the lifecycle and behavior expectations for game mode implementations.
/// 
/// Game modes control:
/// - Valid move logic (which cells can be played)
/// - Bumping rules (if/when bumping is allowed)
/// - Win conditions (how a player wins)
/// - Game state transitions (turn flow, phase changes)
/// 
/// All 5 game modes inherit from GameModeBase and implement this interface.
/// </summary>

public interface IGameMode
{
    /// <summary>
    /// User-friendly name of this game mode.
    /// Examples: "Bump 5", "Rush to Five", "No Fives", "Alternating Bumps", "Survival Mode"
    /// </summary>
    string ModeName { get; }
    
    /// <summary>
    /// Description of how this mode plays.
    /// Explains rules, win conditions, and unique mechanics.
    /// </summary>
    string ModeDescription { get; }
    
    /// <summary>
    /// Initialize this mode with a reference to the game state manager.
    /// Called when mode is selected, before gameplay starts.
    /// 
    /// This method should:
    /// - Store the GameStateManager reference
    /// - Store the GameState reference
    /// - Set up any mode-specific state variables
    /// - Hook into GameStateManager events if needed
    /// </summary>
    /// <param name="gameStateManager">The game state manager instance</param>
    void Initialize(GameStateManager gameStateManager);
    
    /// <summary>
    /// Called when the game starts (after mode initialization).
    /// Set up initial state, reset scores, place starting chips if needed.
    /// 
    /// This method should:
    /// - Reset any mode-specific counters
    /// - Initialize score tracking
    /// - Place any starting chips on the board
    /// - Fire any startup events
    /// </summary>
    void OnGameStart();
    
    /// <summary>
    /// Called at the beginning of each player's turn.
    /// 
    /// This method should:
    /// - Update phase indicator if needed
    /// - Reset turn-specific state
    /// - Enable/disable bumping options
    /// - Fire turn start events
    /// </summary>
    /// <param name="currentPlayer">The player whose turn it is</param>
    void OnTurnStart(Player currentPlayer);
    
    /// <summary>
    /// Check if a player can place a chip in the specified cell.
    /// Called before allowing a move to execute.
    /// 
    /// This method should:
    /// - Check if the cell is empty
    /// - Check mode-specific valid move rules
    /// - Return true only if move is legal
    /// 
    /// Examples:
    /// - Game1: Allow any empty cell
    /// - Game3: Only allow cells that don't create 5-in-a-row (penalty)
    /// </summary>
    /// <param name="player">The player attempting the move</param>
    /// <param name="cellIndex">The board cell index (0-11)</param>
    /// <returns>true if move is valid, false if invalid</returns>
    bool IsValidMove(Player player, int cellIndex);
    
    /// <summary>
    /// Called after a chip is successfully placed on the board.
    /// Execute any post-placement logic specific to this mode.
    /// 
    /// This method should:
    /// - Update mode-specific scoring
    /// - Check for mode-specific effects
    /// - Trigger post-placement animations
    /// - Update UI indicators
    /// </summary>
    /// <param name="player">The player who placed the chip</param>
    /// <param name="cellIndex">The cell where chip was placed</param>
    void OnChipPlaced(Player player, int cellIndex);
    
    /// <summary>
    /// Check if a bump action is allowed in this mode.
    /// Called before allowing a bump to execute.
    /// 
    /// This method should:
    /// - Check if bumping is enabled in this mode
    /// - Check if the bump is legal (e.g., mode-specific restrictions)
    /// - Check if the target player/cell can be bumped
    /// - Return true only if bump is legal
    /// 
    /// Examples:
    /// - Game1: Allow bumping any opponent chip
    /// - Game4: Only allow bumping if it's your turn to bump
    /// - Game2: Never allow bumping (return false always)
    /// </summary>
    /// <param name="bumpingPlayer">The player performing the bump</param>
    /// <param name="targetPlayer">The player being bumped</param>
    /// <param name="targetCell">The cell of the chip being bumped</param>
    /// <returns>true if bump is allowed, false if not</returns>
    bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell);
    
    /// <summary>
    /// Called when a bump occurs (after CanBump returns true and bump is confirmed).
    /// Execute mode-specific bump logic (penalties, scoring, effects).
    /// 
    /// This method should:
    /// - Apply mode-specific bump penalties
    /// - Update scores if needed
    /// - Trigger bump animations
    /// - Fire bump events
    /// </summary>
    /// <param name="bumpingPlayer">The player performing the bump</param>
    /// <param name="bumpedPlayer">The player being bumped</param>
    void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer);
    
    /// <summary>
    /// Check if the specified player has won the game.
    /// Called after each state change to detect win condition.
    /// 
    /// This method should:
    /// - Check mode-specific win condition
    /// - Return true only if player has definitively won
    /// 
    /// Examples:
    /// - Game1: Check if player has 5 in a row (any direction)
    /// - Game2: Check if player has 5 chips on board
    /// - Game5: Check if player has 50+ points
    /// </summary>
    /// <param name="player">The player to check</param>
    /// <returns>true if player has won, false otherwise</returns>
    bool CheckWinCondition(Player player);
    
    /// <summary>
    /// Check if the dice roll results in a lost turn.
    /// </summary>
    bool IsLoseTurnRoll(int[] roll);

    /// <summary>
    /// Called when the game ends (after CheckWinCondition returns true for a player).
    /// Execute mode-specific cleanup and finalization.
    /// 
    /// This method should:
    /// - Log final scores
    /// - Execute end-game animations
    /// - Fire game end events
    /// - Clean up mode-specific resources
    /// </summary>
    /// <param name="winner">The player who won the game</param>
    void OnGameEnd(Player winner);
    /// <summary>
    /// Whether bumping is allowed in this mode.
    /// </summary>
    bool AllowBumping { get; }

    /// <summary>
    /// Maximum number of players supported by this mode.
    /// </summary>
    int MaxPlayers { get; }

    /// <summary>
    /// Whether rolling a single 6 causes the player to lose their turn.
    /// </summary>
    bool RollingASixLosesTurn { get; }
}
