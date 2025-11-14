# UI ENGINEERING TEAM - SPRINT 5 DESIGN PHASE
## HUD & Menu System Preparation - BEGIN NOW

**From**: Amp (Managing Engineer)  
**To**: UI Engineer Agent  
**Date**: Nov 14, 2025  
**Authority**: BEGIN DESIGN PHASE NOW  
**Target Completion**: Design complete by Dec 4 (when Sprint 4 finishes)  
**Formal Execution**: Sprint 5 starts Dec 12

---

## MISSION OVERVIEW

Design complete UI/HUD system to be implemented Dec 12-19. This enables:
- Players to see game state during gameplay
- Players to make game selections
- Players to configure settings
- Beautiful, responsive UI across all devices

**Current Status**: Design phase (parallel with Gameplay/Board teams)

---

## UI DESIGN TASKS

### Task 1: HUD Manager Architecture
**Purpose**: Orchestrate all in-game UI elements

**HUD Components to Design**:
1. **Dice Roll Button** - Click to roll dice
2. **Bump Button** - Click to bump opponent (if allowed)
3. **Declare Win Button** - Click to declare victory (if eligible)
4. **Scoreboard Display** - Show players, scores, turn order
5. **Phase Indicator** - Show current game phase (Rolling → Waiting → etc.)
6. **Chip Counter** - Show how many chips each player has placed

**Design Considerations**:
```
Layout Questions:
- Where do buttons appear on screen?
- How do they respond to different resolutions?
- Which elements are always visible?
- Which elements show/hide based on game state?
- Safe area handling (mobile notches)?

Button States:
- Dice Roll Button: Available during roll phase, grayed out otherwise
- Bump Button: Shows only if bump is legal, visible only when available
- Declare Win Button: Shows only when player has won, clickable only then

Data Flow:
- When does HUD update from game state?
- What events trigger HUD updates?
- How does HUD send input back to GameStateManager?

Feedback:
- Button presses (visual feedback, animation)
- State transitions (fade, slide, highlight)
- Phase changes (indicator animation)
```

**Deliverable**: Architecture document with:
- HUD component list & positions
- State machine diagram (which elements visible when?)
- Button state definitions
- Event flow (game state → HUD updates)
- Responsive layout strategy
- Mobile safe area strategy

**Target**: Complete by Nov 20

---

### Task 2: Popup System Design
**Purpose**: Display critical game messages and decisions

**Popup Types**:
1. **Penalty Popup**
   - Title: "Penalty!"
   - Message: "Triple doubles - you lose your turn"
   - Buttons: OK
   - Appears: After rolling triple doubles
   - Dismissal: Auto-dismiss or click OK

2. **Pass the Chip Popup**
   - Title: "Pass the Chip"
   - Message: "You rolled a [N]. Choose a chip to move."
   - Buttons: Yes (proceed), No (I forfeit)
   - Appears: After roll in PassTheChip mode
   - Required: Player must respond

3. **Take It Off Popup**
   - Title: "Take It Off!"
   - Message: "You bumped opponent's chip!"
   - Buttons: OK
   - Appears: After bump action
   - Feedback: Visual celebration animation

**Design Considerations**:
```
Popup Manager:
- How to queue popups if multiple show simultaneously?
- How to prevent player interaction during popup?
- How to handle popup timeouts?
- Animation strategy (fade in/out, slide)?

Modal Behavior:
- Popup blocks game input until dismissed
- Darken background to focus attention
- Button responsiveness (touch targets ≥44px)

Responsive Design:
- Popup centered on screen
- Scales to fit screen size
- Text wrapping for long messages
- Button sizing for different devices
```

**Deliverable**: Design document with:
- Popup layouts (wireframes/descriptions)
- Queue management strategy
- Animation specifications
- Dismissal strategies per popup type
- Button design & sizing
- Responsive scaling approach

**Target**: Complete by Nov 20

---

### Task 3: Game Mode Selection Screen
**Purpose**: Let player choose which of 5 game modes to play

**Screen Elements**:
1. **Mode Selection Grid**
   - 5 mode cards (one per game mode)
   - Each shows: Mode name, description, player count, difficulty
   - Each is clickable to select

2. **Mode Card Design**
   - Game mode icon/image
   - "Bump 5" title
   - "Get 5 in a row to win" description
   - "2-4 Players" info
   - Difficulty indicator

