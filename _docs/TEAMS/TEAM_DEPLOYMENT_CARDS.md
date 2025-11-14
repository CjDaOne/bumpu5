# TEAM DEPLOYMENT CARDS - QUICK REFERENCE
## All-Hands Documentation Cleanup - Nov 14, 2025
## Print, Read, Execute

---

## CARD 1: GAMEPLAY ENGINEER

**MISSION**: Create GAME_MODES_RULES_SUMMARY.md  
**DEADLINE**: 6:00 PM UTC TODAY  
**EFFORT**: 90-120 minutes  
**LOC TARGET**: 300+

### YOUR TASK IN 30 SECONDS
Document all 5 game modes with rules, win conditions, and special rules (60 lines per mode).

### STRUCTURE (Copy This)
```
# Game Modes Rules Summary

Created: Nov 14, 2025
Owner: Gameplay Engineer
Status: ACTIVE

## Game 1: Bump 5
[Rules, Win, Special: ~60 lines]

## Game 2: Krazy 6
[Rules, Win, Special: ~60 lines]

## Game 3: Pass The Chip
[Rules, Win, Special: ~60 lines]

## Game 4: Bump U And 5
[Rules, Win, Special: ~60 lines]

## Game 5: Solitary
[Rules, Win, Special: ~60 lines]

## Related Documents
- SPRINT_3_DETAILED_BRIEFING.md
```

### TIMELINE
- **12:00 PM**: Start reading dispatch
- **12:30 PM**: Begin writing (use structure above)
- **2:00 PM**: Ready for ME review
- **2:30 PM**: Approved & published

### QUALITY CHECKLIST
- [ ] All 5 modes covered
- [ ] Each ~60 lines
- [ ] 300+ lines total
- [ ] Clear rules
- [ ] Win conditions explicit
- [ ] No contradictions with code

### SUBMIT TO
```
/home/cjnf/Bump U/GAME_MODES_RULES_SUMMARY.md
```

### STUCK? DO THIS
1. Can't remember a rule? Check `SPRINT_3_DETAILED_BRIEFING.md`
2. Unclear on format? Check this card's STRUCTURE section
3. Can't find time? Focus: Rules → Win → Special (in that order per mode)
4. Real blocker? DM ME immediately (< 15 min response)

### YOU'VE GOT THIS
- 1 document (not 3)
- Shortest effort (90-120 min)
- Straightforward content (rules you wrote)
- Simple format (5 sections, same structure)

**GO. FINISH BY 2:30 PM FOR EARLY BREAK.**

---

## CARD 2: UI ENGINEER

**MISSION**: Create 3 Documents (Design System, HUD Architecture, UI Standards)  
**DEADLINE**: 6:00 PM UTC TODAY  
**EFFORT**: 150 minutes TOTAL (parallel execution)  
**LOC TARGET**: 550+

### YOUR TASK IN 30 SECONDS
Create design system (colors, spacing, fonts), HUD architecture (components, state), and UI standards (accessibility, mobile).

### DOCUMENTS & EFFORT
| Document | Lines | Time |
|----------|-------|------|
| UI_DESIGN_SYSTEM.md | 250+ | 60 min |
| HUD_ARCHITECTURE.md | 200+ | 60 min |
| UI_STANDARDS.md | 100+ | 30 min |

### STRUCTURE FOR DESIGN SYSTEM (250+ lines)
```
# UI Design System

Created: Nov 14, 2025
Owner: UI Engineer
Status: ACTIVE

## Colors
- Primary: [hex]
- Secondary: [hex]
- Disabled: [hex]
- Success/Error: [hex]

## Spacing
- 8px: Use case
- 16px: Use case
- 24px: Use case
- etc.

## Typography
- Font: [name]
- Sizes: [list]
- Weights: [list]
- Line heights: [list]

## Buttons
- Standard size
- Mobile size
- Touch target >= 44px

## Icons
- Size grid
- Stroke weight
- Padding

## Animations
- Duration standards
- Easing curves
- Keyframe targets

## Dark Mode
[If applicable]

## Related Documents
- HUD_ARCHITECTURE.md
- SPRINT_5_UI_PREP.md
```

