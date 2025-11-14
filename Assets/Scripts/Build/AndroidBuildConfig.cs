using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// AndroidBuildConfig - Platform-specific configuration for Android builds.
/// 
/// Targets:
/// - Build Size: < 100MB (Play Store limit for immediate download)
/// - Load Time: < 10 seconds
/// - Performance: 30 FPS minimum, 60 FPS target on modern devices
/// - API Level: 24+ (Android 7+) for broader device support
/// 
/// Optimizations:
/// - Split APK by architecture (ARM64 primary)
/// - ProGuard code obfuscation and shrinking
/// - Texture compression (ASTC/ETC2)
/// - On-demand asset delivery
/// - Battery optimization (frame rate throttling)
/// </summary>
public class AndroidBuildConfig
{
    // ============================================
    // BUILD SETTINGS
    // ============================================
    
    public static readonly AndroidBuildSettings DEFAULT_SETTINGS = new AndroidBuildSettings
    {
        // Output
        outputPath = "Builds/Android",
        buildName = "BumpU",
        packageName = "com.bumpu.game",
        versionCode = 1,
        versionName = "1.0.0",
        
        // Constraints
        maxSizeBytes = 100 * 1024 * 1024, // 100MB Play Store limit
        targetLoadTimeSeconds = 10f,
        targetFrameRateMin = 30,
        targetFrameRateMax = 60,
        
        // API & Compatibility
        minAPILevel = 24, // Android 7.0
        targetAPILevel = 34, // Android 14
        architectures = new[] { "ARM64" }, // Primary: ARM64 only for <100MB
        splitAPKByArchitecture = true,
        
        // Graphics
        graphicsAPI = "OpenGLES3",
        enableGPUInstancing = true,
        enableDynamicResolution = true,
        
        // Performance
        enableManagedStripping = true,
        useProGuard = true,
        scriptBackend = "IL2CPP",
        
        // Audio
        compressionFormat = "Vorbis",
        audioQuality = 0.6f, // 60% quality for mobile
        audioLoadInBackground = false,
        
        // Texture Compression
        primaryTextureCompression = "ASTC",
        fallbackTextureCompression = "ETC2",
        
        // Battery & Thermal
        batteryOptimization = true,
        thermalThrottling = true,
        
        // Features
        stripFeatures = new List<string>
        {
            "Analytics",
            "Cloud",
            "VR",
            "AR",
            "Stadia"
        },
        
        // Permissions
        requiredPermissions = new List<string>
        {
            "INTERNET",
            "ACCESS_NETWORK_STATE"
        },
        
        // Scenes
        enabledScenes = new List<string>
        {
            "Assets/Scenes/MainMenu.unity",
            "Assets/Scenes/GameplayCore.unity",
            "Assets/Scenes/GameEnd.unity"
        },
        
        // Store
        playstoreReady = true,
        releaseKeystore = false
    };
    
    // ============================================
    // BUILD VALIDATION
    // ============================================
    
    public static bool ValidateBuildSize(long buildSizeBytes)
    {
        bool valid = buildSizeBytes <= DEFAULT_SETTINGS.maxSizeBytes;
        
        Debug.Log($"[AndroidBuildConfig] Build Size: {buildSizeBytes / (1024f * 1024f):F2}MB / {DEFAULT_SETTINGS.maxSizeBytes / (1024f * 1024f):F0}MB {(valid ? "✓" : "✗")}");
        
        if (!valid)
            Debug.LogError($"Android build exceeds Play Store limit: {buildSizeBytes / (1024f * 1024f):F2}MB > {DEFAULT_SETTINGS.maxSizeBytes / (1024f * 1024f):F0}MB");
        
        return valid;
    }
    
