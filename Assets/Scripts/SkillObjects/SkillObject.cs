using UnityEngine;

public class SkillObject : MonoBehaviour
{
    [SerializeField] protected float damage;

    public void SetProjectileDamage(float damage)
    {
        this.damage = damage;
    }

    public float GetProjectileDamage()
    {
        return damage;
    }
}
