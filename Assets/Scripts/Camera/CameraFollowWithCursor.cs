using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public class CameraFollowWithCursor : MonoBehaviour
    {
        private Transform player;
        public GameObject cursorPos;
        private float camX, camY;
        private Vector2 screenWH;
        private float distance;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            screenWH = Camera.main.sensorSize;        
        }
        private void Update()
        {
            //在鼠标和人物之间    
            camX = (Camera.main.ScreenToWorldPoint(cursorPos.transform.position).x + player.position.x) / 2;
            camY = (Camera.main.ScreenToWorldPoint(cursorPos.transform.position).y + player.position.y) / 2;
            distance = cursorPos.GetComponent<CursorUI>().PcDistance;
            Camera.main.orthographicSize = distance<8?8:distance;//限制大小
            transform.position = new Vector3(camX, camY, -1);
        }
    }

}