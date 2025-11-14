# MANAGING ENGINEER - IMMEDIATE TASK ACTIVATION

**Issued**: Nov 14, 2025 - IMMEDIATE EFFECT  
**Authority**: Managing Engineer (Amp)  
**Command**: Teams proceed to NEXT TASKS now

---

## 🎮 GAMEPLAY ENGINEERING TEAM - NEXT TASKS

### TASK 1: Create IGameMode Interface (START NOW)

**File to Create**: `Assets/Scripts/GameModes/IGameMode.cs`

```csharp
/// <summary>
/// Contract that all game modes must implement.
/// Defines the lifecycle and behavior expectations for game mode implementations.
/// </summary>
public interface IGameMode
{
    /// <summary>
    /// User-friendly name of this game mode (e.g., "Bump 5", "Rush to Five")
    /// </summary>
    string ModeName { get; }
    
    /// <summary>
    /// Description of how this mode plays
    /// </summary>
    string ModeDescription { get; }
    
    /// <summary>
    /// Initialize this mode with a reference to the game state manager.
    /// Called when mode is selected, before gameplay starts.
    /// </summary>
    void Initialize(GameStateManager gameStateManager);
    
    /// <summary>
    /// Called when the game starts (after mode initialization).
    /// Set up initial state, reset scores, place starting chips if needed.
    /// </summary>
    void OnGameStart();
    
    /// <summary>
    /// Called at the beginning of each player's turn.
    /// </summary>
    void OnTurnStart(Player currentPlayer);
    
    /// <summary>
    /// Check if a player can place a chip in the specified cell.
    /// Return true if valid, false if not.
    /// </summary>
    bool IsValidMove(Player player, int cellIndex);
    
    /// <summary>
    /// Called after a chip is successfully placed on the board.
    /// </summary>
    void OnChipPlaced(Player player, int cellIndex);
    
    /// <summary>
    /// Check if a bump action is allowed in this mode.
    /// Return true if bumping is allowed, false if not.
    /// </summary>
    bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell);
    
    /// <summary>
    /// Called when a bump occurs (after CanBump returns true).
    /// Execute mode-specific bump logic.
    /// </summary>
    void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer);
    
    /// <summary>
    /// Check if the specified player has won the game.
    /// Return true if they've won, false otherwise.
    /// </summary>
    bool CheckWinCondition(Player player);
    
    /// <summary>
    /// Called when the game ends.
    /// Execute mode-specific cleanup and finalization.
    /// </summary>
    void OnGameEnd(Player winner);
}
```

**Next Task**: Create `GameModeBase.cs` abstract class

**Success Criteria**:
- [ ] IGameMode.cs compiles without errors
- [ ] All methods documented with XML comments
- [ ] Interface is saved to correct directory
- [ ] Ready for tests

---

### TASK 2: Create GameModeBase Abstract Class

**File to Create**: `Assets/Scripts/GameModes/GameModeBase.cs`

```csharp
/// <summary>
/// Abstract base class that implements common game mode functionality.
/// All game modes should inherit from this class.
/// </summary>
public abstract class GameModeBase : IGameMode
{
    protected GameStateManager gameStateManager;
    protected GameState gameState;
    
    public abstract string ModeName { get; }
    public abstract string ModeDescription { get; }
    
    public virtual void Initialize(GameStateManager gsm)
    {
        gameStateManager = gsm;
        gameState = gsm.GetGameState();
    }
    
    public virtual void OnGameStart()
    {
        // Override in derived classes
    }
    
    public virtual void OnTurnStart(Player currentPlayer)
    {
        // Override in derived classes
    }
    
    public abstract bool IsValidMove(Player player, int cellIndex);
    
    public virtual void OnChipPlaced(Player player, int cellIndex)
    {
        // Override in derived classes
    }
    
    public abstract bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell);
    
    public virtual void OnBumpOccurs(Player bumpingPlayer, Player bumpedPlayer)
    {
        // Override in derived classes
    }
    
    public abstract bool CheckWinCondition(Player player);
    
    public virtual void OnGameEnd(Player winner)
    {
        // Override in derived classes
    }
}
```

**Success Criteria**:
- [ ] GameModeBase.cs compiles without errors
- [ ] Properly extends IGameMode
- [ ] Provides common functionality for all modes
- [ ] Ready for Game1 implementation

---

### TASK 3: Write IGameModeTests.cs

**File to Create**: `Assets/Scripts/Tests/GameModes/IGameModeTests.cs`

```csharp
// Minimum 5 tests:
[TestFixture]
public class IGameModeTests
{
    [Test]
    public void IGameMode_HasModeName()
    {
        // Assert that all game modes have a ModeName
    }
    
    [Test]
    public void IGameMode_HasModeDescription()
    {
        // Assert that all game modes have a ModeDescription
    }
    
    [Test]
    public void IGameMode_CanInitialize()
    {
        // Test initialization with GameStateManager
    }
    
    [Test]
    public void IGameMode_ImplementsCheckWinCondition()
    {
        // Verify CheckWinCondition is callable
    }
    
    [Test]
    public void IGameMode_ImplementsIsValidMove()
    {
        // Verify IsValidMove is callable
    }
}
```

