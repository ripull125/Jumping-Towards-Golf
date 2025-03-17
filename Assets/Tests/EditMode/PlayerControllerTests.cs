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

        // **Manually assign Rigidbody2D in PlayerController**
        playerController.GetType().GetField("rb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(playerController, rb);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(player);
    }

    [Test]
    public void CapVelocity_LimitsSpeedCorrectly()
    {
        rb.linearVelocity = new Vector2(10f, 0f);
        playerController.CapVelocity();
        Assert.AreEqual(5f, rb.linearVelocity.x, "Expected velocity to be capped at 5, but it wasn't.");

        rb.linearVelocity = new Vector2(-10f, 0f);
        playerController.CapVelocity();
        Assert.AreEqual(-5f, rb.linearVelocity.x, "Expected velocity to be capped at -5, but it wasn't.");
    }

    [Test]
    public void Jump_UpdatesYVelocity()
    {
        float jumpForce = 7f;
        playerController.Jump(jumpForce);
        Assert.AreEqual(jumpForce, rb.linearVelocity.y, "Jump did not correctly update Y velocity.");
    }

    [Test]
    public void SetState_ChangesStateSuccessfully()
    {
        PlayerState newState = new PlayerStateJumping(playerController);
        playerController.SetState(newState);
        Assert.AreEqual(newState, playerController.CurrentState, "SetState() did not correctly update the player's state.");
    }
}
