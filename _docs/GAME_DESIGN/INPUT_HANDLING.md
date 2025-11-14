# Input Handling

**Created**: Nov 14, 2025  
**Owner**: Board Engineer  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Overview

Input Handling is the layer that converts user interactions (taps, clicks, keyboard) into game actions. It bridges the UI/Board layers with GameStateManager, ensuring responsive and reliable player interactions while preventing invalid or duplicate inputs.

---

## Tap/Click Detection

### Detection Architecture

```
Input Event (Touch/Mouse)
    ↓
InputHandler.DetectTap(position)
    ↓
RaycastUI(position) → Find target cell
    ↓
Is cell registered? → Yes/No
    ↓
[Yes] CellView.OnTapped()
      ↓
      BoardGridManager.OnCellTapped(cellIndex)
      ↓
      GameStateManager.ProcessMoveAttempt(cellIndex)
      
[No] Ignore input (feedback: none)
```

### Platform Differences

#### Mouse Input (WebGL/Desktop)
- **Detection**: OnMouseUp event or Pointer.Click
- **Coordinates**: Screen space pixels
- **Hover Support**: Enable hover state on buttons
- **Double-Click**: Ignore (debounce to single click)
- **Right-Click**: Ignore (no context menu needed)

#### Touch Input (Android/iOS)
- **Detection**: OnTouchPhase.Ended
- **Coordinates**: Touch screen coordinates
- **Multi-Touch**: Ignore (single touch only)
- **Press Duration**: Detect long press (1.5s) for alternate actions (future)
- **Tap Duration**: Short press (< 200ms) = normal tap

### Implementation

```csharp
public class BoardInputHandler : MonoBehaviour
{
    private BoardGridManager boardGridManager;
    private GraphicRaycaster uiRaycaster;
    private bool inputEnabled = true;
    
    void Update()
    {
        if (!inputEnabled) return;
        
        // Web/Desktop
        if (Input.GetMouseButtonUp(0))
        {
            HandleTap(Input.mousePosition);
        }
        
        // Mobile (if not detected by UI system)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended)
            {
                HandleTap(touch.position);
            }
        }
    }
    
    private void HandleTap(Vector2 screenPosition)
    {
        // Raycast to find cell under tap
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = screenPosition;
        
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        uiRaycaster.Raycast(eventData, raycastResults);
        
        // Find first CellView in results
        foreach (RaycastResult result in raycastResults)
        {
            CellView cellView = result.gameObject.GetComponent<CellView>();
            if (cellView != null)
            {
                cellView.OnTapped();
                return;
            }
        }
    }
    
    public void DisableInput() => inputEnabled = false;
    public void EnableInput() => inputEnabled = true;
}
```

---

## Move Validation Flow

### Validation Process

```
Player taps Cell X
    ↓
Is input enabled? (not animating, not waiting)
    ├─ No → Ignore, show feedback "Please wait..."
    └─ Yes → Continue
    ↓
Is it player's turn?
    ├─ No → Ignore, show feedback "Not your turn"
    └─ Yes → Continue
    ↓
BoardGridManager.OnCellTapped(cellIndex)
    ↓
GameStateManager.ValidateMove(cellIndex)
    ├─ Check: Is move in valid moves list?
    ├─ Check: Can cell be moved to? (not home)
    ├─ Check: What action? (normal move, bump, etc.)
    └─ Return: Valid or Invalid
    ↓
[Valid] 
├─ Execute move in game logic
├─ Animate chip movement
├─ Update board state
└─ Proceed to next phase

[Invalid]
├─ Show feedback: "Invalid move selected"
├─ Flash cell red for 200ms
├─ Remain in same phase (try again)
└─ Keep valid moves highlighted
```

### Validation Rules

**Move is VALID if**:
- Cell is in the valid moves list from dice roll
- Cell is empty OR contains opponent's chip (bump)
- Cell is not player's home
- Game is not in waiting/paused state

**Move is INVALID if**:
- Cell is NOT in valid moves list
- Cell contains player's own chip (can't move over own piece)
- Player is not active
- Game is in waiting/paused state

---

## Valid Moves Highlighting

### Display Sequence

