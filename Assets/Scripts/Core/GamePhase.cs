/// <summary>
/// Enum representing all game phases in the turn cycle.
/// </summary>
public enum GamePhase
{
    Setup = 0,      // Initial game setup
    Rolling = 1,    // Player rolls dice
    Placing = 2,    // Player places chip on board
    Bumping = 3,    // Optional: bump opponent chip
    EndTurn = 4,    // Turn wrapping (penalties, pass chip, etc.)
    GameWon = 5,    // Win condition detected, showing winner
    GameOver = 6    // Final state, game complete
}
