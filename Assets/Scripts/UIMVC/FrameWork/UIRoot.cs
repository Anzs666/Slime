using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace UGUI.Framework
{
    /// <summary>
    /// 将窗口分类
    /// </summary>
	public static class UIRoot
	{
        private static Transform transform;             //ui根节点
        private static Transform fixStation;            //固定显示的节点
        private static Transform workStation;           //全屏显示的节点
        private static Transform noticeStation;         //弹出的节点 
        private static Transform fixedStation;          //固定小窗体节点
        private static Transform recyclePool;           //回收池
        private static Transform scriptManager;         //代码节点 
        
        private static bool isInit=false;

        
        private static void Init()
        {   
            if (transform == null)
            {
                try
                {
                    transform = GameObject.FindGameObjectWithTag("UIRoot").transform;
                }
                catch
                {
                    GameObject UIRoot = GameObject.Instantiate(ResourcesManager.Load<GameObject>("UIRoot")) as GameObject;
                    transform = UIRoot.transform;
                }
            }
            if (recyclePool == null)
            {
                recyclePool = transform.Find("RecyclePool");
            }
            if (workStation == null)
            {
                workStation = transform.Find("WorkStation");
            }
            if (fixedStation == null)
            {
                fixedStation = transform.Find("FixedStation");
            }
            if (noticeStation == null)
            {
                noticeStation = transform.Find("NoticeStation");
            }
            if (scriptManager == null)
            {
                scriptManager = transform.Find("ScriptManager");
            }
            isInit = true;
        }
    
        public static void SetParent(Transform window, bool isOpen,UIFormType uIFormType)
        {
            if (isInit == false)
            {
                Init();
            }
            if (isOpen == true)//打开则放置在对应的窗口下
            {
                switch (uIFormType)
                {
                    case UIFormType.Tip:
                        window.SetParent(noticeStation, false);
                        break;
                    case UIFormType.Normal:
                        window.SetParent(workStation, false);
                        break;
                    case UIFormType.Fixed:
                        window.SetParent(fixedStation, false);
                        break;
                    default:
                        break;
                }
            }
            else//否则放入回收池
            {
                window.SetParent(recyclePool, true);
            }

        }

        public static void SetScriptParent(Transform ScriptTrans)
        {
            if (isInit == false)
            {
                Init();
            }
            ScriptTrans.SetParent(scriptManager, false);
        }
	}
}
