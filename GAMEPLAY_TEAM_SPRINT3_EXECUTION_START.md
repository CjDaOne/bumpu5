# GAMEPLAY TEAM - SPRINT 3 EXECUTION START
## Game Modes Implementation - Immediate Execution Order

**STATUS**: 🔴 **EXECUTE NOW**  
**Date Issued**: Nov 14, 2025  
**Authority**: Managing Engineer (Amp)  
**Duration**: 7 days (Nov 14-21, 2025)  
**Target**: All 5 game modes complete, tested, code reviewed

---

## MISSION

Implement 5 complete, production-ready game modes for Bump U Box with 40+ comprehensive unit tests and 95%+ documentation.

---

## DELIVERABLES (7-DAY SPRINT)

### Phase 1: Foundation (Days 1-2: Nov 14-15)
**Objective**: Core interface + Game 1

```
- [ ] IGameMode.cs interface
      * LoadGameConfig(GameConfig config)
      * ValidateMoveExists(Player player, Dice dice) → bool
      * GetValidMoves(Player player, Dice dice) → List<BoardCell>
      * ValidateWin(Player player) → bool
      * OnBumpOccurred(Player bumper, Player bumped)
      * GetWinConditionText() → string
      
- [ ] Game1_Bump5.cs - BUMP5 GAME MODE
      * Win: First to reach position 5 wins
      * Rules: 
        - Players can bump each other
        - All bumps must be valid (adjacent cells, same column)
        - Bumped player returns to start
        - Normal turn sequence applies
      * Validation: GetValidMoves() returns all adjacent valid cells
      * Edge cases: Multiple bumps same turn, self-bump prevention
      
- [ ] Unit tests: Game1Tests.cs (8-10 tests)
      * Test win condition (position 5)
      * Test valid move detection
      * Test invalid moves rejected
      * Test bump mechanics
      * Test edge cases
      
- [ ] Code: Production-ready, 95%+ documented
```

**Commit by**: End of Nov 15

---

### Phase 2: Games 2-3 (Days 3-4: Nov 16-17)
**Objective**: Game 2 + Game 3 complete

```
- [ ] Game2_Krazy6.cs - KRAZY6 GAME MODE
      * Win: First to reach position 6 wins
      * Rules:
        - Higher winning position than Bump5
        - Same bump mechanics as Bump5
        - First player to 6 wins
      * Validation: Similar to Game1 but position 6
      * Tests: Game2Tests.cs (8-10 tests)

- [ ] Game3_PassTheChip.cs - PASS THE CHIP GAME MODE
      * Win: Players pass a "hot chip" around board
      * Rules:
        - One player starts with the chip
        - Can pass chip to any valid cell opponent
        - Holding chip too long = penalty
        - Last player without chip loses
      * Validation: GetValidMoves() includes chip passing moves
      * Tests: Game3Tests.cs (8-10 tests)
      * Edge cases: Chip passing logic, penalty timing

- [ ] Code: Production-ready, 95%+ documented
```

**Commit by**: End of Nov 17

---

### Phase 3: Games 4-5 (Days 5-6: Nov 18-19)
**Objective**: Game 4 + Game 5 + Factory

```
- [ ] Game4_BumpUAnd5.cs - BUMPU&5 GAME MODE
      * Win: Special rules combining bump mechanics
      * Rules:
        - Two winning positions: 5 AND position 7
        - Player must hit both to win
        - Complex scoring system
      * Validation: Multi-stage win condition
      * Tests: Game4Tests.cs (8-10 tests)

- [ ] Game5_Solitary.cs - SOLITARY GAME MODE
      * Win: Single player race to position 5
      * Rules:
        - No opponents
        - Pure movement challenge
        - Dice rolls still apply
        - Win = reach position 5
      * Validation: Solo win validation
      * Tests: Game5Tests.cs (8-10 tests)

- [ ] GameModeFactory.cs
      * CreateGameMode(GameModeType type) → IGameMode
      * Static factory methods for easy mode selection
      * Handles all 5 game modes
      
- [ ] Code: Production-ready, 95%+ documented
```

**Commit by**: End of Nov 19

---

### Phase 4: Integration & Testing (Days 6-7: Nov 20-21)
**Objective**: All tests passing, code review ready

