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

    [UnityTest]
    public IEnumerator TestRightMovement()
    {
        Input.GetKey(KeyCode.RightArrow);
        yield return null;

        Assert.Greater(playerController.Rigidbody.velocity.x, 0f);
    }

    [UnityTest]
    public IEnumerator TestLeftMovement()
    {
        Input.GetKey(KeyCode.LeftArrow);
        yield return null;

        Assert.Less(playerController.Rigidbody.velocity.x, 0f);
    }

    [UnityTest]
    public IEnumerator TestNoMovement()
    {
        yield return null;
        Assert.AreEqual(0f, playerController.Rigidbody.velocity.x);
    }

    [Test]
    public void TestCapVelocity()
    {
        playerController.Move(10f);
        Assert.AreEqual(5f, playerController.Rigidbody.velocity.x);
    }

    [UnityTest]
    public IEnumerator TestStateTransitionToJumping()
    {
        Assert.IsInstanceOf<PlayerStateNormal>(playerController.CurrentState);
        Input.GetKeyDown(KeyCode.Space);
        yield return null;

        Assert.IsInstanceOf<PlayerStateJumping>(playerController.CurrentState);
    }
}
