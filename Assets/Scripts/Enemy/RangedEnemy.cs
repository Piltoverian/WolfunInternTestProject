using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float PoisonDamage = 30f;
    [SerializeField] private float PoisonDuration = 3f;
    [SerializeField] EnemyPoisonBullet poisonProjectilePrefab;
    [SerializeField] GameObject OutputPos;
    private bool isAttacking = false;
    public override void Die()
    {
        WaveManager.Instance.OnEnemyDied();
        player.GetComponent<PlayerLevelModule>().AddExp(enemyBaseStatsSO.expReward);
        ObjectPooling.Instance.ReturnToPool(gameObject);
    }

    public IEnumerator AttackPlayer()
    {
        GameObject poisonProjectile = ObjectPooling.Instance.InstantiateObject(poisonProjectilePrefab.gameObject, OutputPos.transform.position);
        poisonProjectile.GetComponent<EnemyPoisonBullet>().Initialize(PoisonDuration, PoisonDamage,transform.forward);
        isAttacking = true;
        agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        agent.isStopped = false;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            if (CheckAttackRange())
            {
                StartCoroutine(AttackPlayer());
            }
            else
            {
                MoveAndRotateToWardPlayer();
            }
        }
    }
}
