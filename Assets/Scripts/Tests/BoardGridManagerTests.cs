using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Comprehensive unit tests for BoardGridManager.
/// Tests board creation, cell management, input handling, and game state integration.
/// </summary>
public class BoardGridManagerTests
{
    private GameStateManager gameStateManager;
    private BoardGridManager boardManager;
    private GameObject boardGameObject;
    
    [SetUp]
    public void Setup()
    {
        // Create game state manager
        GameObject gsObject = new GameObject("GameStateManager");
        gameStateManager = gsObject.AddComponent<GameStateManager>();
        
        // Create board manager
        boardGameObject = new GameObject("BoardGridManager");
        boardManager = boardGameObject.AddComponent<BoardGridManager>();
    }
    
    [TearDown]
    public void Teardown()
    {
        boardManager.Shutdown();
        Object.Destroy(boardGameObject);
        Object.Destroy(gameStateManager.gameObject);
    }
    
    // ============================================
    // INITIALIZATION TESTS
    // ============================================
    
    [Test]
    public void TestBoardInitialization()
    {
        // Arrange & Act
        boardManager.Initialize(gameStateManager);
        
        // Assert
        Assert.IsTrue(boardManager.IsInitialized);
        Assert.IsNotNull(boardManager.Cells);
        Assert.AreEqual(12, boardManager.Cells.Length);
    }
    
    [Test]
    public void TestBoardInitializationWithNullGameStateManager()
    {
        // Act
        boardManager.Initialize(null);
        
        // Assert
        Assert.IsFalse(boardManager.IsInitialized);
    }
    
