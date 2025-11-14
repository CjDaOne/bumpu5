# SPRINT 3 DETAILED BRIEFING
## Game Modes Implementation (Gameplay Engineer Agent)

**Issued By**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025  
**Duration**: 7 days (Nov 14-21)  
**Target**: Complete all 5 game modes + 40+ tests  

---

## OVERVIEW

Sprint 3 transforms the core game state machine into 5 distinct game experiences. Each game mode modifies rules, special cases, and win conditions while reusing the same underlying state machine and board logic.

**High Level**:
1. Define `IGameMode` interface (1 file)
2. Implement 5 concrete game modes (5 files)
3. Test all modes thoroughly (1 test file, 40+ tests)
4. Integrate with GameStateManager
5. Code review and approval

**Success**: All 5 modes playable end-to-end with zero critical bugs.

---

## PART 1: IGameMode INTERFACE

**File**: `Assets/Scripts/Modes/IGameMode.cs`  
**Type**: Interface definition  
**Purpose**: Define contract for all game modes  

### Interface Definition

```csharp
using System;

/// <summary>
/// Interface for game mode implementations.
/// Each game mode can override rules, win conditions, and special cases.
/// </summary>
public interface IGameMode
{
    // ============================================
    // METADATA PROPERTIES
    // ============================================
    
    /// <summary>Gets the short name of the game mode (e.g., "Bump5", "Krazy6")</summary>
    string ModeName { get; }
    
    /// <summary>Gets the long description (e.g., "Bump 5 in a Row")</summary>
    string ModeLongName { get; }
    
    /// <summary>Gets the maximum number of players for this mode</summary>
    int MaxPlayers { get; }
    
    // ============================================
    // RULE CONFIGURATION PROPERTIES
    // ============================================
    
    /// <summary>Does this mode use 5-in-a-row for win detection?</summary>
    bool Use5InARow { get; }
    
    /// <summary>Should rolling 3 doubles in a row cause loss of turn?</summary>
    bool UseTripleDoublesPenalty { get; }
    
    /// <summary>Is rolling 5+6 a "safe" roll (no placement)?</summary>
    bool Use5Plus6Safe { get; }
    
    /// <summary>Does rolling a 6 lose the turn?</summary>
    bool RollingASixLosesTurn { get; }
    
    /// <summary>Can opponent chips be bumped?</summary>
    bool AllowBumping { get; }
    
    // ============================================
    // WIN CONDITION CHECK
    // ============================================
    
    /// <summary>
    /// Check if the given player has met the win condition for this mode.
    /// </summary>
    /// <param name="player">Player to check</param>
    /// <param name="board">Board state to check against</param>
    /// <returns>True if player has won</returns>
    bool CheckWinCondition(Player player, BoardModel board);
    
    // ============================================
    // SPECIAL RULES HOOKS
    // ============================================
    
    /// <summary>
    /// Called at the start of each turn.
    /// Allows mode to apply any special initialization logic.
    /// </summary>
    void OnTurnStart(GameStateManager stateManager, Player currentPlayer);
    
    /// <summary>
    /// Called after dice are rolled.
    /// Allows mode to modify how the roll is interpreted.
    /// </summary>
    /// <param name="stateManager">Current game state</param>
    /// <param name="rollResult">Dice roll (can be modified)</param>
    void OnDiceRolled(GameStateManager stateManager, int[] rollResult);
    
    /// <summary>
    /// Called when a chip is about to be placed.
    /// Allows mode to validate or modify placement.
    /// </summary>
    /// <param name="stateManager">Current game state</param>
    /// <param name="targetCell">Cell where chip will be placed</param>
    /// <returns>True if placement is allowed</returns>
    bool CanPlaceChip(GameStateManager stateManager, int targetCell);
    
    /// <summary>
    /// Called when bumping is about to occur.
    /// Allows mode to override bump behavior (swap instead of remove, etc).
    /// </summary>
    /// <param name="stateManager">Current game state</param>
    /// <param name="bumperPlayer">Player doing the bumping</param>
    /// <param name="targetCell">Cell with opponent chip</param>
    /// <returns>True if bump should be applied</returns>
    bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell);
    
    /// <summary>
    /// Called when turn ends.
    /// Allows mode to apply any special end-of-turn logic.
    /// </summary>
    void OnTurnEnd(GameStateManager stateManager, Player currentPlayer);
    
    // ============================================
    // INITIALIZATION
    // ============================================
    
    /// <summary>
    /// Called when game starts with this mode.
    /// Allows mode to initialize any special state.
    /// </summary>
    void Initialize(GameStateManager stateManager);
}
```

