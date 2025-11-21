using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Core game board logic for Bump U Box.
/// Updated to a 5x5 grid (25 cells) with proper adjacency and win detection.
/// </summary>
public class BoardModel
{
    public const int GRID_SIZE = 5; // 5x5 grid
    public const int BOARD_SIZE = GRID_SIZE * GRID_SIZE; // 25 cells
    private const int WINNING_COUNT = 5; // 5 in a row

    private BoardCell[] cells;
    private Player[] players;
    private Dictionary<int, int[]> adjacencyMap; // cell index -> neighbor indices

    /// <summary>
    /// Gets all cells on the board.
    /// </summary>
    public BoardCell[] Cells => cells;

    /// <summary>
    /// Gets a specific cell by index.
    /// </summary>
    public BoardCell GetCell(int index)
    {
        if (index < 0 || index >= BOARD_SIZE) return null;
        return cells[index];
    }

    /// <summary>
    /// Gets all players in the game.
    /// </summary>
    public Player[] Players => players;

    /// <summary>
    /// Initializes a new BoardModel with 2 players.
    /// </summary>
    public BoardModel(Player player1, Player player2)
    {
        cells = new BoardCell[BOARD_SIZE];
        players = new Player[] { player1, player2 };
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            cells[i] = new BoardCell(i);
        }
        BuildAdjacencyMap();
    }

    /// <summary>
    /// Builds adjacency map for a 5x5 grid (up, down, left, right).
    /// </summary>
    private void BuildAdjacencyMap()
    {
        adjacencyMap = new Dictionary<int, int[]>();
        for (int index = 0; index < BOARD_SIZE; index++)
        {
            var (row, col) = IndexToCoord(index);
            var neighbors = new List<int>();
            // Up
            if (row > 0) neighbors.Add(CoordToIndex(row - 1, col));
            // Down
            if (row < GRID_SIZE - 1) neighbors.Add(CoordToIndex(row + 1, col));
            // Left
            if (col > 0) neighbors.Add(CoordToIndex(row, col - 1));
            // Right
            if (col < GRID_SIZE - 1) neighbors.Add(CoordToIndex(row, col + 1));
            adjacencyMap[index] = neighbors.ToArray();
        }
    }

    /// <summary>
    /// Convert linear index to (row, column).
    /// </summary>
    public (int row, int col) IndexToCoord(int index)
    {
        return (index / GRID_SIZE, index % GRID_SIZE);
    }

    /// <summary>
    /// Convert (row, column) to linear index.
    /// </summary>
    public int CoordToIndex(int row, int col)
    {
        return row * GRID_SIZE + col;
    }

    /// <summary>
    /// Gets adjacent cell indices for a given cell.
    /// </summary>
    public List<int> GetAdjacentCells(int cellIndex)
    {
        if (!adjacencyMap.ContainsKey(cellIndex))
        {
            Debug.LogError($"Cell index {cellIndex} out of bounds");
            return new List<int>();
        }
        return new List<int>(adjacencyMap[cellIndex]);
    }

    /// <summary>
    /// Checks if two cells are adjacent.
    /// </summary>
    public bool IsAdjacent(int cell1, int cell2)
    {
        return adjacencyMap.ContainsKey(cell1) && System.Array.Exists(adjacencyMap[cell1], n => n == cell2);
    }

    /// <summary>
    /// Checks if a player has 5 chips in a row (horizontal, vertical, or diagonal).
    /// </summary>
    public bool Check5InARow(Player player)
    {
        if (player == null) return false;
        // Horizontal
        for (int r = 0; r < GRID_SIZE; r++)
        {
            int count = 0;
            for (int c = 0; c < GRID_SIZE; c++)
            {
                int idx = CoordToIndex(r, c);
                if (cells[idx].Owner == player) count++; else count = 0;
                if (count >= WINNING_COUNT) return true;
            }
        }
        // Vertical
        for (int c = 0; c < GRID_SIZE; c++)
        {
            int count = 0;
            for (int r = 0; r < GRID_SIZE; r++)
            {
                int idx = CoordToIndex(r, c);
                if (cells[idx].Owner == player) count++; else count = 0;
                if (count >= WINNING_COUNT) return true;
            }
        }
        // Diagonal TL-BR
        for (int start = 0; start <= GRID_SIZE - WINNING_COUNT; start++)
        {
            int count = 0;
            for (int i = 0; i < GRID_SIZE - start; i++)
            {
                int idx = CoordToIndex(start + i, i);
                if (cells[idx].Owner == player) count++; else count = 0;
                if (count >= WINNING_COUNT) return true;
            }
        }
        // Diagonal TR-BL
        for (int start = WINNING_COUNT - 1; start < GRID_SIZE; start++)
        {
            int count = 0;
            for (int i = 0; i <= start; i++)
            {
                int idx = CoordToIndex(i, start - i);
                if (cells[idx].Owner == player) count++; else count = 0;
                if (count >= WINNING_COUNT) return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Checks if a move is valid (source has player's chip, target empty or opponent).
    /// </summary>
    public bool IsValidMove(Player player, int fromCell, int toCell)
    {
        if (fromCell < 0 || fromCell >= BOARD_SIZE || toCell < 0 || toCell >= BOARD_SIZE) return false;
        if (!cells[fromCell].HasChip || cells[fromCell].CurrentChip.Owner != player) return false;
        if (fromCell == toCell) return false;
        if (!IsAdjacent(fromCell, toCell)) return false;
        if (cells[toCell].HasChip && cells[toCell].CurrentChip.Owner == player) return false;
        return true;
    }

    /// <summary>
    /// Checks if a bump is valid on a target cell.
    /// </summary>
    public bool CanBump(Player player, int targetCell)
    {
        if (targetCell < 0 || targetCell >= BOARD_SIZE) return false;
        if (!cells[targetCell].HasChip) return false;
        if (cells[targetCell].CurrentChip.Owner == player) return false;
        return true;
    }

    /// <summary>
    /// Applies a bump, removing opponent's chip.
    /// </summary>
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
            player.AddScore(10);
        }
    }

    /// <summary>
    /// Places a chip on a cell.
    /// </summary>
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
    /// Clears the board.
    /// </summary>
    public void Clear()
    {
        foreach (BoardCell cell in cells)
        {
            cell.Clear();
        }
    }

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
