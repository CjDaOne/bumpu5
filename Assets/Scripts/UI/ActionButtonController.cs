using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ActionButtonController - Manages dice roll, bump, and declare win buttons.
/// 
/// Responsibilities:
/// - Enable/disable buttons based on game phase
/// - Route button clicks to GameStateManager
/// - Provide visual feedback (enabled/disabled states)
/// - Only enable buttons when valid for current phase and player
/// </summary>
public class ActionButtonController : MonoBehaviour
{
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private GameStateManager gameStateManager;
    private Button diceRollButton;
    private Button bumpButton;
    private Button declareWinButton;
    private bool isInitialized = false;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize button controller</summary>
    public void Initialize(GameStateManager stateManager, Button rollBtn, Button bumpBtn, Button winBtn)
    {
        gameStateManager = stateManager;
        diceRollButton = rollBtn;
        bumpButton = bumpBtn;
        declareWinButton = winBtn;
        
        if (gameStateManager == null)
        {
            Debug.LogError("ActionButtonController.Initialize: gameStateManager is null");
            return;
        }
        
        // Connect button click handlers
        if (diceRollButton != null)
            diceRollButton.onClick.AddListener(OnDiceRollClicked);
        
        if (bumpButton != null)
            bumpButton.onClick.AddListener(OnBumpClicked);
        
        if (declareWinButton != null)
            declareWinButton.onClick.AddListener(OnDeclareWinClicked);
        
        // Subscribe to phase changes
        gameStateManager.OnPhaseChanged += OnPhaseChanged;
        
        isInitialized = true;
        Debug.Log("ActionButtonController initialized");
    }
    
    // ============================================
    // BUTTON CLICK HANDLERS
    // ============================================
    
    private void OnDiceRollClicked()
    {
        if (gameStateManager == null || gameStateManager.CurrentPhase != GamePhase.RollingDice)
        {
            Debug.LogWarning("Dice roll clicked in invalid phase");
            return;
        }
        
        Debug.Log("[ActionButtonController] Dice roll clicked");
        gameStateManager.RollDice();
    }
    
    private void OnBumpClicked()
    {
        if (gameStateManager == null || gameStateManager.CurrentPhase != GamePhase.Bumping)
        {
            Debug.LogWarning("Bump clicked in invalid phase");
            return;
        }
        
        Debug.Log("[ActionButtonController] Bump clicked");
        // TODO: Route to bump logic in GameStateManager
    }
    
    private void OnDeclareWinClicked()
    {
        if (gameStateManager == null || gameStateManager.CurrentPlayer == null)
        {
            Debug.LogWarning("Declare win clicked with invalid state");
            return;
        }
        
        Debug.Log("[ActionButtonController] Declare win clicked");
        
        Player currentPlayer = gameStateManager.CurrentPlayer;
        if (gameStateManager.CurrentGameMode != null && gameStateManager.CurrentGameMode.CheckWinCondition(currentPlayer))
        {
            gameStateManager.EndGame(currentPlayer);
        }
    }
    
    // ============================================
    // STATE MANAGEMENT
    // ============================================
    
    /// <summary>Handle phase change - update button states</summary>
    private void OnPhaseChanged(GamePhase newPhase)
    {
        UpdateButtonStates(newPhase, gameStateManager.CurrentPlayer);
    }
    
    /// <summary>Update button enabled/disabled based on phase and player</summary>
    public void UpdateButtonStates(GamePhase phase, Player currentPlayer)
    {
        bool isCurrentPlayer = currentPlayer != null; // Simplified for single-agent version
        
        // Dice Roll Button: Enabled during RollingDice phase
        if (diceRollButton != null)
        {
            bool diceEnabled = (phase == GamePhase.RollingDice) && isCurrentPlayer;
            SetButtonState(diceRollButton, diceEnabled);
        }
        
        // Bump Button: Enabled during Bumping phase
        if (bumpButton != null)
        {
            bool bumpEnabled = (phase == GamePhase.Bumping) && isCurrentPlayer;
            SetButtonState(bumpButton, bumpEnabled);
        }
        
        // Declare Win Button: Enabled during Placing phase if win condition could be met
        if (declareWinButton != null)
        {
            bool winEnabled = (phase == GamePhase.Placing) && isCurrentPlayer && CanDeclareWin(currentPlayer);
            SetButtonState(declareWinButton, winEnabled);
        }
    }
    
    /// <summary>Check if current player can declare win</summary>
    private bool CanDeclareWin(Player player)
    {
        if (gameStateManager == null || gameStateManager.CurrentGameMode == null)
            return false;
        
        return gameStateManager.CurrentGameMode.CheckWinCondition(player);
    }
    
    /// <summary>Set button enabled/disabled with visual feedback</summary>
    private void SetButtonState(Button button, bool enabled)
    {
        if (button == null)
            return;
        
        button.interactable = enabled;
        
        // Update visual feedback
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            Color color = enabled ? Color.white : new Color(0.8f, 0.8f, 0.8f, 0.5f);
            buttonImage.color = color;
        }
        
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            Color textColor = enabled ? Color.white : new Color(0.5f, 0.5f, 0.5f);
            buttonText.color = textColor;
        }
    }
    
    // ============================================
    // PUBLIC INTERFACE
    // ============================================
    
    /// <summary>Temporarily disable all action buttons</summary>
    public void DisableAllButtons()
    {
        if (diceRollButton != null)
            diceRollButton.interactable = false;
        if (bumpButton != null)
            bumpButton.interactable = false;
        if (declareWinButton != null)
            declareWinButton.interactable = false;
    }
    
    /// <summary>Re-enable buttons based on current game state</summary>
    public void RefreshButtonStates()
    {
        if (gameStateManager != null)
            UpdateButtonStates(gameStateManager.CurrentPhase, gameStateManager.CurrentPlayer);
    }
}
