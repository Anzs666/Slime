using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UGUI.Package
{
    /// <summary>
    /// 类似栈结构
    /// </summary>
    public class ShortCutItemBase
    {
        private int itemMax;
        private int itemNum = 0;
        private List<Item> items = new List<Item>();

        public int ItemMax { get => itemMax; set => itemMax = value; }
        public int ItemNum { get => itemNum; set => itemNum = value; }

        public ShortCutItemBase(int gridsNum)
        {
            ItemMax = gridsNum;
            for (int i = 0; i < ItemMax; i++)
            {
                items.Add(new Item());
            }
        }

        /// <summary>
        /// 通过索引获取物品
        /// </summary>
        /// <param name="_no"></param>
        /// <returns></returns>
        public Item GetItemByNo(int _no)
        {
            return items[_no];
        }

        /// <summary>
        /// 添加Item
        /// </summary>
        /// <param name="itemToAdd"></param>
        public int AddItem(Item itemToAdd)
        {
            if (IsFull()) return -1;             //物品栏已满,添加失败
            int i;
            for (i = 0; i < items.Count; i++)       //找到第一格为-1的
            {
                if (items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    break;
                }
            }
            ItemNum++;
            return i;                            //添加成功
        }

        /// <summary>
        /// 通过id删除item
        /// </summary>
        /// <param name="index"></param>
        public bool DelectItemByNo(int _no)
        {
            if (items[_no].ID == -1) return false;//删除失败
            items[_no] = null;
            items[_no] = new Item();
            ItemNum--;
            return true;                        //删除成功
        }

        /// <summary>
        /// 是否已满
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            return ItemNum >= ItemMax ? true : false;
        }

    }
}
