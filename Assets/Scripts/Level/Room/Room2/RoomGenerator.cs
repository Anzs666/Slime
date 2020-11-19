using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Common.Helper.RandomRoomSystem
{
    //父类房间生成器，连接房间类型资源库
    //用于区分地形和生成方法

    public abstract class RoomGenerator : MonoBehaviour
    {
        //--------------------
        //房间数据库
        //房间类型列表
        public GameObject[] RoomTypes;
        //房间地貌列表
        public GameObject[] RoomTypeBlocks;
        //房间物品列表
        public GameObject[] Items;

        //------------------------
        //房间数组
        public List<GameObject> RoomArray;
        //房间最大数量
        public int Max_Room = 10;
        //步长
        private const int STEP = 10;
        //开始结束房间
        public GameObject startRoom;
        public GameObject endRoom;
        //生成点
        private Vector3 generatorPoint = new Vector3(0, 0, 0);
        //初始化房间生成器
        private void CreateRoomOnPos(GameObject roomType, Vector3 pos)
        {
            if (RoomArray == null) RoomArray = new List<GameObject>();
            GameObject roomToAdd = GameObject.Instantiate(roomType, pos, Quaternion.identity);
            roomToAdd.GetComponent<Room>().InitBlocks(RoomTypeBlocks);
            RoomArray.Add(roomToAdd);
        }
        //寻找合适的房间
        private GameObject FindRandomSuitableRoomTypes(string dir)
        {
            List<GameObject> sRoom = new List<GameObject>();
            for (int i=0;i<RoomTypes.Length;i++)
            {
                string rt = RoomTypes[i].name.Split('_')[1];//截取后半部分
                bool isSuit = true;
                for (int j = 0; j < dir.Length; j++)
                {
                    if (!rt.Contains(dir[j].ToString()))
                        isSuit = false;
                }
                if (isSuit)
                {
                    sRoom.Add(RoomTypes[i]);
                }
            }
            return sRoom.ToArray()[Random.Range(0, sRoom.Count)];
        }

        //生成关卡
        public void InitRooms()
        {
            CreateRoomOnPos(FindRandomSuitableRoomTypes("RBLT"), generatorPoint);
            int roomCout = 1;
            int direction = Random.Range(1, 6);
            while (roomCout < Max_Room)
            {               
                //左
                if (direction >= 1 && direction <= 2)
                {
                    generatorPoint.x -= STEP;                   
                    CreateRoomOnPos(FindRandomSuitableRoomTypes("B"), generatorPoint);
                    direction = Random.Range(1, 6);
                    if (direction >= 3 && direction <= 4)
                        direction = 5;
                }
                //右
                else if (direction >= 3&&direction<=4)
                {
                    generatorPoint.x +=STEP;
                    CreateRoomOnPos(FindRandomSuitableRoomTypes("B"), generatorPoint);
                    direction = Random.Range(1, 6);
                    if(direction >= 1 && direction <= 2)
                        direction = 5;
                }
                //下
                else if (direction == 5)
                {
                    generatorPoint.y -= STEP;
                    CreateRoomOnPos(FindRandomSuitableRoomTypes("B"), generatorPoint);
                    direction = Random.Range(1, 6);
                }
                roomCout++;
            }
        }
        //释放关卡
        public void FreeLevel() { }
    }

}