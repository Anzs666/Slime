using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//保存原始数据

namespace PureMVCTextDemo
{
    public class MyData
    {
        private int _Level = 0;

        public int Level { get => _Level; set => _Level = value; }
    }
}