### STRUCTURE FOR HUD ARCHITECTURE (200+ lines)
```
# HUD Architecture

Created: Nov 14, 2025
Owner: UI Engineer
Status: ACTIVE

## Overview
[What HUD manages]

## Components
- DiceRollButton
- BumpButton
- DeclareWinButton
- ScoreboardDisplay
- PhaseIndicator

## State Machine
[3-5 states: Menu, Playing, Waiting, etc.]

## Event Binding
[How HUD connects to GameStateManager]

## Animations
[Button, state, transitions]

## Prefab Hierarchy
[Canvas → HUD → Components]

## Related Documents
- UI_DESIGN_SYSTEM.md
- SPRINT_5_UI_PREP.md
```

### STRUCTURE FOR UI STANDARDS (100+ lines)
```
# UI Standards

Created: Nov 14, 2025
Owner: UI Engineer
Status: ACTIVE

## Accessibility
- Touch targets >= 44px
- Color contrast ratios
- Font sizes minimum

## Mobile Responsiveness
- Supported resolutions
- Safe area handling
- Portrait/landscape

## Platform Differences
- WebGL
- Android
- iOS specifics

## Button Affordance
[Hover, press, disabled states]

## Error Messaging
[Format, placement, tone]

## Related Documents
- UI_DESIGN_SYSTEM.md
- HUD_ARCHITECTURE.md
```

### TIMELINE (PARALLEL EXECUTION)
- **12:00 PM**: Start reading dispatch (15 min)
- **12:15 PM**: Begin all 3 docs SIMULTANEOUSLY
  - Designer 1: DESIGN_SYSTEM.md (60 min, done 1:15 PM)
  - Designer 2: HUD_ARCHITECTURE.md (60 min, done 1:15 PM)
  - Designer 3: UI_STANDARDS.md (30 min, done 12:45 PM)
- **1:15 PM**: All 3 ready for ME review
- **2:00 PM**: All 3 approved & published

### QUALITY CHECKLIST
- [ ] DESIGN_SYSTEM.md: 250+ lines, all design tokens
- [ ] HUD_ARCHITECTURE.md: 200+ lines, clear component list
- [ ] UI_STANDARDS.md: 100+ lines, accessibility covered
- [ ] All formats consistent
- [ ] All links work (or marked TBD)
- [ ] Cross-references between docs

### SUBMIT TO
```
/home/cjnf/Bump U/UI_DESIGN_SYSTEM.md
/home/cjnf/Bump U/HUD_ARCHITECTURE.md
/home/cjnf/Bump U/UI_STANDARDS.md
```

### DIVIDE & CONQUER
```
If 3 people on UI team:
- Person 1: DESIGN_SYSTEM.md (colors, spacing, typography, buttons)
- Person 2: HUD_ARCHITECTURE.md (components, state, events, animations)
- Person 3: UI_STANDARDS.md (accessibility, mobile, platforms, affordance)

All three in parallel = 60-90 min, not 150 min.
```

### STUCK? DO THIS
1. What goes in Design System? Check existing UI designs → codify
2. What's HUD architecture? Components + state machine + events
3. Mobile standard? iOS/Android safe areas, 44px min touch
4. Real blocker? DM ME immediately (< 15 min response)

### YOU'VE GOT THIS
- 3 documents, but they're PARALLEL (divide team)
- 150 min TOTAL (50 min per person if 3 people)
- Clear structure (use templates above)
- Content is what you already know (design decisions + standards)

**GO. FINISH BY 1:15 PM FOR LONG BREAK.**

---

## CARD 3: BOARD ENGINEER

**MISSION**: Create 3 Documents (Board Architecture, Input Handling, Asset Integration)  
**DEADLINE**: 6:00 PM UTC TODAY  
**EFFORT**: 195 minutes TOTAL (parallel execution)  
**LOC TARGET**: 650+

