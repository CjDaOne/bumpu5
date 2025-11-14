# Project Quality Gates & Code Review Framework

**Purpose**: Enforce consistent code quality across all sprints  
**Enforced By**: Managing Engineer (Amp)  
**Updated**: Nov 14, 2025

---

## Code Review Process

### Before Submission
Engineer runs self-review checklist on their own branch.

### During Submission
Engineer creates PR/submission with:
- Brief description of changes
- Files modified
- Tests added/modified
- Any design decisions

### During Review (ME - 24-hour SLA)
1. Code standards compliance check
2. Architecture review
3. Test coverage verification
4. Integration point validation
5. Performance assessment
6. Approval or request for changes

### After Approval
1. Merge to main branch
2. Add to project status documentation
3. Prepare for next sprint

---

## Code Standards Checklist

Every code submission must pass ALL of these checks:

### Naming & Convention (Mandatory)
- [ ] Classes: PascalCase (GameStateManager, Game1_Bump5)
- [ ] Methods: PascalCase verbs (RollDice, IsValidMove)
- [ ] Properties: PascalCase (CurrentPlayer, LastRoll)
- [ ] Private fields: camelCase (_currentPlayer, _lastRoll)
- [ ] Constants: UPPER_SNAKE_CASE (BOARD_SIZE = 12)
- [ ] Interfaces: I prefix (IGameMode, IBoardCell)
- [ ] Test classes: ClassNameTests (GameStateManagerTests)
- [ ] Test methods: MethodName_Condition_Result

### Documentation (Mandatory)
- [ ] All public classes have /// <summary>
- [ ] All public methods have /// <summary> + <param> + <returns>
- [ ] Complex logic has inline comments
- [ ] No TODOs without issues/tickets
- [ ] README.md updated if major changes

### Code Quality (Mandatory)
- [ ] No compiler errors
- [ ] No compiler warnings
- [ ] No #pragma warning suppression
- [ ] No magic numbers (use constants)
- [ ] Guard clauses instead of nested ifs
- [ ] No code duplication
- [ ] Methods ≤ 50 lines (preferably ≤ 30)
- [ ] Classes follow single responsibility principle
- [ ] Tight cohesion, loose coupling

### Error Handling (Mandatory)
- [ ] Null checks on entry (ArgumentNullException)
- [ ] Invalid state detection (InvalidOperationException)
- [ ] Debug.Log for info, Debug.LogWarning for concerns, Debug.LogError for failures
- [ ] No silent failures
- [ ] No infinite loops

### Testing (Mandatory)
- [ ] Unit tests for all public methods
- [ ] ≥ 80% code coverage
- [ ] Edge cases tested
- [ ] All tests pass locally
- [ ] No flaky tests (deterministic)
- [ ] Mock/stub external dependencies
- [ ] Test names clearly indicate what's being tested

### Performance (Mandatory)
- [ ] No allocations in Update/per-frame code
- [ ] No expensive calculations per frame
- [ ] Object pooling for frequently instantiated objects
- [ ] No unnecessary loops
- [ ] Algorithm complexity acceptable
- [ ] Profile results attached for critical code

### Git & Version Control (Mandatory)
- [ ] Commit message format: `[Sprint X] Description`
- [ ] Commits are atomic (one logical change per commit)
- [ ] No WIP or debug commits
- [ ] Clean git history (no merge commits on personal branches)
- [ ] All commits signed off (git commit -s)

---

## Acceptance Criteria by Sprint

### Sprint 1: Core Logic (COMPLETE ✅)
- ✅ 7 core classes: Player, Chip, BoardCell, BoardModel, DiceManager, TurnManager
- ✅ 57 unit tests with 85%+ coverage
- ✅ All tests pass
- ✅ No compiler errors/warnings
- ✅ All documentation complete
- ✅ Code review approved
- ✅ Ready for production

### Sprint 2: State Machine (IN PROGRESS)
- [ ] GameStateManager.cs implemented
- [ ] GamePhase enum defined
- [ ] 22+ integration tests (target 85%+ coverage)
- [ ] All phase transitions tested
- [ ] Event system tested
- [ ] Edge cases handled (6, 5+6, no moves, win detection)
- [ ] Code review approved
- [ ] Ready for Sprint 3 dependency

### Sprint 3: Game Modes (PLANNED)
- [ ] IGameMode interface defined
- [ ] Game1_Bump5 implemented & tested
- [ ] Game2_Krazy6 implemented & tested
- [ ] Game3_PassTheChip implemented & tested
- [ ] Game4_BumpUAnd5 implemented & tested
- [ ] Game5_Solitary implemented & tested
- [ ] 40+ mode-specific tests
- [ ] GameModeFactory implemented
- [ ] Code review approved

### Sprint 4: Board Visualization (PLANNED)
- [ ] BoardGridManager implemented
- [ ] Cell prefab interactive & clickable
- [ ] Visual feedback for valid moves
- [ ] Chip placement animation
- [ ] Board scene integration
- [ ] 15+ integration tests
- [ ] Code review approved

### Sprint 5: UI Framework (PLANNED)
- [ ] HUDManager orchestrating all UI
- [ ] DiceRollButton functional
- [ ] BumpButton context-sensitive
- [ ] ScoreboardDisplay live updates
- [ ] PopupManager showing messages
- [ ] GameModeSelectionScreen working
- [ ] 15+ UI tests
- [ ] All buttons 44px+ (mobile touch target)
- [ ] Canvas responsive to game state

