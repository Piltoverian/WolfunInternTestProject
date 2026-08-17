using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButtonUI : MonoBehaviour
{
    [SerializeField] private int skillIndex;
    [SerializeField] private Sprite skillIcon;
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI cooldownText;
    [SerializeField] private TextMeshProUGUI chargeText;
    
    private PlayerSkillModule playerSkillModule;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerSkillModule = playerObj.GetComponent<PlayerSkillModule>();
        }
        skillImage= GetComponent<Image>();
        if (skillImage != null)
        {
            skillImage.sprite = skillIcon;
        }
    }

    private void Update()
    {
        if (playerSkillModule == null) return;
        
        Skill targetSkill = playerSkillModule.GetSkill(skillIndex);
        if (targetSkill != null)
        {
            // Hiển thị thời gian hồi chiêu thay cho vòng tròn
            if (cooldownText != null)
            {
                if (targetSkill.IsSkillChargeOnCooldown())
                {
                    if (!cooldownText.gameObject.activeSelf) cooldownText.gameObject.SetActive(true);
                    cooldownText.text = targetSkill.GetCurrentChargeCooldownValue().ToString("F1");
                }
                else
                {
                    if (cooldownText.gameObject.activeSelf) cooldownText.gameObject.SetActive(false);
                }
            }

            // Hiển thị số lượng Charge còn lại
            if (chargeText != null)
            {
                if (targetSkill.IsChargeSkill())
                {
                    if (!chargeText.gameObject.activeSelf) chargeText.gameObject.SetActive(true);
                    chargeText.text = $"{targetSkill.GetCurrentCharge()}/{targetSkill.GetMaxCharge()}";
                }
                else
                {
                    // Nếu không phải skill có nhiều charge thì không cần hiện số charge
                    if (chargeText.gameObject.activeSelf) chargeText.gameObject.SetActive(false);
                }
            }
        }
    }
    
    public void OnSkillButtonClick()
    {
        if (playerSkillModule == null) return;
        Skill targetSkill = playerSkillModule.GetSkill(skillIndex);
        if(!targetSkill.IsSkillReady())
        {
            Debug.Log("Skill is on cooldown or not ready.");
            return;
        }
        if (targetSkill != null)
        {
            targetSkill.UseSkill(playerSkillModule.gameObject, playerSkillModule.getOutPutPos().position);
        }
    }
}
