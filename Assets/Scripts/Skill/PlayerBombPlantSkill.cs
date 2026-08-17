using UnityEngine;

public class PlayerBombPlantSkill : Skill
{
    [SerializeField] private PlayerBomb bomb;

    public override void UseSkill(GameObject activator, Vector3 targetPosition)
    {
        if(IsSkillReady())
        {
            UseSkillCharge();
            float damage = CalculateSkillDamage(activator);
            ObjectPooling.Instance.InstantiateObject(bomb.gameObject, targetPosition+activator.transform.forward.normalized*0.8f, Quaternion.identity).GetComponent<PlayerBomb>().Initialize(damage);
        }
    }
}
