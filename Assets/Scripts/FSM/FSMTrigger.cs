using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    public abstract class FSMTrigger
    {
        public FSMTriggerID TriggerID { get; set; }

        public FSMTrigger()
        {
            Init();
        }

        /// <summary>
        /// 初始化必要条件，为编号赋值
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// 处理逻辑
        /// </summary>
        /// <returns></returns>
        public abstract bool HandleTrigger(FSMBase fsm);

    }
}