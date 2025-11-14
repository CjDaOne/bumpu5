# Sprint 5 Kickoff - UI Framework & HUD System

**Sprint Duration**: Week 5 (Dec 12 - Dec 19, 2025)  
**Lead**: UI Engineer Agent  
**Dependency**: Sprint 1-4 complete & approved  
**Goal**: Create complete HUD, buttons, scoreboard, and popup system  

---

## What We're Building

The user interface layer—all buttons, text displays, menus, and popups that the player innaging teracts with during gameplay. This includes:

- Dice roll button & animation
- BUMP button (context-sensitive)
- Scoreboard (live score updates)
- Popup messages (PENALTY, PASS THE CHIP, TAKE IT OFF)
- Game instructions overlay
- Mode selection screen

By end of this sprint, all UI is functional and responsive to GameStateManager events.

---

## Core Components

### 1. HUDManager.cs (New)

Main orchestrator for all on-screen UI during gameplay.

```csharp
public class HUDManager : MonoBehaviour
{
    // Game state reference
    private GameStateManager gameStateManager;
    private GameState gameState;
    
    // UI Elements
    [SerializeField] private DiceRollButton diceButton;
    [SerializeField] private BumpButton bumpButton;
    [SerializeField] private DeclareWinButton winButton;
    [SerializeField] private ScoreboardDisplay scoreboard;
    [SerializeField] private PopupManager popupManager;
    [SerializeField] private PhaseIndicator phaseIndicator;
    
    // Setup
    public void Initialize(GameStateManager gsm);
    
    // State listeners
    public void OnGamePhaseChanged(GamePhase phase);
    public void OnPlayerChanged(Player currentPlayer);
    public void OnDiceRolled(int[] roll);
    public void OnGameWon(Player winner);
    public void OnInvalidAction(string reason);
    
    // Button callbacks
    public void OnDiceButtonClicked();
    public void OnBumpButtonClicked(int targetCell);
    public void OnWinButtonClicked();
    public void OnRulesButtonClicked();
    
    // UI updates
    private void UpdateButtonState();
    private void UpdateScoreboard();
    private void UpdatePhaseIndicator();
}
```

---

### 2. DiceRollButton.cs (New)

Button to roll the dice. Animates the roll result.

```csharp
public class DiceRollButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text rollText;
    [SerializeField] private Image diceImage;
    [SerializeField] private ParticleSystem rollFX;
    
    private DiceAnimation diceAnimator;
    
    public event System.Action OnRollClicked;
    
    // State
    public void SetInteractable(bool interactable);
    public void SetRollText(int[] roll);
    
    // Animation
    public IEnumerator AnimateDiceRoll(int[] diceRoll);
    public void ShowRollResult(int[] roll);
    
    private void OnButtonClicked()
    {
        StartCoroutine(AnimateDiceRoll(null)); // actual roll from manager
        OnRollClicked?.Invoke();
    }
}
```

---

### 3. BumpButton.cs (New)

Context-sensitive button to bump opponent chip.

```csharp
public class BumpButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text labelText = "BUMP";
    [SerializeField] private CanvasGroup canvasGroup;
    
    public event System.Action<int> OnBumpClicked;  // targetCell
    
    private int targetCellIndex = -1;
    
    // State
    public void ShowBumpOption(int targetCell);
    public void HideBumpOption();
    public void SetInteractable(bool interactable);
    
    private void OnButtonClicked()
    {
        if (targetCellIndex >= 0)
            OnBumpClicked?.Invoke(targetCellIndex);
    }
}
```

---

### 4. DeclareWinButton.cs (New)

Button to declare victory (only shows if player has 5 in a row).

```csharp
public class DeclareWinButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text labelText = "I WON!";
    
    public event System.Action OnWinClicked;
    
    // State
    public void SetInteractable(bool canWin);
    public void Show();
    public void Hide();
    
    private void OnButtonClicked()
    {
        OnWinClicked?.Invoke();
    }
}
```

---

### 5. ScoreboardDisplay.cs (New)

Live scoreboard showing both players' scores.

