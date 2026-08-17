using UnityEngine;

public class PlayerExplodeDash : Skill
{
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float explosionRadius = 3f;

    public override void UseSkill(GameObject activator, Vector3 targetPosition)
    {
        if(IsSkillReady())
        {
            UseSkillCharge();
            if(activator.gameObject.activeSelf&&gameObject.activeSelf)
            StartCoroutine(DashRoutine(activator));
        }
    }

    private System.Collections.IEnumerator DashRoutine(GameObject activator)
    {
        PlayerMovement movement = activator.GetComponent<PlayerMovement>();
        PlayerSkillModule skillmodule=activator.GetComponent<PlayerSkillModule>();
        if (movement != null)
        {
            movement.EnableDashing();
            skillmodule.DisableAllSkill();
            yield return new WaitForSeconds(dashDuration);
            
            movement.DisableDashing();
            skillmodule.EnableAllSkill();
            
            Explode(activator);
        }
    }

    private void Explode(GameObject activator)
    {
        float damage = CalculateSkillDamage(activator);
        
        float scaledRadius = explosionRadius * GameConfig.Instance.GetGameConfigSO().UnityUnitPerGameUnit;
        Collider[] colliders = Physics.OverlapCapsule(activator.transform.position - Vector3.up * scaledRadius * 2, activator.transform.position + Vector3.up * scaledRadius * 2, scaledRadius);
        foreach (Collider nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Enemy"))
            {
                nearbyObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }


        GameObject debugSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        debugSphere.transform.position = activator.transform.position;
        debugSphere.transform.localScale = new Vector3(scaledRadius * 2, scaledRadius * 2, scaledRadius * 2);
        

        Renderer rend = debugSphere.GetComponent<Renderer>();
        if (rend != null) {
            rend.material.color = new Color(1f, 0f, 0f, 0.3f);

        }

        Destroy(debugSphere.GetComponent<Collider>());
        Destroy(debugSphere, 0.2f);
    }
}
