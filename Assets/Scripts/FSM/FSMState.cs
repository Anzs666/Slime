using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{

    public abstract class FSMState
    {
        public FSMStateID stateID { get; set; }

        //映射表
        private Dictionary<FSMTriggerID, FSMStateID> map;
        //条件列表
        private List<FSMTrigger> Triggers;
        
        public FSMState()
        {
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            Triggers = new List<FSMTrigger>();
            Init();

        }

        /// <summary>
        /// *检测当前状态是否满足
        /// </summary>
        public void Reason(FSMBase fsm)
        {
            for (int i = 0; i < Triggers.Count; i++)
            {
                //如果返回值为true
                if (Triggers[i].HandleTrigger(fsm))
                {
                    //从表中获取输出状态
                    FSMStateID stateID = map[Triggers[i].TriggerID];
                    //切换状态
                    fsm.ChangeActiveState(stateID);
                    return;
                }
            }
        }

        public abstract void Init();

        /// <summary>
        /// 由状态机调用
        /// </summary>
        /// <param name="triggerID"></param>
        /// <param name="stateID"></param>
        public void AddMap(FSMTriggerID triggerID,FSMStateID stateID)
        {
            //条件对应id
            map.Add(triggerID, stateID);
            //创建
            CreateTrigger(triggerID);
        }

        private void CreateTrigger(FSMTriggerID triggerID)
        {
        //反射获取类型
            Type type = Type.GetType("AI.FSM."+triggerID+"Trigger");
            FSMTrigger trigger = Activator.CreateInstance(type) as FSMTrigger;
            Triggers.Add(trigger);
        }
        
        //有些不一定要实现的用virtual
        public virtual void EnterState(FSMBase fsm) { }

        public virtual void ActionState(FSMBase fsm) { }

        public virtual void ExitState(FSMBase fsm) { }
	}
}
