using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.FSM;

public class BatFSM :FSMBase
{
    public override void ConfigFSM()
    {
        base.ConfigFSM();
        states = new List<FSMState>();
        //创建状态对象 
        //添加映射（AddMap）*
        //加入状态机
        //--------------------         
        //随机移动
        RandomMoveState randomMove = new RandomMoveState();
        randomMove.AddMap(FSMTriggerID.ScoutedTargetIn, FSMStateID.Follow);
        randomMove.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        states.Add(randomMove);
        //跟随
        FollowState follow = new FollowState();
        follow.AddMap(FSMTriggerID.ScoutedTargetOut, FSMStateID.RandomMove);
        follow.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        follow.AddMap(FSMTriggerID.AttackTargetIn, FSMStateID.Attacking);
        states.Add(follow);
        //攻击
        AttackingState attacking = new AttackingState();
        attacking.AddMap(FSMTriggerID.AttackTargetOut, FSMStateID.Follow);
        attacking.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        states.Add(attacking);
        //站立
        IdleState idle = new IdleState();
        //idle.AddMap(FSMTriggerID.)
        idle.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        states.Add(idle);
        //死亡
        DeadState dead = new DeadState();
        states.Add(dead);
    }
}
