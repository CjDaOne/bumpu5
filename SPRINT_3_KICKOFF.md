# Sprint 3 Kickoff - Game Modes Architecture (5 Modes)

**Sprint Duration**: Week 3 (Nov 28 - Dec 5, 2025)  
**Lead**: Gameplay Engineer Agent  
**Dependency**: Sprint 1 & 2 complete & approved  
**Goal**: Implement all 5 game modes with custom rules, scoring, and win conditions  

---

## What We're Building

The 5 distinct game modes, each with different rules, scoring, and win conditions. All implement the `IGameMode` interface created in this sprint, allowing GameStateManager to work with any mode polymorphically.

By end of this sprint, all 5 game modes are fully playable through GameStateManager.

---

## Game Mode Specifications

### Mode 1: BUMP 5 (Classic)

**Description**: The classic Bump U Box game. First player to get 5 chips in a row wins.

**Rules**:
- Standard 2-player game
- Roll 2 dice each turn
- Move chip to adjacent cell
- Can bump opponent's chip off the board
- First to 5 in a row wins

**Scoring**:
- Bump: +10 points
- Win: +100 points

**Special Cases**:
- 5+6 roll: "Safe" - no bump possible from this roll
- Double roll: Player rolls again
- 6 on die: Does not lose turn (unlike some variants)

**IGameMode Implementation**:
```csharp
public class Game1_Bump5 : IGameMode
{
    public string ModeName => "Bump 5";
    public int ModeID => 0;
    
    public bool CheckWin(Player player, BoardModel board)
    {
        return board.Check5InARow(player);
    }
    
    public bool CanRollAgain(int[] diceRoll)
    {
        // Double roll → roll again
        return diceRoll.Length == 2 && diceRoll[0] == diceRoll[1];
    }
    
    public bool IsBumpAllowed(Player player, int targetCell, BoardModel board)
    {
        // Standard bump allowed
        return board.CanBump(player, targetCell);
    }
    
    // ... other methods ...
}
```

---

### Mode 2: KRAZY 6 (Wild Variant)

**Description**: A chaotic variant where rolling double-6 has special consequences.

**Rules**:
- Standard movement and bumping
- Roll 2 dice each turn
- First to 5 in a row wins

**Scoring**:
- Bump: +10 points
- Double-6 penalty: -50 points (and lose turn)
- Win: +100 points

**Special Cases**:
- Double-6 is a "Krazy" roll: Player loses 50 points and loses turn
- Other doubles: Normal roll-again rule
- 5+6: Still "safe" (no bump)

**IGameMode Implementation**:
```csharp
public class Game2_Krazy6 : IGameMode
{
    public string ModeName => "Krazy 6";
    public int ModeID => 1;
    
    public void OnDiceRolled(Player player, int[] diceRoll)
    {
        // Check for double-6 penalty
        if (diceRoll.Length == 2 && diceRoll[0] == 6 && diceRoll[1] == 6)
        {
            player.RemoveScore(50); // Penalty
            // Note: Turn loss handled by GameStateManager
        }
    }
    
    public bool CanRollAgain(int[] diceRoll)
    {
        // NO roll again for double-6 (it's a penalty)
        if (diceRoll.Length == 2 && diceRoll[0] == 6 && diceRoll[1] == 6)
            return false;
        
        // Other doubles: roll again
        return diceRoll.Length == 2 && diceRoll[0] == diceRoll[1];
    }
    
    // ... other methods ...
}
```

---

### Mode 3: PASS THE CHIP (Social)

**Description**: A cooperative/competitive hybrid where players pass a shared chip.

**Rules**:
- One "special" chip passes between players
- On your turn, you can move the special chip OR a regular chip
- Moving the special chip scores double points
- First to get the special chip 5 times wins

**Scoring**:
- Regular move: +5 points
- Special chip move: +10 points (doubled)
- Hold special chip: +1 per turn
- Win: +200 points

**Special Cases**:
- Special chip cannot be bumped (or bumps reset possession)
- Doubles: Player rolls again (standard)
- 5+6: Still safe

