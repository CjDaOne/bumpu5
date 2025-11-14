using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// DiceRollButton - Button to roll the dice and advance the game.
/// 
/// Responsibilities:
/// - Display rollable state during RollingDice phase
/// - Trigger dice roll when clicked
/// - Show dice animation result
/// - Handle button enable/disable based on game state
/// - Provide visual feedback (animation)
/// </summary>
public class DiceRollButton : MonoBehaviour
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
    
    [SerializeField]
    private float animationDuration = 0.5f;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private bool isInitialized = false;
    private bool isInteractable = false;
    private Coroutine rollAnimationCoroutine;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize the dice roll button</summary>
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
            gameStateManager.OnDiceRolled += OnDiceRolled;
        }
        
        isInitialized = true;
        SetInteractable(false);
        
        Debug.Log("[DiceRollButton] Initialized");
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
            gameStateManager.OnDiceRolled -= OnDiceRolled;
        }
        
        if (rollAnimationCoroutine != null)
        {
            StopCoroutine(rollAnimationCoroutine);
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
            Debug.Log("[DiceRollButton] Button clicked but not interactable");
            return;
        }
        
        if (gameStateManager.CurrentPhase != GamePhase.RollingDice)
        {
            Debug.Log("[DiceRollButton] Button clicked but not in RollingDice phase");
            return;
        }
        
        Debug.Log("[DiceRollButton] Rolling dice...");
        
        // Roll the dice
        gameStateManager.RollDice();
        
        // Start animation
        if (rollAnimationCoroutine != null)
        {
            StopCoroutine(rollAnimationCoroutine);
        }
        rollAnimationCoroutine = StartCoroutine(PlayRollAnimation());
    }
    
    // ============================================
    // PHASE HANDLING
    // ============================================
    
    /// <summary>Called when game phase changes</summary>
    private void OnPhaseChanged(GamePhase newPhase)
    {
        // Enable button only during RollingDice phase
        SetInteractable(newPhase == GamePhase.RollingDice);
        
        // Update button text based on phase
        if (buttonText != null)
        {
            switch (newPhase)
            {
                case GamePhase.RollingDice:
                    buttonText.text = "Roll Dice";
                    break;
                case GamePhase.Placing:
                    buttonText.text = "Placing...";
                    break;
                case GamePhase.Bumping:
                    buttonText.text = "Bumping...";
                    break;
                case GamePhase.EndTurn:
                    buttonText.text = "End Turn";
                    break;
                default:
                    buttonText.text = "Waiting...";
                    break;
            }
        }
    }
    
    /// <summary>Called when dice are rolled</summary>
    private void OnDiceRolled(int[] diceResult)
    {
        if (diceResult == null || diceResult.Length < 2)
            return;
        
        Debug.Log($"[DiceRollButton] Dice rolled: {diceResult[0]} + {diceResult[1]}");
        
        // Update button to show result
        if (buttonText != null)
        {
            buttonText.text = $"Rolled: {diceResult[0]} + {diceResult[1]}";
        }
    }
    
    // ============================================
    // ANIMATIONS
    // ============================================
    
    /// <summary>Play roll animation</summary>
    private IEnumerator PlayRollAnimation()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            
            // Animate button scale (bounce effect)
            float scale = 1f + Mathf.Sin(elapsedTime / animationDuration * Mathf.PI) * 0.1f;
            gameObject.transform.localScale = new Vector3(scale, scale, 1f);
            
            yield return null;
        }
        
        // Reset scale
        gameObject.transform.localScale = Vector3.one;
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
        
        Debug.Log($"[DiceRollButton] Set interactable: {interactable}");
    }
}