### Why This Interface?

✅ **Extensible**: Easy to add new game modes in future  
✅ **Flexible**: Hooks for all major decision points  
✅ **Type-Safe**: Compile-time checking  
✅ **Testable**: Each mode independently testable  
✅ **Simple**: Only necessary methods, no bloat  

---

## PART 2: GAME MODE IMPLEMENTATIONS

### GAME 1: Bump 5 (Standard Game)

**File**: `Assets/Scripts/Modes/Game1_Bump5.cs`  
**Complexity**: Medium (baseline game)  

#### Rules Summary
```
- Win: 5 chips in a row (horizontally or vertically on 12-cell board)
- Rolling a 6: LOSE TURN (no placement)
- 5+6 combo: SAFE ROLL (no placement, no turn loss)
- Doubles: Get to place AND roll again
- Triple doubles: Lose turn (penalty)
- Bumping: Remove opponent chip from board
- First to 5-in-a-row wins game
```

#### Class Structure
```csharp
public class Game1_Bump5 : IGameMode
{
    // Properties
    public string ModeName => "Bump5";
    public string ModeLongName => "Bump 5 in a Row";
    public int MaxPlayers => 2;
    public bool Use5InARow => true;
    public bool UseTripleDoublesPenalty => true;
    public bool Use5Plus6Safe => true;
    public bool RollingASixLosesTurn => true;
    public bool AllowBumping => true;
    
    // Methods
    public bool CheckWinCondition(Player player, BoardModel board);
    public void OnTurnStart(GameStateManager stateManager, Player currentPlayer);
    public void OnDiceRolled(GameStateManager stateManager, int[] rollResult);
    public bool CanPlaceChip(GameStateManager stateManager, int targetCell);
    public bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell);
    public void OnTurnEnd(GameStateManager stateManager, Player currentPlayer);
    public void Initialize(GameStateManager stateManager);
}
```

#### Implementation Details

**CheckWinCondition**:
```csharp
bool CheckWinCondition(Player player, BoardModel board)
{
    // Delegate to BoardModel's 5-in-a-row detection
    return board.Check5InARow(player);
}
```

**OnBumpAttempt**:
```csharp
bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell)
{
    // Standard bump: remove opponent chip
    // This should return true to allow GameStateManager to execute bump
    return true;
}
```

**Other hooks**: Pass-through (return early, do nothing)

#### Unit Tests (5+ tests)
```csharp
[Test] public void Game1_ModeName_IsCorrect()
[Test] public void Game1_MaxPlayers_IsTwo()
[Test] public void Game1_Win_Requires5InARow()
[Test] public void Game1_Rolling6_LosesTurn()
[Test] public void Game1_Bumping_RemovesOpponentChip()
```

---

### GAME 2: Krazy 6

**File**: `Assets/Scripts/Modes/Game2_Krazy6.cs`  
**Complexity**: Medium (modified rules)  

#### Rules Summary
```
- Win: 5 chips in a row (same as Bump5)
- Rolling a 6: GOOD ROLL! (don't lose turn, bonus)
- Double 6s: Extra bonus movement
- 5+6: Still a safe roll (but 6 is good, so different interpretation)
- Doubles: Roll again (like Bump5)
- Triple doubles: Still lose turn
- Bumping: Yes, remove opponent chip
- First to 5-in-a-row wins
```

#### Key Difference
In standard Bump5, rolling a 6 loses your turn. In Krazy 6, rolling a 6 is actually good luck - you get to place AND get a bonus.

#### Class Structure
```csharp
public class Game2_Krazy6 : IGameMode
{
    public string ModeName => "Krazy6";
    public string ModeLongName => "Krazy 6";
    public bool RollingASixLosesTurn => false;  // KEY DIFFERENCE
    
    // Override rolling logic
    public void OnDiceRolled(GameStateManager stateManager, int[] rollResult);
    
    // Rest inherited from base behavior
}
```

#### Implementation Details

