# Managing Engineer Briefing: Unity 6.0 Compatibility Review
## IMMEDIATE ACTION REQUIRED - All Teams

**Date**: Nov 14, 2025, 8:15 PM UTC  
**From**: Amp (Managing Engineer)  
**To**: All Development Teams  
**Priority**: 🔴 CRITICAL

---

## SITUATION REPORT

The Bump U Box project is configured correctly for **Unity 6000.2.12f1** in ProjectSettings. However, a comprehensive code audit has identified **15 production files** across 3 systems that contain deprecated, experimental, or incompatible APIs with this version.

**These issues must be resolved by Nov 17-18 EOD before Sprint 8 Phase 1 testing begins on Nov 16.**

This is a **blocking requirement**. Incompatible code will prevent testing from proceeding and will delay the release from Dec 15 to late January.

---

## KEY STATISTICS

| Metric | Count | Severity |
|--------|-------|----------|
| **Total Files Affected** | 15 | 🔴 CRITICAL |
| **Text Component (deprecated)** | 9 files | 🔴 CRITICAL |
| **Layout Groups (deprecated)** | 2 files | 🔴 CRITICAL |
| **Old Input System** | 1 file | 🔴 CRITICAL |
| **LeanTween (verify)** | 2 files | 🔴 CRITICAL |
| **API Verification** | 3 files | 🟡 MEDIUM |
| **Estimated Work** | 26-32 hours | Can parallelize |
| **Deadline** | Nov 17-18 EOD | Non-negotiable |

---

## WHAT'S HAPPENING

Unity 6.0 (major version) introduced several breaking changes:

1. **Text UI Component Deprecated** → Must use TextMeshPro
2. **Layout Groups Removed** → Must use new LayoutGroup system or manual positioning
3. **Old Input System Unreliable** → Must use New InputSystem
4. **Third-party Libraries** → LeanTween compatibility must be verified
5. **Deprecated APIs** → Profiler, Font loading, Handheld haptics

Since the project is configured for Unity 6.0, **we are already committed to this version**. Code must align.

---

## TEAM ASSIGNMENTS

### UI Engineer - 9 Files (14 hours estimated)
**Critical Migrations** (all use deprecated `Text` component):
1. ScoreboardController.cs (2-3h) - Text + Layout + Font
2. PauseMenuController.cs (2-3h) - Text + Layout + Alignment
3. HUDManager.cs (1-2h) - Text references
4. DiceRollButton.cs (1h) - Text
5. BumpButton.cs (1h) - Text
6. DeclareWinButton.cs (1h) - Text
7. ActionButtonController.cs (1h) - Text
8. ModalController.cs (2h) - Text + LeanTween verification
9. NotificationController.cs (1h) - Text

**Deadline**: Nov 17 EOD (CRITICAL)

---

### Board Engineer - 3 Files (10 hours estimated)
**Critical Migrations**:
1. BoardInputHandler.cs (2-3h) - Old Input System → New InputSystem
2. CellView.cs (3-4h) - LeanTween replacement + Text
3. ChipVisualizer.cs (3-4h) - LeanTween replacement

**Deadline**: Nov 17 EOD (CRITICAL)

---

### Build Engineer - 3 Files (2 hours estimated)
**Verification Tasks**:
1. PerformanceProfiler.cs (1h) - Verify Profiler APIs
2. iOSBuildConfig.cs (30m) - Update bitcode comment
3. AndroidBuildConfig.cs (30m) - Verify Graphics API

**Deadline**: Nov 18 EOD (CAN OVERLAP with Phase 1 testing)

---

## REFERENCE DOCUMENTS

**Read these immediately** (in order):

1. **ME_UNITY6_COMPATIBILITY_DIRECTIVE.md** (Root)
   - Managing Engineer binding directive
   - Assignments and requirements
   - Escalation path

2. **_docs/STANDARDS/UNITY_6_COMPATIBILITY_AUDIT.md** (Detailed)
   - Comprehensive analysis of all 15 files
   - Migration solutions with code examples
   - Unity 6 breaking changes explained
   - Testing verification checklist

3. **_docs/STANDARDS/UNITY_6_COMPATIBILITY_FIXES.md** (Tracking)
   - Real-time progress log
   - Template for documenting changes
   - Sign-off checklist

---

## CRITICAL MIGRATION PATTERNS

### Pattern 1: Text → TextMeshPro
```csharp
// BEFORE (DEPRECATED)
using UnityEngine.UI;
private Text myText;
myText.text = "Hello";
myText.fontSize = 14;

// AFTER (REQUIRED)
using TMPro;
private TextMeshProUGUI myText;
myText.text = "Hello";
myText.fontSize = 14;
```

### Pattern 2: Old Input → New InputSystem
```csharp
// BEFORE (UNRELIABLE)
if (Input.GetKeyDown(KeyCode.Space)) { }

// AFTER (REQUIRED)
using UnityEngine.InputSystem;
private InputAction spaceAction;
if (spaceAction.triggered) { }
```

### Pattern 3: LeanTween → Coroutine (if incompatible)
```csharp
// BEFORE (IF INCOMPATIBLE)
LeanTween.alphaCanvas(canvasGroup, 0.5f, 0.3f);

// AFTER (SAFE FALLBACK)
StartCoroutine(FadeAlpha(canvasGroup, 0.5f, 0.3f));
private IEnumerator FadeAlpha(CanvasGroup cg, float target, float duration)
{
    float elapsed = 0, start = cg.alpha;
    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        cg.alpha = Mathf.Lerp(start, target, elapsed / duration);
        yield return null;
    }
    cg.alpha = target;
}
```

