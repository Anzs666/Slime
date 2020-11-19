using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gojiu.slim
{
	///<summary>
	///控制小地图的显现
	///<summary>
	public class WallMap : MonoBehaviour
	{
        private GameObject mapSprite;

        private void OnEnable()
        {
            mapSprite = transform.parent.GetChild(0).gameObject;
            mapSprite.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                //摄像机移动
                //CameraController.instance.SwitchRoom(transform);
                mapSprite.SetActive(true);
            }
        }
    }
}
