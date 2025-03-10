using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

// Dummy player controller to test collision behavior.
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
        // Create Bee GameObject and add required components.
        beeGO = new GameObject("Bee");
        beeGO.AddComponent<SpriteRenderer>();  // For spriteRenderer.
        beeGO.AddComponent<Animator>();          // For animator.
        beeController = beeGO.AddComponent<BeeController>();

        // Create patrol point GameObjects.
        pointAGO = new GameObject("PointA");
        pointAGO.transform.position = new Vector2(-5, 0);
        pointBGO = new GameObject("PointB");
        pointBGO.transform.position = new Vector2(5, 0);
        // Assign patrol points to BeeController.
        beeController.pointA = pointAGO.transform;
        beeController.pointB = pointBGO.transform;

        // Create Player GameObject.
        playerGO = new GameObject("Player");
        // Add a dummy player controller script.
        dummyPlayer = playerGO.AddComponent<DummyPlayerController>();
        // Tag the player properly.
        playerGO.tag = "Player";
        // Position the player far away initially.
        playerGO.transform.position = new Vector2(10, 0);
        // Assign player to BeeController.
        beeController.player = playerGO.transform;

        // Set chase range (2 units in this example).
        beeController.chaseRange = 2f;
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(beeGO);
        Object.Destroy(playerGO);
        Object.Destroy(pointAGO);
        Object.Destroy(pointBGO);
    }

    [UnityTest]
    public IEnumerator BeeController_StaysInPatrolMode_WhenPlayerIsFar()
    {
        // With the player far away, the bee should be patrolling.
        // Record initial position.
        Vector3 initialPosition = beeGO.transform.position;
        // Wait one frame for Update() to run.
        yield return null;
        // After Update, the bee should have moved according to its patrol strategy.
        Vector3 newPosition = beeGO.transform.position;
        Assert.AreNotEqual(initialPosition, newPosition, "Bee did not move while patrolling.");
        
        // Also, the animator parameter should be false.
        Animator animator = beeGO.GetComponent<Animator>();
        Assert.IsFalse(animator.GetBool("isChasing"), "Animator parameter isChasing should be false in patrol mode.");
    }

    [UnityTest]
    public IEnumerator BeeController_SwitchesToChaseMode_WhenPlayerIsClose_AndFlipsSprite()
    {
        // Position the player close to the bee to trigger chase mode.
        playerGO.transform.position = beeGO.transform.position + Vector3.one; // Within 2 units.
        yield return null;
        yield return null; // Wait additional frame(s) for Update() to register change.

        // Check that the bee switched to chase mode.
        Animator animator = beeGO.GetComponent<Animator>();
        Assert.IsTrue(animator.GetBool("isChasing"), "Animator parameter isChasing should be true when chasing.");

        // Test the sprite flipping.
        SpriteRenderer sr = beeGO.GetComponent<SpriteRenderer>();
        // Place player to the left of the bee.
        playerGO.transform.position = beeGO.transform.position + new Vector3(-1, 0, 0);
        yield return null;
        Assert.IsTrue(sr.flipX, "Sprite should be flipped (flipX true) when player is to the left.");

        // Place player to the right of the bee.
        playerGO.transform.position = beeGO.transform.position + new Vector3(1, 0, 0);
        yield return null;
        Assert.IsFalse(sr.flipX, "Sprite should not be flipped (flipX false) when player is to the right.");
    }

    [UnityTest]
    public IEnumerator BeeController_OnTriggerEnter2D_CallsPlayerRespawn()
    {
        // Add 2D colliders to both Bee and Player so that collision events can occur.
        CircleCollider2D beeCollider = beeGO.AddComponent<CircleCollider2D>();
        beeCollider.isTrigger = true;
        CircleCollider2D playerCollider = playerGO.AddComponent<CircleCollider2D>();
        playerCollider.isTrigger = true;

        // Manually call OnTriggerEnter2D on BeeController by passing the player's collider.
        beeController.OnTriggerEnter2D(playerCollider);
        yield return null;
        
        // Verify that the player's Respawn method was called.
        Assert.IsTrue(dummyPlayer.respawnCalled, "Player's Respawn method was not called on collision.");
    }
}
