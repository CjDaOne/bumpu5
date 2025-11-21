# Bump U Box 5: 3D Game Board Design Specification
## Updated Project Requirements (Jan 1, 2026 onwards)

**Date**: January 1, 2026  
**Status**: 🟡 **NEW SPECIFICATION - REQUIRES IMPLEMENTATION**  
**Impact Level**: HIGH (Board system redesign)  
**Teams Affected**: Board Engineer, Gameplay Engineer, UI Engineer

---

## EXECUTIVE SUMMARY

The Bump U Box 5 project must transition from the current **11x7 2D circular board** to a **5x5 3D square grid system** with physics-based chip placement and updated game mechanics.

**Key Changes**:
- Board geometry: 5x5 grid (25 spaces) instead of 11x7
- Numbering scheme: 1-5 only (with rotating pattern)
- 3D visualization: Chips use physics colliders
- Special focus: Center #5 node (8 adjacent spaces)
- Game logic: Updated victory conditions

---

## I. PROJECT IMPACT ASSESSMENT

### Current vs. New Architecture

| Aspect | Current (2D) | New (3D) | Impact |
|--------|------------|----------|--------|
| **Board Geometry** | 11x7 circular | 5x5 square grid | Major restructure |
| **Numbering** | 0-11 (12 spaces) | 1-5 (rotating) | Logic update |
| **Visualization** | 2D flat layout | 3D physics-based | Complete redesign |
| **Chip Placement** | Direct placement | Physics colliders | New system |
| **Center Space** | Cell #5 | Row 3, Col 3 (#5) | Architecture change |
| **Game Logic** | Existing modes | Updated mechanics | Rule changes |

### Affected Code Components

