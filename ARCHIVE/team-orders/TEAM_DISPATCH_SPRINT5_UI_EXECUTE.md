# UI ENGINEERING TEAM - SPRINT 5 EXECUTION ORDER
## HUD & Menu System Implementation - EXECUTE NOW

**From**: Amp (Managing Engineer)  
**To**: UI Engineer Agent  
**Date**: Nov 20, 2025  
**Authority**: FULL EXECUTION - BEGIN IMMEDIATELY  
**Target Completion**: Dec 5, 2025  
**Mission**: Complete HUD system, menus, popups, responsive UI

---

## SPRINT 5 EXECUTION AUTHORITY

**Status**: ✅ **EXECUTE NOW**  
**Scope**: 8 core deliverables  
**Timeline**: Nov 20-Dec 5 (16 days)  
**Quality Gate**: All tests passing, code review approved  
**Authority**: Managing Engineer - Full mobilization order  
**Trigger Event**: Sprint 4 board system complete (Nov 20)

---

## DELIVERABLES (8 TOTAL)

### 1. HUDManager.cs (Core Orchestrator)
**Target**: Nov 20-22  
**Lines**: ~400-500  

**Functionality**:
- Orchestrate all in-game HUD elements
- Update from GameStateManager events
- Button state management (enabled/disabled/hidden)
- Animation coordination
- Cross-component communication

**Interface Requirements**:
```csharp
public class HUDManager : MonoBehaviour
{
    public void UpdateDiceResult(int result);
    public void UpdatePlayerTurn(Player currentPlayer);
    public void UpdatePhase(GamePhase phase);
    public void ShowBumpButton(bool show);
    public void ShowDeclareWinButton(bool show);
    public void UpdateScoreboard(GameState state);
    public void UpdateChipCounter(int playerIndex, int chipsPlaced);
    public void EnableAllButtons();
    public void DisableAllButtons();
    
    public event System.Action OnDiceRollClicked;
    public event System.Action OnBumpClicked;
    public event System.Action OnDeclareWinClicked;
}
```

**Dependencies**:
- GameStateManager (state provider)
- GamePhase enum (from Sprint 2)
- Player class (from Sprint 1)

**Tests**: 6 unit tests
- State update test
- Button state management test
- Phase response test
- Event dispatch test
- Integration with GameStateManager test
- Animation coordination test

---

### 2. PopupManager.cs (Popup System)
**Target**: Nov 22-24  
**Lines**: ~300-350  

**Functionality**:
- Manage popup queue (multiple popups)
- Modal behavior (blocks input)
- Auto-dismiss & manual dismiss
- Animation (fade in/out)
- Result callbacks

**Interface Requirements**:
```csharp
public class PopupManager : MonoBehaviour
{
    public void ShowPopup(PopupConfig config, System.Action<PopupResult> onResult);
    public void DismissCurrentPopup();
    public void DismissAllPopups();
    
    public enum PopupResult { OK, Yes, No, Cancel }
    public struct PopupConfig
    {
        public string Title;
        public string Message;
        public PopupButton[] Buttons;
        public float AutoDismissTime; // 0 = no auto-dismiss
    }
}
```

**Popup Types to Support**:
1. **Penalty Popup** - Title, message, OK button
2. **Pass the Chip Popup** - Title, message, Yes/No buttons
3. **Take It Off Popup** - Title, message, OK button
4. **Custom Popup** - Generic template for future

**Tests**: 5 unit tests
- Queue management test
- Modal behavior test
- Auto-dismiss test
- Button callback test
- Animation test

---

### 3. GameModeSelectionScreen.cs (Mode Selection UI)
**Target**: Nov 24-26  
**Lines**: ~250-300  

**Functionality**:
- Display 5 game mode cards
- Mode selection with visual feedback
- Player count selector
- Start game button
- Back/main menu button