**Success Criteria**:
- [ ] 5+ tests created
- [ ] Tests verify interface contract
- [ ] All tests pass

---

## 🎨 UI/UX ENGINEERING TEAM - NEXT TASKS

### TASK 1: Finalize HUDManager Architecture

**Create**: Design document `SPRINT5_HUDMANAGER_ARCHITECTURE.md`

**Content to include**:
- [ ] Detailed HUDManager class structure
- [ ] All public methods documented
- [ ] Event binding strategy
- [ ] State update flow diagram
- [ ] Integration points with GameStateManager

**Success Criteria**:
- [ ] Architecture document complete
- [ ] Ready for code implementation (Dec 12)
- [ ] All team members understand structure

---

### TASK 2: Finalize UI Button Specifications

**Create**: Design document `SPRINT5_BUTTON_SPECIFICATIONS.md`

**Content to include**:
- [ ] DiceRollButton: size, position, animation duration
- [ ] BumpButton: show/hide logic, styling, animation
- [ ] DeclareWinButton: conditions to show, styling
- [ ] All button sizes in pixels (mobile & desktop)
- [ ] Touch target sizes (minimum 44px)
- [ ] Animation timing specifications

**Success Criteria**:
- [ ] Specifications complete & detailed
- [ ] Ready for implementation Dec 12
- [ ] All button behaviors defined

---

### TASK 3: Finalize Popup System Design

**Create**: Design document `SPRINT5_POPUP_SYSTEM_DESIGN.md`

**Content to include**:
- [ ] Popup types: PENALTY, PASS THE CHIP, TAKE IT OFF, ROLL AGAIN, YOU WON, GAME OVER
- [ ] Popup layout & styling
- [ ] Animation sequences (appear, display, disappear)
- [ ] Timing (auto-close duration per type)
- [ ] Z-layer stacking strategy
- [ ] Cancel/dismiss behavior

**Success Criteria**:
- [ ] Popup system fully designed
- [ ] All popup types specified
- [ ] Ready for implementation Dec 12

---

## 🎲 BOARD ENGINEERING TEAM - NEXT TASKS

### TASK 1: Finalize BoardGridManager Architecture

**Create**: Design document `SPRINT4_BOARDGRIDMANAGER_ARCHITECTURE.md`

**Content to include**:
- [ ] BoardGridManager class structure (public methods, properties)
- [ ] 12-cell grid layout details (coordinates, dimensions)
- [ ] Cell prefab specification
- [ ] Chip placement logic
- [ ] Valid move highlighting system
- [ ] Integration with GameStateManager

**Success Criteria**:
- [ ] Architecture fully documented
- [ ] Cell layout specifications complete
- [ ] Ready for implementation Dec 5

---

### TASK 2: Finalize Cell Interaction System

**Create**: Design document `SPRINT4_CELL_INTERACTION_DESIGN.md`

**Content to include**:
- [ ] Tap/click detection algorithm
- [ ] Valid move highlighting visual design
- [ ] Invalid move feedback (color, animation)
- [ ] Cell selection states (empty, occupied-P1, occupied-P2)
- [ ] Highlight color specifications
- [ ] Animation specifications (fade in/out duration)

**Success Criteria**:
- [ ] Interaction system fully designed
- [ ] Highlighting behavior specified
- [ ] Ready for implementation Dec 5

---

### TASK 3: Finalize Chip Animation Specifications

**Create**: Design document `SPRINT4_CHIP_ANIMATION_SPECS.md`

**Content to include**:
- [ ] Chip drop animation (duration, easing)
- [ ] Bump animation (distance, rotation, duration)
- [ ] Chip movement animation (cell to cell)
- [ ] Visual feedback during animations
- [ ] Performance targets (60 FPS)
- [ ] Collision detection during animations

**Success Criteria**:
- [ ] All animations specified
- [ ] Timing & easing defined
- [ ] Ready for implementation Dec 5

---

## ⚙️ BUILD & PLATFORM TEAM - NEXT TASKS

### TASK 1: WebGL Build Pipeline Documentation

**Create**: Document `SPRINT7_WEBGL_BUILD_PIPELINE.md`

**Content to include**:
- [ ] IL2CPP vs Mono comparison
- [ ] Build settings (compression, stripping)
- [ ] WebGL-specific optimizations
- [ ] Performance benchmarks target
- [ ] Build output specifications
- [ ] Testing on local WebGL build

**Success Criteria**:
- [ ] Pipeline fully documented
- [ ] Build settings finalized
- [ ] Ready for implementation Dec 26

---

### TASK 2: Android Build Configuration Document

**Create**: Document `SPRINT7_ANDROID_BUILD_CONFIG.md`