**OnDiceRolled**:
```csharp
void OnDiceRolled(GameStateManager stateManager, int[] rollResult)
{
    // Check if we rolled any 6s
    // If single 6: Award bonus movement/token
    // If double 6: Award extra bonus
    
    // Note: This hook doesn't change the actual roll,
    // but allows the mode to apply bonuses
}
```

**How This Works**:
- GameStateManager.RollDice() calls OnDiceRolled hook
- Mode can apply bonuses without changing GameStateManager logic
- GameStateManager's existing 6-penalty logic is disabled for this mode
  (via RollingASixLosesTurn property)

#### Unit Tests (5+ tests)
```csharp
[Test] public void Game2_Rolling6_GivesBonus()
[Test] public void Game2_Double6_ExtraBonus()
[Test] public void Game2_Still5InARow_ToWin()
[Test] public void Game2_TripleDoubles_StillLoseTurn()
[Test] public void Game2_Bumping_StillWorks()
```

---

### GAME 3: Pass The Chip

**File**: `Assets/Scripts/Modes/Game3_PassTheChip.cs`  
**Complexity**: Medium-High (swap logic)  

#### Rules Summary
```
- Win: 5 chips in a row
- Rolling a 6: Lose turn (like Bump5)
- 5+6: Safe roll
- Doubles: Roll again
- Triple doubles: Lose turn
- Bumping: NO - instead, SWAP positions with opponent's chip
- First to 5-in-a-row wins
```

#### Key Feature: Swap Instead of Bump
When you land on an opponent's chip, instead of removing it, you swap positions:
- Your chip moves to cell X (where opponent was)
- Opponent's chip moves to cell Y (where your chip came from)

This creates interesting strategic play.

#### Class Structure
```csharp
public class Game3_PassTheChip : IGameMode
{
    public string ModeName => "PassTheChip";
    public string ModeLongName => "Pass The Chip";
    public bool AllowBumping => false;  // No bumping in this mode
    
    // Override bump logic to swap instead
    public bool OnBumpAttempt(...);
    
    // Helper to execute swap
    private void SwapChips(Player player1, int cell1, Player player2, int cell2);
}
```

#### Implementation Details

**OnBumpAttempt** (the key difference):
```csharp
bool OnBumpAttempt(GameStateManager stateManager, Player bumperPlayer, int targetCell)
{
    // Instead of removing the opponent chip, swap positions
    // Bumper's chip moves to targetCell
    // Opponent's chip moves back to where bumper came from
    
    SwapChips(bumperPlayer, targetCell, opponentPlayer, previousCell);
    
    // Return false to prevent GameStateManager from applying standard bump
    return false;
}
```

**Why Return False?**
- Returning false tells GameStateManager: "I handled the bump, don't do the default"
- GameStateManager can see mode returned false and skip ApplyBump()
- This requires coordination with GameStateManager design

#### Unit Tests (6+ tests)
```csharp
[Test] public void Game3_Swap_MovesYourChipToOpponentCell()
[Test] public void Game3_Swap_MovesOpponentChipToYourPreviousCell()
[Test] public void Game3_NoBumping_ChipsStayOnBoard()
[Test] public void Game3_StillRolling6Loses()
[Test] public void Game3_Win_Still5InARow()
[Test] public void Game3_SwapEvent_Fires()
```

---

### GAME 4: Bump U & 5

**File**: `Assets/Scripts/Modes/Game4_BumpUAnd5.cs`  
**Complexity**: High (hybrid mode)  

#### Rules Summary
```
- Combines Bump5 + Krazy6
- Rolling a 6: GOOD (from Krazy6)
- Double 6s: Extra bonus movement (from Krazy6)
- 5+6: Safe roll (from Bump5)
- Doubles: Roll again
- Triple doubles: Lose turn
- Bumping: Remove opponent chip (from Bump5)
- Win: 5 in a row
```

#### Why This Mode?
Players who like both modes can play them together!

#### Class Structure
```csharp
public class Game4_BumpUAnd5 : IGameMode
{
    public string ModeName => "BumpUAnd5";
    public string ModeLongName => "Bump U & 5";
    public bool RollingASixLosesTurn => false;  // From Krazy6
    public bool AllowBumping => true;           // From Bump5
    
    // Inherit Krazy6's rolling logic
    public void OnDiceRolled(GameStateManager stateManager, int[] rollResult);
    
    // Inherit Bump5's bump logic
    public bool OnBumpAttempt(...);
}
```

