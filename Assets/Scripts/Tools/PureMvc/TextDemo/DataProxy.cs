using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Proxy;
using UnityEngine;

//数据代理类
namespace PureMVCTextDemo
{
    public class DataProxy : Proxy
    {
        //public new const string Name="";
        //private MyData=null;--父类中使用object代替了
        public DataProxy(string proxyName, object data = null) : base(proxyName, data)
        {
            
        }

        public void AddLevel(int addNumber)
        {
           //把数据结果加到数据层
           //把数据结果显示在视图层
        }

    }
}
