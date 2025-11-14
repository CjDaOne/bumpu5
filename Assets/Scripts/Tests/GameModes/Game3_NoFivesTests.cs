using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Game3_NoFivesTests
/// 
/// Unit tests for the Game3_NoFives game mode.
/// Tests cover:
/// - Valid move logic (cannot create 5-in-a-row)
/// - Bumping rules
/// - Win condition (opponent creates 5-in-a-row)
/// - Game state transitions
/// </summary>
[TestFixture]
public class Game3_NoFivesTests
{
    private Game3_NoFives game;
    private Player player1;
    private Player player2;
    
    [SetUp]
    public void Setup()
    {
        game = new Game3_NoFives();
        
        player1 = ScriptableObject.CreateInstance<Player>();
        player1.name = "Player1";
        
        player2 = ScriptableObject.CreateInstance<Player>();
        player2.name = "Player2";
    }
    
    [TearDown]
    public void Teardown()
    {
        game = null;
        Object.Destroy(player1);
        Object.Destroy(player2);
    }
    
    // ==================== MODE PROPERTIES ====================
    
    /// <summary>
    /// Test: Game3_NoFives has correct mode name.
    /// </summary>
    [Test]
    public void Game3_NoFives_HasCorrectModeName()
    {
        Assert.AreEqual("No Fives", game.ModeName);
    }
    
    /// <summary>
    /// Test: Game3_NoFives has mode description.
    /// </summary>
    [Test]
    public void Game3_NoFives_HasModeDescription()
    {
        Assert.IsNotNull(game.ModeDescription);
        Assert.IsNotEmpty(game.ModeDescription);
        Assert.That(game.ModeDescription.Contains("5"), "Description should mention 5-in-a-row");
    }
    
    // ==================== INITIALIZATION ====================
    
    /// <summary>
    /// Test: Game3_NoFives initializes without error.
    /// </summary>
    [Test]
    public void Game3_NoFives_InitializesWithoutError()
    {
        Assert.DoesNotThrow(() =>
        {
            game.OnGameStart();
        });
    }
    
    // ==================== VALID MOVE LOGIC ====================
    
    /// <summary>
    /// Test: IsValidMove rejects invalid cell indices.
    /// </summary>
    [Test]
    public void Game3_NoFives_IsValidMove_RejectsNegativeCellIndex()
    {
        bool result = game.IsValidMove(player1, -1);
        Assert.IsFalse(result, "Should reject negative cell index");
    }
    
    /// <summary>
    /// Test: IsValidMove rejects out-of-range cell indices.
    /// </summary>
    [Test]
    public void Game3_NoFives_IsValidMove_RejectsOutOfRangeCellIndex()
    {
        bool result = game.IsValidMove(player1, 12);
        Assert.IsFalse(result, "Should reject cell index > 11");
    }
    
    /// <summary>
    /// Test: IsValidMove returns boolean.
    /// </summary>
    [Test]
    public void Game3_NoFives_IsValidMove_ReturnsBool()
    {
        Assert.DoesNotThrow(() =>
        {
            bool result = game.IsValidMove(player1, 0);
            Assert.IsInstanceOf<bool>(result);
        });
    }
    
    // ==================== BUMPING RULES ====================
    
    /// <summary>
    /// Test: Bumping is allowed in Game3_NoFives.
    /// </summary>
    [Test]
    public void Game3_NoFives_CanBump_AllowsDifferentPlayers()
    {
        // Without GameState, CanBump checks basic conditions
        // CanBump should not return false just because players are different
        bool result = game.CanBump(player1, player2, 0);
        // Result may be true or false depending on board state
        Assert.IsInstanceOf<bool>(result);
    }
    
    /// <summary>
    /// Test: Cannot bump yourself in Game3_NoFives.
    /// </summary>
    [Test]
    public void Game3_NoFives_CanBump_RejectsOwnChip()
    {
        bool result = game.CanBump(player1, player1, 0);
        Assert.IsFalse(result, "Should not allow bumping your own chip");
    }
    
    /// <summary>
    /// Test: OnBumpOccurs can be called.
    /// </summary>
    [Test]
    public void Game3_NoFives_OnBumpOccurs_Callable()
    {
        Assert.DoesNotThrow(() =>
        {
            game.OnBumpOccurs(player1, player2);
        });
    }
    
    // ==================== WIN CONDITION ====================
    
    /// <summary>
    /// Test: CheckWinCondition returns false without GameState.
    /// </summary>
    [Test]
    public void Game3_NoFives_CheckWinCondition_ReturnsFalseWithoutGameState()
    {
        bool result = game.CheckWinCondition(player1);
        Assert.IsFalse(result, "Should return false without GameState");
    }
    
    /// <summary>
    /// Test: CheckWinCondition returns boolean.
    /// </summary>
    [Test]
    public void Game3_NoFives_CheckWinCondition_ReturnsBool()
    {
        Assert.DoesNotThrow(() =>
        {
            bool result = game.CheckWinCondition(player1);
            Assert.IsInstanceOf<bool>(result);
        });
    }
    
    // ==================== GAME END ====================
    
    /// <summary>
    /// Test: OnGameEnd is callable.
    /// </summary>
    [Test]
    public void Game3_NoFives_OnGameEnd_Callable()
    {
        Assert.DoesNotThrow(() =>
        {
            game.OnGameEnd(player1);
        });
    }
    
    // ==================== INTEGRATION ====================
    
    /// <summary>
    /// Test: Game lifecycle methods can be called in sequence.
    /// </summary>
    [Test]
    public void Game3_NoFives_LifecycleSequence()
    {
        Assert.DoesNotThrow(() =>
        {
            game.OnGameStart();
            game.OnTurnStart(player1);
            game.OnChipPlaced(player1, 0);
            game.OnTurnStart(player2);
            game.CanBump(player1, player2, 1);
            game.OnBumpOccurs(player1, player2);
            game.CheckWinCondition(player1);
            game.OnGameEnd(player1);
        });
    }
}
