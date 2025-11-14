# UI Design System
**Created**: Nov 14, 2025  
**Owner**: UI Engineer  
**Status**: ACTIVE

---

## Color Palette

### Primary Colors
- **Primary Blue**: #1E88E5 (main buttons, focus states)
- **Primary Dark**: #1565C0 (hover state, darker shade)
- **Primary Light**: #64B5F6 (disabled, background tints)

### Secondary Colors
- **Success Green**: #43A047 (win state, valid actions)
- **Warning Orange**: #FB8C00 (alert, pending actions)
- **Error Red**: #E53935 (invalid actions, penalties)
- **Info Cyan**: #29B6F6 (information badges)

### Neutral Colors
- **White**: #FFFFFF (backgrounds, primary text)
- **Gray 100**: #F5F5F5 (light backgrounds)
- **Gray 500**: #9E9E9E (secondary text, borders)
- **Gray 900**: #212121 (primary text, dark elements)
- **Black**: #000000 (highest contrast, emphasis)

### State Colors
- **Disabled**: #BDBDBD (grayed out, 50% opacity)
- **Focus**: #1E88E5 with 4px outline
- **Hover**: Lighten primary by 10%
- **Active**: Darken primary by 15%

---

## Spacing System

### Base Unit: 8px
- **xs**: 4px (micro spacing, letter-spacing)
- **sm**: 8px (tight spacing, small padding)
- **md**: 16px (standard spacing, padding)
- **lg**: 24px (larger spacing, section margins)
- **xl**: 32px (major spacing, container margins)
- **2xl**: 48px (page-level spacing)
- **3xl**: 64px (major section breaks)

### Usage Guidelines
- Padding (buttons): 12px (vertical) × 24px (horizontal)
- Padding (cards): 24px (all sides)
- Margin (between elements): 16px
- Margin (between sections): 32px
- Gap (grid/flex): 16px or 24px

---

## Typography

### Font Family
- **Primary**: Roboto (system default fallback: -apple-system, BlinkMacSystemFont, "Segoe UI")
- **Mono**: Roboto Mono (code, numbers, IDs)

### Font Sizes
- **Display**: 48px (1.5rem, headings, large titles)
- **Headline**: 36px (1.125rem, section titles)
- **Title**: 28px (0.875rem, card titles, subsections)
- **Body Large**: 18px (0.5625rem, primary text)
- **Body Regular**: 16px (0.5rem, default text size)
- **Body Small**: 14px (0.4375rem, secondary text, captions)
- **Label**: 12px (0.375rem, UI labels, tags)
- **Mono**: 14px (fixed-width code, statistics)

### Font Weights
- **Light**: 300 (secondary information, disabled text)
- **Regular**: 400 (body text, default)
- **Medium**: 500 (labels, buttons, emphasis)
- **Bold**: 700 (headings, important text)

### Line Heights
- **Tight**: 1.2 (headings, titles)
- **Normal**: 1.5 (body text)
- **Relaxed**: 1.75 (long-form content)
- **Loose**: 2.0 (accessibility-focused paragraphs)

### Text Contrast
- **AA Standard**: 4.5:1 (body text on background)
- **AAA Standard**: 7:1 (important text, accessibility)
- **Minimum**: Never go below 3:1 for any text

---

## Button System

### Standard Button Sizes
- **Large**: 56px height, 24px padding (primary actions)
- **Medium**: 48px height, 20px padding (standard buttons, touch)
- **Small**: 40px height, 16px padding (secondary actions)
- **Tiny**: 32px height, 12px padding (tertiary actions)

### Mobile Button Sizing
- **Touch Target**: Minimum 44×44px (iOS/Android standard)
- **Recommended**: 48×48px (optimal for mobile)
- **Spacing Between**: 8px minimum gap

### Button States
- **Default**: Primary color background, white text
- **Hover**: 10% lighter background, pointer changes
- **Pressed/Active**: 15% darker background, 2px inset shadow
- **Disabled**: Gray background, 50% opacity, no pointer events
- **Focus**: 4px outline in primary color, 2px offset

### Button Types
- **Primary**: Bold, primary color, main actions
- **Secondary**: Outlined, gray border, secondary actions
- **Tertiary**: Text-only, primary color text, minimal actions
- **Danger**: Error red background, white text, destructive actions

---

## Icon System

