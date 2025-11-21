# Bump U - Team Specifications & API Contracts

## TEAM 1: CORE SYSTEMS SPECIFICATIONS

### 1.1 Board System
```csharp
namespace Assets.Scripts.Core
{
    public class GameBoard
    {
        public int Width { get; } = 11;
        public int Height { get; } = 7;
        
        // Get/Set chip at position
        public Chip GetChip(int row, int col);
        public void PlaceChip(int row, int col, Chip chip);
        public void RemoveChip(int row, int col);
        
        // Get board number at position
        public int GetBoardNumber(int row, int col);
        
        // Queries
        public List<(int row, int col)> GetEmptyPositions();
        public List<(int row, int col)> GetPositionsByNumber(int number);
        public Chip[] GetChipsInLine(int row, int col, Direction direction, int length);
    }
}
```

**Deliverables:**
- Board initialization with pre-set numbers (1-6 pattern per PDF)
- Chip placement validation (empty cell check)
- Chip removal and queries
- Unit tests: 20+ test cases covering all methods

---

### 1.2 Dice System
```csharp
public class GameDice
{
    public int Roll(); // Returns 1-6
    public (int, int) RollBoth(); // Returns tuple (die1, die2)
    
    // Game-specific roll handling
    public bool IsRollValid(int roll, GameType game);
}
```

**Deliverables:**
- Deterministic random number generation (seeded for testing)
- Roll history tracking (for undo/replay)
- Unit tests: All roll scenarios per game

---

### 1.3 Chip System
```csharp
public class Chip
{
    public ChipColor Color { get; set; }
    public GameType Owner { get; set; }
    
    public enum ChipColor { Red, Blue }
}
```

**Deliverables:**
- Chip representation (color, owner)
- Pooling system for performance (chips reused)
- Unit tests: Chip state transitions

---

### 1.4 Game State Manager
```csharp
public class GameStateManager
{
    public GameType CurrentGame { get; set; }
    public Player CurrentPlayer { get; set; }
    public GamePhase Phase { get; set; } // Setup, Playing, Finished
    
    public void AdvanceTurn(int rollResult);
    public bool TryPlaceChip(int row, int col);
    public bool TryBumpChip(int row, int col);
    public GameResult GetGameResult();
    
    public event Action<GamePhaseChanged> OnPhaseChanged;
    public event Action<PlayerChanged> OnPlayerChanged;
}

public enum GamePhase { Setup, Rolling, Placing, Bumping, TurnEnd, GameOver }
public enum GameResult { RedWins, BlueWins, Draw, InProgress }
```

**Deliverables:**
- Turn management system
- State transitions with validation
- Event system for UI updates
- Save/load game state
- Unit tests: State machine completeness

---

## TEAM 2: GAME LOGIC SPECIFICATIONS

### 2.1 Game Interface (All games implement)
```csharp
public interface IGameRules
{
    GameType GameType { get; }
    
    // Turn handling
    bool CanPlaceChip(int row, int col, int rollResult);
    bool CanBumpChip(int row, int col, int rollResult);
    bool IsValidMove(GameAction action, int rollResult);
    
    // Win conditions
    bool CheckWin(Player player);
    bool CheckDraw();
    string GetWinPhrase(Player player);
    
    // Scoring
    int CalculateScore(Player player);
    
    // Special rules
    bool IsRollSpecial(int roll); // #5, #6 handling
}
```

---

### 2.2 Game #1 Implementation (5-in-a-row)
```csharp
public class Game1Rules : IGameRules
{
    // Win detection: 5 chips in a row (horizontal, vertical, diagonal)
    public bool CheckWin(Player player) 
    {
        // Check all directions from each player chip
        // Return true if any line of 5 found
    }
    
    public string GetWinPhrase(Player player) => "BUMP U BOX 5";
    public bool IsRollSpecial(int roll) => roll == 6; // Lose turn
}
```

**Requirements:**
- 5-in-a-row detection in all directions
- Unit tests: 50+ scenarios (corners, edges, middle)
- Win phrase verification

---

### 2.3 Game #2 Implementation (Krazy 6)
```csharp
public class Game2Rules : IGameRules
{
    // Win: All 6 chips on board + roll #6
    public bool CheckWin(Player player)
    {
        // Check if player has 6 chips AND last roll was #6
    }
    
    public string GetWinPhrase(Player player) => "BUMP U KRAZY 6";
    public bool IsRollSpecial(int roll) => roll == 6; // Win condition or bump
}
```

---

### 2.4 Game #3 Implementation (Pass the Chip)
```csharp
public class Game3Rules : IGameRules
{
    // Win: Box in center #5, earn points
    public bool CheckWin(Player player)
    {
        // Check if all 8 surrounding positions of center #5 are covered
        // No winner until board section is complete
    }
    
    public int CalculateScore(Player player)
    {
        // Chips on board + opponent chips bumped
    }
    
    public string GetWinPhrase(Player player) => "BUMP U";
}
```

