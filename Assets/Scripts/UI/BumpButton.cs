using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BumpButton - Context-sensitive button for bumping opponent chips.
/// 
/// Responsibilities:
/// - Display only during Placing/Bumping phases
/// - Show visual feedback when hovering over bumpable chips
/// - Trigger bump action when clicked with valid target
/// - Handle button enable/disable based on game state
/// </summary>
public class BumpButton : MonoBehaviour
{
    // ============================================
    // REFERENCES
    // ============================================
    
    private Button button;
    private Text buttonText;
    private Image buttonImage;
    private GameStateManager gameStateManager;
    private HUDManager hudManager;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    [SerializeField]
    private Color enabledColor = Color.white;
    
    [SerializeField]
    private Color disabledColor = new Color(0.5f, 0.5f, 0.5f);
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private bool isInitialized = false;
    private bool isInteractable = false;
    private int selectedCellIndex = -1;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize the bump button</summary>
    public void Initialize(GameStateManager gsm, HUDManager hm)
    {
        gameStateManager = gsm;
        hudManager = hm;
        
        // Get button component
        button = GetComponent<Button>();
        if (button == null)
        {
            button = gameObject.AddComponent<Button>();
        }
        
        // Get text and image components
        buttonText = GetComponentInChildren<Text>();
        buttonImage = GetComponent<Image>();
        
        // Subscribe to button click
        button.onClick.AddListener(OnButtonClicked);
        
        // Subscribe to game state events
        if (gameStateManager != null)
        {
            gameStateManager.OnPhaseChanged += OnPhaseChanged;
            gameStateManager.OnPlayerChanged += OnPlayerChanged;
        }
        
        isInitialized = true;
        SetInteractable(false);
        
        Debug.Log("[BumpButton] Initialized");
    }
    
    /// <summary>Shutdown the button</summary>
    public void Shutdown()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }
        
        if (gameStateManager != null)
        {
            gameStateManager.OnPhaseChanged -= OnPhaseChanged;
            gameStateManager.OnPlayerChanged -= OnPlayerChanged;
        }
        
        isInitialized = false;
    }
    
    // ============================================
    // BUTTON INTERACTION
    // ============================================
    
    /// <summary>Called when button is clicked</summary>
    private void OnButtonClicked()
    {
        if (!isInteractable || gameStateManager == null)
        {
            Debug.Log("[BumpButton] Button clicked but not interactable");
            return;
        }
        
        // Check if current game mode allows bumping
        if (!gameStateManager.CurrentGameMode.AllowBumping)
        {
            Debug.Log("[BumpButton] Bumping not allowed in this game mode");
            return;
        }
        
        Debug.Log("[BumpButton] Bump action triggered");
        
        // Show popup to select bump target
        ShowBumpSelectionPopup();
    }
    
    // ============================================
    // PHASE HANDLING
    // ============================================
    
    /// <summary>Called when game phase changes</summary>
    private void OnPhaseChanged(GamePhase newPhase)
    {
        // Enable bump button during Placing or Bumping phases
        bool canBump = (newPhase == GamePhase.Placing || newPhase == GamePhase.Bumping);
        
        if (gameStateManager != null && gameStateManager.CurrentGameMode != null)
        {
            canBump = canBump && gameStateManager.CurrentGameMode.AllowBumping;
        }
        
        SetInteractable(canBump);
        
        // Update button text based on phase
        if (buttonText != null)
        {
            switch (newPhase)
            {
                case GamePhase.Placing:
                case GamePhase.Bumping:
                    buttonText.text = "Bump";
                    break;
                default:
                    buttonText.text = "Bump (N/A)";
                    break;
            }
        }
    }
    
    /// <summary>Called when current player changes</summary>
    private void OnPlayerChanged(Player newPlayer)
    {
        // Reset selected cell when player changes
        selectedCellIndex = -1;
    }
    
    // ============================================
    // BUMP SELECTION
    // ============================================
    
    /// <summary>Show popup to select bump target</summary>
    private void ShowBumpSelectionPopup()
    {
        if (hudManager == null || hudManager.PopupManager == null)
        {
            Debug.LogWarning("[BumpButton] Cannot show popup - no PopupManager");
            return;
        }
        
        // Show popup with instructions
        hudManager.ShowNotification("Click on opponent chip to bump", PopupType.Info);
    }
    
    /// <summary>Process bump target selection</summary>
    public void SelectBumpTarget(int cellIndex)
    {
        if (!isInteractable || gameStateManager == null)
            return;
        
        selectedCellIndex = cellIndex;
        
        // Get cell occupant
        Player targetPlayer = gameStateManager.GetPlayerAtCell(cellIndex);
        if (targetPlayer == null)
        {
            Debug.Log("[BumpButton] Cell is empty");
            return;
        }
        
        Player currentPlayer = gameStateManager.CurrentPlayer;
        if (currentPlayer == null)
        {
            Debug.Log("[BumpButton] No current player");
            return;
        }
        
        // Try to bump
        if (gameStateManager.CurrentGameMode.CanBump(currentPlayer, targetPlayer, cellIndex))
        {
            gameStateManager.BumpOpponentChip(cellIndex);
            Debug.Log($"[BumpButton] Bumped {targetPlayer.PlayerName} from cell {cellIndex}");
        }
        else
        {
            Debug.Log("[BumpButton] Cannot bump this cell");
            if (hudManager != null)
            {
                hudManager.ShowNotification("Cannot bump this chip", PopupType.Warning);
            }
        }
    }
    
    // ============================================
    // PUBLIC CONTROL
    // ============================================
    
    /// <summary>Enable or disable the button</summary>
    public void SetInteractable(bool interactable)
    {
        isInteractable = interactable;
        
        if (button != null)
        {
            button.interactable = interactable;
        }
        
        if (buttonImage != null)
        {
            buttonImage.color = interactable ? enabledColor : disabledColor;
        }
        
        Debug.Log($"[BumpButton] Set interactable: {interactable}");
    }
}
