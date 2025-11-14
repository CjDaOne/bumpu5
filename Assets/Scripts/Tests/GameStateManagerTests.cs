using NUnit.Framework;
using System;
using System.Collections.Generic;

/// <summary>
/// Integration tests for GameStateManager.
/// Tests complete turn flow, phase transitions, event firing, and edge cases.
/// </summary>
public class GameStateManagerTests
{
    private GameStateManager stateManager;
    private Player player1;
    private Player player2;
    
    [SetUp]
    public void Setup()
    {
        stateManager = new GameStateManager();
        player1 = new Player(1, "Player 1");
        player2 = new Player(2, "Player 2");
    }
    
    [TearDown]
    public void TearDown()
    {
        stateManager = null;
        player1 = null;
        player2 = null;
    }
    
    #region Setup & Initialization Tests
    
    [Test]
    public void Initialize_WithValidPlayers_SetsUpGameCorrectly()
    {
        // Act
        stateManager.Initialize(player1, player2);
        
        // Assert
        Assert.AreEqual(GamePhase.Setup, stateManager.CurrentPhase);
        Assert.AreEqual(player1, stateManager.CurrentPlayer);
        Assert.AreEqual(0, stateManager.TurnNumber);
    }
    
    [Test]
    public void Initialize_WithNullPlayer_RaisesError()
    {
        // Arrange
        bool errorFired = false;
        stateManager.OnInvalidAction += (msg) => errorFired = true;
        
        // Act
        stateManager.Initialize(null, player2);
        
        // Assert
        Assert.IsTrue(errorFired);
    }
    
    [Test]
    public void StartGame_TransitionsToRollingPhase()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        bool phaseChanged = false;
        GamePhase newPhase = GamePhase.Setup;
        stateManager.OnPhaseChanged += (phase) => { phaseChanged = true; newPhase = phase; };
        
        // Act
        stateManager.StartGame();
        
