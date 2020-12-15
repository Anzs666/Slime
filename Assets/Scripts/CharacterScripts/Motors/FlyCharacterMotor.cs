using System.Collections;
using System.Collections.Generic;
using ARPG.Character.Function;
using UnityEngine;
namespace ARPG.Character
{
    /// <summary>
    /// 飞行驱动器
    /// </summary>
    public class FlyCharacterMotor : CharacterMotor,ICharacterFly
    {
        private float flySpeed;
        public override void InitMotor(CharacterStatus _status)
        {
            flySpeed = _status.Speed;
            //重力设置为零
            rigb2D.gravityScale = 0;
        }
        public void Fall()
        {
            //重力设置为1
            rigb2D.gravityScale = 1;
        }
        public void DirectMove(Vector2 direct)
        {
            rigb2D.velocity = direct.normalized* flySpeed;
        }
    }
}
