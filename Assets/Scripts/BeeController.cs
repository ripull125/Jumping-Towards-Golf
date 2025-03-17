using UnityEngine;

public class BeeController : MonoBehaviour
{
    private IBeeStrategy currentStrategy;
    public Transform player;
    public float chaseRange = 2f;
    private IBeeStrategy patrolStrategy;
    private IBeeStrategy chaseStrategy;
    private Animator animator;
    private bool currentlyChasing;
    private SpriteRenderer spriteRenderer;
    public Transform pointA;
    public Transform pointB;




    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (animator == null || spriteRenderer == null || player == null) return;
        patrolStrategy = new BeePatrolStrategy(pointA.position, pointB.position, 2f, spriteRenderer);
        chaseStrategy = new BeeChaseStrategy(1f);
        currentStrategy = patrolStrategy;
        currentlyChasing = false;
        animator.SetBool("isChasing", false);
    }

    void Update()
    {
        BeeUpdateLogic();
    }

    private void BeeUpdateLogic()
    {
        if (player == null || animator == null || spriteRenderer == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance < chaseRange && !currentlyChasing)
        {
            currentStrategy = chaseStrategy;
            animator.SetBool("isChasing", true);
            currentlyChasing = true;
        }
        else if (distance >= chaseRange && currentlyChasing)
        {
            currentStrategy = patrolStrategy;
            animator.SetBool("isChasing", false);
            currentlyChasing = false;
        }
        if (currentlyChasing) FlipSprite();
    }

    private void FlipSprite()
    {
        spriteRenderer.flipX = player.position.x < transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.Respawn();
        }
    }

    public void ForceUpdate()
    {
        BeeUpdateLogic();
        if (animator != null)
        {
            animator.Update(0f);
        }
    }

    public void Test_OnTriggerEnter(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }
}
