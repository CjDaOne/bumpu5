# UI Design System

**Created**: Nov 14, 2025  
**Owner**: UI Engineer  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Colors

### Primary Palette
- **Primary Action**: `#2563EB` (Blue) - Dice button, main interactions
- **Primary Alt**: `#1E40AF` (Dark Blue) - Hover/pressed state
- **Success**: `#10B981` (Green) - Win, successful actions
- **Error**: `#EF4444` (Red) - Invalid moves, errors
- **Warning**: `#F59E0B` (Amber) - Penalties, cautions

### Secondary Palette
- **Background**: `#F9FAFB` (Light Gray) - Canvas background
- **Surface**: `#FFFFFF` (White) - Cards, panels
- **Border**: `#E5E7EB` (Gray) - Dividers, edges
- **Text Primary**: `#1F2937` (Dark Gray) - Main text
- **Text Secondary**: `#6B7280` (Medium Gray) - Secondary text
- **Disabled**: `#D1D5DB` (Light Gray) - Disabled buttons/text

### Game-Specific
- **Player 1**: `#EF4444` (Red)
- **Player 2**: `#3B82F6` (Blue)
- **Player 3**: `#FBBF24` (Amber)
- **Player 4**: `#10B981` (Green)
- **Valid Move Highlight**: `#DBEAFE` (Light Blue, 50% opacity)
- **Selected Cell**: `#BFDBFE` (Medium Blue, 80% opacity)

---

## Spacing Scale

### Base Unit: 8px
- **xs**: 4px - Small internal spacing
- **sm**: 8px - Button padding, small gaps
- **md**: 16px - Standard padding, card spacing
- **lg**: 24px - Section spacing, large gaps
- **xl**: 32px - Major section breaks
- **2xl**: 48px - Top-level spacing

### Use Cases
- **sm (8px)**: Icon-to-text spacing, small button padding
- **md (16px)**: Card padding, button padding, content margins
- **lg (24px)**: Section separation, modal padding
- **xl (32px)**: Screen padding, major layout blocks

---

## Typography

### Font Family
- **Primary Font**: `Roboto` or `Arial` (fallback)
- **Weight Range**: 400 (regular), 600 (medium), 700 (bold)

### Font Sizes
- **Display**: 32px (main menu title)
- **Heading 1**: 28px (screen headers)
- **Heading 2**: 24px (section headers)
- **Heading 3**: 20px (subsection headers)
- **Body Large**: 16px (main content)
- **Body Regular**: 14px (secondary content, labels)
- **Body Small**: 12px (helper text, captions)
- **Caption**: 10px (annotations, timestamps)

### Line Heights
- **Display**: 1.1 (32px)
- **Heading**: 1.2 (24-28px text)
- **Body**: 1.5 (14-16px text)
- **Tight**: 1.3 (labels, buttons)

### Font Weights
- **Regular (400)**: Body text, regular content
- **Medium (600)**: Button labels, subsection headers
- **Bold (700)**: Main headers, important labels

---

## Buttons

### Standard Button
- **Height**: 48px (desktop), 56px (mobile)
- **Min Width**: 96px (desktop), 120px (mobile)
- **Padding**: 12px 24px (desktop), 16px 32px (mobile)
- **Border Radius**: 8px
- **Font Size**: 14px (desktop), 16px (mobile)
- **Font Weight**: 600

