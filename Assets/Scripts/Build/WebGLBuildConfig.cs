using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// WebGLBuildConfig - Platform-specific configuration for WebGL builds.
/// 
/// Targets:
/// - Build Size: < 50MB
/// - Load Time: < 5 seconds
/// - Performance: 60 FPS on desktop browsers
/// 
/// Optimizations:
/// - WebGL 2.0 support for better performance
/// - ARM64 Wasm for smaller builds
/// - Gzip compression enabled
/// - Streaming assets for progressive loading
/// - Code stripping with IL2CPP
/// </summary>
public class WebGLBuildConfig
{
    // ============================================
    // BUILD SETTINGS
    // ============================================
    
    public static readonly WebGLBuildSettings DEFAULT_SETTINGS = new WebGLBuildSettings
    {
        // Output
        outputPath = "Builds/WebGL",
        buildName = "BumpU_WebGL",
        
        // Constraints
        maxSizeBytes = 50 * 1024 * 1024, // 50MB
        targetLoadTimeSeconds = 5f,
        targetFrameRate = 60,
        
        // Graphics
        enableWebGL2 = true,
        graphicsAPIs = new[] { "WebGL2", "WebGL" },
        enableDynamicResolution = true,
        targetResolution = new Vector2Int(1280, 720),
        
        // Performance
        enableManagedStripping = true,
        stripEngineCode = true,
        stripUnusedMeshComponents = true,
        optimizeForBrowserLoad = true,
        
        // Audio
        compressionFormat = "Vorbis",
        audioQuality = 0.7f, // 70% quality, smaller file
        
        // Texture Compression
        textureCompression = "ASTC", // ASTC for smaller size
        
        // Features to Strip
        stripFeatures = new List<string>
        {
            "Analytics",
            "Cloud",
            "Networking",
            "VR",
            "AR"
        },
        
        // Scenes
        enabledScenes = new List<string>
        {
            "Assets/Scenes/MainMenu.unity",
            "Assets/Scenes/GameplayCore.unity",
            "Assets/Scenes/GameEnd.unity"
        },
        
        // Streaming
        enableStreamingAssets = true,
        preloadCoreScenesOnStartup = true
    };
    
    // ============================================
    // BUILD VALIDATION
    // ============================================
    
    public static bool ValidateBuildSize(long buildSizeBytes)
    {
        bool valid = buildSizeBytes <= DEFAULT_SETTINGS.maxSizeBytes;
        
        Debug.Log($"[WebGLBuildConfig] Build Size: {buildSizeBytes / (1024f * 1024f):F2}MB / {DEFAULT_SETTINGS.maxSizeBytes / (1024f * 1024f):F0}MB {(valid ? "✓" : "✗")}");
        
        if (!valid)
            Debug.LogError($"WebGL build exceeds size limit: {buildSizeBytes / (1024f * 1024f):F2}MB > {DEFAULT_SETTINGS.maxSizeBytes / (1024f * 1024f):F0}MB");
        
        return valid;
    }
    
    public static bool ValidateLoadTime(float loadTimeSeconds)
    {
        bool valid = loadTimeSeconds <= DEFAULT_SETTINGS.targetLoadTimeSeconds;
        
        Debug.Log($"[WebGLBuildConfig] Load Time: {loadTimeSeconds:F2}s / {DEFAULT_SETTINGS.targetLoadTimeSeconds}s {(valid ? "✓" : "✗")}");
        
        if (!valid)
            Debug.LogError($"WebGL build load time exceeds target: {loadTimeSeconds:F2}s > {DEFAULT_SETTINGS.targetLoadTimeSeconds}s");
        
        return valid;
    }
    
    // ============================================
    // OPTIMIZATION RECOMMENDATIONS
    // ============================================
    
    public static List<string> GetOptimizationTips()
    {
        return new List<string>
        {
            "1. Enable WebGL 2.0 for better performance and smaller build",
            "2. Use Brotli compression (if server supports it) for maximum reduction",
            "3. Enable streaming assets to load scenes on-demand",
            "4. Strip unused IL2CPP code with aggressive stripping",
            "5. Use ASTC texture compression for smaller VRAM footprint",
            "6. Compress audio to Ogg Vorbis format (~70% quality)",
            "7. Enable progressive loading indicator during startup",
            "8. Cache build output in browser (set proper cache headers)",
            "9. Use CDN delivery for geo-distributed load",
            "10. Monitor memory usage - target < 512MB for smooth gameplay"
        };
    }
    
    /// <summary>Generate build report with size breakdown</summary>
    public static string GenerateBuildReport(long totalSize, Dictionary<string, long> componentSizes)
    {
        string report = "=== WebGL Build Report ===\n\n";
        report += $"Total Size: {totalSize / (1024f * 1024f):F2}MB (limit: {DEFAULT_SETTINGS.maxSizeBytes / (1024f * 1024f):F0}MB)\n";
        report += $"Status: {(totalSize <= DEFAULT_SETTINGS.maxSizeBytes ? "✓ PASS" : "✗ FAIL")}\n\n";
        
        report += "Component Breakdown:\n";
        foreach (var entry in componentSizes)
        {
            float percentage = (entry.Value / (float)totalSize) * 100;
            report += $"  {entry.Key}: {entry.Value / (1024f * 1024f):F2}MB ({percentage:F1}%)\n";
        }
        
        return report;
    }
}

/// <summary>WebGL-specific build settings</summary>
[System.Serializable]
public class WebGLBuildSettings
{
    // Output
    public string outputPath;
    public string buildName;
    
    // Constraints
    public int maxSizeBytes;
    public float targetLoadTimeSeconds;
    public int targetFrameRate;
    
    // Graphics
    public bool enableWebGL2;
    public string[] graphicsAPIs;
    public bool enableDynamicResolution;
    public Vector2Int targetResolution;
    
    // Performance
    public bool enableManagedStripping;
    public bool stripEngineCode;
    public bool stripUnusedMeshComponents;
    public bool optimizeForBrowserLoad;
    
    // Audio
    public string compressionFormat;
    public float audioQuality;
    
    // Texture
    public string textureCompression;
    
    // Features
    public List<string> stripFeatures;
    
    // Scenes
    public List<string> enabledScenes;
    
    // Streaming
    public bool enableStreamingAssets;
    public bool preloadCoreScenesOnStartup;
}
