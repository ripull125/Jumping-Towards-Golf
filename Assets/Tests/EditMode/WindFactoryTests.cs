using NUnit.Framework;
using UnityEngine;

public class WindFactoryTests
{
    private GameObject windFactoryObject;
    private WindFactory windFactory;

    [SetUp]
    public void SetUp()
    {
        windFactoryObject = new GameObject();
        windFactory = windFactoryObject.AddComponent<WindFactory>();
        windFactory.windPrefab = new GameObject("WindPrefab");
        windFactory.windPrefab.AddComponent<Wind>();

        windFactory.spawnAreaMin = new Vector2(-5, -5);
        windFactory.spawnAreaMax = new Vector2(5, 5);
        windFactory.maxWindCount = 3;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(windFactoryObject);
        Object.DestroyImmediate(windFactory.windPrefab);

        foreach (var wind in Object.FindObjectsOfType<Wind>())
        {
            Object.DestroyImmediate(wind.gameObject);
        }
    }

    [Test]
    public void SpawnWind_SpawnsWindObject_WhenUnderMaxCount()
    {
        windFactory.SpawnWind();
        int windCount = Object.FindObjectsOfType<Wind>().Length;
        Assert.AreEqual(1, windCount, "WindFactory should have spawned one wind object.");
    }
    [Test]
    public void SpawnWind_LimitsSpawnedObjects()
    {
        windFactory.maxWindCount = 3;

        windFactory.SpawnWind();
        windFactory.SpawnWind();
        windFactory.SpawnWind();
        windFactory.SpawnWind();

        int windCount = Object.FindObjectsOfType<Wind>().Length;

        Assert.LessOrEqual(windCount, windFactory.maxWindCount,
            $"Spawned {windCount} winds, but max allowed is {windFactory.maxWindCount}");
    }


    [Test]
    public void OnWindDestroyed_DecreasesWindCount()
    {
        windFactory.SpawnWind();
        Wind wind = Object.FindObjectOfType<Wind>();

        windFactory.OnWindDestroyed();

        int windCount = Object.FindObjectsOfType<Wind>().Length;
        Assert.AreEqual(0, windCount, "Wind count should decrease after destruction.");
    }

}
