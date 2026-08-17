using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "ScriptableObjects/SkillSO", order = 3)]
public class SkillSO : ScriptableObject
{
    public int maxCharge;
    public float chargeCooldown;
    public float skillDammage;
    public float betweenChargeCooldown;
}
