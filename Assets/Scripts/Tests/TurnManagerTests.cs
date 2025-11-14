using NUnit.Framework;
using System.Collections.Generic;

/// <summary>
/// Unit tests for the TurnManager class.
/// Tests player rotation, turn advancement, and player state tracking.
/// </summary>
public class TurnManagerTests
{
    private TurnManager turnManager;
    private Player player1;
    private Player player2;
    private List<Player> players;

    [SetUp]
    public void Setup()
    {
        player1 = new Player(0, "Player One");
        player2 = new Player(1, "Player Two");
        players = new List<Player> { player1, player2 };
        turnManager = new TurnManager(players);
    }

    [Test]
    public void CreateTurnManager_StartsWithFirstPlayer()
    {
        Assert.AreEqual(player1, turnManager.CurrentPlayer);
        Assert.AreEqual(0, turnManager.CurrentPlayerIndex);
    }

    [Test]
    public void GetCurrentPlayer_ReturnsCorrectPlayer()
    {
        Assert.AreEqual(player1, turnManager.GetCurrentPlayer());
    }

    [Test]
    public void GetNextPlayer_ReturnsNextPlayerInRotation()
    {
        Assert.AreEqual(player2, turnManager.GetNextPlayer());
    }

    [Test]
    public void AdvanceTurn_RotatesCorrectly()
    {
        Assert.AreEqual(player1, turnManager.CurrentPlayer);

        turnManager.AdvanceTurn();
        Assert.AreEqual(player2, turnManager.CurrentPlayer);
        Assert.AreEqual(1, turnManager.CurrentPlayerIndex);

        turnManager.AdvanceTurn();
        Assert.AreEqual(player1, turnManager.CurrentPlayer);
        Assert.AreEqual(0, turnManager.CurrentPlayerIndex);
    }

    [Test]
    public void AdvanceTurn_WrapsAroundCorrectly()
    {
        turnManager.AdvanceTurn();
        turnManager.AdvanceTurn();
        Assert.AreEqual(0, turnManager.CurrentPlayerIndex);
    }

    [Test]
    public void GetPlayerByIndex_ReturnsCorrectPlayer()
    {
        Assert.AreEqual(player1, turnManager.GetPlayerByIndex(0));
        Assert.AreEqual(player2, turnManager.GetPlayerByIndex(1));
    }

    [Test]
    public void GetPlayerCount_ReturnsCorrectCount()
    {
        Assert.AreEqual(2, turnManager.GetPlayerCount());
    }

    [Test]
    public void SetCurrentPlayer_ChangesActivePlayer()
    {
        Assert.AreEqual(0, turnManager.CurrentPlayerIndex);

        turnManager.SetCurrentPlayer(1);
        Assert.AreEqual(1, turnManager.CurrentPlayerIndex);
        Assert.AreEqual(player2, turnManager.CurrentPlayer);
    }

    [Test]
    public void SetCurrentPlayer_WithInvalidIndex_DoesNotChange()
    {
        int originalIndex = turnManager.CurrentPlayerIndex;
        turnManager.SetCurrentPlayer(5);
        Assert.AreEqual(originalIndex, turnManager.CurrentPlayerIndex);
    }

    [Test]
    public void Reset_ReturnsToFirstPlayer()
    {
        turnManager.AdvanceTurn();
        Assert.AreEqual(1, turnManager.CurrentPlayerIndex);

        turnManager.Reset();
        Assert.AreEqual(0, turnManager.CurrentPlayerIndex);
        Assert.AreEqual(player1, turnManager.CurrentPlayer);
    }

    [Test]
    public void MultiplePlayersInList_RotatesCorrectly()
    {
        Player player3 = new Player(2, "Player Three");
        List<Player> threePlayers = new List<Player> { player1, player2, player3 };
        TurnManager tm3 = new TurnManager(threePlayers);

        Assert.AreEqual(0, tm3.CurrentPlayerIndex);
        tm3.AdvanceTurn();
        Assert.AreEqual(1, tm3.CurrentPlayerIndex);
        tm3.AdvanceTurn();
        Assert.AreEqual(2, tm3.CurrentPlayerIndex);
        tm3.AdvanceTurn();
        Assert.AreEqual(0, tm3.CurrentPlayerIndex);
    }

    [Test]
    public void ToString_ReflectsCurrentPlayer()
    {
        string str = turnManager.ToString();
        Assert.Contains("Player One", str);
        Assert.Contains("Current Turn", str);
    }
}
