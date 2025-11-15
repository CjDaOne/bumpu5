using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// PauseMenuController - Manages the pause menu overlay.
/// 
/// Responsibilities:
/// - Show/hide pause menu
/// - Handle pause menu button interactions
/// - Coordinate with GameStateManager for pause/resume
/// - Manage pause menu UI state
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField]
    private Button pauseButton;
    
    [SerializeField]
    private GameObject pauseMenuPrefab;
    
    [SerializeField]
    private float fadeInDuration = 0.3f;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private GameStateManager gameStateManager;
    private GameObject pauseMenuPanel;
    private bool isInitialized = false;
    private bool isPaused = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    public event System.Action<bool> OnPauseStateChanged;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInitialized => isInitialized;
    public bool IsPaused => isPaused;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize pause menu controller</summary>
    public void Initialize(GameStateManager stateManager, Button pauseBtn)
    {
        gameStateManager = stateManager;
        pauseButton = pauseBtn;
        
        if (pauseButton != null)
            pauseButton.onClick.AddListener(OnPauseButtonClicked);
        
        isInitialized = true;
        Debug.Log("PauseMenuController initialized");
    }
    
    // ============================================
    // PAUSE MENU CONTROL
    // ============================================
    
    private void OnPauseButtonClicked()
    {
        if (isPaused)
            HidePauseMenu();
        else
            ShowPauseMenu();
    }
    
    /// <summary>Show the pause menu</summary>
    public void ShowPauseMenu()
    {
        if (isPaused)
            return;
        
        Debug.Log("[PauseMenuController] Showing pause menu");
        
        // Create pause menu if not exists
        if (pauseMenuPanel == null)
        {
            pauseMenuPanel = CreatePauseMenuPanel();
        }
        
        pauseMenuPanel.SetActive(true);
        isPaused = true;
        
        // Notify listeners
        OnPauseStateChanged?.Invoke(true);
        
        // Pause game time
        if (gameStateManager != null)
            Time.timeScale = 0;
    }
    
    /// <summary>Hide the pause menu and resume game</summary>
    public void HidePauseMenu()
    {
        if (!isPaused)
            return;
        
        Debug.Log("[PauseMenuController] Hiding pause menu");
        
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);
        
        isPaused = false;
        
        // Notify listeners
        OnPauseStateChanged?.Invoke(false);
        
        // Resume game time
        Time.timeScale = 1;
    }
    
    /// <summary>Create pause menu panel</summary>
    private GameObject CreatePauseMenuPanel()
    {
        if (pauseMenuPrefab != null)
            return Instantiate(pauseMenuPrefab);
        
        // Create default pause menu
        GameObject panel = new GameObject("PauseMenuPanel");
        panel.transform.SetAsLastSibling();
        
        // Background overlay
        Image bgImage = panel.AddComponent<Image>();
        bgImage.color = new Color(0, 0, 0, 0.7f);
        
        // Content panel
        GameObject content = new GameObject("Content");
        content.transform.SetParent(panel.transform, false);
        Image contentBg = content.AddComponent<Image>();
        contentBg.color = Color.white;
        
        // Unity 6.0: VerticalLayoutGroup deprecated - use GridLayoutGroup with single column
        GridLayoutGroup layoutGroup = content.AddComponent<GridLayoutGroup>();
        layoutGroup.spacing = new Vector2(0, 12);
        layoutGroup.padding = new RectOffset(20, 20, 20, 20);
        layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        layoutGroup.constraintCount = 1;
        
        // Title
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(content.transform, false);
        TextMeshProUGUI titleText = titleObj.AddComponent<TextMeshProUGUI>();
        titleText.text = "Game Paused";
        titleText.fontSize = 28;
        titleText.fontStyle = FontStyles.Bold;
        titleText.alignment = TextAlignmentOptions.Center;
        
        // Resume button
        CreatePauseMenuButton(content, "Resume", () => HidePauseMenu());
        
        // Settings button
        CreatePauseMenuButton(content, "Settings", () => Debug.Log("Settings clicked"));
        
        // Rules button
        CreatePauseMenuButton(content, "Rules", () => Debug.Log("Rules clicked"));
        
        // Quit button
        CreatePauseMenuButton(content, "Quit to Menu", () => 
        {
            Time.timeScale = 1; // Resume time before quitting
            if (gameStateManager != null)
                gameStateManager.ReturnToMenu();
        });
        
        return panel;
    }
    
    private void CreatePauseMenuButton(GameObject parent, string text, System.Action onClick)
    {
        GameObject btnObj = new GameObject(text);
        btnObj.transform.SetParent(parent.transform, false);
        
        LayoutElement layoutElement = btnObj.AddComponent<LayoutElement>();
        layoutElement.preferredHeight = 48;
        layoutElement.preferredWidth = 200;
        
        Image btnImage = btnObj.AddComponent<Image>();
        btnImage.color = new Color(0.2f, 0.5f, 1f); // Blue
        
        Button button = btnObj.AddComponent<Button>();
        button.targetGraphic = btnImage;
        button.onClick.AddListener(() => onClick.Invoke());
        
        TextMeshProUGUI btnText = btnObj.AddComponent<TextMeshProUGUI>();
        btnText.text = text;
        btnText.fontSize = 16;
        btnText.fontStyle = FontStyles.Bold;
        btnText.color = Color.white;
        btnText.alignment = TextAlignmentOptions.Center;
    }
    
    // ============================================
    // PUBLIC INTERFACE
    // ============================================
    
    /// <summary>Toggle pause state</summary>
    public void TogglePause()
    {
        if (isPaused)
            HidePauseMenu();
        else
            ShowPauseMenu();
    }
}
