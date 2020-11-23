using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Character.Function;
namespace ARPG.Character
{
    public class PlayerMotor :CharacterMotor,ICharacterJump,ICharacterWalk
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

        private float jumpForce = 0f;
        private float fallMultiplier = 2.5f;
        private float lowJumpMultiplier = 2f;
        private bool jumping = false;
        public bool Jumping { get => jumping; set => jumping = value; }

        private void FixedUpdate()
        {
            Fall();
            CheckGround();
        }
        public override void InitMotor(CharaterStatus status)
        {
            base.InitMotor(status);
            this.addSpeed = status.AddSpeed;
            this.MAXMoveSpeed = status.Speed;
            this.jumpForce = status.JumpForce;
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

        public void Jump()
        {
            if (IsGround)
            {
                rigb2D.velocity = new Vector2(rigb2D.velocity.x, jumpForce);
            }
        }

        public void HorizontalMove(float dx)
        {
            float vx = dx*addSpeed/dx<=MAXMoveSpeed?dx*addSpeed:MAXMOVESpeed;
           // rigb2D.velocity = new Vector2(rigb2D.velocity.x + vx, rigb2D.velocity.y);
            //转向
            if (vx != 0)
                transform.localScale = new Vector3(dx, 1, 1);
        }

        public void DirectMove(Vector2 direction)
        {
            throw new System.NotImplementedException();
        }

        public void SetJumpForce(float force)
        {
            jumpForce = force;
        }
    }

}