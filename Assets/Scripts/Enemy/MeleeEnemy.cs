using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    [SerializeField] private float attackDamage = 30f;
    [SerializeField] private float attackCooldown = 1f;
    private bool isAttacking = false;
    public override void Die()
    {
        WaveManager.Instance.OnEnemyDied();
        player.GetComponent<PlayerLevelModule>().AddExp(enemyBaseStatsSO.expReward);
        ObjectPooling.Instance.ReturnToPool(gameObject);
    }


    public IEnumerator AttackPlayer()
    {
        PlayerDamageSystem playerDamageSystem = player.GetComponent<PlayerDamageSystem>();
        if (playerDamageSystem != null)
        {
            playerDamageSystem.TakeDamage(attackDamage);
            isAttacking = true;
            agent.isStopped = true;
            yield return new WaitForSeconds(1f);
            isAttacking = false;
            agent.isStopped = false;
        }
        else
        {
            Debug.LogError("PlayerDamageSystem component not found on the player object.");
        }
    }

    private void Update()
    {
        if (!isAttacking)
        {
            if(CheckAttackRange())
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