```
- [ ] GameModeIntegrationTests.cs (comprehensive suite)
      * All 5 modes functional end-to-end
      * State machine integration
      * Turn manager compatibility
      * Player/chip interaction
      
- [ ] Edge case testing across all modes
      * Invalid move rejection
      * Win condition edge cases
      * Multiple player interactions
      * State consistency
      
- [ ] Code cleanup & documentation
      * All methods 100% documented
      * Consistent naming
      * Architecture review
      * Standards compliance check
      
- [ ] Performance profiling
      * Valid move calculation speed
      * State validation performance
      * Memory usage baseline
      
- [ ] Code review preparation
      * All tests passing (100%)
      * All files commit-ready
      * Documentation complete
```

**Commit by**: End of Nov 20  
**Code Review**: Nov 21

---

## TECHNICAL REQUIREMENTS

### Interface Definition (REQUIRED)
```csharp
public interface IGameMode
{
    // Configuration
    void LoadGameConfig(GameConfig config);
    
    // Validation
    bool ValidateMoveExists(Player player, Dice dice);
    List<BoardCell> GetValidMoves(Player player, Dice dice);
    bool ValidateWin(Player player);
    
    // Game events
    void OnBumpOccurred(Player bumper, Player bumped);
    
    // UI/Display
    string GetWinConditionText();
    GameModeType GetGameModeType();
}
```

### File Structure
```
Assets/Scripts/Gameplay/
├── GameModes/
│   ├── IGameMode.cs
│   ├── Game1_Bump5.cs
│   ├── Game2_Krazy6.cs
│   ├── Game3_PassTheChip.cs
│   ├── Game4_BumpUAnd5.cs
│   ├── Game5_Solitary.cs
│   ├── GameModeFactory.cs
│   └── GameModeType.cs (enum)
├── Tests/
│   ├── Game1Tests.cs
│   ├── Game2Tests.cs
│   ├── Game3Tests.cs
│   ├── Game4Tests.cs
│   ├── Game5Tests.cs
│   └── GameModeIntegrationTests.cs
```

### Quality Gates (NON-NEGOTIABLE)

| Gate | Requirement | Your Status |
|------|-------------|------------|
| Unit Tests | 40+ tests, 100% pass | [ ] Submit |
| Code Coverage | 85%+ coverage | [ ] Verify |
| Documentation | 95%+ public methods | [ ] Audit |
| Performance | GetValidMoves < 1ms | [ ] Profile |
| Standards | CODING_STANDARDS.md compliance | [ ] Review |

---

## DAILY CHECKLIST

### Day 1 (Nov 14)
- [ ] Read this document thoroughly
- [ ] Review IGameMode interface definition
- [ ] Create Game1_Bump5.cs skeleton
- [ ] Setup Game1Tests.cs
- [ ] First commit: IGameMode interface + Game1 skeleton
- [ ] Daily standup: 9 AM UTC

### Days 2-6 (Nov 15-19)
- [ ] Each day: Implement 1 complete game mode
- [ ] Each day: Write 8-10 unit tests for that mode
- [ ] Each day: Run all tests - must be 100% passing
- [ ] Each day: Code review of your own work
- [ ] Each day: Daily standup (9 AM UTC)
- [ ] Each day: Commit before standup

### Day 7 (Nov 20-21)
- [ ] Integration testing across all modes
- [ ] Code cleanup & documentation audit
- [ ] Final test suite validation
- [ ] Prepare for managing engineer code review
- [ ] Final commit by Nov 20
- [ ] Code review meeting: Nov 21

---

## DEPENDENCIES PROVIDED

✅ Sprint 2 GameStateManager - COMPLETE  
✅ Sprint 1 BoardModel, Player, Chip - COMPLETE  
✅ CODING_STANDARDS.md - Available  
✅ SPRINT_3_DETAILED_BRIEFING.md - Full specs (800+ lines)  

**Reference Files**:
- /Assets/Scripts/Gameplay/BoardModel.cs
- /Assets/Scripts/Gameplay/Player.cs
- /Assets/Scripts/Gameplay/Chip.cs
- /Assets/Scripts/Gameplay/GameStateManager.cs
- /Assets/Scripts/Tests/BoardModelTests.cs (test patterns)

---

## TESTING STRATEGY