**IGameMode Implementation**:
```csharp
public class Game3_PassTheChip : IGameMode
{
    private Chip specialChip;
    private Player chipOwner;
    
    public string ModeName => "Pass the Chip";
    public int ModeID => 2;
    
    public void OnChipMoved(Player player, int fromCell, int toCell)
    {
        // Check if special chip was moved
        if (IsSpecialChip(fromCell))
        {
            chipOwner = player;
            player.AddScore(10); // Double points for special chip
        }
        else
        {
            player.AddScore(5); // Regular move
        }
    }
    
    public bool CheckWin(Player player, BoardModel board)
    {
        // Win condition: have moved special chip 5 times
        // Tracked in player metadata
        return GetSpecialChipMoveCount(player) >= 5;
    }
    
    // ... other methods ...
}
```

---

### Mode 4: BUMP U & 5 (Complex)

**Description**: Combined variant with both bumping mechanics and special 5-in-a-row rule.

**Rules**:
- Move 2 chips per turn (or 1 if only 1 available)
- Must bump if able (forced bump)
- Win by getting 5 in a row OR reaching 200 points

**Scoring**:
- Move: +5 points
- Bump (forced): +15 points (higher due to mandatory)
- Score threshold win: 200 points

**Special Cases**:
- Forced bumping: If you can bump, you must
- Double-5: Loses points (-25) and doesn't roll again (unique)
- Can move 2 chips per turn (choice of which)

**IGameMode Implementation**:
```csharp
public class Game4_BumpUAnd5 : IGameMode
{
    private int chipsMovedThisTurn = 0;
    
    public string ModeName => "Bump U & 5";
    public int ModeID => 3;
    
    public bool CheckWin(Player player, BoardModel board)
    {
        // Win by 5-in-a-row OR reaching 200 points
        return board.Check5InARow(player) || player.Score >= 200;
    }
    
    public void OnDiceRolled(Player player, int[] diceRoll)
    {
        // Double-5 penalty
        if (diceRoll.Length == 2 && diceRoll[0] == 5 && diceRoll[1] == 5)
        {
            player.RemoveScore(25);
        }
        chipsMovedThisTurn = 0; // Reset move counter
    }
    
    public void OnChipMoved(Player player, int fromCell, int toCell)
    {
        chipsMovedThisTurn++;
        player.AddScore(5);
    }
    
    // ... other methods ...
}
```

---

### Mode 5: SOLITARY (Single Player)

**Description**: A puzzle-like single-player challenge mode.

**Rules**:
- Single player vs. board
- Must achieve 5 in a row in minimum turns
- Pre-placed opponent chips block the way
- Roll 2 dice, move strategically around blocks

**Scoring**:
- Move: +1 point
- Clear path move: +5 points
- Win under 20 turns: +500 bonus
- Win 20-30 turns: +250 bonus

**Special Cases**:
- No bumping (opponent chips are fixed)
- 5+6 still counts as safe
- Doubles grant extra movement (roll again)

**IGameMode Implementation**:
```csharp
public class Game5_Solitary : IGameMode
{
    private int turnCount;
    
    public string ModeName => "Solitary";
    public int ModeID => 4;
    
    public bool CheckWin(Player player, BoardModel board)
    {
        bool hasWon = board.Check5InARow(player);
        
        if (hasWon)
        {
            // Award bonus based on turn count
            if (turnCount < 20)
                player.AddScore(500);
            else if (turnCount < 30)
                player.AddScore(250);
        }
        
        return hasWon;
    }
    
    public bool IsBumpAllowed(Player player, int targetCell, BoardModel board)
    {
        return false; // No bumping in Solitary
    }
    
    public void OnTurnEnd(Player player)
    {
        turnCount++;
    }
    
    // ... other methods ...
}
```

---

## File Structure (Created in Sprint 3)

```
/Assets/Scripts/GameModes/
  IGameMode.cs                 (CREATED in this sprint prep)
  Game1_Bump5.cs              (NEW - complete implementation)
  Game2_Krazy6.cs             (NEW - complete implementation)
  Game3_PassTheChip.cs        (NEW - complete implementation)
  Game4_BumpUAnd5.cs          (NEW - complete implementation)
  Game5_Solitary.cs           (NEW - complete implementation)
  GameModeFactory.cs          (NEW - instantiate modes)

/Assets/Scripts/Tests/
  Game1_Bump5Tests.cs         (NEW - 8+ tests)
  Game2_Krazy6Tests.cs        (NEW - 8+ tests)
  Game3_PassTheChipTests.cs   (NEW - 8+ tests)
  Game4_BumpUAnd5Tests.cs     (NEW - 8+ tests)
  Game5_SolitaryTests.cs      (NEW - 8+ tests)
```