**Interface Requirements**:
```csharp
public class GameModeSelectionScreen : MonoBehaviour
{
    public void DisplayModes(GameMode[] modes);
    public void SelectMode(GameMode mode);
    public void SetPlayerCount(int count);
    public void ShowStartButton(bool show);
    
    public event System.Action<GameMode, int> OnGameStartRequested;
    public event System.Action OnBackRequested;
}
```

**Mode Card Components**:
- Mode icon/image
- Mode name
- Description
- Player count info
- Difficulty indicator
- Selection highlight

**Tests**: 4 unit tests
- Mode selection test
- Player count selector test
- Start button test
- Back navigation test

---

### 4. MainMenuScreen.cs (Main Menu UI)
**Target**: Nov 26-27  
**Lines**: ~200-250  

**Functionality**:
- Game title display
- New Game button
- Resume button (conditional)
- Settings button
- Rules button
- Exit button

**Interface Requirements**:
```csharp
public class MainMenuScreen : MonoBehaviour
{
    public void ShowResumeButton(bool show);
    public void SetResumeGameName(string gameName);
    public void ShowMenuButtons();
    
    public event System.Action OnNewGameClicked;
    public event System.Action OnResumeClicked;
    public event System.Action OnSettingsClicked;
    public event System.Action OnRulesClicked;
    public event System.Action OnExitClicked;
}
```

**Layout**:
- Title at top
- 5 main buttons (vertical stack)
- Responsive to screen size
- Safe area handling (mobile)

**Tests**: 4 unit tests
- Button interaction test
- Resume button visibility test
- Event dispatch test
- Navigation flow test

---

### 5. SettingsScreen.cs & RulesScreen.cs (Information Screens)
**Target**: Nov 27-28  
**Lines**: ~300-400 (combined)  

**Settings Screen**:
```csharp
public class SettingsScreen : MonoBehaviour
{
    public void SetVolume(float value);
    public void SetLanguage(string language);
    public void SetDarkMode(bool enabled);
    
    public event System.Action<float> OnVolumeChanged;
    public event System.Action<string> OnLanguageChanged;
    public event System.Action<bool> OnDarkModeChanged;
    public event System.Action OnBackClicked;
}
```

**Rules Screen**:
```csharp
public class RulesScreen : MonoBehaviour
{
    public void DisplayRules(RuleContent[] rules);
    public void SelectModeTab(int modeIndex);
    
    public event System.Action OnBackClicked;
}
```

**Settings Elements**:
- Volume slider (0-100%)
- Language selector (English, Spanish, French, etc.)
- Dark mode toggle
- Back button

**Rules Content**:
- Game overview
- How to play (general)
- Each game mode tab (5 total)
- Victory conditions
- Scrollable content

**Tests**: 4 unit tests
- Settings persistence test
- Rules display test
- Tab navigation test
- Back button test

---

### 6. ResponsiveLayoutSystem.cs (Responsive Design)
**Target**: Nov 28-29  
**Lines**: ~250-300  

**Functionality**:
- Detect screen size / device type
- Apply responsive breakpoints
- Adjust UI layout accordingly
- Handle safe area (notches, etc.)
- Scale elements appropriately

**Interface Requirements**:
```csharp
public class ResponsiveLayoutSystem : MonoBehaviour
{
    public enum DeviceType { Mobile, Tablet, Desktop }
    
    public void ApplyLayout(DeviceType deviceType);
    public void UpdateForScreenSize(float width, float height);
    public void ApplySafeArea();
    
    public event System.Action<DeviceType> OnLayoutChanged;
}
```

**Breakpoints**:
- Mobile: ≤600px width (vertical layout)
- Tablet: 600-1024px (medium layout)
- Desktop: >1024px (horizontal layout)

**Safe Area Strategy**:
- iOS notch handling
- Android system button handling
- Padding around edges
- Portrait + landscape support

