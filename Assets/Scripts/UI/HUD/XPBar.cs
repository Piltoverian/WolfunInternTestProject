using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private PlayerLevelModule levelModule;

    private static readonly int CurrentXp = Shader.PropertyToID("_XPPercentage");

    private void Start()
    {
        if (levelModule == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                levelModule = player.GetComponent<PlayerLevelModule>();
            }
        }
    }

    private void Update()
    {
        if (levelModule != null && image != null && image.material != null)
        {
            float currentXp = levelModule.GetCurrentExp();
            float XPtoLVLUP = levelModule.GetExpToLevelUp();

            if (XPtoLVLUP > 0)
            {
                image.material.SetFloat(CurrentXp, currentXp / XPtoLVLUP);
            }
        }
    }
}