```csharp
public class ScoreboardDisplay : MonoBehaviour
{
    [SerializeField] private Text player1NameText;
    [SerializeField] private Text player1ScoreText;
    [SerializeField] private Image player1Indicator;
    
    [SerializeField] private Text player2NameText;
    [SerializeField] private Text player2ScoreText;
    [SerializeField] private Image player2Indicator;
    
    private Player[] players;
    
    // Setup
    public void Initialize(Player[] allPlayers);
    
    // Updates
    public void UpdateScores(Player[] players);
    public void HighlightCurrentPlayer(int playerIndex);
    
    private void OnScoreChanged(Player player)
    {
        UpdateScoreText(player);
    }
}
```

---

### 6. PopupManager.cs (New)

Centralized system for showing popup messages.

```csharp
public class PopupManager : MonoBehaviour
{
    [SerializeField] private PopupPrefab popupPrefab;
    
    // Popup types
    public void ShowPenalty(Player player, int points, string reason);
    public void ShowPassTheChip(Player fromPlayer, Player toPlayer);
    public void ShowTakeItOff(Player player);
    public void ShowRollAgain(Player player);
    public void ShowYouWon(Player winner);
    public void ShowGameOver(Player winner);
    
    // Animation
    private IEnumerator ShowPopupAnimated(PopupPrefab popup, float duration);
    
    // Generic
    public void ShowCustom(string title, string message, float duration);
}
```

---

### 7. PopupPrefab.cs (New)

Individual popup implementation.

```csharp
public class PopupPrefab : MonoBehaviour
{
    [SerializeField] private Text titleText;
    [SerializeField] private Text messageText;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private RectTransform rectTransform;
    
    // Setup
    public void Initialize(string title, string message);
    
    // Animation
    public IEnumerator AnimateAppear();
    public IEnumerator AnimateDisappear();
    public IEnumerator AnimateAutoClose(float duration);
}
```

---

### 8. GameModeSelectionScreen.cs (New)

Menu to select which of the 5 game modes to play.

```csharp
public class GameModeSelectionScreen : MonoBehaviour
{
    [SerializeField] private ModeButton[] modeButtons = new ModeButton[5];
    [SerializeField] private Text selectedModeDescription;
    [SerializeField] private Button startGameButton;
    
    private IGameMode selectedMode;
    
    // Setup
    public void Initialize();
    
    // Selection
    public void OnModeSelected(int modeID);
    public void OnStartGame();
    
    // Display
    private void DisplayModeDescription(IGameMode mode);
    
    public event System.Action<IGameMode> OnGameModeSelected;
}
```

---

### 9. ModeButton.cs (New)

Individual button for each game mode.

```csharp
public class ModeButton : MonoBehaviour
{
    [SerializeField] private int modeID;
    [SerializeField] private Button button;
    [SerializeField] private Text modeNameText;
    [SerializeField] private Image modeIcon;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private IGameMode gameMode;
    
    public event System.Action<int> OnClicked;
    
    // Setup
    public void Initialize(IGameMode mode);
    
    // State
    public void SetSelected(bool selected);
    public void SetInteractable(bool interactable);
    
    private void OnButtonClicked()
    {
        OnClicked?.Invoke(modeID);
    }
}
```

---

### 10. PhaseIndicator.cs (New)

Displays current game phase (Rolling, Placing, Bumping, etc.)

```csharp
public class PhaseIndicator : MonoBehaviour
{
    [SerializeField] private Text phaseText;
    [SerializeField] private Image phaseIcon;
    
    private Dictionary<GamePhase, string> phaseDisplayNames;
    
    // Setup
    public void Initialize();
    
    // Updates
    public void SetPhase(GamePhase phase);
    
    private void UpdateDisplay(GamePhase phase);
}
```

---

## File Structure (Created in Sprint 5)

