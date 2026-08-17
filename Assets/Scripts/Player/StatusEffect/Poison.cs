using JetBrains.Annotations;
using UnityEngine;

public class Poison : StatusEffect
{
    float damage;
    float totalTicked=0f;
    public override void ConductEffect(GameObject target)
    {
        PlayerDamageSystem damageSystem = target.GetComponent<PlayerDamageSystem>();
        if (damageSystem != null&&IsPoisonTicking())
        {
            damageSystem.TakeDamage(damage);
            totalTicked = 1f;
        }
    }
    
    public Poison(float damage)
    {
        this.damage = damage;
        totalTicked = 0f;
    }


    public override void EndEffect()
    {
        effectEnded = true;
    }

    public override void ResetEffect()
    {
        base.ResetEffect();
    }
    public bool IsPoisonTicking()
    {
        return Mathf.Abs(totalTicked-0) <= Time.fixedDeltaTime;
    }

    public override void UpdateEffect()
    {
        totalTicked -= Time.fixedDeltaTime;
        base.UpdateEffect();
    }
}
