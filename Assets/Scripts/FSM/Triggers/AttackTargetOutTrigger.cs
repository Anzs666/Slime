using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    /// <summary>
    /// 敌人走出攻击范围
    /// </summary>
    public class AttackTargetOutTrigger : FSMTrigger
    {
        public override bool HandleTrigger(FSMBase fsm)
        {
            return fsm.SelectTargetByDistance(fsm.chStatus.attackDistance).Length == 0;
        }
        public override void Init()
        {
            TriggerID = FSMTriggerID.AttackTargetOut;
        }
    }
}
