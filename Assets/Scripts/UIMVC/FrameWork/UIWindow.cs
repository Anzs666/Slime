using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace UGUI.Framework
{


    /// <summary>
    /// 窗口
    /// </summary>
    [RequireComponent(typeof(UIEventListener), typeof(CanvasGroup))]//依赖组件
    public class UIWindow : MonoBehaviour
    {
        //窗体
        protected Transform itransform;
        //资源名称
        protected string resName;
        //是否常驻
        protected bool resident;
        //是否可见
        protected bool visible = false;

        //必要组件
        private CanvasGroup canvasGroup;
        //事件字典
        private Dictionary<string, UIEventListener> uiEventDic;

        public CurrentUIType currentUIType;
        //初始化
        /// <summary>
        /// 查找组件
        /// </summary>
        private void Awake()
        {
            currentUIType = new CurrentUIType() {UIForm_LucencyType=UIFormLucencyType.Lucency,
                UIForm_ShowMode =UIFormShowMode.Normal,
                UIForm_Type=UIFormType.Normal};
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            uiEventDic = new Dictionary<string, UIEventListener>();
            OnAddListener();//注册事件

        }
        private void OnDisable()
        {
            OnRemoveListener();//释放事件
        }

        #region 外部调用
        //生命周期--------------
        public virtual void OnEnter()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
        //暂停/隐藏状态
        public virtual void OnPause()
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        //重启/再显示状态
        public virtual void OnResume()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }
        //冻结状态
        public virtual void OnFreeze()
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = false;
        }
        //退出
        public virtual void OnExit()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
        }
        //=---------------------
        public Transform GetRoot()
        {
            return transform;
        }

        public bool IsVisible()
        {
            return visible;
        }

        public bool IsResident()
        {
            return resident;
        }

        #endregion

        #region 提供给子类调用

        // 根据子物体名称获取ui监听器
        protected UIEventListener GetChildUIEventListener(string name)
        {
            //查找是否存在listener
            if (!uiEventDic.ContainsKey(name))
            {
                //查找子物体
                Transform tf = transform.FindChildByName(name);
                //获取监听器
                UIEventListener uiEventListener = UIEventListener.Get(tf);
                uiEventDic.Add(name, uiEventListener);
            }
            return uiEventDic[name];
        }

        //添加监听游戏事件
        protected virtual void OnAddListener()
        {
        }

        //移除游戏事件
        protected virtual void OnRemoveListener()
        {
        }

        /// <summary>
        ///控制ui显影
        ///ui物体需要添加canvas Group组件
        /// </summary>
        /// <param name="state"></param>
        /// <param name="delay"></param>
        public void SetVisible(bool state, float delay = 0)
        {
            StartCoroutine(SetVisibleDelay(state, delay));
        }

        /// <summary>
        /// 延时控制显隐
        /// </summary>
        /// <param name="state"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator SetVisibleDelay(bool state, float delay)
        {
            yield return new WaitForSeconds(delay);
            //隐藏
            canvasGroup.alpha = state ? 1 : 0;
        }
        #endregion
    }
}
