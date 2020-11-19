using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUI.Framework
{

    public class CurrentUIType
    {
        public UIFormLucencyType UIForm_LucencyType;
        public UIFormShowMode UIForm_ShowMode;
        public UIFormType UIForm_Type;
    }
    //窗体位置
    public enum UIFormType
    {
        Normal,     //普通
        Fixed,      //固定
        Tip,        //弹出
    }
    //窗体切换设置
    public enum UIFormShowMode
    {
        Normal,         //普通
        ReverseChange,  //反向切换
        HideOther       //隐藏其它
    }
    //窗体透明度
    public enum UIFormLucencyType
    {
         Lucency,       //完全透明不能穿透
         TransLucence,  //半透明不能穿透
         ImPenetrable,  //地透明不能穿透
         Pentrate       //可以穿透
    }

    public enum UIWindowType
    {
        Task,
        Package,
        Store,
        Main,
        PCBattle
    }
}
