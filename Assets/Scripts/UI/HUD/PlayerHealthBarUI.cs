using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    [SerializeField] private PlayerData playerData;

    private static readonly int CurrentHealthProp = Shader.PropertyToID("_CurrentHealth");

    private void Start()
    {
        if (playerData == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerData = player.GetComponent<PlayerData>();
            }
        }
    }

    private void Update()
    {
        if (playerData != null && healthImage != null && healthImage.material != null)
        {
            float currentHealth = playerData.GetCurrentHealth();
            float maxHealth = playerData.GetCurrentMaxHealth();
            
            if (maxHealth > 0)
            {
                healthImage.material.SetFloat(CurrentHealthProp, currentHealth / maxHealth);
            }
        }
    }
}
