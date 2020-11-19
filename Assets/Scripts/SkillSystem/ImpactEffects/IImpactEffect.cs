using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Skill
{
    /// <summary>
    /// 技能影响算法接口
    /// </summary>
    public interface IImpactEffect
    {
        //伤害生命
        void Execute(SkillDeployer deployer);

    }
}
