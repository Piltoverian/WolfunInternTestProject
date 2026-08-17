using UnityEngine;

public class PlayerBomb : SkillObject
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionDelay = 2f;
    private float countdown;

    public void Initialize(float damage)
    {
        countdown = explosionDelay;
        SetProjectileDamage(damage);
    }

    public void Explode()
    {
        // Logic for explosion effect, damage, etc.
        float scaledRadius = explosionRadius * GameConfig.Instance.GetGameConfigSO().UnityUnitPerGameUnit;
        Collider[] colliders = Physics.OverlapCapsule(transform.position - Vector3.up * scaledRadius * 2, transform.position + Vector3.up * scaledRadius * 2, scaledRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                nearbyObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
        ObjectPooling.Instance.ReturnToPool(gameObject);
    }
    void Update()
    {
        if (countdown > 0)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                Explode();
            }
        }
    }
}
