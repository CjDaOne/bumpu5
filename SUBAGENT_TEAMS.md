# Subagent Teams - Roles & Responsibilities

## Team Structure

The Bump U Box project is organized into 4 specialized subagent teams, each with clear ownership and deliverables.

---

## Team 1: Gameplay Engineering Team

**Lead**: Gameplay Engineer Agent  
**Focus**: Core game logic, rules, and game mode implementations  

### Responsibilities
- Implement all game rules (dice rolls, turns, bumping, winning)
- Build 5 complete game mode classes (Game1–Game5)
- Create TurnManager and GameStateManager
- Implement DiceManager with all edge cases
- Create BoardModel and game state data structures
- Write unit tests for all logic (target: 80%+ coverage)
- Document game rules and state transitions

### Deliverables by Sprint
- **Sprint 1**: BoardCell, Chip, Player, Dice system + unit tests
- **Sprint 2**: TurnManager, GameStateManager, state machine tests
- **Sprint 3**: All 5 game modes fully implemented + mode-specific tests
- **Sprint 4**: Board system integration with valid move generation
- **Sprint 6**: Mode-specific UI logic & win detection hooks

### Success Criteria
- All game modes playable end-to-end
- Zero logic bugs during QA
- 80%+ unit test coverage
- Clear documentation of rules per mode

---

## Team 2: UI/UX Engineering Team

**Lead**: UI Engineer Agent  
**Focus**: User interface, menus, HUD, and user experience  

### Responsibilities
- Build HUD system (Dice button, BUMP button, Declare Win button)
- Create scoreboard display & live score updates
- Implement popup system (PENALTY, PASS THE CHIP, TAKE IT OFF)
- Build Main Menu & Game Mode Selection screen
- Create Rules/Instructions overlay
- Implement Settings menu (volume, language, etc.)
- Handle canvas scaling & responsive layout
- Build menu navigation flow

### Deliverables by Sprint
- **Sprint 5**: All HUD elements + popup system
- **Sprint 6**: Main Menu, Mode Selection, Rules, Settings screens
- **Sprint 7**: Mobile-specific UI adjustments (button sizing, safe areas)
- **Sprint 8**: Final UX polish & accessibility review

### Success Criteria
- All UI elements respond to game state correctly
- Touch targets ≥44px on mobile
- Full game flow (menu → mode selection → gameplay → end)
- Responsive to all screen sizes

---

## Team 3: Board & Interaction Team

**Lead**: Board Engineer Agent  
**Focus**: Board visualization, cell interactions, animations  

### Responsibilities
- Create BoardGridManager (12-cell layout)
- Build Cell prefabs (clickable, highlightable)
- Implement Chip visuals & placement
- Create valid move highlighting
- Build tap/click detection layer
- Implement chip drop & bump animations
- Handle board art import & slicing
- Create board scene hierarchy

### Deliverables by Sprint
- **Sprint 4**: Board Grid Manager, Cell prefabs, Chip visuals
- **Sprint 4**: Valid move highlighting & tap detection
- **Sprint 5**: Animations (chip drop, bump, movement)
- **Sprint 6**: Full board-to-gameplay integration

### Success Criteria
- Board fully interactive
- Moves execute smoothly with feedback
- Animations are satisfying (not jarring)
- Board layout scales to all devices

---

## Team 4: Build & Platform Team

**Lead**: Build Engineer Agent  
**Focus**: Multi-platform builds, optimization, and deployment  

### Responsibilities
- Set up WebGL build pipeline (IL2CPP, compression)
- Configure Android build (APK, signed release)
- Set up iOS build (Xcode export, provisioning)
- Create input handlers for each platform
- Implement safe area layout (iOS notch handling)
- Optimize for mobile performance (FPS limiting, memory)
- Set up CI/CD pipeline (optional: automated WebGL builds)
- Handle app store submissions (metadata, screenshots, descriptions)

### Deliverables by Sprint
- **Sprint 7**: WebGL build (working, optimized)
- **Sprint 7**: Android build (Play Store ready)
- **Sprint 7**: iOS build (App Store ready)
- **Sprint 7**: Platform-specific input handlers & optimizations
- **Sprint 8**: Final app store submissions

### Success Criteria
- Builds work on actual devices (multiple phones, browser)
- 60 FPS on modern devices, 30 FPS minimum on older
- Touch input works reliably
- Memory usage under limits per platform

---

## Team 5: QA & Testing Team

**Lead**: QA Lead Agent  
**Focus**: Quality assurance, playtesting, bug triage  

### Responsibilities
- Create comprehensive test plans (all modes, all platforms)
- Execute manual playtest scenarios
- Log & triage bugs (critical → nice-to-have)
- Verify bug fixes before closure
- Test edge cases (illegal bumps, win declarations, etc.)
- Test on multiple devices & browsers
- Create release notes & known issues list
- Coordinate final sign-off before release

### Deliverables by Sprint
- **Sprint 5**: Playtest plan document + test scenarios
- **Sprint 6**: First playtest round, bug report #1
- **Sprint 7**: Playtest on actual devices, platform-specific bugs
- **Sprint 8**: Final QA pass, release notes, app store submission review

### Success Criteria
- Zero critical bugs at release
- All 5 modes tested end-to-end
- All platforms tested (WebGL, Android, iOS)
- Zero app store rejection issues

---

## Coordination & Communication

### Daily Standup (15 min)
Each team reports:
- ✅ Completed since last standup
- 🔄 In progress
- 🚫 Blockers (if any)

### Weekly Sprint Review (Friday, 30 min)
- Demos of completed work
- Sprint retrospective
- Next sprint planning

### Async Communication
- **#gameplay**: Gameplay team updates
- **#ui**: UI team updates
- **#build**: Build team updates
- **#qa**: QA team updates
- **#blockers**: Any cross-team issues

### Escalation Path
1. Team lead attempts resolution
2. Managing Engineer (@me) reviews & mediates
3. Final decision: Managing Engineer

---

## Dependencies & Critical Path

| Task | Depends On | Team |
|------|-----------|------|
| Board Integration | Core Logic Complete | Gameplay + Board |
| UI → Game State | Board Integration | UI Team |
| Platform Builds | All Logic + UI Complete | Build Team |
| QA Testing | Platform Builds Ready | QA Team |

---

## Team Autonomy & Accountability

Each team is **autonomous** within their scope but must:
- Deliver on sprint commitments
- Communicate blockers immediately
- Document all decisions
- Follow coding standards
- Submit code for review (managing engineer sign-off)

---

**Last Updated**: Nov 14, 2025  
**Status**: Teams ready for sprint kickoff
