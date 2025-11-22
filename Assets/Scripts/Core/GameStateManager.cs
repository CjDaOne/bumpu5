using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central game state machine managing turn flow and phase transitions.
/// Orchestrates all game logic: dice rolling, chip placement, bumping, win detection.
/// Single source of truth for game state.
/// </summary>
public class GameStateManager : MonoBehaviour
{
    // Constants
    private const int MAX_CONSECUTIVE_DOUBLES = 3;
    
    // Core state
    private GamePhase currentPhase;
    private Player currentPlayer;
    private int[] lastDiceRoll;
    private int turnNumber;
    private int consecutiveDoublesCount;
    
    // References
    private BoardModel boardModel;
    private TurnManager turnManager;
    private DiceManager diceManager;
    private List<Player> players;
    private IGameMode currentGameMode; // Added for UI support
    
    // Turn state
    private bool canRollAgain;
    private Chip lastMovedChip;
    private int lastMovedFromCell;
    private int lastMovedToCell;
    private Player gameWinner;
    
    // Phase transition validation table
    private Dictionary<GamePhase, HashSet<GamePhase>> allowedTransitions;
    
    // Events
    public event Action<GamePhase> OnPhaseChanged;
    public event Action<int[]> OnDiceRolled;
    public event Action<Player> OnPlayerChanged;
    public event Action<Player> OnGameWon;
    public event Action<string> OnInvalidAction;
    public event Action<Player, int> OnChipPlaced; // Added for UI support
    public event Action<int> OnChipBumped; // Added for Board support
    
    // Properties
    public GamePhase CurrentPhase => currentPhase;
    public Player CurrentPlayer => currentPlayer;
    public int[] LastDiceRoll => lastDiceRoll;
    public int TurnNumber => turnNumber;
    public bool CanRollAgain => canRollAgain;
    public Player GameWinner => gameWinner;
    public BoardModel Board => boardModel;
    public IGameMode CurrentGameMode => currentGameMode; // Added for UI support
    
    /// <summary>
    /// Return to the main menu scene.
    /// </summary>
    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// End the game with a winner.
    /// </summary>
    public void EndGame(Player winner)
    {
        gameWinner = winner;
        TransitionPhase(GamePhase.GameWon);
        OnGameWon?.Invoke(winner);
        
        if (currentGameMode != null)
        {
            currentGameMode.OnGameEnd(winner);
        }
    }

    /// <summary>
    /// Set the current game mode.
    /// </summary>
    public void SetGameMode(IGameMode mode)
    {
        currentGameMode = mode;
    }
    
    /// <summary>
    /// Initialize the game state manager with game mode and two players.
    /// </summary>
    /// <param name="mode">The game mode to play</param>
    /// <param name="player1">First player</param>
    /// <param name="player2">Second player</param>
    public void Initialize(IGameMode mode, Player player1, Player player2)
    {
        if (player1 == null || player2 == null) // Allow mode to be null for legacy tests
        {
            OnInvalidAction?.Invoke("Players cannot be null");
            return;
        }
        
        currentGameMode = mode;
        players = new List<Player> { player1, player2 };
        boardModel = new BoardModel(player1, player2);
        turnManager = new TurnManager(players);
        diceManager = new DiceManager();
        
        currentPhase = GamePhase.Setup;
        currentPlayer = turnManager.CurrentPlayer;
        turnNumber = 0;
        lastDiceRoll = null;
        consecutiveDoublesCount = 0;
        canRollAgain = false;
        lastMovedChip = null;
        lastMovedFromCell = -1;
        lastMovedToCell = -1;
        gameWinner = null;
        
        // Initialize phase transition validation table
        InitializePhaseTransitions();
    }

    /// <summary>
    /// Initialize the game state manager with two players (Legacy/Test support).
    /// </summary>
    public void Initialize(Player player1, Player player2)
    {
        Initialize(null, player1, player2);
    }
    
