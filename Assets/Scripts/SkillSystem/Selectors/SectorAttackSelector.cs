using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPG.Character;

namespace ARPG.Skill
{
    public class SectorAttackSelector : IAttackSelector
    {
        public Transform[] SelectTarget(SkillData skillData, Transform skillTF)
        {
            //获取所有目标   
            //skillData.attackTargetTags
            //string[]->Transform[]
            List<Transform> targets = new List<Transform>();
            for (int i = 0; i < skillData.attackTargetTags.Length; i++)
            {
                GameObject[] tempGoArray = GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
                targets.AddRange(tempGoArray.Select(g => g.transform));
            }

            //判断攻击范围（）
            targets=targets.FindAll(t =>
            Vector3.Distance(t.position, skillTF.position) < skillData.attackDistance &&
            Vector3.Angle(skillTF.right, t.position - skillTF.position) <= skillData.attackAngle / 2         
            );
           
            //筛选出活的敌人
            targets = targets.FindAll(t => t.GetComponent<CharacterStatus>().Hp > 0);
            //返回目标（单攻/群攻）
            Transform[] result = targets.ToArray();
            if (skillData.attackType == SkillAttackType.Group||result.Length==0)
                return result;
            Transform min = result.GetMin(t => Vector3.Distance(t.position, skillTF.position));
            return new Transform[] { min };
        }
    }
}
