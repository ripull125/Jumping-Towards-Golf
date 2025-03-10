using UnityEngine;

public class BeePatrolStrategy : IBeeStrategy
{
    private Vector2 pointA;
    private Vector2 pointB;
    private float speed;

    public BeePatrolStrategy(Vector2 pointA, Vector2 pointB, float speed)
    {
        this.pointA = pointA;
        this.pointB = pointB;
        this.speed = speed;
    }

    public void Move(Transform enemyTransform, Transform target)
    {
        // Simple patrol logic: move between pointA and pointB
        // ...
        // (Pseudocode for illustration)
        Vector2 currentPos = enemyTransform.position;
        // Decide direction and move
    }
}
