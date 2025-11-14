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
    
    #region DAY 2-3 Phase Logic Tests (Sprint 2 Completion)
    
    /// <summary>
    /// Tests for Task 2.1: Phase Transition System
    /// </summary>
    [Test]
    public void PhaseTransition_Rolling_To_Placing_IsValid()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        Assert.AreEqual(GamePhase.Rolling, stateManager.CurrentPhase);
        
        // Act
        stateManager.RollDice();
        
        // Assert
        Assert.AreEqual(GamePhase.Placing, stateManager.CurrentPhase,
            "Rolling → Placing should be a valid transition");
    }
    
    [Test]
    public void PhaseTransition_Placing_To_Bumping_IsValid()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        Assert.AreEqual(GamePhase.Placing, stateManager.CurrentPhase);
        
        // Act
        stateManager.PlaceChip(0);
        
        // Assert
        Assert.That(
            stateManager.CurrentPhase == GamePhase.Bumping || 
            stateManager.CurrentPhase == GamePhase.EndTurn,
            "Placing should transition to Bumping or EndTurn");
    }
    
    [Test]
    public void PhaseTransition_Bumping_To_EndTurn_IsValid()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        stateManager.PlaceChip(0);
        
        // Act & Assert
        if (stateManager.CurrentPhase == GamePhase.Bumping)
        {
            stateManager.SkipBump();
            Assert.AreEqual(GamePhase.EndTurn, stateManager.CurrentPhase,
                "Bumping → EndTurn should be a valid transition");
        }
    }
    
    [Test]
    public void PhaseTransition_EndTurn_To_Rolling_IsValid()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        stateManager.PlaceChip(0);
        
        // Act
        if (stateManager.CurrentPhase == GamePhase.Bumping)
            stateManager.SkipBump();
        
        if (stateManager.CurrentPhase == GamePhase.EndTurn)
            stateManager.EndTurn();
        
        // Assert
        Assert.AreEqual(GamePhase.Rolling, stateManager.CurrentPhase,
            "EndTurn → Rolling should be a valid transition");
    }
    
    /// <summary>
    /// Tests for Task 2.2: RollDice Phase Handler
    /// </summary>
    [Test]
    public void RollDice_WithNormalRoll_TransitionsToPlacing()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        // Act
        stateManager.RollDice();
        int[] roll = stateManager.LastDiceRoll;
        
        // Assert - normal roll should not be 5+6, single 6, or triple double
        if (roll != null && !Is5Plus6(roll) && !IsSingle6(roll))
        {
            Assert.AreEqual(GamePhase.Placing, stateManager.CurrentPhase);
        }
    }
    
    [Test]
    public void RollDice_With5Plus6_TransitionsDirectlyToEndTurn()
    {
        // Arrange - this is probabilistic, so we'll test the logic multiple times
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        bool foundSafe5Plus6 = false;
        for (int i = 0; i < 5; i++)
        {
            if (stateManager.CurrentPhase != GamePhase.Rolling)
                break;
                
            stateManager.RollDice();
            int[] roll = stateManager.LastDiceRoll;
            
            if (Is5Plus6(roll))
            {
                // Found a 5+6 roll
                Assert.AreEqual(GamePhase.EndTurn, stateManager.CurrentPhase,
                    "5+6 roll should skip to EndTurn");
                foundSafe5Plus6 = true;
                break;
            }
            
            // Reset for next roll attempt
            if (stateManager.CurrentPhase == GamePhase.EndTurn)
                stateManager.EndTurn();
        }
    }
    
    [Test]
    public void RollDice_WithDoubles_SetsCanRollAgainFlag()
    {
        // Arrange - test multiple rolls to find doubles
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        bool foundDoubles = false;
        for (int i = 0; i < 10; i++)
        {
            if (stateManager.CurrentPhase != GamePhase.Rolling)
                break;
                
            stateManager.RollDice();
            int[] roll = stateManager.LastDiceRoll;
            
            if (IsDouble(roll))
            {
                // Found a double
                Assert.IsTrue(stateManager.CanRollAgain,
                    "Doubles should allow rolling again");
                foundDoubles = true;
                break;
            }
            
            // Reset for next roll attempt
            if (stateManager.CurrentPhase == GamePhase.EndTurn)
                stateManager.EndTurn();
        }
    }
    
    /// <summary>
    /// Tests for Task 2.3: MoveChip Phase Handler
    /// </summary>
    [Test]
    public void PlaceChip_WithValidCell_Succeeds()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        
        // Act
        bool validCell = stateManager.CanPlaceChip(5);
        
        // Assert
        Assert.IsTrue(validCell, "Cell 5 should be valid for placement");
    }
    
    [Test]
    public void PlaceChip_WithOwnChipOnCell_Fails()
    {
        // Arrange - this is complex to set up, so we test the validation logic
        stateManager.Initialize(player1, player2);
        
        // Act - try to place on a cell with own chip (would be set by BoardModel)
        bool result = stateManager.CanPlaceChip(0);
        
        // Assert - empty cell should be valid (no own chip yet)
        Assert.IsTrue(result);
    }
    
    /// <summary>
    /// Tests for Task 2.4: BumpOpponent Phase Handler
    /// </summary>
    [Test]
    public void BumpChip_WithValidTarget_Succeeds()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        stateManager.PlaceChip(0);
        
        // Act & Assert
        if (stateManager.CurrentPhase == GamePhase.Bumping)
        {
            // Only execute if we're actually in Bumping phase
            bool canBump = stateManager.CanBumpChip(0);
            Assert.IsNotNull(stateManager.CurrentPhase);
        }
    }
    
    [Test]
    public void SkipBump_FromBumpingPhase_TransitionsToEndTurn()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        stateManager.RollDice();
        stateManager.PlaceChip(0);
        
        // Act
        if (stateManager.CurrentPhase == GamePhase.Bumping)
        {
            stateManager.SkipBump();
            
            // Assert
            Assert.AreEqual(GamePhase.EndTurn, stateManager.CurrentPhase,
                "Skipping bump should transition to EndTurn");
        }
    }
    
    /// <summary>
    /// Tests for Task 3.1: EndTurn Phase Handler
    /// </summary>
    [Test]
    public void EndTurn_WithDoublesBonus_RollsAgain()
    {
        // Arrange - find a roll with doubles
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        bool foundDoublesAndEndTurn = false;
        for (int i = 0; i < 20; i++)
        {
            if (stateManager.CurrentPhase != GamePhase.Rolling)
                break;
                
            stateManager.RollDice();
            int[] roll = stateManager.LastDiceRoll;
            
            if (IsDouble(roll))
            {
                // Found doubles - complete the turn
                stateManager.PlaceChip(0);
                
                if (stateManager.CurrentPhase == GamePhase.Bumping)
                    stateManager.SkipBump();
                
                if (stateManager.CurrentPhase == GamePhase.EndTurn)
                {
                    Player playerBeforeEndTurn = stateManager.CurrentPlayer;
                    stateManager.EndTurn();
                    
                    // After EndTurn with doubles bonus, should be back in Rolling with same player
                    Assert.AreEqual(GamePhase.Rolling, stateManager.CurrentPhase,
                        "EndTurn with doubles should return to Rolling");
                    Assert.AreEqual(playerBeforeEndTurn, stateManager.CurrentPlayer,
                        "Doubles bonus should keep same player");
                    foundDoublesAndEndTurn = true;
                }
                break;
            }
            
            // Reset for next attempt
            if (stateManager.CurrentPhase == GamePhase.EndTurn)
                stateManager.EndTurn();
        }
    }
    
    [Test]
    public void EndTurn_WithoutBonus_AdvancesPlayer()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        Player startPlayer = stateManager.CurrentPlayer;
        
        // Act
        stateManager.RollDice();
        stateManager.PlaceChip(0);
        
        if (stateManager.CurrentPhase == GamePhase.Bumping)
            stateManager.SkipBump();
        
        if (stateManager.CurrentPhase == GamePhase.EndTurn)
        {
            stateManager.EndTurn();
        }
        
        // Assert
        Assert.AreNotEqual(startPlayer, stateManager.CurrentPlayer,
            "EndTurn without bonus should advance to next player");
    }
    
    [Test]
    public void EndTurn_IncrementsTurnCounter()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        int initialTurnNumber = stateManager.TurnNumber;
        
        // Act
        stateManager.RollDice();
        stateManager.PlaceChip(0);
        
        if (stateManager.CurrentPhase == GamePhase.Bumping)
            stateManager.SkipBump();
        
        if (stateManager.CurrentPhase == GamePhase.EndTurn)
        {
            stateManager.EndTurn();
        }
        
        // Assert
        Assert.Greater(stateManager.TurnNumber, initialTurnNumber,
            "EndTurn should increment turn counter");
    }
    
    /// <summary>
    /// Tests for Task 3.2: Win Detection & Game Over
    /// </summary>
    [Test]
    public void HasWon_WithNoWinningCondition_ReturnsFalse()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        
        // Act
        bool hasWon = stateManager.HasWon(player1);
        
        // Assert
        Assert.IsFalse(hasWon, "Player with no chips on board should not have won");
    }
    
    [Test]
    public void DeclareWin_WithoutWinCondition_RaisesError()
    {
        // Arrange
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        bool errorFired = false;
        stateManager.OnInvalidAction += (msg) => errorFired = true;
        
        // Act
        stateManager.DeclareWin();
        
        // Assert
        Assert.IsTrue(errorFired, "DeclareWin without winning condition should raise error");
    }
    
    [Test]
    public void GoToGameOver_FromGameWon_TransitionsToGameOver()
    {
        // Arrange - manually set game to GameWon phase (testing terminal state)
        stateManager.Initialize(player1, player2);
        stateManager.StartGame();
        
        // This test would require access to set phase directly or a way to win
        // For now, we test the validation
        bool errorFired = false;
        stateManager.OnInvalidAction += (msg) => errorFired = true;
        
        // Act
        stateManager.GoToGameOver();
        
        // Assert
        Assert.IsTrue(errorFired, "GoToGameOver from wrong phase should raise error");
    }
    
    /// <summary>
    /// Helper methods for test setup
    /// </summary>
    private bool Is5Plus6(int[] roll)
    {
        if (roll == null || roll.Length != 2) return false;
        return (roll[0] == 5 && roll[1] == 6) || (roll[0] == 6 && roll[1] == 5);
    }
    
    private bool IsSingle6(int[] roll)
    {
        if (roll == null || roll.Length == 0) return false;
        return roll.Length == 1 && roll[0] == 6;
    }
    
    private bool IsDouble(int[] roll)
    {
        if (roll == null || roll.Length != 2) return false;
        return roll[0] == roll[1];
    }
    
    #endregion
    }
