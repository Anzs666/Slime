using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace gojiu.slim
{
	///<summary>
	///生成基础随机房间位置
	///<summary>
	public class RoomGenerator : MonoBehaviour
	{
        private enum Direction { up,down,left,right}
        private Direction direction;

        [Header("房间信息")]
        public GameObject roomprefab;
        public LayerMask roomLayer;
        public int roomNum;
        public Color startColor, endColor;
        [Header("位置控制")]
        private Transform generatorPoint;
        public float xOffset;
        public float yOffset;

        private List<Room> rooms = new List<Room>();
        public WallType wallType;

        private void Awake()
        {
            generatorPoint = GameObject.Find("Point").GetComponent<Transform>();
        }

        private void Start()
        {
            InitRoom();
            foreach (var room in rooms)
            {
                SetUpDoor(room, room.transform.position);
            }
            FindMaxEndRoom();
            foreach (var room in rooms)
            {
                SetUpDoor(room, room.transform.position);
            }
        }

        private void Update()
        {
            //测试
            //if (Input.anyKey)
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //}
        }

        public void InitRoom()
        {
            for (int i = 0; i < roomNum; i++)
            {
                rooms.Add(Instantiate(roomprefab, generatorPoint.position, Quaternion.identity).GetComponent<Room>());
                ChangeRoomposition();
            }
            rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        }

        private void ChangeRoomposition()
        {
            do
            {
                direction = (Direction)Random.Range(0, 4);//左闭右开
                switch (direction)
                {
                    case Direction.up:
                        generatorPoint.position += new Vector3(0, yOffset, 0);
                        break;
                    case Direction.down:
                        generatorPoint.position += new Vector3(0, -yOffset, 0);
                        break;
                    case Direction.left:
                        generatorPoint.position += new Vector3(-xOffset, 0, 0);
                        break;
                    case Direction.right:
                        generatorPoint.position += new Vector3(xOffset, 0, 0);
                        break;
                    default:
                        break;
                }
            } while (Physics2D.OverlapCircle(generatorPoint.position,0.2f,roomLayer));//这里容易陷入死循环
            
        }
        
        //最远的房间变为绿色
        private void FindMaxEndRoom()
        {
            List<Room> endRooms = new List<Room>();
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].doorNum == 1 || rooms[i].doorNum == 2)
                {
                    endRooms.Add(rooms[i]);
                }
            }
                
            Room endRoom=rooms[1];
            foreach (var room in endRooms)
            {
                if (room.transform.position.sqrMagnitude > endRoom.transform.position.sqrMagnitude)//vector3平方和
                {
                    endRoom = room;
                }
            }
            endRoom.GetComponent<SpriteRenderer>().color = endColor;
            AddBossRoom(ref endRoom);
        }

        public void AddBossRoom(ref Room endRoom)
        {
            //Room bossRoom;
            //if (!endRoom.doorIsDown)
            //{
            //    bossRoom = Instantiate(roomprefab, endRoom.transform.position + new Vector3(0, -yOffset, 0), Quaternion.identity).GetComponent<Room>();
            //    bossRoom.doorIsUp = true; bossRoom.GetComponent<SpriteRenderer>().color = Color.blue;return;
            //}
            //if (!endRoom.doorIsUp)
            //{
            //    bossRoom = Instantiate(roomprefab, endRoom.transform.position + new Vector3(0, yOffset, 0), Quaternion.identity).GetComponent<Room>();
            //    bossRoom.doorIsDown = true; bossRoom.GetComponent<SpriteRenderer>().color = Color.blue;return;
            //}
            //if (!endRoom.doorIsLeft)
            //{
            //    bossRoom = Instantiate(roomprefab, endRoom.transform.position + new Vector3(-xOffset, 0, 0), Quaternion.identity).GetComponent<Room>();
            //    bossRoom.doorIsRight = true; bossRoom.GetComponent<SpriteRenderer>().color = Color.blue;return;
            //}
            //if (!endRoom.doorIsRight)
            //{
            //    bossRoom = Instantiate(roomprefab, endRoom.transform.position + new Vector3(xOffset, 0, 0), Quaternion.identity).GetComponent<Room>();
            //    bossRoom.doorIsLeft = true; bossRoom.GetComponent<SpriteRenderer>().color = Color.blue;return;
            //}
            Room bossRoom;
            if (!endRoom.doorIsDown)
            {
                bossRoom = Instantiate(roomprefab, endRoom.transform.position + new Vector3(0, -yOffset, 0), Quaternion.identity).GetComponent<Room>();
                rooms.Add(bossRoom);
                //bossRoom.doorIsUp = true;
                bossRoom.GetComponent<SpriteRenderer>().color = Color.blue;
                //endRoom.doorIsDown = true;
                //SetUpDoor(endRoom, endRoom.transform.position);
                //SetUpDoor(bossRoom, bossRoom.transform.position);
                return;
            }
            if (!endRoom.doorIsUp)
            {
                bossRoom = Instantiate(roomprefab, endRoom.transform.position + new Vector3(0, yOffset, 0), Quaternion.identity).GetComponent<Room>();
                rooms.Add(bossRoom);
                //bossRoom.doorIsDown = true;
                bossRoom.GetComponent<SpriteRenderer>().color = Color.blue;
                //endRoom.doorIsUp = true;
                //SetUpDoor(endRoom, endRoom.transform.position);
                //SetUpDoor(bossRoom, bossRoom.transform.position);
                return;
            }
            if (!endRoom.doorIsLeft)
            {
                bossRoom = Instantiate(roomprefab, endRoom.transform.position + new Vector3(-xOffset, 0, 0), Quaternion.identity).GetComponent<Room>();
                rooms.Add(bossRoom);
                //bossRoom.doorIsRight = true;
                bossRoom.GetComponent<SpriteRenderer>().color = Color.blue;
                //endRoom.doorIsLeft = true;
                //SetUpDoor(endRoom, endRoom.transform.position);
                //SetUpDoor(bossRoom, bossRoom.transform.position);
                
                return;
            }
            if (!endRoom.doorIsRight)
            {
                bossRoom = Instantiate(roomprefab, endRoom.transform.position + new Vector3(xOffset, 0, 0), Quaternion.identity).GetComponent<Room>();
                rooms.Add(bossRoom);
                //bossRoom.doorIsLeft = true;
                bossRoom.GetComponent<SpriteRenderer>().color = Color.blue;
                //endRoom.doorIsRight = true;
                //SetUpDoor(endRoom, endRoom.transform.position);
                //SetUpDoor(bossRoom, bossRoom.transform.position);
                return;
            }
        }

        /// <summary>
        /// 因为墙是由roommanager管理，导致墙拥有之后无法更新
        /// 方法：找到最远的房间直接添加，不先更新房间出口个数
        /// </summary>
        /// <param name="room"></param>
        /// <param name="roomPosition"></param>
        public void SetUpDoor(Room room, Vector3 roomPosition)
        {
            room.doorIsUp = Physics2D.OverlapCircle(roomPosition+ new Vector3(0, yOffset, 0), 0.2f, roomLayer);
            room.doorIsDown = Physics2D.OverlapCircle(roomPosition+ new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
            room.doorIsLeft = Physics2D.OverlapCircle(roomPosition+ new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
            room.doorIsRight = Physics2D.OverlapCircle(roomPosition+ new Vector3(xOffset, 0, 0), 0.2f, roomLayer);
            room.UpdateRoom();//非常需要调用
            
            //匹配墙
            switch (room.doorNum)
            {
                case 1:
                    if (room.doorIsUp)
                        Instantiate(wallType.singleU, room.transform.position, Quaternion.identity);
                    if (room.doorIsDown)
                        Instantiate(wallType.singleD, room.transform.position, Quaternion.identity);
                    if (room.doorIsLeft)
                        Instantiate(wallType.singleL, room.transform.position, Quaternion.identity);
                    if (room.doorIsRight)
                        Instantiate(wallType.singleR, room.transform.position, Quaternion.identity);
                    break;
                case 2:
                    if (room.doorIsUp&&room.doorIsDown)
                        Instantiate(wallType.doubleUD, room.transform.position, Quaternion.identity);
                    if (room.doorIsUp&&room.doorIsLeft)
                        Instantiate(wallType.doubleUL, room.transform.position, Quaternion.identity);
                    if (room.doorIsUp&&room.doorIsRight)
                        Instantiate(wallType.doubleUR, room.transform.position, Quaternion.identity);
                    if (room.doorIsDown&&room.doorIsRight)
                        Instantiate(wallType.doubleDR, room.transform.position, Quaternion.identity);
                    if (room.doorIsLeft&&room.doorIsRight)
                        Instantiate(wallType.doubleLR, room.transform.position, Quaternion.identity);
                    if (room.doorIsDown&&room.doorIsLeft)
                        Instantiate(wallType.doubleDL, room.transform.position, Quaternion.identity);
                    break;
                case 3:
                    if (room.doorIsDown && room.doorIsLeft&&room.doorIsRight)
                        Instantiate(wallType.tripleDRL, room.transform.position, Quaternion.identity);
                    if (room.doorIsUp && room.doorIsLeft&&room.doorIsRight)
                        Instantiate(wallType.tripleURL, room.transform.position, Quaternion.identity);
                    if (room.doorIsUp && room.doorIsDown&&room.doorIsRight)
                        Instantiate(wallType.tripleUDR, room.transform.position, Quaternion.identity);
                    if (room.doorIsUp && room.doorIsLeft&&room.doorIsDown)
                        Instantiate(wallType.tripleUDL, room.transform.position, Quaternion.identity);
                    break;
                case 4:
                    if (room.doorIsUp && room.doorIsLeft && room.doorIsDown&&room.doorIsRight)
                        Instantiate(wallType.fourDoors, room.transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
    }

    //与[SerializeField]区分，用于类中
    [System.Serializable]
    public class WallType
    {
        public GameObject singleU, singleD, singleL, singleR,
                                  doubleUD, doubleUL, doubleUR, doubleLR, doubleDL, doubleDR,
                                  tripleURL, tripleUDR, tripleUDL, tripleDRL,
                                  fourDoors;
    }
}
