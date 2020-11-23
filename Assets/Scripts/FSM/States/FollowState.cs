using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.FSM;

//跟随装态
public class FollowState : FSMState
{
    private float followSpeed = 3.5f;
    public override void Init()
    {
        stateID = FSMStateID.Follow;
    }
    public override void EnterState(FSMBase fsm)
    {
        base.EnterState(fsm);
        fsm.anim.SetTrigger(fsm.chStatus.chParameter.FindTarget);
    }
    public override void ActionState(FSMBase fsm)
    {
        base.ActionState(fsm);
        //获取最小值
        Vector3 movement=(fsm.SelectTargetByDistanceMin(fsm.chStatus.scoutDistance).position - fsm.transform.position).normalized;
        //fsm.chMotor.MoveSpeed=followSpeed;
        //Debug.Log(movement);
        //fsm.chMotor.Movement(movement);
    }
}
