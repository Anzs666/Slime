using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gojiu.slim
{
	///<summary>
	///近身攻击
	///<summary>
	public class NifeSlash : MonoBehaviour
	{
        private void Update()
        {
            Attack();
        }

        private void Attack()
        {
            Vector2 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;//tan角度弧度转换为角度
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }

       
    }
}
