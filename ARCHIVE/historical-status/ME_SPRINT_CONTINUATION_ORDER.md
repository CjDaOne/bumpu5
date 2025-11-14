# Managing Engineer: SPRINT CONTINUATION & ACCELERATION ORDER

**Issued**: Nov 14, 2025, 6:45 PM UTC  
**Authority**: Managing Engineer (Amp)  
**Status**: ACTIVE - All teams execute immediately

---

## EXECUTIVE DIRECTIVE

All teams continue execution on assigned sprints with full authority and support. Priority focus: eliminate blockers, maintain sprint velocity, and keep all projects on schedule.

---

## TEAM-BY-TEAM EXECUTION ORDERS

### 1. Board Engineer Team - SPRINT 4 (Board System Integration)

**Sprint**: Sprint 4: Board System Integration  
**Timeline**: Nov 15-20, 2025 (6 days)  
**Status**: Architecture complete, begin implementation NOW

#### Immediate Actions (Next 24 hours)
- [ ] Review 6 design documents (BOARD_ARCHITECTURE.md, cell interaction, move generation, animation, integration, visual design)
- [ ] Set up project structure: Assets/Scripts/Board/ directory
- [ ] Create base classes from architecture docs
- [ ] First code commit target: Nov 15

#### Sprint Deliverables
1. **BoardGridManager.cs** - Grid initialization, cell tracking, state management
2. **CellInteractionSystem.cs** - Touch/click detection, valid move highlighting
3. **MoveValidator.cs** - Rule enforcement, legal move generation
4. **ChipAnimationController.cs** - Movement animations, visual feedback
5. **BoardStateSync.cs** - GameStateManager integration
6. **BoardPrefabFactory.cs** - Prefab instantiation, configuration
7. **15+ Unit Tests** - Full coverage of all classes

#### Blockers Resolution
- Design docs available in `_docs/ARCHITECTURE/`
- GameStateManager API documented and ready
- UI team will integrate later; your code should be independent

#### Success Criteria
- [ ] All 6 classes implemented and tested by Nov 20
- [ ] 15+ unit tests 100% passing
- [ ] Code review ready by Nov 20
- [ ] Zero blockers reported in daily standup

---

### 2. UI Engineer Team - SPRINT 5 (UI Framework & HUD)

**Sprint**: Sprint 5: UI Framework & HUD  
**Timeline**: Nov 20-Dec 5, 2025 (10 days) - Design complete, impl starts Nov 20  
**Status**: Architecture complete, implementation starts in 6 days

#### Immediate Actions (Next 6 days - Nov 14-20)
- [ ] Review 6 design documents (HUD architecture, popup system, screens, responsive layout)
- [ ] Prepare project structure: Assets/Scripts/UI/
- [ ] Study Board system implementation (Sprint 4 - review when available Nov 20)
- [ ] Prepare Canvas/UI hierarchy based on design specs
- [ ] Design review meeting with managing engineer (Nov 19)

#### Transition Date: Nov 20
Once Board system is code-reviewed and merged, begin implementation of:

1. **HUDManager.cs** - Core coordinator, lifecycle management
2. **PopupManager.cs** - Modal dialogs, notifications, confirmations
3. **MainMenuScreen.cs** - Game launch, mode selection, settings access
4. **GameModeSelectionScreen.cs** - Mode picking, rules display
5. **SettingsScreen.cs** - Game settings, audio, display options
6. **RulesScreen.cs** - Mode-specific rules display
7. **ResponsiveLayoutManager.cs** - Multi-screen adaptation
8. **20+ Integration Tests** - UI + game logic integration

#### Dependencies
- Sprint 3 game modes finalized (AVAILABLE)
- Sprint 4 board system code (AVAILABLE Nov 20)
- Game mode rules documentation (AVAILABLE)

#### Success Criteria
- [ ] All 7 classes implemented and tested by Dec 1
- [ ] 20+ tests 100% passing
- [ ] Responsive on all target platforms
- [ ] Code review ready by Dec 1
- [ ] Integration tests with Board system passing

---

### 3. Integration Engineer Team - SPRINT 6 (Multi-Mode Integration & Full Game Loop)

**Sprint**: Sprint 6: Multi-Mode Integration & Full Game Loop  
**Timeline**: Nov 21-Dec 5, 2025 (10 days)  
**Status**: Prep phase NOW, execution starts Nov 21