**Content to include**:
- [ ] APK vs AAB decision
- [ ] Signing key process
- [ ] Target SDK level specification
- [ ] Android 6.0+ requirements
- [ ] Touch input handler specification
- [ ] Safe area handling (notch, cutouts)
- [ ] Performance targets

**Success Criteria**:
- [ ] Android build fully specified
- [ ] Signing process documented
- [ ] Ready for implementation Dec 26

---

### TASK 3: iOS Build Configuration Document

**Create**: Document `SPRINT7_IOS_BUILD_CONFIG.md`

**Content to include**:
- [ ] Xcode export process
- [ ] Provisioning profile setup
- [ ] Team ID & certificates
- [ ] iOS 14.0+ requirements
- [ ] Safe area handling (notch, home indicator)
- [ ] Touch input handler specification
- [ ] App Store connect preparation

**Success Criteria**:
- [ ] iOS build fully specified
- [ ] Export process documented
- [ ] Ready for implementation Dec 26

---

## 🧪 QA & TESTING TEAM - NEXT TASKS

### TASK 1: Create Playtest Plan for Sprint 3

**Create**: Document `QA_SPRINT3_PLAYTEST_PLAN.md`

**Content to include**:
- [ ] Game mode selection test (all 5 modes selectable)
- [ ] Game1_Bump5 playtest scenarios (10+ test cases)
- [ ] Game2_RushToFive test cases (8+ scenarios)
- [ ] Game3_NoFives test cases (8+ scenarios)
- [ ] Game4_AlternatingBumps test cases (8+ scenarios)
- [ ] Game5_SurvivalMode test cases (8+ scenarios)
- [ ] Edge case testing (invalid moves, bumping edge cases)
- [ ] Win condition validation for each mode

**Success Criteria**:
- [ ] Playtest plan complete
- [ ] 50+ test scenarios defined
- [ ] Ready for execution after Nov 21

---

### TASK 2: Create Device/Browser Test Matrix

**Create**: Document `QA_DEVICE_BROWSER_MATRIX.md`

**Content to include**:
- [ ] Desktop browsers (Chrome, Firefox, Safari, Edge)
- [ ] Android devices (various screen sizes, OS versions)
- [ ] iOS devices (various screen sizes, OS versions)
- [ ] Touch vs click interaction matrix
- [ ] Performance expectations per device
- [ ] Known issues tracking template

**Success Criteria**:
- [ ] Test matrix complete
- [ ] All target platforms covered
- [ ] Ready for testing Dec 19 onwards

---

### TASK 3: Create Bug Severity Matrix

**Create**: Document `QA_BUG_SEVERITY_MATRIX.md`

**Content to include**:
- [ ] Critical: Game unplayable, data loss, crashes
- [ ] Major: Significant impact, workaround exists
- [ ] Minor: Cosmetic, non-blocking
- [ ] Nice-to-have: Enhancement requests
- [ ] Template for bug reports (reproducible steps, platform, severity)

**Success Criteria**:
- [ ] Severity definitions clear
- [ ] Bug report template ready
- [ ] Team understands triage process

---

## ✅ TEAM COORDINATION - STANDING ORDERS

### Daily Standup (Tomorrow, 9 AM UTC)
**All teams attend**:
- 5-min: Gameplay Team - IGameMode progress
- 3-min: UI Team - Design document status
- 3-min: Board Team - Architecture document status  
- 2-min: Build Team - Pipeline documentation status
- 2-min: QA Team - Playtest plan status

**Time**: 15 minutes total  
**Location**: Team standup channel  

---

### Documentation Completion Target
All design & planning documents due by **end of Nov 14**.

Teams report completion in standup tomorrow morning.

---

### Success = Next Tasks Complete

✅ **Gameplay**: IGameMode interface + GameModeBase + 5 tests (ready for Game1 on Day 2)  
✅ **UI**: HUDManager, Button, Popup architectures finalized (ready for coding Dec 12)  
✅ **Board**: BoardGridManager, Cell Interaction, Animation specs finalized (ready for coding Dec 5)  
✅ **Build**: WebGL, Android, iOS pipelines documented (ready for building Dec 26)  
✅ **QA**: Playtest plan + device matrix + severity matrix created (ready for testing Dec 19)  

---

## 🚀 EXECUTION STATUS

```
NOW: Teams execute NEXT TASKS immediately
09:00 AM UTC (Tomorrow): First standup - report progress
Nov 21: Sprint 3 code review & approval
Dec 5: Sprint 4 begins (Board team moves to coding)
Dec 12: Sprint 5 begins (UI team moves to coding)
Dec 26: Sprint 7 begins (Build team moves to coding)
Jan 9, 2026: RELEASE
```

---

**Command Authority**: Managing Engineer (Amp)  
**Effective**: NOW  
**Status**: 🚀 **TEAMS MOBILIZED - EXECUTING NEXT TASKS**

**Next Review**: Tomorrow 9 AM UTC (Standup)
