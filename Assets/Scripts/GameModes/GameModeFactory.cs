using System;

/// <summary>
/// Factory for creating game mode instances by ID.
/// Uses simple factory pattern with switch statement.
/// </summary>
public static class GameModeFactory
{
    /// <summary>
    /// Create a game mode instance by ID.
    /// </summary>
    /// <param name="modeId">Game mode ID (1-5)</param>
    /// <returns>IGameMode instance for the specified mode</returns>
    /// <exception cref="ArgumentException">Thrown if modeId is not 1-5</exception>
    public static IGameMode CreateGameMode(int modeId)
    {
        return modeId switch
        {
            1 => new Game1_Bump5(),
            2 => new Game2_Krazy6(),
            3 => new Game3_PassTheChip(),
            4 => new Game4_BumpUAnd5(),
            5 => new Game5_Solitary(),
            _ => throw new ArgumentException($"Unknown game mode ID: {modeId}. Valid IDs are 1-5.")
        };
    }
}
