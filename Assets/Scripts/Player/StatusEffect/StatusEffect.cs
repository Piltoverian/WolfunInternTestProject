using UnityEngine;

public abstract class StatusEffect
{
    protected float duration;
    protected float elapsedTime=0f;
    protected bool effectEnded= false;

    public abstract void ConductEffect(GameObject target);

    public virtual void ApplyEffect(float duration)
    {
        SetDuration(duration);
        ResetEffect();
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public virtual void UpdateEffect()
    {
        elapsedTime += Time.fixedDeltaTime;
        if (elapsedTime >= duration)
        {
            EndEffect();
        }
    }

    public virtual void ResetEffect()
    {
        elapsedTime = 0f;
        effectEnded = false;
    }

    public float GetEffectDuration()
    {
        return duration;
    }   

    public bool IsEffectEnded()
    {
        return effectEnded;
    }

    public abstract void EndEffect();
}
