# Asset Integration
**Created**: Nov 14, 2025  
**Owner**: Board Engineer  
**Status**: ACTIVE

---

## Asset Import Workflow

### Step 1: Import PNG Sprite Sheet
- **Location**: Assets/Board/Sprites/
- **Naming**: `board_cells.png`, `board_chips.png`
- **Format**: PNG with transparency (RGBA)
- **Resolution**: 2x resolution recommended (will scale down)

### Step 2: Configure Import Settings
```
Inspector → Texture Type: Sprite (2D and UI)
          → Sprite Mode: Multiple (if sheet)
          → Pixels Per Unit: 100 (standard)
          → Filter Mode: Point (pixel-perfect, no blur)
          → Compression: Compressed (unless VRAM limited)
```

### Step 3: Slice Sprites
- **Slicing Tool**: Sprite Editor → Automatic Slice
- **Grid Cell Size**: 100×100px (cells), 80×80px (chips)
- **Padding**: 0px (no extra space)
- **Apply** and save

### Step 4: Create Prefabs
- **Cell Prefab**: Drag sliced cell sprite → create prefab
- **Chip Prefab**: Drag sliced chip sprite → create prefab
- **Location**: Assets/Board/Prefabs/
- **Apply** components (BoxCollider, Animator, etc.)

### Step 5: Reference in BoardGridManager
- **Method**: Assign via Inspector or via Resources.Load()
- **Fallback**: Built-in placeholder if missing
- **Testing**: Verify sprites appear correctly in Editor

---

## Naming Conventions

### Sprite Names (After Slicing)
- **Cell Sprites**: `Cell_Empty`, `Cell_Base`, `BoardCell_Variant1`
- **Chip Sprites**: `Chip_Player1`, `Chip_Player2`, `Chip_Player3`, `Chip_Player4`
- **Highlight**: `Highlight_Ring`, `Highlight_Glow`
- **Animations**: `Cell_Highlight`, `Chip_Move`, `Chip_Bump`

### Prefab Names
- **Cell Prefab**: `CellView.prefab`
- **Chip Prefab**: `ChipView.prefab`
- **Board Prefab**: `BoardGridManager.prefab` (container)

### Animation Clip Names
- **State Naming**: `{Component}_{State}` (e.g., `Cell_HighlightEnter`)
- **Action Naming**: `{Component}_{Action}` (e.g., `Chip_Move`)
- **Effect Naming**: `{Effect}_{Type}` (e.g., `Particle_Bump`)

### Folder Structure
```
Assets/
├─ Board/
│  ├─ Sprites/
│  │  ├─ board_cells.png (sprite sheet)
│  │  ├─ board_chips.png (sprite sheet)
│  │  └─ board_effects.png (highlight, effects)
│  ├─ Prefabs/
│  │  ├─ CellView.prefab
│  │  ├─ ChipView.prefab
│  │  └─ BoardGridManager.prefab
│  ├─ Animations/
│  │  ├─ Cell/
│  │  │  ├─ Cell_HighlightEnter.anim
│  │  │  ├─ Cell_HighlightExit.anim
│  │  │  └─ Highlight_Pulse.anim
│  │  ├─ Chip/
│  │  │  ├─ Chip_Move.anim
│  │  │  ├─ Chip_Bump.anim
│  │  │  └─ Chip_Exit.anim
│  │  └─ Controllers/
│  │     ├─ CellViewController.controller
│  │     └─ ChipViewController.controller
│  └─ Materials/
│     ├─ Cell_Default.mat
│     ├─ Cell_Highlight.mat
│     └─ Chip_Shadow.mat
```

---

## Sprite Slicing Specifications

### Cell Sprite Sheet
- **Grid Size**: 100×100px cells
- **Columns**: 6 (cells per row)
- **Rows**: 2 (2 rows of 6)
- **Total Sprites**: 12 (one per cell position)
- **Offset**: 0px (no padding)
- **Padding**: 0px between sprites

### Chip Sprite Sheet
- **Grid Size**: 80×80px chips
- **Columns**: 4 (Player 1-4)
- **Rows**: 1 (one row)
- **Total Sprites**: 4 (one per player color)
- **Offset**: 0px
- **Padding**: 0px