---

## Unit Tests Required

### Test Structure for Each Mode
Each mode gets ~8-10 tests following this pattern:

**Game1_Bump5Tests.cs**:
```
- CheckWin_With5InARow_ReturnsTrue()
- CheckWin_With4InARow_ReturnsFalse()
- CanRollAgain_WithDouble_ReturnsTrue()
- CanRollAgain_WithoutDouble_ReturnsFalse()
- IsBumpAllowed_WithOpponentChip_ReturnsTrue()
- GetValidMoves_ReturnsAdjacentCells()
- OnDiceRolled_With5Plus6_Marked()
- GetRulesText_ReturnsNonEmpty()
```

**Game2_Krazy6Tests.cs**:
```
- OnDiceRolled_With6And6_Deducts50()
- CanRollAgain_With6And6_ReturnsFalse()
- CanRollAgain_WithOtherDouble_ReturnsTrue()
- CheckWin_With5InARow_ReturnsTrue()
- IsBumpAllowed_ReturnsTrue()
- ... (similar pattern)
```

... and so on for each mode.

---

## GameModeFactory (Helper)

```csharp
public static class GameModeFactory
{
    public static IGameMode CreateGameMode(int modeID)
    {
        return modeID switch
        {
            0 => new Game1_Bump5(),
            1 => new Game2_Krazy6(),
            2 => new Game3_PassTheChip(),
            3 => new Game4_BumpUAnd5(),
            4 => new Game5_Solitary(),
            _ => throw new System.ArgumentException($"Unknown mode ID: {modeID}")
        };
    }
    
    public static IGameMode CreateGameMode(string modeName)
    {
        return modeName.ToLower() switch
        {
            "bump 5" => new Game1_Bump5(),
            "krazy 6" => new Game2_Krazy6(),
            "pass the chip" => new Game3_PassTheChip(),
            "bump u & 5" => new Game4_BumpUAnd5(),
            "solitary" => new Game5_Solitary(),
            _ => throw new System.ArgumentException($"Unknown mode: {modeName}")
        };
    }
    
    public static System.Collections.Generic.List<IGameMode> GetAllModes()
    {
        return new System.Collections.Generic.List<IGameMode>
        {
            new Game1_Bump5(),
            new Game2_Krazy6(),
            new Game3_PassTheChip(),
            new Game4_BumpUAnd5(),
            new Game5_Solitary()
        };
    }
}
```

---

## Mode Integration Points

### With GameStateManager
- GameStateManager calls methods on current IGameMode
- Modes are assigned at game start
- GameStateManager doesn't know specifics of each mode

### With UI (Sprint 5)
- Mode name displayed in mode selection screen
- Mode description shown in rules screen
- GetRulesText() used to populate help

### With Board (Sprint 4)
- GetValidMoves() determines which cells light up
- IsBumpAllowed() determines if bump button shows
- Modes don't touch board visualization (pure logic)

---

## Success Criteria

✅ All 5 game modes fully implemented  
✅ All 40+ unit tests pass  
✅ Each mode has unique scoring/rules  
✅ Zero logic bugs in any mode  
✅ Code follows CODING_STANDARDS.md  
✅ Full documentation (method summaries)  
✅ GameModeFactory works for all modes  
✅ All modes integrate seamlessly with GameStateManager  
✅ Rules text complete for each mode  

---

## Review Checklist

Before this sprint is complete:
- [ ] All 5 classes follow naming conventions
- [ ] All public methods documented
- [ ] No magic numbers (all constants defined)
- [ ] All 40+ tests pass
- [ ] Code reviewed by Managing Engineer
- [ ] No console errors/warnings
- [ ] Each mode tested end-to-end in GameStateManager
- [ ] Mode factory tested
- [ ] Rules text is clear and complete
- [ ] Edge cases for each mode handled

---

## Next Sprint Preview (Sprint 4)

Sprint 4 will create the visual board system that displays this logic.

- BoardGridManager visualizes the 12 cells
- UI calls IGameMode.GetValidMoves() to light up valid cells
- Chip visuals placed/moved based on GameStateManager events
- Mode names displayed in UI

---

**Sprint Start Date**: Nov 28, 2025  
**Estimated Completion**: Dec 5, 2025  
**Owner**: Gameplay Engineer Agent  
**Dependency**: Sprint 1 & 2 approved ✅
