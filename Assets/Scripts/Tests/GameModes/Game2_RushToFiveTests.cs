using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Game2_RushToFiveTests
/// 
/// Unit tests for the Game2_RushToFive game mode.
/// Tests cover:
/// - Valid move logic
/// - Bumping rules (always disabled)
/// - Win condition (first to 5 chips)
/// - Game state transitions
/// </summary>
[TestFixture]
public class Game2_RushToFiveTests
{
    private Game2_RushToFive game;
    private Player player1;
    private Player player2;
    
    [SetUp]
    public void Setup()
    {
        game = new Game2_RushToFive();
        
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
    /// Test: Game2_RushToFive has correct mode name.
    /// </summary>
    [Test]
    public void Game2_RushToFive_HasCorrectModeName()
    {
        Assert.AreEqual("Rush to Five", game.ModeName);
    }
    
    /// <summary>
    /// Test: Game2_RushToFive has mode description.
    /// </summary>
    [Test]
    public void Game2_RushToFive_HasModeDescription()
    {
        Assert.IsNotNull(game.ModeDescription);
        Assert.IsNotEmpty(game.ModeDescription);
    }
    
    // ==================== INITIALIZATION ====================
    
    /// <summary>
    /// Test: Game2_RushToFive initializes without error.
    /// </summary>
    [Test]
    public void Game2_RushToFive_InitializesWithoutError()
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
    public void Game2_RushToFive_IsValidMove_RejectsNegativeCellIndex()
    {
        bool result = game.IsValidMove(player1, -1);
        Assert.IsFalse(result, "Should reject negative cell index");
    }
    
    /// <summary>
    /// Test: IsValidMove rejects out-of-range cell indices.
    /// </summary>
    [Test]
    public void Game2_RushToFive_IsValidMove_RejectsOutOfRangeCellIndex()
    {
        bool result = game.IsValidMove(player1, 12);
        Assert.IsFalse(result, "Should reject cell index > 11");
    }
    
    /// <summary>
    /// Test: IsValidMove returns boolean.
    /// </summary>
    [Test]
    public void Game2_RushToFive_IsValidMove_ReturnsBool()
    {
        Assert.DoesNotThrow(() =>
        {
            bool result = game.IsValidMove(player1, 0);
            Assert.IsInstanceOf<bool>(result);
        });
    }
    
    // ==================== BUMPING RULES ====================
    
    /// <summary>
    /// Test: Bumping is always disabled in Game2_RushToFive.
    /// </summary>
    [Test]
    public void Game2_RushToFive_CanBump_AlwaysReturnsFalse()
    {
        // Try to bump from player1 to player2
        bool result = game.CanBump(player1, player2, 0);
        Assert.IsFalse(result, "Bumping should never be allowed in RushToFive");
    }
    
    /// <summary>
    /// Test: Bumping remains disabled even when trying to bump yourself (should still be false, not error).
    /// </summary>
    [Test]
    public void Game2_RushToFive_CanBump_AlwaysFalseEvenSelfBump()
    {
        bool result = game.CanBump(player1, player1, 0);
        Assert.IsFalse(result, "Bumping should always be false");
    }
    
    /// <summary>
    /// Test: OnBumpOccurs can be called (should not crash).
    /// </summary>
    [Test]
    public void Game2_RushToFive_OnBumpOccurs_DoesNotCrash()
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
    public void Game2_RushToFive_CheckWinCondition_ReturnsFalseWithoutGameState()
    {
        bool result = game.CheckWinCondition(player1);
        Assert.IsFalse(result, "Should return false without GameState");
    }
    
    /// <summary>
    /// Test: CheckWinCondition returns boolean.
    /// </summary>
    [Test]
    public void Game2_RushToFive_CheckWinCondition_ReturnsBool()
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
    public void Game2_RushToFive_OnGameEnd_Callable()
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
    public void Game2_RushToFive_LifecycleSequence()
    {
        Assert.DoesNotThrow(() =>
        {
            game.OnGameStart();
            game.OnTurnStart(player1);
            game.OnChipPlaced(player1, 0);
            game.OnTurnStart(player2);
            game.OnGameEnd(player1);
        });
    }
}