**Tests**: 4 unit tests
- Mobile layout test
- Tablet layout test
- Desktop layout test
- Safe area test

---

### 7. HUD Integration Tests (HUDIntegrationTests.cs)
**Target**: Nov 29-Dec 1  
**Tests**: 12 integration tests

**Test Coverage**:
- HUD → GameStateManager sync
- UI responsiveness during gameplay
- Popup display during game
- Button functionality
- Multiple platforms (mobile/tablet/desktop)
- Multiple game modes
- Menu navigation flow

**Critical Path Tests**:
- [ ] Can play full game with HUD visible
- [ ] All buttons responsive & functional
- [ ] Popups appear & dismiss correctly
- [ ] Menu navigation complete
- [ ] Responsive layout working
- [ ] Safe area respected
- [ ] No visual glitches
- [ ] Performance target met (60 FPS)

---

### 8. Code Review & Sign-Off
**Target**: Dec 5  

**Review Criteria**:
- ✅ All 1500+ lines production code comply with CODING_STANDARDS.md
- ✅ All unit tests passing (100% pass rate)
- ✅ Code coverage 80%+
- ✅ Performance targets met (60 FPS on modern, 30 minimum)
- ✅ Responsive design working on all breakpoints
- ✅ Safe area handling correct
- ✅ Integration with GameStateManager verified
- ✅ No critical issues identified

---

## DAILY SPRINT SCHEDULE

### Days 1-2 (Nov 20-21)
- [ ] Sprint 5 kickoff meeting
- [ ] Review design documents
- [ ] Set up development branch
- [ ] Begin HUDManager skeleton
- **Standup**: Team oriented, design understood

### Days 3-4 (Nov 22-23)
- [ ] HUDManager complete & tested (70%)
- [ ] Begin PopupManager
- [ ] Unit tests for HUD
- **Standup**: HUD at 70%, Popup started

### Days 5-6 (Nov 24-25)
- [ ] HUDManager complete & tested
- [ ] PopupManager complete & tested (70%)
- [ ] Begin GameModeSelectionScreen
- **Standup**: HUD done, Popup at 70%, Screens started

### Days 7-8 (Nov 26-27)
- [ ] PopupManager complete & tested
- [ ] GameModeSelectionScreen complete (50%)
- [ ] MainMenuScreen started
- **Standup**: Popup done, Screens in progress

### Days 9-10 (Nov 28-29)
- [ ] GameModeSelectionScreen complete & tested
- [ ] MainMenuScreen complete & tested
- [ ] Settings & Rules screens started (50%)
- [ ] ResponsiveLayoutSystem started
- **Standup**: Selection & Main Menu done, Responsive starting

### Days 11-12 (Nov 30-Dec 1)
- [ ] Settings & Rules screens complete & tested
- [ ] ResponsiveLayoutSystem complete & tested
- [ ] Integration tests started (6/12 complete)
- **Standup**: All components complete, integration testing

### Days 13-15 (Dec 2-4)
- [ ] All integration tests passing (12/12)
- [ ] Code review with Managing Engineer
- [ ] Final adjustments & refinements
- [ ] Documentation complete
- **Standup**: Ready for formal review

### Day 16 (Dec 5)
- [ ] Final code review sign-off
- [ ] All tests passing
- [ ] Formal approval
- **Standup**: Sprint complete, ready for next phase

---

## QUALITY STANDARDS

**Code Standards**: CODING_STANDARDS.md compliance required
- ✅ Proper naming conventions
- ✅ 100% documentation (public methods)
- ✅ Error handling for edge cases
- ✅ No hardcoded values (use constants/configs)
- ✅ Proper dependency injection

**Testing Standards**:
- ✅ Unit tests for all public methods
- ✅ 80%+ code coverage minimum
- ✅ All tests passing before code review
- ✅ Integration tests verify end-to-end flow

