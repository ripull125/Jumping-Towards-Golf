using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

public class DummyPlayerController : MonoBehaviour
{
    public bool respawnCalled = false;
    public void Respawn()
    {
        respawnCalled = true;
    }
}

public class BeeControllerTests
{
    private GameObject beeGO;
    private BeeController beeController;
    private GameObject playerGO;
    private DummyPlayerController dummyPlayer;
    private GameObject pointAGO;
    private GameObject pointBGO;

    [SetUp]
    public void Setup()
    {
        beeGO = new GameObject("Bee");
        beeGO.AddComponent<SpriteRenderer>();
        beeGO.AddComponent<Animator>();
        beeGO.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        beeGO.AddComponent<CircleCollider2D>().isTrigger = true;
        beeController = beeGO.AddComponent<BeeController>();

        pointAGO = new GameObject("PointA");
        pointAGO.transform.position = new Vector2(-5, 0);
        pointBGO = new GameObject("PointB");
        pointBGO.transform.position = new Vector2(5, 0);
        beeController.pointA = pointAGO.transform;
        beeController.pointB = pointBGO.transform;

        playerGO = new GameObject("Player");
        dummyPlayer = playerGO.AddComponent<DummyPlayerController>();
        playerGO.tag = "Player";
        playerGO.transform.position = new Vector2(10, 0);
        playerGO.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        playerGO.AddComponent<CircleCollider2D>().isTrigger = true;
        beeController.player = playerGO.transform;
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(beeGO);
        Object.DestroyImmediate(playerGO);
        Object.DestroyImmediate(pointAGO);
        Object.DestroyImmediate(pointBGO);
    }

    [Test]
    public void Bee_Exists()
    {
        Assert.NotNull(beeGO, "Bee object should exist.");
        Assert.NotNull(beeGO.GetComponent<BeeController>(), "Bee should have BeeController.");
    }

    [Test]
    public void Bee_StartsInPatrolMode()
    {
        Assert.NotNull(beeController, "BeeController is null.");
        Assert.IsFalse(beeGO.GetComponent<Animator>().GetBool("isChasing"), "Bee should start in patrol mode.");
    }

    [Test]
    public void Bee_HasRequiredComponents()
    {
        Assert.NotNull(beeGO.GetComponent<SpriteRenderer>(), "Bee should have a SpriteRenderer.");
        Assert.NotNull(beeGO.GetComponent<Animator>(), "Bee should have an Animator.");
        Assert.NotNull(beeGO.GetComponent<Rigidbody2D>(), "Bee should have a Rigidbody2D.");
        Assert.NotNull(beeGO.GetComponent<CircleCollider2D>(), "Bee should have a Collider.");
    }
}
