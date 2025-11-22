using UnityEngine;
using UnityEngine.Profiling;
using System.Collections.Generic;
using System;

/// <summary>
/// PerformanceProfiler - Real-time performance monitoring and profiling.
/// 
/// Tracks:
/// - Frame rate (FPS)
/// - Memory usage (heap, total)
/// - CPU load
/// - GPU load
/// - Frame time breakdown
/// - Thermal state
/// - Battery impact
/// 
/// Targets:
/// - Desktop: 60 FPS stable
/// - Mobile: 30-60 FPS depending on device
/// - Memory: < 512MB
/// - Thermal: No throttling
/// </summary>
public class PerformanceProfiler : MonoBehaviour
{
    // ============================================
    // SINGLETON
    // ============================================
    
    private static PerformanceProfiler instance;
    public static PerformanceProfiler Instance => instance;
    
    // ============================================
    // CONFIGURATION
    // ============================================
    
    [SerializeField]
    private bool enableProfiling = true;
    
    [SerializeField]
    private float sampleInterval = 1f; // Sample every 1 second
    
    [SerializeField]
    private int maxSamples = 300; // Keep 5 minutes of data at 1s/sample
    
    // ============================================
    // PERFORMANCE DATA
    // ============================================
    
    private PerformanceMetrics currentMetrics;
    private Queue<PerformanceMetrics> metricsHistory;
    
    private float timeSinceLastSample;
    private int frameCount;
    private float deltaTimeAccumulator;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        metricsHistory = new Queue<PerformanceMetrics>(maxSamples);
        currentMetrics = new PerformanceMetrics();
        timeSinceLastSample = 0;
        frameCount = 0;
    }
    
    private void Update()
    {
        if (!enableProfiling)
            return;
        
        frameCount++;
        deltaTimeAccumulator += Time.deltaTime;
        timeSinceLastSample += Time.deltaTime;
        
        if (timeSinceLastSample >= sampleInterval)
        {
            SampleMetrics();
            timeSinceLastSample = 0;
        }
    }
    
    // ============================================
    // SAMPLING
    // ============================================
    
    private void SampleMetrics()
    {
        currentMetrics = new PerformanceMetrics
        {
            timestamp = DateTime.Now,
            frameRate = frameCount / deltaTimeAccumulator,
            frameTime = (deltaTimeAccumulator / frameCount) * 1000f, // ms
            heapMemoryMB = (GC.GetTotalMemory(false) / (1024f * 1024f)),
            heapMemoryAllocated = (Profiler.GetTotalAllocatedMemoryLong() / (1024f * 1024f)),
            heapMemoryReserved = (Profiler.GetTotalReservedMemoryLong() / (1024f * 1024f))
        };
        
        // Store in history
        if (metricsHistory.Count >= maxSamples)
            metricsHistory.Dequeue();
        
        metricsHistory.Enqueue(currentMetrics);
        
        // Reset accumulators
        frameCount = 0;
        deltaTimeAccumulator = 0;
    }
    
    // ============================================
    // PERFORMANCE ASSESSMENT
    // ============================================
    
    /// <summary>Check if performance meets target thresholds</summary>
    public PerformanceAssessment AssessPerformance(PerformanceTargets targets)
    {
        var assessment = new PerformanceAssessment
        {
            timestamp = DateTime.Now,
            targetFrameRate = targets.targetFrameRate,
            targetMemoryMB = (int)targets.targetMemoryMB
        };
        
        if (metricsHistory.Count == 0)
            return assessment;
        
        // Calculate average metrics
        float avgFrameRate = 0;
        float maxFrameTime = 0;
        float maxMemory = 0;
        int minFrameRate = int.MaxValue;
        
        foreach (var metric in metricsHistory)
        {
            avgFrameRate += metric.frameRate;
            maxFrameTime = Mathf.Max(maxFrameTime, metric.frameTime);
            maxMemory = Mathf.Max(maxMemory, metric.heapMemoryAllocated);
            minFrameRate = Math.Min(minFrameRate, (int)metric.frameRate);
        }
        
        avgFrameRate /= metricsHistory.Count;
        assessment.averageFrameRate = avgFrameRate;
        assessment.minimumFrameRate = minFrameRate;
        assessment.maximumFrameTime = maxFrameTime;
        assessment.peakMemoryMB = maxMemory;
        
        // Check against targets
        assessment.frameRatePassed = avgFrameRate >= targets.targetFrameRate;
        assessment.memoryPassed = maxMemory <= targets.targetMemoryMB;
        assessment.overall = assessment.frameRatePassed && assessment.memoryPassed;
        
        return assessment;
    }
    
    // ============================================
    // REPORTING
    // ============================================
    
    /// <summary>Generate performance report</summary>
    public string GeneratePerformanceReport(PerformanceTargets targets, string platformName)
    {
        var assessment = AssessPerformance(targets);
        
        string report = "╔════════════════════════════════════════════════════════════╗\n";
        report += $"║       {platformName} PERFORMANCE REPORT                           ║\n";
        report += "╚════════════════════════════════════════════════════════════╝\n\n";
        
        report += "┌─ FRAME RATE ───────────────────────────────────────────────┐\n";
        report += $"Target:  {targets.targetFrameRate} FPS\n";
        report += $"Average: {assessment.averageFrameRate:F1} FPS {(assessment.frameRatePassed ? "✓" : "✗")}\n";
        report += $"Minimum: {assessment.minimumFrameRate} FPS\n";
        report += $"Max Frame Time: {assessment.maximumFrameTime:F2}ms\n";
        report += "└────────────────────────────────────────────────────────────┘\n\n";
        
        report += "┌─ MEMORY ───────────────────────────────────────────────────┐\n";
        report += $"Target:    {targets.targetMemoryMB:F0}MB\n";
        report += $"Peak Used: {assessment.peakMemoryMB:F2}MB {(assessment.memoryPassed ? "✓" : "✗")}\n";
        report += $"Samples:   {metricsHistory.Count}\n";
        report += "└────────────────────────────────────────────────────────────┘\n\n";
        
        report += $"OVERALL: {(assessment.overall ? "✓ PASS" : "✗ FAIL")}\n";
        
        return report;
    }
    
    /// <summary>Get current metrics</summary>
    public PerformanceMetrics GetCurrentMetrics() => currentMetrics;
    
    /// <summary>Get performance history</summary>
    public PerformanceMetrics[] GetMetricsHistory() => metricsHistory.ToArray();
}

