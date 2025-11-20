/// <summary>
/// Enum representing all game phases in the turn cycle.
/// </summary>
public enum GamePhase
{
    Setup = 0,         // Initial game setup
    GameStart = 1,     // Game starting
    Rolling = 2,       // Player rolls dice
    RollingDice = 2,   // Alias for Rolling (UI compatibility)
    Placing = 3,       // Player places chip on board
    Bumping = 4,       // Optional: bump opponent chip
    EndTurn = 5,       // Turn wrapping (penalties, pass chip, etc.)
    Waiting = 6,       // Waiting for player action
    GameWon = 7,       // Win condition detected, showing winner
    GameEnd = 8,       // Game ending
    GameOver = 9       // Final state, game complete
}
