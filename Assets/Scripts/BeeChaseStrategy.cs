using UnityEngine;

public class BeeChaseStrategy : IBeeStrategy
{
    private float chaseSpeed;

    public BeeChaseStrategy(float chaseSpeed)
    {
        this.chaseSpeed = chaseSpeed;
    }

    public void Move(Transform enemyTransform, Transform target)
    {
        // Move toward the player
        Vector2 direction = (target.position - enemyTransform.position).normalized;
        enemyTransform.position += (Vector3)direction * chaseSpeed * Time.deltaTime;
    }
}