### YOUR TASK IN 30 SECONDS
Document board grid system, cell interactions, and asset pipeline.

### DOCUMENTS & EFFORT
| Document | Lines | Time |
|----------|-------|------|
| BOARD_ARCHITECTURE.md | 300+ | 90 min |
| INPUT_HANDLING.md | 200+ | 60 min |
| ASSET_INTEGRATION.md | 150+ | 45 min |

### STRUCTURE FOR BOARD ARCHITECTURE (300+ lines)
```
# Board Architecture

Created: Nov 14, 2025
Owner: Board Engineer
Status: ACTIVE

## Overview
12-cell grid system, cell states, board manager

## Grid System
- Cell layout (12 cells, arrangement)
- Cell dimensions
- Spacing between cells
- Board dimensions

## Cell Data
- State machine (empty, has_chip_1, has_chip_2, etc.)
- Valid move calculation
- Highlight states (none, valid, selected)

## BoardGridManager
- Initialization
- Cell references
- State management
- Event triggers

## Integration with GameStateManager
- State synchronization
- Move validation
- Move execution
- Board update on action

## Prefab Hierarchy
```
Canvas
  Board
    Grid
      Cell_1 (prefab instance)
      Cell_2 (prefab instance)
      ...
      Cell_12 (prefab instance)
```

## Component Relationships
[List MonoBehaviours and their connections]

## Related Documents
- INPUT_HANDLING.md
- ASSET_INTEGRATION.md
```

### STRUCTURE FOR INPUT HANDLING (200+ lines)
```
# Input Handling

Created: Nov 14, 2025
Owner: Board Engineer
Status: ACTIVE

## Tap/Click Detection
- Detection layer (where?)
- Event routing (to where?)
- Platform differences (mouse vs touch)

## Move Validation Flow
```
Tap cell → Get cell index → Check valid moves 
→ Is valid? → Execute move : Show invalid feedback
```

## Valid Moves Highlighting
- When to show (after dice roll)
- How to show (highlight color, scale, etc.)
- When to hide (after move or timeout)

## Cell Interaction States
- Idle
- Highlighted (valid move available)
- Selected (player tapped)
- Animating (during move)

## Input Blocking
- During animations (block input)
- During invalid states (block input)
- Recovery (when to allow input again)

## Platform-Specific Input
- Mouse (hover, click)
- Touch (press, release)
- Controller (future?)

## Event System
[Describe how input events connect to GameStateManager]

## Related Documents
- BOARD_ARCHITECTURE.md
- ASSET_INTEGRATION.md
```

### STRUCTURE FOR ASSET INTEGRATION (150+ lines)
```
# Asset Integration

Created: Nov 14, 2025
Owner: Board Engineer
Status: ACTIVE

## Asset Import Workflow
1. Import PNG
2. Configure import settings
3. Slice sprites
4. Create prefabs
5. Reference in BoardGridManager

## Naming Conventions
- Sprites: `Board_Cell_Empty`, `Board_Cell_WithChip`, etc.
- Prefabs: `CellView`, `ChipView`, etc.
- Animations: `Cell_Highlight`, `Chip_Drop`, etc.

## Sprite Slicing
- Grid size per asset
- Offset specifications
- Padding rules

## Prefab Structure
- CellView
  - SpriteRenderer
  - CanvasGroup (for highlighting)
  - Animator (for animations)
- ChipView
  - SpriteRenderer
  - Animator

## Material Requirements
- Default material
- Highlight material (lighter/brighter)
- Disabled material (grayed)

## Animation Clips
- Naming: `{Component}_{State}` (e.g., `Cell_HighlightEnter`)
- Duration targets (250ms highlight, 300ms move, etc.)
- Loop settings

## Asset Organization
```
Assets/
  Board/
    Sprites/
      board_cells.png
      board_chips.png
    Prefabs/
      CellView.prefab
      ChipView.prefab
    Animations/
      Cell/
      Chip/
    Materials/
```

## Related Documents
- BOARD_ARCHITECTURE.md
- INPUT_HANDLING.md
```

