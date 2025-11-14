# UI Standards
**Created**: Nov 14, 2025  
**Owner**: UI Engineer  
**Status**: ACTIVE

---

## Accessibility Standards

### Touch Targets
- **Minimum Size**: 44×44px (WCAG 2.1 Level AAA, iOS/Android standard)
- **Recommended Size**: 48×48px (optimal for mobile, no accidental taps)
- **Spacing Between**: Minimum 8px gap between interactive elements
- **Small Elements**: If < 44px, must have 44px padding around (hover/tap zone)

### Color Contrast
- **Normal Text (< 18px)**: 4.5:1 contrast ratio (WCAG AA)
- **Large Text (>= 18px)**: 3:1 contrast ratio (WCAG AA)
- **UI Components/Graphics**: 3:1 minimum
- **Enhanced**: 7:1 (WCAG AAA, where possible)
- **Focus Indicators**: Always visible, never removed
- **Text on Images**: Ensure contrast against underlying image

### Font Size Standards
- **Minimum**: 12px (absolute minimum, body text)
- **Recommended**: 16px (body text)
- **Headings**: 24px+ (large, scannable)
- **Labels**: 14px+ (interactive element labels)
- **Line Height**: 1.5× font size minimum (body text)

### Keyboard Navigation
- **Tab Order**: Logical, top-to-bottom, left-to-right
- **Focus Visible**: Always visible outline or highlight
- **Keyboard Shortcuts**: Standard (Escape = close, Enter = submit)
- **Skip Links**: Optional skip-to-content for complex UIs

### Screen Reader Support
- **Labels**: All buttons/inputs have clear text labels
- **Alt Text**: N/A for game (not image-heavy, but icon labels provided)
- **Semantic HTML**: Proper element hierarchy (Canvas game may not apply)
- **ARIA**: If applicable for complex interactive elements

---

## Mobile Responsiveness

### Supported Screen Sizes
- **Small phones**: 320×568 (iPhone SE)
- **Medium phones**: 375×667 (iPhone 8)
- **Large phones**: 414×896 (iPhone 11 Pro)
- **Tablets**: 768×1024 (iPad mini) to 1024×1366 (iPad Pro)
- **Landscape**: All of above in landscape orientation

### Layout Adaptations
- **Portrait**: Vertical stack (board top, controls bottom)
- **Landscape**: Horizontal (board left, HUD right side)
- **Tablets**: Larger board, button group repositioned
- **Buttons**: Always >= 44px, scale proportionally
- **Text**: Scales with screen, never below 12px

### Safe Area Handling
- **iOS**: Respect safe area (notch, home indicator)
- **Android**: Respect system navigation bars
- **Implementation**: Use CSS/Unity safe area APIs
- **Margin**: 16px padding from screen edges minimum
- **Elements**: Never place critical UI under notch

### Aspect Ratio Support
- **Locked to Portrait**: 9:16 (phones)
- **Locked to Landscape**: 16:9 (optional, for tablets)
- **Adaptive**: Adjust layout based on screen aspect ratio
- **Max Height**: Cap game board at device height minus HUD

---

## Platform Differences

### WebGL (Browser)
- **Input**: Mouse (hover states), keyboard support
- **Cursor**: Change to pointer on interactive elements
- **Hover Effects**: Use hover states (desktop-specific)
- **Performance**: Optimize for 60 FPS on standard gaming laptops
- **Resolution**: Support 720p to 2160p displays
- **Testing Browsers**: Chrome, Firefox, Safari (latest versions)

### Android (Mobile)
- **Input**: Touch only (no hover)
- **Viewport**: Account for notches (Samsung, Pixel)
- **Safe Area**: Use Android's inset management
- **Gesture**: Support swipe back (system gesture)
- **Keyboard**: IME (input method) handling not needed (game input)
- **Orientation**: Support both portrait and landscape
- **DPI**: Support ldpi to xxxhdpi (1x to 4x)

### iOS (Mobile)
- **Input**: Touch only (no hover)
- **Viewport**: iPhone notch (top), Dynamic Island, Home Indicator (bottom)
- **Safe Area**: Mandatory safe area insets
- **Gesture**: Respect system swipe gestures
- **Haptics**: Optional haptic feedback (vibration on actions)
- **Orientation**: Support both portrait and landscape
- **Scaling**: Support 1x to 3x Retina (iPhone 6s+)

### Cross-Platform Standards
- **Button Behavior**: Same on all platforms (press → action)
- **Animations**: Consistent timing, reduced motion respected
- **Colors**: Exactly the same (no platform-specific tints)
- **Typography**: Same font stack, fallback chain

---

## Button Affordance

### Visual States

