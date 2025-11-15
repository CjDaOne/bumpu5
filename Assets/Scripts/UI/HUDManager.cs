using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// HUDManager - Master controller for all HUD elements during gameplay.
/// 
/// Responsibilities:
/// - Initialize all HUD components on game start
/// - Listen to GameStateManager events
/// - Update HUD state based on game state
/// - Coordinate button enable/disable based on game phase
/// - Manage notifications, modals, and pause menu
/// 
/// Architecture:
/// - Subscribes to all GameStateManager events
/// - Delegates to specialized controllers (ActionButtonController, ScoreboardController, etc.)
/// - Maintains HUD state in sync with game state
/// - Handles lifecycle (game start, pause, resume, game end)
/// </summary>
public class HUDManager : MonoBehaviour
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField]
    private GameStateManager gameStateManager;
    
    [SerializeField]
    private Canvas hudCanvas;
    
    [SerializeField]
    private TextMeshProUGUI phaseIndicatorText;
    
    [SerializeField]
    private Button diceRollButton;
    
    [SerializeField]
    private Button bumpButton;
    
    [SerializeField]
    private Button declareWinButton;
    
    [SerializeField]
    private Button pauseButton;
    
    [SerializeField]
    private Transform scoresboardParent;
    
    [SerializeField]
    private GameObject notificationPanelPrefab;
    
    [SerializeField]
    private GameObject modalOverlayPrefab;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private ActionButtonController actionButtonController;
    private ScoreboardController scoreboardController;
    private NotificationController notificationController;
    private ModalController modalController;
    private PauseMenuController pauseMenuController;
    
    private bool isInitialized = false;
    private bool isGamePaused = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    public event System.Action<bool> OnPauseStateChanged;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInitialized => isInitialized;
    public bool IsGamePaused => isGamePaused;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize HUD system with game state reference</summary>
    public void Initialize(GameStateManager stateManager)
    {
        if (stateManager == null)
        {
            Debug.LogError("HUDManager.Initialize: gameStateManager is null");
            return;
        }
        
        gameStateManager = stateManager;
        
        // Initialize child controllers
        InitializeActionButtonController();
        InitializeScoreboardController();
        InitializeNotificationController();
        InitializeModalController();
        InitializePauseMenuController();
        
        // Subscribe to game state events
        SubscribeToGameStateEvents();
        
        isInitialized = true;
        Debug.Log("HUDManager initialized successfully");
    }
    
    /// <summary>Cleanup on shutdown</summary>
    public void Shutdown()
    {
        if (gameStateManager != null)
        {
            gameStateManager.OnPhaseChanged -= OnGameStatePhaseChanged;
            gameStateManager.OnPlayerChanged -= OnGameStatePlayerChanged;
            gameStateManager.OnDiceRolled -= OnGameStateDiceRolled;
            gameStateManager.OnChipPlaced -= OnGameStateChipPlaced;
            gameStateManager.OnGameWon -= OnGameStateGameWon;
        }
        
        isInitialized = false;
    }
    
    // ============================================
    // INITIALIZATION
    // ============================================
    
    private void InitializeActionButtonController()
    {
        if (actionButtonController == null)
            actionButtonController = gameObject.AddComponent<ActionButtonController>();
        
        actionButtonController.Initialize(gameStateManager, diceRollButton, bumpButton, declareWinButton);
    }
    
    private void InitializeScoreboardController()
    {
        if (scoreboardController == null)
            scoreboardController = gameObject.AddComponent<ScoreboardController>();
        
        scoreboardController.Initialize(gameStateManager, scoresboardParent);
    }
    
    private void InitializeNotificationController()
    {
        if (notificationController == null)
            notificationController = gameObject.AddComponent<NotificationController>();
        
        if (notificationPanelPrefab == null)
            notificationPanelPrefab = CreateDefaultNotificationPanel();
        
        notificationController.Initialize(notificationPanelPrefab);
    }
    
    private void InitializeModalController()
    {
        if (modalController == null)
            modalController = gameObject.AddComponent<ModalController>();
        
        if (modalOverlayPrefab == null)
            modalOverlayPrefab = CreateDefaultModalOverlay();
        
        modalController.Initialize(modalOverlayPrefab);
    }
    
    private void InitializePauseMenuController()
    {
        if (pauseMenuController == null)
            pauseMenuController = gameObject.AddComponent<PauseMenuController>();
        
        pauseMenuController.Initialize(gameStateManager, pauseButton);
        pauseMenuController.OnPauseStateChanged += (isPaused) => { isGamePaused = isPaused; OnPauseStateChanged?.Invoke(isPaused); };
    }
    
    // ============================================
    // EVENT SUBSCRIPTION
    // ============================================
    
    private void SubscribeToGameStateEvents()
    {
        if (gameStateManager == null)
            return;
        
        gameStateManager.OnPhaseChanged += OnGameStatePhaseChanged;
        gameStateManager.OnPlayerChanged += OnGameStatePlayerChanged;
        gameStateManager.OnDiceRolled += OnGameStateDiceRolled;
        gameStateManager.OnChipPlaced += OnGameStateChipPlaced;
        gameStateManager.OnGameWon += OnGameStateGameWon;
    }
    
    // ============================================
    // GAME STATE HANDLERS
    // ============================================
    
    private void OnGameStatePhaseChanged(GamePhase newPhase)
    {
        UpdatePhaseIndicator(newPhase);
        
        if (actionButtonController != null)
            actionButtonController.UpdateButtonStates(newPhase, gameStateManager.CurrentPlayer);
    }
    
    private void OnGameStatePlayerChanged(Player newPlayer)
    {
        if (scoreboardController != null)
            scoreboardController.UpdateCurrentPlayerHighlight(newPlayer);
        
        UpdatePhaseIndicator(gameStateManager.CurrentPhase);
    }
    
    private void OnGameStateDiceRolled(int[] dice)
    {
        if (dice == null || dice.Length < 2)
            return;
        
        int total = dice[0] + dice[1];
        string message = $"Rolled: {dice[0]} + {dice[1]} = {total}";
        
        if (notificationController != null)
            notificationController.ShowNotification(message, 3f);
    }
    
    private void OnGameStateChipPlaced(int cellIndex, Player player)
    {
        string message = $"{player.PlayerName} placed on cell {cellIndex}";
        
        if (notificationController != null)
            notificationController.ShowNotification(message, 2f);
    }
    
    private void OnGameStateGameWon(Player winner)
    {
        string title = "Game Won!";
        string message = $"{winner.PlayerName} has won the game!";
        
        if (modalController != null)
            modalController.ShowWinModal(title, message, winner);
    }
    
    // ============================================
    // HUD UPDATES
    // ============================================
    
    private void UpdatePhaseIndicator(GamePhase phase)
    {
        if (phaseIndicatorText == null)
            return;
        
        Player current = gameStateManager.CurrentPlayer;
        string playerName = current != null ? current.PlayerName : "Unknown";
        
        string phaseText = phase switch
        {
            GamePhase.GameStart => "Game Starting...",
            GamePhase.RollingDice => $"{playerName} - Rolling Phase",
            GamePhase.Placing => $"{playerName} - Placing Phase",
            GamePhase.Bumping => $"{playerName} - Bumping Phase",
            GamePhase.EndTurn => $"{playerName} - End Turn",
            GamePhase.Waiting => $"Waiting for {playerName}",
            GamePhase.GameEnd => "Game Over",
            _ => "Unknown Phase"
        };
        
        phaseIndicatorText.text = phaseText;
    }
    
    // ============================================
    // PREFAB CREATION (FALLBACK)
    // ============================================
    
    private GameObject CreateDefaultNotificationPanel()
    {
        GameObject panel = new GameObject("NotificationPanel");
        panel.AddComponent<CanvasGroup>();
        panel.AddComponent<Text>().text = "Notification";
        return panel;
    }
    
    private GameObject CreateDefaultModalOverlay()
    {
        GameObject overlay = new GameObject("ModalOverlay");
        overlay.AddComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        return overlay;
    }
    
    // ============================================
    // PUBLIC INTERFACE
    // ============================================
    
    /// <summary>Show a temporary notification message</summary>
    public void ShowNotification(string message, float duration = 3f)
    {
        if (notificationController != null)
            notificationController.ShowNotification(message, duration);
    }
    
    /// <summary>Show an error notification</summary>
    public void ShowError(string message, float duration = 3f)
    {
        if (notificationController != null)
            notificationController.ShowError(message, duration);
    }
    
    /// <summary>Show a success notification</summary>
    public void ShowSuccess(string message, float duration = 3f)
    {
        if (notificationController != null)
            notificationController.ShowSuccess(message, duration);
    }
    
    /// <summary>Pause the game</summary>
    public void PauseGame()
    {
        if (pauseMenuController != null)
            pauseMenuController.ShowPauseMenu();
    }
    
    /// <summary>Resume the game</summary>
    public void ResumeGame()
    {
        if (pauseMenuController != null)
            pauseMenuController.HidePauseMenu();
    }
}
