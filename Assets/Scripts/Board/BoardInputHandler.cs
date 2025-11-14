using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// BoardInputHandler - Dedicated input processing for the board system.
/// 
/// Responsibilities:
/// - Convert mouse/touch input to cell selections
/// - Handle input during different game phases
/// - Validate input against current game state
/// - Provide haptic feedback on mobile
/// - Support keyboard shortcuts (optional)
/// 
/// Architecture:
/// - Receives input from CellView components
/// - Validates against GameStateManager
/// - Routes valid moves to BoardGridManager
/// - Ignores input during non-game phases
/// </summary>
public class BoardInputHandler : MonoBehaviour
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField]
    private BoardGridManager boardGridManager;
    
    [SerializeField]
    private GameStateManager gameStateManager;
    
    [SerializeField]
    private bool enableKeyboardShortcuts = true;
    
    [SerializeField]
    private bool enableHapticFeedback = true;
    
    [SerializeField]
    private float doubleTapWindow = 0.3f;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private float lastTapTime = 0f;
    private int lastTappedCell = -1;
    private bool isInputEnabled = true;
    private bool isInitialized = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    /// <summary>Fired when valid cell selected</summary>
    public event System.Action<int> OnCellSelected;
    
    /// <summary>Fired when invalid cell selected</summary>
    public event System.Action<int> OnInvalidSelection;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInputEnabled
    {
        get => isInputEnabled;
        set => isInputEnabled = value;
    }
    
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize input handler</summary>
    public void Initialize(BoardGridManager bgm, GameStateManager gsm)
    {
        boardGridManager = bgm;
        gameStateManager = gsm;
        
        if (boardGridManager == null)
        {
            Debug.LogError("BoardInputHandler: BoardGridManager is null");
            return;
        }
        
        if (gameStateManager == null)
        {
            Debug.LogError("BoardInputHandler: GameStateManager is null");
            return;
        }
        
        // Subscribe to board events
        boardGridManager.OnCellSelected += HandleCellSelected;
        
        // Subscribe to game state events
        gameStateManager.OnPhaseChanged += HandlePhaseChanged;
        gameStateManager.OnGameWon += HandleGameWon;
        
        isInitialized = true;
        Debug.Log("BoardInputHandler initialized successfully");
    }
    
    /// <summary>Cleanup on shutdown</summary>
    public void Shutdown()
    {
        if (boardGridManager != null)
        {
            boardGridManager.OnCellSelected -= HandleCellSelected;
        }
        
        if (gameStateManager != null)
        {
            gameStateManager.OnPhaseChanged -= HandlePhaseChanged;
            gameStateManager.OnGameWon -= HandleGameWon;
        }
        
        isInitialized = false;
    }
    
    /// <summary>Update called once per frame</summary>
    private void Update()
    {
        if (!isInputEnabled || !isInitialized)
            return;
        
        // Handle keyboard shortcuts if enabled
        if (enableKeyboardShortcuts)
        {
            HandleKeyboardInput();
        }
    }
    
    // ============================================
    // INPUT HANDLING
    // ============================================
    
    /// <summary>Handle cell selection from BoardGridManager</summary>
    private void HandleCellSelected(int cellIndex)
    {
        if (!isInputEnabled)
        {
            OnInvalidSelection?.Invoke(cellIndex);
            return;
        }
        
        // Only accept input during placement phase
        if (gameStateManager.CurrentPhase != GamePhase.Placing)
        {
            Debug.Log($"[BoardInputHandler] Input ignored - not in placing phase (current: {gameStateManager.CurrentPhase})");
            OnInvalidSelection?.Invoke(cellIndex);
            ProvideInvalidFeedback();
            return;
        }
        
        // Check for double-tap
        if (DetectDoubleTap(cellIndex))
        {
            Debug.Log($"[BoardInputHandler] Double-tap detected on cell {cellIndex}");
            // Could use for special action (e.g., quick bump)
        }
        
        // Validate move through game state manager
        if (gameStateManager.TryPlaceChip(cellIndex))
        {
            Debug.Log($"[BoardInputHandler] Valid move: cell {cellIndex}");
            OnCellSelected?.Invoke(cellIndex);
            ProvideValidFeedback();
        }
        else
        {
            Debug.Log($"[BoardInputHandler] Invalid move: cell {cellIndex}");
            OnInvalidSelection?.Invoke(cellIndex);
            ProvideInvalidFeedback();
        }
    }
    
    /// <summary>Handle keyboard shortcuts</summary>
    private void HandleKeyboardInput()
    {
        // 'R' to roll dice (if in rolling phase)
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gameStateManager.CurrentPhase == GamePhase.RollingDice)
            {
                gameStateManager.RollDice();
            }
        }
        
        // 'B' to bump (if in bumping phase)
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (gameStateManager.CurrentPhase == GamePhase.Bumping)
            {
                // Would need to know which cell to bump
                // This is simplified - actual implementation would need UI feedback
            }
        }
        
        // 'W' to declare win (if player has won)
        if (Input.GetKeyDown(KeyCode.W))
        {
            Player currentPlayer = gameStateManager.CurrentPlayer;
            if (currentPlayer != null && gameStateManager.CurrentGameMode.CheckWinCondition(currentPlayer))
            {
                gameStateManager.EndGame(currentPlayer);
            }
        }
        
        // ESC to show pause menu (handled elsewhere typically)
    }
    
    // ============================================
    // GAME STATE HANDLERS
    // ============================================
    
    /// <summary>Handle phase change</summary>
    private void HandlePhaseChanged(GamePhase newPhase)
    {
        // Enable/disable input based on phase
        switch (newPhase)
        {
            case GamePhase.Placing:
                isInputEnabled = true;
                Debug.Log("[BoardInputHandler] Input enabled for placing phase");
                break;
            
            case GamePhase.RollingDice:
            case GamePhase.Bumping:
            case GamePhase.EndTurn:
                isInputEnabled = false;
                Debug.Log("[BoardInputHandler] Input disabled for non-placing phase");
                break;
            
            case GamePhase.GameStart:
            case GamePhase.GameEnd:
                isInputEnabled = false;
                break;
        }
    }
    
    /// <summary>Handle game won</summary>
    private void HandleGameWon(Player winner)
    {
        isInputEnabled = false;
        Debug.Log($"[BoardInputHandler] Input disabled - game won by {winner.PlayerName}");
    }
    
    // ============================================
    // FEEDBACK GENERATION
    // ============================================
    
    /// <summary>Provide feedback for valid move</summary>
    private void ProvideValidFeedback()
    {
        // Visual feedback handled by CellView
        
        // Audio feedback (if audio manager exists)
        // AudioManager.Instance?.PlaySound("move_valid");
        
        // Haptic feedback on mobile
        if (enableHapticFeedback)
        {
            Handheld.Vibrate();
        }
    }
    
    /// <summary>Provide feedback for invalid move</summary>
    private void ProvideInvalidFeedback()
    {
        // Visual feedback handled by CellView (shake or highlight red)
        
        // Audio feedback (if audio manager exists)
        // AudioManager.Instance?.PlaySound("move_invalid");
        
        // Haptic feedback on mobile (different pattern)
        if (enableHapticFeedback)
        {
            Handheld.Vibrate();
        }
    }
    
    // ============================================
    // GESTURE DETECTION
    // ============================================
    
    /// <summary>Detect double-tap on same cell</summary>
    private bool DetectDoubleTap(int cellIndex)
    {
        float timeSinceLastTap = Time.time - lastTapTime;
        
        // Check if same cell and within time window
        if (cellIndex == lastTappedCell && timeSinceLastTap < doubleTapWindow)
        {
            lastTapTime = 0f;
            lastTappedCell = -1;
            return true;
        }
        
        lastTapTime = Time.time;
        lastTappedCell = cellIndex;
        return false;
    }
    
    // ============================================
    // PUBLIC CONTROL METHODS
    // ============================================
    
    /// <summary>Enable/disable input temporarily</summary>
    public void SetInputEnabled(bool enabled)
    {
        isInputEnabled = enabled;
        Debug.Log($"[BoardInputHandler] Input set to {(enabled ? "enabled" : "disabled")}");
    }
    
    /// <summary>Clear gesture state (useful after phase change)</summary>
    public void ClearGestureState()
    {
        lastTapTime = 0f;
        lastTappedCell = -1;
    }
}
