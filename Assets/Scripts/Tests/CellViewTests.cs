using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Comprehensive unit tests for CellView.
/// Tests cell state management, visual updates, and interaction handling.
/// </summary>
public class CellViewTests
{
    private GameObject cellGameObject;
    private CellView cellView;
    
    [SetUp]
    public void Setup()
    {
        // Create cell GameObject
        cellGameObject = new GameObject("TestCell");
        cellView = cellGameObject.AddComponent<CellView>();
        
        // Add required components
        cellGameObject.AddComponent<Image>();
        cellGameObject.AddComponent<CanvasGroup>();
    }
    
    [TearDown]
    public void Teardown()
    {
        Object.Destroy(cellGameObject);
    }
    
    // ============================================
    // INITIALIZATION TESTS
    // ============================================
    
    [Test]
    public void TestCellInitialization()
    {
        // Act
        cellView.Initialize(5);
        
        // Assert
        Assert.AreEqual(5, cellView.CellIndex);
        Assert.IsFalse(cellView.IsOccupied);
        Assert.IsNull(cellView.Occupant);
    }
    
    [Test]
    public void TestInitializationWithDifferentIndices()
    {
        // Test various indices
        for (int i = 0; i < 12; i++)
        {
            CellView cell = cellGameObject.AddComponent<CellView>();
            cell.Initialize(i);
            Assert.AreEqual(i, cell.CellIndex);
            Object.Destroy(cell);
        }
    }
    
    // ============================================
    // STATE MANAGEMENT TESTS
    // ============================================
    
    [Test]
    public void TestSetEmpty()
    {
        // Arrange
        cellView.Initialize(0);
        Player player = new Player(1, "TestPlayer");
        cellView.SetOccupied(player);
        
        // Act
        cellView.SetEmpty();
        
        // Assert
        Assert.IsFalse(cellView.IsOccupied);
        Assert.IsNull(cellView.Occupant);
    }
    
    [Test]
    public void TestSetOccupied()
    {
        // Arrange
        cellView.Initialize(0);
        Player player = new Player(1, "TestPlayer");
        
        // Act
        cellView.SetOccupied(player);
        
        // Assert
        Assert.IsTrue(cellView.IsOccupied);
        Assert.AreEqual(player, cellView.Occupant);
    }
    
    [Test]
    public void TestSetOccupiedWithNull()
    {
        // Arrange
        cellView.Initialize(0);
        
        // Act
        cellView.SetOccupied(null);
        
        // Assert
        Assert.IsFalse(cellView.IsOccupied);
    }
    
    [Test]
    public void TestSetHighlighted()
    {
        // Arrange
        cellView.Initialize(0);
        
        // Act
        cellView.SetHighlighted(true);
        
        // Assert
        Assert.IsTrue(cellView.IsHighlighted);
        
        // Act again
        cellView.SetHighlighted(false);
        
        // Assert
        Assert.IsFalse(cellView.IsHighlighted);
    }
    
    [Test]
    public void TestSetSelected()
    {
        // Arrange
        cellView.Initialize(0);
        
        // Act
        cellView.SetSelected(true);
        
        // Assert
        Assert.Pass("SetSelected completed without error");
    }
    
    // ============================================
    // STATE TRANSITIONS
    // ============================================
    
    [Test]
    public void TestEmptyToOccupiedTransition()
    {
        // Arrange
        cellView.Initialize(0);
        Player player = new Player(1, "Player1");
        
        // Assert initial state
        Assert.IsFalse(cellView.IsOccupied);
        
        // Act
        cellView.SetOccupied(player);
        
        // Assert final state
        Assert.IsTrue(cellView.IsOccupied);
        Assert.AreEqual(player, cellView.Occupant);
    }
    
    [Test]
    public void TestOccupiedToEmptyTransition()
    {
        // Arrange
        cellView.Initialize(0);
        Player player = new Player(1, "Player1");
        cellView.SetOccupied(player);
        
        // Assert initial state
        Assert.IsTrue(cellView.IsOccupied);
        
        // Act
        cellView.SetEmpty();
        
        // Assert final state
        Assert.IsFalse(cellView.IsOccupied);
        Assert.IsNull(cellView.Occupant);
    }
    