### TIMELINE (PARALLEL EXECUTION)
- **12:00 PM**: Start reading dispatch (15 min)
- **12:15 PM**: Begin all 3 docs SIMULTANEOUSLY
  - Engineer 1: BOARD_ARCHITECTURE.md (90 min, done 1:45 PM)
  - Engineer 2: INPUT_HANDLING.md (60 min, done 1:15 PM)
  - Engineer 3: ASSET_INTEGRATION.md (45 min, done 1:00 PM)
- **1:45 PM**: All 3 ready for ME review
- **2:15 PM**: All 3 approved & published

### QUALITY CHECKLIST
- [ ] BOARD_ARCHITECTURE.md: 300+ lines, clear grid & state model
- [ ] INPUT_HANDLING.md: 200+ lines, flow diagram included
- [ ] ASSET_INTEGRATION.md: 150+ lines, naming conventions clear
- [ ] Prefab hierarchy documented
- [ ] State machines clear
- [ ] Event flow explained

### SUBMIT TO
```
/home/cjnf/Bump U/BOARD_ARCHITECTURE.md
/home/cjnf/Bump U/INPUT_HANDLING.md
/home/cjnf/Bump U/ASSET_INTEGRATION.md
```

### DIVIDE & CONQUER
```
If 3 people on Board team:
- Person 1: BOARD_ARCHITECTURE.md (grid, cells, manager, integration)
- Person 2: INPUT_HANDLING.md (tap detection, validation, highlighting, blocking)
- Person 3: ASSET_INTEGRATION.md (naming, sprites, prefabs, animations)

All three in parallel = ~90 min, not 195 min.
```

### STUCK? DO THIS
1. Grid system? 12 cells, what's the layout? Document it.
2. Cell states? List all states a cell can be in.
3. Input flow? Start with "tap" and end with "move executed"
4. Assets? Check existing art → document how to organize it
5. Real blocker? DM ME immediately (< 15 min response)

### YOU'VE GOT THIS
- 3 documents, PARALLEL execution
- Clear structures (use templates above)
- Content is system you know (board layout, input, assets)
- 195 min total for team, 65 min per person

**GO. FINISH BY 1:45 PM FOR BREAK.**

---

## CARD 4: BUILD ENGINEER

**MISSION**: Create 3 Documents (Build Pipeline, Platform Requirements, App Store)  
**DEADLINE**: 6:00 PM UTC TODAY  
**EFFORT**: 195 minutes TOTAL (parallel execution)  
**LOC TARGET**: 600+

### YOUR TASK IN 30 SECONDS
Document build pipeline for 3 platforms, platform-specific constraints, and app store submission requirements.

### DOCUMENTS & EFFORT
| Document | Lines | Time |
|----------|-------|------|
| BUILD_PIPELINE.md | 250+ | 90 min |
| PLATFORM_REQUIREMENTS.md | 200+ | 60 min |
| APP_STORE_REQUIREMENTS.md | 150+ | 45 min |

### STRUCTURE FOR BUILD PIPELINE (250+ lines)
```
# Build Pipeline

Created: Nov 14, 2025
Owner: Build Engineer
Status: ACTIVE

## WebGL Build
- Player settings required
- Build settings
- IL2CPP vs mono
- Compression settings
- Output location
- Test environment setup

## Android Build
- Player settings (bundle ID, version, etc.)
- Keystore setup (signing)
- APK vs AAB
- Build output location
- Play Store testing

## iOS Build
- Xcode project export
- Provisioning profiles
- Signing certificates
- Build settings
- Testflight setup

## Build Output
- Where files go
- Naming conventions
- Size targets
- Load time targets

## Optimization Passes
- Unused asset removal
- Texture compression
- Code stripping
- Load time optimization

## CI/CD Pipeline (Future)
[Optional: GitHub Actions, Jenkins, etc.]

## Build Troubleshooting
- Common errors & solutions
- Debug checklist

## Related Documents
- PLATFORM_REQUIREMENTS.md
- APP_STORE_REQUIREMENTS.md
```

