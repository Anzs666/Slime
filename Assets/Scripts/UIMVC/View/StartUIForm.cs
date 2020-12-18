using System.Collections;
using System.Collections.Generic;
using Common.Helper;
using UnityEngine;
using UnityEngine.UI;

namespace SUIFW
{
    public class StartUIForm :BaseUIForm
    {
        // Start is called before the first frame update
        void Start()
        {
            RegisterButtonObjectEvent("BtnStartGame", OnBtnStartGameDown);
        }

        private void OnBtnStartGameDown(GameObject gameObject)
        {
            UIManager.GetInstance().CloseUIForms("StartUIForm");
            LoadScenesManager.Load(Scene.Town);
            
        }
    }
}
