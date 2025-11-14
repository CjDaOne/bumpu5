using System;
using System.Collections.Generic;

/// <summary>
/// Central game state machine managing turn flow and phase transitions.
/// Orchestrates all game logic: dice rolling, chip placement, bumping, win detection.
/// Single source of truth for game state.
/// </summary>
public class GameStateManager
{
    // Core state
    private GamePhase currentPhase;
    private Player currentPlayer;
    private int[] lastDiceRoll;
    private int turnNumber;
    
    // References
    private BoardModel boardModel;
    private TurnManager turnManager;
    private DiceManager diceManager;
    private Player[] players;
    
    // Events
    public event Action<GamePhase> OnPhaseChanged;
    public event Action<int[]> OnDiceRolled;
    public event Action<Player> OnPlayerChanged;
    public event Action<Player> OnGameWon;
    public event Action<string> OnInvalidAction;
    
    // Properties
    public GamePhase CurrentPhase => currentPhase;
    public Player CurrentPlayer => currentPlayer;
    public int[] LastDiceRoll => lastDiceRoll;
    public int TurnNumber => turnNumber;
    
    /// <summary>
    /// Initialize the game state manager with players and board.
    /// </summary>
    /// <param name="player1">First player</param>
    /// <param name="player2">Second player</param>
    public void Initialize(Player player1, Player player2)
    {
        if (player1 == null || player2 == null)
        {
            OnInvalidAction?.Invoke("Players cannot be null");
            return;
        }
        
        players = new[] { player1, player2 };
        boardModel = new BoardModel();
        turnManager = new TurnManager(2);
        diceManager = new DiceManager();
        
        currentPhase = GamePhase.Setup;
        currentPlayer = players[0];
        turnNumber = 0;
        lastDiceRoll = null;
    }
    
    /// <summary>
    /// Start the game, transitioning to Rolling phase.
    /// </summary>
    public void StartGame()
    {
        if (currentPhase != GamePhase.Setup)
        {
            OnInvalidAction?.Invoke("Game already started");
            return;
        }
        
        TransitionPhase(GamePhase.Rolling);
    }
    
    /// <summary>
    /// Roll dice and transition to placing phase.
    /// </summary>
    public void RollDice()
    {
        if (currentPhase != GamePhase.Rolling)
        {
            OnInvalidAction?.Invoke($"Cannot roll in {currentPhase} phase");
            return;
        }
        
        lastDiceRoll = diceManager.Roll();
        OnDiceRolled?.Invoke(lastDiceRoll);
        
        // Check for 6 (lose turn) or 5+6 (safe, skip)
        if (IsLoseTurnRoll(lastDiceRoll))
        {
            EndTurn();
            return;
        }
        
        TransitionPhase(GamePhase.Placing);
    }
    
    /// <summary>
    /// Place a chip on the board.
    /// </summary>
    /// <param name="cellIndex">Target cell index</param>
    public void PlaceChip(int cellIndex)
    {
        if (currentPhase != GamePhase.Placing)
        {
            OnInvalidAction?.Invoke($"Cannot place chip in {currentPhase} phase");
            return;
        }
        
        if (!CanPlaceChip(cellIndex))
        {
            OnInvalidAction?.Invoke("Invalid placement target");
            return;
        }
        
        // Execute placement logic (will be enhanced in later sprints)
        // For now, assume placement succeeds
        
        TransitionPhase(GamePhase.Bumping);
    }
    
    /// <summary>
    /// Bump an opponent's chip.
    /// </summary>
    /// <param name="cellIndex">Target cell with opponent chip</param>
    public void BumpOpponentChip(int cellIndex)
    {
        if (currentPhase != GamePhase.Bumping)
        {
            OnInvalidAction?.Invoke($"Cannot bump in {currentPhase} phase");
            return;
        }
        
        if (!CanBumpChip(cellIndex))
        {
            OnInvalidAction?.Invoke("Cannot bump that cell");
            return;
        }
        
        // Execute bump logic (will be enhanced in later sprints)
        
        TransitionPhase(GamePhase.EndTurn);
    }
    
    /// <summary>
    /// End current turn and transition to next player.
    /// </summary>
    public void EndTurn()
    {
        if (currentPhase != GamePhase.Placing && currentPhase != GamePhase.Bumping && currentPhase != GamePhase.Rolling)
        {
            OnInvalidAction?.Invoke("Cannot end turn in current phase");
            return;
        }
        
        turnNumber++;
        turnManager.RotatePlayer();
        currentPlayer = players[turnManager.CurrentPlayerIndex];
        OnPlayerChanged?.Invoke(currentPlayer);
        
        TransitionPhase(GamePhase.Rolling);
    }
    
    /// <summary>
    /// Check if a chip can be placed on a given cell.
    /// </summary>
    /// <param name="cellIndex">Target cell index</param>
    /// <returns>True if placement is valid</returns>
    public bool CanPlaceChip(int cellIndex)
    {
        if (cellIndex < 0 || cellIndex >= BoardModel.BOARD_SIZE)
        {
            return false;
        }
        
        // Additional validation will be added in later sprints
        return true;
    }
    
    /// <summary>
    /// Check if a chip can be bumped on a given cell.
    /// </summary>
    /// <param name="cellIndex">Target cell index</param>
    /// <returns>True if bump is valid</returns>
    public bool CanBumpChip(int cellIndex)
    {
        if (cellIndex < 0 || cellIndex >= BoardModel.BOARD_SIZE)
        {
            return false;
        }
        
        // Additional validation will be added in later sprints
        return true;
    }
    
    /// <summary>
    /// Check if current player has won.
    /// </summary>
    /// <returns>True if win condition met</returns>
    public bool HasWon(Player player)
    {
        // Will be enhanced with game mode specific logic in Sprint 3
        return boardModel.Has5InARow(player);
    }
    
    /// <summary>
    /// Transition to a new game phase and fire event.
    /// </summary>
    /// <param name="newPhase">Target phase</param>
    private void TransitionPhase(GamePhase newPhase)
    {
        currentPhase = newPhase;
        OnPhaseChanged?.Invoke(newPhase);
        
        // Check win condition after transition
        if (HasWon(currentPlayer))
        {
            OnGameWon?.Invoke(currentPlayer);
            currentPhase = GamePhase.GameOver;
            OnPhaseChanged?.Invoke(GamePhase.GameOver);
        }
    }
    
    /// <summary>
    /// Check if roll results in losing the turn (rolling a 6 alone).
    /// </summary>
    /// <param name="roll">Dice roll result</param>
    /// <returns>True if turn is lost</returns>
    private bool IsLoseTurnRoll(int[] roll)
    {
        // Single die showing 6 = lose turn
        if (roll.Length == 1 && roll[0] == 6)
        {
            return true;
        }
        
        return false;
    }
}
