using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Character;

namespace ARPG.Skill
{
    public class CostSpImpactEffect :IImpactEffect
    {
        public void Execute(SkillDeployer deployer)
        {
            var status = deployer.SkillData.owner.GetComponent<CharacterStatus>();
            status.Sp -= deployer.SkillData.costSp;
        }

    }
}
