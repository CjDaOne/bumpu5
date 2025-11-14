using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HUDManager - Master orchestrator for all HUD elements (Heads-Up Display).
/// 
/// Responsibilities:
/// - Create and manage all HUD UI elements
/// - Update HUD state based on game events
/// - Coordinate button interactions
/// - Display game information (current player, phase, scores)
/// - Handle HUD visibility/layout based on game state
/// 
/// Architecture:
/// - Subscribes to GameStateManager events
/// - Creates and manages DiceRollButton, BumpButton, DeclareWinButton
/// - Updates ScoreboardDisplay with current scores
/// - Manages PopupManager for notifications
/// - Updates PhaseIndicator with current game phase
/// </summary>
public class HUDManager : MonoBehaviour
{
    // ============================================
    // REFERENCES
    // ============================================
    
    [SerializeField]
    private Canvas hudCanvas;
    
    [SerializeField]
    private GameStateManager gameStateManager;
    
    // ============================================
    // HUD COMPONENTS
    // ============================================
    
    [SerializeField]
    private DiceRollButton diceRollButton;
    
    [SerializeField]
    private BumpButton bumpButton;
    
    [SerializeField]
    private DeclareWinButton declareWinButton;
    
    [SerializeField]
    private ScoreboardDisplay scoreboardDisplay;
    
    [SerializeField]
    private PopupManager popupManager;
    
    [SerializeField]
    private PhaseIndicator phaseIndicator;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private bool isInitialized = false;
    private bool isHUDVisible = true;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize the HUD system</summary>
    public void Initialize(GameStateManager gsm)
    {
        if (gsm == null)
        {
            Debug.LogError("[HUDManager] GameStateManager is null");
            return;
        }
        
        gameStateManager = gsm;
        
        // Ensure canvas exists
        if (hudCanvas == null)
        {
            hudCanvas = GetComponent<Canvas>();
        }
        
        // Initialize components
        InitializeComponents();
        
        // Subscribe to game state events
        SubscribeToGameStateEvents();
        
        isInitialized = true;
        Debug.Log("[HUDManager] HUD initialized successfully");
    }
    
    /// <summary>Shutdown HUD system</summary>
    public void Shutdown()
    {
        UnsubscribeFromGameStateEvents();
        
        // Cleanup components
        if (diceRollButton != null)
            diceRollButton.Shutdown();
        
        if (bumpButton != null)
            bumpButton.Shutdown();
        
        if (declareWinButton != null)
            declareWinButton.Shutdown();
        
        if (scoreboardDisplay != null)
            scoreboardDisplay.Shutdown();
        
        if (popupManager != null)
            popupManager.Shutdown();
        
        isInitialized = false;
    }
    
    // ============================================
    // INITIALIZATION
    // ============================================
    
    /// <summary>Initialize all HUD components</summary>
    private void InitializeComponents()
    {
        // Dice Roll Button
        if (diceRollButton == null)
        {
            diceRollButton = gameObject.AddComponent<DiceRollButton>();
        }
        diceRollButton.Initialize(gameStateManager, this);
        
        // Bump Button
        if (bumpButton == null)
        {
            bumpButton = gameObject.AddComponent<BumpButton>();
        }
        bumpButton.Initialize(gameStateManager, this);
        
        // Declare Win Button
        if (declareWinButton == null)
        {
            declareWinButton = gameObject.AddComponent<DeclareWinButton>();
        }
        declareWinButton.Initialize(gameStateManager, this);
        
        // Scoreboard Display
        if (scoreboardDisplay == null)
        {
            scoreboardDisplay = gameObject.AddComponent<ScoreboardDisplay>();
        }
        scoreboardDisplay.Initialize(gameStateManager);
        
        // Popup Manager
        if (popupManager == null)
        {
            popupManager = gameObject.AddComponent<PopupManager>();
        }
        popupManager.Initialize();
        
        // Phase Indicator
        if (phaseIndicator == null)
        {
            phaseIndicator = gameObject.AddComponent<PhaseIndicator>();
        }
        phaseIndicator.Initialize(gameStateManager);
    }
    
