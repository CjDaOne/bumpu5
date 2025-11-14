using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Central game state machine managing turn flow and phase transitions.
/// Orchestrates all game logic: dice rolling, chip placement, bumping, win detection.
/// Single source of truth for game state.
/// </summary>
public class GameStateManager
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
    
    // Properties
    public GamePhase CurrentPhase => currentPhase;
    public Player CurrentPlayer => currentPlayer;
    public int[] LastDiceRoll => lastDiceRoll;
    public int TurnNumber => turnNumber;
    public bool CanRollAgain => canRollAgain;
    public Player GameWinner => gameWinner;
    
    /// <summary>
    /// Initialize the game state manager with two players.
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
    /// Roll dice and transition to placing phase.
    /// </summary>
    public void RollDice()
    {
        if (currentPhase != GamePhase.Rolling)
        {
            OnInvalidAction?.Invoke($"Cannot roll in {currentPhase} phase");
            return;
        }
        
        lastDiceRoll = diceManager.RollTwoDice();
        OnDiceRolled?.Invoke(lastDiceRoll);
        
        // Check for special cases
        if (IsSafe5Plus6(lastDiceRoll))
        {
            // 5+6 is "safe" - skip all movement phases, go straight to end turn
            EndTurn();
            return;
        }
        
        if (IsLoseTurnRoll(lastDiceRoll))
        {
            // Rolled a 6 on single die - lose turn
            EndTurn();
            return;
        }
        
        // Check for doubles
        if (IsDoubleRoll(lastDiceRoll))
        {
            consecutiveDoublesCount++;
            if (consecutiveDoublesCount >= MAX_CONSECUTIVE_DOUBLES)
            {
                // Three doubles in a row = lose turn
                consecutiveDoublesCount = 0;
                EndTurn();
                return;
            }
            canRollAgain = true;
        }
        else
        {
            consecutiveDoublesCount = 0;
            canRollAgain = false;
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
        
        // Get the distance from the dice roll
        int moveDistance = DiceManager.GetDiceSum(lastDiceRoll);
        
        // For now, assume the move is valid and place the chip
        // Full board interaction will be enhanced in later sprints
        lastMovedToCell = cellIndex;
        
        // Check if bumping is possible at this cell
        bool canBumpAtCell = boardModel.CanBump(currentPlayer, cellIndex);
        
        if (canBumpAtCell)
        {
            TransitionPhase(GamePhase.Bumping);
        }
        else
        {
            TransitionPhase(GamePhase.EndTurn);
        }
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
        
        // Execute the bump
        boardModel.ApplyBump(currentPlayer, cellIndex);
        
        TransitionPhase(GamePhase.EndTurn);
    }
    
    /// <summary>
    /// Skip bumping and advance to end turn.
    /// </summary>
    public void SkipBump()
    {
        if (currentPhase != GamePhase.Bumping)
        {
            OnInvalidAction?.Invoke("Cannot skip bump in current phase");
            return;
        }
        
        TransitionPhase(GamePhase.EndTurn);
    }
    
    /// <summary>
    /// End current turn and transition to next player.
    /// </summary>
    public void EndTurn()
    {
        if (currentPhase != GamePhase.Placing && 
            currentPhase != GamePhase.Bumping && 
            currentPhase != GamePhase.Rolling)
        {
            OnInvalidAction?.Invoke("Cannot end turn in current phase");
            return;
        }
        
        // Check if current player can roll again (doubles)
        if (canRollAgain)
        {
            canRollAgain = false;
            TransitionPhase(GamePhase.Rolling);
            return;
        }
        
        // Advance to next player
        turnNumber++;
        turnManager.AdvanceTurn();
        currentPlayer = turnManager.CurrentPlayer;
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
            
        return boardModel.Check5InARow(player);
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
        
        currentPhase = newPhase;
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