    [Test]
    public void TestAllCellsCreated()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        
        // Assert
        for (int i = 0; i < 12; i++)
        {
            Assert.IsNotNull(boardManager.Cells[i]);
        }
    }
    
    // ============================================
    // CELL POSITIONING TESTS
    // ============================================
    
    [Test]
    public void TestCellPositioning()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        
        // Assert cells are positioned in circular layout
        CellView cell0 = boardManager.Cells[0];
        CellView cell6 = boardManager.Cells[6];
        
        Assert.IsNotNull(cell0);
        Assert.IsNotNull(cell6);
        
        // Cell 0 and 6 should be roughly opposite (180 degrees apart)
        Vector3 pos0 = cell0.transform.position;
        Vector3 pos6 = cell6.transform.position;
        
        float distance = Vector3.Distance(pos0, pos6);
        Assert.Greater(distance, 3f); // Should be roughly 2 * cellRadius apart
    }
    
    [Test]
    public void TestCellIndicesCorrect()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        
        // Assert each cell has correct index
        for (int i = 0; i < 12; i++)
        {
            Assert.AreEqual(i, boardManager.Cells[i].CellIndex);
        }
    }
    
    // ============================================
    // CELL STATE MANAGEMENT TESTS
    // ============================================
    
    [Test]
    public void TestUpdateCellDisplayEmpty()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        CellView cell = boardManager.Cells[0];
        
        // Act
        boardManager.UpdateCellDisplay(0, null);
        
        // Assert
        Assert.IsFalse(cell.IsOccupied);
        Assert.IsNull(cell.Occupant);
    }
    
    [Test]
    public void TestUpdateCellDisplayOccupied()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        Player player = new Player(1, "TestPlayer");
        CellView cell = boardManager.Cells[0];
        
        // Act
        boardManager.UpdateCellDisplay(0, player);
        
        // Assert
        Assert.IsTrue(cell.IsOccupied);
        Assert.AreEqual(player, cell.Occupant);
    }
    
    [Test]
    public void TestClearBoard()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        Player player = new Player(1, "TestPlayer");
        
        // Fill all cells
        for (int i = 0; i < 12; i++)
        {
            boardManager.UpdateCellDisplay(i, player);
        }
        
        // Act
        boardManager.ClearBoard();
        
        // Assert
        for (int i = 0; i < 12; i++)
        {
            Assert.IsFalse(boardManager.Cells[i].IsOccupied);
        }
    }
    
    // ============================================
    // VALID MOVES TESTS
    // ============================================
    
    [Test]
    public void TestShowValidMoves()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        int[] validMoves = new int[] { 2, 5, 8 };
        
        // Act
        boardManager.ShowValidMoves(validMoves);
        
        // Assert - cells should be highlighted
        Assert.IsTrue(boardManager.Cells[2].IsHighlighted);
        Assert.IsTrue(boardManager.Cells[5].IsHighlighted);
        Assert.IsTrue(boardManager.Cells[8].IsHighlighted);
        
        // Other cells should not be highlighted
        Assert.IsFalse(boardManager.Cells[0].IsHighlighted);
        Assert.IsFalse(boardManager.Cells[1].IsHighlighted);
    }
    
    [Test]
    public void TestClearValidMoves()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        int[] validMoves = new int[] { 2, 5, 8 };
        boardManager.ShowValidMoves(validMoves);
        
        // Act
        boardManager.ClearValidMoves();
        
        // Assert - all cells should be unhighlighted
        for (int i = 0; i < 12; i++)
        {
            Assert.IsFalse(boardManager.Cells[i].IsHighlighted);
        }
    }
    
    [Test]
    public void TestShowValidMovesWithNull()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        
        // Act
        boardManager.ShowValidMoves(null);
        
        // Assert - no cells should be highlighted
        for (int i = 0; i < 12; i++)
        {
            Assert.IsFalse(boardManager.Cells[i].IsHighlighted);
        }
    }
    
    // ============================================
    // ANIMATION TESTS
    // ============================================
    
    [Test]
    public void TestAnimateChipPlacement()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        Player player = new Player(1, "TestPlayer");
        
        // Act
        boardManager.AnimateChipPlacement(0, player);
        
        // Assert - should not throw exception
        Assert.Pass("Animation triggered without exception");
    }
    
    [Test]
    public void TestAnimateChipBump()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        
        // Act
        boardManager.AnimateChipBump(0);
        
        // Assert - should not throw exception
        Assert.Pass("Animation triggered without exception");
    }
    
    // ============================================
    // EVENT TESTS
    // ============================================
    
    [Test]
    public void TestOnCellSelectedEvent()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        int selectedCellIndex = -1;
        boardManager.OnCellSelected += (cellIndex) => { selectedCellIndex = cellIndex; };
        
        // Act
        boardManager.Cells[5].OnClicked?.Invoke(boardManager.Cells[5]);
        
        // Assert
        Assert.AreEqual(5, selectedCellIndex);
    }
    
    [Test]
    public void TestOnCellHoveredEvent()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        int hoveredCellIndex = -1;
        boardManager.OnCellHovered += (cellIndex) => { hoveredCellIndex = cellIndex; };
        
        // Act
        boardManager.Cells[3].OnHovered?.Invoke(boardManager.Cells[3]);
        
        // Assert
        Assert.AreEqual(3, hoveredCellIndex);
    }
    
    [Test]
    public void TestOnCellExitedEvent()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        int exitedCellIndex = -1;
        boardManager.OnCellExited += (cellIndex) => { exitedCellIndex = cellIndex; };
        
        // Act
        boardManager.Cells[7].OnExited?.Invoke(boardManager.Cells[7]);
        
        // Assert
        Assert.AreEqual(7, exitedCellIndex);
    }
    
    // ============================================
    // INVALID INPUT TESTS
    // ============================================
    
    [Test]
    public void TestUpdateCellDisplayInvalidIndex()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        Player player = new Player(1, "TestPlayer");
        
        // Act - should not throw
        boardManager.UpdateCellDisplay(-1, player);
        boardManager.UpdateCellDisplay(12, player);
        boardManager.UpdateCellDisplay(100, player);
        
        // Assert - should complete without error
        Assert.Pass("Invalid indices handled gracefully");
    }
    
    [Test]
    public void TestAnimateChipPlacementInvalidIndex()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        Player player = new Player(1, "TestPlayer");
        
        // Act - should not throw
        boardManager.AnimateChipPlacement(-1, player);
        boardManager.AnimateChipPlacement(12, player);
        
        // Assert
        Assert.Pass("Invalid animation indices handled gracefully");
    }
    
    // ============================================
    // SHUTDOWN TESTS
    // ============================================
    
    [Test]
    public void TestBoardShutdown()
    {
        // Arrange
        boardManager.Initialize(gameStateManager);
        
        // Act
        boardManager.Shutdown();
        
        // Assert
        Assert.IsFalse(boardManager.IsInitialized);
    }
}
