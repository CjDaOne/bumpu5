# Coding Standards & Guidelines

## Language: C#

All code follows C# conventions and best practices for Unity projects.

---

## Naming Conventions

### Classes
```csharp
public class BoardCell { }
public class TurnManager { }
public class Game1_Bump5 { }
```
**Rule**: PascalCase, descriptive nouns, game mode classes use underscore notation (GameX_ModeName)

### Methods
```csharp
public void RollDice() { }
private bool IsValidMove(int cellIndex) { }
public void OnBumpTriggered() { }
```
**Rule**: PascalCase, descriptive verbs, prefix with `On` for event handlers

### Variables & Properties
```csharp
private int currentPlayerIndex;
public int PlayerScore { get; set; }
private bool isValidMove = false;
```
**Rule**: camelCase for local variables, PascalCase for public properties

### Constants
```csharp
private const int BOARD_CELLS = 12;
private const float CHIP_DROP_DURATION = 0.5f;
```
**Rule**: UPPER_SNAKE_CASE for all constants

### Interfaces
```csharp
public interface IGameMode { }
public interface IBoardCell { }
```
**Rule**: PascalCase with `I` prefix

---

## Documentation Standards

### Method Summaries (Required for public methods)
```csharp
/// <summary>
/// Determines if a move to the specified cell is valid for the current player.
/// </summary>
/// <param name="cellIndex">The target cell index (0-11)</param>
/// <returns>True if the move is legal, false otherwise</returns>
public bool IsValidMove(int cellIndex)
{
    // implementation
}
```

### Inline Comments (For complex logic)
```csharp
// Check if player has any chips on the board
bool hasChips = player.Chips.Count > 0;

// A bump is only valid if target cell has an opponent's chip
if (targetCell.Owner != currentPlayer && targetCell.HasChip)
{
    TriggerBump();
}
```

### Class Documentation
```csharp
/// <summary>
/// Manages the game state machine and turn flow.
/// Handles player rotation, turn phases, and win detection.
/// </summary>
public class TurnManager
{
    // implementation
}
```

---

## Code Organization

### File Structure
- **One public class per file** (except for small helper classes)
- File name matches class name: `TurnManager.cs`, `BoardCell.cs`
- Related private classes can share a file if tightly coupled

### Class Member Order
1. Constants
2. Static fields
3. Private/protected fields
4. Public properties
5. Constructors
6. Public methods
7. Private methods

Example:
```csharp
public class GameManager : MonoBehaviour
{
    private const int MAX_PLAYERS = 2;
    
    private IGameMode currentGameMode;
    private TurnManager turnManager;
    
    public int CurrentPlayerIndex { get; private set; }
    
    public void Initialize() { }
    private void Update() { }
    private void HandleInput() { }
}
```

---

## Unity-Specific Standards

### Serialization
```csharp
[SerializeField] private float moveAnimationDuration = 0.3f;
[SerializeField] private Color validMoveColor = Color.green;

public float MoveAnimationDuration => moveAnimationDuration;
```
**Rule**: Private fields with [SerializeField], expose as read-only properties if needed

### Coroutines
```csharp
public IEnumerator PlayChipDropAnimation(Chip chip, Vector3 targetPos)
{
    float elapsed = 0f;
    while (elapsed < moveAnimationDuration)
    {
        elapsed += Time.deltaTime;
        chip.Transform.position = Vector3.Lerp(chip.StartPos, targetPos, elapsed / moveAnimationDuration);
        yield return null;
    }
}
```

### Singleton Pattern (if needed)
```csharp
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
```

---

## Testing Standards

### Unit Test Naming
```csharp
[Test]
public void IsValidMove_WithOpponentChip_ReturnsTrue() { }

[Test]
public void RollDice_AlwaysReturnsValueBetween1And6() { }

[Test]
public void CheckWin_With5InARow_ReturnsTrue() { }
```
**Rule**: `MethodName_Condition_ExpectedResult`

### Test Organization
```csharp
public class TurnManagerTests
{
    private TurnManager turnManager;
    private MockPlayer player1, player2;
    
    [SetUp]
    public void Setup()
    {
        turnManager = new TurnManager();
        // initialize test data
    }
    
    [Test]
    public void MoveToNextPlayer_IncreasesPlayerIndex() { }
}
```

---

## Performance Guidelines

### Memory
- Avoid creating new arrays/lists every frame
- Use object pooling for frequently instantiated objects (chips, animations)
- Reuse collections where possible

### CPU
- Avoid expensive calculations in Update()
- Use caching for frequently accessed values
- Minimize Physics checks (use spatial hashing for board queries)

### Mobile Optimization
- Frame rate cap: 30 FPS on mobile (conserve battery)
- Texture compression: ASTC (iOS), ETC2 (Android)
- Keep draw calls under 100 per frame

---

## Error Handling

### Invalid States
```csharp
public void PlayTurn(Player player)
{
    if (player == null)
        throw new ArgumentNullException(nameof(player));
    
    if (!IsPlayerTurn(player))
        throw new InvalidOperationException($"Not {player.Name}'s turn");
    
    // proceed
}
```

### Logging
```csharp
Debug.Log($"Player {currentPlayer.Name} rolled: {roll[0]}, {roll[1]}");
Debug.LogWarning($"Invalid move attempted by {player.Name}");
Debug.LogError($"Game mode {modeName} not found");
```

---

## Git & Version Control

### Branch Naming
- `feature/board-grid-manager`
- `bugfix/dice-roll-edge-case`
- `refactor/ui-canvas-scaling`

### Commit Messages
```
[Sprint X] Brief description of change

- Detail 1
- Detail 2
```

Example:
```
[Sprint 4] Implement BoardGridManager and Cell prefabs

- Add 12-cell grid layout with adjacency detection
- Create clickable Cell prefab with highlighting
- Implement valid move visual feedback
```

### Code Review Checklist
- [ ] Code follows naming conventions
- [ ] Methods are documented
- [ ] No magic numbers (use constants)
- [ ] Unit tests included (where applicable)
- [ ] No console errors/warnings
- [ ] Performance acceptable

---

## Anti-Patterns (Avoid These)

❌ Deeply nested conditionals (use guard clauses)
```csharp
// Bad
if (player != null)
{
    if (player.HasChips)
    {
        if (IsValidMove(cell))
        {
            MoveChip();
        }
    }
}

// Good
if (player == null) return;
if (!player.HasChips) return;
if (!IsValidMove(cell)) return;

MoveChip();
```

❌ God objects (classes doing too much)  
❌ Magic numbers without constants  
❌ Ignored exceptions  
❌ Tight coupling between systems  
❌ Missing null checks  

---

## Review & Approval

All code must pass:
1. ✅ Peer review (formatting, logic)
2. ✅ Managing Engineer review (architecture, standards)
3. ✅ All tests pass
4. ✅ No new warnings/errors

---

**Last Updated**: Nov 14, 2025  
**Status**: Enforced for all code submissions