#### Implementation Details

**Conceptually**:
- Take favorable rules from Game2 (Krazy6)
- Keep bumping from Game1 (Bump5)
- Result: A fast-paced game with rolling bonuses AND bumping

This is mostly configuration-based (setting properties correctly).

#### Unit Tests (8+ tests)
```csharp
[Test] public void Game4_Hybrid_Rolling6IsGood()
[Test] public void Game4_Hybrid_BumpingRemovesChip()
[Test] public void Game4_Hybrid_AllFeaturesCombine()
[Test] public void Game4_Win_5InARow()
[Test] public void Game4_Doubles_RollAgain()
[Test] public void Game4_TripleDoubles_LoseTurn()
[Test] public void Game4_SafeRoll_5Plus6()
[Test] public void Game4_GameFlow_CompleteRound()
```

---

### GAME 5: Solitary

**File**: `Assets/Scripts/Modes/Game5_Solitary.cs`  
**Complexity**: Medium-High (single player)  

#### Rules Summary
```
- Single player mode
- Goal: Place chips to create 5-in-a-row as fast as possible
- Rolling: Normal dice rolls
- Win: Get 5 in a row
- Tracking: Best time, fewest rolls, etc.
- No opponent bumping
```

#### Why This Mode?
Puzzle mode - relax and try to achieve 5-in-a-row. Can be timed.

#### Class Structure
```csharp
public class Game5_Solitary : IGameMode
{
    public string ModeName => "Solitary";
    public string ModeLongName => "Solitary Puzzle";
    public int MaxPlayers => 1;  // Single player only
    
    // Track stats
    private int rollCount;
    private DateTime startTime;
    
    public void Initialize(GameStateManager stateManager);
    public bool CheckWinCondition(Player player, BoardModel board);
    public void OnTurnEnd(GameStateManager stateManager, Player currentPlayer);
    
    // Properties
    public int RollCount { get; }
    public TimeSpan ElapsedTime { get; }
}
```

#### Implementation Details

**Initialize**:
```csharp
void Initialize(GameStateManager stateManager)
{
    rollCount = 0;
    startTime = DateTime.Now;
}
```

**OnTurnEnd**:
```csharp
void OnTurnEnd(GameStateManager stateManager, Player currentPlayer)
{
    // No player rotation in solitary
    // Game loops for single player until win
}
```

**Features**:
- No opponent chips (only one player)
- Can't bump (no opponents)
- Timer tracks how long it takes
- Roll counter tracks efficiency

#### Unit Tests (5+ tests)
```csharp
[Test] public void Game5_SinglePlayer_MaxPlayersIsOne()
[Test] public void Game5_Win_5InARow_SinglePlayer()
[Test] public void Game5_TracksRollCount()
[Test] public void Game5_TracksElapsedTime()
[Test] public void Game5_NoBumping_SinglePlayer()
```

---

## PART 3: GAMEMODE FACTORY (Helper)

**File**: `Assets/Scripts/Modes/GameModeFactory.cs`  
**Purpose**: Create game modes by ID  

### Simple Factory Pattern

```csharp
public static class GameModeFactory
{
    public static IGameMode CreateGameMode(int modeId)
    {
        return modeId switch
        {
            1 => new Game1_Bump5(),
            2 => new Game2_Krazy6(),
            3 => new Game3_PassTheChip(),
            4 => new Game4_BumpUAnd5(),
            5 => new Game5_Solitary(),
            _ => throw new ArgumentException("Unknown game mode ID")
        };
    }
}
```

### Usage
```csharp
IGameMode mode = GameModeFactory.CreateGameMode(1);  // Creates Bump5
```

---

## PART 4: INTEGRATION WITH GAMESTATE MANAGER

### How GameStateManager Uses IGameMode

The GameStateManager needs to call mode hooks at key points:

**1. During Initialization**:
```csharp
public void Initialize(IGameMode gameMode, Player player1, Player player2)
{
    // ... existing init code ...
    this.gameMode = gameMode;
    gameMode.Initialize(this);
}
```

