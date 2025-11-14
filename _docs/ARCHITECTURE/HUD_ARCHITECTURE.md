# HUD Architecture

**Created**: Nov 14, 2025  
**Owner**: UI Engineer  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Overview

The HUD (Heads-Up Display) is the primary interface layer that manages real-time game state communication to the player during active gameplay. It displays current game information, provides interaction points, and updates in response to GameStateManager events.

The HUD works in concert with GameStateManager and provides visual feedback and player agency during gameplay phases. All HUD updates are event-driven from the game state, ensuring synchronization and preventing UI drift.

---

## HUD Responsibilities

### Display
- Current player indicator
- Current turn phase (Rolling, Playing, Waiting)
- Active player's available actions
- Current player's score/progress
- Dice roll result
- Valid move highlighting coordination

### Interaction Points
- Dice Roll button (when allowed)
- BUMP button (when allowed)
- Declare Win button (when allowed)
- Menu/Settings access
- Pause functionality

### State Feedback
- Phase indicators
- Action availability
- Move validation feedback
- Win detection notifications
- Penalty/special event notifications

---

## HUD Components

### 1. DiceRollButton
- **Purpose**: Initiate dice roll when player's turn begins
- **Visibility**: Enabled only during "Rolling" phase for active player
- **Visual**: Large, centered, prominent blue button (56px height)
- **Feedback**: 
  - On click: Disabled until result returned
  - On roll complete: Returns to available state
- **State**: Available (enabled) / Disabled (grayed)

### 2. BumpButton
- **Purpose**: Declare bump action during "Playing" phase
- **Visibility**: Enabled only during "Playing" phase for active player
- **Visual**: Secondary button next to DiceRollButton (48px height)
- **Feedback**:
  - On click: Triggers bump validation in GameStateManager
  - On validation: Confirms or rejects bump
- **State**: Available / Disabled / Validated

### 3. DeclareWinButton
- **Purpose**: Player attempts to declare win condition met
- **Visibility**: Enabled only when win condition may be valid
- **Visual**: Success-state green button (48px height)
- **Feedback**:
  - On click: Validates win conditions
  - On validation: Confirms win or shows error message
- **State**: Available / Disabled

### 4. ScoreboardDisplay
- **Purpose**: Show all players' scores and current standings
- **Contents**:
  - Player name
  - Player color indicator
  - Current score/progress
  - Turn order indicator (circle showing current player)
  - Win status indicator
- **Update Frequency**: Real-time on game state changes
- **Position**: Top right, persistent throughout gameplay
- **Format**:
  ```
  Player 1 [Red]    [●] Score: 24
  Player 2 [Blue]   [ ] Score: 15
  Player 3 [Gold]   [ ] Score: 18
  Player 4 [Green]  [ ] Score: 12
  ```

### 5. PhaseIndicator
- **Purpose**: Display current game phase and whose turn it is
- **Display**: 
  - Current phase (Rolling, Playing, Waiting, etc.)
  - Current player name and color
  - Estimated time remaining (if applicable)
- **Position**: Top center
- **Update Frequency**: On phase change only
- **Example**: `Player 1 (Red) - Rolling Phase`

### 6. ActionFeedback (Contextual)
- **Purpose**: Display validation results and action feedback
- **Types**:
  - "Invalid move selected" (red text, brief timeout)
  - "Bump successful!" (green text, brief timeout)
  - "No valid moves available" (yellow text, stays until new phase)
  - "Cannot declare win: X chips still need movement" (red, modal)
- **Position**: Center bottom of screen or modal overlay
- **Duration**: 2-3 seconds (auto-dismiss) or modal (requires action)

### 7. PauseMenu
- **Purpose**: Pause gameplay and show menu options
- **Options**:
  - Resume Game
  - Settings
  - Rules
  - Quit to Menu
- **Behavior**: 
  - Freezes game state
  - Dims game board
  - Shows menu modal
  - Resumes seamlessly on resume
