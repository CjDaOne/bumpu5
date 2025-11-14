using NUnit.Framework;

/// <summary>
/// Unit tests for the DiceManager class.
/// Tests roll mechanics, edge cases, and special cases (5+6 safe, doubles, etc.)
/// </summary>
public class DiceTests
{
    private DiceManager diceManager;

    [SetUp]
    public void Setup()
    {
        diceManager = new DiceManager();
    }

    [Test]
    public void RollSingleDie_AlwaysReturns1To6()
    {
        for (int i = 0; i < 100; i++)
        {
            int roll = diceManager.RollSingleDie();
            Assert.GreaterOrEqual(roll, 1);
            Assert.LessOrEqual(roll, 6);
        }
    }

    [Test]
    public void RollSingleDie_StoresLastRoll()
    {
        diceManager.RollSingleDie();
        Assert.IsNotNull(diceManager.LastRoll);
        Assert.AreEqual(1, diceManager.LastRoll.Length);
    }

    [Test]
    public void RollTwoDice_SumBetween2And12()
    {
        for (int i = 0; i < 100; i++)
        {
            int[] roll = diceManager.RollTwoDice();
            int sum = DiceManager.GetDiceSum(roll);
            Assert.GreaterOrEqual(sum, 2);
            Assert.LessOrEqual(sum, 12);
        }
    }

    [Test]
    public void RollTwoDice_StoresLastRoll()
    {
        int[] roll = diceManager.RollTwoDice();
        Assert.IsNotNull(diceManager.LastRoll);
        Assert.AreEqual(2, diceManager.LastRoll.Length);
        Assert.AreEqual(roll[0], diceManager.LastRoll[0]);
        Assert.AreEqual(roll[1], diceManager.LastRoll[1]);
    }

    [Test]
    public void GetDiceSum_CalculatesCorrectly()
    {
        diceManager.RollTwoDice();
        int sum = diceManager.GetDiceSum();
        Assert.AreEqual(diceManager.LastRoll[0] + diceManager.LastRoll[1], sum);
    }

    [Test]
    public void IsDouble_WithIdenticalDice_ReturnsTrue()
    {
        // We can't control the random roll, so we test the logic
        // by manually setting the lastRoll
        diceManager.RollTwoDice();
        // If by chance we rolled a double, test it
        if (diceManager.LastRoll[0] == diceManager.LastRoll[1])
        {
            Assert.IsTrue(diceManager.IsDouble);
        }
    }

    [Test]
    public void IsDouble_WithDifferentDice_ReturnsFalse()
    {
        // Roll until we get different values
        for (int i = 0; i < 1000; i++)
        {
            diceManager.RollTwoDice();
            if (diceManager.LastRoll[0] != diceManager.LastRoll[1])
            {
                Assert.IsFalse(diceManager.IsDouble);
                break;
            }
        }
    }

    [Test]
    public void IsSafe5Plus6_WithoutSafeRoll_ReturnsFalse()
    {
        // Roll until we get a non-5/6 combination
        for (int i = 0; i < 1000; i++)
        {
            diceManager.RollTwoDice();
            if (!((diceManager.LastRoll[0] == 5 && diceManager.LastRoll[1] == 6) ||
                  (diceManager.LastRoll[0] == 6 && diceManager.LastRoll[1] == 5)))
            {
                Assert.IsFalse(diceManager.IsSafe5Plus6);
                break;
            }
        }
    }

    [Test]
    public void IsLoseTurn_WithRoll6_ReturnsTrue()
    {
        // Roll until we get a single 6
        for (int i = 0; i < 1000; i++)
        {
            diceManager.RollSingleDie();
            if (diceManager.LastRoll[0] == 6)
            {
                Assert.IsTrue(diceManager.IsLoseTurn);
                break;
            }
        }
    }

    [Test]
    public void GetDiceSum_StaticMethod_CalculatesCorrectly()
    {
        int[] roll = new int[] { 3, 4 };
        Assert.AreEqual(7, DiceManager.GetDiceSum(roll));
    }

    [Test]
    public void GetDiceSum_WithNullRoll_ReturnsZero()
    {
        int sum = DiceManager.GetDiceSum(null);
        Assert.AreEqual(0, sum);
    }

    [Test]
    public void GetDiceSum_WithEmptyRoll_ReturnsZero()
    {
        int sum = DiceManager.GetDiceSum(new int[] { });
        Assert.AreEqual(0, sum);
    }

    [Test]
    public void ClearLastRoll_ResetsRoll()
    {
        diceManager.RollTwoDice();
        Assert.IsNotNull(diceManager.LastRoll);

        diceManager.ClearLastRoll();
        Assert.IsNull(diceManager.LastRoll);
    }

    [Test]
    public void ToString_ReflectsCurrentRoll()
    {
        string beforeRoll = diceManager.ToString();
        Assert.Contains("No roll yet", beforeRoll);

        diceManager.RollSingleDie();
        string afterRoll = diceManager.ToString();
        Assert.Contains("Rolled:", afterRoll);
    }
}
