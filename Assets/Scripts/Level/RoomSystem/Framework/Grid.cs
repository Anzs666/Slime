using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common.Helper.RoomSystem
{
    //格子类型
    public enum GridType
    {
        Wall,
        Air,
    }
    public class Grid
    {
        //public GridType gridType;
        //public Vector2 worldPosition;
        public int x,y;
        /// <summary>
        /// 走到该点的花费
        /// </summary>
        public int gCost=0;
        /// <summary>
        /// 该点走到目标点的花费
        /// </summary>
        public int hCost=0;
        public bool walkAble;
        public Grid parent;
        public Grid(bool _walkAble,int _x,int _y)
        {
            walkAble = _walkAble;
            //worldPosition = _worldPos;
            x = _x;y = _y;
        }
        //f=g+h
        public int fCost
        {
            get => gCost + hCost;
        }
    }
}
