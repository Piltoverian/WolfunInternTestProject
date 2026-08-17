using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDamageSystem : PlayerModule
{
    private PlayerData playerData;
    [SerializeField] private GameObject losingPanel;

    private void Start()
    {
        playerData = GetComponent<PlayerData>();
    }

    public void TakeDamage(float damage)
    {
        if (playerData == null) return;
        
        PlayerData.PlayerCurrentStats data = playerData.GetPlayerCurrentStats();
        float realdamage = (damage - data.currentArmor) > 0 ? damage - data.currentArmor : 0;
        data.currentHealth -= realdamage;
        playerData.ApplyNewcurrentStats(data);
        if (data.currentHealth < 0)
        {
            Die();
        }
    }
    public override void UpdateModule()
    {
        
    }

    public void Die()
    {
        losingPanel.SetActive(true);
        ObjectPooling.Instance.ReturnToPool(gameObject);
    }
}
