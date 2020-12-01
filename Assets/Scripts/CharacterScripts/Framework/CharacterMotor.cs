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

        public virtual void InitMotor(CharacterStatus _status){
        }

        public void SetVelocity(Vector2 velocity)
        {
            rigb2D.velocity = velocity;
        }
        public Vector2 GetVelocity()
        {
            return rigb2D.velocity;
        }

        //判断落地
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

