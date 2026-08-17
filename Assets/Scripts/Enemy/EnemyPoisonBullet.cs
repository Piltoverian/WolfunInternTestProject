using UnityEngine;

public class EnemyPoisonBullet : MonoBehaviour
{
    [SerializeField] private Vector3 bulletDirection;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float vanishingDistance=5f;
    private float poisonDuration = 3f;
    private float poisonDamagePerTick = 30f;
    public void Initialize(float poisonDuration,float poisonDamagePerTick, Vector3 direction)
    {
        bulletDirection = direction.normalized;
        transform.forward = new Vector3(bulletDirection.x, bulletDirection.y, bulletDirection.z);
        this.poisonDuration = poisonDuration;
        this.poisonDamagePerTick = poisonDamagePerTick;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.GetComponent<EnemyPoisonBullet>() != null)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStatusModule>().ApplyEffect(new Poison(poisonDamagePerTick), poisonDuration);
        }
        ObjectPooling.Instance.ReturnToPool(gameObject);
    }

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), vanishingDistance / bulletSpeed);
    }
    private void ReturnToPool()
    {
        CancelInvoke();
        ObjectPooling.Instance.ReturnToPool(gameObject);
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed * GameConfig.Instance.GetGameConfigSO().UnityUnitPerGameUnit;
    }
}
