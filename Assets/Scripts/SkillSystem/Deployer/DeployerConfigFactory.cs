using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Skill
{
    /// <summary>
    /// 释放器配置工厂：提供创建释放器各种算法对象功能
    /// 作用:将对象的创建与使用分离
    /// </summary>
    public class DeployerConfigFactory
    {
        public static Dictionary<string, object> cache=new Dictionary<string, object>();

        public static IAttackSelector CreateAttackSelector(SkillData skillData)
        {
            //选取（单个）
            //选取对象命名规则
            //ARPGDemo.Skill+枚举名+AttackSelector
            string className = string.Format("ARPG.Skill.{0}AttackSelector", skillData.selectorType);
            return CreateObject<IAttackSelector>(className);
        }

        public static IImpactEffect[] CreateImpactEffects(SkillData skillData)
        {
            IImpactEffect[] impactArray = new IImpactEffect[skillData.impactType.Length];

            //影响(数组类型)
            //影响效果命名规则
            //ARPG.Skill.+impactTypy[?]+Impact
            for (int i = 0; i < skillData.impactType.Length; i++)
            {               
                string classNameImapct = string.Format("ARPG.Skill.{0}ImpactEffect", skillData.impactType[i]);           
                impactArray[i] = CreateObject<IImpactEffect>(classNameImapct);
            }
            return impactArray;
        }

        /// <summary>
        /// 反射获取类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="className"></param>
        /// <returns></returns>
        private static T CreateObject<T>(string className) where T : class
        {
            if (!cache.ContainsKey(className))//避免重复创建
            {

                Type type = Type.GetType(className);
                object instance = Activator.CreateInstance(type);
                cache.Add(className, instance);
              
            }
            return cache[className] as T;
        }
    }
}