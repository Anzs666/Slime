using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.FSM;

public class IdleState : FSMState
{
    public override void Init()
    {
        stateID = FSMStateID.Idle;
    }

    public override void EnterState(FSMBase fsm)
    {
        base.EnterState(fsm);
        //播放待机动画
        fsm.anim.SetBool(fsm.chStatus.chParameter.Idle, true);
    }


    public override void ExitState(FSMBase fsm)
    {
        base.EnterState(fsm);
        //播放待机动画
        fsm.anim.SetBool(fsm.chStatus.chParameter.Idle, false);
    }
}
