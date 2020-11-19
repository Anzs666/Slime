using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UGUI.Package
{
    public class ShortCutManager : MonoBehaviour
    {
        private ShortCutItemBase itemBase;                                  //用于储存基本信息

        [Header("Panels")]
        public GameObject[] panels = new GameObject[6];


        public delegate void ItemChangeEventHandler(Item changeItem);      //快捷栏改变委托
        public event ItemChangeEventHandler ItemChange;
        // Update is called once per frame


        /// <summary>
        /// 通过id删除item
        /// </summary>
        /// <param name="index"></param>
        public void DelectItemByNo(int itemNo)
        {
            if (itemBase == null || !itemBase.DelectItemByNo(itemNo)) return;
            panels[itemNo].transform.GetChild(0).GetComponent<Image>().sprite = null;
        }

        /// <summary>
        /// 添加Item
        /// </summary>
        /// <param name="itemToAdd"></param>
        public void AddItem(Item itemToAdd)
        {
            Debug.Log("添加了");
            if (itemBase == null) itemBase = new ShortCutItemBase(panels.Length);
            int addNo = itemBase.AddItem(itemToAdd);
            if (addNo == -1) return;
            //找到格子的图片物品的图标
            panels[addNo].transform.GetChild(0).GetComponent<Image>().sprite = itemToAdd.Sprite;
            //panels[addNo].transform.GetChild(0).GetComponent<Image>().
        }
    }
}