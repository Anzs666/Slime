using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGUI.Package
{
    public enum ItemType
    {
        Equipment,      //装备
        Disposable,     //一次性物品
        Material,       //材料
        SkillItem,      //技能球
    }
    /// <summary>
    /// 物品类，储存物品的基本信息
    /// </summary>
    public class Item
    {
        public int ID { get; set; }                     //物品id
        public string Name { get; set; }                //物品名称
        public int Value { get; set; }                  //价值
        public int Weight { get; set; }
        public string Desp { get; set; }                //说明             
        public Sprite Sprite { get; set; }              //背包内武器图片更改
        public GameObject Entity { get; set; }          //游戏实体物品
        public ItemType itemType { get; set; }          //物品类型

        public Item(int id, string name, int value,int weight,string desp, string _sprite, string _entityStr, ItemType type)
        {
            this.ID = id;
            this.Name = name;
            this.Value = value;
            this.Weight = weight;
            this.Desp = desp;
            //"Testures/Image/"
            this.Sprite = Resources.Load<Sprite>(_sprite);//通过查找资源文件夹中的图片文件名来添加图片。
            this.Entity = Resources.Load<GameObject>(_entityStr);
            this.itemType = type;
        }

        /// <summary>
        /// 当创建一个空的物体时将ID赋值为-1，避免出现重叠
        /// </summary>
        public Item()
        {
            this.ID = -1;
        }

        public override string ToString()
        {
            string str = ID.ToString();
            str += "/"+Name;
            str += "/" + Value.ToString();
            str += "/" + Desp;
            str += "/" + Sprite;
            str += "/" + itemType.ToString();
            return str;
        }
    }

}