#### Default State
- **Background**: Primary color (#1E88E5)
- **Text**: White, bold
- **Border**: None (solid fill)
- **Shadow**: Slight elevation (2px shadow)
- **Cursor**: Pointer

#### Hover State (Desktop Only)
- **Background**: Darken 10% (#1565C0)
- **Shadow**: Increase elevation (4px shadow)
- **Scale**: Subtle 1.02x scale
- **Cursor**: Pointer

#### Pressed/Active State
- **Background**: Darken 15% (#0D47A1)
- **Shadow**: Inset 2px shadow (pressed effect)
- **Scale**: 0.98x (slight depression)
- **Duration**: 100ms (instant feedback)

#### Disabled State
- **Background**: Gray (#BDBDBD)
- **Text**: Darker gray, 50% opacity
- **Cursor**: Not-allowed
- **Interaction**: No events, no hover effect
- **Opacity**: 60% overall

#### Focus State
- **Outline**: 4px solid primary color
- **Offset**: 2px outside button
- **Visible**: Always, keyboard-only or always-on
- **Color**: High contrast (primary + background)

### Feedback Mechanisms
- **Visual**: Color change, scale, shadow change
- **Haptic**: Optional vibration (100ms pulse on press)
- **Audio**: Optional subtle sound effect (optional)
- **Animation**: Instant (< 100ms perceived latency)

---

## Error Messaging

### Message Format
- **Tone**: Clear, non-technical, friendly
- **Action**: Tell user what's wrong + how to fix
- **Length**: 1-2 sentences maximum
- **Icon**: Error icon (⚠️ or ❌) + red color

### Message Placement
- **Position**: Near the error (inline if possible, or popup)
- **Visual**: Red text, high contrast background
- **Duration**: Persistent until user fixes or dismisses
- **Prominence**: High visibility, but not intrusive

### Examples
- ❌ "Invalid move. Please select a valid cell."
- ❌ "You need 5 bumps to win. Current: 3."
- ❌ "Cell is already occupied. Choose another."

### Error Types
- **User Input**: Missing data, invalid selection
- **Game Logic**: Rule violation, illegal move
- **System**: Network error (not applicable for single-player)

---

## Animation Preferences

### Respects System Setting
- **prefers-reduced-motion**: CSS media query
- **Behavior**: Disable or shorten animations
- **Guidelines**:
  - Flash/strobe: Forbidden (< 3 Hz or > 55 Hz threshold violated)
  - Movement: Minimize for sensitive users
  - Scale: Fast transitions preferred (100-150ms vs 300ms)

### Implementation
```css
@media (prefers-reduced-motion: reduce) {
  * {
    animation-duration: 0.01ms !important;
    animation-iteration-count: 1 !important;
    transition-duration: 0.01ms !important;
  }
}
```

---

## Text Legibility

### Line Length
- **Optimal**: 50-75 characters per line
- **Maximum**: 100 characters (avoid extreme lengths)
- **Body Text**: 60-70 characters
- **Headings**: Flexible

### Line Spacing (Line Height)
- **Body Text**: 1.5 (24px for 16px font)
- **Headings**: 1.2 (comfortable reading)
- **Dense Content**: 1.75 (accessibility enhancement)

### Paragraph Spacing
- **Default**: 1.5× line height between paragraphs
- **Generous**: 2× line height (for emphasis sections)
- **Minimal**: 1× line height (in tight spaces)

### Readability Checks
- **Font**: Sans-serif preferred (Roboto, Arial)
- **Weight**: Regular (400) for body, Bold (700) for emphasis
- **Italics**: Avoid for body text (harder to read on screens)
- **All-Caps**: Avoid for body text (slower reading)

---

## Orientation Lock

### Portrait Lock (Phone)
- Lock to portrait by default
- Landscape support optional (if device large enough)
- Rotation trigger: iOS auto-rotate enabled, Android similar

### Landscape Support (Tablet/Opt-in)
- Provide landscape layout
- Reposition HUD (left/right side instead of bottom)
- Scale board to fit wider screen
- Support both orientations seamlessly

### Transition Animation
- Rotate board 90°
- Reposition HUD components
- Duration: 300ms
- Easing: Ease-in-out

---

## Loading States

### Loading Indicator
- **Style**: Spinning circle or progress bar
- **Color**: Primary blue (#1E88E5)
- **Position**: Center of screen
- **Text**: "Loading..." or game-specific message
- **Duration**: Shown for >= 500ms (no flash)

### Minimum Load Time
- **Perceived minimum**: 500ms (even if loads faster)
- **Prevents flashing**: Provides consistent feedback
- **Fallback**: If load > 5s, show progress percentage

---

## Notification System

### Toast Notifications
- **Position**: Bottom-center or top-center
- **Duration**: 3-4 seconds auto-dismiss
- **Types**: Success (green), Error (red), Info (blue)
- **Animation**: Slide in from bottom, fade out
- **Text**: 1-line max, action not required

### Modal Dialogs
- **Position**: Center of screen
- **Overlay**: Semi-transparent black background
- **Close Options**: X button, outside click, or button action
- **Animation**: Fade in (150ms), scale (slight zoom)
- **Keyboard**: Escape key closes (if applicable)

---

## Related Documents
- UI_DESIGN_SYSTEM.md
- HUD_ARCHITECTURE.md
- SPRINT_5_UI_PREP.md

---

**Status**: Complete - Production Ready
