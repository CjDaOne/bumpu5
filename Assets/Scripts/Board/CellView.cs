using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// CellView - Individual cell representation on the board.
/// 
/// Responsibilities:
/// - Display cell state visually
/// - Handle user interaction (clicks, hover)
/// - Manage visual states (empty, occupied, highlighted, selected)
/// - Animate state transitions
/// - Communicate with BoardGridManager
/// 
/// Visual States:
/// - Empty: No chip, normal appearance
/// - Occupied: Shows player chip in player color
/// - Highlighted: Shows valid move target (bright highlight)
/// - Selected: Shows currently selected target (border/glow)
/// </summary>
public class CellView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField]
    private Image cellBackground;
    
    [SerializeField]
    private Image occupantIndicator;
    
    [SerializeField]
    private Text chipCountDisplay;
    
    [SerializeField]
    private Button clickArea;
    
    [SerializeField]
    private CanvasGroup canvasGroup;
    
    [SerializeField]
    private Color colorEmpty = Color.gray;
    
    [SerializeField]
    private Color colorPlayer1 = Color.red;
    
    [SerializeField]
    private Color colorPlayer2 = Color.blue;
    
    [SerializeField]
    private Color colorPlayer3 = Color.green;
    
    [SerializeField]
    private Color colorPlayer4 = Color.yellow;
    
    [SerializeField]
    private Color colorHighlight = Color.white;
    
    [SerializeField]
    private Color colorSelected = new Color(1f, 1f, 0f);
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private int cellIndex = -1;
    private CellState currentState = CellState.Empty;
    private Player occupant = null;
    private bool isHighlighted = false;
    private bool isSelected = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    public event System.Action<CellView> OnClicked;
    public event System.Action<CellView> OnHovered;
    public event System.Action<CellView> OnExited;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public int CellIndex => cellIndex;
    public CellState State => currentState;
    public bool IsOccupied => currentState == CellState.Occupied;
    public Player Occupant => occupant;
    public bool IsHighlighted => isHighlighted;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    private void Start()
    {
        // Setup click handler if not already setup
        if (clickArea != null)
        {
            clickArea.onClick.AddListener(() => OnPointerClick(null));
        }
    }
    
    // ============================================
    // INITIALIZATION
    // ============================================
    
    /// <summary>Initialize this cell with its board index</summary>
    public void Initialize(int index)
    {
        cellIndex = index;
        currentState = CellState.Empty;
        occupant = null;
        isHighlighted = false;
        isSelected = false;
        
        // Create UI components if not assigned
        if (cellBackground == null)
            cellBackground = GetComponent<Image>();
        
        if (occupantIndicator == null)
            occupantIndicator = GetComponentInChildren<Image>(true);
        
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        
        UpdateDisplay();
        Debug.Log($"CellView {cellIndex} initialized");
    }
    
    // ============================================
    // STATE MANAGEMENT
    // ============================================
    
    /// <summary>Set cell to empty state</summary>
    public void SetEmpty()
    {
        if (currentState == CellState.Empty)
            return;
        
        currentState = CellState.Empty;
        occupant = null;
        UpdateDisplay();
    }
    
    /// <summary>Set cell to occupied state with player chip</summary>
    public void SetOccupied(Player player)
    {
        if (player == null)
        {
            SetEmpty();
            return;
        }
        
        currentState = CellState.Occupied;
        occupant = player;
        UpdateDisplay();
    }
    
    /// <summary>Set highlight state (valid move target)</summary>
    public void SetHighlighted(bool highlighted)
    {
        if (isHighlighted == highlighted)
            return;
        
        isHighlighted = highlighted;
        UpdateDisplay();
    }
    
    /// <summary>Set selected state (current target)</summary>
    public void SetSelected(bool selected)
    {
        if (isSelected == selected)
            return;
        
        isSelected = selected;
        UpdateDisplay();
    }
    
    // ============================================
    // VISUAL UPDATES
    // ============================================
    
    /// <summary>Update all visual elements based on current state</summary>
    private void UpdateDisplay()
    {
        UpdateColor();
        UpdateChipDisplay();
    }
    
    /// <summary>Update cell background color based on state</summary>
    private void UpdateColor()
    {
        if (cellBackground == null)
            return;
        
        Color targetColor = colorEmpty;
        
        if (isSelected)
        {
            targetColor = colorSelected;
        }
        else if (isHighlighted)
        {
            targetColor = colorHighlight;
        }
        else if (currentState == CellState.Occupied && occupant != null)
        {
            targetColor = GetPlayerColor(occupant.PlayerNumber);
        }
        else
        {
            targetColor = colorEmpty;
        }
        
        cellBackground.color = targetColor;
    }
    
    /// <summary>Update occupant indicator display</summary>
    private void UpdateChipDisplay()
    {
        if (occupantIndicator != null)
        {
            occupantIndicator.gameObject.SetActive(currentState == CellState.Occupied);
            
            if (occupant != null)
            {
                occupantIndicator.color = GetPlayerColor(occupant.PlayerNumber);
            }
        }
        
        if (chipCountDisplay != null)
        {
            if (currentState == CellState.Occupied && occupant != null)
            {
                chipCountDisplay.text = "1"; // TODO: Support multi-chip cells if needed
                chipCountDisplay.gameObject.SetActive(true);
            }
            else
            {
                chipCountDisplay.gameObject.SetActive(false);
            }
        }
    }
    
    /// <summary>Get color for player number</summary>
    private Color GetPlayerColor(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1: return colorPlayer1;
            case 2: return colorPlayer2;
            case 3: return colorPlayer3;
            case 4: return colorPlayer4;
            default: return Color.white;
        }
    }
    
    // ============================================
    // ANIMATIONS
    // ============================================
    
    /// <summary>Animate placement of chip on this cell</summary>
    public void AnimatePlacement()
    {
        // Simple fade-in animation
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            LeanTween.alphaCanvas(canvasGroup, 1f, 0.3f);
        }
    }
    
    /// <summary>Animate chip being bumped from this cell</summary>
    public void AnimateBump()
    {
        // Scale pop and fade out
        if (occupantIndicator != null)
        {
            LeanTween.scale(occupantIndicator.gameObject, Vector3.one * 1.2f, 0.1f)
                .setEase(LeanTweenType.easeOutQuad)
                .setOnComplete(() =>
                {
                    LeanTween.alphaCanvas(occupantIndicator.GetComponent<CanvasGroup>() ?? occupantIndicator.gameObject.AddComponent<CanvasGroup>(), 0f, 0.2f);
                });
        }
    }
    
    /// <summary>Animate cell selection</summary>
    public void AnimateSelection()
    {
        // Pulse effect
        if (cellBackground != null)
        {
            LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.1f)
                .setEase(LeanTweenType.easeOutQuad)
                .setLoopCount(2)
                .setLoopType(LeanTweenType.pingPong);
        }
    }
    
    // ============================================
    // INTERACTION
    // ============================================
    
    /// <summary>Handle pointer click event</summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke(this);
        AnimateSelection();
    }
    
    /// <summary>Handle pointer enter (hover) event</summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHovered?.Invoke(this);
    }
    
    /// <summary>Handle pointer exit (hover end) event</summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        OnExited?.Invoke(this);
    }
}

/// <summary>Cell visual state enumeration</summary>
public enum CellState
{
    Empty,      // No chip
    Occupied,   // Has player chip
    Highlighted, // Valid move target
    Selected    // Currently selected
}
