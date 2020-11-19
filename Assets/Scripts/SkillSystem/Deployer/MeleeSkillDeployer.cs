using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Skill
{
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeploySkill()
        {
            CalculateTargets();
            ImpactTargets();
        }
    }

}