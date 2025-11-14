using System.Collections.Generic;

/// <summary>
/// Represents a player in the Bump U Box game.
/// Tracks ownership, chips, score, and active status.
/// </summary>
public class Player
{
    private int playerIndex;
    private string name;
    private List<Chip> chips;
    private int score;
    private bool isActive;

    /// <summary>
    /// Gets the player's index (0 or 1 for 2-player game).
    /// </summary>
    public int PlayerIndex => playerIndex;

    /// <summary>
    /// Gets or sets the player's display name.
    /// </summary>
    public string Name
    {
        get => name;
        set => name = value;
    }

    /// <summary>
    /// Gets the list of all chips owned by this player.
    /// </summary>
    public List<Chip> Chips => chips;

    /// <summary>
    /// Gets or sets the player's current score.
    /// </summary>
    public int Score
    {
        get => score;
        set => score = value;
    }

    /// <summary>
    /// Gets or sets whether this player is currently active in the game.
    /// </summary>
    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    /// <summary>
    /// Initializes a new Player instance.
    /// </summary>
    /// <param name="index">Player index (0 or 1)</param>
    /// <param name="playerName">Display name for the player</param>
    public Player(int index, string playerName)
    {
        playerIndex = index;
        name = playerName;
        chips = new List<Chip>();
        score = 0;
        isActive = true;
    }

    /// <summary>
    /// Adds points to the player's score.
    /// </summary>
    /// <param name="points">Points to add (can be negative)</param>
    public void AddScore(int points)
    {
        score += points;
        if (score < 0) score = 0;
    }

    /// <summary>
    /// Subtracts points from the player's score.
    /// </summary>
    /// <param name="points">Points to subtract</param>
    public void RemoveScore(int points)
    {
        score -= points;
        if (score < 0) score = 0;
    }

    /// <summary>
    /// Gets all chips currently on the board.
    /// </summary>
    /// <returns>List of active chips on board</returns>
    public List<Chip> GetChipsOnBoard()
    {
        List<Chip> onBoard = new List<Chip>();
        foreach (Chip chip in chips)
        {
            if (chip.IsActive && chip.CurrentCell != null)
                onBoard.Add(chip);
        }
        return onBoard;
    }

    /// <summary>
    /// Gets all chips not on the board (in reserve).
    /// </summary>
    /// <returns>List of inactive chips</returns>
    public List<Chip> GetChipsOffBoard()
    {
        List<Chip> offBoard = new List<Chip>();
        foreach (Chip chip in chips)
        {
            if (!chip.IsActive || chip.CurrentCell == null)
                offBoard.Add(chip);
        }
        return offBoard;
    }

    /// <summary>
    /// Returns a string representation of the player.
    /// </summary>
    public override string ToString()
    {
        return $"Player {playerIndex}: {name} (Score: {score})";
    }
}
