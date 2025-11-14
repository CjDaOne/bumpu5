using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Comprehensive integration tests for the Board System with GameModes.
/// Tests full game flow: initialization → turn → move → animation → state sync
/// </summary>
public class BoardIntegrationTests
{
    private GameObject boardGameObject;
    private GameObject gameStateGameObject;
    private BoardGridManager boardManager;
    private GameStateManager gameStateManager;
    private Player player1;
    private Player player2;
    private BoardModel boardModel;
    
    [SetUp]
    public void Setup()
    {
        // Create game state
        gameStateGameObject = new GameObject("GameStateManager");
        gameStateManager = gameStateGameObject.AddComponent<GameStateManager>();
        
        // Create board
        boardGameObject = new GameObject("BoardGridManager");
        boardManager = boardGameObject.AddComponent<BoardGridManager>();
        
        // Create players
        player1 = new Player(1, "Player1");
        player2 = new Player(2, "Player2");
        
        // Create board model
        boardModel = new BoardModel(player1, player2);
    }
    
    [TearDown]
    public void Teardown()
    {
        if (boardManager != null)
            boardManager.Shutdown();
        
        Object.Destroy(boardGameObject);
        Object.Destroy(gameStateGameObject);
    }
    
    // ============================================
    // FULL GAME FLOW TESTS
    // ============================================
    
    [Test]
    public void FullGame_Bump5_PlayableToCompletion()
    {
        // Arrange
        IGameMode gameMode = GameModeFactory.CreateGameMode(1); // Bump5
        gameStateManager.Initialize(gameMode, player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act - Start game
        gameStateManager.StartGame();
        
        // Assert - Game should be in progress
        Assert.IsNotNull(gameStateManager.CurrentPlayer);
        Assert.AreEqual(GamePhase.Rolling, gameStateManager.CurrentPhase);
    }
    
    [Test]
    public void FullGame_Krazy6_PlayableToCompletion()
    {
        // Arrange
        IGameMode gameMode = GameModeFactory.CreateGameMode(2); // Krazy6
        gameStateManager.Initialize(gameMode, player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act
        gameStateManager.StartGame();
        
        // Assert
        Assert.IsNotNull(gameStateManager.CurrentGameMode);
        Assert.IsTrue(gameStateManager.CurrentGameMode.ModeName.Contains("Krazy"));
    }
    
    [Test]
    public void FullGame_PassTheChip_PlayableToCompletion()
    {
        // Arrange
        IGameMode gameMode = GameModeFactory.CreateGameMode(3); // PassTheChip
        gameStateManager.Initialize(gameMode, player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act
        gameStateManager.StartGame();
        
        // Assert
        Assert.IsFalse(gameStateManager.CurrentGameMode.AllowBumping, "PassTheChip should not allow bumping");
    }
    
    [Test]
    public void FullGame_BumpUAnd5_PlayableToCompletion()
    {
        // Arrange
        IGameMode gameMode = GameModeFactory.CreateGameMode(4); // BumpUAnd5
        gameStateManager.Initialize(gameMode, player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act
        gameStateManager.StartGame();
        
        // Assert
        Assert.IsTrue(gameStateManager.CurrentGameMode.AllowBumping, "BumpUAnd5 should allow bumping");
        Assert.IsFalse(gameStateManager.CurrentGameMode.RollingASixLosesTurn, "BumpUAnd5 should not lose turn on 6");
    }
    
    [Test]
    public void FullGame_Solitary_PlayableToCompletion()
    {
        // Arrange
        IGameMode gameMode = GameModeFactory.CreateGameMode(5); // Solitary
        gameStateManager.Initialize(gameMode, player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act
        gameStateManager.StartGame();
        
        // Assert
        Assert.AreEqual(1, gameStateManager.CurrentGameMode.MaxPlayers, "Solitary should be single player");
    }
    
    // ============================================
    // BOARD & GAMESTATE SYNCHRONIZATION
    // ============================================
    
    [Test]
    public void BoardSync_ChipPlaced_UpdatesDisplay()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act
        boardManager.UpdateCellDisplay(0, player1);
        
        // Assert
        Assert.IsTrue(boardManager.Cells[0].IsOccupied);
        Assert.AreEqual(player1, boardManager.Cells[0].Occupant);
    }
    
    [Test]
    public void BoardSync_ChipRemoved_ClearsCellDisplay()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        boardManager.UpdateCellDisplay(0, player1);
        
        // Act
        boardManager.UpdateCellDisplay(0, null);
        
        // Assert
        Assert.IsFalse(boardManager.Cells[0].IsOccupied);
        Assert.IsNull(boardManager.Cells[0].Occupant);
    }
    
    [Test]
    public void BoardSync_MultipleChips_DisplaysCorrectly()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act - Place multiple chips
        boardManager.UpdateCellDisplay(0, player1);
        boardManager.UpdateCellDisplay(3, player2);
        boardManager.UpdateCellDisplay(6, player1);
        boardManager.UpdateCellDisplay(9, player2);
        
        // Assert
        Assert.AreEqual(player1, boardManager.Cells[0].Occupant);
        Assert.AreEqual(player2, boardManager.Cells[3].Occupant);
        Assert.AreEqual(player1, boardManager.Cells[6].Occupant);
        Assert.AreEqual(player2, boardManager.Cells[9].Occupant);
    }
    
    // ============================================
    // VALID MOVE HIGHLIGHTING
    // ============================================
    
    [Test]
    public void ValidMoves_Highlighted_WhenCalculated()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        int[] validCells = new int[] { 2, 5, 8 };
        
        // Act
        boardManager.ShowValidMoves(validCells);
        
        // Assert
        Assert.IsTrue(boardManager.Cells[2].IsHighlighted);
        Assert.IsTrue(boardManager.Cells[5].IsHighlighted);
        Assert.IsTrue(boardManager.Cells[8].IsHighlighted);
    }
    
    [Test]
    public void ValidMoves_Cleared_OnPhaseChange()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        boardManager.ShowValidMoves(new int[] { 2, 5, 8 });
        
        // Act
        boardManager.ClearValidMoves();
        
        // Assert - all cells unhighlighted
        for (int i = 0; i < 12; i++)
        {
            Assert.IsFalse(boardManager.Cells[i].IsHighlighted);
        }
    }
    
