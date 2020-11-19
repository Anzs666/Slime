using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.FSM;

public class AttackingState : FSMState
{
    private bool isAttack = false;
    private float lastAttackTime;
    public override void Init()
    {
        stateID = FSMStateID.Attacking;
    }

    public override void ActionState(FSMBase fsm)
    {
        base.ActionState(fsm);
        Vector3 attackMovement = Vector3.zero;
        //攻击间隔判断
        if (!isAttack && lastAttackTime + fsm.chStatus.attackInterval < Time.time)
        {
            attackMovement = (fsm.SelectTargetByDistanceMin(fsm.chStatus.scoutDistance).position - fsm.transform.position).normalized * 10;
            //给一个初始初速度
            fsm.chMotor.SetVelocity(attackMovement);
            //设置动画
            fsm.anim.SetTrigger(fsm.chStatus.chParameter.Attack01);
            fsm.anim.SetFloat("Attack01_Angle", attackMovement.x);
            //速度延时停止
            fsm.AttackStop(0.5f);
            lastAttackTime = Time.time;
        }

    }
}
