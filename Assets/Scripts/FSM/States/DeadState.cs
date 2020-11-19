using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.FSM;

public class DeadState : FSMState
{
    public override void Init()
    {
        stateID = FSMStateID.Dead;
    }

    public override void EnterState(FSMBase fsm)
    {
        base.EnterState(fsm);
        fsm.anim.SetBool(fsm.chStatus.chParameter.Death, true);
        fsm.Dead(0.52f);
    }
}
