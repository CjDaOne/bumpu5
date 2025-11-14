# Asset Integration

**Created**: Nov 14, 2025  
**Owner**: Board Engineer  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Overview

Asset Integration defines the workflow for importing, organizing, and managing art assets (sprites, animations, materials) in Unity. This document ensures consistent naming, folder structure, and usage patterns across the project.

---

## Asset Import Workflow

### Standard Workflow (PNG Sprite Sheets)

**Step 1: Prepare Art File**
- File format: PNG (24-bit RGB with alpha)
- Resolution: 2x target screen resolution (2160 × 3840 for 1080 × 1920 base)
- DPI: 72 DPI (web standard)
- Color profile: sRGB (not linear)

**Step 2: Place in Project**
```
Assets/
└─ Board/
   └─ Sprites/
      └─ board_cells.png
```

**Step 3: Configure Import Settings**
- Texture Type: Sprite (2D and UI)
- Sprite Mode: Multiple (grid-based slicing)
- Pixel Per Unit: 100 (for 1080 × 1920 reference)
- Compression: High Quality
- Filter: Point (no filtering - crisp pixel art)

**Step 4: Slice Sprites (Grid or Custom)**
- Grid size: Determine from art (e.g., 200 × 200 per sprite)
- Cell size: 200 × 200 pixels (if uniform)
- Offset: 0, 0
- Padding: 2 pixels (internal padding)

**Step 5: Name Individual Sprites**
- Convention: `{Asset}_{Type}_{State}`
- Example: `Board_Cell_Empty`, `Board_Cell_Filled`, `Chip_Red`

**Step 6: Create Prefabs**
- Drag sprite into scene
- Add components (Button, Image, Animator, etc.)
- Test visually
- Drag from scene into `Assets/Board/Prefabs/`
- Name: `CellView.prefab`

**Step 7: Reference in Code**
```csharp
[SerializeField] private Sprite cellEmptySprite;
[SerializeField] private Sprite chipRedSprite;

// In editor: Drag sprites into inspector
```

---

## Naming Conventions

### Sprite Naming

**Format**: `{Category}_{Component}_{State}`

**Examples**:
- `Board_Cell_Empty` - Empty board cell
- `Board_Cell_WithChip_Red` - Cell with red chip
- `Board_Cell_WithChip_Blue` - Cell with blue chip
- `Chip_Red` - Red player chip (standalone)
- `Chip_Blue` - Blue player chip
- `Button_Dice` - Dice roll button
- `Button_Bump` - Bump action button
- `Icon_Settings` - Settings gear icon
- `Icon_Help` - Help question mark

**Naming Rules**:
- PascalCase for category (Board, Chip, Button, Icon)
- PascalCase for components (Cell, Dice, Bump)
- PascalCase for states (Empty, Filled, Pressed, Disabled)
- Use underscores to separate parts
- No spaces or special characters
- Lowercase acceptable for short names: `chip_red` (both acceptable)

### Prefab Naming

**Format**: `{Type}{Purpose}.prefab`

**Examples**:
- `CellView.prefab` - Individual board cell
- `ChipView.prefab` - Chip piece
- `DiceButton.prefab` - Dice roll button
- `ScoreboardRow.prefab` - Player score row
- `NotificationPanel.prefab` - Notification UI

**Rules**:
- PascalCase
- Descriptive and unique
- Include "View" suffix for visual prefabs
- Include "Manager" suffix for managers (if prefab-based)

### Material Naming

**Format**: `{Surface}_{Effect}.mat`

**Examples**:
- `Cell_Default.mat` - Normal cell material
- `Cell_Highlight.mat` - Highlighted cell material
- `Chip_Red.mat` - Red chip material
- `UI_Button_Default.mat` - Button default state

### Animation Clip Naming

**Format**: `{Component}_{State}`

**Examples**:
- `Cell_HighlightEnter` - Highlight fade-in
- `Cell_HighlightExit` - Highlight fade-out
- `Chip_Drop` - Chip landing/bouncing
- `Chip_Spin` - Chip spinning
- `Button_Press` - Button press animation
- `WinCelebration_Confetti` - Confetti effect

---

## Sprite Slicing

### Grid-Based Slicing (Uniform Sprites)

**Use When**: All sprites in sheet are same size

**Process**:
1. Import PNG as described above
2. In inspector: Sprite Mode → Multiple
3. Click "Sprite Editor"
4. Click "Slice" dropdown → Grid by Cell Count (or Grid by Cell Size)
5. Set column/row counts or cell size
6. Click "Slice"
7. Apply

**Example**:
```
Sheet: 400 × 400 pixels
Cells: 2 × 2 grid
Cell size: 200 × 200 each
Result: 4 sprites (Sprite 0, Sprite 1, Sprite 2, Sprite 3)
Rename each to match convention
```

