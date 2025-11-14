using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// iOSBuildConfig - Platform-specific configuration for iOS builds.
/// 
/// Targets:
/// - Build Size: < 100MB (App Store download limit)
/// - Load Time: < 10 seconds
/// - Performance: 30 FPS minimum, 60 FPS target
/// - OS Version: iOS 14+ for broad device support
/// 
/// Optimizations:
/// - App Thinning (bitcode stripping per device)
/// - Metal graphics API (required for best performance)
/// - ASTC texture compression
/// - On-demand resources for optional content
/// - Battery optimization with frame rate throttling
/// </summary>
public class iOSBuildConfig
{
    // ============================================
    // BUILD SETTINGS
    // ============================================
    
    public static readonly iOSBuildSettings DEFAULT_SETTINGS = new iOSBuildSettings
    {
        // Output
        outputPath = "Builds/iOS",
        xcodePath = "Builds/iOS/BumpU",
        productName = "Bump U",
        bundleIdentifier = "com.bumpu.game",
        version = "1.0.0",
        buildNumber = 1,
        
        // Constraints
        maxSizeBytes = 100 * 1024 * 1024, // 100MB App Store limit
        targetLoadTimeSeconds = 10f,
        targetFrameRateMin = 30,
        targetFrameRateMax = 60,
        
        // OS & Compatibility
        targetOSVersion = "14.0", // iOS 14+
        supportedDevices = new[] { "iPhone", "iPad" },
        supportedOrientations = new[] { "Portrait" },
        landscapeSupport = false,
        
        // Graphics
        graphicsAPI = "Metal",
        enableMetalEditorSupport = true,
        enableDynamicResolution = true,
        
        // Performance
        enableManagedStripping = true,
        scriptBackend = "IL2CPP",
        enableAppThinning = true,
        enableBitcode = false, // Deprecated in newer Xcode
        
        // Audio
        compressionFormat = "ALAC", // Apple Lossless (better quality on Apple hardware)
        audioQuality = 0.8f,
        audioLoadInBackground = false,
        
        // Texture Compression
        textureCompression = "ASTC",
        
        // Battery & Performance
        batteryOptimization = true,
        thermalThrottling = true,
        backgroundFrameRateLimit = 20,
        
        // Features to Strip
        stripFeatures = new List<string>
        {
            "Analytics",
            "Cloud",
            "VR",
            "AR",
            "Stadia"
        },
        
        // Capabilities
        requiredCapabilities = new List<string>
        {
            "armv7", // 32-bit support for older devices
            "arm64"  // 64-bit (required for new apps)
        },
        
        // Scenes
        enabledScenes = new List<string>
        {
            "Assets/Scenes/MainMenu.unity",
            "Assets/Scenes/GameplayCore.unity",
            "Assets/Scenes/GameEnd.unity"
        },
        
        // App Store
        appStoreReady = true,
        requiresAppReview = true,
        ageRating = "4+" // ESRB: Everyone (4+)
    };
    
    // ============================================
    // BUILD VALIDATION
    // ============================================
    
    public static bool ValidateBuildSize(long buildSizeBytes)
    {
        bool valid = buildSizeBytes <= DEFAULT_SETTINGS.maxSizeBytes;
        
        Debug.Log($"[iOSBuildConfig] Build Size: {buildSizeBytes / (1024f * 1024f):F2}MB / {DEFAULT_SETTINGS.maxSizeBytes / (1024f * 1024f):F0}MB {(valid ? "✓" : "✗")}");
        
        if (!valid)
            Debug.LogError($"iOS build exceeds App Store limit: {buildSizeBytes / (1024f * 1024f):F2}MB > {DEFAULT_SETTINGS.maxSizeBytes / (1024f * 1024f):F0}MB");
        
        return valid;
    }
    
    public static bool ValidateAppStoreCompliance(string ageRating, List<string> privacyPolicies)
    {
        bool hasAgeRating = !string.IsNullOrEmpty(ageRating);
        bool hasPrivacyPolicy = privacyPolicies != null && privacyPolicies.Count > 0;
        
        bool valid = hasAgeRating && hasPrivacyPolicy;
        Debug.Log($"[iOSBuildConfig] App Store Compliance: {(valid ? "✓ PASS" : "✗ FAIL")}");
        
        return valid;
    }
    
