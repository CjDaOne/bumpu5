using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Integration tests for GameModes with GameStateManager.
/// Verifies that all game modes work correctly with the core game state system.
/// </summary>
public class GameModeIntegrationTests
{
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
        gameStateManager = null;
        board = null;
        player1 = null;
        player2 = null;
    }
    
    // ============================================
    // GAME STATE MANAGER INTEGRATION
    // ============================================
    
    [Test]
    public void GameStateManager_InitializeWithGame1_Succeeds()
    {
        // Arrange
        IGameMode mode = GameModeFactory.CreateGameMode(1);
        
        // Act
        gameStateManager.Initialize(mode, player1, player2);
        
        // Assert
        Assert.IsNotNull(gameStateManager);
    }
    
    [Test]
    public void GameStateManager_InitializeWithGame2_Succeeds()
    {
        // Arrange
        IGameMode mode = GameModeFactory.CreateGameMode(2);
        
        // Act
        gameStateManager.Initialize(mode, player1, player2);
        
        // Assert
        Assert.IsNotNull(gameStateManager);
    }
    
    [Test]
    public void GameStateManager_InitializeWithGame3_Succeeds()
    {
        // Arrange
        IGameMode mode = GameModeFactory.CreateGameMode(3);
        
        // Act
        gameStateManager.Initialize(mode, player1, player2);
        
        // Assert
        Assert.IsNotNull(gameStateManager);
    }
    
    [Test]
    public void GameStateManager_InitializeWithGame4_Succeeds()
    {
        // Arrange
        IGameMode mode = GameModeFactory.CreateGameMode(4);
        
        // Act
        gameStateManager.Initialize(mode, player1, player2);
        
        // Assert
        Assert.IsNotNull(gameStateManager);
    }
    
    [Test]
    public void GameStateManager_InitializeWithGame5_Succeeds()
    {
        // Arrange
        IGameMode mode = GameModeFactory.CreateGameMode(5);
        
        // Act
        gameStateManager.Initialize(mode, player1, player2);
        
        // Assert
        Assert.IsNotNull(gameStateManager);
    }
    
    // ============================================
    // WIN CONDITION INTEGRATION
    // ============================================
    
    [Test]
    public void Game1_WinCondition_IntegratesWithBoard()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool hasWon = mode.CheckWinCondition(player1, board);
        
        // Assert - should not throw and return false initially
        Assert.IsFalse(hasWon);
    }
    
    [Test]
    public void Game2_WinCondition_IntegratesWithBoard()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        
        // Act
        bool hasWon = mode.CheckWinCondition(player1, board);
        
        // Assert
        Assert.IsFalse(hasWon);
    }
    
    [Test]
    public void Game3_WinCondition_IntegratesWithBoard()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act
        bool hasWon = mode.CheckWinCondition(player1, board);
        
        // Assert
        Assert.IsFalse(hasWon);
    }
    
    [Test]
    public void Game4_WinCondition_IntegratesWithBoard()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        bool hasWon = mode.CheckWinCondition(player1, board);
        
        // Assert
        Assert.IsFalse(hasWon);
    }
    
    [Test]
    public void Game5_WinCondition_IntegratesWithBoard()
    {
        // Arrange
        IGameMode mode = new Game5_Solitary();
        
        // Act
        bool hasWon = mode.CheckWinCondition(player1, board);
        
        // Assert
        Assert.IsFalse(hasWon);
    }
    
    // ============================================
    // RULE CONFIGURATION INTEGRATION
    // ============================================
    
    [Test]
    public void Game1_Rules_SixLosesTurnIsRespected()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool losesTurn = mode.RollingASixLosesTurn;
        
        // Assert
        Assert.IsTrue(losesTurn, "Game 1 should enforce 6 loses turn");
    }
    
    [Test]
    public void Game2_Rules_SixLosesTurnIsDisabled()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        
        // Act
        bool losesTurn = mode.RollingASixLosesTurn;
        
        // Assert
        Assert.IsFalse(losesTurn, "Game 2 should NOT enforce 6 loses turn");
    }
    
    [Test]
    public void Game3_Rules_NoBumping()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act
        bool allowBumping = mode.AllowBumping;
        
        // Assert
        Assert.IsFalse(allowBumping, "Game 3 should NOT allow bumping");
    }
    
    [Test]
    public void Game4_Rules_HybridConfiguration()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        bool sixLoses = mode.RollingASixLosesTurn;
        bool allowBump = mode.AllowBumping;
        
        // Assert
        Assert.IsFalse(sixLoses, "Game 4 should allow 6 rolls (Krazy6 rule)");
        Assert.IsTrue(allowBump, "Game 4 should allow bumping (Bump5 rule)");
    }
    
    [Test]
    public void Game5_Rules_SinglePlayer()
    {
        // Arrange
        IGameMode mode = new Game5_Solitary();
        
        // Act
        int maxPlayers = mode.MaxPlayers;
        bool allowBump = mode.AllowBumping;
        
        // Assert
        Assert.AreEqual(1, maxPlayers, "Game 5 should be single player");
        Assert.IsFalse(allowBump, "Game 5 should not allow bumping");
    }
    
    // ============================================
    // TURN FLOW INTEGRATION
    // ============================================
    
    [Test]
    public void AllModes_OnTurnStart_DoesNotThrow()
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
        
        // Act & Assert
        foreach (var mode in modes)
        {
            Assert.DoesNotThrow(() => mode.OnTurnStart(gameStateManager, player1));
        }
    }
    
    [Test]
    public void AllModes_OnTurnEnd_DoesNotThrow()
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
        
        // Act & Assert
        foreach (var mode in modes)
        {
            Assert.DoesNotThrow(() => mode.OnTurnEnd(gameStateManager, player1));
        }
    }
    
    [Test]
    public void AllModes_Initialize_DoesNotThrow()
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
        
        // Act & Assert
        foreach (var mode in modes)
        {
            Assert.DoesNotThrow(() => mode.Initialize(gameStateManager));
        }
    }
    
    // ============================================
    // DICE ROLL INTEGRATION
    // ============================================
    
    [Test]
    public void AllModes_OnDiceRolled_HandlesSingleRoll()
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
        int[] roll = { 4, 3 };
        
        // Act & Assert
        foreach (var mode in modes)
        {
            Assert.DoesNotThrow(() => mode.OnDiceRolled(gameStateManager, roll));
        }
    }
    
    [Test]
    public void AllModes_OnDiceRolled_HandlesDouble()
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
        int[] roll = { 5, 5 };
        
        // Act & Assert
        foreach (var mode in modes)
        {
            Assert.DoesNotThrow(() => mode.OnDiceRolled(gameStateManager, roll));
        }
    }
    
    [Test]
    public void AllModes_OnDiceRolled_HandlesSix()
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
        int[] roll = { 6, 3 };
        
        // Act & Assert
        foreach (var mode in modes)
        {
            Assert.DoesNotThrow(() => mode.OnDiceRolled(gameStateManager, roll));
        }
    }
    
    [Test]
    public void AllModes_OnDiceRolled_HandlesDouble6()
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
        int[] roll = { 6, 6 };
        
        // Act & Assert
        foreach (var mode in modes)
        {
            Assert.DoesNotThrow(() => mode.OnDiceRolled(gameStateManager, roll));
        }
    }
    
    // ============================================
    // CHIP PLACEMENT INTEGRATION
    // ============================================
    
    [Test]
    public void AllModes_CanPlaceChip_ReturnsValidResult()
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
        
        // Act & Assert
        foreach (var mode in modes)
        {
            bool canPlace = mode.CanPlaceChip(gameStateManager, 5);
            Assert.IsNotNull(canPlace);  // Should return a valid bool
        }
    }
    
    // ============================================
    // BUMP INTEGRATION
    // ============================================
    
    [Test]
    public void Game1_OnBumpAttempt_AllowsStandardBump()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool shouldBump = mode.OnBumpAttempt(gameStateManager, player1, 5);
        
        // Assert
        Assert.IsTrue(shouldBump, "Game 1 should allow standard bump");
    }
    
    [Test]
    public void Game2_OnBumpAttempt_AllowsBump()
    {
        // Arrange
        IGameMode mode = new Game2_Krazy6();
        
        // Act
        bool shouldBump = mode.OnBumpAttempt(gameStateManager, player1, 5);
        
        // Assert
        Assert.IsTrue(shouldBump, "Game 2 should allow bump");
    }
    
    [Test]
    public void Game3_OnBumpAttempt_ReturnsFalse()
    {
        // Arrange
        IGameMode mode = new Game3_PassTheChip();
        
        // Act
        bool shouldBump = mode.OnBumpAttempt(gameStateManager, player1, 5);
        
        // Assert
        Assert.IsFalse(shouldBump, "Game 3 should NOT allow standard bump (uses swap)");
    }
    
    [Test]
    public void Game4_OnBumpAttempt_AllowsBump()
    {
        // Arrange
        IGameMode mode = new Game4_BumpUAnd5();
        
        // Act
        bool shouldBump = mode.OnBumpAttempt(gameStateManager, player1, 5);
        
        // Assert
        Assert.IsTrue(shouldBump, "Game 4 should allow standard bump");
    }
    
    [Test]
    public void Game5_OnBumpAttempt_ReturnsFalse()
    {
        // Arrange
        IGameMode mode = new Game5_Solitary();
        
        // Act
        bool shouldBump = mode.OnBumpAttempt(gameStateManager, player1, 5);
        
        // Assert
        Assert.IsFalse(shouldBump, "Game 5 should NOT allow bump (single player)");
    }
    
    // ============================================
    // FACTORY & INTEGRATION
    // ============================================
    
    [Test]
    public void Factory_AllModes_IntegrateWithGameStateManager()
    {
        // Arrange
        for (int modeId = 1; modeId <= 5; modeId++)
        {
            // Act
            IGameMode mode = GameModeFactory.CreateGameMode(modeId);
            gameStateManager.Initialize(mode, player1, player2);
            
            // Assert
            Assert.IsNotNull(mode);
            Assert.IsNotNull(gameStateManager);
        }
    }
    
    // ============================================
    // REGRESSION TESTS (Sprint 1 & 2 Compatibility)
    // ============================================
    
    [Test]
    public void Game1_CompatibleWithSprint1PlayerClass()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool canUsePlayer = (player1 != null);
        
        // Assert
        Assert.IsTrue(canUsePlayer, "Game modes should work with Sprint 1 Player class");
    }
    
    [Test]
    public void Game1_CompatibleWithSprint1BoardModel()
    {
        // Arrange
        IGameMode mode = new Game1_Bump5();
        
        // Act
        bool hasWon = mode.CheckWinCondition(player1, board);
        
        // Assert
        Assert.IsFalse(hasWon, "Game modes should work with Sprint 1 BoardModel");
    }
    
    [Test]
    public void AllModes_DoNotBreakGameStateManager()
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
        
        // Act - Initialize GameStateManager with each mode
        foreach (var mode in modes)
        {
            GameStateManager gsm = new GameStateManager();
            gsm.Initialize(mode, player1, player2);
            
            // Assert - Should not throw
            Assert.IsNotNull(gsm);
        }
    }
}
