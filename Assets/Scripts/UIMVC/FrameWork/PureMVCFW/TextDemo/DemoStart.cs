using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PureMVCTextDemo {
    public class DemoStart : MonoBehaviour
    {
        //获取子节点
        void Start()
        {
            //启动框架，传入UI根节点 
            new ApplicationFacade(this.gameObject);
        }
    }
}