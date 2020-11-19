using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
 
namespace UGUI.Framework
{
    /// <summary>
    /// ui事件管理类
    /// </summary>
    public class UIEventListener : MonoBehaviour, 
        IPointerClickHandler, 
        IPointerDownHandler, 
        IPointerEnterHandler, 
        IPointerExitHandler, 
        IPointerUpHandler,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler
    {
        public delegate void VoidDelegate(GameObject go);//委托事件
        public VoidDelegate onClick;//点击事件
        public VoidDelegate onDown;
        public VoidDelegate onEnter;
        public VoidDelegate onExit;
        public VoidDelegate onUp;
        public VoidDelegate onBeginDrag;
        public VoidDelegate onEndDrag;
        public VoidDelegate onDrag;

        /// <summary>
        /// 通过变换组件获取事件监听器
        /// </summary>
        /// <param name="go"></param>
        /// <returns></returns>
        static public UIEventListener Get(Transform go)
        {
            UIEventListener listener = go.GetComponent<UIEventListener>();
            if (listener == null) listener = go.gameObject.AddComponent<UIEventListener>();
            return listener;
        }

        #region 继承接口
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(gameObject);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            onDown?.Invoke(gameObject);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            onEnter?.Invoke(gameObject);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            onExit?.Invoke(gameObject);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            onUp?.Invoke(gameObject);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke(gameObject);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke(gameObject);
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag?.Invoke(gameObject);
        }
        #endregion
    }
}
