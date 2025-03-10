using UnityEngine;

public interface IBeeStrategy
{
    void Move(Transform enemyTransform, Transform target);
}