#### Immediate Actions (Nov 14-20 - Preparation Phase)
- [ ] Review GameStateManager implementation (Sprint 2 - already done)
- [ ] Document all 5 game mode interfaces (from Sprint 3)
- [ ] Study Board system design (Nov 20)
- [ ] Study UI system design (Nov 20)
- [ ] Prepare integration test framework setup

#### Execution Phase (Nov 21+)
Implement multi-mode orchestration:

1. **GameFlowManager.cs** - Mode initialization, state transitions, win conditions
2. **InputCoordinationSystem.cs** - Route input from UI to Board to GameState
3. **GameStateSync.cs** - Keep all systems synchronized with game state
4. **PauseResumeSystem.cs** - Game pause/resume with state preservation
5. **MenuTransitionManager.cs** - Smooth UI transitions between screens
6. **20+ Integration Tests** - Full game flow testing

#### Critical Dependency
- Sprint 4 Board system code (need by Nov 20)
- Sprint 5 UI system code (need by Dec 1)

#### Success Criteria
- [ ] Full game loop playable (start game → play mode → return to menu)
- [ ] All modes fully integrated and playable
- [ ] State consistency maintained across all subsystems
- [ ] 20+ integration tests 100% passing
- [ ] Code review ready by Dec 5

---

### 4. Build Engineer Team - SPRINT 7 (Platform Builds & Optimization)

**Sprint**: Sprint 7: Platform Builds & Optimization  
**Timeline**: Nov 20-Dec 10, 2025 (Prep), Dec 5-20 (Full execution)  
**Status**: Prep NOW, full execution Dec 5

#### Immediate Actions (Nov 14-20 - Preparation Phase)
- [ ] Review BUILD_PIPELINE.md and PLATFORM_REQUIREMENTS.md
- [ ] Set up build automation infrastructure (Unity Cloud Build OR local automation)
- [ ] Configure WebGL export settings (target: < 50MB, < 5 sec load)
- [ ] Configure Android build settings (target: < 100MB, Play Store ready)
- [ ] Configure iOS build settings (target: < 100MB, App Store ready)
- [ ] Create build scripts and CI/CD pipeline
- [ ] Set up performance profiling tools

#### Full Execution Phase (Dec 5+)
1. **WebGL Build Pipeline** - < 50MB, < 5 sec load time
2. **Android Build Pipeline** - Play Store requirements met
3. **iOS Build Pipeline** - App Store requirements met
4. **Performance Optimization Report** - Profiling results, recommendations
5. **Build Automation & CI/CD** - Automated builds on commit
6. **Store Submission Prep** - Asset preparation, metadata

#### Dependencies
- Complete game loop from Integration team (Dec 5)

#### Success Criteria
- [ ] All 3 platforms building successfully by Dec 10
- [ ] Load times within targets
- [ ] Store submission requirements met
- [ ] Build automation operational by Dec 10
- [ ] Performance optimization completed

---

### 5. QA Lead Team - SPRINT 8 (Comprehensive Testing & Polish)

**Sprint**: Sprint 8: QA, Playtesting & Polish  
**Timeline**: Nov 14-Dec 15, 2025 (parallel with all other sprints)  
**Status**: START NOW (test infrastructure, plans, cases)

#### Immediate Actions (Nov 14-20 - Test Preparation)
- [ ] Review TEST_PLAN_MASTER.md and REGRESSION_TESTING.md
- [ ] Set up Unity Test Framework infrastructure
- [ ] Create comprehensive test case repository
- [ ] Establish bug tracking process
- [ ] Prepare automated test scripts
- [ ] Create playtesting guides for all modes
- [ ] Prepare device testing matrix (desktop, Android, iOS)

#### Concurrent Testing (as code arrives)
- **Unit Tests**: Verify team implementations against test plans
- **Integration Tests**: Test cross-system interactions
- **Regression Tests**: Prevent re-introduction of fixed bugs
- **Playtesting**: All 5 game modes on all platforms
- **Edge Cases**: Boundary conditions, error states
- **Performance**: Load times, memory usage, frame rates

#### Key Testing Activities
1. **Week 1-2** (Nov 14-27): Board system testing (as code arrives)
2. **Week 3** (Nov 28-Dec 4): UI system testing + Board integration
3. **Week 4** (Dec 5-11): Full game loop testing
4. **Week 5** (Dec 12-18): Platform builds testing + optimization