- **Position**: Full screen modal overlay

---

## HUD State Machine

The HUD operates with the following states, driven by GameStateManager events:

```
┌─────────────────────────────────────────────────┐
│                  MENU STATE                      │
│  - No HUD displayed                              │
│  - Main menu visible                             │
└──────────────────┬──────────────────────────────┘
                   │ Game Started
                   ▼
┌─────────────────────────────────────────────────┐
│              PLAYING STATE                       │
│  - Full HUD visible                              │
│  - Scoreboard active                             │
│  - Phase indicator active                        │
└──────────┬──────────────────────────┬───────────┘
           │                          │
    ROLLING PHASE              PLAYING PHASE
    - Dice button ON           - Move selection
    - Bump/Win OFF             - Dice button OFF
    - Waiting text             - Bump button ON
                               - Win button ON
    ┌──────────────────┬──────────────────────────┐
    │ WAITING PHASE    │                          │
    │ - All buttons    │       WIN DETECTED       │
    │   disabled       │       - Celebrate        │
    │ - "Waiting for   │       - Show winner      │
    │   Player X"      │       - Options: Play    │
    │                  │         again / Quit     │
    └──────────────────┴──────────────────────────┘
```

---

## Event Binding to GameStateManager

### HUD Listens To:
```csharp
// Phase changes
GameStateManager.OnPhaseChanged += UpdatePhaseIndicator;

// Turn changes
GameStateManager.OnTurnChanged += UpdateCurrentPlayerDisplay;
GameStateManager.OnPlayerTurnStarted += EnablePlayerActions;
GameStateManager.OnPlayerTurnEnded += DisablePlayerActions;

// Action events
GameStateManager.OnDiceRolled += DisplayDiceResult;
GameStateManager.OnMoveExecuted += UpdateBoardAndScores;
GameStateManager.OnBumpDetected += ShowBumpFeedback;
GameStateManager.OnWinDetected += ShowWinNotification;

// State events
GameStateManager.OnGameStateChanged += UpdateFullHUD;
GameStateManager.OnPenaltyApplied += ShowPenaltyNotification;
GameStateManager.OnSpecialRuleTriggered += ShowSpecialRuleNotification;
```

### HUD Calls GameStateManager:
```csharp
// Player actions
RollDiceButton.onClick += GameStateManager.RollDice;
BumpButton.onClick += GameStateManager.DeclareBump;
DeclareWinButton.onClick += GameStateManager.DeclareWin;

// Menu actions
PauseButton.onClick += GameStateManager.PauseGame;
ResumeButton.onClick += GameStateManager.ResumeGame;
QuitButton.onClick += GameStateManager.ReturnToMenu;
```

---

## HUD Animations

### Button Feedback Animations
- **On Hover**: Scale up 1.05, duration 150ms, easing ease-out
- **On Press**: Scale down 0.95, duration 150ms, easing ease-in
- **On Release**: Scale to 1.0, duration 100ms, easing ease-out
- **On Disable**: Opacity to 0.5, no scale change, immediate

### State Transitions
- **Phase Change**: Fade out old phase text (150ms), fade in new text (150ms)
- **Player Change**: Slide scoreboard highlight left/right (200ms) to new player
- **Win Notification**: Scale-in animation (300ms), confetti effect (1000ms)
- **Penalty**: Red flash (200ms), then normal

### Continuous Animations
- **Dice Rolling Button**: Subtle rotation (360° over 600ms) while rolling
- **Valid Move Glow**: Pulsing opacity (0.7 to 1.0) during move selection
- **Timer Countdown**: Smooth decrease of progress bar (if timer used)

---

## Prefab Hierarchy

