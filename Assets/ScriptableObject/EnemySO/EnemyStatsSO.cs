using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsSO", menuName = "ScriptableObjects/EnemyStatsSO", order = 4)]
public class EnemyStatsSO : ScriptableObject
{
    public float health;
    public float speed;
    public float attackRange;
    public float expReward = 30f;
}