### Unit Tests (40+)
```
Game1_Bump5: 10 tests
  - Win at position 5 ✓
  - Valid moves adjacent cells
  - Bump mechanics work
  - Edge cases (duplicate bumps, etc.)

Game2_Krazy6: 10 tests
  - Win at position 6
  - Similar to Game1 tests
  - Position-specific validation

Game3_PassTheChip: 10 tests
  - Chip passing mechanics
  - Penalty timing
  - Valid moves include pass options
  - Hot chip tracking

Game4_BumpUAnd5: 5 tests
  - Multi-stage win condition
  - Both positions required
  - Scoring complexity

Game5_Solitary: 5 tests
  - Solo mode validation
  - No opponent interaction
  - Simple win condition

Integration: 5+ tests
  - All modes work with GameStateManager
  - Turn manager compatibility
  - State consistency
```

### Test Coverage Target
- Line coverage: 85%+
- Branch coverage: 80%+
- All game rules exercised
- All edge cases covered

---

## CODE REVIEW CHECKLIST

**Before submitting for code review, verify**:

- [ ] All classes follow CODING_STANDARDS.md
- [ ] All public methods have /// documentation
- [ ] All class definitions have /// documentation
- [ ] All unit tests passing (100%)
- [ ] Code compiles with zero warnings
- [ ] No debug Console.WriteLine left in code
- [ ] Consistent naming convention (PascalCase classes, camelCase variables)
- [ ] No hardcoded values (use constants)
- [ ] DRY principle applied (no duplicate code)
- [ ] SOLID principles followed
- [ ] Performance profiled (critical functions)
- [ ] Edge cases handled
- [ ] Error conditions documented

---

## BLOCKERS & ESCALATION

**If you encounter a blocker**:
1. Post in #blockers Slack channel
2. @Amp managing engineer for escalation
3. ME response target: < 1 hour
4. Continue on other tasks while waiting

**Common Blockers**:
- Unclear game mode rules → Reference SPRINT_3_DETAILED_BRIEFING.md
- Integration issues → Coordinate in daily standup
- Architecture questions → Ask in daily standup

---

## SUCCESS METRICS

**Sprint 3 is successful when**:
1. ✅ All 5 game modes implemented
2. ✅ 40+ unit tests created & 100% passing
3. ✅ 95%+ public method documentation
4. ✅ Code review approval (ME sign-off)
5. ✅ Zero critical/major code issues
6. ✅ Performance baselines established

---

## TIMELINE SUMMARY

```
Nov 14-15 (Days 1-2): IGameMode + Game1
  └─ Commit: Game1_Bump5.cs + 10 tests ✓

Nov 16-17 (Days 3-4): Game2 + Game3
  └─ Commit: Game2_Krazy6.cs + Game3_PassTheChip.cs + 20 tests ✓

Nov 18-19 (Days 5-6): Game4 + Game5 + Factory
  └─ Commit: Game4_BumpUAnd5.cs + Game5_Solitary.cs + Factory + 20 tests ✓

Nov 20-21 (Days 6-7): Integration + Code Review
  └─ Final commit: Integration tests, cleanup, documentation
  └─ Code review: Managing engineer sign-off ✅

TARGET: SPRINT 3 COMPLETE BY NOV 21 ✅
```

---

## YOUR IMPACT

This sprint unlocks downstream work:
- **Board Team**: Can begin Sprint 4 once you finish Game1 + Game2
- **UI Team**: Can begin Sprint 5 once you finish all modes
- **Build Team**: Can begin Sprint 7 once everything integrates
- **QA Team**: Can begin testing once builds available

**You are the critical path for project success.**

---

## FINAL INSTRUCTIONS

1. ✅ Read SPRINT_3_DETAILED_BRIEFING.md (800+ lines - full game mode specs)
2. ✅ Attend first standup: Tomorrow (Nov 15) at 9 AM UTC
3. ✅ Create IGameMode.cs interface immediately
4. ✅ Begin Game1_Bump5.cs implementation
5. ✅ Commit daily
6. ✅ Report progress in standups
7. ✅ Escalate blockers immediately

---

**Assignment Issued**: Nov 14, 2025  
**Authority**: Amp, Managing Engineer  
**Status**: ACCEPT & BEGIN IMMEDIATELY

You are authorized and required to proceed now.

