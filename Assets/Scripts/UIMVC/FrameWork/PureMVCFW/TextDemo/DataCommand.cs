using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;

//数据控制类
namespace PureMVCTextDemo
{
    public class DataCommand : SimpleCommand
    {
        //执行方法
        public override void Execute(INotification notification)
        {
            //调用数据层“增加等级方法”
            DataProxy datapro =Facade.RetrieveProxy(DataProxy.NAME)as DataProxy;     
            datapro.AddLevel(10);
        }
    }

}