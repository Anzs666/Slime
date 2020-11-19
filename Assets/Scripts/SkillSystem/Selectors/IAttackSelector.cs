using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Skill
{
    //攻击选区接口
    public interface IAttackSelector
    {
        /// <summary>
        /// 搜索目标
        /// </summary>
        /// <param name="skillData"></param>
        /// <param name="skillTF"></param>
        /// <returns></returns>
        Transform[] SelectTarget(SkillData skillData,Transform skillTF);
    }
}
