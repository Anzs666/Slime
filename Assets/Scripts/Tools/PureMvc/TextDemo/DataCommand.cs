using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
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
            DataProxy datapro = (DataProxy)Facade.RetrieveProxy(DataProxy.NAME);
            datapro.AddLevel(10);
        }
    }

}