    [Test]
    public void TestMultipleStateChanges()
    {
        // Arrange
        cellView.Initialize(0);
        Player player1 = new Player(1, "Player1");
        Player player2 = new Player(2, "Player2");
        
        // Act & Assert through multiple transitions
        cellView.SetOccupied(player1);
        Assert.AreEqual(player1, cellView.Occupant);
        
        cellView.SetOccupied(player2);
        Assert.AreEqual(player2, cellView.Occupant);
        
        cellView.SetEmpty();
        Assert.IsNull(cellView.Occupant);
        
        cellView.SetOccupied(player1);
        Assert.AreEqual(player1, cellView.Occupant);
    }
    
    // ============================================
    // VISUAL STATE TESTS
    // ============================================
    
    [Test]
    public void TestHighlightingWithOccupant()
    {
        // Arrange
        cellView.Initialize(0);
        Player player = new Player(1, "Player1");
        cellView.SetOccupied(player);
        
        // Act
        cellView.SetHighlighted(true);
        
        // Assert
        Assert.IsTrue(cellView.IsHighlighted);
        Assert.IsTrue(cellView.IsOccupied);
    }
    
    [Test]
    public void TestSelectionState()
    {
        // Arrange
        cellView.Initialize(0);
        
        // Act
        cellView.SetSelected(true);
        cellView.SetSelected(false);
        
        // Assert - should not throw
        Assert.Pass("Selection state changes handled correctly");
    }
    
    // ============================================
    // INTERACTION TESTS
    // ============================================
    
    [Test]
    public void TestClickEvent()
    {
        // Arrange
        cellView.Initialize(0);
        bool eventFired = false;
        cellView.OnClicked += (cell) => { eventFired = true; };
        
        // Act
        cellView.OnPointerClick(null);
        
        // Assert
        Assert.IsTrue(eventFired);
    }
    
    [Test]
    public void TestHoverEvent()
    {
        // Arrange
        cellView.Initialize(0);
        bool eventFired = false;
        cellView.OnHovered += (cell) => { eventFired = true; };
        
        // Act
        cellView.OnPointerEnter(null);
        
        // Assert
        Assert.IsTrue(eventFired);
    }
    
    [Test]
    public void TestExitEvent()
    {
        // Arrange
        cellView.Initialize(0);
        bool eventFired = false;
        cellView.OnExited += (cell) => { eventFired = true; };
        
        // Act
        cellView.OnPointerExit(null);
        
        // Assert
        Assert.IsTrue(eventFired);
    }
    
    [Test]
    public void TestMultipleClickListeners()
    {
        // Arrange
        cellView.Initialize(0);
        int clickCount = 0;
        cellView.OnClicked += (cell) => { clickCount++; };
        cellView.OnClicked += (cell) => { clickCount++; };
        
        // Act
        cellView.OnPointerClick(null);
        
        // Assert
        Assert.AreEqual(2, clickCount);
    }
    
    // ============================================
    // PLAYER COLORS TESTS
    // ============================================
    
    [Test]
    public void TestDifferentPlayerColors()
    {
        // Arrange
        cellView.Initialize(0);
        Player player1 = new Player(1, "Player1");
        Player player2 = new Player(2, "Player2");
        Player player3 = new Player(3, "Player3");
        Player player4 = new Player(4, "Player4");
        
        // Act & Assert - should handle all player numbers
        cellView.SetOccupied(player1);
        Assert.AreEqual(player1, cellView.Occupant);
        
        cellView.SetOccupied(player2);
        Assert.AreEqual(player2, cellView.Occupant);
        
        cellView.SetOccupied(player3);
        Assert.AreEqual(player3, cellView.Occupant);
        
        cellView.SetOccupied(player4);
        Assert.AreEqual(player4, cellView.Occupant);
    }
    
    // ============================================
    // EDGE CASE TESTS
    // ============================================
    
    [Test]
    public void TestSettingHighlightMultipleTimes()
    {
        // Arrange
        cellView.Initialize(0);
        
        // Act
        cellView.SetHighlighted(true);
        cellView.SetHighlighted(true); // Set again
        cellView.SetHighlighted(true); // Set again
        
        // Assert
        Assert.IsTrue(cellView.IsHighlighted);
    }
    
    [Test]
    public void TestSettingEmptyMultipleTimes()
    {
        // Arrange
        cellView.Initialize(0);
        
        // Act
        cellView.SetEmpty();
        cellView.SetEmpty(); // Set again
        cellView.SetEmpty(); // Set again
        
        // Assert
        Assert.IsFalse(cellView.IsOccupied);
    }
}