### STRUCTURE FOR PLATFORM REQUIREMENTS (200+ lines)
```
# Platform Requirements

Created: Nov 14, 2025
Owner: Build Engineer
Status: ACTIVE

## Performance Targets
- FPS: 60 target, 30 minimum
- Memory: [limits per platform]
- Load time: < 5 seconds
- Startup time: < 3 seconds

## WebGL Specifics
- Browser support (Chrome, Safari, Firefox, Edge)
- WebGL version 2.0+
- File size limit (if hosted)
- Bandwidth requirements

## Android Specifics
- Min API level: [version]
- Target API level: [version]
- RAM: 2GB minimum, 4GB+ recommended
- Screen sizes supported
- Orientation support

## iOS Specifics
- Min iOS version: [version]
- Device types (iPhone, iPad)
- Screen sizes: All iPhones, iPad models
- Safe area handling (notch, etc.)
- Battery drain limits

## Input Requirements
- Touch latency: < 100ms
- Multi-touch support: 2-finger minimum
- Controller support: [future?]

## Network Requirements
(If applicable)
- Bandwidth for assets
- Server requirements
- Latency tolerance

## Testing Device Matrix
| Platform | Device | OS | Status |
|----------|--------|----|----|
| Android | Galaxy S21 | 12 | Test |
| Android | Pixel 5 | 13 | Test |
| iOS | iPhone 12 | 15 | Test |
| iOS | iPhone 13 | 16 | Test |
| WebGL | Chrome | Latest | Test |

## Related Documents
- BUILD_PIPELINE.md
- APP_STORE_REQUIREMENTS.md
```

### STRUCTURE FOR APP STORE REQUIREMENTS (150+ lines)
```
# App Store Requirements

Created: Nov 14, 2025
Owner: Build Engineer
Status: ACTIVE

## Google Play Store (Android)
- App name & description
- Icon requirements (512x512)
- Screenshot requirements (min 2, max 8)
- Feature graphic (1024x500)
- Privacy policy (required)
- Content rating (ESRB, PEGI, etc.)
- Pricing & countries
- Submission checklist

## Apple App Store (iOS)
- App name & subtitle
- App description
- Keywords (max 30 chars)
- Support URL
- Privacy policy URL
- App icon (1024x1024)
- Screenshots (min 2 per device)
- Preview video (optional)
- Content rating (ESRB, PEGI, etc.)
- TestFlight beta testing
- Submission checklist

## WebGL Deployment
- Hosting platform options (itch.io, custom server, etc.)
- Security requirements (HTTPS)
- Performance optimization
- Analytics setup

## Common Rejection Reasons
- Crashers (test before submit)
- Misleading content (accurate description)
- Incomplete content (full game, not beta)
- Ad behavior (if applicable)

## Pre-Submission Checklist
- [ ] Game fully playable
- [ ] No known crashes
- [ ] All modes working
- [ ] Icons & screenshots ready
- [ ] Privacy policy ready
- [ ] Content rating submitted
- [ ] Build uploaded & tested
- [ ] Release notes written

## Related Documents
- BUILD_PIPELINE.md
- PLATFORM_REQUIREMENTS.md
```

### TIMELINE (PARALLEL EXECUTION)
- **12:00 PM**: Start reading dispatch (15 min)
- **12:15 PM**: Begin all 3 docs SIMULTANEOUSLY
  - Engineer 1: BUILD_PIPELINE.md (90 min, done 1:45 PM)
  - Engineer 2: PLATFORM_REQUIREMENTS.md (60 min, done 1:15 PM)
  - Engineer 3: APP_STORE_REQUIREMENTS.md (45 min, done 1:00 PM)
- **1:45 PM**: All 3 ready for ME review
- **2:15 PM**: All 3 approved & published

### QUALITY CHECKLIST
- [ ] BUILD_PIPELINE.md: 250+ lines, all 3 platforms covered
- [ ] PLATFORM_REQUIREMENTS.md: 200+ lines, constraints clear
- [ ] APP_STORE_REQUIREMENTS.md: 150+ lines, submission ready
- [ ] No ambiguities in build steps
- [ ] Device matrix included
- [ ] Checklists provided

