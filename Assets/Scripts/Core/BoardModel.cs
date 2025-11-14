using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Core game board logic for Bump U Box.
/// Manages a 12-cell grid, chip placement, bumping, and win detection (5-in-a-row).
/// This is pure game logic with no visual representation.
/// </summary>
public class BoardModel
{
    private const int BOARD_SIZE = 12;
    private const int WINNING_ROW = 5;

    private BoardCell[] cells;
    private Player[] players;
    private int[][] adjacencyMap;

    /// <summary>
    /// Gets all cells on the board.
    /// </summary>
    public BoardCell[] Cells => cells;

    /// <summary>
    /// Gets all players in the game.
    /// </summary>
    public Player[] Players => players;

    /// <summary>
    /// Initializes a new BoardModel with 2 players.
    /// </summary>
    /// <param name="player1">First player</param>
    /// <param name="player2">Second player</param>
    public BoardModel(Player player1, Player player2)
    {
        cells = new BoardCell[BOARD_SIZE];
        players = new Player[] { player1, player2 };

        // Create cells
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            cells[i] = new BoardCell(i);
        }

        // Build adjacency map (each cell knows its neighbors)
        BuildAdjacencyMap();
    }

    /// <summary>
    /// Builds the adjacency map for the 12-cell grid.
    /// The board is arranged in a specific layout (to be visualized later).
    /// For now, we assume a simple linear adjacency: each cell connects to next/previous.
    /// This may be updated based on actual board layout.
    /// </summary>
    private void BuildAdjacencyMap()
    {
        adjacencyMap = new int[BOARD_SIZE][];

        // Simple circular adjacency: each cell adjacent to next and previous
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            List<int> neighbors = new List<int>();

            // Previous cell (circular)
            neighbors.Add((i - 1 + BOARD_SIZE) % BOARD_SIZE);

            // Next cell (circular)
            neighbors.Add((i + 1) % BOARD_SIZE);

            adjacencyMap[i] = neighbors.ToArray();
        }

        // Note: This is a basic linear adjacency.
        // If the board has a 2D layout, update this method accordingly.
    }

    /// <summary>
    /// Gets all adjacent cells to a given cell index.
    /// </summary>
    /// <param name="cellIndex">Cell index (0-11)</param>
    /// <returns>List of adjacent cell indices</returns>
    public List<int> GetAdjacentCells(int cellIndex)
    {
        if (cellIndex < 0 || cellIndex >= BOARD_SIZE)
        {
            Debug.LogError($"Cell index {cellIndex} out of bounds");
            return new List<int>();
        }

        List<int> neighbors = new List<int>(adjacencyMap[cellIndex]);
        return neighbors;
    }

    /// <summary>
    /// Checks if two cells are adjacent.
    /// </summary>
    /// <param name="cell1">First cell index</param>
    /// <param name="cell2">Second cell index</param>
    /// <returns>True if cells are adjacent</returns>
    public bool IsAdjacent(int cell1, int cell2)
    {
        if (cell1 < 0 || cell1 >= BOARD_SIZE || cell2 < 0 || cell2 >= BOARD_SIZE)
            return false;

        foreach (int neighbor in adjacencyMap[cell1])
        {
            if (neighbor == cell2)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if a player has 5 chips in a row on the board.
    /// This is the winning condition.
    /// </summary>
    /// <param name="player">Player to check</param>
    /// <returns>True if player has 5 in a row</returns>
    public bool Check5InARow(Player player)
    {
        if (player == null) return false;

        // Check from each cell as a potential start
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            if (Check5InARowRecursive(player, i, 0, -1))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Recursively checks for 5 consecutive chips of the same owner.
    /// </summary>
    /// <param name="player">Player to check</param>
    /// <param name="currentCell">Current cell index</param>
    /// <param name="count">Current count of consecutive chips</param>
    /// <param name="previousCell">Previous cell visited (-1 for none)</param>
    /// <returns>True if 5 in a row detected</returns>
    private bool Check5InARowRecursive(Player player, int currentCell, int count, int previousCell)
    {
        // Base case: found 5 in a row
        if (count >= WINNING_ROW)
            return true;

        // Bounds check
        if (currentCell < 0 || currentCell >= BOARD_SIZE)
            return false;

        // Current cell doesn't belong to player
        if (cells[currentCell].Owner != player)
            return false;

        // Increment count
        count++;

        // If we found 5, return true
        if (count >= WINNING_ROW)
            return true;

        // Check adjacent cells (but don't go back to previous cell)
        foreach (int nextCell in adjacencyMap[currentCell])
        {
            if (nextCell != previousCell)
            {
                if (Check5InARowRecursive(player, nextCell, count, currentCell))
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Checks if a move is valid for a player.
    /// A valid move must land on an empty cell or an opponent's cell (bump).
    /// </summary>
    /// <param name="player">Player making the move</param>
    /// <param name="fromCell">Starting cell</param>
    /// <param name="toCell">Target cell</param>
    /// <returns>True if the move is legal</returns>
    public bool IsValidMove(Player player, int fromCell, int toCell)
    {
        // Bounds check
        if (fromCell < 0 || fromCell >= BOARD_SIZE || toCell < 0 || toCell >= BOARD_SIZE)
            return false;

        // Check if from cell has player's chip
        if (!cells[fromCell].HasChip || cells[fromCell].CurrentChip.Owner != player)
            return false;

        // Target cannot be same as source
        if (fromCell == toCell)
            return false;

        // Target must be adjacent
        if (!IsAdjacent(fromCell, toCell))
            return false;

        // Target must be empty or occupied by opponent
        if (cells[toCell].HasChip && cells[toCell].CurrentChip.Owner == player)
            return false; // Can't move to own chip

        return true;
    }

    /// <summary>
    /// Checks if a bump is valid on a target cell.
    /// A bump is valid if the target cell has an opponent's chip.
    /// </summary>
    /// <param name="player">Player attempting the bump</param>
    /// <param name="targetCell">Target cell to bump</param>
    /// <returns>True if bump is allowed</returns>
    public bool CanBump(Player player, int targetCell)
    {
        if (targetCell < 0 || targetCell >= BOARD_SIZE)
            return false;

        // Target must have a chip
        if (!cells[targetCell].HasChip)
            return false;

        // Chip must belong to opponent
        if (cells[targetCell].CurrentChip.Owner == player)
            return false;

        return true;
    }

    /// <summary>
    /// Applies a bump to a target cell, removing the opponent's chip.
    /// Updates scores accordingly.
    /// </summary>
    /// <param name="player">Player doing the bumping</param>
    /// <param name="targetCell">Target cell to bump</param>
    public void ApplyBump(Player player, int targetCell)
    {
        if (!CanBump(player, targetCell))
        {
            Debug.LogError($"Cannot bump cell {targetCell} for player {player.PlayerIndex}");
            return;
        }

        Chip bumpedChip = cells[targetCell].BumpChip();
        if (bumpedChip != null)
        {
            bumpedChip.Remove();
            player.AddScore(10); // Bump reward points
        }
    }

    /// <summary>
    /// Places a chip on a cell.
    /// </summary>
    /// <param name="chip">Chip to place</param>
    /// <param name="cellIndex">Target cell</param>
    public void PlaceChip(Chip chip, int cellIndex)
    {
        if (cellIndex < 0 || cellIndex >= BOARD_SIZE)
        {
            Debug.LogError($"Cell index {cellIndex} out of bounds");
            return;
        }

        cells[cellIndex].PlaceChip(chip);
        cells[cellIndex].Owner = chip.Owner;
    }

    /// <summary>
    /// Moves a chip from one cell to another.
    /// </summary>
    /// <param name="fromCell">Source cell</param>
    /// <param name="toCell">Target cell</param>
    public void MoveChip(int fromCell, int toCell)
    {
        if (!IsValidMove(cells[fromCell].CurrentChip.Owner, fromCell, toCell))
        {
            Debug.LogError($"Invalid move from {fromCell} to {toCell}");
            return;
        }

        Chip chip = cells[fromCell].RemoveChip();
        if (chip != null)
        {
            cells[toCell].PlaceChip(chip);
            cells[toCell].Owner = chip.Owner;
        }
    }

    /// <summary>
    /// Clears the board (resets all cells).
    /// </summary>
    public void Clear()
    {
        foreach (BoardCell cell in cells)
        {
            cell.Clear();
        }
    }

    /// <summary>
    /// Returns a string representation of the board state.
    /// </summary>
    public override string ToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("=== Board State ===");
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            sb.AppendLine($"Cell {i}: {cells[i]}");
        }
        return sb.ToString();
    }
}
