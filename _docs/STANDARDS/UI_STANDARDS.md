# UI Standards

**Created**: Nov 14, 2025  
**Owner**: UI Engineer  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Accessibility Standards

### Touch Targets

**Minimum Size**: 44px × 44px (WCAG 2.5.5 Level AAA)  
**Recommended Size**: 56px × 56px (mobile games)  
**Spacing Between Targets**: 8px minimum (to prevent accidental taps)

### Application
- All buttons must be at least 44px × 44px
- Interactive elements on mobile must be at least 56px × 56px
- Dice button, BUMP button, Declare Win button: all 56px height minimum
- Scoreboard player rows: 48px height minimum
- Close buttons: 40px × 40px minimum

### Testing
- Test on actual mobile devices (not just emulators)
- Test with finger sizes (larger than mouse cursor)
- No accidental double-taps should trigger actions

---

### Color Contrast Ratios

**WCAG AA Standard**: 4.5:1 for normal text, 3:1 for large text  
**WCAG AAA Standard**: 7:1 for normal text, 4.5:1 for large text

### Text Contrast
| Text Type | Min Size | Min Ratio |
|-----------|----------|-----------|
| Body text | 14px | 4.5:1 |
| Labels | 12px | 4.5:1 |
| Large text | 18px+ | 3:1 |
| UI components | N/A | 3:1 |