    /// <summary>
    /// Initialize the valid phase transitions for the state machine.
    /// </summary>
    private void InitializePhaseTransitions()
    {
        allowedTransitions = new Dictionary<GamePhase, HashSet<GamePhase>>
        {
            // Setup → Rolling (start game)
            { GamePhase.Setup, new HashSet<GamePhase> { GamePhase.Rolling } },
            
            // Rolling → Placing, EndTurn, or GameWon
            { GamePhase.Rolling, new HashSet<GamePhase> { GamePhase.Placing, GamePhase.EndTurn, GamePhase.GameWon } },
            
            // Placing → Bumping or EndTurn
            { GamePhase.Placing, new HashSet<GamePhase> { GamePhase.Bumping, GamePhase.EndTurn, GamePhase.GameWon } },
            
            // Bumping → EndTurn
            { GamePhase.Bumping, new HashSet<GamePhase> { GamePhase.EndTurn, GamePhase.GameWon } },
            
            // EndTurn → Rolling, GameWon, or GameOver
            { GamePhase.EndTurn, new HashSet<GamePhase> { GamePhase.Rolling, GamePhase.GameWon, GamePhase.GameOver } },
            
            // GameWon → GameOver
            { GamePhase.GameWon, new HashSet<GamePhase> { GamePhase.GameOver } },
            
            // GameOver (terminal state)
            { GamePhase.GameOver, new HashSet<GamePhase>() }
        };
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
        
        turnNumber = 1;
        TransitionPhase(GamePhase.Rolling);
    }
    
    /// <summary>
    /// Roll dice and transition to appropriate next phase based on roll.
    /// Handles special cases: 5+6 safe, single 6 lose turn, doubles.
    /// DAY 2 - TASK 2.2: RollDice Phase Handler
    /// </summary>
    /// <remarks>
    /// Special Roll Outcomes:
    /// - 5+6: Safe roll - no placement, skip to EndTurn
    /// - Single 6: Lose turn - no placement, skip to EndTurn
    /// - Doubles: Get to place chip AND roll again
    /// - Triple doubles: Lose turn immediately
    /// - Normal: Place chip once, move to next player
    /// </remarks>
    public void RollDice()
    {
        // Phase validation: only roll in Rolling phase
        if (currentPhase != GamePhase.Rolling)
        {
            OnInvalidAction?.Invoke($"Cannot roll in {currentPhase} phase");
            return;
        }
        
        // Execute dice roll and notify listeners
        lastDiceRoll = diceManager.RollTwoDice();
        OnDiceRolled?.Invoke(lastDiceRoll);
        
        // === SPECIAL CASE 1: 5+6 "Safe" Roll ===
        // No movement, no bonus roll. Safe in some house rules.
        if (IsSafe5Plus6(lastDiceRoll))
        {
            consecutiveDoublesCount = 0;
            canRollAgain = false;
            TransitionPhase(GamePhase.EndTurn);
            return;
        }
        
        // === SPECIAL CASE 2: Single 6 "Lose Turn" ===
        // Rolling a 6 alone (not a double) loses the turn.
        // Delegated to GameMode (Krazy6 overrides this)
        if (currentGameMode != null && currentGameMode.IsLoseTurnRoll(lastDiceRoll))
        {
            consecutiveDoublesCount = 0;
            canRollAgain = false;
            TransitionPhase(GamePhase.EndTurn);
            return;
        }
        
        // === SPECIAL CASE 3: Doubles ===
        // Double rolls allow placement AND another roll.
        if (IsDoubleRoll(lastDiceRoll))
        {
            consecutiveDoublesCount++;
            
            // === SPECIAL CASE 3b: Three Consecutive Doubles ===
            // Rolling 3 doubles in a row = lose turn (penalty rule).
            if (consecutiveDoublesCount >= MAX_CONSECUTIVE_DOUBLES)
            {
                consecutiveDoublesCount = 0;
                canRollAgain = false;
                TransitionPhase(GamePhase.EndTurn);
                return;
            }
            
            // Normal double: grant roll-again privilege
            canRollAgain = true;
        }
        else
        {
            // Not a double: reset double counter and don't allow another roll
            consecutiveDoublesCount = 0;
            canRollAgain = false;
        }
        
        // === NORMAL FLOW: Proceed to Placement ===
        // Player must place a chip based on dice roll.
        TransitionPhase(GamePhase.Placing);
    }
    
