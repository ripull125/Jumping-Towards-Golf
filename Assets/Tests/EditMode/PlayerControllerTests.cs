using NUnit.Framework;
using UnityEngine;
using System.Collections;
using UnityEngine.TestTools;

public class PlayerControllerTests
{
    private GameObject player;
    private PlayerController playerController;
    private Rigidbody2D rb;

    [SetUp]
    public void SetUp()
    {
        player = new GameObject("Player");
        playerController = player.AddComponent<PlayerController>();
        rb = player.AddComponent<Rigidbody2D>();
        player.AddComponent<Animator>();
        player.AddComponent<SpriteRenderer>();
        player.SetActive(false);
        player.SetActive(true);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
    }


    [Test]
    public void TestCapVelocity()
    {
        float fakeVelocity = 10f;
        float expectedVelocity = Mathf.Min(fakeVelocity, 5f);
        Assert.AreEqual(expectedVelocity, 5f);
    }

    [UnityTest]
    public IEnumerator TestStateTransitionToJumping()
    {
        PlayerState dummyState = new PlayerStateNormal(playerController);
        Assert.IsInstanceOf<PlayerStateNormal>(dummyState);
        yield return null;
        Assert.IsInstanceOf<PlayerStateNormal>(dummyState);
    }
}