### Examples
- Dark text (#1F2937) on light background (#FFFFFF): ✅ 16:1 (exceeds AA & AAA)
- Primary button (#2563EB) with white text (#FFFFFF): ✅ 8.6:1 (exceeds AA & AAA)
- Error text (#EF4444) on white: ✅ 5.9:1 (exceeds AA, below AAA for body)

### Testing
- Use color contrast checker tools (WebAIM, Axe DevTools)
- Test with color blindness simulators (Coblis)
- Never rely on color alone to convey information

---

### Font Sizes & Readability

**Minimum Font Size**: 12px (body text), 14px recommended  
**Maximum Line Length**: 60-80 characters for optimal readability

### Font Size Standards
- Display: 32px
- Heading 1: 28px
- Heading 2: 24px
- Body: 14-16px
- Small: 12px (minimum for body)
- Caption: 10px (only for secondary info)

### Line Height
- Display: 1.1 (tight spacing for large text)
- Body: 1.5 (comfortable reading)
- Labels: 1.3 (compact but readable)

---

### Focus & Keyboard Navigation

**Focus Indicator**: 2px outline, #2563EB, 4px offset from element  
**Focus Order**: Logical, left-to-right, top-to-bottom

### Implementation
```csharp
// Focus state CSS equivalent in Unity
button:focus {
  outline: 2px solid #2563EB;
  outline-offset: 4px;
}
```

### Tab Navigation
- All interactive elements must be keyboard accessible
- Tab order: logical (top-left to bottom-right)
- Escape key: close modals/menus

---

## Mobile Responsiveness

### Supported Resolutions

**Minimum Width**: 320px (iPhone SE)  
**Maximum Width**: 1920px (desktop/tablet in landscape)  
**Aspect Ratios**: 9:16 (portrait, mobile), 16:9 (landscape, tablet)

### Device Classes
| Class | Width | Height | Example |
|-------|-------|--------|---------|
| Small Phone | 320-375px | 640-812px | iPhone SE, 6/7/8 |
| Large Phone | 375-430px | 812-932px | iPhone 12/13/14 |
| Tablet Portrait | 600-768px | 800-1024px | iPad mini |
| Tablet Landscape | 1024px+ | 600px+ | iPad Pro |
| Desktop | 1280px+ | 720px+ | PC/Mac |

### Responsive Scaling

**Base Breakpoints**:
- **Mobile**: < 480px
- **Tablet**: 480px - 768px
- **Desktop**: > 768px

**Scaling Rules**:
- Canvas scaler: Reference resolution 1080 × 1920 (9:16)
- Scale with screen size (automatic scaling)
- Safe area adjustment for notches/safe areas

---

### Safe Area Handling

**iOS Notch Areas**: Must reserve space for notch/dynamic island  
**Android Safe Area**: Account for system gesture areas

### Implementation
```csharp
// Get safe area insets
var safeArea = Screen.safeArea;
var left = safeArea.xMin;
var right = Screen.width - safeArea.xMax;
var top = Screen.height - safeArea.yMax;
var bottom = safeArea.yMin;

// Adjust HUD panels with safe area
hudPanel.offsetMin = new Vector2(left, bottom);
hudPanel.offsetMax = new Vector2(-right, -top);
```

### Safe Area Testing
- Test on actual iOS devices with notch (iPhone X+)
- Test on Android with gesture navigation (Pixel phones)
- Test on iPad in landscape mode
- Ensure all buttons are visible in safe area

---

### Portrait & Landscape Orientation

**Portrait (Primary)**:
- 9:16 aspect ratio
- Board in center, HUD above/below
- Vertical scoreboard on right

**Landscape (Secondary)**:
- 16:9 aspect ratio
- Board on left, scoreboard on right
- Buttons at bottom

### Orientation Handling
- Auto-rotate enabled (with locked orientations)
- Locked to portrait for phone games
- Allow landscape for tablet games
- Smooth transition animation (200ms)

---

## Platform-Specific Standards

### WebGL (Browser)

**Target Resolution**: 1080 × 1920 (9:16 portrait)  
**Frame Rate**: 60 FPS target, 30 FPS minimum  
**Browser Support**: Chrome, Firefox, Safari (latest 2 versions)

**Considerations**:
- High DPI displays (Retina, 4K)
- Mouse input (hover states)
- Keyboard input (tab navigation)
- No touch input (or optional touch on tablets)

---

### Android

**Target Resolution**: 1080 × 2160 (9:20, accounting for status bar)  
**Frame Rate**: 60 FPS target, 30 FPS minimum on older devices  
**API Level**: 24+ (Android 7.0+)

**Android-Specific**:
- Safe area for system gesture buttons (bottom)
- Status bar area (top, reserved)
- Navigation bar area (bottom, varies by device)
- Back button handling
- Touch input responsiveness

---

### iOS

**Target Resolution**: 1080 × 2160 (9:19.5, accounting for notch)  
**Frame Rate**: 60 FPS target, 30 FPS minimum on older devices  
**iOS Version**: 12.0+

**iOS-Specific**:
- Safe area for notch/dynamic island
- Safe area for gesture zones (left/right/bottom)
- Home indicator area (bottom)
- Touch input with haptic feedback
- No back button (swipe gesture)

---

## Button Affordance

### Visual Affordance States

**Enabled (Default)**
- Full opacity, normal color
- Text color: high contrast
- Pointer cursor on hover

**Hover State** (Desktop only)
- Background lightens by 10%
- Text remains same
- Slight scale increase (1.05)
- Duration: 150ms

**Pressed/Active State**
- Background darkens by 20%
- Slight scale decrease (0.95)
- Duration: 150ms
- Instant feedback (no delay)

**Disabled State**
- 50% opacity
- Gray text (#9CA3AF)
- Gray background (#D1D5DB)
- No cursor change (not-allowed)
- No animation on interaction

**Focused State** (Keyboard navigation)
- 2px outline #2563EB
- 4px offset
- No other visual changes

---

## Error Messaging

### Error Display Standards

**Location**: Center-bottom of screen or modal overlay  
**Duration**: 
- Brief errors: 2-3 seconds (auto-dismiss)
- Critical errors: Modal (requires action)

**Visual**: 
- Red text (#EF4444) or red icon
- White background or transparent with text
- Sufficient contrast (4.5:1 minimum)

### Error Message Format

**Tone**: Clear, non-blaming, actionable  
**Length**: 1-2 sentences max  
**Example**:
- ❌ "You failed to declare a win"
- ✅ "Cannot declare win: 2 chips still need movement"

### Error Types & Messages

| Error | Message | Duration | Type |
|-------|---------|----------|------|
| Invalid move | "Invalid move selected" | 2s | Toast |
| Cannot bump | "Cannot bump: no chips to move" | 3s | Toast |
| Cannot win | "Cannot declare win: [reason]" | Modal | Modal |
| Crash | "Game error encountered. Return to menu?" | Modal | Modal |
| Network | "Connection lost. Offline mode." | 3s | Toast |

---

## Success Messaging

**Visual**: Green text (#10B981) or checkmark icon  
**Duration**: 2-3 seconds (auto-dismiss)  
**Examples**:
- "Bump successful!"
- "Move completed"
- "You won the game!"

---

## Loading & Waiting States

**Visual Indicator**:
- Loading spinner (rotating circle)
- Progress bar (if duration known)
- "Loading..." text

**Duration**: Display until complete (min 1s for perception)

**Example**: While waiting for other players to complete turn

---

## Input Field Standards

**Height**: 40px  
**Padding**: 8px horizontal, 8px vertical  
**Border**: 1px #D1D5DB  
**Border Radius**: 6px  
**Font Size**: 14px  
**Placeholder Text**: Gray #9CA3AF, 14px

**Focus State**:
- Border: 2px #2563EB
- Shadow: 0 0 0 3px rgba(37, 99, 235, 0.1)

---

## Text Standards

### Headings
- **H1**: 28px, 700 weight, line-height 1.2
- **H2**: 24px, 600 weight, line-height 1.2
- **H3**: 20px, 600 weight, line-height 1.2

### Body
- **Regular**: 14-16px, 400 weight, line-height 1.5
- **Small**: 12px, 400 weight, line-height 1.4

### Labels & UI
- **Label**: 12px, 600 weight, line-height 1.3
- **Button**: 14px, 600 weight, line-height 1.3

---

## Spacing & Layout

**Margin/Padding Scale**:
- 8px (xs) - Small internal spacing
- 16px (sm) - Standard padding
- 24px (md) - Section spacing
- 32px (lg) - Major blocks

**Grid**: 8px grid (all elements align to 8px multiples)

---

## Dark Mode (Future Consideration)

- Not required for launch
- Design system supports future dark mode
- Use color variables (not hard-coded colors)
- Prepare for: theme switching, persistence

---

## Related Documents

- UI_DESIGN_SYSTEM.md
- HUD_ARCHITECTURE.md
- SPRINT_5_UI_PREP.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Implementation
