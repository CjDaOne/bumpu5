# Sprint 1 Kickoff - Core Game Logic Foundation

**Sprint Duration**: Week 1  
**Lead**: Gameplay Engineer Agent  
**Goal**: Build testable, non-visual game logic with 80%+ unit test coverage  

---

## What We're Building

The foundation of the entire game—pure C# logic that runs independently of Unity. At the end of this sprint, the game will be 100% playable in code (you won't see anything on screen, but all rules work).

---

## Deliverables (In Order)

### 1. Core Classes (Pure C#, No Unity Dependency)

#### BoardCell.cs
Represents a single cell on the 12-cell board.
```
Properties:
- CellIndex (0-11)
- Owner (Player or null)
- HasChip (bool)
- IsHighlighted (bool)

Methods:
- PlaceChip(Player p)
- RemoveChip()
- BumpChip()
```

#### Chip.cs
Represents a single game piece.
```
Properties:
- Owner (Player)
- CurrentCell (BoardCell)
- IsActive (bool)

Methods:
- MoveTo(BoardCell target)
- Remove()
```

#### Player.cs
Represents a player in the game.
```
Properties:
- PlayerIndex (0 or 1)
- Name (string)
- Chips (List<Chip>)
- Score (int)
- IsActive (bool)

Methods:
- AddScore(int points)
- RemoveScore(int points)
- GetChipsOnBoard()
- GetChipsOffBoard()
```

#### BoardModel.cs
Core board logic (no visual representation).
```
Properties:
- Cells (BoardCell[12])
- Players (Player[])
- Grid Adjacency Map

Methods:
- IsAdjacent(int cell1, int cell2) → bool
- GetAdjacentCells(int cellIndex) → List<int>
- Check5InARow(Player p) → bool (detect winner)
- Check5InARow(int startIndex) → bool (recursive search)
- IsValidMove(Player p, int fromCell, int toCell) → bool
- CanBump(Player p, int targetCell) → bool
- ApplyBump(Player p, int targetCell) → void
```

---

### 2. Dice System

#### DiceManager.cs
Handles all dice rolls and edge cases.
```
Methods:
- RollSingleDie() → int (1-6)
- RollTwoDice() → int[2]
- GetDiceSum(int[2] roll) → int

Properties:
- IsDouble (bool) → Are both dice the same?
- LastRoll (int[2])

Edge Cases to Handle:
- 5 + 6 = "safe" (no bump possible from rolling)
- 6 = lose turn
- Double-5 = -50 points (Game4 specific, handled in GameMode)
- Any other double = stay in play
```

---

### 3. Turn Manager (Foundational)

#### TurnManager.cs (Basic version for Sprint 1)
Tracks whose turn it is.
```
Properties:
- CurrentPlayer (Player)
- CurrentPlayerIndex (int)
- PlayerList (Player[])

Methods:
- GetNextPlayer() → Player
- AdvanceTurn() → void
- GetCurrentPlayer() → Player
```

---

### 4. Game State Data Structures

#### GameState.cs (Serializable)
Snapshot of the current game.
```
Properties:
- CurrentPlayerIndex (int)
- Players (Player[])
- Board (BoardModel)
- LastDiceRoll (int[2])
- TurnPhase (Setup, Rolling, Placing, Bumping, EndTurn)
```

---

## Unit Tests Required

### Test File: BoardModelTests.cs
```
- Test5InARow_WithVerticalChips_ReturnsTrue()
- Test5InARow_WithHorizontalChips_ReturnsTrue()
- Test5InARow_WithDiagonalChips_ReturnsTrue()
- Test5InARow_With4Chips_ReturnsFalse()
- IsAdjacent_WithAdjacentCells_ReturnsTrue()
- IsAdjacent_WithNonAdjacentCells_ReturnsFalse()
- CanBump_WithOpponentChip_ReturnsTrue()
- CanBump_WithOwnChip_ReturnsFalse()
- ApplyBump_RemovesChipFromBoard()
- ApplyBump_UpdatesScores()
```

### Test File: DiceTests.cs
```
- RollSingleDie_AlwaysReturns1To6()
- RollTwoDice_SumBetween2And12()
- IsDouble_With5And5_ReturnsTrue()
- IsDouble_With3And5_ReturnsFalse()
- Get5Plus6AsDouble_ReturnsFalse() (special case)
```

### Test File: PlayerTests.cs
```
- CreatePlayer_HasEmptyChipList()
- AddScore_IncreasesScore()
- RemoveScore_DecreasesScore()
- GetChipsOnBoard_ReturnsOnlyActiveChips()
```

### Test File: TurnManagerTests.cs
```
- GetNextPlayer_RotatesCorrectly()
- AdvanceTurn_UpdatesCurrentPlayer()
- GetCurrentPlayer_ReturnsCorrectPlayer()
```

---

## File Structure (Created)

```
/Assets/Scripts/Core/
  BoardCell.cs
  Chip.cs
  Player.cs
  BoardModel.cs
  DiceManager.cs
  TurnManager.cs
  GameState.cs

/Assets/Scripts/Tests/
  BoardModelTests.cs
  DiceTests.cs
  PlayerTests.cs
  TurnManagerTests.cs
```

---

## Success Criteria

✅ All 7 core classes implemented  
✅ All 4 test files passing (80%+ coverage)  
✅ Zero logic bugs  
✅ All edge cases handled (5+6, double rolls, etc.)  
✅ Code follows CODING_STANDARDS.md  
✅ Documentation complete (method summaries)  

---

## Review Checklist

Before this sprint is complete:
- [ ] All classes follow PascalCase naming
- [ ] All public methods documented with /// comments
- [ ] No Magic numbers (all constants defined)
- [ ] Unit tests pass 100%
- [ ] Code reviewed by Managing Engineer
- [ ] No console errors/warnings

---

## Next Sprint Preview

Sprint 2 will take these classes and add:
- GameStateManager (full state machine)
- Turn flow with phases (rolling → placing → bumping → end)
- Integration tests connecting all systems

---

**Sprint Start Date**: Nov 14, 2025  
**Estimated Completion**: Nov 21, 2025  
**Owner**: Gameplay Engineer Agent
