using UnityEngine;

public class XPText : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI xpText;
    [SerializeField] private PlayerLevelModule levelModule;
    

    // Update is called once per frame
    void Update()
    {
        xpText.text = "LVL: " + levelModule.GetCurrentLevel();
    }
}
