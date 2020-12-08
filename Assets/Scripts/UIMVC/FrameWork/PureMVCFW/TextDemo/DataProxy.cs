using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using UnityEngine;

//数据代理类
namespace PureMVCTextDemo
{
    public class DataProxy : Proxy
    {
        //声明本类名称
        public new const string NAME="DataProxy";
        //引用实体类
        private MyData myData=null;//父类中使用object代替了
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="proxyName"></param>
        /// <param name="data"></param>
        public DataProxy():base(NAME)//注意
        {
            myData = new MyData();
        }   
        /// <summary>
        /// 增加玩家等级
        /// </summary>
        /// <param name="addNumber"></param>
        public void AddLevel(int addNumber)
        {
            //把数据结果加到数据层
            myData.Level += addNumber;
            //把数据结果发送到视图层
            SendNotification("Msg_AddLevel", myData);
           //...
        }

    }
}
