using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ModalController - Manages modal dialogs (win screen, error dialogs, etc.)
/// 
/// Responsibilities:
/// - Display modal overlays with dimming
/// - Show win/loss screens
/// - Handle modal button interactions
/// - Support custom modal content
/// </summary>
public class ModalController : MonoBehaviour
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField]
    private GameObject modalOverlayPrefab;
    
    [SerializeField]
    private float fadeInDuration = 0.3f;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private GameObject currentModal;
    private Image dimBackground;
    private Text modalTitle;
    private Text modalMessage;
    private Button primaryButton;
    private Button secondaryButton;
    private CanvasGroup canvasGroup;
    private bool isInitialized = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    public event System.Action<int> OnModalButtonClicked;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize modal controller</summary>
    public void Initialize(GameObject prefab)
    {
        modalOverlayPrefab = prefab;
        isInitialized = true;
        Debug.Log("ModalController initialized");
    }
    
    // ============================================
    // MODAL DISPLAY
    // ============================================
    
    /// <summary>Show win modal</summary>
    public void ShowWinModal(string title, string message, Player winner)
    {
        ShowModal(title, message, "Play Again", "Main Menu", 
            () => OnWinModalButton(0, winner),
            () => OnWinModalButton(1, winner));
    }
    
    /// <summary>Show loss modal</summary>
    public void ShowLossModal(string title, string message)
    {
        ShowModal(title, message, "Play Again", "Main Menu",
            () => { Debug.Log("Play Again clicked"); },
            () => { Debug.Log("Main Menu clicked"); });
    }
    
    /// <summary>Show error modal</summary>
    public void ShowErrorModal(string title, string message)
    {
        ShowModal(title, message, "OK", null,
            () => HideModal(),
            null);
    }
    
    /// <summary>Generic modal display</summary>
    public void ShowModal(string title, string message, string button1Text, string button2Text,
        System.Action button1Action, System.Action button2Action)
    {
        // Close any existing modal
        if (currentModal != null)
            HideModal();
        
        // Create modal from prefab
        if (modalOverlayPrefab == null)
            modalOverlayPrefab = CreateDefaultModalPrefab();
        
        currentModal = Instantiate(modalOverlayPrefab);
        
        // Setup modal content
        SetupModalContent(title, message, button1Text, button2Text, button1Action, button2Action);
        
        // Animate fade in
        canvasGroup = currentModal.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = currentModal.AddComponent<CanvasGroup>();
        
        canvasGroup.alpha = 0;
        LeanTween.alphaCanvas(canvasGroup, 1, fadeInDuration);
    }
    
    /// <summary>Setup modal content and buttons</summary>
    private void SetupModalContent(string title, string message, string button1Text, string button2Text,
        System.Action button1Action, System.Action button2Action)
    {
        // Find or create title text
        modalTitle = FindOrCreateText(currentModal, "Title");
        if (modalTitle != null)
            modalTitle.text = title;
        
        // Find or create message text
        modalMessage = FindOrCreateText(currentModal, "Message");
        if (modalMessage != null)
            modalMessage.text = message;
        
        // Find or create buttons
        primaryButton = FindOrCreateButton(currentModal, "Button1", button1Text, button1Action);
        
        if (!string.IsNullOrEmpty(button2Text))
        {
            secondaryButton = FindOrCreateButton(currentModal, "Button2", button2Text, button2Action);
        }
    }
    
    /// <summary>Hide the current modal</summary>
    public void HideModal()
    {
        if (currentModal == null)
            return;
        
        if (canvasGroup != null)
        {
            LeanTween.alphaCanvas(canvasGroup, 0, fadeInDuration)
                .setOnComplete(() =>
                {
                    if (currentModal != null)
                        Destroy(currentModal);
                    currentModal = null;
                });
        }
        else
        {
            Destroy(currentModal);
            currentModal = null;
        }
    }
    
    // ============================================
    // BUTTON HANDLERS
    // ============================================
    
    private void OnWinModalButton(int buttonIndex, Player winner)
    {
        HideModal();
        
        switch (buttonIndex)
        {
            case 0:
                Debug.Log("Play Again clicked");
                // TODO: Restart game
                break;
            case 1:
                Debug.Log("Main Menu clicked");
                // TODO: Return to main menu
                break;
        }
    }
    
    // ============================================
    // UTILITY
    // ============================================
    
    private Text FindOrCreateText(GameObject parent, string name)
    {
        Transform existing = parent.transform.Find(name);
        if (existing != null)
            return existing.GetComponent<Text>();
        
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent.transform, false);
        return textObj.AddComponent<Text>();
    }
    
    private Button FindOrCreateButton(GameObject parent, string name, string text, System.Action onClick)
    {
        Transform existing = parent.transform.Find(name);
        Button button = null;
        
        if (existing != null)
        {
            button = existing.GetComponent<Button>();
        }
        else
        {
            GameObject btnObj = new GameObject(name);
            btnObj.transform.SetParent(parent.transform, false);
            button = btnObj.AddComponent<Button>();
            
            Text btnText = btnObj.AddComponent<Text>();
            btnText.text = text;
        }
        
        if (button != null && onClick != null)
        {
            button.onClick.AddListener(() => onClick.Invoke());
        }
        
        return button;
    }
    
    private GameObject CreateDefaultModalPrefab()
    {
        GameObject modal = new GameObject("ModalOverlay");
        
        // Add background image
        Image bgImage = modal.AddComponent<Image>();
        bgImage.color = new Color(0, 0, 0, 0.5f);
        
        // Add panel content
        GameObject panel = new GameObject("Content");
        panel.transform.SetParent(modal.transform, false);
        panel.AddComponent<Image>().color = Color.white;
        
        return modal;
    }
}
