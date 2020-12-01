using System.Collections;
using System.Collections.Generic;
using ARPG.Character;
using UnityEngine;
using gojiu.slim;

namespace ARPG.Skill
{
    public class DamageImpactEffect :IImpactEffect
    {
        public void Execute(SkillDeployer deployer)
        {
            foreach (var t in deployer.SkillData.attackTargets)
            {
                float damage= deployer.SkillData.atkRatio*0.01f* deployer.SkillData.owner.transform.GetComponent<CharacterStatus>().baseATK;
                t.GetComponent<CharacterStatus>().Hp -= damage;
                //t.GetComponent<DamageNumGenerator>().CreateDamageNum(damage,1f,0.5f);
                t.GetComponent<Enemy>().SetHurtFlash();
            }
        }
    }

}