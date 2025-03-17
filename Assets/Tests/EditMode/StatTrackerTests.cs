using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class StatTrackerTests
{
    private GameObject statTrackerGO;
    private StatTracker statTracker;
    private GameObject statDisplayGO;
    private StatDisplay statDisplay;
    private Text statsText;

    [SetUp]
    public void SetUp()
    {
        if (StatTracker.Instance == null)
        {
            statTrackerGO = new GameObject("StatTracker");
            statTracker = statTrackerGO.AddComponent<StatTracker>();
        }
        else
        {
            statTracker = StatTracker.Instance;
        }

        statDisplayGO = new GameObject("StatDisplay");
        statDisplay = statDisplayGO.AddComponent<StatDisplay>();
        statsText = new GameObject("StatsText").AddComponent<Text>();
        statDisplay.statsText = statsText;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(statTrackerGO);
        Object.DestroyImmediate(statDisplayGO);
    }

    [Test]
    public void StatTracker_AddsJumpsCorrectly()
    {
        statTracker.AddJumps(3);
        Assert.AreEqual(3, statTracker.totalJumpsUsed);

        statTracker.AddJumps(2);
        Assert.AreEqual(5, statTracker.totalJumpsUsed);
    }

    [Test]
    public void StatTracker_TracksLevelCompletions()
    {
        statTracker.LevelCompleted();
        Assert.AreEqual(1, statTracker.levelsCompleted);

        statTracker.LevelCompleted();
        Assert.AreEqual(2, statTracker.levelsCompleted);
    }

    [Test]
    public void StatDisplay_HasValidTextComponent()
    {
        Assert.NotNull(statDisplay.statsText);
    }

    [Test]
    public void StatTracker_ResetsCorrectly()
    {
        statTracker.AddJumps(10);
        statTracker.LevelCompleted();
        statTracker.totalJumpsUsed = 0;
        statTracker.levelsCompleted = 0;
        Assert.AreEqual(0, statTracker.totalJumpsUsed);
        Assert.AreEqual(0, statTracker.levelsCompleted);
    }
}
