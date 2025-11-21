using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// GameModeBase Abstract Class
/// Provides common functionality for all game modes.
/// Updated to support dynamic board sizes (e.g., 5x5 grid).
/// </summary>
public abstract class GameModeBase : IGameMode
{
    // Protected state references accessible to derived classes
    protected GameStateManager gameStateManager;

    // Abstract properties for mode metadata
    public abstract string ModeName { get; }
    public abstract string ModeDescription { get; }

    // Initialization
    public virtual void Initialize(GameStateManager gsm)
    {
        gameStateManager = gsm;
        Debug.Log($"[GameMode] {ModeName} initialized");
    }

    // Lifecycle hooks
    public virtual void OnGameStart()
    {
        Debug.Log($"[GameMode] {ModeName} game started");
    }

    public virtual void OnTurnStart(Player currentPlayer)
    {
        Debug.Log($"[GameMode] {ModeName} turn started for {currentPlayer.PlayerName}");
    }

    // Must be overridden per mode
    public abstract bool IsValidMove(Player player, int cellIndex);

    public virtual void OnChipPlaced(Player player, int cellIndex)
    {
        Debug.Log($"[GameMode] {ModeName} chip placed by {player.PlayerName} at cell {cellIndex}");
    }

    public abstract bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell);

    public virtual void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        Debug.Log($"[GameMode] {ModeName} bump occurred: {bumpingPlayer.PlayerName} bumped {bumpedPlayer.PlayerName}");
    }

    public abstract bool CheckWinCondition(Player player);

    public virtual void OnGameEnd(Player winner)
    {
        Debug.Log($"[GameMode] {ModeName} game ended. Winner: {winner.PlayerName}");
    }

    /// <summary>
    /// Check if the dice roll results in a lost turn.
    /// Default: Single 6 causes lost turn.
    /// </summary>
    public virtual bool IsLoseTurnRoll(int[] roll)
    {
        if (roll == null || roll.Length == 0) return false;
        // Default rule: Single 6 = lose turn
        return roll.Length == 1 && roll[0] == 6;
    }

    // ==================== UTILITY METHODS ====================
    protected bool IsCellEmpty(int cellIndex)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        BoardCell cell = gameStateManager.Board.GetCell(cellIndex);
        return cell != null && !cell.Is_Occupied;
    }

    protected bool IsCellOccupiedBy(int cellIndex, Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        BoardCell cell = gameStateManager.Board.GetCell(cellIndex);
        return cell != null && cell.Is_Occupied && cell.Owner == player;
    }

    protected BoardCell GetCell(int cellIndex)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return null;
        return gameStateManager.Board.GetCell(cellIndex);
    }

    protected int[] GetCellsOccupiedBy(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return new int[0];
        int boardSize = gameStateManager.Board.BOARD_SIZE;
        List<int> occupied = new List<int>();
        for (int i = 0; i < boardSize; i++)
        {
            if (IsCellOccupiedBy(i, player))
                occupied.Add(i);
        }
        return occupied.ToArray();
    }

    protected int GetChipCountForPlayer(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return 0;
        int boardSize = gameStateManager.Board.BOARD_SIZE;
        int count = 0;
        for (int i = 0; i < boardSize; i++)
        {
            if (IsCellOccupiedBy(i, player))
                count++;
        }
        return count;
    }

    // Generic 5‑in‑a‑row detection using the board's own method.
    protected bool HasFiveInARow(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null)
            return false;
        return gameStateManager.Board.Check5InARow(player);
    }
}
