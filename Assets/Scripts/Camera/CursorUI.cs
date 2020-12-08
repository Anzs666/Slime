using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public class CursorUI : MonoBehaviour
    {
        //玩家和鼠标的位置
        private Transform playerTrans;
        private Vector3 mousePos;
        //光标向量
        private Vector2 pcDirection;
        //光标和玩家之间的距离
        public float maxDistance;
        private float pcDistance;
        public float PcDistance { get => pcDistance; }

        //玩家和光标之间的向量


        private void Start()
        {
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        }
        // Update is called once per frame
        void Update()
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - playerTrans.position;
            pcDistance = direction.magnitude;
            //光标限制
            pcDistance=pcDistance > maxDistance ? maxDistance : pcDistance;
            pcDirection =direction.normalized*pcDistance;
            //光标移动
            transform.position = Camera.main.WorldToScreenPoint(new Vector2(playerTrans.position.x,playerTrans.position.y)+ pcDirection);
        }
    }

}