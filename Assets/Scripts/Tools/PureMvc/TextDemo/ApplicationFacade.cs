using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns.Facade;
using UnityEngine;
namespace PureMVCTextDemo
{
    public class ApplicationFacade : Facade
    {
        public ApplicationFacade(GameObject goRootNode)
        {
            ////mvc三层绑定
            //RegisterCommand("Reg_StartDataCommand",typeof(DataCommand));
            //RegisterMediator(new DataMediator(goRootNode));
            //RegisterProxy(new DataProxy());
        }
    }
}