    /// <summary>
    /// Place a chip on the board at the target cell.
    /// DAY 2 - TASK 2.3: MoveChip Phase Handler
    /// </summary>
    /// <param name="cellIndex">Target cell index (0-11)</param>
    /// <remarks>
    /// Placement Logic:
    /// 1. Validate phase is Placing
    /// 2. Validate cell is a valid placement target
    /// 3. Execute placement on BoardModel
    /// 4. Check if bumping is possible at this cell
    /// 5. Transition to Bumping (if possible) or EndTurn (if not)
    /// </remarks>
    public void PlaceChip(int cellIndex)
    {
        // Phase validation: only place chips in Placing phase
        if (currentPhase != GamePhase.Placing)
        {
            OnInvalidAction?.Invoke($"Cannot place chip in {currentPhase} phase");
            return;
        }
        
        // Cell validation: ensure target is a valid placement
        if (!CanPlaceChip(cellIndex))
        {
            OnInvalidAction?.Invoke("Invalid placement target");
            return;
        }
        
        // Get the distance from the dice roll for validation
        int moveDistance = DiceManager.GetDiceSum(lastDiceRoll);
        
        // Record the movement
        lastMovedToCell = cellIndex;
        
        // Notify listeners of chip placement
        OnChipPlaced?.Invoke(currentPlayer, cellIndex);
        
        // Check if bumping is possible at this cell
        // If yes, transition to Bumping phase (optional)
        // If no, skip bumping and go straight to EndTurn
        bool canBumpAtCell = boardModel.CanBump(currentPlayer, cellIndex);
        
        if (canBumpAtCell)
        {
            // Opponent chip present at this cell - give player choice to bump or skip
            TransitionPhase(GamePhase.Bumping);
        }
        else
        {
            // No opponent chip - skip bumping phase
            TransitionPhase(GamePhase.EndTurn);
        }
    }
    
    /// <summary>
    /// Bump an opponent's chip at the target cell.
    /// DAY 2 - TASK 2.4: BumpOpponent Phase Handler
    /// </summary>
    /// <param name="cellIndex">Target cell with opponent chip</param>
    /// <remarks>
    /// Bump Logic:
    /// 1. Validate phase is Bumping
    /// 2. Validate opponent chip is present and bumpable
    /// 3. Execute bump on BoardModel (moves opponent chip off board)
    /// 4. Award player bump bonus if applicable
    /// 5. Transition to EndTurn
    /// </remarks>
    public void BumpOpponentChip(int cellIndex)
    {
        // Phase validation: only bump in Bumping phase
        if (currentPhase != GamePhase.Bumping)
        {
            OnInvalidAction?.Invoke($"Cannot bump in {currentPhase} phase");
            return;
        }
        
        // Cell validation: ensure target has bumpable opponent chip
        if (!CanBumpChip(cellIndex))
        {
            OnInvalidAction?.Invoke("Cannot bump that cell");
            return;
        }
        
        // Execute the bump (removes opponent chip from board)
        boardModel.ApplyBump(currentPlayer, cellIndex);
        
        // Notify listeners
        OnChipBumped?.Invoke(cellIndex);
        
        // Transition to end turn (bump completes the turn)
        TransitionPhase(GamePhase.EndTurn);
    }
    
    /// <summary>
    /// Skip bumping and advance to end turn.
    /// DAY 2 - TASK 2.4: BumpOpponent Phase Handler (Skip Path)
    /// </summary>
    /// <remarks>
    /// Skip Bump Logic:
    /// 1. Validate phase is Bumping
    /// 2. Transition directly to EndTurn (no bump executed)
    /// 3. Player may skip even if bumping is possible
    /// </remarks>
    public void SkipBump()
    {
        // Phase validation: only skip bump in Bumping phase
        if (currentPhase != GamePhase.Bumping)
        {
            OnInvalidAction?.Invoke("Cannot skip bump in current phase");
            return;
        }
        
        // Skip to end turn (no bump executed)
        TransitionPhase(GamePhase.EndTurn);
    }
    