### Custom Slicing (Non-Uniform Sprites)

**Use When**: Sprites are different sizes or irregular layout

**Process**:
1. Import PNG as described
2. Sprite Mode → Multiple
3. Click "Sprite Editor"
4. Manually draw rectangles around each sprite
5. Name each slice individually
6. Apply

**Rules**:
- No overlap between rectangles
- Include padding around sprites (2-4 pixels)
- Leave gaps between sprites for transparency
- Test rendering (no bleeding/artifacts)

---

## Prefab Structure

### CellView Prefab

```
CellView (GameObject)
│
├─ Properties
│  ├─ Layer: UI
│  └─ Tag: Cell
│
├─ RectTransform
│  ├─ Width: 120
│  ├─ Height: 120
│  ├─ Anchor: Middle Center
│  └─ Position: (determined by board layout)
│
├─ BackgroundImage (Image component)
│  ├─ Source Image: Board_Cell_Empty
│  ├─ Color: White (255, 255, 255, 255)
│  ├─ Raycast Target: true
│  └─ RectTransform: 120 × 120 (fill parent)
│
├─ ChipImage (Image component) - INITIALLY DISABLED
│  ├─ Source Image: Chip_Red (or reference in code)
│  ├─ Color: Red (255, 0, 0, 255) or player color
│  ├─ Raycast Target: false
│  └─ RectTransform: 100 × 100 (centered, smaller than cell)
│
├─ HighlightOverlay (Image component) - INITIALLY DISABLED
│  ├─ Source Image: None (use color instead)
│  ├─ Color: Light Blue (219, 234, 254, 180) with pulsing animation
│  ├─ Raycast Target: false
│  └─ RectTransform: 130 × 130 (slightly larger)
│
├─ SelectionBorder (Image component) - INITIALLY DISABLED
│  ├─ Source Image: Border sprite (3px outline) or procedural
│  ├─ Color: Blue (37, 99, 235, 255)
│  ├─ Raycast Target: false
│  └─ RectTransform: 130 × 130
│
├─ Button (Button component)
│  ├─ Transition: Color Tint
│  ├─ Target Graphic: BackgroundImage
│  ├─ Normal Color: White
│  ├─ Highlighted Color: Light Gray (240, 240, 240)
│  ├─ Pressed Color: Medium Gray (200, 200, 200)
│  ├─ Disabled Color: Light Gray (211, 211, 211)
│  └─ On Click: CellView.OnTapped()
│
├─ Animator (Animator component) - OPTIONAL
│  ├─ Controller: CellView.controller
│  ├─ Avatar: None (2D sprite, no avatar)
│  └─ Culling Type: Always animate
│
└─ CellView (MonoBehaviour script)
   ├─ cellIndex: int [set in inspector]
   ├─ backgroundImage: Image [auto-populated]
   ├─ chipImage: Image [auto-populated]
   ├─ highlightOverlay: Image [auto-populated]
   ├─ selectionBorder: Image [auto-populated]
   ├─ animator: Animator [auto-populated]
   │
   ├─ SetChipColor(Color): void
   ├─ SetState(CellState): void
   ├─ SetHighlight(HighlightState): void
   ├─ OnTapped(): void
   └─ AnimateChipArrival(): IEnumerator
```

### ChipView Prefab (Alternate Structure)

```
ChipView (GameObject) - Floating chip for animation
│
├─ SpriteRenderer (SpriteRenderer component)
│  ├─ Sprite: Chip_Red (or dynamic)
│  ├─ Color: Red (255, 0, 0, 255)
│  ├─ Sorting Order: 10 (above board)
│  └─ Flip: None
│
├─ Animator (Animator component)
│  ├─ Controller: ChipView.controller
│  └─ Parameters: [move_distance, bump_intensity]
│
└─ ChipView (MonoBehaviour script)
   ├─ spriteRenderer: SpriteRenderer
   ├─ animator: Animator
   ├─ SetColor(Color): void
   └─ PlayAnimation(string): void
```

---

## Material Requirements

