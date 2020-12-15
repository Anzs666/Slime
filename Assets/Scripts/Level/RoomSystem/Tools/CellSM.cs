using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common.Helper.RoomSystem
{
    public static class CellSM
    {
        /// <summary>
        /// 检查某单元格周围n圈内墙数
        /// </summary>
        /// <param name="map"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        private static int CheckNeighborWall(RoomMap map, int x, int y, int r)
        {
            int wallNum = 0;
            for (int i = x - r; i <= x + r; i++)
                for (int j = y - r; j <= y + r; j++)
                    if (i >= 0 && i < map.Height && j >= 0 && j < map.Width &&
                        !(i == x && j == y) && map.Map[i, j] == GridType.Wall)//防止超界
                        wallNum++;
            return wallNum;
        }
        /// <summary>
        /// 细胞自动机算法
        /// </summary>
        /// <param name="map"></param>
        public static RoomMap CellAM(RoomMap map)
        {
            for (int i = 1; i < map.Height - 1; i++)
                for (int j = 1; j < map.Width - 1; j++)
                    if (map.Map[i, j] == GridType.Wall)//如果该单元格是墙
                    {
                        if (CheckNeighborWall(map, i, j, 1) < 4)//且周围一圈的墙数量少于4
                            map.Map[i, j] = GridType.Air;//变成空气
                    }
                    else if (map.Map[i, j] == GridType.Air)//如果该单元格是空气
                    {
                        if (CheckNeighborWall(map, i, j, 1) >= 5)//且周围一圈的墙数量大于5
                            map.Map[i, j] = GridType.Wall;//变成墙
                    }
            return map;
        }
        /// <summary>
        /// 细胞自动机算法2
        /// 只要周围一圈墙数大于5或者周围两圈墙数小于五就变成墙
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        public static RoomMap CellAM2(RoomMap map)
        {
            for (int j = 1; j < map.Width - 1; j++)
                for (int i = 1; i < map.Height - 1; i++)
                    if ((map.Map[i, j] == GridType.Wall || map.Map[i, j] == GridType.Air) &&
                        (CheckNeighborWall(map, i, j, 1) >= 5||CheckNeighborWall(map, i, j, 2) < 5))
                            map.Map[i, j] = GridType.Wall;
            return map;
        }
        /// <summary>
        /// 将房间打乱
        /// </summary>
        /// <param name="map"></param>
        /// <param name="wallRate">生成墙的概率</param>
        public static RoomMap RandomRoom(RoomMap map, float wallRate)
        {
            int currentNum;
            for (int i = 1; i < map.Height - 1; i++)
                for (int j = 1; j < map.Width - 1; j++)
                {
                    currentNum = Random.Range(0, 100);
                    if (currentNum <= wallRate)
                        map.Map[i, j] = GridType.Wall;
                    else
                        map.Map[i, j] = GridType.Air;
                }
            return map;
        }
    }
}
