using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;//

namespace Common.Helper.RandomRoomSystem
{
    /// <summary>
    /// 该类使用单例模式
    ///基于TileRule的随机生成tile
    ///需要选取不同的地表
    /// </summary>
    public class RoomCellSMGenerator : MonoSingleton<RoomCellSMGenerator>
    {
        //目标地图实体
        public GameObject tile;
        //目标物体
        private Tilemap aimRoomMap;
        public RuleTile platRuleTile;
        //房间宽度
        private const int ROOM_MAXWIDTH = 50;
        private const int ROOM_MAXHEIGHT = 50;
        private const int WALLRATE = 45;
        private RoomMap rMap;
        //初始化
        public override void Init()
        {
            base.Init();
            aimRoomMap = tile.GetComponent<Tilemap>();
            rMap = new RoomMap(ROOM_MAXWIDTH, ROOM_MAXHEIGHT);
            FillAllWall();
            rMap = CellSM.RandomRoom(rMap, WALLRATE);           
            for (int j = 0; j < 2; j++)
            {     
                for (int i = 0; i < 2; i++)
                    rMap = CellSM.CellAM(rMap);
                for (int x = 0; x < 4; x++)
                    rMap = CellSM.CellAM2(rMap);
            }
            rMap = CellSM.CellAM(rMap);
            RefrashMap();
        }
        /// <summary>
        /// 改变地图
        /// </summary>
        public void RefrashMap()
        {
            Vector3Int position = new Vector3Int();
            for (int i = 0; i < rMap.Height; i++)
                for (int j = 0; j < rMap.Width; j++)
                {
                    if (rMap.Map[i, j] == GridType.Wall)
                    {
                        position.y = i;
                        position.x = j;
                        position.z = 0;
                        aimRoomMap.SetTile(position, platRuleTile);
                    }
                }
        }
        /// <summary>
        /// 填充所有墙
        /// </summary>
        public void FillAllWall()
        {
            for (int i = 0; i < rMap.Height; i++)
                for (int j = 0; j < rMap.Width; j++)
                    rMap.Map[i, j] = GridType.Wall;
        }
        /// <summary>
        /// 填充所有空气
        /// </summary>
        public void FillAllAir()
        {
            for (int i = 0; i < rMap.Height; i++)
                for (int j = 0; j < rMap.Width; j++)
                    rMap.Map[i, j] = GridType.Air;
        }
        /// <summary>
        /// 摧毁地图
        /// </summary>
        public void DestoryMap()
        {

        }


    }
}
