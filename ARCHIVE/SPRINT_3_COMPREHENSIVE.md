# Sprint 3: Game Modes Architecture
**Comprehensive Briefing Package**

**Sprint Duration**: Week 3 (Nov 28 - Dec 5, 2025)  
**Lead**: Gameplay Engineer Agent  
**Managing Engineer**: Amp (active oversight)  
**Status**: 📋 Ready to commence Nov 28

---

## Executive Summary

Sprint 3 implements all 5 game modes that plug into the GameStateManager from Sprint 2. Each mode implements the `IGameMode` interface and defines its own rules for scoring, winning, bumping, and roll-again conditions.

By Dec 5, all 5 modes will be fully playable and testable in code.

**Deliverables**: 5 game mode classes + 1 factory + 40+ unit tests  
**Estimated LOC**: ~1,500 lines  
**Success Criteria**: All modes pass spec tests, factory works, zero logic bugs

---

## What We're Building

### Core Concept

Each game mode implements `IGameMode` interface:

```csharp
public interface IGameMode
{
    int GameModeID { get; }
    string GameModeName { get; }
    
    // Called by GameStateManager when move completes
    bool CheckWin(Player player, BoardModel board);
    
    // Mode-specific rules
    bool CanRollAgain(int[] diceRoll, int consecutiveDoubles);
    bool MustBump(Chip movedChip);
    int CalculateScore(Player player, BoardModel board);
}
```

### The 5 Game Modes

1. **Game1_Bump5** - Classic 5-in-a-row
2. **Game2_Krazy6** - Anything goes with 6
3. **Game3_PassTheChip** - Team play with passing
4. **Game4_BumpUAnd5** - Score-based win (20 points)
5. **Game5_Solitary** - Single-player with bonuses

### GameModeFactory

Simple factory for creating modes:

```csharp
public class GameModeFactory
{
    public static IGameMode CreateGameMode(int gameModeID)
    {
        return gameModeID switch
        {
            1 => new Game1_Bump5(),
            2 => new Game2_Krazy6(),
            3 => new Game3_PassTheChip(),
            4 => new Game4_BumpUAnd5(),
            5 => new Game5_Solitary(),
            _ => throw new ArgumentException($"Unknown mode: {gameModeID}")
        };
    }
}
```

---

## Deliverables (In Order)

### 1. IGameMode.cs (Interface, 30 lines)

**Purpose**: Define contract all game modes must implement

```csharp
public interface IGameMode
{
    /// <summary>Game mode ID (1-5)</summary>
    int GameModeID { get; }
    
    /// <summary>Human-readable name (e.g., "Bump 5", "Krazy 6")</summary>
    string GameModeName { get; }
    
    /// <summary>
    /// Check if player has won. Called after every move/bump.
    /// </summary>
    /// <param name="player">Player to check</param>
    /// <param name="board">Current board state</param>
    /// <returns>True if player has won</returns>
    bool CheckWin(Player player, BoardModel board);
    
    /// <summary>
    /// Determine if player can roll again (mode-specific rule).
    /// </summary>
    /// <param name="diceRoll">The dice result</param>
    /// <param name="consecutiveDoubles">Count of consecutive doubles</param>
    /// <returns>True if player can roll again</returns>
    bool CanRollAgain(int[] diceRoll, int consecutiveDoubles);
    
    /// <summary>
    /// Check if bumping is required (some modes skip it).
    /// </summary>
    /// <param name="movedChip">The chip that just moved</param>
    /// <returns>True if bump is mandatory</returns>
    bool MustBump(Chip movedChip);
    
    /// <summary>
    /// Calculate player's score (mode-specific scoring).
    /// </summary>
    /// <param name="player">Player to score</param>
    /// <param name="board">Current board state</param>
    /// <returns>Current score for player</returns>
    int CalculateScore(Player player, BoardModel board);
}
```

**Requirements**:
- ✅ Clear contract for all modes
- ✅ All methods fully documented
- ✅ Parameter and return types clear

**Testing**: None (interface used in mode tests)

---

### 2. Game1_Bump5.cs (Basic 5-in-a-row, 80 lines)

**Purpose**: Classic game mode - win with 5 chips in a row

