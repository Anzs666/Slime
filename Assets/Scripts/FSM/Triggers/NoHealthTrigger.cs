using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{ 
    /// <summary>
    /// 血量判断的条件
    /// </summary>
    public class NoHealthTrigger : FSMTrigger
    {

        public override void Init()
        {
            TriggerID = FSMTriggerID.NoHealth;
        }

        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.chStatus.Hp <= 0;
        }

    }

}
