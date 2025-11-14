# Game Modes Rules Summary
**Created**: Nov 14, 2025  
**Owner**: Gameplay Engineer  
**Status**: ACTIVE

---

## Game 1: Bump 5

### Rules
- Players take turns rolling a die. Each die result moves a chip forward by that many cells.
- A player can only move a chip that is on the board or bring a chip onto the board (with any valid roll).
- Players must achieve exactly a count of 5 bumps to win. A "bump" occurs when a player's chip lands on a cell already occupied by an opponent's chip.
- When a player lands on an opponent's chip, the opponent's chip is returned to its starting area (off the board).
- A player cannot move a chip off the board unless they've achieved the required 5 bumps.
- Each player has a maximum of 2 chips in play simultaneously.

### Win Conditions
- Player declares win when they have bumped 5 times AND successfully move a chip off the board
- Win is validated by GameStateManager before being accepted
- Only one player wins per game

### Special Rules
- First turn of game: Player must roll to move a chip onto the board (cannot move existing chips)
- Rolling a 6: Player gets an extra turn (can roll again immediately)
- Multiple bumps in one turn: Each bump counts toward the 5-bump requirement
- If no legal moves available: Player passes their turn
- Chip priority: Player can choose which chip to move (if multiple valid moves exist)

---

## Game 2: Krazy 6

### Rules
- Players roll a die and move chips forward by the rolled value
- Any roll of a 6 grants an immediate extra turn
- Players win by getting all chips off the board, but with a twist: they must use exactly 6 dice rolls to do so
- Bumping an opponent's chip sends it back to start (same as Game 1)
- A player can move any of their chips on the board each turn
- Chips must reach the finish cell (cell 12) and then can exit

### Win Conditions
- Player moves all their chips off the board
- Player must accomplish this in exactly 6 total rolls (across all turns)
- If a player exceeds 6 rolls before finishing, they cannot win in this mode
- Win is declared after 6th roll if all chips are off the board

### Special Rules
- Roll limit: Exactly 6 rolls total (this is the "krazy" constraint)
- Extra turns from rolling 6: Each counts toward the 6-roll limit
- Bumps still send opponent chips back to start
- Strategy: Players must plan moves efficiently to finish within exactly 6 rolls
- If blocked with chips still on board at roll 6: Game ends, no winner

---

## Game 3: Pass The Chip

### Rules
- Each player starts with 1 chip on the board
- Players roll a die to move their chip forward
- Bumping an opponent's chip sends it back to start (standard rule)
- When a player bumps an opponent's chip, control of that opponent's chip passes to the bumping player
- The passing player now controls that chip and can move it on their next turn
- A player wins by being the last one standing with a chip still on the board

### Win Conditions
- Player wins when all other players have lost control of all their chips
- A player loses control when their chip is bumped 3 times
- After 3 bumps, a player's chip is removed from play
- Last player with an active chip (not removed) wins

### Special Rules
- Chip control transfer: When you bump, you gain control of the bumped chip
- Bump counter: Each chip tracks its own bump count (visible to all players)
- Removed chips: Chips removed after 3 bumps cannot return to the board
- Single chip mode: Each player only has 1 chip (no multiple chips like other modes)
- Turn priority: Even though a bumped chip is now yours, you must wait for your next turn to move it

---

## Game 4: Bump U And 5

### Rules
- Combination mode mixing Game 1 (Bump 5) and standard board racing
- Players roll to move chips forward
- Players must achieve exactly 5 bumps (like Game 1)
- Bumping an opponent's chip sends it back to start
- After achieving 5 bumps, player must then race to get all chips off the board
- Win requires both conditions: 5 bumps achieved AND all chips off the board

### Win Conditions
- Player must achieve exactly 5 bumps
- After 5 bumps achieved, player must move all their chips off the board
- Win declared when both conditions are met
- Game tracks bump count separately from chip exit tracking

### Special Rules
- Two-phase victory: First phase is bumping (5 required), second phase is exiting (all chips)
- Can only exit chips after bumping requirement is met
- Bumps beyond 5 do not help progression (only first 5 count)
- Visual indicator: UI shows "5/5 bumps - now exit" message to guide player
- Rolling a 6: Extra turn (helps with both phases)
- Chip management: Can focus on any chips, but all must exit to win

---

## Game 5: Solitary

### Rules
- Single-player mode (player vs. board challenge, no opponents)
- Player has 2 chips to place strategically on a 12-cell board
- No dice rolling; instead, player is given a target sequence of cell numbers
- Player must place both chips to land on the exact target sequence
- Each turn, player can move one chip forward by a random amount (1-6 cells)
- Player must visit all cells in the target sequence in order using only 2 chips

### Win Conditions
- Player completes the target sequence by landing on all required cells in order
- Both chips are used across the sequence (distributed however player chooses)
- Target sequences increase in difficulty:
  - Easy: 5-cell sequence
  - Medium: 8-cell sequence
  - Hard: 12-cell sequence (visit all cells)
- Win declared when final target cell is reached

### Special Rules
- No opponents: Player races against the board/sequence challenge
- Random movement: Each turn grants 1-6 cells of forward movement (predetermined)
- Chip reset: Can restart chips to any earlier cell (no penalty, but uses a turn)
- Target visibility: Target sequence shown to player at game start
- Bonus: Can earn score multiplier for completing without resets
- Movement mechanic: Move is predetermined; player only chooses which chip moves

---

## Related Documents
- SPRINT_3_DETAILED_BRIEFING.md
- IGameMode Interface Definition
- GameStateManager.cs (implements these rules)

---

**Status**: Complete - Ready for Code Review
