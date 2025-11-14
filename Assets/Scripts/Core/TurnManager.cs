using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages turn flow and player rotation in the game.
/// Tracks whose turn it is and handles turn advancement.
/// </summary>
public class TurnManager
{
    private List<Player> playerList;
    private int currentPlayerIndex;

    /// <summary>
    /// Gets the current active player.
    /// </summary>
    public Player CurrentPlayer => playerList[currentPlayerIndex];

    /// <summary>
    /// Gets the current player's index.
    /// </summary>
    public int CurrentPlayerIndex => currentPlayerIndex;

    /// <summary>
    /// Gets the list of all players.
    /// </summary>
    public List<Player> PlayerList => playerList;

    /// <summary>
    /// Initializes a new TurnManager with a list of players.
    /// </summary>
    /// <param name="players">List of players in game order</param>
    public TurnManager(List<Player> players)
    {
        if (players == null || players.Count == 0)
        {
            Debug.LogError("TurnManager requires at least one player");
            playerList = new List<Player>();
            currentPlayerIndex = 0;
            return;
        }

        playerList = new List<Player>(players);
        currentPlayerIndex = 0;
    }

    /// <summary>
    /// Gets the next player in rotation.
    /// </summary>
    /// <returns>Next player</returns>
    public Player GetNextPlayer()
    {
        int nextIndex = (currentPlayerIndex + 1) % playerList.Count;
        return playerList[nextIndex];
    }

    /// <summary>
    /// Advances to the next player's turn.
    /// </summary>
    public void AdvanceTurn()
    {
        currentPlayerIndex = (currentPlayerIndex + 1) % playerList.Count;
    }

    /// <summary>
    /// Gets the current active player.
    /// </summary>
    /// <returns>Current player</returns>
    public Player GetCurrentPlayer()
    {
        return CurrentPlayer;
    }

    /// <summary>
    /// Sets the current player by index.
    /// </summary>
    /// <param name="playerIndex">Index of player (0-based)</param>
    public void SetCurrentPlayer(int playerIndex)
    {
        if (playerIndex < 0 || playerIndex >= playerList.Count)
        {
            Debug.LogError($"Player index {playerIndex} out of bounds");
            return;
        }

        currentPlayerIndex = playerIndex;
    }

    /// <summary>
    /// Gets the number of players in the game.
    /// </summary>
    /// <returns>Player count</returns>
    public int GetPlayerCount()
    {
        return playerList.Count;
    }

    /// <summary>
    /// Gets a player by index.
    /// </summary>
    /// <param name="index">Player index</param>
    /// <returns>Player at index</returns>
    public Player GetPlayerByIndex(int index)
    {
        if (index < 0 || index >= playerList.Count)
        {
            Debug.LogError($"Player index {index} out of bounds");
            return null;
        }

        return playerList[index];
    }

    /// <summary>
    /// Resets the turn to the first player.
    /// </summary>
    public void Reset()
    {
        currentPlayerIndex = 0;
    }

    /// <summary>
    /// Returns a string representation of the turn state.
    /// </summary>
    public override string ToString()
    {
        return $"Current Turn: {CurrentPlayer.Name} (Player {currentPlayerIndex})";
    }
}
