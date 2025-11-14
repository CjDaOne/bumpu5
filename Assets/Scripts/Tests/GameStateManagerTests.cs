using NUnit.Framework;
using System;

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
        // Act & Assert
        bool errorFired = false;
        stateManager.OnInvalidAction += (msg) => errorFired = true;
        stateManager.Initialize(null, player2);
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
    public void PlaceChip_TransitionsFromPlacingToBumping()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        
        // Act
        stateManager.PlaceChip(0);
        
        // Assert
        Assert.AreEqual(GamePhase.Bumping, stateManager.CurrentPhase);
    }
    
    [Test]
    public void BumpChip_TransitionsFromBumpingToEndTurn()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        stateManager.PlaceChip(0);
        
        // Act
        stateManager.BumpOpponentChip(1);
        
        // Assert
        Assert.AreEqual(GamePhase.EndTurn, stateManager.CurrentPhase);
    }
    
    [Test]
    public void EndTurn_TransitionsFromEndTurnToRollingNextPlayer()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        Player originalPlayer = stateManager.CurrentPlayer;
        
        // Act
        stateManager.EndTurn();
        
        // Assert
        Assert.AreEqual(GamePhase.Rolling, stateManager.CurrentPhase);
        Assert.AreNotEqual(originalPlayer, stateManager.CurrentPlayer);
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
    public void CanBumpChip_ReturnsTrueForValidCell()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act
        bool result = stateManager.CanBumpChip(5);
        
        // Assert
        Assert.IsTrue(result);
    }
    
    [Test]
    public void CanBumpChip_ReturnsFalseForInvalidCell()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act
        bool result = stateManager.CanBumpChip(100);
        
        // Assert
        Assert.IsFalse(result);
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
        Assert.AreEqual(initialTurns + 1, stateManager.TurnNumber);
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
    
    #endregion
}