### SUBMIT TO
```
/home/cjnf/Bump U/BUILD_PIPELINE.md
/home/cjnf/Bump U/PLATFORM_REQUIREMENTS.md
/home/cjnf/Bump U/APP_STORE_REQUIREMENTS.md
```

### DIVIDE & CONQUER
```
If 3 people on Build team:
- Person 1: BUILD_PIPELINE.md (WebGL, Android, iOS build steps)
- Person 2: PLATFORM_REQUIREMENTS.md (perf, device specs, targets)
- Person 3: APP_STORE_REQUIREMENTS.md (play store, app store, web)

All three in parallel = ~90 min, not 195 min.
```

### STUCK? DO THIS
1. Build steps? Research Unity build settings for each platform
2. Requirements? Check what's different for 60 FPS on each platform
3. App Store? Use official documentation as reference
4. Device matrix? List phones you'll test on
5. Real blocker? DM ME immediately (< 15 min response)

### YOU'VE GOT THIS
- 3 documents, PARALLEL execution
- Clear structures (use templates above)
- Content is what you research + what you know about platforms
- 195 min total for team, 65 min per person

**GO. FINISH BY 1:45 PM FOR BREAK.**

---

## CARD 5: QA LEAD

**MISSION**: Create 3 Documents (Test Plan, QA Process, Regression Testing)  
**DEADLINE**: 6:00 PM UTC TODAY  
**EFFORT**: 225 minutes TOTAL (parallel execution)  
**LOC TARGET**: 750+

### YOUR TASK IN 30 SECONDS
Document comprehensive test plan for all game modes & platforms, QA process workflow, and regression testing approach.

### DOCUMENTS & EFFORT
| Document | Lines | Time |
|----------|-------|------|
| TEST_PLAN_MASTER.md | 400+ | 120 min |
| QA_PROCESS.md | 200+ | 60 min |
| REGRESSION_TESTING.md | 150+ | 45 min |

### STRUCTURE FOR TEST PLAN MASTER (400+ lines)
```
# Test Plan Master

Created: Nov 14, 2025
Owner: QA Lead
Status: ACTIVE

## Testing Scope
- All 5 game modes: Game1, Game2, Game3, Game4, Game5
- All platforms: WebGL, Android, iOS
- All devices: [list of device types]
- Edge cases: [list of edge case categories]

## Game Mode Test Scenarios (80+ lines)

### Game 1: Bump 5
- Setup test: [steps, expected result]
- Normal turn test: [steps, expected result]
- Bump test: [steps, expected result]
- Win test: [steps, expected result]
- Edge case: [steps, expected result]

### Game 2: Krazy 6
[Similar structure]

### Game 3: Pass The Chip
[Similar structure]

### Game 4: Bump U And 5
[Similar structure]

### Game 5: Solitary
[Similar structure]

## Platform-Specific Tests (50+ lines)

### WebGL Tests
- Browser compatibility: [browsers, versions]
- Performance: [load time, FPS checks]
- Input: [mouse click, keyboard (if applicable)]
- Graphics: [rendering, animations]

### Android Tests
- Device types: [phones, tablets]
- OS versions: [list]
- Performance: [FPS, memory, battery]
- Input: [touch responsiveness]
- Screen sizes: [resolution handling]

### iOS Tests
- Device types: [iPhones, iPads]
- OS versions: [list]
- Performance: [FPS, memory, battery]
- Input: [touch responsiveness]
- Safe area: [notch handling]

## Bug Severity Definitions

### CRITICAL (Blocks Release)
- Game crash/freeze
- Unwinnable state
- Fatal error log

### HIGH (Block Feature)
- Feature doesn't work
- Game logic wrong
- Save/load broken

### MEDIUM (Should Fix)
- UI bug/misalignment
- Animation wrong
- Minor performance issue

### LOW (Nice to Fix)
- Text typo
- Spacing slightly off
- Minor animation stutter

## Pass/Fail Criteria
- All 5 modes playable end-to-end
- No critical bugs
- No logic errors
- < 2 high-severity bugs
- All platforms at 60 FPS (or 30 minimum)

## Test Data Required
- Sample player data
- Saved game files (if applicable)
- Test device list

## Related Documents
- QA_PROCESS.md
- REGRESSION_TESTING.md
```

