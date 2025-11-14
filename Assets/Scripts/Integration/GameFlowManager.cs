using UnityEngine;

/// <summary>
/// GameFlowManager - Master orchestrator for the entire game flow.
/// 
/// Responsibilities:
/// - Initialize all systems (GameStateManager, BoardGridManager, HUDManager)
/// - Manage game state transitions (menu → mode selection → game → end)
/// - Coordinate between Board, UI, and GameState systems
/// - Handle game lifecycle (start, pause, resume, end)
/// 
/// Architecture:
/// - Single point of control for game flow
/// - Subscribes to major system events
/// - Initializes systems in correct order
/// - Delegates to specialized systems for their responsibilities
/// </summary>
public class GameFlowManager : MonoBehaviour
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField]
    private GameStateManager gameStateManager;
    
    [SerializeField]
    private BoardGridManager boardGridManager;
    
    [SerializeField]
    private HUDManager hudManager;
    
    [SerializeField]
    private BoardInputHandler boardInputHandler;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private GameFlowState currentFlowState = GameFlowState.MainMenu;
    private bool isInitialized = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    public event System.Action<GameFlowState> OnFlowStateChanged;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public GameFlowState CurrentFlowState => currentFlowState;
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize all game systems in correct order</summary>
    public void Initialize()
    {
        if (isInitialized)
        {
            Debug.LogWarning("GameFlowManager already initialized");
            return;
        }
        
        Debug.Log("[GameFlowManager] Initializing all systems...");
        
        // Initialize GameStateManager first (base system)
        if (gameStateManager != null)
        {
            gameStateManager.Initialize(GameModeFactory.CreateGameMode(1), 
                new Player(1, "Player1"),
                new Player(2, "Player2"));
            Debug.Log("[GameFlowManager] GameStateManager initialized");
        }
        else
        {
            Debug.LogError("GameFlowManager: GameStateManager not assigned");
            return;
        }
        
        // Initialize BoardGridManager (depends on GameStateManager)
        if (boardGridManager != null)
        {
            boardGridManager.Initialize(gameStateManager);
            Debug.Log("[GameFlowManager] BoardGridManager initialized");
        }
        else
        {
            Debug.LogWarning("GameFlowManager: BoardGridManager not assigned");
        }
        
        // Initialize HUDManager (depends on GameStateManager)
        if (hudManager != null)
        {
            hudManager.Initialize(gameStateManager);
            Debug.Log("[GameFlowManager] HUDManager initialized");
        }
        else
        {
            Debug.LogWarning("GameFlowManager: HUDManager not assigned");
        }
        
        // Initialize BoardInputHandler (depends on BoardGridManager and GameStateManager)
        if (boardInputHandler != null)
        {
            boardInputHandler.Initialize(boardGridManager, gameStateManager);
            Debug.Log("[GameFlowManager] BoardInputHandler initialized");
        }
        else
        {
            Debug.LogWarning("GameFlowManager: BoardInputHandler not assigned");
        }
        
        // Subscribe to game state events
        SubscribeToGameStateEvents();
        
        isInitialized = true;
        SetFlowState(GameFlowState.MainMenu);
        
        Debug.Log("[GameFlowManager] All systems initialized successfully");
    }
    
    /// <summary>Cleanup all systems</summary>
    public void Shutdown()
    {
        if (boardInputHandler != null)
            boardInputHandler.Shutdown();
        
        if (boardGridManager != null)
            boardGridManager.Shutdown();
        
        if (hudManager != null)
            hudManager.Shutdown();
        
        if (gameStateManager != null)
            gameStateManager.Shutdown();
        
        isInitialized = false;
        Debug.Log("[GameFlowManager] All systems shut down");
    }
    
    // ============================================
    // FLOW STATE MANAGEMENT
    // ============================================
    
    /// <summary>Set the current game flow state</summary>
    private void SetFlowState(GameFlowState newState)
    {
        if (currentFlowState == newState)
            return;
        
        Debug.Log($"[GameFlowManager] Flow state: {currentFlowState} → {newState}");
        currentFlowState = newState;
        OnFlowStateChanged?.Invoke(newState);
        
        switch (newState)
        {
            case GameFlowState.MainMenu:
                OnEnterMainMenu();
                break;
            case GameFlowState.GameModeSelection:
                OnEnterGameModeSelection();
                break;
            case GameFlowState.GamePlaying:
                OnEnterGamePlaying();
                break;
            case GameFlowState.GamePaused:
                OnEnterGamePaused();
                break;
            case GameFlowState.GameEnd:
                OnEnterGameEnd();
                break;
        }
    }
    
    // ============================================
    // FLOW STATE HANDLERS
    // ============================================
    
    private void OnEnterMainMenu()
    {
        Debug.Log("[GameFlowManager] Entering Main Menu");
        // TODO: Show main menu UI
    }
    
    private void OnEnterGameModeSelection()
    {
        Debug.Log("[GameFlowManager] Entering Game Mode Selection");
        // TODO: Show game mode selection UI
    }
    
    private void OnEnterGamePlaying()
    {
        Debug.Log("[GameFlowManager] Entering Game Playing");
        
        if (gameStateManager != null)
        {
            gameStateManager.StartGame();
        }
    }
    
    private void OnEnterGamePaused()
    {
        Debug.Log("[GameFlowManager] Entering Game Paused");
        
        if (hudManager != null)
            hudManager.PauseGame();
    }
    
    private void OnEnterGameEnd()
    {
        Debug.Log("[GameFlowManager] Entering Game End");
        // TODO: Show game end screen
    }
    
    // ============================================
    // EVENT SUBSCRIPTION
    // ============================================
    
    private void SubscribeToGameStateEvents()
    {
        if (gameStateManager == null)
            return;
        
        gameStateManager.OnGameWon += OnGameWon;
    }
    
    // ============================================
    // EVENT HANDLERS
    // ============================================
    
    private void OnGameWon(Player winner)
    {
        Debug.Log($"[GameFlowManager] Game won by {winner.PlayerName}");
        SetFlowState(GameFlowState.GameEnd);
    }
    
    // ============================================
    // PUBLIC INTERFACE
    // ============================================
    
    /// <summary>Start a new game with specified mode</summary>
    public void StartGame(int gameModeId)
    {
        Debug.Log($"[GameFlowManager] Starting game with mode {gameModeId}");
        
        if (gameStateManager == null)
        {
            Debug.LogError("GameFlowManager: Cannot start game - GameStateManager not initialized");
            return;
        }
        
        IGameMode gameMode = GameModeFactory.CreateGameMode(gameModeId);
        if (gameMode == null)
        {
            Debug.LogError($"GameFlowManager: Unknown game mode {gameModeId}");
            return;
        }
        
        // Reinitialize game state with new mode
        gameStateManager.Initialize(gameMode, 
            new Player(1, "Player1"),
            new Player(2, "Player2"));
        
        // Reset board
        if (boardGridManager != null)
            boardGridManager.ClearBoard();
        
        // Transition to playing state
        SetFlowState(GameFlowState.GamePlaying);
    }
    
    /// <summary>Pause the current game</summary>
    public void PauseGame()
    {
        if (currentFlowState != GameFlowState.GamePlaying)
        {
            Debug.LogWarning("GameFlowManager: Cannot pause - not in playing state");
            return;
        }
        
        SetFlowState(GameFlowState.GamePaused);
    }
    
    /// <summary>Resume the paused game</summary>
    public void ResumeGame()
    {
        if (currentFlowState != GameFlowState.GamePaused)
        {
            Debug.LogWarning("GameFlowManager: Cannot resume - not in paused state");
            return;
        }
        
        SetFlowState(GameFlowState.GamePlaying);
    }
    
    /// <summary>Return to main menu</summary>
    public void ReturnToMainMenu()
    {
        Debug.Log("[GameFlowManager] Returning to main menu");
        
        // Clean up current game
        if (boardGridManager != null)
            boardGridManager.ClearBoard();
        
        SetFlowState(GameFlowState.MainMenu);
    }
    
    /// <summary>End current game</summary>
    public void EndGame()
    {
        Debug.Log("[GameFlowManager] Ending game");
        SetFlowState(GameFlowState.GameEnd);
    }
}

/// <summary>Game flow state enumeration</summary>
public enum GameFlowState
{
    MainMenu,
    GameModeSelection,
    GamePlaying,
    GamePaused,
    GameEnd
}
