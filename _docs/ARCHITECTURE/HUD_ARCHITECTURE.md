# HUD Architecture
**Created**: Nov 14, 2025  
**Owner**: UI Engineer  
**Status**: ACTIVE

---

## Overview

The HUD (Heads-Up Display) is the primary user interface during active gameplay. It displays real-time game state, provides interactive buttons for player actions, and communicates game events to the player. The HUD is orchestrated by HUDManager, which listens to GameStateManager events and updates all UI elements synchronously.

---

## Core Components

### DiceRollButton
- **Purpose**: Initiates a dice roll action
- **State**: Active during player's turn, disabled otherwise
- **Interaction**: 
  - Click/tap triggers GameStateManager.RollDice()
  - Shows animation while rolling (duration 300ms)
  - Displays rolled number (1-6) briefly
- **Visual**: Large primary button, center-bottom of screen
- **Feedback**: Roll result animation + sound effect
- **Position**: Bottom-center, 48px height, 200px width

### BumpButton
- **Purpose**: Declare a bump action (land on opponent's chip)
- **State**: Active only when valid move would result in bump
- **Interaction**:
  - Click/tap moves selected chip to target cell with bump logic
  - Automatically detects if move is bump-eligible
  - Shows as disabled if current selected move is not a bump
- **Visual**: Secondary button (outlined), right-side of screen
- **Feedback**: Bump animation (opponent chip eject, scoring)
- **Position**: Right-center, 48px height, 160px width

### DeclareWinButton
- **Purpose**: Declare victory when win conditions met
- **State**: Active only when player meets game mode's win condition
- **Interaction**:
  - Click/tap validates win condition
  - Transitions to win screen on success
  - Shows confirmation dialog if uncertain
- **Visual**: Success green button, top-right of screen
- **Feedback**: Victory animation, confetti, sound
- **Position**: Top-right, 48px height, 160px width
- **Safety**: Requires 2-step confirmation to prevent accidents

### ScoreboardDisplay
- **Purpose**: Show current score/progress for all players
- **Content**:
  - Player names + colors
  - Current bumps (for Bump-based modes)
  - Chips on board vs off board
  - Current turn indicator
  - Chip count for active player
- **State**: Updates real-time on every action
- **Position**: Top-center, vertical list format
- **Update Frequency**: Every game state change (< 50ms)

### PhaseIndicator
- **Purpose**: Show current game phase/turn state
- **Content**:
  - Current game phase (Setup, Playing, Waiting, Ended)
  - Active player name
  - Current roll value (if applicable)
  - Turn order visualization
- **State**: Updates with each phase transition
- **Position**: Top-left corner, compact format
- **Visual**: Text + icon, clear typography

### PopupManager
- **Purpose**: Display contextual notifications and alerts
- **Types**:
  - **PENALTY**: Rule violation, penalty applied (red alert)
  - **PASS_THE_CHIP**: Chip control transferred (info popup)
  - **TAKE_IT_OFF**: Player can exit chip (success popup)
  - **INVALID_MOVE**: Move not allowed (warning popup)
  - **GENERAL**: Information or confirmation dialogs
- **Behavior**: Center-screen modal, auto-dismiss or user dismiss
- **Animation**: Fade in 150ms, fade out 150ms, 3-second display (manual popup)
- **Position**: Center-screen, 300px width, responsive height

### GameModeSelectionScreen
- **Purpose**: Allow player to choose game mode at start
- **Content**:
  - 5 buttons for Game 1-5
  - Game name + short description
  - Player count (solo/multiplayer indicator)
- **Interaction**: Click game → configure player count → start
- **Visual**: Grid of game mode cards, 2-3 per row
- **Position**: Full screen, visible before gameplay starts

---

## HUD State Machine

### States
1. **Menu** - Game mode selection screen, main menu
2. **Setup** - Player/mode configured, ready to start
3. **Playing** - Active gameplay, player can take actions
4. **Waiting** - Waiting for opponent turn or system action
5. **Ended** - Game finished, win/loss screen shown
6. **Paused** - Game paused, pause menu showing

### State Transitions
```
Menu → Setup → Playing ↔ Waiting → Ended
  ↓                ↓      ↓       ↓
  └─────────────→ Paused ←──────┘
```

### Entry Actions
- **Menu**: Show game mode selection, hide HUD buttons
- **Setup**: Hide game mode screen, show game board
- **Playing**: Activate buttons (Dice, Bump, DeclareWin), show phase
- **Waiting**: Disable buttons, show "waiting" message
- **Ended**: Show win/loss screen, deactivate all buttons
- **Paused**: Show pause menu, dim board, disable interactions

---

## Event Binding System

### GameStateManager → HUD Events
```
OnGameStateChanged
├─ Update ScoreboardDisplay
├─ Update PhaseIndicator
├─ Update button availability
└─ Trigger animations if major change

OnDiceRolled
├─ Animate dice display (3x rotation, 300ms)
├─ Show rolled number (1-6)
└─ Trigger sound effect

OnMoveMade
├─ Update board visualization
├─ Update scoreboard (chip counts)
├─ Check if bump occurred
└─ Show popup if needed

OnGameWon
├─ Activate DeclareWinButton
├─ Show victory indicators
└─ Enable win declaration

OnBumpOccurred
├─ Show PENALTY or PASS_THE_CHIP popup
├─ Update scoreboard
└─ Play bump sound effect

OnPhaseChanged
├─ Update PhaseIndicator
├─ Enable/disable buttons based on phase
└─ Update turn indicator
```

### Button → GameStateManager Events
```
DiceRollButton.OnClick
└─ Call GameStateManager.RollDice()

BumpButton.OnClick
└─ Call GameStateManager.ExecuteMove(selectedCell, isBump: true)

DeclareWinButton.OnClick
└─ Call GameStateManager.DeclareWin()
```

---

## Animation Specifications

### DiceRollButton Animation
- Trigger: DiceRollButton clicked
- Animation: Rotate 3 full rotations (1080°)
- Duration: 300ms
- Easing: Ease-Out
- Post-animation: Display rolled number in label

### BumpButton Feedback
- Trigger: Valid bump move executed
- Animation: Scale up 1.2x, bounce effect
- Duration: 250ms
- Sound: Bump/collision sound effect (100ms)
- Particle effect: Optional chip collision animation

### ScoreboardDisplay Update
- Trigger: Game state changed
- Animation: Subtle fade transition on changed values
- Duration: 150ms
- Target: Only fields that changed (not entire board)

### PopupAnimation
- Entry: Fade in + slide up (center origin)
- Duration: 150ms easing out
- Exit: Fade out + slide down
- Duration: 150ms easing in
- Display duration: 3000ms (auto-dismiss) or manual

### PhaseIndicator Transition
- Trigger: Phase changed
- Animation: Pulse effect (scale 1.0 → 1.1 → 1.0)
- Duration: 200ms
- Color shift: Highlight if turn changes

---

## Prefab Hierarchy

```
Canvas (ScreenSpace-Overlay, 1920×1080 design resolution)
│
├─ Board (12-cell game board)
│  └─ [Cells and Chips - see BOARD_ARCHITECTURE.md]
│
├─ HUD (Container for all HUD elements)
│  │
│  ├─ TopBar
│  │  ├─ PhaseIndicator (top-left)
│  │  ├─ ScoreboardDisplay (top-center)
│  │  └─ DeclareWinButton (top-right)
│  │
│  ├─ BottomBar
│  │  ├─ DiceRollButton (bottom-center, large)
│  │  └─ BumpButton (bottom-right)
│  │
│  ├─ PopupContainer (center, above everything)
│  │  ├─ PopupBackground (overlay)
│  │  └─ PopupContent (text + buttons)
│  │
│  └─ GameModeSelectionScreen (full-screen, shows on startup)
│     ├─ Background
│     ├─ Title
│     └─ GameModeButtons[5] (1-5 for each mode)
│
└─ SystemIndicators
   ├─ FPSCounter (debug, top-right)
   └─ DebugLog (debug, bottom-left)
```

---

## Component Dependencies

### HUDManager Script
- Listens to: GameStateManager.OnGameStateChanged
- Controls: All HUD components
- Updates: Button states, text displays, animations
- Broadcasts: None (receives events, doesn't send)

### DiceRollButton Script
- Listens to: None (user input)
- Calls: GameStateManager.RollDice()
- Updates: Button animation, result display
- State: Enabled only during player's turn

### ScoreboardDisplay Script
- Listens to: GameStateManager.OnGameStateChanged
- Updates: Player names, scores, chip counts, turn indicator
- Refresh rate: Real-time (synchronous)
- No user input

### PopupManager Script
- Listens to: GameStateManager events (bump, penalty, etc.)
- Displays: Popup prefabs, auto-dismisses or waits for input
- Animation: Fade/slide transitions
- Queue: Can handle multiple popups (queued)

---

## Interaction Flow

### Example: Player Takes a Turn
1. Player taps DiceRollButton
2. DiceRollButton.OnClick() → GameStateManager.RollDice()
3. GameStateManager rolls 1-6, updates GameState
4. GameStateManager broadcasts OnDiceRolled event
5. HUDManager receives event:
   - DiceRollButton animates (rotation + result display)
   - Board highlighting updates (valid moves shown)
   - BumpButton enabled/disabled (if bump possible)
6. Player selects board cell (or taps BumpButton if applicable)
7. GameStateManager executes move, broadcasts OnMoveMade
8. HUDManager updates:
   - ScoreboardDisplay (chip counts, scores)
   - PhaseIndicator (next player's turn)
   - Popups (if penalty or special event)
9. Turn passes to next player

---

## Canvas Scaling Strategy

### Design Resolution
- Base resolution: 1920×1080 (16:9)
- Safe area: 10% margin on all sides

### Mobile Scaling
- iPhone (375×667): Scale UI proportionally, button sizes stay >= 44px
- iPad (1024×768): Similar scaling
- Android phones: Variable, but min touch target always 44px
- Android tablets: Similar to iPad scaling

### Responsive Rules
- Buttons never scale below 44px (touch target)
- Text scales with canvas (min 12px at smallest resolution)
- Popups constrain to safe area
- Landscape mode: HUD repositions left/right
- Portrait mode: HUD stacks top/center/bottom

---

## Related Documents
- UI_DESIGN_SYSTEM.md
- UI_STANDARDS.md
- SPRINT_5_UI_PREP.md

---

**Status**: Complete - Production Ready
