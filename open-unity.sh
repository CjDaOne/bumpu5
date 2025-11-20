#!/bin/bash
# Unity Project Launcher - Bump U
# This script cleans stale lock files and opens the project in Unity Editor

PROJECT_PATH="/home/cjnf/Desktop/Bump U"
UNITY_EDITOR="/home/cjnf/Unity/Hub/Editor/6000.2.12f1/Editor/Unity"

echo "🎮 Bump U - Unity Project Launcher"
echo "=================================="

# Kill any existing Unity processes for this project
echo "🔍 Checking for running Unity instances..."
pkill -f "Unity.*Bump U" 2>/dev/null
sleep 1

# Remove stale PID files
echo "🧹 Cleaning stale lock files..."
rm -f "$PROJECT_PATH/Library/ilpp.pid" 2>/dev/null
rm -f "$PROJECT_PATH/Temp/UnityLockfile" 2>/dev/null

# Launch Unity Editor
echo "🚀 Launching Unity Editor..."
"$UNITY_EDITOR" -projectPath "$PROJECT_PATH" &

echo "✅ Unity Editor is launching!"
echo "   Project: $PROJECT_PATH"
echo "   Please wait 30-60 seconds for Unity to load..."
