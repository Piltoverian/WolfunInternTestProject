using NUnit.Framework.Interfaces;
using System;
using UnityEngine;

public abstract class Skill:MonoBehaviour
{
    protected float CurrentChargeCooldown;
    protected int currentCharge;
    protected float currentbetweenChargeCooldown;
    [SerializeField]protected SkillSO skillSO;

    public void InitializeSkill()
    {
        currentCharge = skillSO.maxCharge;
        CurrentChargeCooldown = skillSO.chargeCooldown;
        currentbetweenChargeCooldown = 0;
    }

    public bool IsSkillReady()
    {
        return currentCharge > 0&& currentbetweenChargeCooldown <= 0;
    }
    
    public bool IsChargeSkill()
    {
        return skillSO.maxCharge>1;
    }

    public bool IsSkillChargeOnCooldown()
    {
        return currentCharge < skillSO.maxCharge;
    }

    public int GetCurrentCharge() => currentCharge;
    public int GetMaxCharge() => skillSO.maxCharge;
    public float GetChargeCooldownRatio() => 1f - (CurrentChargeCooldown / skillSO.chargeCooldown);
    public float GetCurrentChargeCooldownValue() => CurrentChargeCooldown;

    public void UpdateSkillCooldown(float deltaTime)
    {
        if(currentCharge < skillSO.maxCharge)
        {
            if (CurrentChargeCooldown > 0)
            {
                CurrentChargeCooldown -= deltaTime;
            }
            if (CurrentChargeCooldown <= 0)
            {
                currentCharge++;
                CurrentChargeCooldown = skillSO.chargeCooldown;
            }
        }    
        
        if (currentbetweenChargeCooldown > 0)
        {
            currentbetweenChargeCooldown -= deltaTime;
        }
        if (currentbetweenChargeCooldown <= 0)
        {
            currentbetweenChargeCooldown = 0;
        }
    }

    public void UseSkillCharge()
    {
        if(currentCharge>0)
        {
            currentCharge--;
            currentbetweenChargeCooldown = skillSO.betweenChargeCooldown;
        }       
    }

    public float CalculateSkillDamage(GameObject activator)
    {
        if(activator.TryGetComponent<PlayerData>(out var playerData))
        {
            return skillSO.skillDammage * (1f + playerData.GetPlayerCurrentStats().currentDamageMul);
        }
        return skillSO.skillDammage;
    }

    public abstract void UseSkill(GameObject activator, Vector3 targetPosition);
}