---

### 2.5 Game #4 Implementation (#5 Bumping)
```csharp
public class Game4Rules : IGameRules
{
    // Win: First to 5 chips on board (vs #5 as opponent)
    public bool CheckWin(Player player) => player.ChipsOnBoard == 5;
    
    public string GetWinPhrase(Player player) => "BUMP U and #5";
    
    public bool IsRollSpecial(int roll)
    {
        // #5 = opponent bumps player
        // #6 = lose turn
        // #4 = can choose #4 or lower
    }
}
```

---

### 2.6 Game #5 Implementation (Solitary)
```csharp
public class Game5Rules : IGameRules
{
    // Single player: Fill board OR get bumped off
    // #6 on dice removes chips (opponent)
    // #5+#6 together = #5 helps
    
    public bool CheckWin(Player player) => BoardIsFull();
    public bool CheckDraw() => NoChipsRemaining();
}
```

---

### 2.7 Bumping System (All games)
```csharp
public class BumpingSystem
{
    public bool TryBump(int row, int col, int rollResult, Player bumper)
    {
        // Validate:
        // 1. Opponent chip at position
        // 2. Roll matches board number
        // 3. Bumper said "BUMP U" (or game-specific phrase)
        
        if (!ValidateBumpPhrase(bumper)) return false;
        if (!ValidateBumpNumber(row, col, rollResult)) return false;
        
        RemoveChip(row, col);
        bumper.Score += 1;
        return true;
    }
    
    private bool ValidateBumpPhrase(Player bumper)
    {
        // Check phrase buffer from player input
        // Timeout: 5 seconds after roll
    }
}
```

---

## TEAM 3: UI/UX SPECIFICATIONS

### 3.1 Scene Structure
```
Scenes/
├── MainMenu.unity
├── GameSelect.unity
├── Game1.unity
├── Game2.unity
├── Game3.unity
├── Game4.unity
├── Game5.unity
└── GameOver.unity
```

---

### 3.2 HUD Elements (In-game canvas)
```csharp
public class GameHUD : MonoBehaviour
{
    // Display
    public Text CurrentPlayerText; // "Red's Turn" / "Blue's Turn"
    public Text DiceResultText; // "You rolled: 4"
    public Text ScoreText; // Game-specific scoring
    public Button BumpButton; // Only shown when bump is valid
    
    // Input handling
    public void OnBoardClicked(int row, int col);
    public void OnBumpPhraseSpoken(string phrase);
    public void OnBumpButtonClicked();
}
```

---

### 3.3 Win Screen
```csharp
public class WinScreen : MonoBehaviour
{
    public Text WinnerText; // "Red wins!"
    public Text WinPhraseRequiredText; // Show phrase if not said
    public Text ScoreFinalText; // Final score
    
    public Button PlayAgainButton;
    public Button MenuButton;
}
```

---

## TEAM 4: AUDIO/POLISH SPECIFICATIONS

### 4.1 Audio Requirements
```
Audio/SFX/
├── dice_roll.wav          (300ms)
├── chip_place.wav         (200ms)
├── chip_bump.wav          (400ms, with impact sound)
├── win_fanfare.wav        (2s)
└── ui_click.wav           (100ms)

Audio/Music/
├── menu_bg.mp3            (loop)
└── game_bg.mp3            (loop, game-specific)
```

---

### 4.2 Animation Requirements
- Dice roll animation: 0.5s (spin effect)
- Chip place: 0.3s fade-in
- Chip bump: 0.4s bounce out + particle burst
- Win screen: 1.0s scale-up with shine effect

---

## TEAM 5: QA SPECIFICATIONS

### 5.1 Test Coverage Requirements
- Core Systems: >95% code coverage
- Game Logic: 100% rule paths tested
- UI: All button/input paths functional
- Integration: All 5 games playable end-to-end

### 5.2 Rule Verification Checklist
```
Game #1:
- [ ] 5-in-a-row detection works horizontally
- [ ] 5-in-a-row detection works vertically
- [ ] 5-in-a-row detection works diagonally
- [ ] Must say "BUMP U BOX 5" to win
- [ ] #6 roll loses turn
- [ ] Wrong number bump allows opponent response

Game #2:
- [ ] Can only place chips after rolling #6
- [ ] Win requires all 6 chips + #6 roll
- [ ] #6 roll bumps opponent if they have <6 chips
- [ ] Must say "BUMP U KRAZY 6" to win

... (similar for Games 3-5)
```

---

## API CONTRACTS SUMMARY

All teams must follow these contracts:

1. **Naming:** `IGameRules`, `GameBoard`, `BumpingSystem`, `GameStateManager`
2. **Events:** Use `Action<T>` delegates for notifications
3. **Null Safety:** Never pass null; return empty collections
4. **Error Handling:** Throw `InvalidOperationException` only for logic errors
5. **Testing:** All public methods must have unit tests
6. **Documentation:** XML comments on all public APIs
