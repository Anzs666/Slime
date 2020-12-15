using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;//

namespace Common.Helper.RoomSystem
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
            if (aimRoomMap == null) { }
            if (platRuleTile == null) { }
            aimRoomMap = tile.GetComponent<Tilemap>();
            rMap = new RoomMap(ROOM_MAXWIDTH, ROOM_MAXHEIGHT);
            //CreateCellMap();
            SetBroad();
            RefrashMap();
        }
        /// <summary>
        /// 获得地图
        /// </summary>
        /// <returns></returns>
        public RoomMap GetRoom()
        {
            return rMap;
        }
        /// <summary>
        /// 创建细胞地图
        /// </summary>
        public void CreateCellMap()
        {
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
        /// 设置边界
        /// </summary>
        public void SetBroad()
        {
            for (int i = 0; i < rMap.Height; i++)
                for (int j = 0; j < rMap.Width; j++)
                    if(i==0||i == rMap.Height-1||j==0||j== rMap.Width-1||(i>=4&&i<=8&& j >= 4 && j <= 8)|| (i >= 10 && j >= 10 && j <= 20))
                        rMap.Map[i, j] = GridType.Wall;
                    else
                        rMap.Map[i, j] = GridType.Air;
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
