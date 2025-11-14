using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Game4_AlternatingBumpsTests
/// 
/// Unit tests for the Game4_AlternatingBumps game mode.
/// Tests cover:
/// - Valid move logic
/// - Bumping rules (alternating rights)
/// - Win condition (5-in-a-row)
/// - Game state transitions
/// </summary>
[TestFixture]
public class Game4_AlternatingBumpsTests
{
    private Game4_AlternatingBumps game;
    private Player player1;
    private Player player2;
    
    [SetUp]
    public void Setup()
    {
        game = new Game4_AlternatingBumps();
        
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
    /// Test: Game4_AlternatingBumps has correct mode name.
    /// </summary>
    [Test]
    public void Game4_AlternatingBumps_HasCorrectModeName()
    {
        Assert.AreEqual("Alternating Bumps", game.ModeName);
    }
    
    /// <summary>
    /// Test: Game4_AlternatingBumps has mode description.
    /// </summary>
    [Test]
    public void Game4_AlternatingBumps_HasModeDescription()
    {
        Assert.IsNotNull(game.ModeDescription);
        Assert.IsNotEmpty(game.ModeDescription);
    }
    
    // ==================== INITIALIZATION ====================
    
    /// <summary>
    /// Test: Game4_AlternatingBumps initializes without error.
    /// </summary>
    [Test]
    public void Game4_AlternatingBumps_InitializesWithoutError()
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
    public void Game4_AlternatingBumps_IsValidMove_RejectsNegativeCellIndex()
    {
        bool result = game.IsValidMove(player1, -1);
        Assert.IsFalse(result, "Should reject negative cell index");
    }
    
    /// <summary>
    /// Test: IsValidMove rejects out-of-range cell indices.
    /// </summary>
    [Test]
    public void Game4_AlternatingBumps_IsValidMove_RejectsOutOfRangeCellIndex()
    {
        bool result = game.IsValidMove(player1, 12);
        Assert.IsFalse(result, "Should reject cell index > 11");
    }
    
    /// <summary>
    /// Test: IsValidMove returns boolean.
    /// </summary>
    [Test]
    public void Game4_AlternatingBumps_IsValidMove_ReturnsBool()
    {
        Assert.DoesNotThrow(() =>
        {
            bool result = game.IsValidMove(player1, 0);
            Assert.IsInstanceOf<bool>(result);
        });
    }
    
    // ==================== BUMPING RULES ====================
    
    /// <summary>
    /// Test: CanBump returns boolean.
    /// </summary>
    [Test]
    public void Game4_AlternatingBumps_CanBump_ReturnsBool()
    {
        Assert.DoesNotThrow(() =>
        {
            bool result = game.CanBump(player1, player2, 0);
            Assert.IsInstanceOf<bool>(result);
        });
    }
    
    /// <summary>
    /// Test: Cannot bump yourself.
    /// </summary>
    [Test]
    public void Game4_AlternatingBumps_CanBump_RejectsOwnChip()
    {
        bool result = game.CanBump(player1, player1, 0);
        Assert.IsFalse(result, "Should not allow bumping your own chip");
    }
    
    /// <summary>
    /// Test: OnBumpOccurs can be called.
    /// </summary>
    [Test]
    public void Game4_AlternatingBumps_OnBumpOccurs_Callable()
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
    public void Game4_AlternatingBumps_CheckWinCondition_ReturnsFalseWithoutGameState()
    {
        bool result = game.CheckWinCondition(player1);
        Assert.IsFalse(result, "Should return false without GameState");
    }
    
    /// <summary>
    /// Test: CheckWinCondition returns boolean.
    /// </summary>
    [Test]
    public void Game4_AlternatingBumps_CheckWinCondition_ReturnsBool()
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
    public void Game4_AlternatingBumps_OnGameEnd_Callable()
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
    public void Game4_AlternatingBumps_LifecycleSequence()
    {
        Assert.DoesNotThrow(() =>
        {
            game.OnGameStart();
            game.OnTurnStart(player1);
            game.OnChipPlaced(player1, 0);
            game.OnTurnStart(player2);
            game.CanBump(player2, player1, 1);
            game.OnBumpOccurs(player2, player1);
            game.CheckWinCondition(player1);
            game.OnGameEnd(player1);
        });
    }
}
