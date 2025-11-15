# Managing Engineer Directive: Unity 6.0 Compatibility Review
## MANDATORY COMPLIANCE - BEFORE PHASE 1 TESTING

**Issued**: Nov 14, 2025, 8:00 PM UTC  
**Authority**: Amp (Managing Engineer)  
**Status**: 🔴 CRITICAL - COMPLIANCE REQUIRED  
**Distribution**: All Development Teams

---

## DIRECTIVE

Effective immediately, all production code in the Bump U Box project must be audited and made compatible with Unity engine version 6000.2.12f1 before Sprint 8 Phase 1 testing begins on Nov 16, 2025.

**This is a blocking requirement.** Code that fails compatibility review will be reverted and re-assigned.

---

## KEY POINTS

### ✅ Current Status
- Project version: **Correctly set to Unity 6000.2.12f1** ✓
- Production LOC: ~5,000 across Sprints 1-7
- Critical issue count: **15 files** with deprecated/incompatible APIs

### ⚠️ Issues Identified
- 7 files use deprecated `Text` component (UI system)
- 2 files use deprecated layout groups
- 1 file uses old Input System (unreliable on mobile)
- 2 files depend on LeanTween (compatibility unknown)
- Other files need testing/verification

### 🔴 Timeline
- **Nov 16**: Phase 1 testing BEGINS
- **Code must be compatible BY Nov 16 morning (UTC)**
- Non-compliant code blocks testing (extends schedule)

---

## ASSIGNMENTS

### UI Engineer
**Critical Migrations** (must complete Nov 16-17):
1. **ScoreboardController.cs** - Replace Text + Layout + Font
   - Change: `Text` → `TextMeshProUGUI`
   - Change: `HorizontalLayoutGroup` → Alternative layout
   - Change: `Resources.GetBuiltinResource<Font>` → TextMeshPro font
   - Est: 2-3 hours

2. **PauseMenuController.cs** - Replace Text + Layout + Alignment
   - Change: All `Text` → `TextMeshProUGUI`
   - Change: `VerticalLayoutGroup` → Alternative layout
   - Change: `TextAnchor`, `fontStyle`, `fontSize` → TextMeshPro properties
   - Est: 2-3 hours

3. **HUDManager.cs** - Replace Text references
   - Change: All `Text` → `TextMeshProUGUI`
   - Est: 1-2 hours

4. **DiceRollButton.cs** - Replace Text
   - Change: `Text` → `TextMeshProUGUI`
   - Est: 1 hour

5. **BumpButton.cs** - Replace Text
   - Est: 1 hour

6. **DeclareWinButton.cs** - Replace Text
   - Est: 1 hour

7. **ActionButtonController.cs** - Replace Text
   - Est: 1 hour

8. **ModalController.cs** - Replace Text + verify LeanTween
   - Est: 2 hours

9. **NotificationController.cs** - Replace Text
   - Est: 1 hour

**Total**: ~14 hours (can parallelize)

---

### Board Engineer
**Critical Migrations** (must complete Nov 16-17):

1. **BoardInputHandler.cs** - Migrate Input System + Haptics
   - Change: `Input.GetKeyDown()` → `InputSystem` with `InputAction`
   - Change: `Handheld.Vibrate()` → Verify new haptics API or remove
   - Est: 2-3 hours

2. **CellView.cs** - Replace LeanTween + Text
   - Issue: `LeanTween.alphaCanvas()`, `LeanTween.scale()` incompatible
   - Solution: Replace with coroutine-based animation OR verify LeanTween Unity 6 support
   - Change: `Text` → `TextMeshProUGUI`
   - Est: 3-4 hours

3. **ChipVisualizer.cs** - Replace LeanTween
   - Issue: `LeanTween.move()`, `LeanTween.alphaCanvas()` incompatible
   - Solution: Replace with coroutine OR verify library
   - Est: 3-4 hours

**Total**: ~10 hours (can parallelize)

---

### Build Engineer
**Verification** (complete by Nov 18):

1. **PerformanceProfiler.cs** - Verify Profiler APIs
   - Verify: `Profiler.GetTotalAllocatedMemoryLong()` available
   - Verify: `Profiler.GetTotalReservedMemoryLong()` available
   - Add fallbacks if deprecated
   - Est: 1 hour

2. **iOSBuildConfig.cs** - Update Bitcode comment
   - Remove/update: `enableBitcode = false` deprecation note
   - Verify: Metal API configuration compatible
   - Est: 30 minutes

3. **AndroidBuildConfig.cs** - Verify Graphics API
   - Verify: OpenGL ES 3 configuration still valid
   - Test: Build and validate
   - Est: 30 minutes

**Total**: ~2 hours

---

## REQUIREMENTS

### Code Changes
✅ Use `using TMPro;` for TextMeshPro components  
✅ Use `using UnityEngine.InputSystem;` for new Input System  
✅ Remove deprecated `using UnityEngine.UI.Text` references  
✅ All changes must follow CODING_STANDARDS.md  