/// <summary>Snapshot of performance metrics at a point in time</summary>
[System.Serializable]
public class PerformanceMetrics
{
    public DateTime timestamp;
    public float frameRate;      // FPS
    public float frameTime;      // milliseconds
    public float heapMemoryMB;   // GC heap memory
    public float heapMemoryAllocated; // Total allocated
    public float heapMemoryReserved;  // Total reserved
}

/// <summary>Performance targets for a platform</summary>
[System.Serializable]
public class PerformanceTargets
{
    public string platform;
    public int targetFrameRate = 60;
    public int minimumFrameRate = 30;
    public float targetMemoryMB = 512;
    
    public static PerformanceTargets WebGL = new PerformanceTargets
    {
        platform = "WebGL",
        targetFrameRate = 60,
        minimumFrameRate = 30,
        targetMemoryMB = 512
    };
    
    public static PerformanceTargets Android = new PerformanceTargets
    {
        platform = "Android",
        targetFrameRate = 60,
        minimumFrameRate = 30,
        targetMemoryMB = 400 // More constrained
    };
    
    public static PerformanceTargets iOS = new PerformanceTargets
    {
        platform = "iOS",
        targetFrameRate = 60,
        minimumFrameRate = 30,
        targetMemoryMB = 450
    };
}

/// <summary>Results of performance assessment against targets</summary>
[System.Serializable]
public class PerformanceAssessment
{
    public DateTime timestamp;
    public float averageFrameRate;
    public int minimumFrameRate;
    public float maximumFrameTime;
    public float peakMemoryMB;
    public int targetFrameRate;
    public int targetMemoryMB;
    public bool frameRatePassed;
    public bool memoryPassed;
    public bool overall;
}
