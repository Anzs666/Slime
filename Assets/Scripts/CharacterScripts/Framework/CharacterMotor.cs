using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Character
{
    /// <summary>
    ///角色马达
    /// </summary>
    public abstract class CharacterMotor : MonoBehaviour
    {
        public Transform[] groundChecks;
        private bool isGround = true;

        protected Rigidbody2D rigb2D;
        private RaycastHit2D checkResult;

        public bool IsGround { get => isGround; set => isGround = value; }
        //落地完成委托
        // public delegate void DelGroundCheck();
        //最大值控制

        public void Awake()
        {
            rigb2D = GetComponent<Rigidbody2D>();
        }
        /// <summary>
        /// 初始化驱动
        /// </summary>
        /// <param name="_status"></param>
        public abstract void InitMotor(CharacterStatus _status);
        /// <summary>
        /// 设置速度
        /// </summary>
        /// <param name="velocity"></param>
        public void SetVelocity(Vector2 velocity)
        {
            rigb2D.velocity = velocity;
        }
        /// <summary>
        /// 获得速度
        /// </summary>
        /// <returns></returns>
        public Vector2 GetVelocity()
        {
            return rigb2D.velocity;
        }
        /// <summary>
        /// 判断落地
        /// </summary>
        protected void CheckGround()
        {
            for (int i = 0; i < groundChecks.Length; i++)
            {
                checkResult = Physics2D.Linecast(transform.position, groundChecks[i].position, 1 << LayerMask.NameToLayer("Ground"));
                isGround = checkResult;
                if (isGround) break;
            }
        }
    }
}