    /// <summary>
    /// End current turn and advance game state.
    /// DAY 3 - TASK 3.1: EndTurn Phase Handler
    /// </summary>
    /// <remarks>
    /// End Turn Logic (Decision Tree):
    /// 1. Check if player can roll again (rolled doubles)
    ///    YES: Reset state, stay with same player, go to Rolling
    ///    NO: Continue...
    /// 2. Check if player has won
    ///    YES: Transition to GameWon phase
    ///    NO: Continue...
    /// 3. Advance to next player
    ///    - Increment turn number
    ///    - Rotate player via TurnManager
    ///    - Fire OnPlayerChanged event
    ///    - Reset turn-specific state
    /// 4. Transition back to Rolling for next player
    /// </remarks>
    public void EndTurn()
    {
        // Phase validation: only end turn from Placing, Bumping, Rolling, or EndTurn phases
        if (currentPhase != GamePhase.Placing && 
            currentPhase != GamePhase.Bumping && 
            currentPhase != GamePhase.Rolling &&
            currentPhase != GamePhase.EndTurn)
        {
            OnInvalidAction?.Invoke("Cannot end turn in current phase");
            return;
        }
        
        // === END TURN DECISION 1: Check for Doubles Bonus ===
        // If player rolled doubles, they get another roll (stay as current player)
        if (canRollAgain)
        {
            // Reset roll-again flag and clear roll history
            canRollAgain = false;
            lastDiceRoll = null;
            lastMovedToCell = -1;
            lastMovedFromCell = -1;
            
            // Give same player another turn
            TransitionPhase(GamePhase.Rolling);
            return;
        }
        
        // === END TURN DECISION 2: Check for Win Condition ===
        // Before advancing turn, check if current player has won
        if (HasWon(currentPlayer))
        {
            gameWinner = currentPlayer;
            OnGameWon?.Invoke(currentPlayer);
            TransitionPhase(GamePhase.GameWon);
            return;
        }
        
        // === END TURN DECISION 3: Advance to Next Player ===
        // No bonus roll, no win - normal turn completion
        
        // Increment turn counter (used for statistics)
        turnNumber++;
        
        // Rotate to next player via TurnManager
        turnManager.AdvanceTurn();
        currentPlayer = turnManager.CurrentPlayer;
        OnPlayerChanged?.Invoke(currentPlayer);
        
        // Reset all turn-specific state for next player
        lastDiceRoll = null;
        lastMovedToCell = -1;
        lastMovedFromCell = -1;
        lastMovedChip = null;
        consecutiveDoublesCount = 0;
        
        // Transition to Rolling phase for next player
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
        
        // Cell must be empty or have opponent's chip (for bumping)
        BoardCell targetCell = boardModel.Cells[cellIndex];
        if (targetCell.HasChip && targetCell.Owner == currentPlayer)
        {
            return false; // Can't move to own chip
        }
        
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
        
        return boardModel.CanBump(currentPlayer, cellIndex);
    }
    
    /// <summary>
    /// Check if current player has won.
    /// </summary>
    /// <returns>True if win condition met</returns>
    public bool HasWon(Player player)
    {
        if (player == null)
            return false;
            
        if (currentGameMode != null)
        {
            return currentGameMode.CheckWinCondition(player);
        }
            
        return boardModel.Check5InARow(player);
    }
    
    /// <summary>
    /// Declare that the current player has won (for UI button).
    /// DAY 3 - TASK 3.2: Win Declaration Handler
    /// Only valid if player actually has winning condition met.
    /// </summary>
    /// <remarks>
    /// Called when player clicks "I WON!" button.
    /// Validates that win condition is actually met before accepting declaration.
    /// </remarks>
    public void DeclareWin()
    {
        // Player validation
        if (currentPlayer == null)
        {
            OnInvalidAction?.Invoke("No current player");
            return;
        }
        
        // Win condition validation
        if (!HasWon(currentPlayer))
        {
            OnInvalidAction?.Invoke("Cannot declare win - condition not met");
            return;
        }
        
        // Set winner and fire event
        gameWinner = currentPlayer;
        OnGameWon?.Invoke(currentPlayer);
        TransitionPhase(GamePhase.GameWon);
    }
    
    /// <summary>
    /// Transition to game over (terminal state).
    /// DAY 3 - TASK 3.2: Game Over Terminal Handler
    /// Called after GameWon phase display delay (celebration screen, etc).
    /// </summary>
    /// <remarks>
    /// This is a terminal state - once reached, game is over and cannot continue.
    /// Used to finalize the game after winner is declared.
    /// </remarks>
    public void GoToGameOver()
    {
        // Phase validation: only transition from GameWon
        if (currentPhase != GamePhase.GameWon)
        {
            OnInvalidAction?.Invoke("Can only go to GameOver from GameWon");
            return;
        }
        
        // Terminal transition
        TransitionPhase(GamePhase.GameOver);
    }
    
