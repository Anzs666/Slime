using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using UnityEngine;

//视图层
namespace PureMVCTextDemo
{
    public class DataMediator : Mediator
    {
        public DataMediator(string mediatorName, object viewComponent = null) : base(mediatorName, viewComponent)
        {
            
        }
        //本视图层可以接受的消息
        public override string[] ListNotificationInterests()
        {
            List<string> ListResult = new List<string>();
            ListResult.Add("Msg_AddLevel");
            return ListResult.ToArray();
        }
        //处理接受的消息
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case "Msg_AddLevel":
                    MyData myData = notification.Body as MyData;
                    //数据赋值
                    break;
                default:
                    break;
            }
        }
    }
}