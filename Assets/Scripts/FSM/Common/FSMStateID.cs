using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI.FSM
{
    public enum FSMStateID
    {
        None,
        Default,
        Dead,
        Idle,     
        Follow,
        Attacking,
        RunAway,
        RandomMove,
    }
}