### Default Material
- **Name**: `Standard.mat` (Unity default)
- **Shader**: Sprites/Default
- **Texture**: None (uses sprite's texture)
- **Color**: White (1, 1, 1, 1)

### Highlight Material
- **Name**: `Cell_Highlight.mat`
- **Shader**: Sprites/Default
- **Color**: Light Blue (0.86, 0.92, 0.99, 0.5)
- **Blend Mode**: Additive (for overlay effect)

### Disabled Material
- **Name**: `Cell_Disabled.mat`
- **Shader**: Sprites/Default
- **Color**: Gray (0.7, 0.7, 0.7, 0.5)
- **Blend Mode**: Normal

### Use in Code
```csharp
public class CellView : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material disabledMaterial;
    
    public void SetHighlight(bool highlight)
    {
        Image img = GetComponent<Image>();
        img.material = highlight ? highlightMaterial : defaultMaterial;
    }
}
```

---

## Animation Clips

### Cell Highlight Animation

**Duration**: 1000ms (1 second loop)  
**Keyframes**: 
- 0ms: Opacity 0.6
- 500ms: Opacity 0.9
- 1000ms: Opacity 0.6

**Easing**: Smooth (sine curve)  
**Loop**: Yes  
**Looped Time**: True

### Chip Movement Animation

**Duration**: 300ms (smooth movement)  
**Start**: Source cell position
**End**: Destination cell position  
**Path**: Straight line (no arc)  
**Easing**: EaseInOutCubic

**Keyframes**:
- 0ms: Position = source
- 150ms: Position = lerp(source, dest, 0.5)
- 300ms: Position = dest

### Chip Bump Animation

**Duration**: 200ms (quick bounce)  
**Keyframes**:
- 0ms: Scale (1, 1), Position original
- 100ms: Scale (1.2, 1.2), Position moved back
- 200ms: Scale (1, 1), Position original

**Easing**: EaseOutBounce

### Button Press Animation

**Duration**: 150ms  
**Keyframes**:
- 0ms: Scale (1, 1)
- 75ms: Scale (0.95, 0.95)
- 150ms: Scale (1, 1)

**Easing**: EaseInOutQuad

---

## Asset Organization (Folder Structure)

```
Assets/
│
├─ Board/
│  │
│  ├─ Sprites/
│  │  ├─ board_cells.png
│  │  ├─ board_chips.png
│  │  └─ board_backgrounds.png
│  │
│  ├─ Prefabs/
│  │  ├─ CellView.prefab
│  │  ├─ ChipView.prefab
│  │  └─ BoardGridManager.prefab
│  │
│  ├─ Animations/
│  │  ├─ Cell/
│  │  │  ├─ CellView.controller
│  │  │  ├─ Cell_HighlightEnter.anim
│  │  │  ├─ Cell_HighlightExit.anim
│  │  │  └─ Cell_PulseLoop.anim
│  │  │
│  │  └─ Chip/
│  │     ├─ ChipView.controller
│  │     ├─ Chip_Drop.anim
│  │     ├─ Chip_Bump.anim
│  │     └─ Chip_Spin.anim
│  │
│  └─ Materials/
│     ├─ Cell_Default.mat
│     ├─ Cell_Highlight.mat
│     └─ Cell_Disabled.mat
│
├─ UI/
│  │
│  ├─ Sprites/
│  │  ├─ ui_buttons.png (contains button sprites)
│  │  └─ ui_icons.png
│  │
│  ├─ Prefabs/
│  │  ├─ DiceButton.prefab
│  │  ├─ BumpButton.prefab
│  │  ├─ DeclareWinButton.prefab
│  │  ├─ ScoreboardRow.prefab
│  │  └─ NotificationPanel.prefab
│  │
│  ├─ Animations/
│  │  ├─ Button_Press.anim
│  │  ├─ Button_Hover.anim
│  │  └─ Notification_FadeOut.anim
│  │
│  └─ Materials/
│     ├─ UI_Button_Default.mat
│     └─ UI_Text_Default.mat
│
└─ Audio/
   ├─ SFX/
   │  ├─ dice_roll.wav
   │  ├─ bump.wav
   │  └─ win.wav
   │
   └─ Music/
      └─ gameplay_loop.mp3
```

---

## Import Settings Template

### Image Import Settings (Standard)
```
Texture Type: Sprite (2D and UI)
Sprite Mode: Multiple
Pixels Per Unit: 100
Sprite Pivot: Center
Generate Mipmaps: Off
Wrap Mode: Clamp
Filter Mode: Point (no filtering)
Compression: High Quality (or LZ4)
Alpha Handling: Keep Alpha
```

### Prefab Settings (Standard)
```
Drag on Canvas as UI element
Or drag into 3D scene for world space
Set up RectTransforms for proper sizing
Add scripts and event handlers
Test before saving as prefab
```

---

## Testing Checklist

- [ ] All sprites import without errors
- [ ] Sprite names follow convention
- [ ] Sprites render without bleeding/artifacts
- [ ] Prefabs instantiate correctly
- [ ] Materials apply correctly
- [ ] Animations play at correct speed
- [ ] Colors match design system
- [ ] Resolution scales properly (1080 × 1920)
- [ ] Safe area handling (notch, gesture areas)
- [ ] No missing dependencies
- [ ] File sizes reasonable (compress if needed)

---

## Related Documents

- BOARD_ARCHITECTURE.md
- INPUT_HANDLING.md
- UI_DESIGN_SYSTEM.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Implementation
