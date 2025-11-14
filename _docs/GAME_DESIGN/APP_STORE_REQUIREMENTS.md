# App Store Requirements

**Created**: Nov 14, 2025  
**Owner**: Build Engineer  
**Status**: ACTIVE  
**Authority**: Team Assignment Complete

---

## Overview

App Store Requirements define the specific submission process, asset requirements, metadata, and approval guidelines for releasing Bump U on Google Play Store (Android) and Apple App Store (iOS).

---

## Google Play Store (Android)

### Account & Setup

**Prerequisites**:
- Google Play Developer account ($25 one-time)
- Google account linked
- Verified identity (name, address)
- Payment method on file

**Setup Process**:
1. Visit [Google Play Console](https://play.google.com/console)
2. Create new app → Select "Create app"
3. Choose app name, default language
4. Select content rating
5. Complete store listing

### App Metadata

#### App Name
- **Format**: Max 50 characters
- **Example**: "Bump U - Board Game"
- **Best Practice**: Include game type (Board Game, Dice Game) for searchability

#### Short Description
- **Format**: Max 80 characters
- **Purpose**: Shows on store listing and search
- **Example**: "Roll dice, bump opponents, race to win!"

#### Full Description
- **Format**: Max 4,000 characters
- **Purpose**: Detailed explanation of game
- **Include**:
  - Game premise (2-3 sentences)
  - Core gameplay mechanics (3-4 bullet points)
  - Game modes available
  - Platform support (iOS, WebGL)
  - No in-app purchases
  - Offline gameplay (single-player mode note)

**Example Description**:
```
Bump U is a classic board game of strategy and luck for 1-4 players.

GAME MODES:
• Bump 5: First to score 25 points wins
• Krazy 6: Race around board twice
• Pass The Chip: Pass chip to opponent
• Bump U And 5: Combination of rules
• Solitary: Single-player puzzle mode

FEATURES:
• Roll dice and move chips around the board
• Bump opponents' chips back to home
• Race to victory against AI or local players
• Works offline - no internet required
• Available on Android, iOS, and web browsers

No in-app purchases. No ads. Pure board game fun!
```

### App Icon

**Specifications**:
- **Size**: 512 × 512 pixels
- **Format**: PNG or JPEG
- **Safe Area**: All critical content must fit in 432 × 432 (center)
- **Background**: Opaque (no transparency)
- **Colors**: Follow your brand (game logo)

**Design Guidelines**:
- Clear, recognizable at small size (32 × 32)
- No text (except logo wordmark)
- Readable from distance
- Avoid thin lines (< 2 pixels)

**Process**:
1. Create/design 512 × 512 icon
2. Save as PNG (no transparency needed)
3. Upload in Google Play Console → App icon
4. Preview shows how icon appears

### Feature Graphic

**Specifications**:
- **Size**: 1024 × 500 pixels
- **Format**: PNG or JPEG
- **Safe Area**: All content in 924 × 500 (centered)
- **Background**: Can be any color or pattern
- **No transparency**: Opaque background only

**Purpose**: Banner image shown on store listing and featured sections

**Design Tips**:
- Showcase main game board or characters
- Include app name/logo (optional)
- Make visually appealing, not cluttered
- Ensure text is readable

### Screenshots

**Requirements**:
- **Minimum**: 2 screenshots
- **Maximum**: 8 screenshots
- **Size**: 1080 × 1920 pixels (or other standard sizes)
- **Format**: PNG or JPEG
- **Text**: Optional, but helpful for context

**Recommended Screenshots**:
1. Main menu (overview)
2. Gameplay (board view)
3. Win screen (celebration)
4. Settings/options
5. Help/rules screen
6. [Additional gameplay detail]
7. [Game mode example]
8. [Mobile experience]

**Best Practices**:
- Include 1-2 lines of text per screenshot (e.g., "Roll dice and move your chip")
- Show actual gameplay (not just menus)
- Use consistent device frame (optional)
- Highlight key features

### Privacy Policy

**Required**: Yes (even if no data collection)

**Hosting**: Can be on website or within app

**Minimum Content**:
```
PRIVACY POLICY

Bump U does not collect, store, or transmit any personal 
or usage data from players.

Data Not Collected:
- No user accounts required
- No analytics or tracking
- No device identifiers tracked
- No crash reports sent
- No third-party SDKs

Offline Play:
All gameplay occurs entirely on the player's device. 
No internet connection is required after initial download.

Contact:
For questions, contact [your email]
```

**Link in Console**:
- Create text file on your website
- Provide HTTPS URL in Play Console
- Or paste full text in console field

### Content Rating

**IARC Questionnaire**:
1. Go to Google Play Console
2. Content ratings → Complete questionnaire
3. Answer questions about:
   - Violence/gore
   - Sexual content
   - Language
   - Substance use
   - Game mechanics (gambling?)

**Typical Answers for Bump U**:
- Violence: No
- Sexual content: No
- Language: No profanity
- Substance use: No
- Gambling: No (dice roll is mechanic, not gambling)
- Result: PEGI 3 / ESRB E / USK 0

### Pricing & Distribution

**Pricing**:
- Price tier: Free
- Pricing currency: Auto-detect (or select)
- Free trial: N/A (game is free)

**Distribution**:
- Countries: All countries (or select specific)
- Targeting: No regional restrictions

**Release Date**:
- Set to automatic (immediately after approval)
- Or schedule for specific date

### Pre-Submission Checklist

- [ ] App name (50 chars max)
- [ ] Short description (80 chars)
- [ ] Full description (4,000 chars)
- [ ] App icon (512 × 512 PNG)
- [ ] Feature graphic (1024 × 500 PNG)
- [ ] 2-8 screenshots (1080 × 1920)
- [ ] Privacy policy (URL or text)
- [ ] Content rating (completed IARC)
- [ ] Pricing (Free selected)
- [ ] APK/AAB build uploaded
- [ ] App credentials filled
- [ ] Rights confirmed (no licensing issues)

### Submission Process

**Step 1: Prepare Assets**
- Create all graphics (icon, feature, screenshots)
- Write metadata (name, description)
- Create privacy policy

**Step 2: Upload Build**
- Go to Console → Internal Testing
- Upload signed APK or AAB
- Wait for processing (~ 5 min)
- Test link generated

**Step 3: Internal Testing**
- Add test users (email addresses)
- Send link to testers
- Verify game works
- Check for crashes
- Confirm all features functional

**Step 4: Upload to Production**
- Console → Production
- Upload same APK/AAB
- Fill metadata (name, description, etc.)
- Upload all assets (icon, feature, screenshots)
- Set privacy policy link

**Step 5: Submit for Review**
- Review all information
- Agree to content policies
- Click "Submit app for review"
- Wait 24-48 hours for review

**Step 6: Monitoring**
- Check Console for approval/rejection
- If rejected: Review feedback, fix issues, resubmit
- If approved: Live on Play Store

### Common Rejection Reasons

| Reason | Solution |
|--------|----------|
| APK crashes | Test on multiple devices, fix bugs, rebuild |
| Misleading description | Ensure description matches actual game |
| Content rating mismatch | Retake questionnaire, ensure accurate |
| Screenshot quality low | Use actual device screenshots (1080 × 1920) |
| Icon too small/unclear | Redesign icon (512 × 512, clear) |
| Missing privacy policy | Add link to privacy policy in metadata |
| Intellectual property issue | Ensure all art/music are original/licensed |

---

## Apple App Store (iOS)

### Account & Setup

**Prerequisites**:
- Apple Developer Program account ($99/year)
- Apple ID with verified identity
- Payment method on file
- App-specific password (not main Apple ID password)

**Setup Process**:
1. Visit [App Store Connect](https://appstoreconnect.apple.com)
2. "My Apps" → Create new app
3. Select platform (iOS)
4. Fill in app name, bundle identifier, SKU, language
5. Select category, subcategory

### App Metadata

#### App Name
- **Format**: Max 30 characters (for icon label)
- **Example**: "Bump U"
- **Important**: Keep short for display under icon

#### Subtitle
- **Format**: Max 30 characters
- **Example**: "Roll, Bump, Win!"
- **Purpose**: Shows below app name in App Store

#### Description
- **Format**: Max 4,000 characters
- **Purpose**: Full description of game features and gameplay
- **Same as Android**: Use similar content, adapted for iOS

**Example**:
```
Bump U is a classic board game of strategy and luck for 1-4 players.

Roll dice, move your chip around the board, and race to victory! 
Bump opponents back to home, strategize your moves, and compete 
in 5 unique game modes:

• Bump 5 - First to 25 points wins
• Krazy 6 - Race around twice
• Pass The Chip - Pass to your opponent
• Bump U And 5 - Combined rules
• Solitary - Single-player mode

Features:
✓ Play offline - no internet required
✓ Works on iPhone and iPad
✓ No in-app purchases or ads
✓ Support for 1-4 players
✓ AI opponents available
```

#### Keywords
- **Format**: 30 characters max total (comma-separated)
- **Purpose**: Improve search discoverability
- **Examples**: "board game, dice game, multiplayer, strategy"
- **Count**: 3-5 keywords recommended

#### Support URL
- **Format**: HTTPS link to website/support page
- **Purpose**: Link users can click for help
- **Example**: "https://bumpu.example.com/support"

#### Privacy Policy URL
- **Format**: HTTPS link (required)
- **Purpose**: Link to full privacy policy
- **Example**: "https://bumpu.example.com/privacy"

### App Icon

**Specifications**:
- **Size**: 1024 × 1024 pixels
- **Format**: PNG
- **Transparency**: Not allowed (opaque)
- **Safe Area**: Avoid corners (rounded in display)
- **Requirements**: No text (except small logo)

**Process**:
1. Create/design 1024 × 1024 PNG
2. App Store Connect → App Information → App Icon
3. Drag and drop or select file
4. Preview shows how it will appear

### App Preview Video (Optional)

**Specifications** (if included):
- **Resolution**: 1080 × 1920 (9:16 portrait)
- **Duration**: 15-30 seconds
- **Format**: MP4 or MOV
- **No audio**: Auto-muted on store

**Purpose**: Showcase gameplay to potential users

**Content Ideas**:
- Intro (title screen, 3 sec)
- Gameplay (dice roll, move chip, 8 sec)
- Game mode showcase (2-3 modes, 5 sec)
- Win screen (2 sec)
- Call to action (3 sec)

### Screenshots

**Requirements**:
- **Minimum**: 2 screenshots
- **Maximum**: 10 screenshots
- **Resolution**: 1080 × 1920 pixels (9:16 portrait)
- **Format**: PNG or JPEG
- **Order**: Shows in order you upload

**Recommended Set**:
1. Main menu / title screen
2. Gameplay (board with chips)
3. Dice roll / game action
4. Win condition / celebration
5. Game settings/options
6. Rules/help screen
7. Game mode selection
8. Single-player AI gameplay

**Best Practices**:
- Use actual device screenshots (not mocked-up)
- Include 1 line of descriptive text (optional)
- Show key features in each screenshot
- Consistent device/background

### Privacy Policy

**Required**: Yes, full URL must be provided

**Hosting**: On your website (HTTPS required)

**Content**: Same as Android privacy policy

**Minimum**:
```
PRIVACY POLICY - Bump U

Bump U does not collect, store, or transmit any personal data.

What We Don't Collect:
- No user accounts or sign-up required
- No analytics or tracking
- No device identifiers
- No crash reports
- No third-party SDKs or trackers

Gameplay:
All gameplay is offline and local. No internet connection required.

Questions:
Contact: [email]
Website: [website]
```

### Content Rating (Age Rating)

**Required**: ESRB, PEGI, ClassInd rating

**Process**:
1. App Store Connect → App Information → Age Rating
2. Answer 8-10 questions
3. System auto-calculates rating

**Typical Answer for Bump U**:
- Violence: None
- Sexual/suggestive: None
- Alcohol/tobacco: None
- Gambling: No (dice is game mechanic)
- Result: 4+ (or equivalent in your region)

### Release Notes

**For Each Version**:
- **Format**: 170 characters max
- **Purpose**: Shown on app store updates page
- **Example**: "Version 1.0 - Initial release with 5 game modes, offline play, and 1-4 player support."

### Category & Subcategory

**Primary Category**: Games

**Subcategory Options**:
- Board Games (recommended)
- Strategy Games
- Casual Games

**Most Appropriate**: Board Games

### Pre-Submission Checklist

- [ ] App name (max 30 chars)
- [ ] Subtitle (max 30 chars, optional but recommended)
- [ ] Description (max 4,000 chars)
- [ ] Keywords (max 30 chars total)
- [ ] Support URL (HTTPS)
- [ ] Privacy Policy URL (HTTPS)
- [ ] App icon (1024 × 1024 PNG)
- [ ] 2-10 screenshots (1080 × 1920)
- [ ] Age rating completed
- [ ] Category selected (Board Games)
- [ ] Release notes (170 chars)
- [ ] Build uploaded and processed
- [ ] EULA accepted
- [ ] Content rights confirmed

### Submission Process (TestFlight + Production)

**Step 1: Build Preparation**
- Create signed build in Xcode
- Upload to App Store Connect
- Wait for processing (5-10 min)

**Step 2: TestFlight (Internal Testing)**
- Go to TestFlight tab
- Build processing complete → add testers
- Add internal team members (email)
- Send invitations
- Test on actual devices
- Verify 60 FPS, no crashes

**Step 3: TestFlight (External Testing, Optional)**
- Create external test group
- Add up to 10,000 beta testers
- Distribute link (TestFlight link or email)
- Collect feedback for 1-2 weeks
- Monitor crash reports

**Step 4: Fill App Metadata**
- All information from checklist above
- Screenshots, descriptions, keywords
- Privacy policy and support URLs
- Age rating, category
- Release notes

**Step 5: Select Build for Production**
- Choose build (internal tested)
- Attach to production release
- Review all information
- Press "Submit for Review"

**Step 6: Apple Review Process**
- Automated initial checks (1-5 min)
- Manual review (24-48 hours typical)
- Potential issues:
  - App crashes → fix and resubmit
  - Misleading description → update and resubmit
  - Intellectual property → clarify or remove

**Step 7: Release**
- Approved → Release immediately or schedule date
- Users can download from App Store
- Monitor crash reports and reviews

### Common Rejection Reasons

| Reason | Solution |
|--------|----------|
| Crashes on startup | Test on multiple iOS versions, fix bugs |
| Unresponsive UI | Ensure buttons work, animations smooth |
| Privacy policy missing/invalid | Add valid HTTPS URL to privacy policy |
| Misleading description | Match description to actual game |
| Inappropriate content | Remove or rate correctly |
| Incomplete game | Ensure fully playable, not beta |
| Spam-like behavior | Avoid excessive notifications |
| Too similar to existing app | Differentiate or clarify unique features |

### Post-Launch Monitoring

**Monitor**:
- Crash reports (every day)
- User reviews (for feedback)
- App analytics (user retention)
- Performance metrics (FPS stability)

**Updates**:
- Plan bug fixes for v1.0.1 (if needed)
- Plan feature updates for v1.1+
- Maintain regular update cycle (at least annually)

---

## WebGL Deployment (Itch.io)

### Setup

1. Create [Itch.io](https://itch.io) account (free)
2. Creator dashboard → Create new project
3. Fill in title, description, category (Game)
4. Set as "HTML" game
5. Upload WebGL build

### Metadata

**Title**: "Bump U"  
**Description**: Same as store listings  
**Category**: Board Game  
**Tags**: board-game, dice-game, multiplayer, strategy

### Build Upload

**Process**:
1. Create WebGL build in Unity
2. Go to project settings
3. Select "Upload to itch.io"
4. Drag build folder → Upload
5. Make public when ready

**File Structure to Upload**:
```
bumpu-webgl/
├─ index.html
├─ Build/
│  ├─ [ProjectName].framework.js.gz
│  ├─ [ProjectName].data.gz
│  └─ [ProjectName].wasm.gz
├─ TemplateData/
│  ├─ style.css
│  └─ favicon.ico
└─ StreamingAssets/
```

### Publishing

1. Set visibility: Public
2. Choose if monetization needed
3. Add optional "Download" option (allow offline play)
4. Share link with community

---

## Cross-Store Coordination

### Timeline

```
Week 1: Prepare assets (all platforms)
  ├─ Icon, screenshots, descriptions
  ├─ Privacy policy
  └─ Content rating questionnaire

Week 2: Internal testing
  ├─ Android: Closed testing on Play Store
  ├─ iOS: TestFlight internal
  └─ WebGL: Manual testing on itch.io

Week 3: Submission
  ├─ Android: Submit to Play Store
  ├─ iOS: Submit to App Store
  └─ WebGL: Already live on itch.io

Week 4: Monitoring
  ├─ Check for approvals/rejections
  ├─ Fix issues if needed
  ├─ Go live on iOS/Android
  └─ Monitor crash reports
```

### Consistent Messaging

**Across all platforms**:
- Same app name (or minor adaptation)
- Same description (localize if needed)
- Same icon design
- Consistent screenshots style
- Same privacy policy

---

## Related Documents

- BUILD_PIPELINE.md
- PLATFORM_REQUIREMENTS.md
- SPRINT_7_BUILD_PREP.md

---

**Last Updated**: Nov 14, 2025  
**Status**: Complete & Ready for Implementation
