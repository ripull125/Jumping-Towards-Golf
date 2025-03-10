using UnityEngine;

public class BeeController : MonoBehaviour
{
    private IBeeStrategy currentStrategy;

    public Transform player;
    public float chaseRange = 2f;

    // Example: define or create your strategies
    private IBeeStrategy patrolStrategy;
    private IBeeStrategy chaseStrategy;

    private Animator animator;
    private bool currentlyChasing;

    void Start()
    {
        animator = GetComponent<Animator>();
        patrolStrategy = new BeePatrolStrategy(pointA: new Vector2(-3, 0),
                                            pointB: new Vector2(3, 0),
                                            speed: 2f);
        chaseStrategy = new BeeChaseStrategy(chaseSpeed: 3f);

        currentStrategy = patrolStrategy; // Start patrolling
        currentlyChasing = false;
        animator.SetBool("isChasing", false);
        
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // Decide which behavior to use based on player's distance
        if (distance < chaseRange && !currentlyChasing)
        {
            // Switch to chase
            currentStrategy = chaseStrategy;
            animator.SetBool("isChasing", true);
            FlipSprite();
            currentlyChasing = true;
        }
        else if (distance >= chaseRange && currentlyChasing)
        {
            // Switch to patrol
            currentStrategy = patrolStrategy;
            animator.SetBool("isChasing", false);
            FlipSprite();
            currentlyChasing = false;
        }
        
        // Execute movement behavior
        currentStrategy.Move(transform, player);
    }

    // Flips the bee's x scale so it faces the player
    void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        // If player is to the left, ensure the bee is flipped (negative x); otherwise, positive.
        if (player.position.x < transform.position.x)
            scale.x = -Mathf.Abs(scale.x);
        else
            scale.x = Mathf.Abs(scale.x);
            
        transform.localScale = scale;
    }
}