### Button States
- **Default**: Background color, text color
- **Hover**: Slightly darker background (10% darkening)
- **Pressed/Active**: Darker background (20% darkening), slight scale down (98%)
- **Disabled**: Gray background (#D1D5DB), gray text (#9CA3AF), 50% opacity

### Button Variants
| Variant | Background | Text | Border |
|---------|-----------|------|--------|
| Primary | #2563EB | White | None |
| Secondary | #FFFFFF | #2563EB | 2px #2563EB |
| Danger | #EF4444 | White | None |
| Success | #10B981 | White | None |
| Disabled | #D1D5DB | #9CA3AF | None |

### Touch Targets
- **Minimum**: 44px × 44px (WCAG standard)
- **Recommended**: 56px × 56px (mobile)
- **Spacing Between**: 8px minimum

---

## Icons

### Icon Grid
- **Small**: 16px × 16px
- **Regular**: 24px × 24px
- **Large**: 32px × 32px
- **XL**: 48px × 48px

### Icon Style
- **Stroke Weight**: 2px (consistent)
- **Corner Radius**: 2px (slight rounding on corners)
- **Padding Inside Icon**: 2px (internal breathing room)

### Icon Use Cases
- **Action Icons**: Dice button, BUMP button, declare win
- **UI Icons**: Menu, settings, help, close
- **Status Icons**: Valid move check, error cross, win star

---

## Animations

### Duration Standards
- **Quick Feedback**: 150ms (button press, toggle, quick feedback)
- **Transition**: 250ms (fade in/out, slide)
- **Entrance**: 300ms (modal appear, complex animation)
- **Drag/Drop**: 200ms (smooth movement)

### Easing Curves
- **Ease-In**: `cubic-bezier(0.4, 0, 1, 1)` - Exit animations
- **Ease-Out**: `cubic-bezier(0, 0, 0.2, 1)` - Entrance animations
- **Ease-In-Out**: `cubic-bezier(0.4, 0, 0.2, 1)` - Continuous animations
- **Custom Spring**: `cubic-bezier(0.68, -0.55, 0.265, 1.55)` - Bounce effect

### Animation Targets
- **Scale**: 0.95 (pressed) to 1.0 (normal)
- **Opacity**: 0 to 1 (fade in)
- **Translate**: -10px to 0px (slide in from left)
- **Rotate**: 0deg to 360deg (spin for loading)

---

## Layout Components

### Card
- **Border Radius**: 8px
- **Padding**: 16px
- **Shadow**: `0 1px 3px rgba(0,0,0,0.1)`
- **Background**: #FFFFFF
- **Border**: 1px #E5E7EB

### Modal/Dialog
- **Max Width**: 480px (desktop), 90vw (mobile)
- **Border Radius**: 12px
- **Padding**: 24px
- **Shadow**: `0 10px 25px rgba(0,0,0,0.2)`
- **Backdrop**: rgba(0,0,0,0.5)

### Input Field
- **Height**: 40px
- **Padding**: 8px 12px
- **Border Radius**: 6px
- **Border**: 1px #D1D5DB
- **Font Size**: 14px
- **Focus Border**: 2px #2563EB

---

## Responsive Breakpoints

### Screen Sizes
- **Mobile**: < 480px
- **Tablet**: 480px - 768px
- **Desktop**: > 768px

### Layout Adjustments
| Element | Mobile | Tablet | Desktop |
|---------|--------|--------|---------|
| Font Size (Body) | 14px | 14px | 16px |
| Button Height | 56px | 48px | 48px |
| Padding | 12px | 16px | 24px |
| Column Count | 1 | 2 | 3 |

---

## Accessibility

### Color Contrast
- **Text on Background**: Minimum 4.5:1 ratio (WCAG AA)
- **Large Text**: Minimum 3:1 ratio
- **UI Components**: Minimum 3:1 ratio

### Touch Targets
- **Minimum Size**: 44px × 44px
- **Minimum Spacing**: 8px between targets
- **Recommended**: 56px × 56px for mobile games

### Visual Indicators
- **Focus State**: 2px outline, #2563EB, 4px offset
- **Disabled State**: Visual distinction (color + opacity)
- **Active State**: Clear visual feedback (scale, color)

---

## Dark Mode (Future)

### Dark Palette
- **Background**: `#1F2937`
- **Surface**: `#111827`
- **Border**: `#374151`
- **Text Primary**: `#F9FAFB`
- **Text Secondary**: `#D1D5DB`

---

## Related Documents

- HUD_ARCHITECTURE.md
- UI_STANDARDS.md
- SPRINT_5_UI_PREP.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Implementation