3. **Player Count Selector**
   - Slider or buttons: 1-4 players
   - Shows based on selected mode (Solitary = 1 only)

4. **Start Game Button**
   - Large, prominent
   - Appears after mode + player count selected

5. **Back/Main Menu Button**
   - Returns to main menu
   - Top-left or bottom-left

**Design Considerations**:
```
Layout:
- Portrait or landscape mode?
- Grid layout for 5 modes? (2×2 + 1)
- Scrollable if needed?

Interaction:
- Tap to select mode (highlight/border shows selection)
- Visual feedback (animate selection)
- Mode preview possible? (show example board?)

Responsive:
- Works on phone (small screen)
- Works on tablet (medium screen)
- Works on desktop browser (large screen)

UX Flow:
1. Player sees 5 mode options
2. Player taps mode (highlights)
3. Player selects player count (if applicable)
4. Player taps "Start Game"
5. Game begins
```

**Deliverable**: Design document with:
- Screen layout wireframe
- Mode card specifications
- Interactive flow diagram
- Selection feedback animation
- Responsive breakpoints
- Button sizing

**Target**: Complete by Nov 21

---

### Task 4: Main Menu Screen
**Purpose**: Game entry point and navigation hub

**Screen Elements**:
1. **Game Title**
   - Large, prominent at top
   - Logo/branding

2. **Main Buttons**
   - "New Game" → Goes to Mode Selection
   - "Resume" → Continues previous game (if exists)
   - "Settings" → Goes to Settings menu
   - "Rules" → Shows Rules/Instructions
   - "Exit" → Closes game

3. **Visual Design**
   - Background image/pattern
   - Theme colors
   - Button styling

**Design Considerations**:
```
Navigation:
- Where are buttons positioned? (vertical stack? grid?)
- Button sizes (must be ≥44px for mobile)
- Safe area handling (notches, etc.)

State:
- "Resume" only shows if game in progress
- Should show "Resume [Mode Name]" for clarity

Animations:
- Screen fade-in animation
- Button hover effects
- Button press feedback

Theme:
- Color scheme for all buttons
- Font choices
- Background imagery
```

**Deliverable**: Design document with:
- Screen layout
- Button specifications
- Navigation flow
- Resume game logic
- Animation specifications
- Visual theme/branding

**Target**: Complete by Nov 21

---

### Task 5: Settings & Rules Screens
**Purpose**: Configuration and game instruction displays

**Settings Screen**:
```
Options:
- Volume slider (0-100%)
- Language selector (English, Spanish, French, etc.)
- Difficulty toggle (if needed)
- Dark mode toggle (if needed)

Layout:
- Vertical list of settings
- Each setting has label + control
- Back button to return to main menu
```

**Rules Screen**:
```
Content:
- Game overview (what is Bump U?)
- How to play (general)
- Game mode selection
- Each game mode tab with rules
- Victory conditions

Navigation:
- Scrollable content
- Tabs for each game mode
- Back button
```

**Design Considerations**:
```
Rules Presentation:
- Long scrollable content
- Tab navigation per mode?
- Text + diagrams?
- Mobile scrolling performance

Settings UX:
- Simple, clear options
- Visual feedback on changes
- Save/Apply button needed?
- Reset to defaults option?
```

**Deliverable**: Design documents with:
- Settings layout & controls
- Rules content structure
- Navigation strategy
- Scrolling & tab design
- Mobile responsiveness

**Target**: Complete by Nov 22

---

### Task 6: Responsive Layout Strategy
**Purpose**: Ensure UI works on all device sizes

**Breakpoints to Design For**:
```
Mobile (≤600px width):
- Vertical layout
- Full-width buttons
- Touch targets ≥44px
- Notch handling

Tablet (600-1024px):
- Medium layout
- Multi-column where appropriate
- Balanced spacing

Desktop (>1024px):
- Horizontal layout possible
- Use white space
- Large touch targets
```

**Design Considerations**:
```
Safe Area:
- iOS notch/island handling
- Android system button area
- Always leave padding

Scaling:
- Text sizes adjust per breakpoint
- Button sizes stay ≥44px
- Images scale appropriately

Canvas Settings:
- Scale with screen size?
- Reference resolution?
- UI aspect ratio locked?

Testing:
- Prototype on real devices
- Test landscape + portrait
- Test on various screen sizes
```

