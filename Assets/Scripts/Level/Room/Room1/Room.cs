using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gojiu.slim
{
	///<summary>
	///挂在房间预制体上
	///<summary>
	public class Room : MonoBehaviour
	{
        public GameObject doorUp, doorDown, doorLeft, doorRight;
        public bool doorIsUp, doorIsDown, doorIsLeft, doorIsRight;

        public int doorNum;//开门数量

        public List<Transform> enemyPoints;
        public bool haveEnemy;
        public int enemycount;
        //public bool doorClose;

        //不可以放在awake中
        private void Start()
        {
            //UpdateDoor(false);
            //doorUp.SetActive(doorIsUp);
            //doorDown.SetActive(doorIsDown);
            //doorLeft.SetActive(doorIsLeft);
            //doorRight.SetActive(doorIsRight);
            doorUp.SetActive(false);
            doorDown.SetActive(false);
            doorLeft.SetActive(false);
            doorRight.SetActive(false);
         
        }


        public void UpdateRoom()
        {
            if (doorIsUp) doorNum++;
            if (doorIsLeft) doorNum++;
            if (doorIsDown) doorNum++;
            if (doorIsRight) doorNum++;
            
        }

        public void UpdateDoor(bool doorClose)
        {
            doorUp.SetActive(doorIsUp&&doorClose);
            doorDown.SetActive(doorIsDown&&doorClose);
            doorLeft.SetActive(doorIsLeft&&doorClose);
            doorRight.SetActive( doorIsRight&&doorClose);

        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.transform.tag=="Player")
            {
                UpdateDoor(true);
                
                EnemyProduce();
                OpenDoor();
            }
        }

        private void EnemyProduce()
        {
            if (!haveEnemy)
            {
                haveEnemy = true;
                for (int i = 0; i < enemyPoints.Count; i++)
                {
                    //EnemyListManage.instance.ProduceEnemy(0, enemyPoints[i]);
                }
            }
           
        }

        private void OpenDoor()
        {
            //1.自己房间的怪物清空
            enemycount = enemyPoints.Count;
            for (int i = 0; i < enemyPoints.Count; i++)
            {
                if (enemyPoints[i].childCount == 0)
                {
                    enemycount--;
                }
            }
            if (enemycount == 0)
            {
                doorUp.SetActive(false);
                doorDown.SetActive(false);
                doorLeft.SetActive(false);
                doorRight.SetActive(false);
            }
          
        }
        //放进了wallmap
        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.tag=="Player")
        //    {
        //        CameraController.instance.SwitchRoom(transform);
        //    }
        //}
    }
}