- ❌ **BoardModel.cs** - Rewrite (5x5 instead of 11x7)
- ❌ **BoardGridManager.cs** - Rewrite (3D physics instead of 2D UI)
- ❌ **CellView.cs** - Rewrite (3D GameObject instead of UI element)
- ❌ **ChipVisualizer.cs** - Update (physics-based movement)
- ❌ **Game*.cs files** - Update (new victory conditions)
- ⚠️ **DiceManager.cs** - Update (handle #6 logic changes)
- ⚠️ **TurnManager.cs** - Potential updates (turn flow changes)

---

## II. 3D BOARD GEOMETRY & GRID SYSTEM

### A. Grid Structure

**Dimensions**: **5x5 Grid System**
- Total spaces: 25 nodes
- Each node: Identifiable 3D GameObject
- Each node: Can hold a chip
- Purpose: Support victory conditions (5-in-a-row, boxing center #5)

**Implementation Approach**:
```csharp
// Pseudocode structure
[5, 5] grid of nodes
foreach row (0-4):
    foreach col (0-4):
        create Node(row, col)
        assign number value (1-5 pattern)
        add Collider component
        store reference in NodeData
```

### B. Numbering Sequence (1-5 Only)

**Critical**: Numbers must be **1 through 5 ONLY** (no 6 on board, as #6 is special action).

**Numbering Pattern** (5x5 Grid with row/column shifting):

```
        Column 1   Column 2   Column 3   Column 4   Column 5
Row 1:     1          2          3          4          5
Row 2:     2          3          4          5          1
Row 3:     3          4          5          1          2
Row 4:     4          5          1          2          3
Row 5:     5          1          2          3          4
```

**Pattern Logic**:
- Number at position (row, col) = ((row + col) % 5) + 1
- This creates a diagonal shift pattern
- Ensures all numbers 1-5 appear in each row and column

**Implementation**:
```csharp
for (int row = 0; row < 5; row++) {
    for (int col = 0; col < 5; col++) {
        int nodeNumber = ((row + col) % 5) + 1;
        grid[row, col].SetNumber(nodeNumber);
    }
}
```

### C. CRITICAL: Center #5 Space

**Position**: Row 3, Column 3 (0-indexed: Row 2, Column 2)

**Importance**: This is the **KEY OBJECTIVE** for Game #3 (PASS THE CHIP).

**Requirements**:
- ✅ Visually distinct (different color, shader, or marking)
- ✅ Must track its own state (boxed in vs. not)
- ✅ Must reference its 8 adjacent spaces
- ✅ Adjacent spaces must be detected automatically

**8 Adjacent Spaces** (surrounding center #5):
```
[3,4]  [4,4]  [5,4]
[3,3]  [#5]   [5,3]    <- Center at Row 3, Col 3
[3,2]  [4,2]  [5,2]
```

**Boxing Detection Logic**:
```csharp
bool IsCenter5BoxedIn() {
    // Check all 8 adjacent spaces
    // If all 8 spaces have opponent chips, return true
    // This means the center #5 is "boxed in"
    return CheckAdjacentSpaces();
}
```

---

## III. 3D GAME ASSETS (Prefabs)

### A. Playing Chips (3D Prefabs)

**Specifications**:
- **Total**: 30 chips (15 Red, 15 Blue)
- **Shape**: 3D object (sphere, cylinder, or custom cube)
- **Physics**: Must have Collider component (for placement detection)
- **Material**: Distinct color per player (Red vs. Blue)
- **Size**: Proportional to grid (roughly 0.8x node size to leave gaps)

**Prefab Structure**:
```
Chip (GameObject)
├── MeshRenderer (visual)
├── Collider (physics interaction)
├── ChipController.cs (script)
└── ChipData (contains player, state)
```

**ChipController Requirements**:
- Track owner (Red/Blue)
- Track state (Active/Inactive/Bumped)
- Detect collisions (bumping)
- Respond to placement/removal events

### B. Game Dice (3D Prefabs)

**Specifications**:
- **Quantity**: 2 dice (for dual-roll mechanics)
- **Type**: 6-sided cubes
- **Physics**: Enable Rigidbody for rolling physics
- **Outcome**: Numbers 1-6 visible on each face

**Dice Implementation**:
```csharp
// Dice roll mechanics
public class DiceRoller {
    public int Roll() {
        // Physics-based: throw die with impulse
        // Detect landing face
        // Return number 1-6
    }
    
    // Or simple random:
    public int Roll() {
        return Random.Range(1, 7);
    }
}
```

**Visual Requirements**:
- Each face labeled with number (1-6)
- Realistic rolling animation
- Clear outcome display to players

---

## IV. CORE GAME LOGIC UPDATES (C#)

### A. Board Data Structure (BoardModel.cs Rewrite)

**New Structure**:
```csharp
public class BoardModel {
    private Node[,] grid; // 5x5 grid
    
    public BoardModel() {
        grid = new Node[5, 5];
        InitializeGrid();
    }
    
    private void InitializeGrid() {
        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 5; col++) {
                int nodeNumber = ((row + col) % 5) + 1;
                grid[row, col] = new Node(row, col, nodeNumber);
            }
        }
    }
    
    public Node GetNode(int row, int col) => grid[row, col];
    
    public bool CanPlaceChip(int number, Player player) {
        // Find all nodes with this number that are empty
        // Return true if at least one exists
    }
    
    public void PlaceChip(int row, int col, Chip chip) {
        // Place chip on node
        // Trigger collision detection
    }
}
```

**Node Class**:
```csharp
public class Node {
    public int Row { get; }
    public int Col { get; }
    public int Number { get; }
    public Chip OccupiedBy { get; set; }
    
    public List<Node> AdjacentNodes { get; } = new();
    
    public Node(int row, int col, int number) {
        Row = row;
        Col = col;
        Number = number;
    }
    
    public void SetAdjacentNodes(List<Node> adjacent) {
        AdjacentNodes.AddRange(adjacent);
    }
}
```

### B. Placement & Covering Rules

**Current State**: Player rolls dice (1-5)  
**Action**: Player selects a node with matching number and places chip  
**Validation**:
```csharp
public bool TryPlaceChip(int diceNumber, int row, int col, Player player) {
    Node node = grid[row, col];
    
    // Validate: Node must have matching number
    if (node.Number != diceNumber) {
        return false; // Invalid placement
    }
    
    // Validate: Node must be empty
    if (node.OccupiedBy != null) {
        return false; // Space already taken
    }
    
    // Place chip
    Chip chip = new Chip(player);
    node.OccupiedBy = chip;
    
    // Check for bumping (if opponent chip nearby)
    CheckBumpingConditions(node, player);
    
    return true;
}
```

### C. Updated Bumping Mechanic

**Rules**:
1. Player rolls dice number (1-5)
2. If that number is covered by opponent chip, player CAN bump
3. To execute bump, player MUST say "BUMP U"
4. Bumped chip is removed from board

**Implementation**:
```csharp
public bool TryBumpChip(int diceNumber, bool declaredBump) {
    // Find opponent chip on node with this number
    Node targetNode = FindNodeWithNumber(diceNumber);
    
    if (targetNode.OccupiedBy == null) {
        return false; // No chip to bump
    }
    
    if (targetNode.OccupiedBy.Owner == currentPlayer) {
        // Own chip - bumping self loses turn
        RemoveChip(targetNode);
        currentPlayer.LoseTurn();
        return true;
    }
    
    // Opponent chip
    if (!declaredBump) {
        // Must declare "BUMP U" to bump
        return false;
    }
    
    // Execute bump
    RemoveChip(targetNode);
    return true;
}
```

### D. Handling #6 Roll (Special Action)

**Rules vary by game**:

**Game #1, #3, #4** - Roll #6 = Lose Turn:
```csharp
if (diceRoll == 6) {
    currentPlayer.LoseTurn();
    return;
}
```

**Game #2 (KRAZY 6)** - #6 needed to START:
```csharp
if (game == GameType.KRAZY_6) {
    if (playerChipsOnBoard < 6 && diceRoll == 6) {
        // Can start placing chips
        EnableChipPlacement();
    } else if (playerChipsOnBoard >= 6 && diceRoll == 6) {
        // All chips on board - can declare win
        if (declaredWin) {
            DeclareWinner();
        }
    }
}
```

**Game #5 (SOLITARY)** - #6 = Opponent (removes chips):
```csharp
if (game == GameType.SOLITARY) {
    if (diceRoll == 6) {
        // #6 is your opponent
        RemoveLastChip();
    }
    if (dice1 == 6 && dice2 == 6) {
        // Double #6 - remove 2 chips
        RemoveLastChip();
        RemoveLastChip();
    }
}
```

---

## V. UPDATED VICTORY CONDITIONS

### Game #1: BUMP "U" BOX 5 (5-in-a-row)

**Victory Condition**:
- Get **5 chips in a row** (horizontal, vertical, or diagonal)
- Say **"BUMP "U" BOX 5"**
- Win is verified by system

**Implementation**:
```csharp
public class Game1_Bump5 : IGameMode {
    public bool CheckVictory(Player player) {
        // Check all rows for 5 in a row
        if (CheckRows(player)) return true;
        
        // Check all columns for 5 in a row
        if (CheckColumns(player)) return true;
        
        // Check all diagonals for 5 in a row
        if (CheckDiagonals(player)) return true;
        
        return false;
    }
}
```

### Game #2: BUMP "U" KRAZY 6 (6 chips + #6 roll)

**Victory Condition**:
- Place **6 chips** on board (any positions)
- Roll a **#6**
- Say **"BUMP "U" KRAZY 6"**

**Implementation**:
```csharp
public class Game2_Krazy6 : IGameMode {
    public bool CheckVictory(Player player) {
        bool has6Chips = player.ChipsOnBoard.Count == 6;
        bool rolledSix = lastDiceRoll == 6;
        bool declaredWin = playerDeclaration == "BUMP U KRAZY 6";
        
        return has6Chips && rolledSix && declaredWin;
    }
}
```

### Game #3: BUMP "U" PASS THE CHIP (Box in center #5)

**Victory Condition**:
- **Center #5 space is boxed in** (surrounded by 8 chips)
- Center space must be opponent's chip or empty
- Surrounding 8 spaces must be YOUR chips

**Implementation**:
```csharp
public class Game3_PassTheChip : IGameMode {
    public bool CheckVictory(Player player) {
        Node center = board.GetNode(2, 2); // Row 3, Col 3 (0-indexed: 2,2)
        
        // Get all 8 adjacent nodes
        List<Node> adjacent = GetAdjacentNodes(center);
        
        // All 8 must have this player's chips
        foreach (Node adj in adjacent) {
            if (adj.OccupiedBy == null || adj.OccupiedBy.Owner != player) {
                return false;
            }
        }
        
        return true;
    }
}
```

### Game #4: BUMP "U" and #5 (5 chips + declaration)

**Victory Condition**:
- Place **5 chips** on board
- Say **"BUMP "U" and #5"**
- #5 plays as opponent (removes chips if you roll #5)

**Implementation**:
```csharp
public class Game4_BumpUAnd5 : IGameMode {
    public bool CheckVictory(Player player) {
        bool has5Chips = player.ChipsOnBoard.Count == 5;
        bool declaredWin = playerDeclaration == "BUMP U and #5";
        
        return has5Chips && declaredWin;
    }
}
```

### Game #5: BUMP "U" SOLITARY (Fill board without being bumped)

**Victory Condition**:
- Place chips on board avoiding bumps
- Fill **entire 5x5 board** (25 chips total)
- Single-player mode (no opponent)
- #6 is the opponent (removes last chip placed)

**Implementation**:
```csharp
public class Game5_Solitary : IGameMode {
    public bool CheckVictory(Player player) {
        // Count total chips on board
        int chipCount = 0;
        foreach (Node node in board.AllNodes) {
            if (node.OccupiedBy != null) {
                chipCount++;
            }
        }
        
        // Board is full = victory
        return chipCount == 25;
    }
}
```

---

## VI. IMPLEMENTATION ROADMAP

### Phase 1: Board Redesign (1 week)

**Board Engineer** - Primary owner

1. **Day 1-2**: Rewrite BoardModel.cs
   - Create 5x5 grid structure
   - Implement node numbering (1-5 pattern)
   - Create Node class
   - Add center #5 tracking

2. **Day 3-4**: Create 3D Board Visualization (BoardGridManager.cs rewrite)
   - Create 3D grid layout in scene
   - Create Node GameObjects (cubes or custom shapes)
   - Add visual distinction for center #5
   - Add Collider components to nodes

3. **Day 5**: Testing & Validation
   - Verify grid layout correct
   - Verify numbering correct
   - Test node placement
   - Validate center #5 detection

### Phase 2: 3D Assets (1 week)

**Board Engineer** - Asset creation

1. **Day 1-2**: Create Chip Prefab (3D)
   - 3D sphere or cube
   - Material with player color
   - Collider component
   - ChipController script

2. **Day 3-4**: Create Dice Prefabs (3D)
   - 6-sided cube
   - Labeled faces (1-6)
   - Physics Rigidbody
   - Roll mechanics

3. **Day 5**: Testing & Validation
   - Chips render correctly
   - Dice physics work
   - Placement detection works

### Phase 3: Game Logic Updates (1-2 weeks)

**Gameplay Engineer** - Primary owner

1. **Week 1**:
   - Update Game*.cs files (new victory conditions)
   - Update DiceManager (handle #6 per game)
   - Update TurnManager (new turn flow)
   - Update BumpingLogic

2. **Week 2**:
   - Implement center #5 boxing detection
   - Test all 5 game victory conditions
   - Test bumping mechanics
   - Test #6 handling

### Phase 4: UI Updates (1 week)

**UI Engineer** - Adapt existing UI

1. **Day 1-2**: Adapt HUD for 5x5 board
2. **Day 3-4**: Update game mode descriptions
3. **Day 5**: Test UI with new board

### Phase 5: Integration & Testing (1-2 weeks)

**All Teams**:
1. Integration of all systems
2. Full game testing
3. Edge case validation
4. Performance optimization
5. Final QA testing

---

## VII. TEAM ASSIGNMENTS

### Board Engineer
- ✅ **Primary responsibility**: Board redesign & 3D visualization
- Rewrite BoardModel.cs (5x5 grid)
- Rewrite BoardGridManager.cs (3D layout)
- Create Node system
- Create Chip & Dice prefabs
- Update CellView/ChipVisualizer for 3D

### Gameplay Engineer
- ✅ **Primary responsibility**: Game logic updates
- Update all Game*.cs files (new victory conditions)
- Update DiceManager (#6 handling)
- Implement center #5 boxing detection
- Update bumping mechanics
- Testing & validation

### UI Engineer
- Update HUD for 5x5 board
- Update game mode descriptions
- Adapt menus if needed
- Test UI integration

### QA Lead
- Create test cases for new board
- Test all 5 game victory conditions
- Test bumping mechanics
- Test #6 handling per game
- Performance testing
- Edge case testing

### Build Engineer
- Monitor build health
- Test all platforms (Android, iOS, WebGL)
- Performance profiling
- Optimization

---

## VIII. CRITICAL SUCCESS CRITERIA

### Must Have (MVP)
- ✅ 5x5 grid system working
- ✅ Numbering 1-5 correct
- ✅ Center #5 detection working
- ✅ Chip placement on nodes
- ✅ Bumping mechanics working
- ✅ #6 handling per game correct
- ✅ All 5 game victory conditions working

### Should Have
- ✅ 3D physics-based chip placement
- ✅ Realistic dice rolling animation
- ✅ Visual feedback for bumping
- ✅ Center #5 visual distinction

### Nice to Have
- ✅ Advanced physics interactions
- ✅ Particle effects
- ✅ Sound effects for bumping
- ✅ Animations for chip removal

---

## IX. RISKS & MITIGATION

### Risk 1: Board redesign scope
**Impact**: HIGH  
**Mitigation**: Start with basic 5x5 grid, add 3D later

### Risk 2: Game logic complexity
**Impact**: MEDIUM  
**Mitigation**: Test each game mode individually

### Risk 3: Performance with 3D
**Impact**: MEDIUM  
**Mitigation**: Profile early, optimize as needed

### Risk 4: Physics interactions
**Impact**: MEDIUM  
**Mitigation**: Keep physics simple (colliders only)

---

## X. TIMELINE ESTIMATE

| Phase | Duration | Start | End |
|-------|----------|-------|-----|
| Board Redesign | 1 week | Jan 2 | Jan 8 |
| 3D Assets | 1 week | Jan 9 | Jan 15 |
| Game Logic | 1-2 weeks | Jan 16 | Jan 29 |
| UI Updates | 1 week | Jan 30 | Feb 5 |
| Integration/Testing | 1-2 weeks | Feb 6 | Feb 19 |
| **TOTAL** | **5-6 weeks** | **Jan 2** | **Feb 19** |

---

## XI. QUESTIONS FOR CLARIFICATION

1. **Board Rendering**: Should the 5x5 grid be 3D isometric, top-down, or custom angle?
2. **Chip Size**: What size should chips be relative to grid nodes?
3. **Physics**: Should chips roll/slide with physics, or snap-to-grid placement?
4. **Victory Animation**: What visual/audio feedback for victories?
5. **Backward Compatibility**: Drop old 11x7 board or maintain both?

---

## APPENDIX: CODE TEMPLATES

### BoardModel.cs (New Structure)
```csharp
using UnityEngine;
using System.Collections.Generic;

public class BoardModel {
    private Node[,] grid = new Node[5, 5];
    
    public void Initialize() {
        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 5; col++) {
                int number = ((row + col) % 5) + 1;
                grid[row, col] = new Node(row, col, number);
            }
        }
        SetupAdjacentNodes();
    }
    
    private void SetupAdjacentNodes() {
        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 5; col++) {
                List<Node> adjacent = GetAdjacentNodes(row, col);
                grid[row, col].SetAdjacentNodes(adjacent);
            }
        }
    }
    
    private List<Node> GetAdjacentNodes(int row, int col) {
        List<Node> adjacent = new();
        for (int r = row - 1; r <= row + 1; r++) {
            for (int c = col - 1; c <= col + 1; c++) {
                if (r >= 0 && r < 5 && c >= 0 && c < 5 && (r != row || c != col)) {
                    adjacent.Add(grid[r, c]);
                }
            }
        }
        return adjacent;
    }
    
    public Node GetNode(int row, int col) => grid[row, col];
    public Node GetCenter() => grid[2, 2];
}

public class Node {
    public int Row { get; }
    public int Col { get; }
    public int Number { get; }
    public Chip OccupiedBy { get; set; }
    public List<Node> AdjacentNodes { get; } = new();
    
    public Node(int row, int col, int number) {
        Row = row;
        Col = col;
        Number = number;
    }
    
    public void SetAdjacentNodes(List<Node> adjacent) {
        AdjacentNodes.AddRange(adjacent);
    }
}
```

---

**Document Status**: DRAFT FOR TEAM REVIEW  
**Next Step**: Team lead kickoff meeting to discuss implementation approach

