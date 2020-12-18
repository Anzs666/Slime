using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;//调用事件系统
using UnityEngine.UI;

namespace UGUI.Package
{
    /// <summary>
    /// 用于储存物品的基本信息，以及对物品的操作,该类添加到物品item的预设上
    /// </summary>
    public class ItemGridUI : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Item item;//存放物品信息
        public int gridIndex;//表示拖动前的父容器格子
       // private UIPackageWindow PackageSp;

        public void SetImage(Sprite sprite)
        {
            GetComponent<Image>().sprite = sprite;
        }
        public void SetText(string _text)
        {
            //TransformHelper.FindChildByName(transform,"Text").GetComponent<Text>().text = _text;
        }
        
        void Start()
        {
            //PackageSp = GameObject.Find("UIPackageWindow(Clone)").GetComponent<UIPackageWindow>();
        }

        //public void OnBeginDrag(PointerEventData eventData)
        //{
        //    if (item != null)//这步！=null很重要
        //    {
        //        this.transform.SetParent(transform.parent.parent);
        //        this.transform.position = eventData.position;
        //        GetComponent<CanvasGroup>().blocksRaycasts = false;//关闭射线阻挡
        //    }
        //}

        //public void OnDrag(PointerEventData eventData)
        //{
        //    if (item != null)
        //    {
        //        this.transform.position = eventData.position;
        //    }
        //}

        //public void OnEndDrag(PointerEventData eventData)
        //{
        //    this.transform.SetParent(PackageSp.grids[gridIndex].transform);//放在背包下的指定格子中
        //    this.transform.position = this.transform.parent.position;//将位置坐标归零
        //    GetComponent<CanvasGroup>().blocksRaycasts = true;//启用射线
        //}

    }
}
