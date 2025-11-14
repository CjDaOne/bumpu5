# Bump U Box - Technical Architecture

## 1. High-Level Vision

**Project Goal**: Create a multi-platform digital version of Bump U Box for Android, iOS, and Web Browser (WebGL).

**Technology Stack**:
- **Engine**: Unity (2022 LTS or newer)
- **Language**: C# with VS Code
- **Platforms**: WebGL, Android, iOS
- **Design Philosophy**: One codebase → many platforms

**Core Principles**:
- Modular game modes with shared interface
- Clear separation: UI, game state, board logic
- Test-driven logic for all game rules
- Data-driven board configuration
- Touch-friendly, responsive UI

---

## 2. System Architecture

### System 1: Core Gameplay
- Turn Manager
- Dice Manager (1-die, 2-die rolls, edge cases)
- Valid Move Generator
- Bump System & Validation
- Rule Interpreter (per game mode)
- Win Detection
- Game State Machine

### System 2: Board
- Grid Manager (12-cell grid)
- Cell Metadata & Ownership
- Chip Placement & Movement
- Visual Highlighting
- Interaction Layer (tap/drag detection)

### System 3: Game Modes (5 implementations)
- Game1_Bump5.cs
- Game2_Krazy6.cs
- Game3_PassTheChip.cs
- Game4_BumpUAnd5.cs
- Game5_Solitary.cs

All implement: `IGameMode` interface

### System 4: UI & UX
- Dice Roll Button
- BUMP Button
- Declare Win Button
- Scoreboard Display
- Popup Prompts (PENALTY, PASS THE CHIP, TAKE IT OFF)
- Game Instructions/Rules Menu
- Game Mode Selection Screen

### System 5: Platform Layer
- Mobile Input Handler (iOS/Android)
- Browser Input Handler (Mouse/Touch)
- Resolution Scaling & Safe Areas
- Performance & Memory Settings
- Device-specific optimizations

### System 6: DevOps & Tools
- GitHub Repository & Branch Strategy
- GitHub Projects Board
- CI Pipeline (automated WebGL builds)
- Build profiles for all platforms
- Playtest build distribution

---

## 3. Folder Structure

```
/Assets
  /Scripts
    /Core              (Gameplay logic, turn/dice/rules)
    /Board             (Board model, cells, grid)
    /GameModes         (5 game mode implementations)
    /UI                (HUD, menus, popups)
    /Managers          (Game manager, scene manager)
    /Platform          (Input handlers, device optimization)
    /Tests             (Unit tests for all systems)
  /Prefabs
    /Board
    /UI
    /Chips
    /Effects
  /Scenes
    /MainMenu
    /Gameplay
    /GameOverScreen
  /Art
    /Board
    /Chips
    /UI
    /Effects
  /Audio
    /SFX
    /Music
  /Resources
    /BoardConfigs
    /GameModeData
```

---

## 4. Core Development Phases

### Phase 1: Foundation (Non-Visual Logic)
- BoardCell, Chip, Player classes (pure C#)
- Board adjacency & 5-in-a-row detection
- Dice roll system (all edge cases)
- Turn manager state machine
- IGameMode interface + all 5 implementations
- 100% unit test coverage for logic

### Phase 2: Board Integration
- Grid manager & cell layout
- Chip placement & movement animation
- Valid move highlighting
- Click/tap detection & move execution

### Phase 3: Game Modes & Rules
- Full implementation of all 5 modes
- Mode-specific UI elements
- Bump triggers & penalties
- Win detection per mode

### Phase 4: Visual Polish
- Dice animation
- Chip drop/bump animations
- Board art & theming
- Sound effects & music

### Phase 5: UI/UX & Menus
- Main menu & mode selection
- HUD (scoreboard, buttons)
- Rules/instructions screen
- Settings (volume, difficulty)

### Phase 6: Multi-Platform Build
- WebGL export & optimization
- Android build & play store setup
- iOS build & app store setup
- Testing on actual devices

### Phase 7: QA & Polishing
- Comprehensive playtest iteration
- Bug fixes & edge case handling
- Performance optimization
- Final release preparation

---

## 5. Class Hierarchy (Key Classes)

```
IGameMode
├─ Game1_Bump5
├─ Game2_Krazy6
├─ Game3_PassTheChip
├─ Game4_BumpUAnd5
└─ Game5_Solitary

BoardModel (pure C#)
├─ BoardCell
├─ Chip
├─ Player
└─ Grid (adjacency logic)

GameManager (Singleton)
├─ CurrentGameMode (IGameMode)
├─ BoardManager
├─ DiceManager
├─ TurnManager
└─ UIManager

InputHandler (abstract)
├─ MobileInputHandler
└─ BrowserInputHandler
```

---

## 6. Platform-Specific Adjustments

### WebGL
- IL2CPP backend for performance
- Texture compression (ASTC/ETC2)
- Memory: 512–1024 MB allocation
- Canvas scaling & DPI awareness
- Mouse + Touch support

### Mobile (iOS/Android)
- Safe area layout (notch handling)
- Button sizing for thumb operation
- FPS limiter (30 FPS for battery)
- Device orientation lock (portrait)
- Network-aware (WiFi checks for cloud save)

---

## 7. Build Profiles

- **Development**: Full debug logging, no optimizations
- **Staging**: Optimized, no analytics
- **Production**: Full optimization, analytics enabled

---

## 8. Quality Standards

- **Code Style**: C# naming conventions (PascalCase classes, camelCase variables)
- **Documentation**: Inline comments for complex logic, method summaries
- **Testing**: Unit tests for all rules, integration tests for game modes
- **Performance**: Target 60 FPS on mid-range devices, 30 FPS floor
- **Accessibility**: Large touch targets, high contrast UI

---

**Last Updated**: Nov 14, 2025  
**Status**: Approved for development