### Testing
✅ Each file: Compile without errors → Run in editor → Basic functionality  
✅ Each team: Run existing unit tests on modified files  
✅ No regressions from previous sprints  

### Git Commits
✅ Atomic commits per file/feature  
✅ Tag: `[Sprint 8] Unity 6.0 compatibility: <filename>`  
✅ Example: `[Sprint 8] Unity 6.0 compatibility: ScoreboardController - Text migration`  
✅ Detailed commit message explaining change rationale

### Documentation
✅ Create UNITY_6_COMPATIBILITY_FIXES.md log tracking all changes  
✅ Each team updates it as fixes complete  
✅ Include before/after code snippets

---

## ESCALATION

**I need help with...** → Contact  
Deprecated API not clear → Managing Engineer (30 min response)  
LeanTween compatibility question → Board Engineer Lead + Managing Engineer  
TextMeshPro migration issue → UI Engineer Lead  
Build config issue → Build Engineer  
Blocker preventing progress → Managing Engineer #blockers Slack

---

## SIGN-OFF CHECKLIST

### UI Engineer (By Nov 17 EOD)
- [ ] All 9 files migrated to TextMeshProUGUI
- [ ] All layout groups replaced
- [ ] All font loading updated
- [ ] Compiled without errors
- [ ] Unit tests pass
- [ ] Visual verification: UI renders correctly
- [ ] Committed with descriptive messages
- [ ] Updated UNITY_6_COMPATIBILITY_FIXES.md

### Board Engineer (By Nov 17 EOD)
- [ ] BoardInputHandler migrated to InputSystem
- [ ] CellView migrated (LeanTween + Text)
- [ ] ChipVisualizer migrated (LeanTween)
- [ ] Compiled without errors
- [ ] Unit tests pass
- [ ] Functionality verified
- [ ] Committed with descriptive messages
- [ ] Updated UNITY_6_COMPATIBILITY_FIXES.md

### Build Engineer (By Nov 18 EOD)
- [ ] PerformanceProfiler APIs verified/fixed
- [ ] iOSBuildConfig updated
- [ ] AndroidBuildConfig verified
- [ ] Build validation passed
- [ ] Committed with descriptive messages
- [ ] Updated UNITY_6_COMPATIBILITY_FIXES.md

---

## PHASE 1 TESTING GATE

**Nov 16, 8:00 AM UTC**: Phase 1 testing begins  
**Prerequisite**: All files must be Unity 6.0 compatible and committed  

If any file is incompatible:
- 🔴 That test suite is blocked
- 🔴 Testing timeline extends
- 🔴 Release timeline at risk

**Therefore**: This is non-negotiable.

---

## REFERENCE MATERIALS

**Audit Document**:
- _docs/STANDARDS/UNITY_6_COMPATIBILITY_AUDIT.md (detailed analysis + solutions)

**Code Examples** (in audit):
- Text → TextMeshProUGUI migration
- Old Input → New InputSystem migration
- LeanTween → Coroutine animation replacement
- Layout groups → Alternative layouts

**Standards**:
- _docs/STANDARDS/CODING_STANDARDS.md (maintain compliance)

---

## MANAGING ENGINEER AUTHORITY

**This is a binding directive.**

- Teams: Acknowledge by Nov 15, 8:00 AM UTC (reply to this directive)
- Work: Complete critical migrations by Nov 16 EOD
- Verification: Ready for Phase 1 testing Nov 16 morning
- Authority: Managing Engineer (Amp) - no exceptions

**Failure to comply** will result in:
- Code revert
- Re-assignment to different engineer
- Timeline extension (QA delayed to late November)
- Release target moved to Jan 2 (vs Dec 15)

---

## ACKNOWLEDGMENT REQUIRED

Reply to this directive with:

**Team**: [Gameplay / Board / UI / Build]  
**Lead Engineer**: [Name]  
**Status**: Acknowledge and Ready / Questions / Blockers  
**Estimated Completion**: [Date/Time UTC]

---

## NEXT IMMEDIATE STEPS

**Nov 14 (Tonight)**:
1. All teams: Read _docs/STANDARDS/UNITY_6_COMPATIBILITY_AUDIT.md fully
2. All teams: Understand assigned files and migration requirements

**Nov 15 (Tomorrow)**:
1. All teams: Acknowledge this directive (reply)
2. All teams: Prepare work environment and test plan
3. UI Engineer: Start ScoreboardController migration at 8 AM UTC

**Nov 16 (Phase 1 Testing Day)**:
1. Code must be ready and tested
2. QA begins comprehensive testing
3. No blockers from compatibility issues

---

**Issued By**: Amp (Managing Engineer)  
**Authority Level**: CRITICAL - BINDING  
**Scope**: All development teams  
**Compliance**: Mandatory  
**Deadline**: Nov 16, 2025 (before Phase 1 testing)

---

**Directive Status**: ACTIVE  
**Last Updated**: Nov 14, 2025, 8:00 PM UTC