**2. During Roll**:
```csharp
public void RollDice()
{
    // ... existing roll code ...
    lastDiceRoll = diceManager.RollTwoDice();
    gameMode.OnDiceRolled(this, lastDiceRoll);  // Hook
    // ... rest of roll logic ...
}
```

**3. During Bump**:
```csharp
public void BumpOpponentChip(int cellIndex)
{
    // ... validation ...
    if (gameMode.OnBumpAttempt(this, currentPlayer, cellIndex))
    {
        boardModel.ApplyBump(currentPlayer, cellIndex);  // Standard bump
    }
    TransitionPhase(GamePhase.EndTurn);
}
```

**4. During Win Check**:
```csharp
public bool HasWon(Player player)
{
    return gameMode.CheckWinCondition(player, boardModel);
}
```

---

## PART 5: TESTING STRATEGY

### Test File: GameModeTests.cs

**Organization**:
```
Setup
├─ Game1 Tests (5+ tests)
├─ Game2 Tests (5+ tests)
├─ Game3 Tests (5+ tests)
├─ Game4 Tests (8+ tests)
├─ Game5 Tests (5+ tests)
└─ Interface Tests (3+ tests)
```

### Test Categories

**1. Mode Metadata Tests**
```csharp
[Test] public void Mode_Name_IsCorrect() { }
[Test] public void Mode_MaxPlayers_IsValid() { }
[Test] public void Mode_Properties_AreConsistent() { }
```

**2. Win Condition Tests**
```csharp
[Test] public void Mode_WinCondition_DetectsCorrectly() { }
[Test] public void Mode_WinCondition_ReturnsFalseWhenNotWon() { }
[Test] public void Mode_WinCondition_FiresEvent() { }
```

**3. Special Rule Tests**
```csharp
[Test] public void Game2_Rolling6_DoesNotLoseTurn() { }
[Test] public void Game3_Landing_TriggersSwap() { }
[Test] public void Game5_SinglePlayer_Works() { }
```

**4. Integration Tests**
```csharp
[Test] public void Mode_WithGameStateManager_IntegratesCorrectly() { }
[Test] public void Mode_FullGameFlow_Plays() { }
```

### Test Example (Game1_Bump5)

```csharp
[Test]
public void Game1_ModeName_IsCorrect()
{
    // Arrange
    IGameMode mode = new Game1_Bump5();
    
    // Act
    string name = mode.ModeName;
    
    // Assert
    Assert.AreEqual("Bump5", name);
}

[Test]
public void Game1_WinCondition_Requires5InARow()
{
    // Arrange
    var game = new Game1_Bump5();
    var board = new BoardModel(player1, player2);
    
    // Act - place chips in a row
    for (int i = 0; i < 5; i++)
    {
        board.Cells[i].PlaceChip(new Chip(player1));
    }
    
    // Assert
    Assert.IsTrue(game.CheckWinCondition(player1, board));
}
```

---

## CODE QUALITY STANDARDS

✅ **Documentation**: 100% public methods documented with /// comments  
✅ **Naming**: Clear, descriptive names (ModeName, CheckWinCondition)  
✅ **Single Responsibility**: Each mode implements ONE set of rules  
✅ **No Duplication**: Common logic in interface, game-specific in modes  
✅ **Testing**: Every mode has minimum 5 tests  
✅ **Integration**: Works seamlessly with GameStateManager  

---

## TIMELINE

**Day 1**: IGameMode interface + Game1_Bump5  
**Day 2**: Game2_Krazy6 + Game3_PassTheChip  
**Day 3**: Game4_BumpUAnd5 + Game5_Solitary  
**Day 4**: GameModeFactory + integration testing  
**Day 5**: GameModeTests.cs (all 40+ tests)  
**Day 6**: Bug fixes + edge case testing  
**Day 7**: Code review + final approval  

---

## SUCCESS CRITERIA

✅ All 5 game modes fully implemented  
✅ All 5 modes playable end-to-end  
✅ 40+ unit tests with 100% pass rate  
✅ IGameMode interface validates correctly  
✅ Code review approved by Managing Engineer  
✅ Zero critical bugs  
✅ 80%+ code coverage  

---

**READY TO BEGIN SPRINT 3?**

All information provided. All requirements clear. Proceed with implementation.

**Contact Managing Engineer with any questions: Amp**

---

*End of Briefing Document*
