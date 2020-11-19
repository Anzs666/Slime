using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Skill
{
    //远程技能释放器
    public class RemoteSkillDeployer : SkillDeployer
    {
        
        /// <summary>
        /// 技能内部的算法
        /// </summary>
        public override void DeploySkill()
        {
            //transform.SetParent(SkillData.owner.transform);

            CalculateTargets();

            ImpactTargets();
           
        }
    }

}