```csharp
public class Game1_Bump5 : IGameMode
{
    public int GameModeID => 1;
    public string GameModeName => "Bump 5";
    
    public bool CheckWin(Player player, BoardModel board)
    {
        // Check if player has 5 chips in any direction
        // (horizontal, vertical, diagonal)
        // Return true if 5 found
    }
    
    public bool CanRollAgain(int[] diceRoll, int consecutiveDoubles)
    {
        // Standard rule: any double allows roll again
        // 3 consecutive doubles = lose turn
        int sum = diceRoll[0] + diceRoll[1];
        if (diceRoll[0] == diceRoll[1])  // Is it a double?
        {
            if (consecutiveDoubles >= 3) return false;
            return true;
        }
        return false;
    }
    
    public bool MustBump(Chip movedChip)
    {
        // Bumping is optional in Bump5
        return false;
    }
    
    public int CalculateScore(Player player, BoardModel board)
    {
        // Score = number of chips on board
        return board.GetChipsForPlayer(player).Count;
    }
}
```

**Win Condition**: 5 chips in any straight line (horizontal, vertical, diagonal)

**Roll Again**: Any double (except 3 doubles in a row)

**Bumping**: Optional

**Score**: Number of chips on board

**Testing** (8+ tests):
- `CheckWin_With5InARow_ReturnsTrue()`
- `CheckWin_With4InARow_ReturnsFalse()`
- `CheckWin_Horizontal_ReturnsTrue()`
- `CheckWin_Vertical_ReturnsTrue()`
- `CheckWin_Diagonal_ReturnsTrue()`
- `CanRollAgain_WithDouble_ReturnsTrue()`
- `CanRollAgain_TripleDouble_ReturnsFalse()`
- `MustBump_ReturnsFalse()`

---

### 3. Game2_Krazy6.cs (Anything with 6, 100 lines)

**Purpose**: Any roll with 6 wins immediately

```csharp
public class Game2_Krazy6 : IGameMode
{
    public int GameModeID => 2;
    public string GameModeName => "Krazy 6";
    
    public bool CheckWin(Player player, BoardModel board)
    {
        // Note: In Krazy 6, win happens when rolling 6, not by board state
        // This should return true if player has any valid chips on board
        return board.GetChipsForPlayer(player).Count > 0;
    }
    
    // Override CheckWin to also handle immediate 6-roll win
    public bool WinsOnRoll(int[] diceRoll)
    {
        // Any die showing 6 = immediate win
        return diceRoll.Contains(6);
    }
    
    public bool CanRollAgain(int[] diceRoll, int consecutiveDoubles)
    {
        // Any 6 = roll again
        return diceRoll.Contains(6);
    }
    
    public bool MustBump(Chip movedChip)
    {
        // No bumping in Krazy 6
        return false;
    }
    
    public int CalculateScore(Player player, BoardModel board)
    {
        // Score = number of 6s rolled (tracked elsewhere)
        return board.GetChipsForPlayer(player).Count;
    }
}
```

**Win Condition**: Roll a 6 on either die

**Roll Again**: Any 6

**Bumping**: Not allowed (skip entirely)

**Special**: GameStateManager must call `WinsOnRoll()` during Rolling phase

**Testing** (8+ tests):
- `WinsOnRoll_With6_ReturnsTrue()`
- `WinsOnRoll_WithoutSix_ReturnsFalse()`
- `CanRollAgain_With6_ReturnsTrue()`
- `CanRollAgain_Without6_ReturnsFalse()`
- `MustBump_ReturnsFalse()`

---

### 4. Game3_PassTheChip.cs (Team play, 120 lines)

**Purpose**: Team-based mode where players pass chips

```csharp
public class Game3_PassTheChip : IGameMode
{
    private Dictionary<int, int> playerTeams;  // player index -> team ID
    
    public int GameModeID => 3;
    public string GameModeName => "Pass The Chip";
    
    public void AssignTeams(int[] teamAssignments)
    {
        // teamAssignments[playerIndex] = teamID (0 or 1)
        for (int i = 0; i < teamAssignments.Length; i++)
        {
            playerTeams[i] = teamAssignments[i];
        }
    }
    
    public bool CheckWin(Player player, BoardModel board)
    {
        // Win when team gets 5 in a row
        int team = playerTeams[FindPlayerIndex(player)];
        return HasTeamWon(board, team);
    }
    
    private bool HasTeamWon(BoardModel board, int team)
    {
        // Check for 5 in a row belonging to ANY player on this team
    }
    
    public bool CanRollAgain(int[] diceRoll, int consecutiveDoubles)
    {
        // Double = roll again (standard rule)
        return ItsADouble(diceRoll) && consecutiveDoubles < 3;
    }
    
    public bool MustBump(Chip movedChip)
    {
        // Can bump opponent team's chips
        return false;  // Optional
    }
    
    public int CalculateScore(Player player, BoardModel board)
    {
        // Score = team's total chips on board
        int team = playerTeams[FindPlayerIndex(player)];
        return GetTeamChipCount(board, team);
    }
}
```

**Win Condition**: Any team member gets 5 in a row

