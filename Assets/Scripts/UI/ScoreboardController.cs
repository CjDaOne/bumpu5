using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// ScoreboardController - Manages the player scoreboard display.
/// 
/// Responsibilities:
/// - Display all players' scores and status
/// - Update scores in real-time
/// - Highlight current player
/// - Show win status
/// - Create/manage player row UI elements
/// </summary>
public class ScoreboardController : MonoBehaviour
{
    // ============================================
    // INTERNAL STATE
    // ============================================
    
    private GameStateManager gameStateManager;
    private Transform scoresboardParent;
    private List<PlayerScoreRow> playerRows = new List<PlayerScoreRow>();
    private bool isInitialized = false;
    
    // ============================================
    // PROPERTIES
    // ============================================
    
    public bool IsInitialized => isInitialized;
    
    // ============================================
    // LIFECYCLE
    // ============================================
    
    /// <summary>Initialize scoreboard controller</summary>
    public void Initialize(GameStateManager stateManager, Transform parent)
    {
        gameStateManager = stateManager;
        scoresboardParent = parent;
        
        if (gameStateManager == null)
        {
            Debug.LogError("ScoreboardController.Initialize: gameStateManager is null");
            return;
        }
        
        // Create player rows
        CreatePlayerRows();
        
        // Subscribe to events
        gameStateManager.OnPlayerChanged += OnPlayerChanged;
        gameStateManager.OnGameWon += OnGameWon;
        
        isInitialized = true;
        Debug.Log("ScoreboardController initialized");
    }
    
    /// <summary>Create UI rows for each player</summary>
    private void CreatePlayerRows()
    {
        if (scoresboardParent == null)
        {
            Debug.LogWarning("ScoreboardController: scoreboard parent is null, cannot create rows");
            return;
        }
        
        // For now, hardcode 2-4 players support
        // In a full implementation, this would read from game configuration
        int playerCount = 2;
        
        for (int i = 0; i < playerCount; i++)
        {
            GameObject rowObj = new GameObject($"PlayerRow_{i + 1}");
            rowObj.transform.SetParent(scoresboardParent, false);
            
            LayoutElement layoutElement = rowObj.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 40;
            
            HorizontalLayoutGroup layoutGroup = rowObj.AddComponent<HorizontalLayoutGroup>();
            layoutGroup.spacing = 8;
            layoutGroup.padding = new RectOffset(8, 8, 4, 4);
            
            PlayerScoreRow row = new PlayerScoreRow();
            row.rowObject = rowObj;
            row.playerIndex = i;
            row.CreateUI();
            
            playerRows.Add(row);
        }
    }
    
    // ============================================
    // UPDATE HANDLERS
    // ============================================
    
    /// <summary>Update current player highlight</summary>
    public void UpdateCurrentPlayerHighlight(Player currentPlayer)
    {
        if (gameStateManager == null)
            return;
        
        for (int i = 0; i < playerRows.Count; i++)
        {
            PlayerScoreRow row = playerRows[i];
            
            // Determine if this row represents the current player
            bool isCurrentPlayer = (currentPlayer != null && currentPlayer.PlayerNumber == i + 1);
            row.SetHighlighted(isCurrentPlayer);
        }
    }
    
    /// <summary>Update all scores from game state</summary>
    public void UpdateAllScores()
    {
        // Placeholder: Would update scores from player objects
        // This would be called periodically or on game state changes
    }
    
    /// <summary>Handler for game won event</summary>
    private void OnGameWon(Player winner)
    {
        if (winner == null)
            return;
        
        int winnerIndex = winner.PlayerNumber - 1;
        if (winnerIndex >= 0 && winnerIndex < playerRows.Count)
        {
            playerRows[winnerIndex].SetWon(true);
        }
    }
    
    /// <summary>Handler for player changed event</summary>
    private void OnPlayerChanged(Player newPlayer)
    {
        UpdateCurrentPlayerHighlight(newPlayer);
    }
    
    // ============================================
    // INTERNAL CLASS: PlayerScoreRow
    // ============================================
    
    private class PlayerScoreRow
    {
        public GameObject rowObject;
        public int playerIndex;
        
        private Image colorIndicator;
        private Text playerNameText;
        private Text scoreText;
        private Image currentPlayerIndicator;
        
        public void CreateUI()
        {
            // Color indicator (16x16)
            GameObject colorObj = new GameObject("ColorIndicator");
            colorObj.transform.SetParent(rowObject.transform, false);
            LayoutElement colorLayout = colorObj.AddComponent<LayoutElement>();
            colorLayout.preferredWidth = 16;
            colorLayout.preferredHeight = 16;
            colorIndicator = colorObj.AddComponent<Image>();
            colorIndicator.color = GetPlayerColor(playerIndex + 1);
            
            // Player name (variable width)
            GameObject nameObj = new GameObject("PlayerName");
            nameObj.transform.SetParent(rowObject.transform, false);
            LayoutElement nameLayout = nameObj.AddComponent<LayoutElement>();
            nameLayout.preferredWidth = 120;
            playerNameText = nameObj.AddComponent<Text>();
            playerNameText.text = $"Player {playerIndex + 1}";
            playerNameText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            playerNameText.fontSize = 14;
            
            // Score text
            GameObject scoreObj = new GameObject("Score");
            scoreObj.transform.SetParent(rowObject.transform, false);
            scoreText = scoreObj.AddComponent<Text>();
            scoreText.text = "Score: 0";
            scoreText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            scoreText.fontSize = 12;
            
            // Current player indicator
            GameObject indicatorObj = new GameObject("CurrentPlayerIndicator");
            indicatorObj.transform.SetParent(rowObject.transform, false);
            LayoutElement indicatorLayout = indicatorObj.AddComponent<LayoutElement>();
            indicatorLayout.preferredWidth = 16;
            indicatorLayout.preferredHeight = 16;
            currentPlayerIndicator = indicatorObj.AddComponent<Image>();
            currentPlayerIndicator.color = new Color(1, 1, 0); // Yellow circle
            currentPlayerIndicator.gameObject.SetActive(false);
        }
        
        public void SetHighlighted(bool highlighted)
        {
            if (currentPlayerIndicator != null)
                currentPlayerIndicator.gameObject.SetActive(highlighted);
        }
        
        public void SetWon(bool won)
        {
            // Visual feedback for winner (e.g., special color or decoration)
            if (rowObject != null)
            {
                Image bgImage = rowObject.AddComponent<Image>();
                bgImage.color = won ? new Color(1, 1, 0, 0.2f) : Color.white;
            }
        }
        
        private Color GetPlayerColor(int playerNumber)
        {
            switch (playerNumber)
            {
                case 1: return Color.red;
                case 2: return Color.blue;
                case 3: return new Color(1, 1, 0); // Yellow
                case 4: return Color.green;
                default: return Color.white;
            }
        }
    }
}
