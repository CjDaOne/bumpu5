using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Editor script to trigger builds via Menu Item or Command Line.
/// Bridges the gap between BuildPipeline configuration and Unity's BuildPlayer.
/// </summary>
public class BuildCommand
{
    // ============================================
    // MENU ITEMS
    // ============================================

    [MenuItem("Build/Build All Platforms", false, 1)]
    public static void BuildAll()
    {
        BuildWebGL();
        BuildAndroid();
        BuildIOS();
    }

    [MenuItem("Build/Build WebGL", false, 11)]
    public static void BuildWebGL()
    {
        PerformBuild(BuildPipeline.WEBGL_CONFIG);
    }

    [MenuItem("Build/Build Android", false, 12)]
    public static void BuildAndroid()
    {
        PerformBuild(BuildPipeline.ANDROID_CONFIG);
    }

    [MenuItem("Build/Build iOS", false, 13)]
    public static void BuildIOS()
    {
        PerformBuild(BuildPipeline.IOS_CONFIG);
    }

    // ============================================
    // COMMAND LINE ENTRY POINT
    // ============================================

    /// <summary>
    /// Entry point for CI/CD or command line builds.
    /// Usage: Unity -quit -batchmode -executeMethod BuildCommand.BuildFromCommandLine -buildTarget <Target>
    /// </summary>
    public static void BuildFromCommandLine()
    {
        // Parse command line arguments if needed, or just default to a specific target
        // For simplicity, we'll check for a custom arg or just build all
        
        string[] args = System.Environment.GetCommandLineArgs();
        string target = "All";

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-buildTarget" && i + 1 < args.Length)
            {
                target = args[i + 1];
            }
        }

        switch (target.ToLower())
        {
            case "webgl":
                BuildWebGL();
                break;
            case "android":
                BuildAndroid();
                break;
            case "ios":
                BuildIOS();
                break;
            default:
                Debug.Log($"[BuildCommand] Building All Targets (Target was '{target}')");
                BuildAll();
                break;
        }
    }

    // ============================================
    // BUILD LOGIC
    // ============================================

    private static void PerformBuild(BuildPipeline.BuildConfig config)
    {
        if (config == null)
        {
            Debug.LogError("[BuildCommand] Invalid build configuration.");
            return;
        }

        Debug.Log($"[BuildCommand] Starting build for {config.platform}...");

        // Ensure output directory exists
        string buildPath = config.outputPath;
        if (!Directory.Exists(buildPath))
        {
            Directory.CreateDirectory(buildPath);
        }

        // Get scenes
        string[] scenes = GetEnabledScenes();

        // Setup build options
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenes;
        buildPlayerOptions.locationPathName = GetBuildLocation(config);
        buildPlayerOptions.target = GetBuildTarget(config.platform);
        buildPlayerOptions.options = BuildOptions.None;

        if (config.enableScriptDebugging)
        {
            buildPlayerOptions.options |= BuildOptions.Development | BuildOptions.AllowDebugging;
        }

        // Execute Build
        UnityEditor.Build.Reporting.BuildReport report = UnityEditor.BuildPipeline.BuildPlayer(buildPlayerOptions);
        UnityEditor.Build.Reporting.BuildSummary summary = report.summary;

        if (summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log($"[BuildCommand] {config.platform} Build Succeeded: {summary.totalSize / 1024 / 1024} MB");
            
            // Validate against constraints
            BuildPipelineManager.ValidateBuild(config.platform, (long)summary.totalSize, 0); // Load time hard to measure here
        }
        else if (summary.result == UnityEditor.Build.Reporting.BuildResult.Failed)
        {
            Debug.LogError($"[BuildCommand] {config.platform} Build Failed: {summary.totalErrors} errors");
        }
    }

    private static string[] GetEnabledScenes()
    {
        // In a real scenario, read from BuildSettings or config
        // For now, use the hardcoded list from BuildPipeline
        return BuildPipeline.GetRequiredScenes("Any").ToArray();
    }

    private static BuildTarget GetBuildTarget(string platform)
    {
        return platform switch
        {
            "WebGL" => BuildTarget.WebGL,
            "Android" => BuildTarget.Android,
            "iOS" => BuildTarget.iOS,
            _ => BuildTarget.StandaloneWindows64 // Default fallback
        };
    }

    private static string GetBuildLocation(BuildPipeline.BuildConfig config)
    {
        // Unity requires specific extensions for some platforms
        return config.platform switch
        {
            "Android" => Path.Combine(config.outputPath, "game.apk"),
            "iOS" => config.outputPath, // iOS builds to a folder (Xcode project)
            "WebGL" => config.outputPath, // WebGL builds to a folder
            _ => Path.Combine(config.outputPath, "game.exe")
        };
    }
}
