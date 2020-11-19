using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Character
{
    /// <summary>
    ///角色马达
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        //水平移动
        private float moveSpeed;
        private float MAXMoveSpeed;
        private float rotateSpeed = 0f;
        //跳跃
        public Transform[] groundChecks;
        private float jumpForce = 0f;
        private float fallMultiplier = 2.5f;
        private float lowJumpMultiplier = 2f;
        private bool isGround = true;
        private bool jumping = false;

        private Rigidbody2D rigb2D;
        private RaycastHit2D checkResult;
        private CharacterInputController_2D controller;

        //最大值控制
        public float MoveSpeed
        {
            set { moveSpeed = value < MAXMoveSpeed ? value : MAXMoveSpeed; }
        }
        public bool Jumping { get => jumping; set => jumping = value; }
        public float MAXMOVESpeed { get => MAXMoveSpeed; }
        public bool IsGround { get => isGround; set => isGround = value; }

        public void Awake()
        {
            rigb2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Fall();
            CheckGround();
        }
        public void InitMotor(CharaterStatus status)
        {
            MAXMoveSpeed = status.Speed;
            SetJumpForce(status.JumpForce);
        }
        //设置初始跳跃强度
        public void SetJumpForce(float force)
        {
            jumpForce = force;
        }
        public void SetVelocity(Vector2 velocity)
        {
            rigb2D.velocity = velocity;
        }
        public Vector2 GetVelocity()
        {
            return rigb2D.velocity;
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
        //旋转
        public void LookAtTarget(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            Quaternion LookDir = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, LookDir, rotateSpeed * Time.deltaTime);
        }
        //判断落地
        private void CheckGround()
        {
            for (int i = 0; i < groundChecks.Length; i++)
            {
                checkResult = Physics2D.Linecast(transform.position, groundChecks[i].position, 1 << LayerMask.NameToLayer("Ground"));
                isGround = checkResult;
                if (isGround) break;
            }
        }
        //跳跃
        public void Jump()
        {
            if (isGround)
            {
                rigb2D.velocity = new Vector2(rigb2D.velocity.x, jumpForce);
            }
        }
        //移动
        public void HorizontalMovement(float horizontalMove)
        {
            float vx = horizontalMove * moveSpeed + rigb2D.velocity.x;
            if (Mathf.Abs(vx) <= MAXMoveSpeed)
            {
                rigb2D.velocity = new Vector2(vx, rigb2D.velocity.y);
            }
            //转向
            if (horizontalMove != 0)
                transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }
}

