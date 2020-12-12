using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using SUIFW;
using ABFW;

namespace SlimeGame
{
    public class GameController : MonoSingleton<GameController>
    {
        string uiFormName = "StartUIForm.prefab";
        GameObject UIForm;
        //游戏开始前
        public void Start()
        {
            UIManager.GetInstance().ShowUIForms("StartUIForm");
        }
    }
}
