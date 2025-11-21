# Bump U Box 5 - 5x5 Grid Update Walkthrough

## Overview
This update transitions the "Bump U" game from a 12-cell board to a 5x5 grid (25 cells) and implements the specific rules for 5 distinct game modes as per the "BUMP U BOX 5" specification.

## Changes

### Core Architecture
- **NodeData.cs**: Created a ScriptableObject to store static data for each node (Row, Column, Number).
- **BoardModel.cs**: Rewritten for 5x5 grid support.
    - `BOARD_SIZE` updated to 25.
    - `Check5InARow` implemented to detect horizontal, vertical, and diagonal wins on the 5x5 grid.
    - Adjacency logic updated for 5x5 grid.
- **NodeDataGenerator.cs**: Created an Editor script (`BumpU/Generate Node Data`) to automatically generate the 25 NodeData assets with the shifting numbering pattern (1-5).

### Game Modes
All game modes were updated to support the dynamic board size and specific rule sets.

#### Game 1: Bump 5 (Classic)
- **Win Condition**: 5 chips in a row (Horizontal, Vertical, Diagonal).
- **Rules**: Standard bumping. Single 6 loses turn.
- **Updates**: Updated `IsValidMove` and `CheckWinCondition` to use `BoardModel`'s 5x5 logic.

#### Game 2: Krazy 6
- **Win Condition**: 6 chips on the board.
- **Rules**:
    - Must roll a 6 to start placing chips (if chip count == 0).
    - If <= 5 chips and roll 6: **BUMPED by the 6** (Lose turn and remove a chip).
    - If > 5 chips and roll 6: **Good Roll** (Bonus turn).
- **Updates**: Implemented custom `IsLoseTurnRoll` logic and `OnDiceRolled` handler for penalties.

#### Game 3: Pass The Chip
- **Win Condition**: Center #5 node (index 12) boxed in by 8 adjacent chips.
- **Rules**: Swap instead of bump (handled by existing logic, though visual swap pending). Single 6 loses turn.
- **Updates**: Implemented specific "Boxed In" win condition checking 8 neighbors of center.

#### Game 4: Bump U & 5
- **Win Condition**: 5 chips anywhere on the board.
- **Rules**: Standard bumping. Single 6 loses turn.
- **Updates**: Updated `CheckWinCondition` to count total chips.

#### Game 5: Solitary
- **Win Condition**: Fill the entire board (25 chips).
- **Rules**:
    - Single 6: Remove last placed chip.
    - Double 6: Remove last two placed chips.
    - 5+6: Safe (cancels removal).
- **Updates**: Created new class `Game5_Solitary`. Implemented placement history tracking and removal logic in `OnDiceRolled`.

## Verification
- **Compilation**: Code structure verified for all game modes and core classes.
- **Logic**:
    - `BoardModel.Check5InARow` verified for 5x5 grid indices.
    - `Game2` start/penalty rules implemented.
    - `Game3` center adjacency implemented.
    - `Game5` stack-based removal implemented.

## Next Steps
1.  Open Unity Editor.
2.  Run `BumpU > Generate Node Data` from the menu to populate `Assets/Resources/NodeData`.
3.  Assign the generated NodeData assets to the Board in the scene (or ensure BoardModel loads them).
4.  Playtest each game mode to verify rules and win conditions.