```
/Assets/Scripts/UI/
  HUDManager.cs                 (NEW - orchestrator)
  DiceRollButton.cs             (NEW - roll dice)
  BumpButton.cs                 (NEW - bump opponent)
  DeclareWinButton.cs           (NEW - declare win)
  ScoreboardDisplay.cs          (NEW - score tracking)
  PopupManager.cs               (NEW - popup system)
  PopupPrefab.cs                (NEW - individual popup)
  GameModeSelectionScreen.cs    (NEW - mode select)
  ModeButton.cs                 (NEW - individual mode)
  PhaseIndicator.cs             (NEW - game phase display)
  RulesScreen.cs                (NEW - display rules)
  SettingsMenu.cs               (NEW - game settings)

/Assets/Prefabs/UI/
  HUD.prefab                    (NEW - complete HUD)
  DiceButton.prefab             (NEW)
  BumpButton.prefab             (NEW)
  WinButton.prefab              (NEW)
  Popup.prefab                  (NEW - popup prefab)
  ScoreBoardDisplay.prefab      (NEW)
  ModeButton.prefab             (NEW)

/Assets/Scenes/
  GameModeSelection.unity       (NEW - mode select screen)
  Gameplay.unity                (UPDATED - add HUD)
  RulesScreen.unity             (NEW - rules display)

/Assets/Art/UI/
  button-roll-dice.png          (To be created by artist)
  button-bump.png               (To be created by artist)
  button-win.png                (To be created by artist)
  popup-background.png          (To be created by artist)
  mode-selector-bg.png          (To be created by artist)
```

---

## Interaction Flow (Example: Dice Roll)

```
Player taps Dice Button
    ↓
DiceRollButton.OnButtonClicked()
    ↓
Invoke OnRollClicked event
    ↓
HUDManager.OnDiceButtonClicked()
    ↓
Call GameStateManager.RollDice()
    ↓
GameStateManager fires OnDiceRolled(roll) event
    ↓
DiceRollButton.AnimateDiceRoll(roll)
    ↓
Display dice results with animation
    ↓
HUDManager.UpdateButtonState()
    ↓
Activate Placing phase buttons
```

---

## Unit Tests Required

### DiceRollButtonTests.cs

```
- SetInteractable_EnablesButton()
- OnButtonClicked_FiresEvent()
- AnimateDiceRoll_CompletesAnimation()
- ShowRollResult_DisplaysCorrectly()
```

### ScoreboardDisplayTests.cs

```
- Initialize_DisplaysBothPlayers()
- UpdateScores_RefreshesText()
- HighlightCurrentPlayer_IndicatesCorrectly()
```

### PopupManagerTests.cs

```
- ShowPenalty_DisplaysPenaltyPopup()
- ShowPassTheChip_DisplaysTransfer()
- ShowYouWon_DisplaysWinMessage()
- AutoClose_RemovesPopupAfterDuration()
```

### GameModeSelectionScreenTests.cs

```
- Initialize_DisplaysAllModes()
- OnModeSelected_HighlightsSelectedMode()
- OnStartGame_FiresGameModeSelected()
- DisplayModeDescription_ShowsCorrectText()
```

---

## Canvas Setup

```
Canvas
├─ HUD Panel (Top)
│  ├─ DiceRollButton
│  ├─ BumpButton
│  ├─ DeclareWinButton
│  └─ PhaseIndicator
├─ ScoreboardPanel (Top Right)
│  ├─ Player1Score
│  └─ Player2Score
├─ GameBoard (Center)
└─ PopupContainer (Center)
   └─ (Popups spawn here)
```

---

## Success Criteria

✅ All buttons functional and interactable  
✅ HUD updates with game state  
✅ Popups display correctly with animations  
✅ Scoreboard live-updates with scores  
✅ Mode selection screen lets users pick mode  
✅ All 15+ unit tests pass  
✅ Code follows CODING_STANDARDS.md  
✅ Full documentation  
✅ Responsive to all game phases  
✅ Touch-friendly (44px+ buttons on mobile)  

---

## Performance Targets

- Button click response: < 100ms
- Popup animation: Smooth (60 FPS)
- Score update: Instant
- Canvas rendering: < 5ms

---

## Next Sprint Preview (Sprint 6)

Sprint 6 integrates all UI systems:

- Main menu
- Settings menu
- Game flow (menu → mode select → gameplay)
- Resume/restart functionality

---

**Sprint Start Date**: Dec 12, 2025  
**Estimated Completion**: Dec 19, 2025  
**Owner**: UI Engineer Agent  
**Dependency**: Sprint 1-4 approved ✅
