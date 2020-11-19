using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Skill
{
    [Serializable]
    public class SkillData
    {
        /// 技能Id
        public int skillID;
        /// 技能名字
        public string name;
        /// 技能描述
        public string description;
        //冷却时间
        public float coolTime;
        //冷却剩余时间
        public float coolRemain;
        //消耗法力
        public int costSp;
        //攻击距离
        public float attackDistance;
        //攻击宽度
        public float attackBroad;
        //攻击角度
        public float attackAngle;
        //攻击目标tag
        public string[] attackTargetTags = { "Enemy" };
        //攻击目标对象数组
        [HideInInspector]
        public Transform[] attackTargets;
        //技能影响类型
        public string[] impactType = { "CostSp", "Damage" };
        //连击的下一个技能编号
        public int nextBatterId;
        //持伤害比率
        public float atkRatio;
        //持续时间
        public float durationTime;
        //伤害间隔
        public float atkInterval;
        //技能所属
        [HideInInspector]
        public GameObject owner;
        //技能预制件名
        public string prefabName;
        //技能预制件
        [HideInInspector]
        public GameObject skillprefab;
        //技能动画名称
        public string animationName;
        //受击特效名称
        public string hitFxName;
        //受击特效预制件
        [HideInInspector]
        public GameObject hitFxPrefab;
        //技能等级
        public int level;
        ////攻击类型，单体、群攻
        public SkillAttackType attackType;
        ////类型选择 扇形
        public SelectorType selectorType;

    }
}