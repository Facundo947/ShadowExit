using UnityEngine;

public abstract class EnemyFactoryBase : ScriptableObject
{
    //interfaz abstracta para la ceacion de los enemigos
    public abstract GameObject CreateFollowEnemy(Vector3 position, Transform followTarget);
    public abstract GameObject CreatePatrolEnemy(Vector3 position, Transform followTarget, Transform[] patrolWaypoints);
}