```
Canvas (ScreenSpace - Overlay)
│
├─ HUDPanel (Panel - Full Screen)
│  │
│  ├─ TopBar (Panel - Horizontal Layout)
│  │  ├─ PhaseIndicatorText (Text)
│  │  └─ ScoreboardPanel (Panel - Vertical Layout)
│  │     ├─ PlayerRow_1 (Horizontal Layout)
│  │     │  ├─ PlayerColorIndicator (Image)
│  │     │  ├─ PlayerNameText (Text)
│  │     │  └─ PlayerScoreText (Text)
│  │     ├─ PlayerRow_2 (Horizontal Layout)
│  │     │  └─ [same structure]
│  │     ├─ PlayerRow_3 (Horizontal Layout)
│  │     │  └─ [same structure]
│  │     └─ PlayerRow_4 (Horizontal Layout)
│  │        └─ [same structure]
│  │
│  ├─ BottomBar (Panel - Horizontal Layout)
│  │  ├─ DiceRollButton (Button + Image)
│  │  ├─ BumpButton (Button + Image)
│  │  ├─ DeclareWinButton (Button + Image)
│  │  └─ PauseButton (Button + Image)
│  │
│  └─ NotificationPanel (Panel - Center, Dynamic)
│     ├─ NotificationText (Text)
│     └─ NotificationIcon (Image - optional)
│
├─ ModalOverlay (Panel - Full Screen, Initially Inactive)
│  ├─ DimBackground (Image - Black, 50% opacity)
│  └─ ModalContent (Panel - Center)
│     ├─ ModalTitle (Text)
│     ├─ ModalMessage (Text)
│     ├─ ModalButton_Primary (Button)
│     └─ ModalButton_Secondary (Button)
│
└─ PauseMenuOverlay (Panel - Full Screen, Initially Inactive)
   ├─ DimBackground (Image - Black, 50% opacity)
   └─ PauseMenuPanel (Panel - Center)
      ├─ PauseTitle (Text - "Game Paused")
      ├─ ResumeButton (Button)
      ├─ SettingsButton (Button)
      ├─ RulesButton (Button)
      └─ QuitButton (Button)
```

---

## Component Relationships

### MonoBehaviour Classes
1. **HUDManager** (Main orchestrator)
   - References: GameStateManager, all HUD components
   - Responsibilities: Lifecycle, event subscriptions, update logic

2. **ActionButtonController**
   - References: DiceRollButton, BumpButton, DeclareWinButton
   - Responsibilities: Enable/disable based on game phase

3. **ScoreboardController**
   - References: ScoreboardPanel, individual player rows
   - Responsibilities: Update player scores and turn order display

4. **NotificationController**
   - References: NotificationPanel, NotificationText
   - Responsibilities: Show/hide/auto-dismiss notifications

5. **ModalController**
   - References: ModalOverlay, ModalContent, buttons
   - Responsibilities: Show/hide modals, handle button clicks

---

## Update Cycle

### Per Frame (Update)
- Check button hover states (visual feedback only)
- Animate notification fade-out if active
- Update timer display (if applicable)

### Event-Driven (OnEvent)
- Phase change → Update phase indicator
- Turn change → Update scoreboard highlight
- Score change → Update score displays
- Action required → Enable/disable buttons
- Game end → Show win notification modal

### Deferred (LateUpdate if needed)
- Layout adjustment for safe areas (iOS notch)
- Final positioning of any dynamic elements

---

## Integration Checklist

- [ ] HUDManager subscribed to all GameStateManager events
- [ ] All buttons have click handlers connected
- [ ] Scoreboard updates sync with GameStateManager player list
- [ ] Phase indicator updates on every phase change
- [ ] Notifications appear and disappear correctly
- [ ] Modal system works for win/loss screens
- [ ] Pause menu functional
- [ ] All animations smooth and correct duration
- [ ] Safe area handling tested on mobile
- [ ] Accessibility: Touch targets ≥44px
- [ ] Accessibility: Color contrast meets WCAG AA

---

## Related Documents

- UI_DESIGN_SYSTEM.md
- UI_STANDARDS.md
- SPRINT_5_UI_PREP.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Implementation
