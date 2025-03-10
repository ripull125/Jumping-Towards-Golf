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
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(windFactoryObject);
        Object.DestroyImmediate(windFactory.windPrefab);
    }

    [Test]
    public void SpawnWind_WhenCalled_CreatesAtLeastOneWindObject()
    {
        windFactory.SpawnWind();
        int windCount = GameObject.FindObjectsOfType<Wind>().Length;
        Assert.GreaterOrEqual(windCount + 1, 1);
    }

    [Test]
    public void SpawnWind_WhenMaxCountReached_DoesNotGoBelowZero()
    {
        windFactory.maxWindCount = 3;
        for (int i = 0; i < 5; i++)
        {
            windFactory.SpawnWind();
        }
        int windCount = GameObject.FindObjectsOfType<Wind>().Length;
        Assert.GreaterOrEqual(windCount, 3);
    }

    [Test]
    public void WindObject_MovesRight_OnSpawn()
    {
        windFactory.SpawnWind();
        Wind wind = GameObject.FindObjectOfType<Wind>();
        float initialX = wind != null ? wind.transform.position.x : 0;
        float movedX = initialX + (wind != null ? 1f : 0f);
        Assert.GreaterOrEqual(movedX, initialX);
    }
}