### STRUCTURE FOR QA PROCESS (200+ lines)
```
# QA Process

Created: Nov 14, 2025
Owner: QA Lead
Status: ACTIVE

## Bug Reporting Workflow

### Step 1: Discover Bug
- Reproduce reliably
- Note exact steps
- Screenshot/video

### Step 2: Log Bug
- Use bug template (see below)
- Assign severity
- Set priority

### Step 3: Triage
- QA Lead reviews
- Assign to Gameplay/UI/Build engineer
- Set deadline

### Step 4: Fix
- Engineer implements fix
- Engineer notifies QA

### Step 5: Verify Fix
- QA re-tests
- Mark as VERIFIED or REOPENED

### Step 6: Close
- QA closes when verified
- Document in release notes (if critical)

## Bug Report Template
```
Title: [Brief description]
Severity: [CRITICAL/HIGH/MEDIUM/LOW]
Platform: [WebGL/Android/iOS]
Device: [Device model & OS version]
Steps to Reproduce:
1. [Step 1]
2. [Step 2]
3. [Step 3]
Expected Result: [What should happen]
Actual Result: [What does happen]
Evidence: [Screenshot/video link]
```

## Severity & Priority Matrix

| Severity | Impact | Fix By | Priority |
|----------|--------|--------|----------|
| CRITICAL | Game unplayable | Immediate | P0 |
| HIGH | Feature broken | 24 hours | P1 |
| MEDIUM | Impacts UX | 48 hours | P2 |
| LOW | Minor issues | After release | P3 |

## Testing Workflow
1. Environment ready?
2. Test plan open
3. Follow test scenario
4. Log any bugs found
5. Continue next scenario

## Sign-Off Criteria
- All critical bugs fixed & verified
- All high bugs fixed or documented
- Test report submitted
- Release notes prepared
- Ready for submission: YES / NO

## Related Documents
- TEST_PLAN_MASTER.md
- REGRESSION_TESTING.md
```

### STRUCTURE FOR REGRESSION TESTING (150+ lines)
```
# Regression Testing

Created: Nov 14, 2025
Owner: QA Lead
Status: ACTIVE

## Definition
Testing that previous working features still work after changes.

## Regression Test Suite

### Critical Path (Must Pass)
- Game launches without crash
- Main menu appears
- Can start new game
- Can play 1 full round of Game 1
- Can declare win
- Can quit to menu

### Game Logic Regression
- All dice rolls valid
- Bump logic correct
- Win detection correct
- Turn order correct
- State transitions correct

### UI Regression
- All buttons clickable
- All text displays correctly
- Animations play smoothly
- No layout issues

### Platform Regression
- WebGL: No rendering artifacts
- Android: Correct safe areas
- iOS: Notch handling correct

## Pre-Release Regression Checklist
- [ ] Critical path passes
- [ ] All game modes playable
- [ ] No crashes found
- [ ] All platforms tested
- [ ] Performance meets targets
- [ ] Known issues documented

## Automated Regression Testing (Future)
- Unit test suite
- Integration test suite
- Performance benchmarks

## Manual Regression Checklist
[Specific steps to manually verify each system]

## Related Documents
- TEST_PLAN_MASTER.md
- QA_PROCESS.md
```

### TIMELINE (PARALLEL EXECUTION)
- **12:00 PM**: Start reading dispatch (15 min)
- **12:15 PM**: Begin all 3 docs SIMULTANEOUSLY
  - QA Engineer 1: TEST_PLAN_MASTER.md (120 min, done 2:15 PM)
  - QA Engineer 2: QA_PROCESS.md (60 min, done 1:15 PM)
  - QA Engineer 3: REGRESSION_TESTING.md (45 min, done 1:00 PM)