        // Assert
        Assert.IsTrue(phaseChanged);
        Assert.AreEqual(GamePhase.Rolling, newPhase);
        Assert.AreEqual(GamePhase.Rolling, stateManager.CurrentPhase);
    }
    
    [Test]
    public void StartGame_InWrongPhase_RaisesError()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        bool errorFired = false;
        stateManager.OnInvalidAction += (msg) => errorFired = true;
        
        // Act
        stateManager.StartGame();
        
        // Assert
        Assert.IsTrue(errorFired);
    }
    
    #endregion
    
    #region Phase Transition Tests
    
    [Test]
    public void RollDice_TransitionsFromRollingToPlacing()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        // Act
        stateManager.RollDice();
        
        // Assert
        Assert.AreEqual(GamePhase.Placing, stateManager.CurrentPhase);
    }
    
    [Test]
    public void RollDice_InWrongPhase_RaisesError()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        bool errorFired = false;
        stateManager.OnInvalidAction += (msg) => errorFired = true;
        
        // Act
        stateManager.RollDice();
        
        // Assert
        Assert.IsTrue(errorFired);
    }
    
    [Test]
    public void RollDice_StoresRollResult()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        // Act
        stateManager.RollDice();
        int[] rollResult = stateManager.LastDiceRoll;
        
        // Assert
        Assert.IsNotNull(rollResult);
        Assert.AreEqual(2, rollResult.Length);
        Assert.GreaterOrEqual(rollResult[0], 1);
        Assert.LessOrEqual(rollResult[0], 6);
    }
    
    [Test]
    public void PlaceChip_TransitionsFromPlacingToBumping()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        
        // Act
        stateManager.PlaceChip(0);
        
        // Assert
        // Note: Phase depends on whether bump is possible at cell 0
        Assert.That(
            stateManager.CurrentPhase == GamePhase.Bumping || 
            stateManager.CurrentPhase == GamePhase.EndTurn,
            "Should be in Bumping or EndTurn phase"
        );
    }
    
    [Test]
    public void PlaceChip_WithInvalidCell_RaisesError()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        bool errorFired = false;
        stateManager.OnInvalidAction += (msg) => errorFired = true;
        
        // Act
        stateManager.PlaceChip(-1);
        
        // Assert
        Assert.IsTrue(errorFired);
    }
    
    [Test]
    public void PlaceChip_InWrongPhase_RaisesError()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        bool errorFired = false;
        stateManager.OnInvalidAction += (msg) => errorFired = true;
        
        // Act
        stateManager.PlaceChip(0);
        
        // Assert
        Assert.IsTrue(errorFired);
    }
    
    [Test]
    public void BumpChip_TransitionsToEndTurn()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        stateManager.PlaceChip(0);
        
        // Act - only try bump if in Bumping phase
        if (stateManager.CurrentPhase == GamePhase.Bumping && stateManager.CanBumpChip(1))
        {
            stateManager.BumpOpponentChip(1);
            Assert.AreEqual(GamePhase.EndTurn, stateManager.CurrentPhase);
        }
    }
    
    [Test]
    public void SkipBump_TransitionsToEndTurn()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        stateManager.PlaceChip(0);
        
        // Act - only skip if in Bumping phase
        if (stateManager.CurrentPhase == GamePhase.Bumping)
        {
            stateManager.SkipBump();
            Assert.AreEqual(GamePhase.EndTurn, stateManager.CurrentPhase);
        }
    }
    
    [Test]
    public void EndTurn_RotatesPlayer()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        Player originalPlayer = stateManager.CurrentPlayer;
        
        // Act
        stateManager.EndTurn();
        
        // Assert
        Assert.AreNotEqual(originalPlayer, stateManager.CurrentPlayer);
        Assert.AreEqual(GamePhase.Rolling, stateManager.CurrentPhase);
    }
    
    [Test]
    public void EndTurn_IncrementsTurnNumber()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        int initialTurns = stateManager.TurnNumber;
        
        // Act
        stateManager.EndTurn();
        
        // Assert
        Assert.Greater(stateManager.TurnNumber, initialTurns);
    }
    
    #endregion
    
    #region Event Firing Tests
    
    [Test]
    public void OnPhaseChanged_FiresForEachTransition()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        int fireCount = 0;
        stateManager.OnPhaseChanged += (phase) => fireCount++;
        
        // Act
        stateManager.StartGame();
        
        // Assert
        Assert.Greater(fireCount, 0);
    }
    
    [Test]
    public void OnDiceRolled_FiresWithCorrectValues()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        int[] firedRoll = null;
        stateManager.OnDiceRolled += (roll) => firedRoll = roll;
        
        // Act
        stateManager.RollDice();
        
        // Assert
        Assert.IsNotNull(firedRoll);
        Assert.AreEqual(firedRoll, stateManager.LastDiceRoll);
        Assert.AreEqual(2, firedRoll.Length);
    }
    
    [Test]
    public void OnPlayerChanged_FiresAfterEndTurn()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        Player firedPlayer = null;
        stateManager.OnPlayerChanged += (player) => firedPlayer = player;
        
        // Act
        stateManager.EndTurn();
        
        // Assert
        Assert.IsNotNull(firedPlayer);
        Assert.AreEqual(stateManager.CurrentPlayer, firedPlayer);
    }
    
    [Test]
    public void OnInvalidAction_FiresForBadMove()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        bool errorFired = false;
        stateManager.OnInvalidAction += (msg) => errorFired = true;
        
        // Act
        stateManager.RollDice(); // Invalid: not in Rolling phase
        
        // Assert
        Assert.IsTrue(errorFired);
    }
    
    #endregion
    
    #region State Query Tests
    
    [Test]
    public void CanPlaceChip_ReturnsTrueForValidCell()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act
        bool result = stateManager.CanPlaceChip(5);
        
        // Assert
        Assert.IsTrue(result);
    }
    
    [Test]
    public void CanPlaceChip_ReturnsFalseForInvalidCell()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act
        bool result = stateManager.CanPlaceChip(-1);
        
        // Assert
        Assert.IsFalse(result);
    }
    
    [Test]
    public void CanPlaceChip_ReturnsFalseForOutOfBounds()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act
        bool result = stateManager.CanPlaceChip(100);
        
        // Assert
        Assert.IsFalse(result);
    }
    
    [Test]
    public void CanBumpChip_ReturnsFalseForEmptyCell()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act
        bool result = stateManager.CanBumpChip(5);
        
        // Assert
        // Empty cell should return false (no chip to bump)
        Assert.IsFalse(result);
    }
    
    [Test]
    public void CanBumpChip_ReturnsFalseForInvalidCell()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act
        bool result = stateManager.CanBumpChip(-1);
        
        // Assert
        Assert.IsFalse(result);
    }
    
    [Test]
    public void GetValidMoves_ReturnsListOfCells()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        
        // Act
        List<int> validMoves = stateManager.GetValidMoves();
        
        // Assert
        Assert.IsNotNull(validMoves);
        Assert.IsInstanceOf<List<int>>(validMoves);
    }
    
    #endregion
    
    #region Edge Case Tests
    
    [Test]
    public void TurnNumber_IncrementOnEndTurn()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        int initialTurns = stateManager.TurnNumber;
        
        // Act
        stateManager.EndTurn();
        
        // Assert
        Assert.Greater(stateManager.TurnNumber, initialTurns);
    }
    
    [Test]
    public void LastDiceRoll_StoresRollResult()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        // Act
        stateManager.RollDice();
        int[] rollResult = stateManager.LastDiceRoll;
        
        // Assert
        Assert.IsNotNull(rollResult);
        Assert.Greater(rollResult.Length, 0);
    }
    
    [Test]
    public void ConsecutiveRolls_WorksCorrectly()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        // Act
        stateManager.RollDice();
        GamePhase phaseAfterRoll1 = stateManager.CurrentPhase;
        
        // Assert
        Assert.That(
            phaseAfterRoll1 == GamePhase.Placing || 
            phaseAfterRoll1 == GamePhase.EndTurn ||
            phaseAfterRoll1 == GamePhase.GameOver,
            "Should transition to valid phase after roll"
        );
    }
    
    [Test]
    public void HasWon_ReturnsFalseWhenNoWinCondition()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act
        bool hasWon = stateManager.HasWon(player1);
        
        // Assert
        Assert.IsFalse(hasWon);
    }
    
    [Test]
    public void CanRollAgain_InitiallyFalse()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act & Assert
        Assert.IsFalse(stateManager.CanRollAgain);
    }
    
    #endregion
    
    #region Integration Tests
    
    [Test]
    public void FullGameFlow_InitializeToRolling()
    {
        // Arrange & Act
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        // Assert
        Assert.AreEqual(GamePhase.Rolling, stateManager.CurrentPhase);
        Assert.AreEqual(1, stateManager.TurnNumber);
    }
    
    [Test]
    public void FullGameFlow_RollingToPlacingToEndTurn()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        Player startPlayer = stateManager.CurrentPlayer;
        
        // Act
        stateManager.RollDice();
        Assert.AreEqual(GamePhase.Placing, stateManager.CurrentPhase);
        
        stateManager.PlaceChip(0);
        Assert.That(
            stateManager.CurrentPhase == GamePhase.Bumping || 
            stateManager.CurrentPhase == GamePhase.EndTurn
        );
        
        if (stateManager.CurrentPhase == GamePhase.Bumping)
        {
            stateManager.SkipBump();
        }
        
        Assert.AreEqual(GamePhase.EndTurn, stateManager.CurrentPhase);
        
        stateManager.EndTurn();
        Assert.AreEqual(GamePhase.Rolling, stateManager.CurrentPhase);
        Assert.AreNotEqual(startPlayer, stateManager.CurrentPlayer);
    }
    
    #endregion
}
