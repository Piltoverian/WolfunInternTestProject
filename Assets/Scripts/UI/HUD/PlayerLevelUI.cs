using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLevelUI : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private PlayerLevelModule playerLevelModule;

    private void Start()
    {
        if (playerLevelModule == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerLevelModule = player.GetComponent<PlayerLevelModule>();
            }
        }
    }

    private void Update()
    {
        if (playerLevelModule != null)
        {
            if (levelText != null)
            {
                levelText.text = "Lv. " + playerLevelModule.GetCurrentLevel();
            }

            if (expSlider != null)
            {
                float currentExp = playerLevelModule.GetCurrentExp();
                float expToLevelUp = playerLevelModule.GetExpToLevelUp();
                
                if (expToLevelUp > 0)
                {
                    expSlider.value = currentExp / expToLevelUp;
                }
            }
        }
    }
}