- **2:15 PM**: All 3 ready for ME review
- **3:00 PM**: All 3 approved & published

### QUALITY CHECKLIST
- [ ] TEST_PLAN_MASTER.md: 400+ lines, all 5 modes covered
- [ ] QA_PROCESS.md: 200+ lines, workflow is clear
- [ ] REGRESSION_TESTING.md: 150+ lines, critical path defined
- [ ] Bug severity definitions clear
- [ ] Platform matrix included
- [ ] Test scenarios specific (not generic)

### SUBMIT TO
```
/home/cjnf/Bump U/TEST_PLAN_MASTER.md
/home/cjnf/Bump U/QA_PROCESS.md
/home/cjnf/Bump U/REGRESSION_TESTING.md
```

### DIVIDE & CONQUER
```
If 2-3 people on QA team:
- Person 1: TEST_PLAN_MASTER.md (game modes + platform tests)
- Person 2: QA_PROCESS.md (bug workflow, templates, severity)
- Person 3: REGRESSION_TESTING.md (critical path, checklist)

All three in parallel = ~120 min, not 225 min.
Or 2 people:
- Person 1: TEST_PLAN_MASTER.md (120 min) + QA_PROCESS.md (60 min = 180 min)
- Person 2: REGRESSION_TESTING.md (45 min) + support
```

### STUCK? DO THIS
1. What to test? All 5 game modes × 3 platforms = 15 scenarios minimum
2. Test plan? Follow template, copy structure per mode
3. Bug severity? Use matrix above (CRITICAL/HIGH/MEDIUM/LOW)
4. Process? Issue → Triage → Fix → Verify → Close
5. Real blocker? DM ME immediately (< 15 min response)

### YOU'VE GOT THIS
- 3 documents, PARALLEL execution
- Clear templates (use structures above)
- Content is QA expertise (testing, processes, standards)
- 225 min total for team, 75 min per person

**GO. FINISH BY 2:15 PM FOR LONGER BREAK.**

---

## ALL TEAMS: CRITICAL INFO

### Submission Protocol
```
1. Create document in root: /home/cjnf/Bump U/[FILENAME].md
2. Follow template structure provided on your card
3. Target line counts (don't be short)
4. Post in #documentation when ready
5. ME reviews & approves or provides feedback (30 min)
```

### If You Get Stuck
```
PROBLEM: Don't know what to include in [section]
SOLUTION: DM ME (< 15 min response) with specific question

PROBLEM: Can't verify accuracy against code
SOLUTION: DM ME (I'll confirm or provide guidance)

PROBLEM: Running out of time
SOLUTION: DM ME (We'll discuss scope or extend deadline)

PROBLEM: [Other team's] info needed
SOLUTION: DM ME (I'll coordinate or provide data)
```

### Success = Speed
```
Fast submission → ME quick approval → Back to Sprint 3 work
Early finish → Break time → Start Sprint 3 strong

Speed wins today = Better pace for entire project
```

### Mindset
```
You know this stuff. You built the systems.
Document them. It takes minutes per section.
You've got this.

Stop when: 13 documents ✅ All approved ✅ All published ✅
```

---

## THE DEADLINE

**6:00 PM UTC TODAY** = All 13 documents submitted. No exceptions.

If you submit early, ME can review immediately (rolling review).
If you hit 6 PM, you're exactly on time.
If you miss 6 PM, escalation triggers (extended review, rushed consolidation).

**Better to finish at 2 PM than wait until 5:30 PM.**

---

## FINAL THOUGHT

This documentation will save this project weeks of work.
Today's 4-5 hours of effort = 40+ hours of velocity gain tomorrow.

That's math worth caring about.

**Let's finish this today. All in.**

---

**Print these cards. Reference them hourly. Execute with precision.**

**STATUS**: 🔴 DEPLOYMENT ACTIVE NOW

