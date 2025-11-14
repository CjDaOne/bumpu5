# QA Process

**Created**: Nov 14, 2025  
**Owner**: QA Lead  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Overview

The QA Process defines how bugs are discovered, reported, triaged, fixed, and verified. It ensures quality standards are maintained throughout development and provides clear communication between QA and engineering teams.

---

## Bug Discovery & Reporting Workflow

### Phase 1: Bug Discovery

**During Testing**:
1. Execute test case from TEST_PLAN_MASTER.md
2. Observe behavior
3. Compare to expected result
4. If mismatch → Bug found

**Documentation**:
- Note exact steps to reproduce
- Take screenshot or video
- Record device/OS/version
- Record FPS/performance if relevant
- Determine reproducibility (always/sometimes/rare)

---

### Phase 2: Bug Report Creation

**Format**: Use BUG REPORT TEMPLATE

```
TITLE: [Brief, descriptive]
  Bad: "bug in game"
  Good: "Dice button becomes unclickable after bump animation"

SEVERITY: [CRITICAL/HIGH/MEDIUM/LOW]
  CRITICAL: Game unplayable, crash
  HIGH: Feature broken, blocking gameplay
  MEDIUM: UX impact, bugs progress
  LOW: Minor, polish issues

PLATFORM: [WebGL/Android/iOS or specific device]
  Example: "iPhone 13, iOS 16"

REPRODUCIBILITY: [Always/Sometimes/Rarely]
  Always: Every single time steps followed
  Sometimes: 50-70% of the time
  Rarely: 10-30% of the time

STEPS TO REPRODUCE: [Numbered list]
  1. From main menu, select Game 1
  2. Roll dice (click Dice button)
  3. Observe dice result
  4. Click on a valid cell
  5. Watch animation complete
  6. Click Dice button again
  [Bug occurs or doesn't occur at step X]

EXPECTED RESULT: [What should happen]
  Example: "Dice button should be enabled and clickable"

ACTUAL RESULT: [What actually happens]
  Example: "Dice button remains grayed out and unclickable"

EVIDENCE: [Screenshot or video link]
  Include timestamp if video

ADDITIONAL NOTES: [Context]
  Example: "May be related to animation timing. 
  Happens more often on older devices."
```

---

### Phase 3: Bug Logging

**Where**: Central bug tracker (Google Sheets or Jira)

**Columns**:
| ID | Title | Severity | Status | Assigned To | Platform | Created | Due |
|----|-------|----------|--------|-------------|----------|---------|-----|
| BUG-001 | Dice button unclickable after bump | HIGH | NEW | [Engineer] | Android | Nov 20 | Nov 21 |

**Status Workflow**:
```
NEW → ASSIGNED → IN PROGRESS → VERIFICATION → CLOSED (or REOPENED)
```

**Tracking Fields**:
- **ID**: BUG-001, BUG-002, etc.
- **Title**: Brief description
- **Severity**: CRITICAL/HIGH/MEDIUM/LOW
- **Status**: NEW, ASSIGNED, IN PROGRESS, VERIFICATION, CLOSED, REOPENED
- **Assigned To**: Engineer name or [Unassigned]
- **Platform**: WebGL, Android, iOS, or specific device
- **Created Date**: Today's date
- **Due Date**: Based on severity
- **Notes**: Additional context, links to screenshots

---

## Triage Process

### Who Triages
- **QA Lead**: Primary (reviews all bugs)
- **Managing Engineer**: Complex bugs or blockers

### When Triages
- **Daily**: Morning standup (review overnight discoveries)
- **As Discovered**: Critical bugs immediately

### Triage Decision

**For Each Bug**:

1. **Verify Reproducibility**
   - Can you reproduce it consistently?
   - If not, request more details from tester
   - Mark: Reproducible → Continue, Not Reproducible → CLOSED

2. **Assess Severity** (if needed)
   - Is severity assigned correctly?
   - Compare to SEVERITY definitions
   - Adjust if needed

3. **Assign to Team**
   - **CRITICAL** → Managing Engineer immediate fix
   - **HIGH** → Gameplay/UI Engineer (assigned, due 24h)
   - **MEDIUM** → Gameplay/UI Engineer (assigned, due 48h)
   - **LOW** → Backlog (assign after release, or skip)

4. **Set Due Date**
   - CRITICAL: Today or tomorrow
   - HIGH: Within 24 hours
   - MEDIUM: Within 48 hours
   - LOW: After release or skip

5. **Status Update**
   - Change status to ASSIGNED (if unassigned)
   - Notify assigned engineer (Slack message)

