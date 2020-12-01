using System;
using System.Collections;
using System.Collections.Generic;
using ARPG.Character;
using ARPG.Skill;
using Common;
using UnityEngine;
using UGUI.Package;

namespace AI.FSM
{   /*
    程序执行流程：
    获取人物基本信息
    状态机检测当前状态——>状态类遍历所有条件对象——>满足条件——>状态机切换当前状态
    */
    /// <summary>
    /// 状态机
    /// </summary>
    public class FSMBase : MonoBehaviour
    {
        //储存状态
        public List<FSMState> states;

        [Tooltip("默认状态编号")]
        public FSMStateID defaultStateID;
        //默认的状态
        private FSMState defaultState;

        [SerializeField]//将私有字段在编译器中显示
        private FSMState currentState;

        [HideInInspector]
        public Animator anim;
        [HideInInspector]
        public CharacterStatus chStatus;
        [HideInInspector]
        public CharacterMotor chMotor;

        void Start()
        {
            InitComponent();
            ConfigFSM();
            InitDefaultState();
        }
        /// <summary>
        /// 配置状态机
        /// </summary>
        public virtual void ConfigFSM()
        {
  
        }


        /// <summary>
        /// 初始化默认状态
        /// </summary>
        private void InitDefaultState()
        {
            defaultState = states.Find(s => s.stateID == defaultStateID);
            currentState = defaultState;
            //进入状态
            currentState.EnterState(this);

        }

        /// <summary>
        /// 组件初始化
        /// </summary>
        public void InitComponent()
        {
            anim = GetComponentInChildren<Animator>();
            chStatus = GetComponent<CharacterStatus>();
            chMotor = GetComponent<CharacterMotor>();
        }
        void Update()
        {
            currentState.Reason(this);
            currentState.ActionState(this);
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="stateID"></param>
        public void ChangeActiveState(FSMStateID stateID)
        {
            //离开上一个状态
            currentState.ExitState(this);
            //如果切换的状态是默认状态
            currentState = stateID == FSMStateID.Default ? defaultState : states.Find(s => s.stateID == stateID);
            //进入下一个状态
            currentState.EnterState(this);
        }
        #region 供state调用
        private Transform[] targets;
        public string targetTag = "Player";
        //通过距离搜索合适的目标
        public Transform[] SelectTargetByDistance(float distance)
        {
            //选择标签
            List<Transform> targets = new List<Transform>();
            GameObject[] tempGoArray = GameObject.FindGameObjectsWithTag(targetTag);
            targets.AddRange(tempGoArray.Select(g => g.transform));
            //判断攻击范围
            targets = targets.FindAll(t =>
              Vector3.Distance(t.position, transform.position) < distance
            );
            //选择最近的目标
            Transform[] result = targets.ToArray();
            return result;
        }

        //获取最小
        public Transform SelectTargetByDistanceMin(float distance)
        {
            return SelectTargetByDistance(distance).GetMin(t => Vector3.Distance(t.position, transform.position));
        }

        public void AttackStop(float time)
        {
            StartCoroutine(AttackStopDelay(time));
        }

        private IEnumerator AttackStopDelay(float time)
        {
            yield return new WaitForSeconds(time);
            chMotor.SetVelocity(Vector3.zero);
        }

        //死亡
        public void Dead(float time)
        {
            StartCoroutine(DeadDelay(time));
        }

        //延时死亡
        private IEnumerator DeadDelay(float time)
        {
            yield return new WaitForSeconds(time);
            //生成物品
            ItemGenerator.Instance.CreateItemGameEntityAtPos(transform,1);
            this.enabled = false;
            Destroy(gameObject);
        }
        #endregion
    }
}