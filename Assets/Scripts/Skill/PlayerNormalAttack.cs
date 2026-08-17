using UnityEngine;

public class PlayerNormalAttack : Skill
{
    [SerializeField] private PlayerBullet projectilePrefab;
    public override void UseSkill(GameObject activator, Vector3 targetPosition)
    {
        if(IsSkillReady())
        {
            UseSkillCharge();
            Vector2 direction2D = new Vector2(activator.transform.forward.x, activator.transform.forward.z).normalized;
            Vector2 Direction1 = GeometryHelper.plusAdirectionByAngle(direction2D, 15f);
            Vector2 Direction2 = GeometryHelper.plusAdirectionByAngle(direction2D, -15f);
            float damage = CalculateSkillDamage(activator);
            ObjectPooling.Instance.InstantiateObject(projectilePrefab.gameObject, targetPosition, Quaternion.LookRotation(new Vector3(Direction1.x, 0, Direction1.y))).GetComponent<PlayerBullet>().Initialize(damage, new Vector3(Direction1.x, 0, Direction1.y));
            ObjectPooling.Instance.InstantiateObject(projectilePrefab.gameObject, targetPosition, Quaternion.LookRotation(new Vector3(Direction2.x, 0, Direction2.y))).GetComponent<PlayerBullet>().Initialize(damage, new Vector3(Direction2.x, 0, Direction2.y));
            ObjectPooling.Instance.InstantiateObject(projectilePrefab.gameObject, targetPosition,Quaternion.LookRotation(new Vector3(direction2D.x, 0, direction2D.y))).GetComponent<PlayerBullet>().Initialize(damage, new Vector3(direction2D.x, 0, direction2D.y));
        }
    }
}