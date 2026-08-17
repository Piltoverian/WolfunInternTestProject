using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerData : PlayerModule
{
    [Header("Stats")]
    [SerializeField] private PlayerBaseStatSO playerBaseStatSO;
    [SerializeField]private float currentHealth;
    private float currentMaxHealth;
    private float currentArmor;
    private float currentDamageMul;
    
    public struct PlayerCurrentStats
    {
        public float currentHealth;
        public float currentArmor;
        public float currentDamageMul;
    }

    public PlayerBaseStatSO GetPlayerBaseStat()
    {
        return playerBaseStatSO;
    }

    public PlayerCurrentStats GetPlayerCurrentStats()
    {
        return new PlayerCurrentStats
        {
            currentHealth = currentHealth,
            currentArmor = currentArmor,
            currentDamageMul = currentDamageMul
        };
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetCurrentMaxHealth() => currentMaxHealth;

    private void Awake()
    {
        currentHealth = playerBaseStatSO.baseMaxHealth;
        currentMaxHealth = playerBaseStatSO.baseMaxHealth;
        currentArmor = playerBaseStatSO.baseArmor;
        currentDamageMul = playerBaseStatSO.baseDamageMul;
    }

    public void ApplyNewcurrentStats(PlayerCurrentStats newStats)
    {
        currentHealth = newStats.currentHealth;
        currentArmor = newStats.currentArmor;
        currentDamageMul = newStats.currentDamageMul;
    }

    public void ApplyLevelUpBonus()
    {
        currentHealth += playerBaseStatSO.perLevelHealth;
        currentArmor += playerBaseStatSO.perLevelArmor;
        currentDamageMul += playerBaseStatSO.perLevelDamageMul;
        currentMaxHealth += playerBaseStatSO.perLevelHealth;
    }
}
