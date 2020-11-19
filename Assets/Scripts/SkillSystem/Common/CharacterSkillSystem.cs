using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace ARPG.Skill
{
    /// <summary>
    /// 将技能系统封装成类
    /// </summary>
    [RequireComponent(typeof(CharacterSkillManager))]
    public class CharacterSkillSystem : MonoBehaviour
    {
        private CharacterSkillManager skillManager;
        private Animator anim;

        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
            anim = GetComponentInChildren<Animator>();
           // GetComponent<AnimationEventBehaviour>()+=
        }

        public void AttackUseSkill(int id)
        {
            //准备技能
            SkillData skillData = skillManager.PrepareSkill(id);
            if (skillData == null) return;
            //生成技能
            skillManager.GenerateSkill(skillData);
            //播放动画
            if(skillData.animationName!=string.Empty)
            anim.SetTrigger(skillData.animationName);          
            //如果单攻
            //朝向目标
            //选中目标
        }

        /// <summary>
        /// 使用随机技能（为NPC提供）
        /// </summary>
        public void UseRandomSkill()
        {
            //从管理器中挑选出随机的技能
            //先产生随机数，在判断技能是否可以释放
            //先筛选出所有可以释放的技能。在产生随机数
            var usableSkills = skillManager.skills.FindAll(s => skillManager.PrepareSkill(s.skillID) != null);
            if (usableSkills.Length == 0) return;
        }
    }

}