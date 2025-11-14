using UnityEngine;

/// <summary>
/// BoardLayoutConfiguration - Data-driven configuration for board layout.
/// 
/// Responsibilities:
/// - Define board dimensions and cell positions
/// - Configure visual styling (colors, sizes)
/// - Manage board variations (different layouts for different devices)
/// - Provide reusable configuration for multiple board instances
/// 
/// Architecture:
/// - Scriptable object or component that holds configuration
/// - Referenced by BoardGridManager during initialization
/// - Allows non-programmers to adjust board appearance
/// - Supports multiple board layouts/themes
/// </summary>
public class BoardLayoutConfiguration : MonoBehaviour
{
    // ============================================
    // BOARD DIMENSIONS
    // ============================================
    
    [SerializeField]
    [Tooltip("Number of cells on the board (typically 12)")]
    private int cellCount = 12;
    
    [SerializeField]
    [Tooltip("Layout type: Circular, Linear, Grid")]
    private BoardLayoutType layoutType = BoardLayoutType.Circular;
    
    // ============================================
    // CIRCULAR LAYOUT PROPERTIES
    // ============================================
    
    [SerializeField]
    [Tooltip("Center position of the board")]
    private Vector2 boardCenterPosition = Vector2.zero;
    
    [SerializeField]
    [Tooltip("Radius of circular layout (distance from center to cells)")]
    private float cellRadius = 3f;
    
    [SerializeField]
    [Tooltip("Size of each cell")]
    private float cellSize = 0.8f;
    
    [SerializeField]
    [Tooltip("Rotation offset in degrees")]
    private float rotationOffset = 90f;
    
    // ============================================
    // VISUAL STYLING
    // ============================================
    
    [SerializeField]
    [Tooltip("Color for empty cells")]
    private Color emptyColor = Color.white;
    
    [SerializeField]
    [Tooltip("Color for occupied cells")]
    private Color occupiedColor = Color.gray;
    
    [SerializeField]
    [Tooltip("Color for highlighted cells (valid moves)")]
    private Color highlightColor = Color.green;
    
    [SerializeField]
    [Tooltip("Color for player 1 chips")]
    private Color player1Color = new Color(1, 0.5f, 0.5f); // Red
    
    [SerializeField]
    [Tooltip("Color for player 2 chips")]
    private Color player2Color = new Color(0.5f, 0.5f, 1); // Blue
    
    // ============================================
    // ANIMATION PROPERTIES
    // ============================================
    
    [SerializeField]
    [Tooltip("Duration of chip placement animation (seconds)")]
    private float chipPlacementDuration = 0.5f;
    
    [SerializeField]
    [Tooltip("Duration of chip bump animation (seconds)")]
    private float chipBumpDuration = 0.3f;
    
    [SerializeField]
    [Tooltip("Scale of chip at end of placement")]
    private float chipFinalScale = 1f;
    
    // ============================================
    // RESPONSIVE DESIGN
    // ============================================
    
    [SerializeField]
    [Tooltip("Adjust layout based on screen size")]
    private bool enableResponsiveLayout = true;
    
    [SerializeField]
    [Tooltip("Scale factor for mobile devices")]
    private float mobileScaleFactor = 0.8f;
    
    [SerializeField]
    [Tooltip("Scale factor for tablets")]
    private float tabletScaleFactor = 1f;
    
    [SerializeField]
    [Tooltip("Scale factor for desktop")]
    private float desktopScaleFactor = 1.2f;
    
    // ============================================
    // INTERACTION PROPERTIES
    // ============================================
    
    [SerializeField]
    [Tooltip("Minimum distance to cell to trigger hover")]
    private float hoverThreshold = 0.1f;
    
    [SerializeField]
    [Tooltip("Time before hover highlight appears (seconds)")]
    private float hoverDelay = 0.1f;
    
    // ============================================
    // PUBLIC PROPERTIES
    // ============================================
    
    public int CellCount => cellCount;
    public BoardLayoutType LayoutType => layoutType;
    public Vector2 BoardCenterPosition => boardCenterPosition;
    public float CellRadius => cellRadius;
    public float CellSize => cellSize;
    public float RotationOffset => rotationOffset;
    
    public Color EmptyColor => emptyColor;
    public Color OccupiedColor => occupiedColor;
    public Color HighlightColor => highlightColor;
    public Color Player1Color => player1Color;
    public Color Player2Color => player2Color;
    
