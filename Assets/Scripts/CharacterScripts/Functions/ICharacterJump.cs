using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Character.Function
{
    public interface ICharacterJump
    {
        void SetJumpForce(float force);     
        void Jump();
    }
}