---

## Engineering Fix Workflow

### Phase 1: Assignment & Planning

**Engineer Receives**:
- Bug report with full details
- Priority/due date
- Slack notification

**Engineer Actions**:
1. Read full bug report
2. Reproduce bug locally (if possible)
3. Identify root cause
4. Plan fix
5. Update status to IN PROGRESS

---

### Phase 2: Implementation & Testing

**Engineer**:
1. Create fix (modify code)
2. Test fix locally
3. Verify fix doesn't break other features
4. Commit to git with message: `Fix BUG-001: [Title]`

**Commit Message Format**:
```
Fix BUG-001: Dice button becomes unclickable after bump

Root cause: Animation completion handler not re-enabling button.
Solution: Add OnAnimationComplete callback to re-enable Dice button.

Tested on: Pixel 5a, iPhone 13, WebGL Chrome
```

---

### Phase 3: Code Review

**Process**:
1. Engineer submits PR (pull request) to main branch
2. Managing Engineer reviews code
3. Managing Engineer approves or requests changes
4. If approved: Merge to main
5. Build updated APK/AAB/WebGL

---

### Phase 4: QA Verification

**QA receives notification**:
- PR merged
- Build updated
- Bug fix ready for testing

**QA Actions**:
1. Update bug status to VERIFICATION
2. Download/install updated build
3. Follow original reproduction steps
4. Test on same platform/device as original bug

**Possible Results**:

**VERIFIED - Bug Fixed**:
```
Status: CLOSED ✅
Notes: Dice button now clickable after bump animation.
Tested on: Pixel 5a (Android 12), iPhone 13 (iOS 16)
Date verified: Nov 21, 2025
```

**REOPENED - Bug Still Present**:
```
Status: REOPENED (back to IN PROGRESS)
Notes: Dice button still unclickable. Issue persists.
Steps verified: Same reproduction steps, same result.
Assigned to: [Original engineer] for further investigation
```

**PARTIAL - Fixed on Some Platforms**:
```
Status: REOPENED (need platform-specific fix)
Notes: Fixed on Android, but still broken on iOS.
Assigned to: [Engineer] for iOS-specific investigation
```

---

## Severity & Priority Matrix

### Mapping Severity to Priority & Due Date

| Severity | Priority | Impact | Fix By | Response Time |
|----------|----------|--------|--------|---|
| CRITICAL | P0 | Game unplayable, crash | Immediate (same day) | < 1 hour |
| HIGH | P1 | Feature broken, blocks gameplay | 24 hours | < 4 hours |
| MEDIUM | P2 | Impacts UX, not blocking | 48 hours | < 12 hours |
| LOW | P3 | Minor, polish | After release or skip | < 1 week |

### Examples

**CRITICAL** (P0):
- App crashes on startup
- Game freezes mid-gameplay
- Infinite loop (can't advance)
- Core logic broken (no valid moves exist)

**HIGH** (P1):
- Dice button doesn't work (can't roll)
- Bump logic wrong (bumps incorrect chip)
- Win condition never triggers
- Menu navigation broken
- Score doesn't update

**MEDIUM** (P2):
- Animation stutters (but completes)
- UI element misaligned
- Button feedback missing
- Text color wrong
- Font size too small
- Sound effect volume off

**LOW** (P3):
- Typo in text
- Spacing 2px off
- Animation timing slightly off (250ms vs 300ms)
- Button hover state subtle
- Footer credit missing

---

## Communication

### To Engineer (Bug Assignment)

**Slack Message**:
```
@Engineer: Bug BUG-001 assigned to you (HIGH priority)
Title: Dice button unclickable after bump animation
Due: Nov 21 (24 hours)

Details:
• Severity: HIGH
• Platform: Android (Pixel 5a, Galaxy S21)
• Reproducibility: Always
• Impact: Player can't continue after bump

Full report: [Link to bug tracker]
Attached: Screenshot showing grayed-out Dice button

Let me know if you need more details or have questions.
```

### To QA (Verification)

**Slack Message**:
```
@QA: BUG-001 fixed and ready for verification
Engineer: [Name]
Build: v1.0-beta-build-47

Tested by engineer on: Pixel 5a, iPhone 13, WebGL
Fix: Added OnAnimationComplete callback to re-enable Dice button

Please verify on all platforms and update bug status.
```

### Daily Standup Template