---

## GIT COMMIT PROTOCOL

**Each change must be committed atomically** with a descriptive message:

```bash
[Sprint 8] Unity 6.0 compatibility: <FileName> - <WhatChanged>
```

**Examples**:
- `[Sprint 8] Unity 6.0 compatibility: ScoreboardController - Text + Layout migration`
- `[Sprint 8] Unity 6.0 compatibility: BoardInputHandler - InputSystem migration`
- `[Sprint 8] Unity 6.0 compatibility: CellView - LeanTween replacement + Text migration`

**Commit body** (required):
- List all changes made
- Explain migration rationale
- Include before/after code if complex
- Note any compatibility decisions (e.g., why coroutine over LeanTween)

---

## TESTING REQUIREMENTS

After each file migration:

✅ **Compiles** - No syntax errors  
✅ **Runtime** - No null references or exceptions  
✅ **Visual** - UI renders correctly  
✅ **Functional** - Feature works as before  
✅ **Unit Tests** - All existing tests pass  
✅ **No Regressions** - From Sprint 1-7 code  

**Document in**: _docs/STANDARDS/UNITY_6_COMPATIBILITY_FIXES.md

---

## TIMELINE

| Date | Milestone | Owner |
|------|-----------|-------|
| **Nov 14** | Audit completed, directive issued | Managing Engineer |
| **Nov 15** | Teams acknowledge, prepare environment | All Teams |
| **Nov 16** | UI + Board begin work; QA testing blocked | UI, Board, QA |
| **Nov 17** | UI + Board complete critical migrations | UI, Board |
| **Nov 18** | Build engineer completes verification | Build |
| **Nov 19** | Final testing and gate release | Managing Engineer |
| **Nov 20** | Phase 1 testing proceeds | QA |

**Critical**: Code must be ready by morning Nov 16 UTC for Phase 1 testing to proceed.

---

## ESCALATION

**"I have a question about..."** → Contact

| Issue | Contact | Response Time |
|-------|---------|---|
| Deprecated API not clear | Managing Engineer | 30 min |
| TextMeshPro migration | UI Engineer Lead | 1 hour |
| Input System migration | Board Engineer Lead | 1 hour |
| LeanTween compatibility | Board Engineer Lead + Managing Engineer | 2 hours |
| Build/Graphics config | Build Engineer | 1 hour |
| **BLOCKER** | Managing Engineer #blockers Slack | URGENT |

---

## SUCCESS CRITERIA

✅ **All 15 files** migrated and tested  
✅ **Zero compilation errors** in migrated code  
✅ **100% test pass rate** (existing unit tests)  
✅ **No regressions** from Sprints 1-7  
✅ **All commits** atomic and descriptive  
✅ **Tracking log** updated (_docs/STANDARDS/UNITY_6_COMPATIBILITY_FIXES.md)  
✅ **Ready for Phase 1 testing** by Nov 16 morning

---

## IF SOMETHING BLOCKS YOU

**Do NOT wait.** Report immediately:

1. **Slack**: #blockers channel
2. **Direct**: Managing Engineer (Amp)
3. **Subject**: "[BLOCKER] <file> - <issue>"

Example: `[BLOCKER] LeanTween compatibility - Cannot find Unity 6 package version`

**Response**: < 1 hour for blocking issues

---

## AUTHORITY & EXPECTATIONS

This is a **binding directive from the Managing Engineer**.

- ✅ **Acknowledged by**: All team leads by Nov 15, 8:00 AM UTC
- ✅ **Completed by**: UI + Board Nov 17 EOD, Build Nov 18 EOD
- ✅ **Tested by**: All teams
- ✅ **Committed by**: All teams with proper git messages

**Failure to comply** extends release timeline by 2+ weeks.

---

## NEXT ACTIONS (NOW)

1. ✅ **Read**: ME_UNITY6_COMPATIBILITY_DIRECTIVE.md (5 minutes)
2. ✅ **Read**: _docs/STANDARDS/UNITY_6_COMPATIBILITY_AUDIT.md (20 minutes)
3. ✅ **Understand**: Your assigned files
4. ✅ **Acknowledge**: Reply to directive (Nov 15, 8:00 AM UTC)
5. ✅ **Prepare**: Environment, test plan
6. ✅ **Start Work**: Nov 16, 8:00 AM UTC

---

## CONTACT

**Managing Engineer**: Amp  
**Questions**: Direct message or #blockers Slack  
**Documentation**: Root and _docs/STANDARDS/ directories  

**Documents Issued**:
- ME_UNITY6_COMPATIBILITY_DIRECTIVE.md (binding directive)
- _docs/STANDARDS/UNITY_6_COMPATIBILITY_AUDIT.md (detailed analysis)
- _docs/STANDARDS/UNITY_6_COMPATIBILITY_FIXES.md (tracking log)

---

**Briefing Status**: ACTIVE  
**Authority**: Managing Engineer (Amp)  
**Scope**: All development teams  
**Compliance**: Mandatory  
**Date**: Nov 14, 2025, 8:15 PM UTC
