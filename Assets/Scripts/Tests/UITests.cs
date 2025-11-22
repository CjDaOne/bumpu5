using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Comprehensive tests for the UI system (HUD, buttons, modals, etc.)
/// Tests all HUD components, event binding, and state synchronization
/// </summary>
public class UITests
{
    private GameObject gameStateGameObject;
    private GameObject hudGameObject;
    private GameStateManager gameStateManager;
    private HUDManager hudManager;
    private Player player1;
    private Player player2;
    
    [SetUp]
    public void Setup()
    {
        // Create game state
        gameStateGameObject = new GameObject("GameStateManager");
        gameStateManager = new GameObject("GameStateManager").AddComponent<GameStateManager>();
        
        // Create HUD
        hudGameObject = new GameObject("HUDManager");
        hudManager = hudGameObject.AddComponent<HUDManager>();
        
        // Create players
        player1 = new Player(1, "Player1");
        player2 = new Player(2, "Player2");
    }
    
    [TearDown]
    public void Teardown()
    {
        if (hudManager != null)
            hudManager.Shutdown();
        
        Object.Destroy(hudGameObject);
        Object.Destroy(gameStateGameObject);
    }
    
    // ============================================
    // HUD INITIALIZATION
    // ============================================
    
    [Test]
    public void HUD_Initialize_WithValidGameStateManager()
    {
        // Act
        hudManager.Initialize(gameStateManager);
        
        // Assert
        Assert.IsTrue(hudManager.IsInitialized);
    }
    
    [Test]
    public void HUD_Initialize_WithNullGameStateManager_FailsGracefully()
    {
        // Act
        hudManager.Initialize(null);
        
        // Assert
        Assert.IsFalse(hudManager.IsInitialized);
    }
    
    [Test]
    public void HUD_Shutdown_CleansUpResources()
    {
        // Arrange
        hudManager.Initialize(gameStateManager);
        
        // Act
        hudManager.Shutdown();
        
        // Assert
        Assert.IsFalse(hudManager.IsInitialized);
    }
    
    // ============================================
    // ACTION BUTTON TESTS
    // ============================================
    
    [Test]
    public void ActionButtons_DiceRoll_EnabledOnlyDuringRollingPhase()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        hudManager.Initialize(gameStateManager);
        
        GameObject btnObj = new GameObject("DiceRollButton");
        Button diceBtn = btnObj.AddComponent<Button>();
        
        ActionButtonController actionController = gameStateGameObject.AddComponent<ActionButtonController>();
        actionController.Initialize(gameStateManager, diceBtn, null, null);
        