**Deliverable**: Design document with:
- Responsive grid system
- Breakpoint specifications
- Safe area padding strategy
- Text & button sizing guide
- Image scaling approach
- Canvas configuration recommendations

**Target**: Complete by Nov 22

---

## DESIGN DOCUMENT TEMPLATE

For each task, create a document structured as:

```markdown
## [Component Name]

### Purpose
[Why this exists and what problem it solves]

### Visual Design
[Description or wireframe of layout]

### Components
- [Component 1]: [Purpose]
- [Component 2]: [Purpose]

### Responsive Design
- Mobile: [Layout description]
- Tablet: [Layout description]
- Desktop: [Layout description]

### Interactions
- [User action]: [System response]
- [User action]: [System response]

### State Machine
[What states can this UI be in, and when?]

### Integration Points
- [Connects to]: [How]
- [Connects to]: [How]

### Edge Cases
- [Edge case]: [How handled]
- [Edge case]: [How handled]

### Animation Plan
- [Element]: [Animation description]
- [Element]: [Animation description]

### Performance Notes
- [Concern]: [Solution]

### Dependencies
- [System]: [Why needed]

### Open Questions
- [Question]
- [Question]
```

---

## REFERENCE MATERIALS

**Game Modes Reference**: SPRINT_3_DETAILED_BRIEFING.md
- Understand all 5 modes to design mode selection UI
- Special rules may affect HUD (e.g., PassTheChip needs chip selection)

**Board System Reference**: Board team's Sprint 4 design (will be ready Nov 21)
- Understand board dimensions to design HUD layout
- Know animation timing for HUD feedback

**Coding Standards**: CODING_STANDARDS.md
- Follow when implementing UI code in Sprint 5

---

## DAILY PROGRESS REPORTING

Report at 9 AM UTC standup:
- ✅ Completed since yesterday
- 🔄 In progress today
- 🚫 Blockers
- 📈 % complete
- 📋 Design documents ready

**Example Day 1**:
> ✅ Completed: None (kickoff day)  
> 🔄 In Progress: HUD Manager & Popup system design  
> 🚫 Blockers: None  
> 📈 Progress: 15%  
> 📋 Design docs: 0/6 ready

---

## SUCCESS CRITERIA

✅ **Complete UI design** for all screens  
✅ **Design documents** for all 6 tasks  
✅ **Responsive layout plan** for all breakpoints  
✅ **Animation specifications** defined  
✅ **Integration strategy** with gameplay  
✅ **Ready to implement** Dec 12 (no design gaps)  
✅ **Mobile-first approach** confirmed

---

## MANAGING ENGINEER SUPPORT

Available for:
- UI/UX design questions
- Responsive layout guidance
- Animation strategy clarification
- Game flow integration questions
- Design review & feedback

Contact: Direct message for urgent, #ui for updates

---

## TIMELINE

| Date | Phase | Target |
|------|-------|--------|
| Nov 14-15 | Design start | Task 1-2 begin |
| Nov 16-18 | Design progress | Task 3-4 progress |
| Nov 19-21 | Design completion | Task 5-6 complete |
| Nov 22-23 | Design review | Final refinements |
| Nov 24 | Ready for execution | Sprint 3 & 4 complete, Sprint 5 ready |
| Dec 12 | Sprint 5 kickoff | Begin implementation |

---

## NEXT: FORMAL SPRINT 5 EXECUTION

When Sprints 3-4 complete, I'll issue TEAM_DISPATCH_SPRINT5_UI_EXECUTE.md with:
- Detailed implementation tasks
- Component specifications
- Unit test requirements
- Code review criteria

Your design work now ensures Sprint 5 execution is smooth and on-schedule.

---

**Status**: BEGIN DESIGN PHASE NOW  
**Authority**: FULL UI OWNERSHIP  
**Deadline**: Design complete Dec 4  
**Impact**: Enables Sprint 5 & 6 execution, player experience

**You're authorized to proceed. Begin immediately. Design thoroughly. Deliver on time.**

---

**From**: Amp (Managing Engineer)  
**Date**: Nov 14, 2025

---

*End of Dispatch*
