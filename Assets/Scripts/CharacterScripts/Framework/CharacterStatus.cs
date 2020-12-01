using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Character
{
    public class CharacterStatus : MonoBehaviour
    {
        public CharacterAnimationParameter chParameter;
        [Tooltip("移动速度")]
        public float Speed;
        [Tooltip("血量")]
        public float Hp;
        [Tooltip("最大血量")]
        public float maxHp;
        [Tooltip("法力")]
        public float Sp;
        [Tooltip("最大法力")]
        public float maxSp;
        [Tooltip("基础攻击力")]
        public float baseATK;
        [Tooltip("基础攻击力")]
        public float basedefence;
        [Tooltip("攻击间隔")]
        public float attackInterval;
        [Tooltip("攻击距离")]
        public float attackDistance;
        [Tooltip("监视范围")]
        public float scoutDistance;

        /// <summary>
        /// 受击
        /// </summary>
        /// <param name="damage"></param>
        public void BeAttacked(float damage)
        {
            damage -= basedefence;
            Hp -= damage;
        }

        public void Death()
        {
            GetComponentInChildren<Animator>().SetBool(chParameter.Death,true);
        }

    }
}
