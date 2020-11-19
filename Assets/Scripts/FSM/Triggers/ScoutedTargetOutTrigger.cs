using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{ 
    /// <summary>
    /// 判断退出监视范围
    /// </summary>
    public class ScoutedTargetOutTrigger :FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.SelectTargetByDistance(fsm.chStatus.scoutDistance).Length == 0;
        }
        public override void Init()
        {
            TriggerID = FSMTriggerID.ScoutedTargetOut;
        }
    }
}