### Sprint 6: Integration (PLANNED)
- [ ] Main menu scene
- [ ] Mode selection → gameplay flow
- [ ] Rules/instructions screen
- [ ] Settings menu
- [ ] Complete game playable end-to-end
- [ ] All 5 modes working

### Sprint 7: Platform Builds (PLANNED)
- [ ] WebGL build compiles
- [ ] Android APK generates
- [ ] iOS Xcode project generates
- [ ] Input handlers for each platform
- [ ] Safe area on iOS
- [ ] Performance: 30+ FPS on tested devices

### Sprint 8: QA & Polish (PLANNED)
- [ ] All platforms tested
- [ ] No critical bugs
- [ ] All modes playable
- [ ] Performance optimized
- [ ] App store submissions ready

---

## Review Timeline

### Per Sprint
- **Days 1-6**: Development + self-review
- **Day 7**: Final refinement + ME code review submission
- **Within 24 hours**: ME review complete, feedback given
- **Day 8+**: Revisions if needed, final approval

### Per Submission
- **ME receives**: PR with tests, documentation, git history
- **ME checks**: All mandatory items in checklist
- **ME approves**: Code is production-ready
- **ME rejects**: Specific items to fix, clear guidance

---

## Quality Metrics Dashboard

### Real-Time Tracking

**Sprint 1** (COMPLETE)
- Classes: 7/7 ✅
- Tests: 57 ✅
- Coverage: 85%+ ✅
- Standards: 100% ✅

**Sprint 2** (ACTIVE)
- GameStateManager: In Progress
- Tests: [% Complete]
- Coverage: [Target 85%]
- Standards: [% Compliance]

---

## Design Review Checklist

For architectural decisions, also verify:

- [ ] Follows ARCHITECTURE.md principles
- [ ] Tight coupling minimized
- [ ] Interfaces used appropriately
- [ ] Single responsibility maintained
- [ ] Extensible for future game modes
- [ ] Dependency injection where applicable
- [ ] No circular dependencies
- [ ] Performance implications considered
- [ ] Memory footprint reasonable
- [ ] Thread-safe if concurrent (not applicable for single-threaded game)

---

## Performance Benchmarks

### Target Metrics
| Component | Metric | Target | Status |
|-----------|--------|--------|--------|
| Game Logic | Dice Roll | < 1ms | Pending |
| Game Logic | Move Validation | < 1ms | Pending |
| Game Logic | Win Detection | < 5ms | Pending |
| UI | Button Click Response | < 100ms | Pending |
| Board | Cell Rendering | < 5ms | Pending |
| Overall | Frame Rate (Modern) | 60 FPS | Pending |
| Overall | Frame Rate (Mobile) | 30 FPS | Pending |

---

## Escalation Process

### Critical Issues
**Definition**: Compilation errors, logic deadlocks, security issues

**Process**:
1. Flag immediately (don't wait for next standup)
2. ME response: 2 hours (weekday)
3. Action: Code review halt, debug session, resolution path

### Major Issues
**Definition**: Test failures, design flaws, performance problems

**Process**:
1. Report in daily standup
2. ME response: 24 hours
3. Action: Design discussion, potential rework

### Minor Issues
**Definition**: Style inconsistencies, documentation gaps, refactoring suggestions

**Process**:
1. Include in code review
2. Address in next revision
3. No blocker to approval

---

## Approval & Sign-Off

### Who Can Approve
- **ME (Amp)**: All code, architecture, final approval
- **Code Owner (Sprint Engineer)**: Cannot self-approve

### What "Approved" Means
- Code meets all mandatory checks
- Tests pass at 80%+ coverage
- Design is sound and extensible
- Production-ready
- Ready to merge to main branch

### Post-Approval
- Code merged to main by ME
- Commit tagged with sprint number
- Status updated in PROJECT_STATUS_REPORT.md
- Next sprint unblocked if dependent

---

## Anti-Patterns to Avoid

### Code Smells
❌ God objects (classes doing too much)  
❌ Magic numbers throughout code  
❌ Comments explaining bad code instead of fixing it  
❌ Deep nesting (5+ levels)  
❌ Silently swallowed exceptions  
❌ Hard-coded values  
❌ Duplicate code across files  

### Design Issues
❌ Tight coupling between systems  
❌ No interfaces for extensibility  
❌ Missing null checks  
❌ Synchronous blocking calls  
❌ Inconsistent error handling  

### Testing Issues
❌ Tests with side effects  
❌ Flaky/non-deterministic tests  
❌ Test coverage < 80%  
❌ No edge case testing  
❌ Tests that test the wrong thing  

---

## Continuous Improvement

### Weekly Review (Fridays)
- Review code quality metrics
- Identify common issues
- Adjust standards if needed
- Celebrate successes

### Post-Project Review
- Metrics summary
- Lessons learned
- Best practices for next project
- Tool improvements

---

## Tools & Resources

- **IDE**: Visual Studio / Rider with C# analyzer
- **Version Control**: Git + GitHub
- **Testing**: NUnit (already installed)
- **Code Analyzer**: Built-in .NET analyzer
- **Documentation**: XML comments (C# standard)
- **Performance**: Unity Profiler

---

**Last Updated**: Nov 14, 2025  
**Enforced**: Yes  
**Non-negotiable**: Yes  
**Questions**: Ask ME