    public static bool ValidateDeviceSupport(List<string> testedDevices)
    {
        // Validate that build was tested on representative devices
        bool hasLowEndDevice = testedDevices.Any(d => d.Contains("Pixel 5a") || d.Contains("Redmi"));
        bool hasMidRangeDevice = testedDevices.Any(d => d.Contains("Pixel 6") || d.Contains("OnePlus"));
        bool hasHighEndDevice = testedDevices.Any(d => d.Contains("Pixel 7") || d.Contains("Samsung"));
        
        bool valid = hasLowEndDevice && hasMidRangeDevice;
        Debug.Log($"[AndroidBuildConfig] Device Coverage: {(valid ? "✓ PASS" : "✗ FAIL")}");
        
        return valid;
    }
    
    // ============================================
    // OPTIMIZATION RECOMMENDATIONS
    // ============================================
    
    public static List<string> GetOptimizationTips()
    {
        return new List<string>
        {
            "1. Use ARM64 architecture only (<100MB constraint)",
            "2. Enable ProGuard/R8 for aggressive code shrinking",
            "3. Use ASTC texture compression (primary) with ETC2 fallback",
            "4. Implement on-demand asset delivery with Play Asset Delivery",
            "5. Enable battery optimization with frame rate throttling",
            "6. Test on low-end devices (Pixel 5a equiv) for performance",
            "7. Monitor thermal throttling and implement frame rate limiting",
            "8. Compress audio to Ogg Vorbis (~60% quality for mobile)",
            "9. Implement background frame rate reduction (20 FPS when backgrounded)",
            "10. Use ProGuard mapping file for crash log deobfuscation",
            "11. Test on both WiFi and cellular networks",
            "12. Verify Play Store compliance (content rating, policies)"
        };
    }
    
    /// <summary>Generate build report with size and compatibility info</summary>
    public static string GenerateBuildReport(long totalSize, List<string> testedDevices)
    {
        string report = "=== Android Build Report ===\n\n";
        report += $"Build Size: {totalSize / (1024f * 1024f):F2}MB (Play Store limit: {DEFAULT_SETTINGS.maxSizeBytes / (1024f * 1024f):F0}MB)\n";
        report += $"Status: {(totalSize <= DEFAULT_SETTINGS.maxSizeBytes ? "✓ PASS" : "✗ FAIL")}\n\n";
        
        report += "Device Testing:\n";
        foreach (var device in testedDevices)
        {
            report += $"  ✓ {device}\n";
        }
        
        report += $"\nAPI Level: {DEFAULT_SETTINGS.minAPILevel} - {DEFAULT_SETTINGS.targetAPILevel}\n";
        report += $"Architecture: {string.Join(", ", DEFAULT_SETTINGS.architectures)}\n";
        report += $"Play Store Ready: {(DEFAULT_SETTINGS.playstoreReady ? "✓ Yes" : "✗ No")}\n";
        
        return report;
    }
}

/// <summary>Android-specific build settings</summary>
[System.Serializable]
public class AndroidBuildSettings
{
    // Output
    public string outputPath;
    public string buildName;
    public string packageName;
    public int versionCode;
    public string versionName;
    
    // Constraints
    public int maxSizeBytes;
    public float targetLoadTimeSeconds;
    public int targetFrameRateMin;
    public int targetFrameRateMax;
    
    // API & Compatibility
    public int minAPILevel;
    public int targetAPILevel;
    public string[] architectures;
    public bool splitAPKByArchitecture;
    
    // Graphics
    public string graphicsAPI;
    public bool enableGPUInstancing;
    public bool enableDynamicResolution;
    
    // Performance
    public bool enableManagedStripping;
    public bool useProGuard;
    public string scriptBackend;
    
    // Audio
    public string compressionFormat;
    public float audioQuality;
    public bool audioLoadInBackground;
    
    // Texture
    public string primaryTextureCompression;
    public string fallbackTextureCompression;
    
    // Battery & Thermal
    public bool batteryOptimization;
    public bool thermalThrottling;
    
    // Features
    public List<string> stripFeatures;
    
    // Permissions
    public List<string> requiredPermissions;
    
    // Scenes
    public List<string> enabledScenes;
    
    // Store
    public bool playstoreReady;
    public bool releaseKeystore;
}