### Highlight Sprite
- **Size**: 100×100px (to fit cell)
- **Style**: Glow ring or gradient
- **Purpose**: Visual feedback for valid moves
- **Offset**: Center of cell

---

## Prefab Structure

### CellView Prefab
```
CellView (GameObject)
├─ Transform
│  ├─ Position: Calculated by BoardGridManager
│  ├─ Scale: 1,1,1 (unit size)
│  └─ Rotation: 0,0,0
│
├─ SpriteRenderer (Background cell)
│  ├─ Sprite: Cell_Empty (assigned per instance)
│  ├─ Sorting Order: 0 (back layer)
│  ├─ Color: #F5F5F5 (light gray)
│  └─ Material: Cell_Default
│
├─ CanvasGroup (For opacity transitions)
│  ├─ Alpha: 1 (full opacity)
│  └─ Blocks Raycasts: Yes
│
├─ Animator (For highlight animation)
│  ├─ Controller: CellViewController.controller
│  └─ Avatar: None (not humanoid)
│
├─ BoxCollider2D (For tap detection)
│  ├─ Size: 100×100px
│  ├─ Offset: 0,0
│  ├─ Is Trigger: Yes
│  └─ Body Type: Dynamic (no gravity)
│
├─ EventTrigger (For pointer events)
│  └─ OnPointerClick: CellView.OnPointerClick()
│
├─ CellView (Custom Script)
│  ├─ CellIndex: [0-11]
│  └─ Public methods: SetState(), Highlight(), UnHighlight()
│
├─ HighlightVisual (Child, Highlight Ring)
│  ├─ Image (or SpriteRenderer)
│  ├─ Sprite: Highlight_Ring
│  ├─ Sorting Order: 1 (above cell)
│  └─ Active: False (hidden until needed)
│
└─ ChipView (Child, optional - created at runtime)
   └─ [See ChipView Prefab structure below]
```

### ChipView Prefab
```
ChipView (GameObject, spawned as needed)
├─ Transform
│  ├─ Position: Same as parent CellView
│  ├─ Scale: 1,1,1
│  └─ Rotation: 0,0,0
│
├─ SpriteRenderer (Chip visual)
│  ├─ Sprite: Chip_Player1 (assigned on instantiation)
│  ├─ Sorting Order: 2 (above cell)
│  ├─ Color: Tinted per player
│  └─ Material: Chip_Default
│
├─ Shadow (Child, optional)
│  ├─ SpriteRenderer
│  ├─ Sprite: Chip_Shadow
│  ├─ Sorting Order: 1 (below chip)
│  └─ Offset: 5px down, 5px right
│
├─ Animator (For chip animations)
│  ├─ Controller: ChipViewController.controller
│  └─ Parameters: moveDirection, isBumping
│
├─ ParticleSystem (For bump effect)
│  ├─ Prefab: Particle_Bump
│  ├─ Play on Awake: False
│  ├─ Emission: Enabled when bump occurs
│  └─ Duration: 500ms
│
└─ ChipView (Custom Script)
   ├─ PlayerId: [1-4]
   ├─ CurrentCell: [0-11]
   └─ Public methods: Move(), Bump(), Exit()
```

---

## Material Requirements

### Default Cell Material
```
Name: Cell_Default
Shader: Standard (or custom 2D)
Color: #F5F5F5 (light gray)
Properties:
  Metallic: 0
  Smoothness: 0.5
  Emission: None
```

### Highlight Material
```
Name: Cell_Highlight
Shader: Custom (additive or glow)
Color: #43A047 (success green) or #1E88E5 (primary blue)
Properties:
  Additive Blend: Yes (glowing effect)
  Emission: Yes (bright, visible)
  Smoothness: High (glossy)
```

### Chip Shadow Material
```
Name: Chip_Shadow
Shader: Standard
Color: #000000 (black, 30% opacity)
Properties:
  Metallic: 0
  Smoothness: 0
  Emission: None
  Alpha: 0.3
```

### Disabled Material
```
Name: Cell_Disabled
Shader: Standard
Color: #BDBDBD (gray, desaturated)
Properties:
  Metallic: 0
  Smoothness: 0
  Emission: None
  Saturation: -100 (grayscale)
```

