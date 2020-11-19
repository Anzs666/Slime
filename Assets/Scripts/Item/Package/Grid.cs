using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;
using UnityEngine.UI;

namespace UGUI.Package
{
    public class Grid : MonoBehaviour//, IDropHandler
    {
        public int gridID;//保存本格子的id用于物体拖拽
        private ItemGridUI itemUI;
        private int currentItemAmount = 0;
        private int maxItemAmount;
        private int currentItemWeight=0;
        private int maxItemWeight;

        public int MaxItemAmount { set => maxItemAmount = value; }
        public int MaxItemWeight { set => maxItemWeight = value; }

        public void AddItem(Item itemToAdd)
        { 
            if (itemUI == null)
            {
                GameObject itemObj = Instantiate(ResourcesManager.Load<GameObject>("ItemInGrid_Sample"));//用itemObj表示实例物体，真正添加到游戏上的物体                 
                itemObj.transform.position = Vector2.zero;      //将物品的坐标归零到父容器               
                itemObj.transform.SetParent(transform, false);  //记得关闭世界坐标       
                itemUI = itemObj.GetComponent<ItemGridUI>();
                itemUI.item = itemToAdd;
                itemUI.gridIndex = gridID;
                itemUI.SetImage(itemToAdd.Sprite);              //物品的图标          
            }
            currentItemAmount++;
            currentItemWeight += itemToAdd.Weight;
            itemUI.SetText(currentItemAmount.ToString());   //物品数量     
        }

        public void DropItem()
        {

        }
        public bool IsOverWeight(int weight)
        {
            return currentItemWeight + weight > maxItemWeight ? true : false;
        }
        public bool Isfilled()
        {
            return currentItemAmount >= maxItemAmount ? true : false;
        }

       

        //private UIPackageWindow PackageSp;

        //void Start()
        //{
        //    PackageSp = GameObject.Find("UIPackageWindow(Clone)").GetComponent<UIPackageWindow>();

        //}

        ///// <summary>
        ///// 查看到底该格子是否是空格子
        ///// </summary>
        ///// <param name="eventData"></param>
        //public void OnDrop(PointerEventData eventData)
        //{
        //    ItemGridEntity droppedItem = eventData.pointerDrag.GetComponent<ItemGridEntity>();
        //    if (PackageSp.items[gridID].ID == -1)//如果该格子为空
        //    {
        //        PackageSp.items[droppedItem.gridIndex] = new Item();
        //        droppedItem.gridIndex = gridID;
        //        PackageSp.items[gridID] = droppedItem.item;
        //    }
        //}
    }
}