    public float ChipPlacementDuration => chipPlacementDuration;
    public float ChipBumpDuration => chipBumpDuration;
    public float ChipFinalScale => chipFinalScale;
    
    public bool EnableResponsiveLayout => enableResponsiveLayout;
    public float MobileScaleFactor => mobileScaleFactor;
    public float TabletScaleFactor => tabletScaleFactor;
    public float DesktopScaleFactor => desktopScaleFactor;
    
    public float HoverThreshold => hoverThreshold;
    public float HoverDelay => hoverDelay;
    
    // ============================================
    // PUBLIC METHODS
    // ============================================
    
    /// <summary>Get position of a cell in the board layout</summary>
    public Vector3 GetCellPosition(int cellIndex)
    {
        if (cellIndex < 0 || cellIndex >= cellCount)
        {
            Debug.LogWarning($"[BoardLayoutConfiguration] Invalid cell index: {cellIndex}");
            return Vector3.zero;
        }
        
        switch (layoutType)
        {
            case BoardLayoutType.Circular:
                return GetCircularLayoutPosition(cellIndex);
            
            case BoardLayoutType.Linear:
                return GetLinearLayoutPosition(cellIndex);
            
            case BoardLayoutType.Grid:
                return GetGridLayoutPosition(cellIndex);
            
            default:
                Debug.LogWarning($"[BoardLayoutConfiguration] Unknown layout type: {layoutType}");
                return Vector3.zero;
        }
    }
    
    /// <summary>Get color for a player's chips</summary>
    public Color GetPlayerColor(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                return player1Color;
            case 1:
                return player2Color;
            default:
                Debug.LogWarning($"[BoardLayoutConfiguration] Unknown player index: {playerIndex}");
                return Color.white;
        }
    }
    
    /// <summary>Get responsive scale factor based on screen size</summary>
    public float GetResponsiveScaleFactor()
    {
        if (!enableResponsiveLayout)
            return 1f;
        
        // Detect device type and return appropriate scale
        #if UNITY_ANDROID || UNITY_IOS
        // Mobile detection
        float screenDiagonal = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height);
        if (screenDiagonal > 2000) // Tablet-sized
            return tabletScaleFactor;
        else
            return mobileScaleFactor;
        #else
        return desktopScaleFactor;
        #endif
    }
    
    // ============================================
    // PRIVATE METHODS
    // ============================================
    
    /// <summary>Calculate position for circular layout</summary>
    private Vector3 GetCircularLayoutPosition(int cellIndex)
    {
        // Calculate angle for this cell
        float angle = (cellIndex / (float)cellCount) * 360f + rotationOffset;
        float radians = angle * Mathf.Deg2Rad;
        
        // Calculate position on circle
        float x = boardCenterPosition.x + Mathf.Cos(radians) * cellRadius;
        float y = boardCenterPosition.y + Mathf.Sin(radians) * cellRadius;
        
        return new Vector3(x, y, 0);
    }
    
    /// <summary>Calculate position for linear layout</summary>
    private Vector3 GetLinearLayoutPosition(int cellIndex)
    {
        // Arrange cells in a line
        float spacing = cellSize * 1.2f; // 20% padding between cells
        float totalWidth = (cellCount - 1) * spacing;
        float startX = boardCenterPosition.x - totalWidth / 2f;
        
        float x = startX + (cellIndex * spacing);
        float y = boardCenterPosition.y;
        
        return new Vector3(x, y, 0);
    }
    
    /// <summary>Calculate position for grid layout (3x4 grid)</summary>
    private Vector3 GetGridLayoutPosition(int cellIndex)
    {
        // Arrange in 3 rows x 4 columns
        int cols = 4;
        int row = cellIndex / cols;
        int col = cellIndex % cols;
        
        float spacing = cellSize * 1.2f;
        float startX = boardCenterPosition.x - (cols - 1) * spacing / 2f;
        float startY = boardCenterPosition.y + 1.2f * spacing;
        
        float x = startX + (col * spacing);
        float y = startY - (row * spacing);
        
        return new Vector3(x, y, 0);
    }
}

/// <summary>Layout type for the board</summary>
public enum BoardLayoutType
{
    Circular = 0,  // Cells arranged in a circle
    Linear = 1,    // Cells arranged in a line
    Grid = 2       // Cells arranged in a grid
}
