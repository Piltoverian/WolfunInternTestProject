using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "PlayerBaseStatSO", menuName = "ScriptableObjects/PlayerBaseStatSO", order = 2)]
public class PlayerBaseStatSO : ScriptableObject
{
    public float baseMaxHealth;
    public float baseSpeed;
    public float rotationSpeed;
    public float baseArmor;
    public float baseDamageMul;
    public float dashSpeed;
    public float perLevelHealth;
    public float perLevelArmor;
    public float perLevelDamageMul;
}