**Roll Again**: Any double (standard)

**Bumping**: Optional, can bump opponent team

**Special**: Team assignments configured before game

**Testing** (8+ tests):
- `AssignTeams_StoresCorrectly()`
- `CheckWin_TeamHas5_ReturnsTrue()`
- `CheckWin_OtherTeamHas5_ReturnsFalse()`
- `CanRollAgain_WithDouble_ReturnsTrue()`
- `CalculateScore_TeamScore_ReturnsTotal()`

---

### 5. Game4_BumpUAnd5.cs (Score-based win, 100 lines)

**Purpose**: Win by reaching 20 points through bumping

```csharp
public class Game4_BumpUAnd5 : IGameMode
{
    public int GameModeID => 4;
    public string GameModeName => "Bump U And 5";
    
    public bool CheckWin(Player player, BoardModel board)
    {
        // Win when player reaches 20 points
        return CalculateScore(player, board) >= 20;
    }
    
    public bool CanRollAgain(int[] diceRoll, int consecutiveDoubles)
    {
        // Only 5s allow roll again in this mode
        int sum = diceRoll[0] + diceRoll[1];
        return sum == 5 && consecutiveDoubles < 3;
    }
    
    public bool MustBump(Chip movedChip)
    {
        // Bumping is mandatory if possible
        return true;
    }
    
    public int CalculateScore(Player player, BoardModel board)
    {
        // Score = (chips on board × 2) + (bumps × 5)
        int chipScore = board.GetChipsForPlayer(player).Count * 2;
        int bumpScore = player.BumpCount * 5;  // Assume Player tracks bumps
        return chipScore + bumpScore;
    }
}
```

**Win Condition**: Reach 20 points (chips×2 + bumps×5)

**Roll Again**: Sum = 5 only

**Bumping**: Mandatory if possible

**Special**: Score calculation includes bumps

**Testing** (8+ tests):
- `CheckWin_Score20Plus_ReturnsTrue()`
- `CheckWin_ScoreLess20_ReturnsFalse()`
- `CanRollAgain_Sum5_ReturnsTrue()`
- `CanRollAgain_OtherSum_ReturnsFalse()`
- `MustBump_ReturnsTrue()`
- `CalculateScore_IncludesBumpBonus()`

---

### 6. Game5_Solitary.cs (Single-player, 100 lines)

**Purpose**: Single-player mode with bonuses and time challenges

```csharp
public class Game5_Solitary : IGameMode
{
    public int GameModeID => 5;
    public string GameModeName => "Solitary";
    
    public bool CheckWin(Player player, BoardModel board)
    {
        // Win when:
        // 1. 5 chips on board, AND
        // 2. All moves completed in under 5 minutes (example)
        // 3. OR reached 50 points
        return CalculateScore(player, board) >= 50;
    }
    
    public bool CanRollAgain(int[] diceRoll, int consecutiveDoubles)
    {
        // In solitary, any roll is allowed (no limit)
        return true;  // Always allow another roll
    }
    
    public bool MustBump(Chip movedChip)
    {
        // No bumping in single-player
        return false;
    }
    
    public int CalculateScore(Player player, BoardModel board)
    {
        // Score = (chips × 5) + bonuses
        int chipScore = board.GetChipsForPlayer(player).Count * 5;
        int bonusScore = CalculateSpeedBonus(player);
        return chipScore + bonusScore;
    }
    
    private int CalculateSpeedBonus(Player player)
    {
        // Bonus points for quick moves (higher bonus for faster)
        return 0;  // Implement as needed
    }
}
```

**Win Condition**: Reach 50 points (no opponents, speed bonus)

**Roll Again**: Always (no limit)

**Bumping**: None (single player)

**Special**: Speed-based bonuses

**Testing** (7+ tests):
- `CheckWin_Score50Plus_ReturnsTrue()`
- `CanRollAgain_AlwaysTrue()`
- `MustBump_ReturnsFalse()`
- `CalculateScore_IncludesBonuses()`

---

### 7. GameModeFactory.cs (Helper, 20 lines)

**Purpose**: Simple factory for creating modes

```csharp
public class GameModeFactory
{
    /// <summary>Create a game mode by ID</summary>
    /// <param name="gameModeID">Mode ID (1-5)</param>
    /// <returns>IGameMode instance</returns>
    public static IGameMode CreateGameMode(int gameModeID)
    {
        return gameModeID switch
        {
            1 => new Game1_Bump5(),
            2 => new Game2_Krazy6(),
            3 => new Game3_PassTheChip(),
            4 => new Game4_BumpUAnd5(),
            5 => new Game5_Solitary(),
            _ => throw new ArgumentException($"Unknown game mode: {gameModeID}")
        };
    }
}
```

