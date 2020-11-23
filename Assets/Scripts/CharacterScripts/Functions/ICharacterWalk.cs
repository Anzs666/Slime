using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Character.Function
{
    public interface ICharacterWalk 
    {
        //移动
        void HorizontalMove(float addSpeed);
        void DirectMove(Vector2 direction);

    }
}
