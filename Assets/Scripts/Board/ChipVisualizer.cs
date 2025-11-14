using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// ChipVisualizer - Renders chips on the board and manages chip animations.
/// 
/// Responsibilities:
/// - Create chip visual GameObjects
/// - Manage chip appearance (color, size, etc.)
/// - Play placement animations (drop from above)
/// - Play bump animations (bounce and disappear)
/// - Track which cells have chips
/// 
/// The visualizer creates lightweight chip objects that display on board cells
/// and animate when chips are placed or bumped.
/// </summary>
public class ChipVisualizer : MonoBehaviour
{
    // ============================================
    // INSPECTOR PROPERTIES
    // ============================================
    
    [SerializeField]
    private GameObject chipPrefab;
    
    [SerializeField]
    private Material[] playerMaterials;
    
    [SerializeField]
    private AnimationCurve placementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    [SerializeField]
    private AnimationCurve bumpCurve = AnimationCurve.Linear(0, 0, 1, 1);
    
    [SerializeField]
    private float placementDuration = 0.3f;
    
    [SerializeField]
    private float bumpDuration = 0.4f;
    
    [SerializeField]
    private float bumpHeight = 1f;
    
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private Dictionary<int, GameObject> chipObjects = new Dictionary<int, GameObject>();
    private bool isInitialized = false;
    
    // ============================================
    // EVENTS
    // ============================================
    
    public event System.Action<int> OnChipPlacementComplete;
    public event System.Action<int> OnChipBumpComplete;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize the chip visualizer</summary>
    public void Initialize()
    {
        // If no prefab assigned, create a default one
        if (chipPrefab == null)
        {
            chipPrefab = CreateDefaultChipPrefab();
        }
        
        isInitialized = true;
        Debug.Log("ChipVisualizer initialized");
    }
    
    // ============================================
    // CHIP MANAGEMENT
    // ============================================
    
    /// <summary>Place a chip on a cell</summary>
    public void PlaceChip(int cellIndex, Player player)
    {
        if (player == null)
        {
            RemoveChip(cellIndex);
            return;
        }
        
        // Remove existing chip if present
        if (chipObjects.ContainsKey(cellIndex))
        {
            RemoveChip(cellIndex);
        }
        
        // Create new chip
        GameObject chipObject = Instantiate(chipPrefab, transform);
        chipObject.name = $"Chip_{cellIndex}_{player.PlayerName}";
        
        // Set position
        CellView cellView = GetCellViewAtIndex(cellIndex);
        if (cellView != null)
        {
            chipObject.transform.position = cellView.transform.position;
        }
        
        // Set appearance
        ApplyPlayerAppearance(chipObject, player.PlayerNumber);
        
        // Store reference
        chipObjects[cellIndex] = chipObject;
        
        Debug.Log($"Chip placed on cell {cellIndex}");
    }
    
    /// <summary>Remove chip from a cell</summary>
    public void RemoveChip(int cellIndex)
    {
        if (!chipObjects.ContainsKey(cellIndex))
            return;
        
        GameObject chipObject = chipObjects[cellIndex];
        chipObjects.Remove(cellIndex);
        
        if (chipObject != null)
        {
            Destroy(chipObject);
        }
    }
    
    /// <summary>Update chip appearance</summary>
    public void UpdateChipAppearance(int cellIndex, Player player)
    {
        if (!chipObjects.ContainsKey(cellIndex))
            return;
        
        GameObject chipObject = chipObjects[cellIndex];
        if (chipObject != null && player != null)
        {
            ApplyPlayerAppearance(chipObject, player.PlayerNumber);
        }
    }
    