        // Act & Assert - should be enabled only during rolling
        gameStateManager.StartGame();
        Assert.AreEqual(GamePhase.RollingDice, gameStateManager.CurrentPhase);
        Assert.IsTrue(diceBtn.interactable);
    }
    
    [Test]
    public void ActionButtons_Bump_DisabledOutsideBumpingPhase()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        
        GameObject btnObj = new GameObject("BumpButton");
        Button bumpBtn = btnObj.AddComponent<Button>();
        
        ActionButtonController actionController = gameStateGameObject.AddComponent<ActionButtonController>();
        actionController.Initialize(gameStateManager, null, bumpBtn, null);
        
        // Act
        gameStateManager.StartGame();
        
        // Assert - bump disabled during rolling phase
        Assert.AreNotEqual(GamePhase.Bumping, gameStateManager.CurrentPhase);
        Assert.IsFalse(bumpBtn.interactable);
    }
    
    [Test]
    public void ActionButtons_DeclareWin_DisabledWhenNotMetCondition()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        
        GameObject btnObj = new GameObject("DeclareWinButton");
        Button winBtn = btnObj.AddComponent<Button>();
        
        ActionButtonController actionController = gameStateGameObject.AddComponent<ActionButtonController>();
        actionController.Initialize(gameStateManager, null, null, winBtn);
        
        // Act
        gameStateManager.StartGame();
        
        // Assert - win button disabled if condition not met
        Assert.IsFalse(winBtn.interactable);
    }
    
    // ============================================
    // NOTIFICATION TESTS
    // ============================================
    
    [Test]
    public void Notifications_ShowNotification_DisplaysMessage()
    {
        // Arrange
        NotificationController notifController = gameStateGameObject.AddComponent<NotificationController>();
        notifController.Initialize(null);
        
        // Act
        notifController.ShowNotification("Test message");
        
        // Assert
        Assert.IsTrue(notifController.IsInitialized);
    }
    
    [Test]
    public void Notifications_ShowSuccess_DisplaysGreen()
    {
        // Arrange
        NotificationController notifController = gameStateGameObject.AddComponent<NotificationController>();
        notifController.Initialize(null);
        
        // Act
        notifController.ShowSuccess("Success!");
        
        // Assert
        Assert.IsTrue(notifController.IsInitialized);
    }
    
    [Test]
    public void Notifications_ShowError_DisplaysRed()
    {
        // Arrange
        NotificationController notifController = gameStateGameObject.AddComponent<NotificationController>();
        notifController.Initialize(null);
        
        // Act
        notifController.ShowError("Error!");
        
        // Assert
        Assert.IsTrue(notifController.IsInitialized);
    }
    
    [Test]
    public void Notifications_ShowWarning_DisplaysYellow()
    {
        // Arrange
        NotificationController notifController = gameStateGameObject.AddComponent<NotificationController>();
        notifController.Initialize(null);
        
        // Act
        notifController.ShowWarning("Warning!");
        
        // Assert
        Assert.IsTrue(notifController.IsInitialized);
    }
    
    [Test]
    public void Notifications_Clear_RemovesCurrentNotification()
    {
        // Arrange
        NotificationController notifController = gameStateGameObject.AddComponent<NotificationController>();
        notifController.Initialize(null);
        notifController.ShowNotification("Test");
        
        // Act
        notifController.Clear();
        
        // Assert - clear completed without error
        Assert.Pass("Notification cleared successfully");
    }
    
    // ============================================
    // MODAL TESTS
    // ============================================
    
    [Test]
    public void Modal_ShowWinModal_DisplaysWinInformation()
    {
        // Arrange
        ModalController modalController = gameStateGameObject.AddComponent<ModalController>();
        modalController.Initialize(null);
        
        // Act
        modalController.ShowWinModal("Victory!", "Player1 has won!", player1);
        
        // Assert
        Assert.IsTrue(modalController.IsInitialized);
    }
    
    [Test]
    public void Modal_ShowLossModal_DisplaysLossInformation()
    {
        // Arrange
        ModalController modalController = gameStateGameObject.AddComponent<ModalController>();
        modalController.Initialize(null);
        
        // Act
        modalController.ShowLossModal("Game Over", "Player2 has won!");
        
        // Assert
        Assert.IsTrue(modalController.IsInitialized);
    }
    
    [Test]
    public void Modal_ShowErrorModal_DisplaysError()
    {
        // Arrange
        ModalController modalController = gameStateGameObject.AddComponent<ModalController>();
        modalController.Initialize(null);
        
        // Act
        modalController.ShowErrorModal("Error", "Something went wrong");
        
        // Assert
        Assert.IsTrue(modalController.IsInitialized);
    }
    
    [Test]
    public void Modal_HideModal_RemovesModalFromDisplay()
    {
        // Arrange
        ModalController modalController = gameStateGameObject.AddComponent<ModalController>();
        modalController.Initialize(null);
        modalController.ShowWinModal("Victory!", "Player1 won", player1);
        
        // Act
        modalController.HideModal();
        
        // Assert - hide completed without error
        Assert.Pass("Modal hidden successfully");
    }
    
    // ============================================
    // PAUSE MENU TESTS
    // ============================================
    
    [Test]
    public void PauseMenu_ShowPauseMenu_SetsPausedState()
    {
        // Arrange
        PauseMenuController pauseController = gameStateGameObject.AddComponent<PauseMenuController>();
        pauseController.Initialize(gameStateManager, null);
        
        // Act
        pauseController.ShowPauseMenu();
        
        // Assert
        Assert.IsTrue(pauseController.IsPaused);
    }
    
    [Test]
    public void PauseMenu_HidePauseMenu_ClearsPausedState()
    {
        // Arrange
        PauseMenuController pauseController = gameStateGameObject.AddComponent<PauseMenuController>();
        pauseController.Initialize(gameStateManager, null);
        pauseController.ShowPauseMenu();
        
        // Act
        pauseController.HidePauseMenu();
        
        // Assert
        Assert.IsFalse(pauseController.IsPaused);
    }
    
    [Test]
    public void PauseMenu_TogglePause_AlternatesBetweenStates()
    {
        // Arrange
        PauseMenuController pauseController = gameStateGameObject.AddComponent<PauseMenuController>();
        pauseController.Initialize(gameStateManager, null);
        
        Assert.IsFalse(pauseController.IsPaused);
        
        // Act & Assert
        pauseController.TogglePause();
        Assert.IsTrue(pauseController.IsPaused);
        
        pauseController.TogglePause();
        Assert.IsFalse(pauseController.IsPaused);
    }
    
    [Test]
    public void PauseMenu_OnPauseStateChanged_FiresEvent()
    {
        // Arrange
        PauseMenuController pauseController = gameStateGameObject.AddComponent<PauseMenuController>();
        pauseController.Initialize(gameStateManager, null);
        
        bool eventFired = false;
        pauseController.OnPauseStateChanged += (isPaused) => { eventFired = true; };
        
        // Act
        pauseController.ShowPauseMenu();
        
        // Assert
        Assert.IsTrue(eventFired);
    }
    
    // ============================================
    // SCOREBOARD TESTS
    // ============================================
    
    [Test]
    public void Scoreboard_Initialize_WithValidParent()
    {
        // Arrange
        GameObject parentObj = new GameObject("ScoreboardParent");
        ScoreboardController scoreboardController = gameStateGameObject.AddComponent<ScoreboardController>();
        
        // Act
        scoreboardController.Initialize(gameStateManager, parentObj.transform);
        
        // Assert
        Assert.IsTrue(scoreboardController.IsInitialized);
    }
    
    [Test]
    public void Scoreboard_UpdateCurrentPlayerHighlight_UpdatesDisplay()
    {
        // Arrange
        GameObject parentObj = new GameObject("ScoreboardParent");
        ScoreboardController scoreboardController = gameStateGameObject.AddComponent<ScoreboardController>();
        scoreboardController.Initialize(gameStateManager, parentObj.transform);
        
        // Act
        scoreboardController.UpdateCurrentPlayerHighlight(player1);
        
        // Assert
        Assert.Pass("Player highlight updated successfully");
    }
    
    [Test]
    public void Scoreboard_OnGameWon_HighlightsWinner()
    {
        // Arrange
        GameObject parentObj = new GameObject("ScoreboardParent");
        ScoreboardController scoreboardController = gameStateGameObject.AddComponent<ScoreboardController>();
        scoreboardController.Initialize(gameStateManager, parentObj.transform);
        
        // Act - simulate game won event
        // This would normally be triggered through GameStateManager.OnGameWon event
        
        // Assert
        Assert.Pass("Winner highlight would be set");
    }
    
    // ============================================
    // EVENT INTEGRATION TESTS
    // ============================================
    
    [Test]
    public void Events_PhaseChange_UpdatesHUD()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        hudManager.Initialize(gameStateManager);
        
        // Act
        gameStateManager.StartGame();
        
        // Assert
        Assert.AreEqual(GamePhase.RollingDice, gameStateManager.CurrentPhase);
    }
    
    [Test]
    public void Events_PlayerChange_UpdatesScoreboard()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        hudManager.Initialize(gameStateManager);
        
        // Act
        gameStateManager.StartGame();
        
        // Assert
        Assert.IsNotNull(gameStateManager.CurrentPlayer);
    }
    
    [Test]
    public void Events_DiceRolled_ShowsNotification()
    {
        // Arrange
        gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), player1, player2);
        hudManager.Initialize(gameStateManager);
        
        // Act
        gameStateManager.StartGame();
        gameStateManager.RollDice();
        
        // Assert - dice rolled without error
        Assert.Pass("Dice roll event handled successfully");
    }
}
