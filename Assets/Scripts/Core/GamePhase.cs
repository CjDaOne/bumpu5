/// <summary>
/// Enum representing all game phases in the turn cycle.
/// </summary>
public enum GamePhase
{
    Setup,          // Initial game setup
    Rolling,        // Player rolls dice
    Placing,        // Player places chip on board
    Bumping,        // Optional: bump opponent chip
    EndTurn,        // Turn wrapping (penalties, pass chip, etc.)
    GameOver        // Win condition detected
}