    /// <summary>Apply player-specific appearance to chip</summary>
    private void ApplyPlayerAppearance(GameObject chipObject, int playerNumber)
    {
        Image image = chipObject.GetComponent<Image>();
        if (image != null)
        {
            image.color = GetPlayerColor(playerNumber);
        }
        
        if (playerMaterials != null && playerNumber >= 1 && playerNumber <= playerMaterials.Length)
        {
            Renderer renderer = chipObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = playerMaterials[playerNumber - 1];
            }
        }
    }
    
    /// <summary>Get color for player number</summary>
    private Color GetPlayerColor(int playerNumber)
    {
        switch (playerNumber)
        {
            case 1: return Color.red;
            case 2: return Color.blue;
            case 3: return Color.green;
            case 4: return Color.yellow;
            default: return Color.white;
        }
    }
    
    // ============================================
    // ANIMATIONS
    // ============================================
    
    /// <summary>Animate chip placement (drop from above)</summary>
    public void AnimateChipPlacement(int cellIndex)
    {
        if (!chipObjects.ContainsKey(cellIndex))
            return;
        
        GameObject chip = chipObjects[cellIndex];
        if (chip == null)
            return;
        
        // Get target position
        CellView cellView = GetCellViewAtIndex(cellIndex);
        Vector3 targetPosition = cellView != null ? cellView.transform.position : chip.transform.position;
        
        // Start from above
        Vector3 startPosition = targetPosition + Vector3.up * 2f;
        chip.transform.position = startPosition;
        
        // Animate drop
        LeanTween.move(chip, targetPosition, placementDuration)
            .setEase(LeanTweenType.easeOutBounce)
            .setOnComplete(() =>
            {
                chip.transform.position = targetPosition;
                OnChipPlacementComplete?.Invoke(cellIndex);
            });
    }
    
    /// <summary>Animate chip being bumped (bounce and fade)</summary>
    public void AnimateChipBump(int cellIndex)
    {
        if (!chipObjects.ContainsKey(cellIndex))
            return;
        
        GameObject chip = chipObjects[cellIndex];
        if (chip == null)
            return;
        
        Vector3 startPosition = chip.transform.position;
        Vector3 peakPosition = startPosition + Vector3.up * bumpHeight;
        
        // Animate arc up and down while fading
        LeanTween.move(chip, peakPosition, bumpDuration * 0.5f)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                LeanTween.move(chip, startPosition, bumpDuration * 0.5f)
                    .setEase(LeanTweenType.easeInQuad);
            });
        
        // Fade out
        CanvasGroup canvasGroup = chip.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = chip.AddComponent<CanvasGroup>();
        
        LeanTween.alphaCanvas(canvasGroup, 0f, bumpDuration)
            .setDelay(bumpDuration * 0.3f)
            .setOnComplete(() =>
            {
                RemoveChip(cellIndex);
                OnChipBumpComplete?.Invoke(cellIndex);
            });
    }
    
    // ============================================
    // UTILITY
    // ============================================
    
    /// <summary>Get chip object at cell index</summary>
    public GameObject GetChipAtCell(int cellIndex)
    {
        if (chipObjects.ContainsKey(cellIndex))
            return chipObjects[cellIndex];
        
        return null;
    }
    
    /// <summary>Clear all chips from board</summary>
    public void Clear()
    {
        List<int> cellsToRemove = new List<int>(chipObjects.Keys);
        foreach (int cellIndex in cellsToRemove)
        {
            RemoveChip(cellIndex);
        }
    }
    
    /// <summary>Get CellView at given index (helper for positioning)</summary>
    private CellView GetCellViewAtIndex(int cellIndex)
    {
        BoardGridManager boardManager = GetComponent<BoardGridManager>();
        if (boardManager != null && boardManager.Cells != null && cellIndex >= 0 && cellIndex < boardManager.Cells.Length)
        {
            return boardManager.Cells[cellIndex];
        }
        
        return null;
    }
    
    /// <summary>Create default chip prefab if none assigned</summary>
    private GameObject CreateDefaultChipPrefab()
    {
        GameObject prefab = new GameObject("DefaultChip");
        prefab.AddComponent<Image>().color = Color.red;
        
        RectTransform rect = prefab.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(80, 80);
        
        return prefab;
    }
}
