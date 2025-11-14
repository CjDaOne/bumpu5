using UnityEngine;

/// <summary>
/// Manages all dice rolls and related game logic.
/// Handles single die, double die, and edge cases (5+6 safe, 6 = lose turn, etc.)
/// </summary>
public class DiceManager
{
    private int[] lastRoll;

    /// <summary>
    /// Gets the last dice roll as an int array [die1, die2].
    /// </summary>
    public int[] LastRoll => lastRoll;

    /// <summary>
    /// Gets whether the last roll was a double (both dice the same).
    /// </summary>
    public bool IsDouble => lastRoll != null && lastRoll[0] == lastRoll[1];

    /// <summary>
    /// Gets whether the last roll is the special 5+6 "safe" combination.
    /// </summary>
    public bool IsSafe5Plus6 => lastRoll != null && 
        ((lastRoll[0] == 5 && lastRoll[1] == 6) || (lastRoll[0] == 6 && lastRoll[1] == 5));

    /// <summary>
    /// Gets whether the player loses their turn (rolled a 6).
    /// Only applies to single die or a die showing 6 in two-die roll.
    /// </summary>
    public bool IsLoseTurn => lastRoll != null && lastRoll.Length > 0 && lastRoll[0] == 6;

    /// <summary>
    /// Initializes the DiceManager.
    /// </summary>
    public DiceManager()
    {
        lastRoll = null;
    }

    /// <summary>
    /// Rolls a single die and stores the result.
    /// </summary>
    /// <returns>Roll result (1-6)</returns>
    public int RollSingleDie()
    {
        int roll = Random.Range(1, 7);
        lastRoll = new int[] { roll };
        return roll;
    }

    /// <summary>
    /// Rolls two dice and stores the result.
    /// </summary>
    /// <returns>Array with two roll results [die1, die2]</returns>
    public int[] RollTwoDice()
    {
        int die1 = Random.Range(1, 7);
        int die2 = Random.Range(1, 7);
        lastRoll = new int[] { die1, die2 };
        return lastRoll;
    }

    /// <summary>
    /// Gets the sum of the last roll.
    /// </summary>
    /// <returns>Sum of dice (2-12 for two dice, 1-6 for single)</returns>
    public int GetDiceSum()
    {
        if (lastRoll == null || lastRoll.Length == 0)
            return 0;

        int sum = 0;
        foreach (int die in lastRoll)
            sum += die;
        return sum;
    }

    /// <summary>
    /// Gets the sum of a specific dice roll.
    /// </summary>
    /// <param name="roll">Array of dice values</param>
    /// <returns>Sum of the dice</returns>
    public static int GetDiceSum(int[] roll)
    {
        if (roll == null || roll.Length == 0)
            return 0;

        int sum = 0;
        foreach (int die in roll)
            sum += die;
        return sum;
    }

    /// <summary>
    /// Clears the last roll record.
    /// </summary>
    public void ClearLastRoll()
    {
        lastRoll = null;
    }

    /// <summary>
    /// Returns a string representation of the last roll.
    /// </summary>
    public override string ToString()
    {
        if (lastRoll == null || lastRoll.Length == 0)
            return "No roll yet";

        if (lastRoll.Length == 1)
            return $"Rolled: {lastRoll[0]}";

        return $"Rolled: [{lastRoll[0]}, {lastRoll[1]}] = {GetDiceSum()}";
    }
}
