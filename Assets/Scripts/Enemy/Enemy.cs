using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyStatsSO enemyBaseStatsSO;
    [SerializeField]protected float health;
    [SerializeField] private float attackDegrees = 25f;
    protected Transform player;
    protected NavMeshAgent agent;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public float GetHealthPercent()
    {
        if (enemyBaseStatsSO.health <= 0) return 0f;
        return health / enemyBaseStatsSO.health;
    }

    public void MoveAndRotateToWardPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (!(distanceToPlayer <= enemyBaseStatsSO.attackRange * GameConfig.Instance.GetGameConfigSO().UnityUnitPerGameUnit))
        {
            agent.SetDestination(player.position);
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
            if (!CheckAttackRange())
            {
                Vector3 directionTowardPlayer = player.position - transform.position;
                directionTowardPlayer.y = 0;
                Quaternion targetRotation = Quaternion.LookRotation(directionTowardPlayer);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90f* Time.deltaTime);
            }
        }
    }

    public void Awake()
    {
        gameObject.tag = "Enemy";
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    public void Start()
    {
        player= GameObject.FindGameObjectWithTag("Player").transform;
        if(player == null)
        {
            Debug.LogError("Player object not found in the scene.");
        }
        agent = GetComponent<NavMeshAgent>();
        if(agent==null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }
        health= enemyBaseStatsSO.health;
    }

    public abstract void Die();
    public virtual bool CheckAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= enemyBaseStatsSO.attackRange * GameConfig.Instance.GetGameConfigSO().UnityUnitPerGameUnit)
        {
            Vector3 directionTowardPlayer = player.position - transform.position;
            directionTowardPlayer = new Vector3(directionTowardPlayer.x, 0, directionTowardPlayer.z).normalized;
            if (Vector3.Dot(transform.forward.normalized, directionTowardPlayer) >= Mathf.Cos(attackDegrees * Mathf.Deg2Rad))
            {
                return true;
            }
        }
        return false;
    }
}
