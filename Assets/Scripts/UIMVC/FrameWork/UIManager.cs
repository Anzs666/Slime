using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;


namespace UGUI.Framework
{
    //单例模式
    /// <summary>
    /// UI管理器，记录所有窗口，提供所有方法
    /// </summary>
    public class UIManager : MonoSingleton<UIManager>
    {
        private Dictionary<UIWindowType, string> uiWindowPathDic;//获取所有窗体的路径
        private Dictionary<UIWindowType, UIWindow> uiWindowDic;//缓存所有窗体
        private Dictionary<UIWindowType, UIWindow> uiCurrentShowDic;//当前显示的ui

        private Stack<UIWindow> windowStack;

        /// <summary>
        /// 继承了父类单例模式类中的初始化方法
        /// </summary>
        public override void Init()
        {
            base.Init();           
            //获取所有路径
            ParseUIWindowPath();
        }

        //通过枚举获得窗体路径
        private void ParseUIWindowPath()
        {
            uiWindowPathDic = new Dictionary<UIWindowType, string>();
            foreach (int e in System.Enum.GetValues(typeof(UIWindowType)))
            {
                string windowName = "UI" + System.Enum.GetName(typeof(UIWindowType), e) + "Window";
                uiWindowPathDic.Add((UIWindowType)e, ResourcesManager.GetPath(windowName));
            }
        }

        //窗体入栈
        private void PutWindowInStack(UIWindow window)
        {
            
        }

        //窗体出栈
        private void PutWindowOutStack(UIWindow window)
        {

        }

        //关闭窗体
        public void CloseWindow(UIWindowType windowType)
        {
            UIWindow uIWindow;
            uiWindowDic.TryGetValue(windowType, out uIWindow);
            if (uIWindow == null)
                return;
            UIRoot.SetParent(uIWindow.GetRoot(), false, uIWindow.currentUIType.UIForm_Type);
            uIWindow.OnExit();
        }

        //打开窗体
        //从uiWindowDic中查找，若没有则创建缓存
        public UIWindow OpenWindow(UIWindowType windowType)
        {
            if (uiWindowDic == null)
            {
                uiWindowDic = new Dictionary<UIWindowType, UIWindow>();
            }
            UIWindow uIWindow;
            uiWindowDic.TryGetValue(windowType, out uIWindow);
            if (uIWindow == null)
            {
                //初始化窗口
                string path;
                uiWindowPathDic.TryGetValue(windowType, out path);
                GameObject instWindow = GameObject.Instantiate(Resources.Load(path)) as GameObject;
                uIWindow = instWindow.GetComponent<UIWindow>();
                //设置根节点
                UIRoot.SetParent(instWindow.transform, true, uIWindow.currentUIType.UIForm_Type);
                //加入缓存集合
                uiWindowDic.Add(windowType, uIWindow);
                return uIWindow;
            }
            else {
                UIRoot.SetParent(uIWindow.transform, true, uIWindow.currentUIType.UIForm_Type);
                uIWindow.OnEnter();
                return uIWindow;
            }
        }

        public UIWindow GetWindow(UIWindowType windowType)
        {
            if (uiWindowDic == null)
            {
                uiWindowDic = new Dictionary<UIWindowType, UIWindow>();
            }
            UIWindow uIWindow;
            uiWindowDic.TryGetValue(windowType, out uIWindow);
            if (uIWindow == null) uIWindow=OpenWindow(windowType);
            return uIWindow;
         }
        
    }
}
