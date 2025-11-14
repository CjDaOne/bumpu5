using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// NotificationController - Manages temporary notification messages.
/// 
/// Responsibilities:
/// - Display temporary notifications (2-3 seconds)
/// - Auto-dismiss notifications
/// - Support different notification types (info, success, error)
/// - Fade in/out animations
/// </summary>
public class NotificationController : MonoBehaviour
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField]
    private GameObject notificationPrefab;
    
    [SerializeField]
    private Transform notificationContainer;
    
    [SerializeField]
    private float defaultDuration = 3f;
    
    [SerializeField]
    private float fadeDuration = 0.3f;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private GameObject currentNotification;
    private CanvasGroup canvasGroup;
    private Text notificationText;
    private Coroutine dismissCoroutine;
    private bool isInitialized = false;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize notification controller</summary>
    public void Initialize(GameObject prefab)
    {
        notificationPrefab = prefab;
        isInitialized = true;
        Debug.Log("NotificationController initialized");
    }
    
    // ============================================
    // NOTIFICATION DISPLAY
    // ============================================
    
    /// <summary>Show a standard notification</summary>
    public void ShowNotification(string message, float duration = 0)
    {
        if (duration == 0)
            duration = defaultDuration;
        
        ShowNotificationInternal(message, Color.white, duration);
    }
    
    /// <summary>Show a success notification (green)</summary>
    public void ShowSuccess(string message, float duration = 0)
    {
        if (duration == 0)
            duration = defaultDuration;
        
        ShowNotificationInternal(message, Color.green, duration);
    }
    
    /// <summary>Show an error notification (red)</summary>
    public void ShowError(string message, float duration = 0)
    {
        if (duration == 0)
            duration = defaultDuration;
        
        ShowNotificationInternal(message, Color.red, duration);
    }
    
    /// <summary>Show a warning notification (yellow)</summary>
    public void ShowWarning(string message, float duration = 0)
    {
        if (duration == 0)
            duration = defaultDuration;
        
        ShowNotificationInternal(message, new Color(1, 1, 0), duration);
    }
    
    /// <summary>Internal method to display notification</summary>
    private void ShowNotificationInternal(string message, Color color, float duration)
    {
        // Cancel any existing dismissal
        if (dismissCoroutine != null)
            StopCoroutine(dismissCoroutine);
        
        // Destroy previous notification if any
        if (currentNotification != null)
            Destroy(currentNotification);
        
        // Create notification
        if (notificationPrefab == null)
        {
            Debug.LogError("NotificationController: notification prefab is null");
            return;
        }
        
        currentNotification = Instantiate(notificationPrefab, notificationContainer);
        
        // Setup text
        notificationText = currentNotification.GetComponent<Text>();
        if (notificationText == null)
            notificationText = currentNotification.GetComponentInChildren<Text>();
        
        if (notificationText != null)
        {
            notificationText.text = message;
            notificationText.color = color;
        }
        
        // Setup canvas group for fade
        canvasGroup = currentNotification.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = currentNotification.AddComponent<CanvasGroup>();
        
        canvasGroup.alpha = 0;
        
        // Fade in
        StartCoroutine(FadeIn(fadeDuration));
        
        // Schedule dismissal
        dismissCoroutine = StartCoroutine(DismissAfterDelay(duration));
    }
    
    // ============================================
    // ANIMATIONS
    // ============================================
    
    private IEnumerator FadeIn(float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
    
    private IEnumerator FadeOut(float duration)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsed / duration));
            yield return null;
        }
        canvasGroup.alpha = 0;
    }
    
    private IEnumerator DismissAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (currentNotification != null)
        {
            yield return StartCoroutine(FadeOut(fadeDuration));
            Destroy(currentNotification);
            currentNotification = null;
        }
    }
    
    // ============================================
    // PUBLIC INTERFACE
    // ============================================
    
    /// <summary>Clear any active notification immediately</summary>
    public void Clear()
    {
        if (dismissCoroutine != null)
            StopCoroutine(dismissCoroutine);
        
        if (currentNotification != null)
            Destroy(currentNotification);
        
        currentNotification = null;
    }
}
