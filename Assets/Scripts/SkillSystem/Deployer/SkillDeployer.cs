using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Skill
{
    /// <summary>
    /// 技能释放器(所有释放器父类)
    /// </summary>
    public abstract class SkillDeployer : MonoBehaviour
    {
        private SkillData skillData;

        public SkillData SkillData
        {
            get
            {
               return skillData;
            }
            set {
                skillData = value;
                InitDeployer();
            }
        }

        private IAttackSelector attackSelector;
        private IImpactEffect[] impactArray;

        /// <summary>
        /// 初始化释放器
        /// </summary>
        private void InitDeployer()
        {
            //创建算法对象
            //skillData.selectorType;            
           
            //选取（单个）
            attackSelector = DeployerConfigFactory.CreateAttackSelector(skillData);
            //影响(数组类型)  
           impactArray = DeployerConfigFactory.CreateImpactEffects(skillData);

        }

        /// <summary>
        /// /执行选区算法对象
        /// </summary>
        protected void CalculateTargets()
        {
            skillData.attackTargets=attackSelector.SelectTarget(skillData, transform);
        }


        /// <summary>
        /// 影响效果算法接口实现
        /// </summary>
        protected void ImpactTargets()
        {
            for (int i = 0; i < impactArray.Length; i++)
            {
                impactArray[i].Execute(this);
            }
        }

        /// <summary>
        /// 释放方法
        /// 供技能管理器调用，子类实现，定义具体释放内容
        /// </summary>
        public abstract void DeploySkill();
    }
}
