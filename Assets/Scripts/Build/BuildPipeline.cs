using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// BuildPipeline - Configuration and utilities for multi-platform builds.
/// 
/// Responsibilities:
/// - Define build configurations for each platform
/// - Manage scene loading strategies per platform
/// - Handle platform-specific initialization
/// - Provide build size optimization options
/// 
/// Platforms:
/// - WebGL (< 50MB, < 5 sec load)
/// - Android (< 100MB, Play Store ready)
/// - iOS (< 100MB, App Store ready)
/// </summary>
public class BuildPipeline : ScriptableObject
{
    // ============================================
    // BUILD CONFIGURATIONS
    // ============================================
    
    [System.Serializable]
    public class BuildConfig
    {
        public string platform;
        public string outputPath;
        public int maxSizeBytes;
        public float targetLoadTimeSeconds;
        public List<string> enabledScenes = new List<string>();
        public List<string> stripScripts = new List<string>();
        public bool enableManagedStripping = true;
        public bool enableScriptDebugging = false;
    }
    
    [SerializeField]
    private List<BuildConfig> buildConfigs = new List<BuildConfig>();
    
    // ============================================
    // PLATFORM DEFINITIONS
    // ============================================
    
    public static readonly BuildConfig WEBGL_CONFIG = new BuildConfig
    {
        platform = "WebGL",
        outputPath = "Builds/WebGL",
        maxSizeBytes = 50 * 1024 * 1024, // 50MB
        targetLoadTimeSeconds = 5f,
        enableManagedStripping = true,
        enableScriptDebugging = false
    };
    
    public static readonly BuildConfig ANDROID_CONFIG = new BuildConfig
    {
        platform = "Android",
        outputPath = "Builds/Android",
        maxSizeBytes = 100 * 1024 * 1024, // 100MB
        targetLoadTimeSeconds = 10f,
        enableManagedStripping = true,
        enableScriptDebugging = false
    };
    
    public static readonly BuildConfig IOS_CONFIG = new BuildConfig
    {
        platform = "iOS",
        outputPath = "Builds/iOS",
        maxSizeBytes = 100 * 1024 * 1024, // 100MB
        targetLoadTimeSeconds = 10f,
        enableManagedStripping = true,
        enableScriptDebugging = false
    };
    
    // ============================================
    // BUILD OPTIMIZATION
    // ============================================
    
    /// <summary>Get all scenes required for a platform build</summary>
    public static List<string> GetRequiredScenes(string platform)
    {
        List<string> scenes = new List<string>()
        {
            "Assets/Scenes/MainMenu.unity",
            "Assets/Scenes/GameplayCore.unity",
            "Assets/Scenes/GameEnd.unity"
        };
        
        return scenes;
    }
    
    /// <summary>Get build size estimate for platform</summary>
    public static int GetBuildSizeEstimate(string platform)
    {
        return platform switch
        {
            "WebGL" => 35 * 1024 * 1024,  // ~35MB estimated
            "Android" => 75 * 1024 * 1024, // ~75MB estimated
            "iOS" => 80 * 1024 * 1024,     // ~80MB estimated
            _ => 0
        };
    }
    
    /// <summary>Get optimization recommendations for platform</summary>
    public static List<string> GetOptimizationTips(string platform)
    {
        List<string> tips = new List<string>();
        
        switch (platform)
        {
            case "WebGL":
                tips.Add("Enable WebGL 2.0 support");
                tips.Add("Use ARM64 for smaller builds");
                tips.Add("Compress WebGL build");
                tips.Add("Enable streaming assets");
                break;
            case "Android":
                tips.Add("Use Android Gradle build");
                tips.Add("Enable split APK for different architectures");
                tips.Add("Compress textures with ASTC/ETC2");
                tips.Add("Use ProGuard for code stripping");
                break;
            case "iOS":
                tips.Add("Use Xcode 13+");
                tips.Add("Enable Metal graphics");
                tips.Add("Configure App Thinning in Xcode");
                tips.Add("Use ASTC textures for iOS 12+");
                break;
        }
        
        return tips;
    }
}

/// <summary>Build pipeline manager for CI/CD integration</summary>
public class BuildPipelineManager
{
    // ============================================
    // BUILD EXECUTION (Placeholder for CI/CD)
    // ============================================
    
    /// <summary>Execute a build for specified platform</summary>
    public static bool ExecuteBuild(string platform)
    {
        Debug.Log($"[BuildPipelineManager] Building for {platform}...");
        
        BuildConfig config = platform switch
        {
            "WebGL" => BuildPipeline.WEBGL_CONFIG,
            "Android" => BuildPipeline.ANDROID_CONFIG,
            "iOS" => BuildPipeline.IOS_CONFIG,
            _ => null
        };
        
        if (config == null)
        {
            Debug.LogError($"Unknown platform: {platform}");
            return false;
        }
        
        Debug.Log($"Build configuration: {config.platform}");
        Debug.Log($"Output path: {config.outputPath}");
        Debug.Log($"Max size: {config.maxSizeBytes / (1024 * 1024)}MB");
        Debug.Log($"Target load time: {config.targetLoadTimeSeconds}s");
        
        // In actual CI/CD pipeline, this would call:
        // - BuildPlayer() with appropriate settings
        // - Size validation
        // - Performance testing
        // - Auto-submission to app stores
        
        return true;
    }
    
    /// <summary>Validate build size and performance</summary>
    public static bool ValidateBuild(string platform, long buildSizeBytes, float loadTimeSeconds)
    {
        BuildConfig config = platform switch
        {
            "WebGL" => BuildPipeline.WEBGL_CONFIG,
            "Android" => BuildPipeline.ANDROID_CONFIG,
            "iOS" => BuildPipeline.IOS_CONFIG,
            _ => null
        };
        
        if (config == null)
            return false;
        
        bool sizeValid = buildSizeBytes <= config.maxSizeBytes;
        bool timeValid = loadTimeSeconds <= config.targetLoadTimeSeconds;
        
        Debug.Log($"[BuildPipelineManager] {platform} Build Validation:");
        Debug.Log($"  Size: {buildSizeBytes / (1024 * 1024)}MB / {config.maxSizeBytes / (1024 * 1024)}MB {(sizeValid ? "✓" : "✗")}");
        Debug.Log($"  Load Time: {loadTimeSeconds}s / {config.targetLoadTimeSeconds}s {(timeValid ? "✓" : "✗")}");
        
        return sizeValid && timeValid;
    }
}
