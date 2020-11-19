using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.FSM
{
    /// <summary>
    /// 判断进入监视范围
    /// </summary>
    public class ScoutedTargetInTrigger:FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.SelectTargetByDistance(fsm.chStatus.scoutDistance).Length != 0;
        }
        public override void Init()
        {
            TriggerID = FSMTriggerID.ScoutedTargetIn;
        }

    }
}
