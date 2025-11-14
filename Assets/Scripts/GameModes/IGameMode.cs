/// <summary>
/// Interface that all Bump U Box game modes must implement.
/// Defines the contract for custom rules, scoring, and win conditions per mode.
/// </summary>
public interface IGameMode
{
    /// <summary>
    /// Gets the human-readable name of this game mode.
    /// </summary>
    string ModeName { get; }

    /// <summary>
    /// Gets a brief description of this mode's rules.
    /// </summary>
    string ModeDescription { get; }

    /// <summary>
    /// Gets the unique ID for this mode (0-4 for 5 modes).
    /// </summary>
    int ModeID { get; }

    /// <summary>
    /// Called when a player rolls the dice.
    /// Allows mode to process the roll and determine valid moves.
    /// </summary>
    /// <param name="player">Player who rolled</param>
    /// <param name="diceRoll">Dice roll result [die1, die2] or [single]</param>
    void OnDiceRolled(Player player, int[] diceRoll);

    /// <summary>
    /// Called when a player moves a chip.
    /// Allows mode to apply scoring or trigger special actions.
    /// </summary>
    /// <param name="player">Player moving the chip</param>
    /// <param name="fromCell">Source cell index</param>
    /// <param name="toCell">Target cell index</param>
    void OnChipMoved(Player player, int fromCell, int toCell);

    /// <summary>
    /// Called when a player bumps an opponent's chip.
    /// Allows mode to apply scoring or special bump rules.
    /// </summary>
    /// <param name="bumpingPlayer">Player doing the bump</param>
    /// <param name="bumpedPlayer">Player whose chip was bumped</param>
    /// <param name="targetCell">Cell where bump occurred</param>
    void OnBump(Player bumpingPlayer, Player bumpedPlayer, int targetCell);

    /// <summary>
    /// Checks if a player has won the game.
    /// Different modes have different win conditions.
    /// </summary>
    /// <param name="player">Player to check</param>
    /// <param name="board">Current board state</param>
    /// <returns>True if player has won</returns>
    bool CheckWin(Player player, BoardModel board);

    /// <summary>
    /// Determines if a bump is allowed in this mode.
    /// Some modes disable bumping entirely.
    /// </summary>
    /// <param name="player">Player attempting bump</param>
    /// <param name="targetCell">Target cell</param>
    /// <param name="board">Current board state</param>
    /// <returns>True if bump is allowed in this mode</returns>
    bool IsBumpAllowed(Player player, int targetCell, BoardModel board);

    /// <summary>
    /// Gets the list of valid target cells after a dice roll.
    /// Different modes may have different movement rules.
    /// </summary>
    /// <param name="player">Player moving</param>
    /// <param name="diceSum">Total from dice roll</param>
    /// <param name="board">Current board state</param>
    /// <returns>List of valid cell indices to move to</returns>
    System.Collections.Generic.List<int> GetValidMoves(Player player, int diceSum, BoardModel board);

    /// <summary>
    /// Determines if a player can roll again.
    /// Typically true for doubles, false otherwise.
    /// </summary>
    /// <param name="diceRoll">Last dice roll</param>
    /// <returns>True if player can roll again</returns>
    bool CanRollAgain(int[] diceRoll);

    /// <summary>
    /// Applies mode-specific end-of-turn processing.
    /// </summary>
    /// <param name="player">Player whose turn is ending</param>
    void OnTurnEnd(Player player);

    /// <summary>
    /// Applies mode-specific start-of-game initialization.
    /// </summary>
    /// <param name="players">All players in the game</param>
    /// <param name="board">Game board</param>
    void Initialize(Player[] players, BoardModel board);

    /// <summary>
    /// Gets mode-specific rules as a formatted string.
    /// Used for displaying rules in UI.
    /// </summary>
    /// <returns>Rules text</returns>
    string GetRulesText();
}