**Format** (15 minutes, all teams):
```
QA REPORT:
✅ Yesterday:
   - Completed 40 test cases (Game 1-2 full)
   - Found 3 bugs (1 HIGH, 2 MEDIUM)
   - Verified 2 fixes

🔄 Today:
   - Test Game 3-4 (60 scenarios)
   - Re-test BUG-001, BUG-002 (verification)
   - Device testing on Android (Pixel 5a, Galaxy S21)

🚫 Blockers:
   - Need latest build for iOS verification
   - One device (iPhone SE) not available
```

---

## Test Reporting & Metrics

### Daily Log (Spreadsheet)

```
Date | Tester | Game Mode | Platform | Test Case | Result | Notes | Bug ID
11/20 | QA-1 | Game 1 Bump | Android (Pixel) | Normal Turn | PASS | Score correct | -
11/20 | QA-1 | Game 1 Bump | Android (Pixel) | Win Declare | FAIL | Modal doesn't close | BUG-001
11/20 | QA-2 | Game 2 Krazy | WebGL | Lap Mechanics | PASS | Lap counter works | -
```

### Weekly Summary Report

```
WEEK OF: Nov 20, 2025

TESTING METRICS:
├─ Total Test Cases Executed: 120
├─ Pass Rate: 95% (114 passed, 6 failed)
├─ Bugs Found: 6
│  ├─ CRITICAL: 0
│  ├─ HIGH: 2
│  ├─ MEDIUM: 3
│  └─ LOW: 1
├─ Bugs Fixed: 4
├─ Bugs Verified: 3
└─ Bugs Remaining: 3

BREAKDOWN BY PLATFORM:
├─ WebGL: 40 tests (38 pass, 2 fail)
├─ Android: 45 tests (42 pass, 3 fail)
└─ iOS: 35 tests (34 pass, 1 fail)

BREAKDOWN BY GAME MODE:
├─ Game 1 (Bump 5): 25 tests, 1 failure
├─ Game 2 (Krazy 6): 25 tests, 1 failure
├─ Game 3 (Pass The Chip): 20 tests, 0 failures
├─ Game 4 (Bump U And 5): 25 tests, 2 failures
└─ Game 5 (Solitary): 25 tests, 2 failures

TREND:
├─ Previous week: 94% pass rate
├─ This week: 95% pass rate ✓ improving
├─ Trajectory: ON TRACK for release

SIGN-OFF STATUS:
├─ Can proceed to next phase? YES
├─ Blockers? None
├─ Confidence level? HIGH

RECOMMENDATION:
✅ Continue testing, target release next week
```

---

## Sign-Off Criteria (Release Gate)

### QA Must Confirm All:

- ✅ All 5 game modes tested end-to-end
- ✅ All platforms tested (WebGL, Android, iOS)
- ✅ Zero CRITICAL bugs remaining
- ✅ Zero HIGH severity bugs remaining (or documented)
- ✅ All test scenarios passed (or documented failures are LOW)
- ✅ Performance meets targets (FPS, memory, load times)
- ✅ No new bugs introduced in last 48 hours
- ✅ All high-priority bugs verified fixed
- ✅ Regression testing passed (all critical paths work)

### Final Sign-Off Report

```
QA SIGN-OFF REPORT
Date: Nov 29, 2025
Signed by: QA Lead

RELEASE READINESS: ✅ GO

Final Metrics:
├─ Total Tests: 200
├─ Pass Rate: 99.5% (199/200)
├─ Outstanding Issues: 1 LOW (cosmetic)
├─ Critical Bugs: 0
├─ High Bugs: 0
└─ Confidence: 99% ready for launch

Tested Devices:
✅ Android: Pixel 5a, Galaxy S21, Galaxy A51, OnePlus 9, Pixel 6a
✅ iOS: iPhone 13, iPhone 12, iPhone SE, iPad Air
✅ WebGL: Chrome, Firefox, Safari

All Game Modes:
✅ Game 1 (Bump 5): Full playthrough, all features
✅ Game 2 (Krazy 6): Full playthrough, all features
✅ Game 3 (Pass The Chip): Full playthrough, all features
✅ Game 4 (Bump U And 5): Full playthrough, all features
✅ Game 5 (Solitary): Full playthrough, all features

Outstanding Items:
├─ BUG-047 (LOW): Button text slightly misaligned on Pixel 4a
│  └─ Severity: LOW, defer to v1.0.1
└─ No other issues

RECOMMENDATION:
✅ APPROVED FOR RELEASE
Proceed to app store submission immediately.
```

---

## Related Documents

- TEST_PLAN_MASTER.md
- REGRESSION_TESTING.md
- SPRINT_8_QA_PLANNING.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for QA Operations
