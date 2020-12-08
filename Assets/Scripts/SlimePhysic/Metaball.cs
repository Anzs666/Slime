using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SlimePhysics
{
    public enum MetaballState
    {
        Water,
        Default,
        None
    }
    public class Metaball : MonoBehaviour
    {
        private MetaballState currentState = MetaballState.Water;
        public Color waterColor;
        public SpriteRenderer particleImage;//仅仅被液体相机所看到，显示Metaball的图片Shader

        /// <summary>
        /// 改变粒子状态
        /// </summary>
        public void SetState(MetaballState newState)
        {
            if (newState != currentState)
            {
                currentState = newState;
                switch (currentState)
                {
                    case MetaballState.Water:
                        particleImage.color = waterColor;//设置颜色让shader分辨液体的颜色
                        GetComponent<Rigidbody2D>().gravityScale = 1.0f; // To simulate Water density
                        break;
                    case MetaballState.Default:
                        break;
                    case MetaballState.None:
                        Destroy(gameObject);
                        break;
                    default:
                        break;
                }
            }
        }
        //void Update()
        //{
        //    switch (currentState)
        //    {
        //        case MetaballState.Water: 
        //            MovementAnimation();
        //            break;
        //    }
        //}
        ///// <summary>
        ///// 运动动画
        ///// </summary>
        //void MovementAnimation()
        //{
        //    Vector3 movementScale = new Vector3(1.0f, 1.0f, 1.0f);		
        //    movementScale.x += Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) / 30.0f;
        //    movementScale.z += Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) / 30.0f;
        //    movementScale.y = 1.0f;
        //    particleImage.gameObject.transform.localScale = movementScale;
        //}
    }
}
