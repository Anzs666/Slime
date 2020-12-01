using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ARPG.Character
{
    public struct BoxCollidersOS{
        public Vector2[] offests;
        public Vector2[] sizes;
        public BoxCollidersOS(Vector2[] _offest,Vector2[] _size)
        {
            this.offests = _offest;
            this.sizes = _size;
        }
    };
    public class PlayerStatus : CharacterStatus
    {
        [Tooltip("加速度")]
        public float AddSpeed;
        [Tooltip("跳跃力度")]
        public float JumpForce;
        [Tooltip("跳跃最大次数")]
        public int JumpMaxCout;
        [HideInInspector]
        private bool isChangingShape = true;
        public bool IsChangingShape { get => isChangingShape; }
        public void ChangeShape() { isChangingShape = !isChangingShape; }
        [Tooltip("碰撞体预制参数")]
        public BoxCollidersOS BCOSs= new BoxCollidersOS( 
            new Vector2[2] 
        { new Vector2(0.00018f, 0.04397f),new Vector2(-0.06947f,1.10380f) }
        ,new Vector2 [2]
        { new Vector2(1.25530f,0.67094f),new Vector2(1.38104f,2.72325f)});
    }
}