using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPG.Character;
using System;

namespace ARPG.Skill
{
    public class RectangleAttackSelector : IAttackSelector
    {
        public Transform[] SelectTarget(SkillData skillData, Transform skillTF)
        {
            //筛选目标
            List<Transform> targets = new List<Transform>();
            for (int i = 0; i < skillData.attackTargetTags.Length; i++)
            {
                GameObject[] tempGoArray = GameObject.FindGameObjectsWithTag(skillData.attackTargetTags[i]);
                targets.AddRange(tempGoArray.Select(g => g.transform));
            }
            //判断攻击范围
            //正余玄定理取绝对值
            targets = targets.FindAll(t =>
            Math.Abs(Vector3.Distance(t.position, skillTF.position) * Math.Cos(Vector3.Angle(t.position - skillTF.position, skillTF.right)))
             < skillData.attackDistance &&
             Math.Abs(Vector3.Distance(t.position, skillTF.position) * Math.Sin(Vector3.Angle(t.position - skillTF.position, skillTF.right)))
             < skillData.attackBroad/2
           );
            //筛选活的敌人
            targets = targets.FindAll(t => t.GetComponent<CharacterStatus>().Hp > 0);
            //返回单攻群攻
            Transform[] result = targets.ToArray();
            if (skillData.attackType == SkillAttackType.Group || result.Length == 0)
                return result;
            Transform min = result.GetMin(t => Vector3.Distance(t.position, skillTF.position));
            return new Transform[] { min };
        }
    }
}
