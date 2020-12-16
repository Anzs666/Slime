using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common.Helper.RoomSystem
{
    /// <summary>
    /// 用于储存每一个格子的数据二维数据结构
    /// 使用单例模式
    /// </summary>
    public class RoomMap
    {
        //保存数据地图
        private Grid[,] map;
        private int width = 0;
        private int height = 0;
        //获得地图
        public Grid[,] Map { get => map; }
        public int Width { get => width; }
        public int Height { get => height; }

        public RoomMap(int _width, int _height)
        {
            height = _height;
            width = _width;
            map = new Grid[Height, Width];
        }
    }

}