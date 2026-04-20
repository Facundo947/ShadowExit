using UnityEngine;

public abstract class EnemyFactoryBase : ScriptableObject
{
    public abstract GameObject CreateFollowEnemy(Vector3 position, Transform followTarget);
    public abstract GameObject CreatePatrolEnemy(Vector3 position, Transform followTarget);
}
