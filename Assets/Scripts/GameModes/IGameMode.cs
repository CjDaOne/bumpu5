using System;
using UnityEngine;

/// <summary>
/// Interface for game mode implementations.
/// Each game mode can override rules, win conditions, and special cases.
/// Defines the contract that all game modes must implement.
/// </summary>
public interface IGameMode
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    /// <summary>Gets the short name of the game mode (e.g., "Bump5", "Krazy6")</summary>
    string ModeName { get; }
    
    /// <summary>Gets the long description (e.g., "Bump 5 in a Row")</summary>
    string ModeLongName { get; }
    
    /// <summary>Gets the maximum number of players for this mode</summary>
    int MaxPlayers { get; }
    
    // ============================================
    // RULE CONFIGURATION PROPERTIES
    // ============================================
    
    /// <summary>Does this mode use 5-in-a-row for win detection?</summary>
    bool Use5InARow { get; }
    
    /// <summary>Should rolling 3 doubles in a row cause loss of turn?</summary>
    bool UseTripleDoublesPenalty { get; }
    
    /// <summary>Is rolling 5+6 a "safe" roll (no placement)?</summary>
    bool Use5Plus6Safe { get; }
    
    /// <summary>Does rolling a 6 lose the turn?</summary>
    bool RollingASixLosesTurn { get; }
    
    /// <summary>Can opponent chips be bumped?</summary>
    bool AllowBumping { get; }
    
    // ============================================
    // WIN CONDITION CHECK
    // ============================================
    
    /// <summary>
    /// Check if the given player has met the win condition for this mode.
    /// </summary>
    /// <param name="player">Player to check</param>
    /// <param name="board">Board state to check against</param>
    /// <returns>True if player has won</returns>
    bool CheckWinCondition(Player player, BoardModel board);
    
    // ============================================
    // SPECIAL RULES HOOKS
    // ============================================
    
    /// <summary>
    /// Called at the start of each turn.
    /// Allows mode to apply any special initialization logic.
    /// </summary>
    void OnTurnStart(GameStateManager stateManager, Player currentPlayer);
    
    /// <summary>
    /// Called after dice are rolled.
    /// Allows mode to modify how the roll is interpreted.
    /// </summary>
    /// <param name="stateManager">Current game state</param>
    /// <param name="rollResult">Dice roll (can be modified)</param>
    void OnDiceRolled(GameStateManager stateManager, int[] rollResult);
    
    /// <summary>
    /// Called when a chip is about to be placed.
    /// Allows mode to validate or modify placement.
    /// </summary>
    /// <param name="stateManager">Current game state</param>
    /// <param name="targetCell">Cell where chip will be placed</param>
    /// <returns>True if placement is allowed</returns>
    bool CanPlaceChip(GameStateManager stateManager, int targetCell);
    
    /// <summary>
    /// Called when bumping is about to occur.
    /// Allows mode to override bump behavior (swap instead of remove, etc).
    /// </summary>
    /// <param name="stateManager">Current game state</param>
    /// <param name="bumperPlayer">Player doing the bumping</param>
    /// <param name="targetCell">Cell with opponent chip</param>
    /// <returns>True if bump should be applied</returns>
    bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell);
    
    /// <summary>
    /// Called when turn ends.
    /// Allows mode to apply any special end-of-turn logic.
    /// </summary>
    void OnTurnEnd(GameStateManager stateManager, Player currentPlayer);
    
    // ============================================
    // INITIALIZATION
    // ============================================
    
    /// <summary>
    /// Called when game starts with this mode.
    /// Allows mode to initialize any special state.
    /// </summary>
    void Initialize(GameStateManager stateManager);
}
