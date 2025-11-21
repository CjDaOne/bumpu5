using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Game Mode 5: Solitary
/// 
/// Rules:
/// - Win: Fill the entire board (25 chips).
/// - Rolling a 6: Your "OPPONENT". Removes the last chip placed.
/// - Double 6s: Remove the last two chips placed.
/// - 5+6: Cancels the removal (Safe roll).
/// - Bumping: No opponent bumping (Solitary).
/// </summary>
public class Game5_Solitary : GameModeBase
{
    // Stack to track placement history for removal logic
    private Stack<int> placementHistory = new Stack<int>();

    public override string ModeName => "Solitary";
    public override string ModeDescription => "Fill the board! Rolling a 6 removes the last placed chip. Double 6s remove two.";

    public override void Initialize(GameStateManager gsm)
    {
        base.Initialize(gsm);
        
        // Subscribe to events
        if (gameStateManager != null)
        {
            gameStateManager.OnDiceRolled += HandleDiceRoll;
            // We need to track placements. 
            // GameStateManager fires OnChipPlaced event.
            // But we can also override OnChipPlaced method.
        }
        Debug.Log("[Game5_Solitary] Initialized");
    }

    public override void OnGameStart()
    {
        base.OnGameStart();
        placementHistory.Clear();
        Debug.Log("[Game5_Solitary] Game started - Watch out for 6s!");
    }
    
    public override void OnChipPlaced(Player player, int cellIndex)
    {
        base.OnChipPlaced(player, cellIndex);
        placementHistory.Push(cellIndex);
        Debug.Log($"[Game5_Solitary] Chip placed at {cellIndex}. History count: {placementHistory.Count}");
    }

    private void HandleDiceRoll(int[] roll)
    {
        if (roll == null || roll.Length != 2) return;

        int d1 = roll[0];
        int d2 = roll[1];
        
        bool hasSix = (d1 == 6 || d2 == 6);
        bool isDoubleSix = (d1 == 6 && d2 == 6);
        bool hasFive = (d1 == 5 || d2 == 5);
        
        // Rule: 5+6 cancels the bump (removal)
        if (hasSix && hasFive)
        {
            Debug.Log("[Game5_Solitary] 5+6 rolled! Removal cancelled.");
            return;
        }

        // Rule: Double 6 removes last two chips
        if (isDoubleSix)
        {
            Debug.Log("[Game5_Solitary] Double 6! Removing last 2 chips.");
            RemoveLastChip();
            RemoveLastChip();
            return;
        }

        // Rule: Single 6 removes last chip
        if (hasSix)
        {
            Debug.Log("[Game5_Solitary] Single 6! Removing last chip.");
            RemoveLastChip();
            return;
        }
    }

    private void RemoveLastChip()
    {
        if (placementHistory.Count > 0)
        {
            int lastIndex = placementHistory.Pop();
            BoardCell cell = GetCell(lastIndex);
            if (cell != null && cell.Is_Occupied)
            {
                cell.Clear();
                Debug.Log($"[Game5_Solitary] Removed chip at {lastIndex}");
            }
            else
            {
                // Chip might have been removed already? (Shouldn't happen in Solitary)
                Debug.LogWarning($"[Game5_Solitary] History pointed to {lastIndex} but it was empty.");
            }
        }
        else
        {
            Debug.Log("[Game5_Solitary] No chips to remove.");
        }
    }

    public override bool IsLoseTurnRoll(int[] roll)
    {
        if (roll == null || roll.Length == 0) return false;
        
        int d1 = roll[0];
        int d2 = (roll.Length > 1) ? roll[1] : 0;
        
        bool hasSix = (d1 == 6 || d2 == 6);
        bool hasFive = (d1 == 5 || d2 == 5);
        
        // 5+6 is Safe (handled by GameStateManager as Safe5Plus6), so we can return false or true?
        // GameStateManager checks IsSafe5Plus6 BEFORE IsLoseTurnRoll.
        // So if it's 5+6, this method won't even be called (or result ignored).
        // But for safety:
        if (hasSix && hasFive) return false; // Safe roll, not a "Lose Turn" penalty (just end of turn)

        // Double 6 is a penalty in Solitary (remove 2 chips), so it SHOULD lose the turn (no placement).
        if (d1 == 6 && d2 == 6) return true;

        // Single 6 is a penalty (remove 1 chip), so it SHOULD lose the turn.
        if (hasSix) return true;

        return false;
    }

    public override bool IsValidMove(Player player, int cellIndex)
    {
        if (cellIndex < 0 || cellIndex >= BoardModel.BOARD_SIZE) return false;
        return IsCellEmpty(cellIndex);
    }

    public override bool CanBump(Player bumpingPlayer, Player targetPlayer, int targetCell)
    {
        // No bumping in Solitary (unless we consider the Dice bumping the player)
        return false;
    }

    public override bool CheckWinCondition(Player player)
    {
        if (gameStateManager == null || gameStateManager.Board == null) return false;

        // Win if board is full (25 chips)
        int totalChips = 0;
        for (int i = 0; i < BoardModel.BOARD_SIZE; i++)
        {
            if (!IsCellEmpty(i)) totalChips++;
        }
        
        return totalChips >= 25;
    }
    
    // Unsubscribe on destroy? GameMode is not a MonoBehaviour, but we should clean up.
    // We don't have a Destroy method in interface.
    // We can unsubscribe in OnGameEnd?
    public override void OnGameEnd(Player winner)
    {
        base.OnGameEnd(winner);
        if (gameStateManager != null)
        {
            gameStateManager.OnDiceRolled -= HandleDiceRoll;
        }
    }
}
