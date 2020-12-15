using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common.Helper.RoomSystem
{
    public class SearchRoadHelper
    {
        /// <summary>
        /// A星寻路算法
        /// </summary>
        /// <param name="room"></param>
        /// <param name="startPos"></param>
        /// <param name="aimPos"></param>
        /// <returns></returns>
        public static List<Vector2> AStar(RoomMap room, Vector2 startPos, Vector2 aimPos)
        {
            List<Vector2> roadList = new List<Vector2>();
            int x = (int)startPos.x, y = (int)startPos.y;
            int ax = (int)aimPos.x, ay = (int)aimPos.y;
            Debug.Log("StartPos=(" + x + ',' + y + "),AimPos=(" + aimPos.x + ',' + aimPos.y + ")");
            int cost = 0;
            while (!(x == ax && y == ay))
            {
                int MinCost = int.MaxValue, dx = x, dy = y;
                for (int i = x - 1; i <= x + 1; i++)
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        int perCost = int.MinValue;
                        if (room.Map[i, j] == GridType.Air && !(i == x && j == y))
                        {
                            if (Mathf.Abs(i - x) + Mathf.Abs(j - y) == 1)
                            {
                                perCost = 10 + cost + (Mathf.Abs(ax - i) + Mathf.Abs(ay - j))*10;
                            }
                            else if (Mathf.Abs(i - x) + Mathf.Abs(j - y) == 2)
                            {
                                perCost = 14 + cost + (Mathf.Abs(ax - i) + Mathf.Abs(ay - j))*10;
                            }
                            if (MinCost > perCost)
                            {
                                MinCost = perCost;//获得最小值
                                dx = i; dy = j;
                            }
                        }
                    }
                }//
                cost = MinCost;
                x = dx; y = dy;//更新位置
                //Debug.Log("add(" + dx + "," + dy + "）," + cost);
                //加入路径
                roadList.Add(new Vector2(x, y));
            }
            return roadList;
        }
    }

}