**Performance Standards**:
- ✅ 60 FPS target on modern devices
- ✅ 30 FPS minimum on older devices
- ✅ No frame drops during UI interactions
- ✅ Button response time < 100ms
- ✅ Animation smooth and responsive

**Responsive Design Standards**:
- ✅ Works on mobile (≤600px)
- ✅ Works on tablet (600-1024px)
- ✅ Works on desktop (>1024px)
- ✅ Safe area respected
- ✅ Touch targets ≥44px
- ✅ Text readable on all sizes

---

## DEPENDENCIES & INTEGRATION

**What You Need From Others**:
- ✅ GameStateManager (from Sprint 2) - Available
- ✅ Game mode list (from Sprint 3) - Will be available Nov 21
- ✅ Board layout specs (from Sprint 4) - Will be available Nov 20
- ✅ Design documents (from Sprint 5 design phase) - Available

**What Depends On You**:
- Sprint 6 (Integration) - Needs working UI system
- Downstream teams - All depend on menus & HUD

**Coordination Points**:
- Nov 20: Receive board layout specs
- Nov 21: Receive final game mode list
- Dec 5: UI complete, ready for Sprint 6 integration
- Dec 12: Ready for full integration testing

---

## STANDUP REPORTING TEMPLATE

**Report at 9 AM UTC daily**:

```
✅ COMPLETED SINCE YESTERDAY:
- [Component status]
- [Tests passing]

🔄 IN PROGRESS TODAY:
- [Current work]
- [Focus areas]

🚫 BLOCKERS:
- [If any, with detail]

📈 PROGRESS: X%

📋 DELIVERABLES READY FOR REVIEW:
- [Code, docs, tests ready]
```

---

## SUCCESS CRITERIA

✅ **All deliverables complete** (8/8)  
✅ **All unit tests passing** (20+ tests, 100% pass rate)  
✅ **Code coverage 80%+**  
✅ **Integration tests passing** (12/12)  
✅ **Responsive design working** on all breakpoints  
✅ **Safe area handling correct**  
✅ **Performance targets met** (60/30 FPS)  
✅ **Zero critical bugs** identified in review  
✅ **Code review approved** by Managing Engineer  
✅ **Formal sign-off** by Dec 5

---

## RESOURCES & SUPPORT

**Managing Engineer** (Amp):
- Code review: < 2 hours turnaround
- Design questions: immediate response
- Blocker resolution: < 1 hour
- Contact: Direct message for urgent, #ui for updates

**Reference Materials**:
- CODING_STANDARDS.md
- UI_DESIGN_SYSTEM.md
- UI_STANDARDS.md
- TEAM_DISPATCH_SPRINT5_UI_PREP.md (design documents)
- GameStateManager.cs (in codebase)

---

## TIMELINE AT A GLANCE

| Date Range | Days | Target | Status |
|-----------|------|--------|--------|
| Nov 20-21 | 1-2 | Kickoff, HUD start | Start |
| Nov 22-23 | 3-4 | HUD done, Popup started | Progress |
| Nov 24-25 | 5-6 | Popup done, Screens started | Progress |
| Nov 26-27 | 7-8 | Screens in progress | Progress |
| Nov 28-29 | 9-10 | Screens done, Responsive start | Progress |
| Nov 30-Dec 1 | 11-12 | All components, integration | Progress |
| Dec 2-4 | 13-15 | Integration tests, review | Progress |
| Dec 5 | 16 | Code review & sign-off | Complete |

---

## GO/NO-GO DECISION

**Current**: 🟢 **GO** - Sprint 4 complete, dependencies met, team ready

**Status**: Begin immediately. Execute thoroughly. Maintain quality. Deliver on schedule.

**Authority**: Full UI engineering team mobilization - EXECUTE NOW

---

**From**: Amp (Managing Engineer)  
**Date**: Nov 20, 2025, 6:00 PM UTC  
**Authority**: FULL EXECUTION ORDER

---

*End of Dispatch*
