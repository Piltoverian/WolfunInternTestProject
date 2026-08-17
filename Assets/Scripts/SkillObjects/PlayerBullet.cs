using UnityEngine;

public class PlayerBullet : SkillObject
{
    [SerializeField] private Vector3 bulletDirection;
    [SerializeField] private float bulletSpeed=10f;
    public void Initialize(float damage, Vector3 direction)
    {
        SetProjectileDamage(damage);
        bulletDirection = direction.normalized;
        transform.forward = new Vector3(bulletDirection.x,bulletDirection.y,bulletDirection.z);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.GetComponent<PlayerBullet>() != null)
        {
            return;
        }
        Debug.Log("Player bullet collided with layer: " + LayerMask.LayerToName(collision.gameObject.layer) + ", tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        ObjectPooling.Instance.ReturnToPool(gameObject);
    }

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), 3f);
    }
    private void ReturnToPool()
    {
        CancelInvoke();
        ObjectPooling.Instance.ReturnToPool(gameObject);
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed* GameConfig.Instance.GetGameConfigSO().UnityUnitPerGameUnit;
    }
}
