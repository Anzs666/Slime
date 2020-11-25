using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.FSM;
using System;

public class RandomMoveState : FSMState
{
    private float randomMoveLastChangeTime = float.MinValue;
    private float randomMoveChangeInterval = 1f;
    private float moveX=0, moveY=0;

    public override void Init()
    {
        stateID = FSMStateID.RandomMove;
    }
    public override void ActionState(FSMBase fsm)
    {
        base.ActionState(fsm);
        //随机移动
        if (randomMoveLastChangeTime + randomMoveChangeInterval < Time.time)
        {
            //fsm.chMotor.MoveSpeed=fsm.chStatus.Speed;
            //随机改变下一次间隙             
            float Angle = UnityEngine.Random.Range(0, 360);
            moveX = (float)Math.Cos(Angle);
            moveY = (float)Math.Sin(Angle);     
            randomMoveChangeInterval = UnityEngine.Random.Range(0.5f, 1.5f);
            randomMoveLastChangeTime = Time.time;
        }
        //fsm.chMotor.Movement(new Vector3(moveX,  moveY,0));
    }
}
