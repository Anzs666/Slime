using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using ARPG.Character;

namespace ARPG.Skill
{
    public class CharacterSkillManager : MonoBehaviour
    {
        //技能列表
        public SkillData[] skills;
        public Transform GeneratePoint;

        private void Start()
        {
            for (int i = 0; i < skills.Length; i++)
            {
                initSkill(skills[i]);
            }
        }

        /// <summary>
        /// 技能初始化
        /// </summary>
        /// <param name="data"></param>
        private void initSkill(SkillData data)
        {          
            data.skillprefab = ResourcesManager.Load<GameObject>(data.prefabName);
            data.owner = gameObject;
        }

        /// <summary>
        /// 准备技能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SkillData PrepareSkill(int id)
        {
            SkillData data = SkillFind(s => s.skillID == id);
            //获取当前角色法力值
            float sp = GetComponent<CharaterStatus>().Sp;
            //判断条件
            if (data != null && data.coolRemain <= 0 && data.costSp <= sp)
                return data;
            else
                return null;
        }

        private SkillData SkillFind(Func<SkillData,bool>handler)
        {
            for (int i = 0; i < skills.Length; i++)
            {
                if (handler(skills[i]))
                {
                    return skills[i];
                }
            }
            return null;
        }


        /// <summary>
        /// 生成技能
        /// </summary>
        public void GenerateSkill(SkillData data)
        {
            //Debug.Log(data.prefabName+data.skillprefab.name);
            //创建技能
            GameObject skillGo= GameObjectPool.Instance.CreateObject(data.prefabName,data.skillprefab, transform.position, GeneratePoint.rotation);
            //获取技能释放器
            SkillDeployer deployer = skillGo.GetComponent<SkillDeployer>();
            //传递技能数据给释放器
            deployer.SkillData = data;//内部创建算法对象
            deployer.DeploySkill();//内部执行算法对象
            //延时销毁重置技能
            GameObjectPool.Instance.CollectObject(skillGo,data.durationTime);
            //开启技能冷却
            StartCoroutine(CoolTimeDown(data));
        }

        /// <summary>
        ///技能冷却
        /// </summary>
        /// <param name="data"></param>
        private IEnumerator CoolTimeDown(SkillData data)
        {
            data.coolRemain = data.coolTime;
            while (data.coolRemain>0)
            {
                yield return new WaitForSeconds(0.1f);
                data.coolRemain-=0.1f;
            }
        }
    }
}