**Requirements**:
- ✅ Clear switch statement
- ✅ Throws for invalid IDs
- ✅ Documented

**Testing** (2+ tests):
- `CreateGameMode_ValidID_ReturnsMode()`
- `CreateGameMode_InvalidID_ThrowsException()`

---

## File Structure

```
/Assets/Scripts/GameModes/
  IGameMode.cs                    (NEW - interface)
  Game1_Bump5.cs                  (NEW)
  Game2_Krazy6.cs                 (NEW)
  Game3_PassTheChip.cs            (NEW)
  Game4_BumpUAnd5.cs              (NEW)
  Game5_Solitary.cs               (NEW)
  GameModeFactory.cs              (NEW)

/Assets/Scripts/Tests/
  Game1_Bump5Tests.cs             (NEW - 8+ tests)
  Game2_Krazy6Tests.cs            (NEW - 8+ tests)
  Game3_PassTheChipTests.cs       (NEW - 8+ tests)
  Game4_BumpUAnd5Tests.cs         (NEW - 8+ tests)
  Game5_SolitaryTests.cs          (NEW - 7+ tests)
  GameModeFactoryTests.cs         (NEW - 2+ tests)
```

---

## Testing Strategy

### Test Organization

Each mode gets its own test file with 7-8 tests focusing on:
- `CheckWin()` behavior
- `CanRollAgain()` logic
- `MustBump()` rules
- `CalculateScore()` accuracy

### Expected Results

22+ new tests + 57 Sprint 1 + 22 Sprint 2 = 101 total tests  
**Target**: 100% pass rate

---

## Edge Cases Per Mode

### Game1_Bump5
- ✅ Diagonals (up-left, up-right, down-left, down-right)
- ✅ Edge pieces (doesn't wrap around)
- ✅ Triple-double rule (3+ doubles = lose turn)

### Game2_Krazy6
- ✅ Either die showing 6 = win
- ✅ 6+6 = double (roll again)
- ✅ No bumping interaction

### Game3_PassTheChip
- ✅ Team assignments validated
- ✅ Team member wins count for whole team
- ✅ Score aggregates across team

### Game4_BumpUAnd5
- ✅ Score includes both chips and bumps
- ✅ Only 5s trigger roll again
- ✅ Bump is mandatory

### Game5_Solitary
- ✅ Single player (no bumping)
- ✅ Speed bonuses (if implemented)
- ✅ No turn rotation

---

## Code Standards Checklist

- [ ] All classes follow PascalCase
- [ ] All public methods documented with `/// <summary>`
- [ ] IGameMode interface fully implemented by each class
- [ ] GameModeFactory switch statement is complete
- [ ] All edge cases handled
- [ ] No magic numbers (use constants)
- [ ] All 40+ tests pass
- [ ] No circular dependencies
- [ ] Integration with Sprint 2 GameStateManager verified
- [ ] No console errors or warnings

---

## Performance Targets

- `CheckWin()`: < 10ms (may require board search)
- `CanRollAgain()`: < 1ms
- `MustBump()`: < 1ms
- `CalculateScore()`: < 5ms

---

## Daily Standup Template

Each day:
- ✅ Completed modes
- 🔄 In progress
- 🚫 Blockers

**Example**:
```
✅ IGameMode interface created
✅ Game1_Bump5 & Game2_Krazy6 implemented
🔄 Game3_PassTheChip (team assignment logic)
🚫 None
```

---

## Key Dates

| Date | Event |
|------|-------|
| Nov 28 (Thu) | Sprint 3 Kickoff |
| Nov 29-30 | Implement modes 1-2 |
| Dec 1-2 | Implement modes 3-4 |
| Dec 3 | Implement mode 5 & factory |
| Dec 4 | Testing & bug fixes |
| Dec 5 (Thu) | Sprint 3 complete, code review |

---

## Success Criteria

- ✅ All 5 game modes implemented
- ✅ GameModeFactory working
- ✅ All 40+ tests passing
- ✅ No logic bugs in mode rules
- ✅ Code reviewed and approved
- ✅ Integration with GameStateManager verified
- ✅ Sprint 1 & 2 tests still passing (no regressions)
- ✅ All modes playable end-to-end

---

## Next: Sprint 4 Preview

Once Sprint 3 is approved, Sprint 4 begins Board Visualization:
- Interactive board rendering in Unity
- Valid move highlighting
- Chip placement & animation
- Touch/mouse input handling

---

**Prepared By**: Managing Engineer (Amp)  
**Date**: Nov 14, 2025  
**Status**: 📋 Ready for Nov 28 Kickoff
