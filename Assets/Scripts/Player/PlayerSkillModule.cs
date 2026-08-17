using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputSettings;

public class PlayerSkillModule : PlayerModule
{
    [SerializeField] private List<SkillActionMapping> SkillMapping= new List<SkillActionMapping>();
    private List<SkillObjectKeyMapping> SkillActions= new List<SkillObjectKeyMapping>();
    [SerializeField] private Transform skillOutput;
    void Start()
    {
        for (int i = 0; i < SkillMapping.Count; i++)
        {
            SkillObjectKeyMapping mapping;
            mapping.action = SkillMapping[i].action;
            mapping.skill = Instantiate(SkillMapping[i].skillprefab, transform);
            SkillActions.Add(mapping);
            mapping.action.Enable();
        }
    }
    
    [System.Serializable]
    struct SkillActionMapping
    {
        public InputAction action;
        public Skill skillprefab;
    }

    struct SkillObjectKeyMapping
    {
        public InputAction action;
        public Skill skill;
    }

    public override void UpdateModule()
    {
        for (int i = 0; i < SkillActions.Count; i++)
        {
            if (SkillActions[i].action.WasPressedThisFrame())
            {
                if (SkillActions[i].skill.IsSkillReady())
                {
                    SkillActions[i].skill.UseSkill(gameObject, skillOutput.position);
                }
            }
        }

        for (int i=0;i< SkillActions.Count;i++)
        {
            SkillActions[i].skill.UpdateSkillCooldown(Time.deltaTime);
        }
    }

    public void DisableAllSkill()
    {
        for (int i = 0; i < SkillActions.Count; i++)
        {
            SkillActions[i].action.Disable();
        }
    }

    public void EnableAllSkill()
    {
        for (int i = 0; i < SkillActions.Count; i++)
        {
            SkillActions[i].action.Enable();
        }
    }

    public Skill GetSkill(int index)
    {
        if (index >= 0 && index < SkillActions.Count)
        {
            return SkillActions[index].skill;
        }
        return null;
    }

    public Transform getOutPutPos()
    {
        return skillOutput;
    }
}