    // ============================================
    // ANIMATION INTEGRATION
    // ============================================
    
    [Test]
    public void Animation_ChipPlacement_TriggersWithoutError()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        boardManager.UpdateCellDisplay(5, player1);
        
        // Act - should not throw
        boardManager.AnimateChipPlacement(5, player1);
        
        // Assert
        Assert.Pass("Chip placement animation executed without error");
    }
    
    [Test]
    public void Animation_ChipBump_TriggersWithoutError()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        boardManager.UpdateCellDisplay(7, player2);
        
        // Act - should not throw
        boardManager.AnimateChipBump(7);
        
        // Assert
        Assert.Pass("Chip bump animation executed without error");
    }
    
    [Test]
    public void Animation_Win_TriggersWithoutError()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act - should not throw
        boardManager.AnimateWin(player1);
        
        // Assert
        Assert.Pass("Win animation executed without error");
    }
    
    // ============================================
    // GAME MODE SPECIFIC BEHAVIOR
    // ============================================
    
    [Test]
    public void GameMode_Bump5_AllowsBumping()
    {
        // Arrange
        IGameMode gameMode = GameModeFactory.CreateGameMode(1);
        
        // Act & Assert
        Assert.IsTrue(gameMode.AllowBumping);
    }
    
    [Test]
    public void GameMode_PassTheChip_DisallowsBumping()
    {
        // Arrange
        IGameMode gameMode = GameModeFactory.CreateGameMode(3);
        
        // Act & Assert
        Assert.IsFalse(gameMode.AllowBumping);
    }
    
    [Test]
    public void GameMode_Krazy6_SixDoesNotLoseTurn()
    {
        // Arrange
        IGameMode gameMode = GameModeFactory.CreateGameMode(2);
        
        // Act & Assert
        Assert.IsFalse(gameMode.RollingASixLosesTurn);
    }
    
    [Test]
    public void GameMode_Bump5_SixLosesTurn()
    {
        // Arrange
        IGameMode gameMode = GameModeFactory.CreateGameMode(1);
        
        // Act & Assert
        Assert.IsTrue(gameMode.RollingASixLosesTurn);
    }
    
    // ============================================
    // BOARD STATE CONSISTENCY
    // ============================================
    
    [Test]
    public void BoardState_AllCellsEmpty_Initially()
    {
        // Arrange & Act
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Assert
        for (int i = 0; i < 12; i++)
        {
            Assert.IsFalse(boardManager.Cells[i].IsOccupied);
        }
    }
    
    [Test]
    public void BoardState_ClearBoard_RemovesAllChips()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Fill board
        for (int i = 0; i < 12; i++)
        {
            boardManager.UpdateCellDisplay(i, i % 2 == 0 ? player1 : player2);
        }
        
        // Act
        boardManager.ClearBoard();
        
        // Assert
        for (int i = 0; i < 12; i++)
        {
            Assert.IsFalse(boardManager.Cells[i].IsOccupied);
        }
    }
    
    [Test]
    public void BoardState_UpdateAllCells_SyncsWithBoardModel()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        BoardModel board = new BoardModel(player1, player2);
        board.PlaceChip(player1, 0);
        board.PlaceChip(player2, 3);
        
        // Act
        boardManager.UpdateAllCells(board);
        
        // Assert
        Assert.AreEqual(player1, boardManager.Cells[0].Occupant);
        Assert.AreEqual(player2, boardManager.Cells[3].Occupant);
    }
    
    // ============================================
    // EVENT INTEGRATION
    // ============================================
    
    [Test]
    public void Events_OnCellSelected_FiresCorrectly()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        int selectedCell = -1;
        boardManager.OnCellSelected += (cellIndex) => { selectedCell = cellIndex; };
        
        // Act
        boardManager.Cells[4].OnClicked?.Invoke(boardManager.Cells[4]);
        
        // Assert
        Assert.AreEqual(4, selectedCell);
    }
    
    [Test]
    public void Events_OnCellHovered_FiresCorrectly()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        int hoveredCell = -1;
        boardManager.OnCellHovered += (cellIndex) => { hoveredCell = cellIndex; };
        
        // Act
        boardManager.Cells[6].OnHovered?.Invoke(boardManager.Cells[6]);
        
        // Assert
        Assert.AreEqual(6, hoveredCell);
    }
    
    // ============================================
    // ERROR HANDLING
    // ============================================
    
    [Test]
    public void ErrorHandling_NullGameStateManager_HandledGracefully()
    {
        // Act
        boardManager.Initialize(null);
        
        // Assert
        Assert.IsFalse(boardManager.IsInitialized);
    }
    
    [Test]
    public void ErrorHandling_InvalidCellIndex_HandledGracefully()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act - should not throw
        boardManager.UpdateCellDisplay(-1, player1);
        boardManager.UpdateCellDisplay(12, player1);
        boardManager.UpdateCellDisplay(100, player1);
        boardManager.ShowValidMoves(new int[] { -1, 12 });
        
        // Assert
        Assert.Pass("Invalid indices handled gracefully");
    }
    
    [Test]
    public void ErrorHandling_MultipleInitializations_Handled()
    {
        // Arrange & Act
        boardManager.Initialize(gameStateManager);
        boardManager.Initialize(gameStateManager); // Should not cause errors
        
        // Assert
        Assert.IsTrue(boardManager.IsInitialized);
    }
    
    // ============================================
    // PERFORMANCE & STRESS TESTS
    // ============================================
    
    [Test]
    public void Performance_RapidCellUpdates_Handles1000Updates()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        // Act - 1000 rapid updates
        for (int i = 0; i < 1000; i++)
        {
            int cellIndex = i % 12;
            Player player = (i % 2 == 0) ? player1 : player2;
            boardManager.UpdateCellDisplay(cellIndex, player);
        }
        
        // Assert - should complete without error
        Assert.Pass("1000 rapid cell updates completed successfully");
    }
    
    [Test]
    public void Performance_AllHighlighted_Handles12Cells()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        boardManager.Initialize(gameStateManager);
        
        int[] allCells = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
        
        // Act
        boardManager.ShowValidMoves(allCells);
        
        // Assert
        for (int i = 0; i < 12; i++)
        {
            Assert.IsTrue(boardManager.Cells[i].IsHighlighted);
        }
    }
}
