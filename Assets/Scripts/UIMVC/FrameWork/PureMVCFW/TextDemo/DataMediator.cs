using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.UI;

//视图层
namespace PureMVCTextDemo
{
    public class DataMediator : Mediator
    {
        public new const string NAME = "DataMediator";
        private Text textLvl;
        private Button BtnDisplayLvlNum;
        public DataMediator(GameObject goRootNode)
        {
            //确定控件
            textLvl = goRootNode.transform.Find("Text").gameObject.GetComponent<Text>();
            BtnDisplayLvlNum = goRootNode.transform.Find("Button").gameObject.GetComponent<Button>();
            //注册事件
            BtnDisplayLvlNum.onClick.AddListener(OnClickAddLevelNumber);
        }
        //用户点击事件
        private void OnClickAddLevelNumber()
        {
            //发送消息
            SendNotification("Reg_StartDataCommand");
        }
        //本视图层可以接受的消息
        public override IList<string> ListNotificationInterests()
        {
            IList<string> ListResult = new List<string>();
            ListResult.Add("Msg_AddLevel");
            return ListResult;
        }
        //处理接受的消息
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case "Msg_AddLevel":
                    MyData myData = notification.Body as MyData;
                    textLvl.text = myData.Level.ToString();
                    //数据赋值
                    break;
                default:
                    break;
            }
        }
    }
}