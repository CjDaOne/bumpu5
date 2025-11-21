# AGENTS.md - Bump U Project Guide

## Project Overview
**Bump U** is a digital board game adaptation of the classic "Bump U Box 5" board game. The game features 5 different games played on a single 11x7 board with chips, dice, and strategic gameplay.

## Game Rules Overview
- **Game #1:** Get 5 chips in a row (horizontally, vertically, diagonally) - must say "BUMP U BOX 5" to win
- **Game #2 (Krazy 6):** Place all 6 chips on board, roll a #6 to win - must say "BUMP U KRAZY 6"
- **Game #3 (Pass the Chip):** Box in center #5 for points - must say "BUMP U" when bumping
- **Game #4 (#5 Bumping):** First to get 5 chips, #5 acts as opponent - must say "BUMP U and #5" to win
- **Game #5 (Solitary):** Fill entire board before getting bumped off - single player mode

## Core Mechanics
- **Bumping:** Remove opponent chips when rolling matching board numbers (must say "BUMP U")
- **Dice Rolls:** Standard d6 die, special rules for #5 and #6 in different games
- **Turn Structure:** 1 roll per turn unless stated otherwise
- **Board:** 11x7 grid with numbers to match dice rolls

## Code Structure (To be created)
- `Assets/Scripts/Games/` - Individual game implementations
- `Assets/Scripts/Core/` - Board, dice, chip management
- `Assets/Scripts/UI/` - Game selection, score tracking, win conditions

## Code Style & Conventions
- **Language:** C# (Unity scripts)
- **Namespace:** `Assets.Scripts.{FeatureName}`
- **Naming:** PascalCase (classes), camelCase (fields/properties)
- **Error Handling:** Null checks for component references, input validation
- **Comments:** Document game rules and win conditions clearly

## Build & Test
- **Open:** Unity 2021+ LTS
- **Run Tests:** Window > General > Test Runner (Unity Test Framework)
- **Build:** File > Build Settings > Build
