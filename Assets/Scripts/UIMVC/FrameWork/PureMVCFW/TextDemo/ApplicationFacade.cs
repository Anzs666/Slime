using System;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
namespace PureMVCTextDemo
{
    public class ApplicationFacade : Facade
    {
        public ApplicationFacade(GameObject goRootNode)
        {
            ////mvc三层绑定 
            RegisterProxy(new DataProxy());
            RegisterMediator(new DataMediator(goRootNode));
            RegisterCommand("Reg_StartDataCommand",typeof(DataCommand));
            
           
        }

    }
}
