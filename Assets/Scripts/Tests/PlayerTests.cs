using NUnit.Framework;
using System.Collections.Generic;

/// <summary>
/// Unit tests for the Player class.
/// Tests score management, chip tracking, and player state.
/// </summary>
public class PlayerTests
{
    private Player player1;
    private Player player2;

    [SetUp]
    public void Setup()
    {
        player1 = new Player(0, "Player One");
        player2 = new Player(1, "Player Two");
    }

    [Test]
    public void CreatePlayer_HasCorrectIndex()
    {
        Assert.AreEqual(0, player1.PlayerIndex);
        Assert.AreEqual(1, player2.PlayerIndex);
    }

    [Test]
    public void CreatePlayer_HasCorrectName()
    {
        Assert.AreEqual("Player One", player1.Name);
        Assert.AreEqual("Player Two", player2.Name);
    }

    [Test]
    public void CreatePlayer_StartsWithZeroScore()
    {
        Assert.AreEqual(0, player1.Score);
        Assert.AreEqual(0, player2.Score);
    }

    [Test]
    public void CreatePlayer_HasEmptyChipList()
    {
        Assert.AreEqual(0, player1.Chips.Count);
    }

    [Test]
    public void AddScore_IncreasesScore()
    {
        player1.AddScore(10);
        Assert.AreEqual(10, player1.Score);

        player1.AddScore(5);
        Assert.AreEqual(15, player1.Score);
    }

    [Test]
    public void RemoveScore_DecreasesScore()
    {
        player1.AddScore(20);
        player1.RemoveScore(5);
        Assert.AreEqual(15, player1.Score);
    }

    [Test]
    public void RemoveScore_CannotGoBelowZero()
    {
        player1.AddScore(5);
        player1.RemoveScore(10);
        Assert.AreEqual(0, player1.Score);
    }

    [Test]
    public void AddChip_AddsToChipList()
    {
        Chip chip = new Chip(player1);
        player1.Chips.Add(chip);
        Assert.AreEqual(1, player1.Chips.Count);
    }

    [Test]
    public void GetChipsOnBoard_ReturnsOnlyActiveChips()
    {
        BoardCell cell = new BoardCell(0);
        Chip chip1 = new Chip(player1);
        Chip chip2 = new Chip(player1);

        // Add chips to player
        player1.Chips.Add(chip1);
        player1.Chips.Add(chip2);

        // Only chip1 is on board
        chip1.MoveTo(cell);

        List<Chip> onBoard = player1.GetChipsOnBoard();
        Assert.AreEqual(1, onBoard.Count);
        Assert.Contains(chip1, onBoard);
    }

    [Test]
    public void GetChipsOffBoard_ReturnsInactiveChips()
    {
        Chip chip1 = new Chip(player1);
        Chip chip2 = new Chip(player1);

        player1.Chips.Add(chip1);
        player1.Chips.Add(chip2);

        List<Chip> offBoard = player1.GetChipsOffBoard();
        Assert.AreEqual(2, offBoard.Count);
    }

    [Test]
    public void SetPlayerName_UpdatesName()
    {
        player1.Name = "New Name";
        Assert.AreEqual("New Name", player1.Name);
    }

    [Test]
    public void IsActive_CanBeToggled()
    {
        Assert.IsTrue(player1.IsActive);
        player1.IsActive = false;
        Assert.IsFalse(player1.IsActive);
    }
}