#### Success Criteria
- [ ] Test plan infrastructure ready by Nov 20
- [ ] 100+ automated test cases prepared
- [ ] All team code 100% test pass rate
- [ ] Zero critical bugs in release build by Dec 15
- [ ] All platforms tested on target devices
- [ ] Go/No-Go decision: Dec 15

---

## CROSS-TEAM COORDINATION

### Daily Standup (All Teams)
- **Time**: 9:00 AM UTC (15 minutes)
- **Attendees**: All 5 team leads + Managing Engineer
- **Template**: ME_DAILY_STANDUP_TEMPLATE.md
- **Focus**: Progress, blockers, dependencies

### Blocker Resolution SLA
- **Critical Blockers**: < 1 hour resolution commitment
- **High Priority**: < 4 hours
- **Normal**: < 1 day
- **Escalation**: #blockers Slack channel

### Code Review SLA
- **Managing Engineer**: < 2 hours turnaround
- **Team Lead Reviews**: < 4 hours
- **Quality Gate**: CODING_STANDARDS.md compliance mandatory

### Communication Channels
- **#gameplay** - Gameplay & Integration team updates
- **#board** - Board system updates
- **#ui** - UI system updates
- **#build** - Build/platform updates
- **#qa** - Testing & QA updates
- **#blockers** - Cross-team issues (escalate here immediately)

---

## SPRINT SCHEDULE SNAPSHOT

| Sprint | Team | Status | Deadline | Notes |
|--------|------|--------|----------|-------|
| 1-3 | Gameplay | ✅ COMPLETE | Nov 14 | Game modes ready |
| 4 | Board | 🟢 EXECUTING | Nov 20 | Begin Nov 15 |
| 5 | UI | 🟢 PREP → EXEC | Dec 5 | Prep now, execute Nov 20 |
| 6 | Integration | 🟢 PREP → EXEC | Dec 5 | Prep now, execute Nov 21 |
| 7 | Build | 🟢 PREP → EXEC | Dec 20 | Prep now, execute Dec 5 |
| 8 | QA | 🟢 EXECUTING | Dec 15 | Run concurrent |

---

## RESOURCE ALLOCATION

### Managing Engineer Support
- **Code Reviews**: Daily (< 2 hours per review)
- **Blocker Resolution**: On-call
- **Architecture Questions**: Immediate response
- **Design Clarifications**: Same-day response

### Team Autonomy
Each team has full authority over:
- Implementation approach (within standards)
- Task prioritization
- Technical decisions
- Test strategies

### Escalation Path
1. Team lead attempts resolution
2. Managing Engineer support (if needed)
3. Architecture review (if design question)
4. Project-wide decision (if cross-team impact)

---

## SUCCESS METRICS (Next 21 Days)

By Dec 5, 2025:
- [ ] Board system 100% complete and tested (Sprint 4)
- [ ] UI system 100% complete and tested (Sprint 5)
- [ ] Full game loop working (Sprint 6)
- [ ] Build pipelines operational (Sprint 7)
- [ ] Comprehensive test plan executed (Sprint 8)

By Dec 15, 2025:
- [ ] All sprints 1-7 complete
- [ ] Zero critical bugs
- [ ] All platforms ready for submission
- [ ] Go/No-Go decision issued

By Jan 2, 2026:
- [ ] Release-ready game delivered
- [ ] All platforms published

---

## AUTHORITY & ACCOUNTABILITY

**Managing Engineer Authority**: Full execution, technical decision-making, resource allocation  
**Team Lead Accountability**: Deliver assigned sprint on schedule with quality gates met  
**QA Lead Accountability**: Verify quality standards maintained throughout

---

## NOTES FOR TEAMS

1. **Design Documents Available**: All architecture/design docs are in `_docs/` or referenced in sprint docs
2. **Code Examples Available**: Sprint 1-3 code provides patterns to follow
3. **Standards Compliance**: All code must follow CODING_STANDARDS.md (code review gates)
4. **Test Coverage**: Minimum 80% code coverage required for all new code
5. **Documentation**: All public methods must have XML documentation comments

---

**Status**: ACTIVE  
**Next Review**: Nov 15, 2025, 9:00 AM UTC (daily standup)  
**Issued By**: Amp (Managing Engineer)  
**Authority**: Confirmed and active
