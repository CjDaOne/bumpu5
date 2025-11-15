using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// DeclareWinButton - Button to declare victory when win condition is met.
/// 
/// Responsibilities:
/// - Check if current player has won
/// - Display button only when win is possible
/// - Trigger game end when clicked
/// - Handle button enable/disable based on win status
/// </summary>
public class DeclareWinButton : MonoBehaviour
{
    // ============================================
    // REFERENCES
    // ============================================
    
    private Button button;
    private TextMeshProUGUI buttonText;
    private Image buttonImage;
    private GameStateManager gameStateManager;
    private HUDManager hudManager;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    [SerializeField]
    private Color enabledColor = Color.green;
    
    [SerializeField]
    private Color disabledColor = new Color(0.5f, 0.5f, 0.5f);
    
    [SerializeField]
    private float winCheckInterval = 1f;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private bool isInitialized = false;
    private bool isInteractable = false;
    private float lastWinCheckTime = 0f;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize the declare win button</summary>
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
            gameStateManager.OnChipPlaced += OnChipPlaced;
            gameStateManager.OnChipBumped += OnChipBumped;
        }
        
        isInitialized = true;
        SetInteractable(false);
        
        Debug.Log("[DeclareWinButton] Initialized");
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
            gameStateManager.OnChipPlaced -= OnChipPlaced;
            gameStateManager.OnChipBumped -= OnChipBumped;
        }
        
        isInitialized = false;
    }
    
    /// <summary>Update called every frame</summary>
    private void Update()
    {
        if (!isInitialized || gameStateManager == null)
            return;
        
        // Check for win condition periodically
        if (Time.time - lastWinCheckTime > winCheckInterval)
        {
            lastWinCheckTime = Time.time;
            CheckWinCondition();
        }
    }
    
    // ============================================
    // BUTTON INTERACTION
    // ============================================
    
    /// <summary>Called when button is clicked</summary>
    private void OnButtonClicked()
    {
        if (!isInteractable || gameStateManager == null)
        {
            Debug.Log("[DeclareWinButton] Button clicked but not interactable");
            return;
        }
        
        Player currentPlayer = gameStateManager.CurrentPlayer;
        if (currentPlayer == null)
        {
            Debug.Log("[DeclareWinButton] No current player");
            return;
        }
        
        // Check win condition
        if (gameStateManager.CurrentGameMode.CheckWinCondition(currentPlayer))
        {
            Debug.Log($"[DeclareWinButton] {currentPlayer.PlayerName} wins!");
            gameStateManager.EndGame(currentPlayer);
        }
        else
        {
            Debug.Log("[DeclareWinButton] Win condition not met");
            if (hudManager != null)
            {
                hudManager.ShowNotification("Win condition not met", PopupType.Warning);
            }
        }
    }
    
    // ============================================
    // WIN CONDITION CHECKING
    // ============================================
    
    /// <summary>Check if current player has won</summary>
    private void CheckWinCondition()
    {
        if (!isInitialized || gameStateManager == null)
            return;
        
        Player currentPlayer = gameStateManager.CurrentPlayer;
        if (currentPlayer == null)
            return;
        
        // Check if win condition is met
        bool hasWon = gameStateManager.CurrentGameMode.CheckWinCondition(currentPlayer);
        
        SetInteractable(hasWon);
        
        if (buttonText != null)
        {
            buttonText.text = hasWon ? "🎯 WIN!" : "Declare Win";
        }
    }
    
    // ============================================
    // PHASE HANDLING
    // ============================================
    
    /// <summary>Called when game phase changes</summary>
    private void OnPhaseChanged(GamePhase newPhase)
    {
        // Button available during most phases
        bool canDeclare = (newPhase != GamePhase.GameStart && newPhase != GamePhase.GameEnd);
        
        if (gameStateManager != null && gameStateManager.CurrentGameMode != null)
        {
            // Check if win is possible in this phase
            Player currentPlayer = gameStateManager.CurrentPlayer;
            if (currentPlayer != null)
            {
                canDeclare = canDeclare && gameStateManager.CurrentGameMode.CheckWinCondition(currentPlayer);
            }
        }
        
        SetInteractable(canDeclare);
    }
    
    /// <summary>Called when chip is placed</summary>
    private void OnChipPlaced(int cellIndex, Player player)
    {
        // Check win condition after placement
        CheckWinCondition();
    }
    
    /// <summary>Called when chip is bumped</summary>
    private void OnChipBumped(int cellIndex)
    {
        // Check win condition after bump
        CheckWinCondition();
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
        
        Debug.Log($"[DeclareWinButton] Set interactable: {interactable}");
    }
}