**1. Dice Roll Complete** (200ms)
   - GameStateManager calculates valid destination cells
   - Broadcasts `OnValidMovesChanged` event

**2. BoardGridManager.HighlightValidMoves()** (immediate)
   - Iterate through valid cell list
   - Set each cell highlight state to `VALID_MOVE`
   - Activate pulsing animation on each cell
   - Update cell button interactability

**3. Visual State** (persistent until move)
   - Valid cells: Light blue glow (#DBEAFE, 70% opacity)
   - Valid cells: Pulsing animation (0.6 → 0.9 opacity, 1000ms loop)
   - All other cells: Normal state
   - Player cannot tap cells outside valid list

**4. Player Selects Cell** (conditional)
   - If valid: Proceed to movement
   - If invalid: Flash red, show error, keep highlights

**5. Move Completes** (500ms after animation)
   - Clear all highlights
   - Update cell visual states
   - Begin next phase

---

### Highlight Implementation

```csharp
public class HighlightState
{
    public enum State { None, ValidMove, Selected, Invalid }
    
    public static void SetHighlight(CellView cell, State state)
    {
        switch (state)
        {
            case State.None:
                cell.highlightOverlay.enabled = false;
                cell.selectionBorder.enabled = false;
                break;
                
            case State.ValidMove:
                cell.highlightOverlay.enabled = true;
                cell.highlightOverlay.color = new Color(219, 234, 254, 0.7f); // #DBEAFE @ 70%
                StartCoroutine(PulseAnimation(cell.highlightOverlay));
                break;
                
            case State.Selected:
                cell.selectionBorder.enabled = true;
                cell.selectionBorder.color = new Color(37, 99, 235, 1f); // #2563EB
                break;
                
            case State.Invalid:
                StartCoroutine(FlashInvalid(cell));
                break;
        }
    }
    
    private IEnumerator PulseAnimation(Image highlight)
    {
        float elapsed = 0f;
        float duration = 1f; // 1 second pulse
        
        while (true)
        {
            float t = (Mathf.Sin(elapsed * Mathf.PI * 2f) + 1f) / 2f;
            float opacity = Mathf.Lerp(0.6f, 0.9f, t);
            Color c = highlight.color;
            c.a = opacity;
            highlight.color = c;
            
            elapsed += Time.deltaTime;
            if (elapsed > duration) elapsed -= duration;
            
            yield return null;
        }
    }
    
    private IEnumerator FlashInvalid(CellView cell)
    {
        Image bg = cell.backgroundImage;
        Color originalColor = bg.color;
        Color flashColor = new Color(254, 226, 226, 1f); // #FEE2E2
        
        float elapsed = 0f;
        float duration = 0.2f;
        
        while (elapsed < duration)
        {
            bg.color = Color.Lerp(originalColor, flashColor, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        bg.color = originalColor;
    }
}
```

---

## Cell Interaction States

### State Diagram

```
IDLE (Waiting for input)
├─ Player taps cell
│
├─ [Valid move] → SELECTED
│  │
│  └─ Animation plays → ANIMATING
│     │
│     └─ Animation complete → IDLE (next player)
│
└─ [Invalid move] → INVALID (flash 200ms)
   │
   └─ Flash complete → IDLE (same player, retry)

HIGHLIGHTED (Valid move, waiting)
├─ Player taps cell
│  ├─ [Valid] → SELECTED
│  └─ [Invalid] → INVALID
│
└─ Animation timeout (5s, if implemented) → IDLE

SELECTED (Player has selected)
├─ Animation starts → ANIMATING
│
└─ Animation complete → IDLE

ANIMATING (Chip moving between cells)
├─ [Normal move] → Update source/dest cells
├─ [Bump] → Move opponent's chip
└─ Complete → Broadcast to GameStateManager

INVALID (Move rejected)
├─ Flash red (200ms)
└─ Return to IDLE (with valid moves still highlighted)
```

---

## Input Blocking

### When to Block

**Block input during**:
- Chip animation (300ms per move)
- Bump animation (200ms per bump)
- Win animation (1000ms)
- Phase transition (200ms)
- Loading/saving state

**Block input NOT during**:
- Valid move highlighting
- Player waiting (allow pause/menu)
- After move animation completes

### Implementation

```csharp
public class InputBlocker
{
    private bool inputBlocked = false;
    
    public void BlockInput(float duration)
    {
        inputBlocked = true;
        boardInputHandler.DisableInput();
        StartCoroutine(UnblockAfterDelay(duration));
    }
    
    private IEnumerator UnblockAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);
        inputBlocked = false;
        boardInputHandler.EnableInput();
    }
    
    public bool IsInputAllowed() => !inputBlocked;
}
```

### Recovery Mechanism

If input is blocked due to animation but animation doesn't complete (error):
- Hard timeout: 5 seconds (fail-safe)
- Manual unblock: OnGameStateChanged event
- Error notification: "Game recovered from animation timeout"

---

## Platform-Specific Input

### Mouse (WebGL/Desktop)

**Primary Method**: `Input.GetMouseButtonUp(0)`  
**Coordinates**: `Input.mousePosition` (screen space)

**Features**:
- Hover states on buttons
- No multi-touch
- Instant response

**Considerations**:
- Debounce double-clicks (require > 200ms between clicks)
- Ignore right-click
- Cursor changes (pointer on hover, default when disabled)

### Touch (Android/iOS)

**Primary Method**: `Input.GetTouch()` with `TouchPhase.Ended`  
**Coordinates**: `touch.position` (screen space)

**Features**:
- Multi-touch ignored (use first touch only)
- Pressure/force ignored (binary: tap or not)
- Delta position available (not used for taps)

**Considerations**:
- Debounce duration: < 200ms = tap, > 200ms = long press (future)
- Ignore simultaneous touches (limit to 1 active touch)
- Handle touch input gracefully on tablets

### Controller (Future)

Not implemented for launch. Placeholder:
- Dpad: Navigate menu
- Button A: Confirm selection
- Button B: Cancel/back

---

## Event System

### Input → Board → Game State Flow

```
Input Event (Touch/Click)
    ↓
BoardInputHandler.HandleTap(position)
    ↓
CellView.OnTapped()
    ↓
BoardGridManager.OnCellTapped(cellIndex)
    ↓
GameStateManager.ProcessMoveAttempt(cellIndex)
    ├─ ValidateMove()
    ├─ ExecuteMove()
    └─ BroadcastEvents()
    
GameStateManager Events:
├─ OnMoveValidated
├─ OnMoveExecuted
├─ OnAnimationStarted
├─ OnAnimationComplete
└─ OnPhaseChanged
    ↓
BoardGridManager.UpdateUI()
    ├─ UpdateCellStates()
    ├─ PlayAnimation()
    └─ HighlightValidMoves() [for next player]
```

### Event Subscriptions

```csharp
// BoardInputHandler subscribes to:
GameStateManager.OnPhaseChanged += UpdateInputState;
GameStateManager.OnAnimationStarted += BlockInput;
GameStateManager.OnAnimationComplete += EnableInput;
GameStateManager.OnGamePaused += DisableInput;

// BoardInputHandler broadcasts to:
public event System.Action<int> OnCellSelected;
OnCellSelected += GameStateManager.ProcessMoveAttempt;
```

---

## Gesture Support (Mobile, Future)

Not required for MVP but framework ready:
- **Swipe**: Navigate menus (left/right)
- **Long Press**: Context menu (1.5s hold)
- **Pinch**: Zoom board (not needed for fixed layout)
- **Rotate**: Not applicable (portrait locked)

---

## Testing Checklist

- [ ] Tap detection works on all platforms
- [ ] Hover states visible on desktop
- [ ] Input blocks during animations
- [ ] Input unblocks after animations
- [ ] Valid move highlighting updates correctly
- [ ] Invalid move feedback displays (red flash)
- [ ] No double-tap execution
- [ ] Touch input responsive (< 100ms latency)
- [ ] Mobile multi-touch ignored
- [ ] Menu navigation works
- [ ] Pause input works
- [ ] Safe area input handling (mobile)

---

## Related Documents

- BOARD_ARCHITECTURE.md
- ASSET_INTEGRATION.md
- SPRINT_4_BOARD_PREP.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Implementation
