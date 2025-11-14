# Input Handling
**Created**: Nov 14, 2025  
**Owner**: Board Engineer  
**Status**: ACTIVE

---

## Tap/Click Detection

### Detection Layer
- **Component**: EventSystem (Unity built-in)
- **Method**: GraphicsRaycaster for board cells
- **Location**: Canvas + CellView colliders
- **Responsiveness**: Every frame input polling

### Event Routing Flow
```
Input.GetMouseButtonDown(0) / Touch input
  ↓
EventSystem.RaycastAll (from input position)
  ↓
Check if hit CellView
  ↓
CellView.OnPointerClick(PointerEventData)
  ↓
BoardGridManager.OnCellTapped(cellIndex)
  ↓
Game logic (validation, move execution)
```

### Platform Differences
- **Mouse (WebGL/Desktop)**:
  - Input.GetMouseButtonDown(0) for click
  - Input.mousePosition for cursor location
  - Hover states available

- **Touch (Mobile)**:
  - Input.touches for multi-touch (if needed)
  - Input.GetTouch(0).position for first touch
  - No hover states (single tap = click)
  - Prevent double-tap zoom (touch-action: manipulation CSS)

---

## Move Validation Flow

### Decision Tree
```
User taps cell → OnCellTapped(cellIndex)
  ├─ Is game in Playing state? 
  │  └─ NO: Ignore input, return
  │
  ├─ Is it player's turn?
  │  └─ NO: Ignore input, show "not your turn", return
  │
  ├─ Has player rolled dice?
  │  └─ NO: Ignore input, show "roll first", return
  │
  ├─ Is cell in valid moves list?
  │  └─ NO: Flash red, show "invalid move", return
  │
  ├─ Execute move
  │  ├─ Call GameStateManager.ExecuteMove(cellIndex)
  │  ├─ If move fails: Show error popup, return
  │  └─ If move succeeds: Continue
  │
  └─ Animation plays
     ├─ Chip moves from current to target
     ├─ Bump effect if applicable
     └─ Wait for animation completion
```

### Validation Checklist
- ✓ Game is in Playing state (not Menu, Setup, etc.)
- ✓ It's the current player's turn
- ✓ Dice has been rolled (roll value available)
- ✓ Cell is in the valid moves list
- ✓ Move doesn't violate game rules
- ✓ Target cell can accept the move

---

## Valid Moves Highlighting

### Highlight Timing
- **Triggered by**: GameStateManager.OnDiceRolled event
- **Calculation**: GameStateManager.GetValidMoves()
- **Display**: BoardGridManager.HighlightValidMoves(list)
- **Duration**: Until move executed or turn ends

### Highlight Logic
```
GetValidMoves():
  1. Get current player's chips
  2. For each chip:
     a. Calculate: target = chipPosition + diceRoll
     b. If target valid (in 1-12):
        - Add to valid moves list
        - Check if bump possible
     c. If target > 12:
        - Only valid if chip is already near finish
  3. Return list of valid cell indices
```