---

## Animation Clips

### Cell Highlight Animations

#### Cell_HighlightEnter.anim
- **Duration**: 250ms
- **Target**: HighlightVisual scale and opacity
- **Keyframes**:
  - 0ms: Scale 0, Opacity 0
  - 250ms: Scale 1, Opacity 0.8
- **Easing**: Ease-out

#### Cell_HighlightExit.anim
- **Duration**: 250ms
- **Target**: HighlightVisual scale and opacity
- **Keyframes**:
  - 0ms: Scale 1, Opacity 0.8
  - 250ms: Scale 0, Opacity 0
- **Easing**: Ease-in

#### Highlight_Pulse.anim
- **Duration**: 600ms (looping)
- **Target**: HighlightVisual scale
- **Keyframes**:
  - 0ms: Scale 1
  - 300ms: Scale 1.1 (peak)
  - 600ms: Scale 1
- **Easing**: Ease-in-out
- **Loop**: Yes, continuous

### Chip Animations

#### Chip_Move.anim
- **Duration**: 300ms
- **Target**: ChipView position (lerp from → to)
- **Path**: Straight line, no arc
- **Easing**: Ease-out (smooth landing)
- **Sound**: Optional slide sound (attached to animation event)

#### Chip_Bump.anim
- **Duration**: 400ms
- **Target**: ChipView position (off-board) and scale (grows)
- **Keyframes**:
  - 0ms: Position source, Scale 1
  - 200ms: Position over cell, Scale 1.1
  - 400ms: Position off-board, Scale 0
- **Easing**: Ease-in (accelerating)
- **Particle Trigger**: At 200ms (collision moment)
- **Sound**: Bump impact sound at 200ms

#### Chip_Exit.anim
- **Duration**: 300ms
- **Target**: ChipView position (move off board edge) and fade
- **Keyframes**:
  - 0ms: Position on finish cell, Opacity 1
  - 300ms: Position off-board, Opacity 0
- **Easing**: Ease-out

---

## Asset Organization Best Practices

### Directory Structure
- **Sprites**: All image assets, organized by type
- **Prefabs**: Reusable game objects
- **Animations**: Animation clips, organized by object
- **Materials**: Material instances
- **Resources**: Assets loaded at runtime (optional)

### Naming Consistency
- Always use PascalCase for files (`CellView.prefab`)
- Always use snake_case for sprites (`board_cells.png`)
- Always include object type in name (`Cell_*`, `Chip_*`)
- Avoid version numbers (use git instead)

### Version Control
- Include `.meta` files with assets
- Exclude `Library/` folder
- Commit artwork changes with code changes
- Tag releases with art asset versions

---

## Performance Optimization

### Sprite Optimization
- **Compression**: Use PNG compression (RGB 16-bit or RGBA)
- **Resolution**: 2x design resolution (will scale down)
- **Pixel Perfect**: Use point sampling (no blur)
- **Atlasing**: Combine related sprites in sheet

### Material Optimization
- **Shaders**: Use built-in shaders (fast)
- **Texture Atlases**: Reduce draw calls
- **Instancing**: Use material instancing for multiple chips
- **Custom Shaders**: Only if necessary

### Animation Optimization
- **Baking**: Bake animation into mesh if static
- **Keyframe Reduction**: Minimize keyframes (linear interpolation)
- **Clip Reuse**: Use same animation for similar moves
- **Disable When Offscreen**: Disable animators when not visible

---

## Fallback & Placeholder Assets

### Built-in Placeholders
- **Missing Sprites**: Use Unity's default pink square
- **Missing Materials**: Use default white material
- **Missing Animations**: Use instant state changes (no animation)

### Testing Without Art
- **Colored Boxes**: Simple colored quads for development
- **Debug Labels**: Text labels showing cell index
- **Temporary Visuals**: Sufficient for gameplay testing

---

## Related Documents
- BOARD_ARCHITECTURE.md
- INPUT_HANDLING.md
- SPRINT_4_KICKOFF.md

---

**Status**: Complete - Production Ready
