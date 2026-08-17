using UnityEngine;

public class PlayerLevelModule : PlayerModule
{
    [SerializeField] private float expToLevelUp = 100f;

    private float currentExp = 0f;
    private int currentLevel = 1;

    public void AddExp(float amount)
    {
        currentExp += amount;
        while (currentExp >= expToLevelUp)
        {
            currentExp -= expToLevelUp;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        PlayerData playerData = GetComponent<PlayerData>();
        if (playerData != null)
        {
            playerData.ApplyLevelUpBonus();
        }
        else
        {
            Debug.LogError("PlayerData not found on " + gameObject.name);
        }

        Debug.Log($"Level Up! Cấp hiện tại: {currentLevel} | EXP dư: {currentExp}");
    }

    public int GetCurrentLevel() => currentLevel;
    public float GetCurrentExp() => currentExp;
    public float GetExpToLevelUp() => expToLevelUp;
}
