using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Character.Function
{
    public interface ICharacterJump
    {
        void ResetJumpMaxCout(CharacterStatus status);
        void AddJumpMaxCout(int num);
        void SetJumpForce(float force);
        void Jump();
    }
}
