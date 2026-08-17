using NUnit.Framework;
using System;
using UnityEngine;
using System.Collections.Generic;
public class PlayerStatusModule : PlayerModule
{
    [SerializeField] private List<StatusEffect> statusEffects = new List<StatusEffect>();

    public void ApplyEffect(StatusEffect effect,float duration)
    {
        for (int i = 0; i < statusEffects.Count; i++)
        {
            if(statusEffects[i].GetType() == effect.GetType())
            {
                statusEffects[i].SetDuration(duration);
                statusEffects[i].ResetEffect();
                return;
            }
        }
        effect.ApplyEffect(duration);
        statusEffects.Add(effect);
    }

    public override void FixedUpdateModule()
    {
        for (int i = 0; i < statusEffects.Count; i++)
        {
            if (statusEffects[i].IsEffectEnded())
            {
                statusEffects.RemoveAt(i);
                i--;
                continue;
            }
            statusEffects[i].ConductEffect(gameObject);
            statusEffects[i].UpdateEffect();
        }   
    }
}