    /// <summary>
    /// Get all valid moves for the current player based on dice roll.
    /// </summary>
    /// <returns>List of valid target cell indices</returns>
    public List<int> GetValidMoves()
    {
        List<int> validMoves = new List<int>();
        
        if (lastDiceRoll == null || lastDiceRoll.Length == 0)
            return validMoves;
        
        // For now, return all non-own chips
        // Full implementation will consider dice distance
        for (int i = 0; i < BoardModel.BOARD_SIZE; i++)
        {
            if (CanPlaceChip(i))
            {
                validMoves.Add(i);
            }
        }
        
        return validMoves;
    }
    
    /// <summary>
    /// Transition to a new game phase after validating the transition.
    /// Includes phase exit/entry hooks for cleanup and setup.
    /// </summary>
    /// <param name="newPhase">Target phase</param>
    private void TransitionPhase(GamePhase newPhase)
    {
        // Validate transition
        if (!IsValidTransition(currentPhase, newPhase))
        {
            OnInvalidAction?.Invoke($"Invalid transition: {currentPhase} → {newPhase}");
            return;
        }
        
        // Call phase exit logic
        OnPhaseExit(currentPhase);
        
        // Update state
        currentPhase = newPhase;
        
        // Call phase entry logic
        OnPhaseEnter(currentPhase);
        
        // Notify listeners of phase change
        OnPhaseChanged?.Invoke(newPhase);
        
        // Check win condition after transition (but not in certain phases)
        if (newPhase != GamePhase.Setup && newPhase != GamePhase.GameWon && newPhase != GamePhase.GameOver)
        {
            if (HasWon(currentPlayer))
            {
                gameWinner = currentPlayer;
                OnGameWon?.Invoke(currentPlayer);
                currentPhase = GamePhase.GameWon;
                OnPhaseChanged?.Invoke(GamePhase.GameWon);
            }
        }
    }
    
    /// <summary>
    /// Called when exiting a phase. Cleanup and finalization logic.
    /// </summary>
    /// <param name="phase">Phase being exited</param>
    private void OnPhaseExit(GamePhase phase)
    {
        // Override in derived classes if needed
        // For now, no-op
    }
    
    /// <summary>
    /// Called when entering a phase. Setup and initialization logic.
    /// </summary>
    /// <param name="phase">Phase being entered</param>
    private void OnPhaseEnter(GamePhase phase)
    {
        // Override in derived classes if needed
        // For now, no-op
    }
    
    /// <summary>
    /// Check if a phase transition is valid.
    /// </summary>
    /// <param name="fromPhase">Current phase</param>
    /// <param name="toPhase">Target phase</param>
    /// <returns>True if transition is allowed</returns>
    private bool IsValidTransition(GamePhase fromPhase, GamePhase toPhase)
    {
        if (allowedTransitions == null || !allowedTransitions.ContainsKey(fromPhase))
            return false;
        
        return allowedTransitions[fromPhase].Contains(toPhase);
    }
    
    /// <summary>
    /// Check if roll results in losing the turn (rolling a 6 alone).
    /// </summary>
    /// <param name="roll">Dice roll result</param>
    /// <returns>True if turn is lost</returns>
    private bool IsLoseTurnRoll(int[] roll)
    {
        if (roll == null || roll.Length == 0)
            return false;
        
        // Single die showing 6 = lose turn
        // This is a baseline rule; modes may override
        return roll.Length == 1 && roll[0] == 6;
    }
    
    /// <summary>
    /// Check if roll is a double (both dice show same value).
    /// </summary>
    /// <param name="roll">Dice roll result</param>
    /// <returns>True if double</returns>
    private bool IsDoubleRoll(int[] roll)
    {
        if (roll == null || roll.Length != 2)
            return false;
        
        return roll[0] == roll[1];
    }
    
    /// <summary>
    /// Check if roll is the special 5+6 "safe" combination.
    /// </summary>
    /// <param name="roll">Dice roll result</param>
    /// <returns>True if 5+6</returns>
    private bool IsSafe5Plus6(int[] roll)
    {
        if (roll == null || roll.Length != 2)
            return false;
        
        return (roll[0] == 5 && roll[1] == 6) || (roll[0] == 6 && roll[1] == 5);
    }
}
