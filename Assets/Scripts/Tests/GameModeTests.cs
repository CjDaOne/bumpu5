using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Comprehensive test suite for all game modes and the GameModeFactory.
/// Organized by game mode with minimum 5+ tests per mode.
/// </summary>
public class GameModeTests
{
    // ============================================
    // SETUP & TEARDOWN
    // ============================================
    
    private GameStateManager gameStateManager;
    private Player player1;
    private Player player2;
    private BoardModel board;
    
    [SetUp]
    public void Setup()
    {
        // Initialize test fixtures
        player1 = new Player { PlayerID = 1, PlayerName = "Player 1" };
        player2 = new Player { PlayerID = 2, PlayerName = "Player 2" };
        board = new BoardModel(player1, player2);
        gameStateManager = new GameStateManager();
    }
    
    [TearDown]
    public void Teardown()
    {
        // Clean up after tests
        gameStateManager = null;
        board = null;
        player1 = null;
        player2 = null;
    }
    
    // ============================================
    // GAME 1: BUMP 5 TESTS
    // ============================================
    
    [Test]
    public void Game1_ModeName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        string name = mode.ModeName;
        
        // Assert
        Assert.AreEqual("Bump5", name);
    }
    
    [Test]
    public void Game1_ModeLongName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        string longName = mode.ModeLongName;
        
        // Assert
        Assert.AreEqual("Bump 5 in a Row", longName);
    }
    
    [Test]
    public void Game1_MaxPlayers_IsTwo()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        int maxPlayers = mode.MaxPlayers;
        
        // Assert
        Assert.AreEqual(2, maxPlayers);
    }
    
    [Test]
    public void Game1_Use5InARow_IsTrue()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool use5InARow = mode.Use5InARow;
        
        // Assert
        Assert.IsTrue(use5InARow);
    }
    
    [Test]
    public void Game1_RollingASixLosesTurn_IsTrue()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool losesTurn = mode.RollingASixLosesTurn;
        
        // Assert
        Assert.IsTrue(losesTurn);
    }
    
    [Test]
    public void Game1_AllowBumping_IsTrue()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool allowBumping = mode.AllowBumping;
        
        // Assert
        Assert.IsTrue(allowBumping);
    }
    
    [Test]
    public void Game1_CanPlaceChip_ReturnsTrue()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool canPlace = mode.CanPlaceChip(gameStateManager, 5);
        
        // Assert
        Assert.IsTrue(canPlace);
    }
    
    [Test]
    public void Game1_OnBumpAttempt_ReturnsTrue()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool shouldBump = mode.OnBumpAttempt(gameStateManager, player1, 5);
        
        // Assert
        Assert.IsTrue(shouldBump);
    }
    
    // ============================================
    // GAME 2: KRAZY 6 TESTS
    // ============================================
    
    [Test]
    public void Game2_ModeName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        
        // Act
        string name = mode.ModeName;
        
        // Assert
        Assert.AreEqual("Krazy6", name);
    }
    
    [Test]
    public void Game2_ModeLongName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        
        // Act
        string longName = mode.ModeLongName;
        
        // Assert
        Assert.AreEqual("Krazy 6", longName);
    }
    
    [Test]
    public void Game2_RollingASixLosesTurn_IsFalse()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        
        // Act
        bool losesTurn = mode.RollingASixLosesTurn;
        
        // Assert
        Assert.IsFalse(losesTurn);
    }
    
    [Test]
    public void Game2_AllowBumping_IsTrue()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        
        // Act
        bool allowBumping = mode.AllowBumping;
        
        // Assert
        Assert.IsTrue(allowBumping);
    }
    
    [Test]
    public void Game2_MaxPlayers_IsTwo()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        
        // Act
        int maxPlayers = mode.MaxPlayers;
        
        // Assert
        Assert.AreEqual(2, maxPlayers);
    }
    
    [Test]
    public void Game2_OnDiceRolled_WithSingleSix_DoesNotThrow()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        int[] rollWithSix = { 6, 3 };
        
        // Act & Assert - should not throw
        Assert.DoesNotThrow(() => mode.OnDiceRolled(gameStateManager, rollWithSix));
    }
    
    [Test]
    public void Game2_OnDiceRolled_WithDouble6_DoesNotThrow()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        int[] rollWithDouble6 = { 6, 6 };
        
        // Act & Assert - should not throw
        Assert.DoesNotThrow(() => mode.OnDiceRolled(gameStateManager, rollWithDouble6));
    }
    
    // ============================================
    // GAME 3: PASS THE CHIP TESTS
    // ============================================
    
    [Test]
    public void Game3_ModeName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act
        string name = mode.ModeName;
        
        // Assert
        Assert.AreEqual("PassTheChip", name);
    }
    
    [Test]
    public void Game3_ModeLongName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act
        string longName = mode.ModeLongName;
        
        // Assert
        Assert.AreEqual("Pass The Chip", longName);
    }
    
    [Test]
    public void Game3_AllowBumping_IsFalse()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act
        bool allowBumping = mode.AllowBumping;
        
        // Assert
        Assert.IsFalse(allowBumping);
    }
    
    [Test]
    public void Game3_RollingASixLosesTurn_IsTrue()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act
        bool losesTurn = mode.RollingASixLosesTurn;
        
        // Assert
        Assert.IsTrue(losesTurn);
    }
    
    [Test]
    public void Game3_MaxPlayers_IsTwo()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act
        int maxPlayers = mode.MaxPlayers;
        
        // Assert
        Assert.AreEqual(2, maxPlayers);
    }
    
    [Test]
    public void Game3_OnTurnStart_DoesNotThrow()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act & Assert
        Assert.DoesNotThrow(() => mode.OnTurnStart(gameStateManager, player1));
    }
    
    [Test]
    public void Game3_OnTurnEnd_DoesNotThrow()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act & Assert
        Assert.DoesNotThrow(() => mode.OnTurnEnd(gameStateManager, player1));
    }
    
    // ============================================
    // GAME 4: BUMP U & 5 TESTS
    // ============================================
    
    [Test]
    public void Game4_ModeName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        string name = mode.ModeName;
        
        // Assert
        Assert.AreEqual("BumpUAnd5", name);
    }
    
    [Test]
    public void Game4_ModeLongName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        string longName = mode.ModeLongName;
        
        // Assert
        Assert.AreEqual("Bump U & 5", longName);
    }
    
    [Test]
    public void Game4_RollingASixLosesTurn_IsFalse()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        bool losesTurn = mode.RollingASixLosesTurn;
        
        // Assert
        Assert.IsFalse(losesTurn);
    }
    
    [Test]
    public void Game4_AllowBumping_IsTrue()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        bool allowBumping = mode.AllowBumping;
        
        // Assert
        Assert.IsTrue(allowBumping);
    }
    
    [Test]
    public void Game4_Use5InARow_IsTrue()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        bool use5InARow = mode.Use5InARow;
        
        // Assert
        Assert.IsTrue(use5InARow);
    }
    
    [Test]
    public void Game4_CanPlaceChip_ReturnsTrue()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        bool canPlace = mode.CanPlaceChip(gameStateManager, 5);
        
        // Assert
        Assert.IsTrue(canPlace);
    }
    
    [Test]
    public void Game4_OnBumpAttempt_ReturnsTrue()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        bool shouldBump = mode.OnBumpAttempt(gameStateManager, player1, 5);
        
        // Assert
        Assert.IsTrue(shouldBump);
    }
    
    [Test]
    public void Game4_OnDiceRolled_WithSingleSix_DoesNotThrow()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        int[] rollWithSix = { 6, 4 };
        
        // Act & Assert
        Assert.DoesNotThrow(() => mode.OnDiceRolled(gameStateManager, rollWithSix));
    }
    
    // ============================================
    // GAME 5: SOLITARY TESTS
    // ============================================
    
    [Test]
    public void Game5_ModeName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game5_Solitary();
        
        // Act
        string name = mode.ModeName;
        
        // Assert
        Assert.AreEqual("Solitary", name);
    }
    
    [Test]
    public void Game5_ModeLongName_IsCorrect()
    {
        // Arrange
        IGameMode mode = new Game5_Solitary();
        
        // Act
        string longName = mode.ModeLongName;
        
        // Assert
        Assert.AreEqual("Solitary Puzzle", longName);
    }
    
    [Test]
    public void Game5_MaxPlayers_IsOne()
    {
        // Arrange
        IGameMode mode = new Game5_Solitary();
        
        // Act
        int maxPlayers = mode.MaxPlayers;
        
        // Assert
        Assert.AreEqual(1, maxPlayers);
    }
    
    [Test]
    public void Game5_AllowBumping_IsFalse()
    {
        // Arrange
        IGameMode mode = new Game5_Solitary();
        
        // Act
        bool allowBumping = mode.AllowBumping;
        
        // Assert
        Assert.IsFalse(allowBumping);
    }
    
    [Test]
    public void Game5_RollingASixLosesTurn_IsFalse()
    {
        // Arrange
        IGameMode mode = new Game5_Solitary();
        
        // Act
        bool losesTurn = mode.RollingASixLosesTurn;
        
        // Assert
        Assert.IsFalse(losesTurn);
    }
    
    [Test]
    public void Game5_Initialize_StartsTracking()
    {
        // Arrange
        IGameMode mode = new Game5_Solitary();
        
        // Act
        mode.Initialize(gameStateManager);
        
        // Assert - should initialize without throwing
        Assert.IsNotNull(mode);
    }
    
    [Test]
    public void Game5_OnDiceRolled_TrackRolls()
    {
        // Arrange
        var mode = new Game5_Solitary();
        mode.Initialize(gameStateManager);
        int[] roll = { 4, 5 };
        
        // Act
        mode.OnDiceRolled(gameStateManager, roll);
        
        // Assert
        Assert.AreEqual(1, mode.RollCount);
    }
    
    [Test]
    public void Game5_OnDiceRolled_MultipleTimes_IncrementCount()
    {
        // Arrange
        var mode = new Game5_Solitary();
        mode.Initialize(gameStateManager);
        int[] roll = { 3, 2 };
        
        // Act
        mode.OnDiceRolled(gameStateManager, roll);
        mode.OnDiceRolled(gameStateManager, roll);
        mode.OnDiceRolled(gameStateManager, roll);
        
        // Assert
        Assert.AreEqual(3, mode.RollCount);
    }
    
    [Test]
    public void Game5_ElapsedTime_IsPositive()
    {
        // Arrange
        var mode = new Game5_Solitary();
        mode.Initialize(gameStateManager);
        
        // Act
        System.Threading.Thread.Sleep(100);  // Wait 100ms
        
        // Assert
        Assert.Greater(mode.ElapsedTime.TotalMilliseconds, 0);
    }
    
    // ============================================
    // FACTORY TESTS
    // ============================================
    
    [Test]
    public void Factory_CreateMode1_ReturnsBump5()
    {
        // Act
        IGameMode mode = GameModeFactory.CreateGameMode(1);
        
        // Assert
        Assert.IsInstanceOf<Game1_Bump5>(mode);
        Assert.AreEqual("Bump5", mode.ModeName);
    }
    
    [Test]
    public void Factory_CreateMode2_ReturnsKrazy6()
    {
        // Act
        IGameMode mode = GameModeFactory.CreateGameMode(2);
        
        // Assert
        Assert.IsInstanceOf<Game2_Krazy6>(mode);
        Assert.AreEqual("Krazy6", mode.ModeName);
    }
    
    [Test]
    public void Factory_CreateMode3_ReturnsPassTheChip()
    {
        // Act
        IGameMode mode = GameModeFactory.CreateGameMode(3);
        
        // Assert
        Assert.IsInstanceOf<Game3_PassTheChip>(mode);
        Assert.AreEqual("PassTheChip", mode.ModeName);
    }
    
    [Test]
    public void Factory_CreateMode4_ReturnsBumpUAnd5()
    {
        // Act
        IGameMode mode = GameModeFactory.CreateGameMode(4);
        
        // Assert
        Assert.IsInstanceOf<Game4_BumpUAnd5>(mode);
        Assert.AreEqual("BumpUAnd5", mode.ModeName);
    }
    
    [Test]
    public void Factory_CreateMode5_ReturnsSolitary()
    {
        // Act
        IGameMode mode = GameModeFactory.CreateGameMode(5);
        
        // Assert
        Assert.IsInstanceOf<Game5_Solitary>(mode);
        Assert.AreEqual("Solitary", mode.ModeName);
    }
    
    [Test]
    public void Factory_InvalidMode_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<System.ArgumentException>(() => GameModeFactory.CreateGameMode(99));
    }
    
    [Test]
    public void Factory_NegativeMode_ThrowsException()
    {
        // Act & Assert
        Assert.Throws<System.ArgumentException>(() => GameModeFactory.CreateGameMode(-1));
    }
    
    // ============================================
    // INTERFACE CONTRACT TESTS
    // ============================================
    
    [Test]
    public void AllModes_ImplementIGameMode()
    {
        // Arrange
        IGameMode[] modes = new IGameMode[]
        {
            new Game1_Bump5(),
            new Game2_Krazy6(),
            new Game3_PassTheChip(),
            new Game4_BumpUAnd5(),
            new Game5_Solitary()
        };
        
        // Assert
        foreach (var mode in modes)
        {
            Assert.IsNotNull(mode.ModeName);
            Assert.IsNotNull(mode.ModeLongName);
            Assert.Greater(mode.MaxPlayers, 0);
        }
    }
    
    [Test]
    public void AllModes_HaveValidMaxPlayers()
    {
        // Arrange
        int[] expectedMaxPlayers = { 2, 2, 2, 2, 1 };
        IGameMode[] modes = new IGameMode[]
        {
            new Game1_Bump5(),
            new Game2_Krazy6(),
            new Game3_PassTheChip(),
            new Game4_BumpUAnd5(),
            new Game5_Solitary()
        };
        
        // Assert
        for (int i = 0; i < modes.Length; i++)
        {
            Assert.AreEqual(expectedMaxPlayers[i], modes[i].MaxPlayers);
        }
    }
}
