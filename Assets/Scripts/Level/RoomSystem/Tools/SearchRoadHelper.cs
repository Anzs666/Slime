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
        public static List<Grid> AStar(Vector2 startPos, Vector2 aimPos){
            ////相等返回
            //if (RoomManager.Instance.GridFromWorldPoint(startPos) == RoomManager.Instance.GridFromWorldPoint(aimPos))
            //    return null;
            Grid startGrid = RoomManager.Instance.GridFromWorldPoint(startPos);
            Grid aimGrid = RoomManager.Instance.GridFromWorldPoint(aimPos);      
            //Debug.Log("StartPos=(" + startGrid.x + ',' + startGrid.y+ "),AimPos=(" + aimGrid.x + ',' + aimGrid.y + ")");
            List<Grid> openSet=new List<Grid>();//可以走的格子
            HashSet<Grid> closedSet=new HashSet<Grid>();//不可以走的格子
            openSet.Add(startGrid);
            while (openSet.Count > 0){
                Grid currentGrid = openSet[0];//目标格子
                for (int i = 1; i < openSet.Count; i++){
                    if (openSet[i].fCost < currentGrid.fCost || openSet[i].fCost == currentGrid.fCost && openSet[i].hCost < currentGrid.hCost) {
                        currentGrid = openSet[i];
                    }
                }
                //Debug.Log("Current:(" + currentGrid.x + "," + currentGrid.y + ",gCost:" + currentGrid.gCost + ",hCost:" + currentGrid.hCost+")");
                openSet.Remove(currentGrid);//将目标格子设置为已经走过的格子
                closedSet.Add(currentGrid);
                if (currentGrid == aimGrid){
                    //到达终点
                    return RetracePath(startGrid, aimGrid);                     
                }
                //查找相邻的格子
                foreach (Grid nGrid in RoomManager.Instance.GetGridNeighbours(currentGrid)){                  
                    if (!nGrid.walkAble || closedSet.Contains(nGrid)){
                        continue;
                    }
                    int newMovementCostToNeighbour = currentGrid.gCost + GetDistance(currentGrid, nGrid);
                    if (newMovementCostToNeighbour < nGrid.gCost || !openSet.Contains(nGrid)) {
                        nGrid.gCost = newMovementCostToNeighbour;
                        nGrid.hCost = GetDistance(nGrid, aimGrid);
                        nGrid.parent = currentGrid;//设置根节点 
                        //Debug.Log("neighbour:(" + nGrid.x + "," + nGrid.y+",gCost:"+nGrid.gCost+",hCost:"+nGrid.hCost + ")");
                        if (!openSet.Contains(nGrid)){
                            openSet.Add(nGrid);
                        }
                    }
                }
            }//
            return null;
        }
        /// <summary>
        /// 寻找父节点
        /// </summary>
        /// <param name="startGrid"></param>
        /// <param name="aimGrid"></param>
        private static List<Grid> RetracePath(Grid startGrid, Grid aimGrid){
            List<Grid> path = new List<Grid>();
            Grid currentGrid = aimGrid;
            while (currentGrid != startGrid){
                path.Add(currentGrid);
                currentGrid = currentGrid.parent;
                //Debug.Log(currentGrid.x+","+ currentGrid.y);
            }
            path.Reverse();//列表追寻根节点
            return path;
        }
        /// <summary>
        /// 计算ab间的距离
        /// </summary>
        /// <param name="gridA"></param>
        /// <param name="gridB"></param>
        /// <returns></returns>
        private static int GetDistance(Grid gridA,Grid gridB){
            int distX = Mathf.Abs(gridA.x - gridB.x);
            int distY = Mathf.Abs(gridA.y - gridB.y);
            if (distX > distY) 
                return 14 * distY + (distX-distY)* 10;
            return 14 * distX + (distY - distX) * 10;
        }
    }

}