    /// <summary>Subscribe to GameStateManager events</summary>
    private void SubscribeToGameStateEvents()
    {
        if (gameStateManager == null)
            return;
        
        gameStateManager.OnPhaseChanged += HandlePhaseChanged;
        gameStateManager.OnPlayerChanged += HandlePlayerChanged;
        gameStateManager.OnGameStarted += HandleGameStarted;
        gameStateManager.OnGameWon += HandleGameWon;
        gameStateManager.OnDiceRolled += HandleDiceRolled;
        gameStateManager.OnChipPlaced += HandleChipPlaced;
        gameStateManager.OnChipBumped += HandleChipBumped;
    }
    
    /// <summary>Unsubscribe from GameStateManager events</summary>
    private void UnsubscribeFromGameStateEvents()
    {
        if (gameStateManager == null)
            return;
        
        gameStateManager.OnPhaseChanged -= HandlePhaseChanged;
        gameStateManager.OnPlayerChanged -= HandlePlayerChanged;
        gameStateManager.OnGameStarted -= HandleGameStarted;
        gameStateManager.OnGameWon -= HandleGameWon;
        gameStateManager.OnDiceRolled -= HandleDiceRolled;
        gameStateManager.OnChipPlaced -= HandleChipPlaced;
        gameStateManager.OnChipBumped -= HandleChipBumped;
    }
    
    // ============================================
    // EVENT HANDLERS
    // ============================================
    
    /// <summary>Handle phase change event</summary>
    private void HandlePhaseChanged(GamePhase newPhase)
    {
        Debug.Log($"[HUDManager] Phase changed to {newPhase}");
        
        // Update phase indicator
        if (phaseIndicator != null)
        {
            phaseIndicator.UpdatePhase(newPhase);
        }
        
        // Update button visibility/enable state
        UpdateButtonStates(newPhase);
        
        // Show phase notification
        if (popupManager != null)
        {
            popupManager.ShowNotification($"Phase: {newPhase}");
        }
    }
    
    /// <summary>Handle player change event</summary>
    private void HandlePlayerChanged(Player newPlayer)
    {
        Debug.Log($"[HUDManager] Current player changed to {newPlayer.PlayerName}");
        
        // Update scoreboard to highlight current player
        if (scoreboardDisplay != null)
        {
            scoreboardDisplay.UpdateCurrentPlayer(newPlayer);
        }
        
        // Show notification
        if (popupManager != null)
        {
            popupManager.ShowNotification($"{newPlayer.PlayerName}'s Turn");
        }
    }
    
    /// <summary>Handle game started event</summary>
    private void HandleGameStarted()
    {
        Debug.Log("[HUDManager] Game started");
        
        // Show initial state
        if (scoreboardDisplay != null)
        {
            scoreboardDisplay.UpdateScores(gameStateManager.Players);
        }
        
        // Show game mode info
        if (popupManager != null && gameStateManager.CurrentGameMode != null)
        {
            popupManager.ShowNotification($"Game: {gameStateManager.CurrentGameMode.ModeName}");
        }
    }
    
    /// <summary>Handle game won event</summary>
    private void HandleGameWon(Player winner)
    {
        Debug.Log($"[HUDManager] Game won by {winner.PlayerName}");
        
        // Show win notification with celebration
        if (popupManager != null)
        {
            popupManager.ShowNotification($"🎉 {winner.PlayerName} wins! 🎉", PopupType.Celebration);
        }
        
        // Disable all action buttons
        if (diceRollButton != null)
            diceRollButton.SetInteractable(false);
        if (bumpButton != null)
            bumpButton.SetInteractable(false);
        if (declareWinButton != null)
            declareWinButton.SetInteractable(false);
    }
    