### Size Grid
- **xs**: 16px (inline, labels, small UI)
- **sm**: 24px (buttons, compact UI)
- **md**: 32px (standard icons, toolbar)
- **lg**: 48px (feature icons, hero elements)
- **xl**: 64px (display icons, prominent features)

### Icon Specifications
- **Stroke Weight**: 2px (medium, legible)
- **Corner Radius**: 2px (rounded corners, modern feel)
- **Padding/Clearance**: Minimum 4px around icon
- **Color**: Inherit from text color or apply specific color

### Icon Usage
- **Buttons**: Icon + label (preferred) or icon-only (with tooltip)
- **Navigation**: Icon-only with label below
- **Inline**: Icon + text, vertically centered
- **Size Pairing**: Never mix icon sizes in same component

---

## Animation & Motion

### Animation Timing
- **Fast**: 150ms (micro interactions, state changes)
- **Normal**: 300ms (transitions, fade effects)
- **Slow**: 500ms (major layout changes, modals)
- **Extra Slow**: 750ms (immersive sequences, emphasis)

### Easing Curves
- **Ease-In-Out**: cubic-bezier(0.4, 0, 0.2, 1) (standard transitions)
- **Ease-Out**: cubic-bezier(0, 0, 0.2, 1) (entering elements)
- **Ease-In**: cubic-bezier(0.4, 0, 1, 1) (exiting elements)
- **Linear**: Linear (continuous motion, spins, progress)

### Animation Types
- **Fade**: Opacity change (150-300ms)
- **Slide**: Transform translate (300ms)
- **Scale**: Transform scale (250ms)
- **Rotate**: Transform rotate (300-500ms)
- **Bounce**: Spring easing (400-600ms)

### Animation Guidelines
- Use animations to guide user attention
- Disable animations if system prefers reduced motion
- Keep animations < 1 second for UI (except special effects)
- Avoid flashing/strobe effects (accessibility)

---

## Dark Mode Support

### Dark Palette
- **Background**: #121212 (primary dark background)
- **Surface**: #1E1E1E (card/component background)
- **Text Primary**: #FFFFFF (main text, 100% opacity)
- **Text Secondary**: #BDBDBD (secondary text, 70% opacity)
- **Divider**: #424242 (borders, 12% opacity)

### Contrast in Dark Mode
- **AA Standard**: Still 4.5:1 (white on dark background achieves this)
- **Lighter colors**: Use gray tints (avoid pure colors)
- **Icons**: Same sizing, higher opacity (60-100%)

### Implementation
- CSS custom properties (--dark-bg, --dark-text, etc.)
- Prefers-color-scheme media query
- System preference detection
- Manual toggle option (optional)

---

## Component Tokens

### Button Components
- Primary Button: Primary Blue bg, white text, 48px tall
- Secondary Button: Outlined, gray border, gray text
- Danger Button: Error Red bg, white text
- Text Button: No background, primary blue text

### Input Components
- Text Input: 44px height (mobile), gray border, gray placeholder
- Touch Target: 44×44px minimum
- Focus State: Blue outline, 4px offset
- Error State: Red border, error message below

### Cards
- Padding: 24px (standard)
- Border-radius: 8px (modern rounded corners)
- Elevation: 4px shadow (subtle depth)
- Max-width: 400px (mobile), unlimited (desktop)

### Modals
- Overlay: Black, 30% opacity
- Modal width: 90% (mobile), 500px (desktop)
- Padding: 24px (standard, 16px mobile)
- Border-radius: 16px (emphasis)

---

## Accessibility Requirements

### Touch Targets
- Minimum: 44×44px (WCAG 2.1 Level AAA)
- Preferred: 48×48px (Android Material standard)
- Spacing: 8px minimum between targets

### Color Contrast
- Text on background: 4.5:1 (AA)
- Large text (18pt+): 3:1 (AA minimum)
- Graphics/components: 3:1 (WCAG 2.1)
- Focus indicators: High contrast outline

### Text Legibility
- Minimum font size: 12px (12px body text is minimum)
- Preferred: 16px body text
- Max line length: 60-80 characters
- Line height: 1.5 minimum (1.75 preferred)

### Interactive Elements
- Focus indicators visible (not removed)
- Keyboard navigation supported
- Touch feedback provided (visual or haptic)
- State changes clear (not color-only)

---

## Related Documents
- HUD_ARCHITECTURE.md
- UI_STANDARDS.md
- SPRINT_5_UI_PREP.md

---

**Status**: Complete - Production Ready
