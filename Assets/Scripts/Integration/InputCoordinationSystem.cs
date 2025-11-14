using UnityEngine;

/// <summary>
/// InputCoordinationSystem - Routes input from multiple sources to appropriate handlers.
/// 
/// Responsibilities:
/// - Coordinate input from UI buttons and board cells
/// - Prevent input conflicts (e.g., board input during pause)
/// - Route input to correct handler based on game state
/// - Manage input enable/disable
/// 
/// Architecture:
/// - Single point of input routing
/// - Respects game phase and pause state
/// - Validates input before routing
/// - Provides centralized input enable/disable
/// </summary>
public class InputCoordinationSystem : MonoBehaviour
{
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private GameStateManager gameStateManager;
    private BoardGridManager boardGridManager;
    private BoardInputHandler boardInputHandler;
    private HUDManager hudManager;
    
    private bool isInputEnabled = true;
    private bool isGamePaused = false;
    private bool isInitialized = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    public event System.Action<bool> OnInputEnabledChanged;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInputEnabled => isInputEnabled;
    public bool IsGamePaused => isGamePaused;
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize input coordination system</summary>
    public void Initialize(GameStateManager stateManager, BoardGridManager boardMgr, 
        BoardInputHandler boardInputMgr, HUDManager hudMgr)
    {
        gameStateManager = stateManager;
        boardGridManager = boardMgr;
        boardInputHandler = boardInputMgr;
        hudManager = hudMgr;
        
        if (gameStateManager == null || boardGridManager == null)
        {
            Debug.LogError("InputCoordinationSystem: Required dependencies are null");
            return;
        }
        
        // Subscribe to pause state changes
        if (hudManager != null)
            hudManager.OnPauseStateChanged += OnPauseStateChanged;
        
        // Subscribe to game state changes
        gameStateManager.OnPhaseChanged += OnGamePhaseChanged;
        
        isInitialized = true;
        Debug.Log("InputCoordinationSystem initialized");
    }
    
    // ============================================
    // INPUT ROUTING
    // ============================================
    
    /// <summary>Handle input from board (cell click)</summary>
    public void OnBoardInput(int cellIndex)
    {
        if (!ValidateInputAllowed("board"))
            return;
        
        Debug.Log($"[InputCoordinationSystem] Board input from cell {cellIndex}");
        
        // Board input only allowed during placing phase
        if (gameStateManager.CurrentPhase != GamePhase.Placing)
        {
            Debug.LogWarning("Board input rejected - not in placing phase");
            return;
        }
        
        // Route to board input handler
        if (boardInputHandler != null)
            boardInputHandler.SetInputEnabled(true);
    }
    
    /// <summary>Handle input from UI (button click)</summary>
    public void OnUIInput(string buttonName)
    {
        if (!ValidateInputAllowed("ui"))
            return;
        
        Debug.Log($"[InputCoordinationSystem] UI input from {buttonName}");
        
        // Different phases allow different inputs
        switch (gameStateManager.CurrentPhase)
        {
            case GamePhase.RollingDice:
                // Dice roll button allowed
                break;
            case GamePhase.Placing:
                // Bump, declare win buttons allowed (if game mode allows)
                break;
            case GamePhase.Bumping:
                // Bump interaction allowed
                break;
            default:
                Debug.LogWarning($"UI input rejected for phase {gameStateManager.CurrentPhase}");
                return;
        }
    }
    
    /// <summary>Validate that input is allowed</summary>
    private bool ValidateInputAllowed(string source)
    {
        // Input disabled globally
        if (!isInputEnabled)
        {
            Debug.LogWarning($"[InputCoordinationSystem] Input blocked - input disabled");
            return false;
        }
        
        // Game paused
        if (isGamePaused && source != "ui")
        {
            Debug.LogWarning($"[InputCoordinationSystem] Input blocked - game paused");
            return false;
        }
        
        // Game not in valid state
        if (gameStateManager == null || gameStateManager.CurrentPhase == GamePhase.GameEnd)
        {
            Debug.LogWarning($"[InputCoordinationSystem] Input blocked - game not active");
            return false;
        }
        
        return true;
    }
    
    // ============================================
    // STATE CHANGE HANDLERS
    // ============================================
    
    private void OnGamePhaseChanged(GamePhase newPhase)
    {
        // Update input handler based on phase
        if (boardInputHandler == null)
            return;
        
        switch (newPhase)
        {
            case GamePhase.Placing:
                boardInputHandler.SetInputEnabled(true);
                break;
            default:
                boardInputHandler.SetInputEnabled(false);
                break;
        }
    }
    
    private void OnPauseStateChanged(bool isPaused)
    {
        isGamePaused = isPaused;
        
        if (isPaused)
        {
            // Disable board input during pause
            if (boardInputHandler != null)
                boardInputHandler.SetInputEnabled(false);
        }
        else
        {
            // Re-enable board input if in placing phase
            if (boardInputHandler != null && gameStateManager.CurrentPhase == GamePhase.Placing)
                boardInputHandler.SetInputEnabled(true);
        }
    }
    
    // ============================================
    // PUBLIC INTERFACE
    // ============================================
    
    /// <summary>Enable/disable all input</summary>
    public void SetInputEnabled(bool enabled)
    {
        if (isInputEnabled == enabled)
            return;
        
        isInputEnabled = enabled;
        Debug.Log($"[InputCoordinationSystem] Input {(enabled ? "enabled" : "disabled")}");
        
        // Update all input handlers
        if (boardInputHandler != null)
            boardInputHandler.SetInputEnabled(enabled && !isGamePaused);
        
        OnInputEnabledChanged?.Invoke(enabled);
    }
    
    /// <summary>Check if specific input type is allowed</summary>
    public bool IsInputTypeAllowed(InputType inputType)
    {
        if (!isInputEnabled || isGamePaused)
            return false;
        
        switch (inputType)
        {
            case InputType.BoardCell:
                return gameStateManager.CurrentPhase == GamePhase.Placing;
            case InputType.UIButton:
                return gameStateManager.CurrentPhase != GamePhase.GameEnd;
            default:
                return false;
        }
    }
}

/// <summary>Enumeration of input types</summary>
public enum InputType
{
    BoardCell,
    UIButton,
    Keyboard
}
