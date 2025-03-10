using UnityEngine;

public class BeePatrolStrategy : IBeeStrategy
{
    private Vector2 pointA;
    private Vector2 pointB;
    private float speed;
    private bool movingToB = true;

    private SpriteRenderer spriteRenderer;

    public BeePatrolStrategy(Vector2 pointA, Vector2 pointB, float speed, SpriteRenderer spriteRenderer)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.speed = speed;
        this.spriteRenderer = spriteRenderer;
    }

    public void Move(Transform enemyTransform, Transform target)
    {
        // // Simple patrol logic: move between pointA and pointB
        // // ...
        // // (Pseudocode for illustration)
        // Vector2 currentPos = enemyTransform.position;
        // // Decide direction and move

        // Determine the current target based on the patrol direction.
        Vector2 targetPos = movingToB ? pointB : pointA;
        
        // Move towards the target position.
        enemyTransform.position = Vector2.MoveTowards(
            enemyTransform.position,
            targetPos,
            speed * Time.deltaTime
        );

        // Flip the bee's sprite based on the target position:
        // If moving to point A (assumed to be on the left), ensure the sprite faces left (negative x scale).
        // If moving to point B (assumed to be on the right), ensure it faces right (positive x scale).
        Vector3 scale = enemyTransform.localScale;
        if (targetPos.x < enemyTransform.position.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
        enemyTransform.localScale = scale;

        // When the bee gets close to the target point, switch the direction.
        if (Vector2.Distance(enemyTransform.position, targetPos) < 0.1f)
        {
            movingToB = !movingToB;
        }

        // Flip the sprite using SpriteRenderer.flipX
        if (targetPos.x < enemyTransform.position.x)
            spriteRenderer.flipX = true;  // Face left
        else
            spriteRenderer.flipX = false; // Face right
    }
}