### Visual Highlighting
- **Color**: Bright green (#43A047) or primary blue (#1E88E5)
- **Animation**: Pulse effect (scale 1.0 → 1.1 → 1.0, 600ms loop)
- **Opacity**: 80% (visible but not too bright)
- **Effect**: Glowing ring around cell
- **Layers**: On top of cell background, below chip

### Clearing Highlights
- **Trigger**: Move executed OR turn ended
- **Method**: BoardGridManager.ClearHighlights()
- **Animation**: Fade out 150ms
- **Result**: Cells return to normal state

---

## Cell Interaction States

### Idle State
- **Appearance**: Normal cell color, no highlight
- **Interaction**: Can be tapped
- **Cursor**: Pointer (on hover, desktop)
- **Animation**: None (unless pulsing as valid move)

### Highlighted (Valid Move Available)
- **Appearance**: Bright color with pulse animation
- **Interaction**: Can be tapped to move
- **Cursor**: Pointer (emphasizing it's clickable)
- **Animation**: Pulse 600ms loop
- **Visual Priority**: Most prominent

### Selected (Tapped by Player)
- **Appearance**: Bold color, scale up slightly
- **Interaction**: Confirms selection, move executes
- **Cursor**: Pointer
- **Duration**: Briefly before move animation
- **Effect**: Scale 1.05x, border highlight

### Animating (Chip Moving)
- **Appearance**: Chip in motion (moving across board)
- **Interaction**: BLOCKED - no further input accepted
- **Cursor**: Not-allowed
- **Duration**: 300ms (move animation)
- **Visual**: Smooth motion, no jerking

### Occupied (Has a Chip)
- **Appearance**: Cell color + chip visual on top
- **Interaction**: Can be source of move (if player's chip)
- **Cursor**: Pointer
- **Chip Display**: Player color + border
- **Visual**: Chip centered in cell

---

## Input Blocking

### When to Block Input
- **During animations**: Chip move, bump effect (300-400ms)
- **During invalid states**: Setup phase, paused, ended
- **During system actions**: Loading, processing move
- **During popups**: Modal dialog shown

### Blocking Implementation
```csharp
private bool inputEnabled = true;

void OnCellTapped(int cellIndex) {
  if (!inputEnabled) return; // Blocked
  
  // Process move
  inputEnabled = false; // Block during animation
  
  // Animate move (300ms)
  // Callback when complete:
  inputEnabled = true; // Re-enable input
}
```

### Blocked Input Feedback
- **Cursor**: Change to not-allowed cursor
- **Visual**: Dim board or cells
- **Message**: Optional "animating..." tooltip
- **Duration**: Until block lifted

### Recovery (Re-enabling Input)
- **Trigger**: Animation complete, state safe
- **Method**: Callback from animation or timer
- **Delay**: Add 100ms buffer for visual completion
- **Feedback**: Cursor changes back to pointer

---

## Platform-Specific Input

### Mouse (WebGL/Desktop)
- **Hover Detection**: Use OnPointerEnter/Exit
- **Click Detection**: Use OnPointerClick
- **Cursor Changes**: 
  - Pointer on hover (clickable elements)
  - Not-allowed during blocked input
- **Drag**: Not used in this game

### Touch (Mobile)
- **Tap Detection**: Use OnPointerClick (IPointerClickHandler)
- **Multi-Touch**: Not used (single player at a time)
- **Swipe**: Not used for cell selection
- **Long-Press**: Not used (single tap = action)
- **Prevent Double-Tap Zoom**: CSS `touch-action: manipulation`

### Controller (Future?)
- **D-Pad Navigation**: Move cursor between cells
- **Button Press**: Select highlighted cell
- **A Button**: Confirm move
- **B Button**: Cancel / back

---

## Event System

### Input to Game Logic Event Flow
```
CellView.OnPointerClick()
  ├─ Sends to InputManager (if used)
  │  └─ or directly to BoardGridManager
  │
BoardGridManager.OnCellTapped(cellIndex)
  ├─ Validates input (state, turn, valid moves)
  │
GameStateManager.ExecuteMove(cellIndex)
  ├─ Processes game logic
  ├─ Updates game state
  └─ Broadcasts events:
     ├─ OnMoveMade
     ├─ OnBumpOccurred (if bump)
     └─ OnGameStateChanged
        │
        └─ BoardGridManager receives
           ├─ Animates chip movement
           ├─ Updates cell visuals
           └─ Re-enables input when done
```

### Data Passed Through Events
```csharp
// OnMoveMade event
public struct MoveData {
  public int FromCell { get; set; }
  public int ToCell { get; set; }
  public int PlayerId { get; set; }
  public bool IsBump { get; set; }
  public bool IsExit { get; set; }
}
```

---

## Touch & Responsiveness

### Touch Feedback
- **Visual**: Cell brightens when touched
- **Haptic**: Optional vibration (10-50ms pulse)
- **Audio**: Optional subtle sound effect
- **Latency**: Should feel immediate (< 100ms perceived)

### Tap Timing
- **Tap Duration**: 50-200ms is standard
- **Double-Tap**: Not used (prevents accidental double-tap)
- **Long-Press**: Not used in this game
- **Debounce**: Minimum 100ms between taps (prevent accidental double-taps)

### Visual Feedback During Touch
```
User touches cell
  ├─ 0ms: Highlight cell (scale up, brighten)
  ├─ 50ms: Maintain highlight
  └─ Release: Execute move or cancel
     ├─ Success: Animate movement
     └─ Failure: Flash red, show error
```

---

## Animation Completion Callbacks

### After Move Animation Completes
1. **Check**: Was it a bump? Show bump animation
2. **Check**: Did chip exit board? Show exit animation
3. **Check**: Does game have a winner? Show win dialog
4. **Default**: Pass turn to next player
5. **Re-enable Input**: Button input becomes active again

### Callback Implementation
```csharp
StartCoroutine(AnimateChipMove(from, to, () => {
  // Callback when animation completes
  inputEnabled = true;
  GameStateManager.ProgressTurn();
  BoardGridManager.HighlightNextValidMoves();
}));
```

---

## Error Handling & Invalid Input

### Invalid Move Handling
```
Player taps invalid cell
  ├─ Flash red (200ms)
  ├─ Show error popup: "Invalid move"
  ├─ Play error sound (optional)
  └─ Keep input enabled (let them try again)
```

### Error Messages
- "Invalid move. Please select a valid cell."
- "You need to roll first."
- "Not your turn."
- "This cell is not available."

### Visual Feedback
- **Flash Color**: Error red (#E53935)
- **Duration**: 200ms flash
- **Repeat**: Yes (one flash, clear)
- **No Block**: Input stays enabled

---

## Performance Considerations

### Input Polling
- **Frequency**: Every frame (60 FPS)
- **Optimization**: Only poll if game is active (not paused)
- **Raycast Optimization**: Use CellView colliders, no full-screen raycast

### Event Subscriptions
- **Memory**: Unsubscribe when not needed
- **Callbacks**: Keep callbacks lightweight
- **Avoid**: Heavy calculations in input handlers

### Animation Optimization
- **Duration**: Cap at 500ms (even for complex moves)
- **Particles**: Use object pooling for bump effects
- **Sounds**: Preload, don't create on demand

---

## Related Documents
- BOARD_ARCHITECTURE.md
- ASSET_INTEGRATION.md
- SPRINT_4_KICKOFF.md

---

**Status**: Complete - Production Ready