    /// <summary>Handle dice rolled event</summary>
    private void HandleDiceRolled(int[] diceResult)
    {
        if (diceResult == null || diceResult.Length < 2)
            return;
        
        int total = diceResult[0] + diceResult[1];
        Debug.Log($"[HUDManager] Dice rolled: {diceResult[0]} + {diceResult[1]} = {total}");
        
        // Show dice roll notification
        if (popupManager != null)
        {
            popupManager.ShowNotification($"🎲 {diceResult[0]} + {diceResult[1]} = {total}");
        }
    }
    
    /// <summary>Handle chip placed event</summary>
    private void HandleChipPlaced(int cellIndex, Player player)
    {
        Debug.Log($"[HUDManager] Chip placed on cell {cellIndex} by {player.PlayerName}");
        
        // Update scoreboard
        if (scoreboardDisplay != null)
        {
            scoreboardDisplay.UpdateScores(gameStateManager.Players);
        }
    }
    
    /// <summary>Handle chip bumped event</summary>
    private void HandleChipBumped(int cellIndex)
    {
        Debug.Log($"[HUDManager] Chip bumped from cell {cellIndex}");
        
        // Show bump notification
        if (popupManager != null)
        {
            popupManager.ShowNotification("💥 Bump!", PopupType.Warning);
        }
        
        // Update scoreboard
        if (scoreboardDisplay != null)
        {
            scoreboardDisplay.UpdateScores(gameStateManager.Players);
        }
    }
    
    // ============================================
    // PUBLIC CONTROL METHODS
    // ============================================
    
    /// <summary>Show or hide the entire HUD</summary>
    public void SetHUDVisible(bool visible)
    {
        if (hudCanvas != null)
        {
            hudCanvas.enabled = visible;
            isHUDVisible = visible;
            Debug.Log($"[HUDManager] HUD {(visible ? "shown" : "hidden")}");
        }
    }
    
    /// <summary>Show a notification popup</summary>
    public void ShowNotification(string message, PopupType type = PopupType.Info)
    {
        if (popupManager != null)
        {
            popupManager.ShowNotification(message, type);
        }
    }
    
    /// <summary>Update button states based on current phase</summary>
    private void UpdateButtonStates(GamePhase currentPhase)
    {
        switch (currentPhase)
        {
            case GamePhase.RollingDice:
                if (diceRollButton != null)
                    diceRollButton.SetInteractable(true);
                if (bumpButton != null)
                    bumpButton.SetInteractable(false);
                if (declareWinButton != null)
                    declareWinButton.SetInteractable(false);
                break;
            
            case GamePhase.Placing:
                if (diceRollButton != null)
                    diceRollButton.SetInteractable(false);
                if (bumpButton != null)
                    bumpButton.SetInteractable(true);
                if (declareWinButton != null)
                    declareWinButton.SetInteractable(true);
                break;
            
            case GamePhase.Bumping:
                if (diceRollButton != null)
                    diceRollButton.SetInteractable(false);
                if (bumpButton != null)
                    bumpButton.SetInteractable(true);
                if (declareWinButton != null)
                    declareWinButton.SetInteractable(true);
                break;
            
            case GamePhase.EndTurn:
                if (diceRollButton != null)
                    diceRollButton.SetInteractable(false);
                if (bumpButton != null)
                    bumpButton.SetInteractable(false);
                if (declareWinButton != null)
                    declareWinButton.SetInteractable(false);
                break;
            
            case GamePhase.GameStart:
            case GamePhase.GameEnd:
                if (diceRollButton != null)
                    diceRollButton.SetInteractable(false);
                if (bumpButton != null)
                    bumpButton.SetInteractable(false);
                if (declareWinButton != null)
                    declareWinButton.SetInteractable(false);
                break;
        }
    }
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInitialized => isInitialized;
    public bool IsHUDVisible => isHUDVisible;
    public DiceRollButton DiceRollButton => diceRollButton;
    public BumpButton BumpButton => bumpButton;
    public DeclareWinButton DeclareWinButton => declareWinButton;
    public ScoreboardDisplay ScoreboardDisplay => scoreboardDisplay;
    public PopupManager PopupManager => popupManager;
    public PhaseIndicator PhaseIndicator => phaseIndicator;
}
