using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Character.Function;
namespace ARPG.Character
{
    public class PlayerMotor : CharacterMotor, ICharacterJump, ICharacterWalk
    {
        //水平移动
        private float moveSpeed;
        public float MoveSpeed
        {
            set { moveSpeed = value < MAXMoveSpeed ? value : MAXMoveSpeed; }
        }
        private float addSpeed;
        private float MAXMoveSpeed;
        public float MAXMOVESpeed { get => MAXMoveSpeed; }
        private float rotateSpeed = 0f;
        //跳跃
        private int jumpCout=0;
        private int jumpMaxCout = 1;
        private float jumpForce = 0f;
        private float fallMultiplier = 2.5f;
        private float lowJumpMultiplier = 2f;
        private bool jumping = false;
        public bool Jumping { get => jumping; set => jumping = value; }

        private void FixedUpdate()
        {
            Fall();
            CheckGround();
            //计数为0
            if (IsGround)
            {
                jumpCout = 0;
            }
        }
        public override void InitMotor(CharacterStatus status)
        {
            PlayerStatus _status = status as PlayerStatus;
            this.addSpeed = _status.AddSpeed;
            this.MAXMoveSpeed = _status.Speed;
            this.jumpForce = _status.JumpForce;
            this.jumpMaxCout = _status.JumpMaxCout;
        }

        private void Fall()
        {
            //下落
            if (rigb2D.velocity.y < 0)
            {
                rigb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rigb2D.velocity.y > 0 && Jumping)
            {
                rigb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

        }
        //跳跃
        public void Jump()
        {
            if (IsGround)
            {
                rigb2D.velocity = new Vector2(rigb2D.velocity.x, jumpForce);
            }
            else if (jumpCout < jumpMaxCout-1)
            {
                //Debug.Log(jumpCout);
                rigb2D.velocity = new Vector2(rigb2D.velocity.x, jumpForce);
                jumpCout++;
            }
        }
        //水平移动
        public void HorizontalMove(float dx)
        {
            if (dx != 0)
            {
                float vx = rigb2D.velocity.x + dx * addSpeed;
                vx = (vx / dx) <= MAXMoveSpeed ? vx : MAXMOVESpeed / dx;
                rigb2D.velocity = new Vector2(vx, rigb2D.velocity.y);
                transform.localScale = new Vector3(dx, 1, 1);
            }
        }
        public void ResetJumpMaxCout(CharacterStatus status)
        {
            PlayerStatus _status = status as PlayerStatus;
            jumpMaxCout = _status.JumpMaxCout;
        }
        public void AddJumpMaxCout(int num)
        {
            jumpMaxCout += num;
        }
        public void SetJumpForce(float force)
        {
            jumpForce = force;
        }
    }

}