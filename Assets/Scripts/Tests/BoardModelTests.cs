using NUnit.Framework;
using System.Collections.Generic;

/// <summary>
/// Unit tests for the BoardModel class.
/// Tests grid logic, chip placement, adjacency, bumping, and win detection.
/// </summary>
public class BoardModelTests
{
    private BoardModel boardModel;
    private Player player1;
    private Player player2;

    [SetUp]
    public void Setup()
    {
        player1 = new Player(0, "Player One");
        player2 = new Player(1, "Player Two");
        boardModel = new BoardModel(player1, player2);
    }

    [Test]
    public void CreateBoard_Has12Cells()
    {
        Assert.AreEqual(12, boardModel.Cells.Length);
    }

    [Test]
    public void CreateBoard_CellsAreEmpty()
    {
        foreach (BoardCell cell in boardModel.Cells)
        {
            Assert.IsFalse(cell.HasChip);
        }
    }

    [Test]
    public void CreateBoard_HasTwoPlayers()
    {
        Assert.AreEqual(2, boardModel.Players.Length);
        Assert.AreEqual(player1, boardModel.Players[0]);
        Assert.AreEqual(player2, boardModel.Players[1]);
    }

    [Test]
    public void PlaceChip_ChipOccupiesCell()
    {
        Chip chip = new Chip(player1);
        boardModel.PlaceChip(chip, 0);

        Assert.IsTrue(boardModel.Cells[0].HasChip);
        Assert.AreEqual(chip, boardModel.Cells[0].CurrentChip);
        Assert.AreEqual(player1, boardModel.Cells[0].Owner);
    }

    [Test]
    public void IsAdjacent_WithAdjacentCells_ReturnsTrue()
    {
        // Cell 0 is adjacent to cell 1 and cell 11
        Assert.IsTrue(boardModel.IsAdjacent(0, 1));
        Assert.IsTrue(boardModel.IsAdjacent(1, 0));
        Assert.IsTrue(boardModel.IsAdjacent(0, 11));
    }

    [Test]
    public void IsAdjacent_WithNonAdjacentCells_ReturnsFalse()
    {
        // Cell 0 is not adjacent to cell 5
        Assert.IsFalse(boardModel.IsAdjacent(0, 5));
    }

    [Test]
    public void GetAdjacentCells_ReturnsCorrectNeighbors()
    {
        List<int> neighbors = boardModel.GetAdjacentCells(0);
        Assert.Contains(1, neighbors);
        Assert.Contains(11, neighbors);
        Assert.AreEqual(2, neighbors.Count);
    }

    [Test]
    public void CanBump_WithOpponentChip_ReturnsTrue()
    {
        Chip chip = new Chip(player2);
        boardModel.PlaceChip(chip, 0);

        Assert.IsTrue(boardModel.CanBump(player1, 0));
    }

    [Test]
    public void CanBump_WithOwnChip_ReturnsFalse()
    {
        Chip chip = new Chip(player1);
        boardModel.PlaceChip(chip, 0);

        Assert.IsFalse(boardModel.CanBump(player1, 0));
    }

    [Test]
    public void CanBump_WithEmptyCell_ReturnsFalse()
    {
        Assert.IsFalse(boardModel.CanBump(player1, 0));
    }

    [Test]
    public void ApplyBump_RemovesChipFromBoard()
    {
        Chip chip = new Chip(player2);
        boardModel.PlaceChip(chip, 0);
        Assert.IsTrue(boardModel.Cells[0].HasChip);

        boardModel.ApplyBump(player1, 0);
        Assert.IsFalse(boardModel.Cells[0].HasChip);
    }

    [Test]
    public void ApplyBump_AwardsPointsToBumper()
    {
        int initialScore = player1.Score;
        Chip chip = new Chip(player2);
        boardModel.PlaceChip(chip, 0);

        boardModel.ApplyBump(player1, 0);
        Assert.Greater(player1.Score, initialScore);
    }

    [Test]
    public void IsValidMove_WithValidAdjacentMove_ReturnsTrue()
    {
        Chip chip = new Chip(player1);
        boardModel.PlaceChip(chip, 0);

        Assert.IsTrue(boardModel.IsValidMove(player1, 0, 1));
    }

    [Test]
    public void IsValidMove_WithInvalidDistance_ReturnsFalse()
    {
        Chip chip = new Chip(player1);
        boardModel.PlaceChip(chip, 0);

        Assert.IsFalse(boardModel.IsValidMove(player1, 0, 5));
    }

    [Test]
    public void IsValidMove_WithNoChip_ReturnsFalse()
    {
        Assert.IsFalse(boardModel.IsValidMove(player1, 0, 1));
    }

    [Test]
    public void IsValidMove_ToOwnChip_ReturnsFalse()
    {
        Chip chip1 = new Chip(player1);
        Chip chip2 = new Chip(player1);
        boardModel.PlaceChip(chip1, 0);
        boardModel.PlaceChip(chip2, 1);

        Assert.IsFalse(boardModel.IsValidMove(player1, 0, 1));
    }

    [Test]
    public void MoveChip_MovesChipToNewCell()
    {
        Chip chip = new Chip(player1);
        boardModel.PlaceChip(chip, 0);
        boardModel.MoveChip(0, 1);

        Assert.IsFalse(boardModel.Cells[0].HasChip);
        Assert.IsTrue(boardModel.Cells[1].HasChip);
        Assert.AreEqual(chip, boardModel.Cells[1].CurrentChip);
    }

    [Test]
    public void Check5InARow_WithoutWinningCondition_ReturnsFalse()
    {
        Chip chip1 = new Chip(player1);
        boardModel.PlaceChip(chip1, 0);

        Assert.IsFalse(boardModel.Check5InARow(player1));
    }

    [Test]
    public void Check5InARow_With5ConsecutiveChips_ReturnsTrue()
    {
        // Place 5 consecutive chips for player1 (0, 1, 2, 3, 4)
        for (int i = 0; i < 5; i++)
        {
            Chip chip = new Chip(player1);
            boardModel.PlaceChip(chip, i);
        }

        // Mark these cells as owned by player1
        for (int i = 0; i < 5; i++)
        {
            boardModel.Cells[i].Owner = player1;
        }

        Assert.IsTrue(boardModel.Check5InARow(player1));
    }

    [Test]
    public void Check5InARow_With4Chips_ReturnsFalse()
    {
        // Place only 4 chips
        for (int i = 0; i < 4; i++)
        {
            Chip chip = new Chip(player1);
            boardModel.PlaceChip(chip, i);
            boardModel.Cells[i].Owner = player1;
        }

        Assert.IsFalse(boardModel.Check5InARow(player1));
    }

    [Test]
    public void Check5InARow_WithCircularWrap_ReturnsTrue()
    {
        // Place 5 chips wrapping around (10, 11, 0, 1, 2)
        int[] positions = new int[] { 10, 11, 0, 1, 2 };
        foreach (int pos in positions)
        {
            Chip chip = new Chip(player1);
            boardModel.PlaceChip(chip, pos);
            boardModel.Cells[pos].Owner = player1;
        }

        Assert.IsTrue(boardModel.Check5InARow(player1));
    }

    [Test]
    public void Clear_RemovesAllChips()
    {
        Chip chip1 = new Chip(player1);
        Chip chip2 = new Chip(player2);
        boardModel.PlaceChip(chip1, 0);
        boardModel.PlaceChip(chip2, 1);

        boardModel.Clear();

        foreach (BoardCell cell in boardModel.Cells)
        {
            Assert.IsFalse(cell.HasChip);
        }
    }
}