    // ============================================
    // OPTIMIZATION RECOMMENDATIONS
    // ============================================
    
    public static List<string> GetOptimizationTips()
    {
        return new List<string>
        {
            "1. Use Metal graphics API exclusively (required for best performance)",
            "2. Enable App Thinning in Xcode to strip per-device optimizations",
            "3. Use ASTC texture compression (Apple optimized)",
            "4. Implement on-demand resources for optional content",
            "5. Test on both iPhone (iPhone 12 equiv) and iPad devices",
            "6. Enable battery optimization with frame rate capping at 30 FPS",
            "7. Implement background frame rate limiting (20 FPS when backgrounded)",
            "8. Use ALAC audio codec for lossless quality",
            "9. Support both Portrait and Landscape (or lock to Portrait for stability)",
            "10. Test on iOS 14+ for broad device support",
            "11. Configure privacy policies and age ratings in App Store Connect",
            "12. Test on cellular network (not just WiFi)",
            "13. Implement proper app delegate lifecycle handling",
            "14. Test app thinning with iOS App Store build"
        };
    }
    
    /// <summary>Generate build report with compliance info</summary>
    public static string GenerateBuildReport(long buildSize, List<string> testedDevices, string ageRating)
    {
        string report = "=== iOS Build Report ===\n\n";
        report += $"Build Size: {buildSize / (1024f * 1024f):F2}MB (App Store limit: {DEFAULT_SETTINGS.maxSizeBytes / (1024f * 1024f):F0}MB)\n";
        report += $"Status: {(buildSize <= DEFAULT_SETTINGS.maxSizeBytes ? "✓ PASS" : "✗ FAIL")}\n\n";
        
        report += "Device Testing:\n";
        foreach (var device in testedDevices)
        {
            report += $"  ✓ {device}\n";
        }
        
        report += $"\nTarget OS: {DEFAULT_SETTINGS.targetOSVersion}+\n";
        report += $"Graphics: {DEFAULT_SETTINGS.graphicsAPI}\n";
        report += $"Age Rating: {ageRating}\n";
        report += $"App Store Ready: {(DEFAULT_SETTINGS.appStoreReady ? "✓ Yes" : "✗ No")}\n";
        report += $"Requires Review: {(DEFAULT_SETTINGS.requiresAppReview ? "✓ Yes" : "✗ No")}\n";
        
        return report;
    }
}

/// <summary>iOS-specific build settings</summary>
[System.Serializable]
public class iOSBuildSettings
{
    // Output
    public string outputPath;
    public string xcodePath;
    public string productName;
    public string bundleIdentifier;
    public string version;
    public int buildNumber;
    
    // Constraints
    public int maxSizeBytes;
    public float targetLoadTimeSeconds;
    public int targetFrameRateMin;
    public int targetFrameRateMax;
    
    // OS & Compatibility
    public string targetOSVersion;
    public string[] supportedDevices;
    public string[] supportedOrientations;
    public bool landscapeSupport;
    
    // Graphics
    public string graphicsAPI;
    public bool enableMetalEditorSupport;
    public bool enableDynamicResolution;
    
    // Performance
    public bool enableManagedStripping;
    public string scriptBackend;
    public bool enableAppThinning;
    public bool enableBitcode;
    
    // Audio
    public string compressionFormat;
    public float audioQuality;
    public bool audioLoadInBackground;
    
    // Texture
    public string textureCompression;
    
    // Battery & Performance
    public bool batteryOptimization;
    public bool thermalThrottling;
    public int backgroundFrameRateLimit;
    
    // Features
    public List<string> stripFeatures;
    
    // Capabilities
    public List<string> requiredCapabilities;
    
    // Scenes
    public List<string> enabledScenes;
    
    // App Store
    public bool appStoreReady;
    public bool requiresAppReview;
    public string ageRating;
}
