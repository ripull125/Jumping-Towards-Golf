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
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        patrolStrategy = new BeePatrolStrategy(pointA: new Vector2(-3, 0),
                                            pointB: new Vector2(3, 0),
                                            speed: 2f);
        chaseStrategy = new BeeChaseStrategy(chaseSpeed: 1f);

        currentStrategy = patrolStrategy; // Start patrolling
        currentlyChasing = false;
        animator.SetBool("isChasing", false);
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            currentlyChasing = true;
        }
        else if (distance >= chaseRange && currentlyChasing)
        {
            // Switch to patrol
            currentStrategy = patrolStrategy;
            animator.SetBool("isChasing", false);
            currentlyChasing = false;
        }
        if (currentlyChasing) {
            FlipSprite();
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
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
            
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is tagged "Player"
        if (other.CompareTag("Player"))
        {
            // Call the respawn method on the player's script.
            other.GetComponent<PlayerController>().Respawn();
        }
    }

}
