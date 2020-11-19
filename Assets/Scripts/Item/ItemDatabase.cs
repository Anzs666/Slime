using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace UGUI.Package
{
    public class ItemDatabase
    {
        private List<Item> database;//存放通过读取到的数据库表
        public Dictionary<int, GameObject> Dic_idToObj = new Dictionary<int, GameObject>();
        // Start is called before the first frame update
        public ItemDatabase()
        {
            ConstructItemDatabase();
        }
        /// <summary>
        /// 构造物品数据库，将itemdata中的物品数据导入，导入时自行导入
        /// 通过json构造
        /// </summary>
        private void ConstructItemDatabase()
        {
            if (database == null) database = new List<Item>();
            TextAsset itemText = Resources.Load<TextAsset>("Jsons/Items");
            string itemsJson = itemText.text;
            JSONObject j = new JSONObject(itemsJson);
            foreach (JSONObject temp in j.list)
            {
                string itemTypeStr = temp["ItemType"].str;
                ItemType _itemType = (ItemType)System.Enum.Parse(typeof(ItemType), itemTypeStr);

                int _id = (int)temp["ID"].n;
                string _name = temp["Name"].str;
                int _value = (int)temp["Value"].n;
                int _weight = (int)temp["Weight"].n;
                string _desp = temp["Desp"].str;
                string _sprite = temp["Sprite"].str;
                string _entity = temp["Entity"].str;
                int _skillId = (int)temp["SkillId"].n;
               
                Item item = null;
                switch (_itemType)
                {
                    case ItemType.Disposable:
                        break;
                    case ItemType.Equipment:
                        break;
                    case ItemType.Material:
                        break;
                    case ItemType.SkillItem:
                        item = new SKillitem(_id, _name, _value,_weight,_desp, _sprite, _entity, _itemType,_skillId);
                        break;
                    default:
                        break;
                }
                database.Add(item);
            }

        }
        /// <summary>
        /// 通过id找到物品并添加，用于其他时刻添加装备
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public Item FetchItemByID(int _id)
        {
            for (int i = 0; i < database.Count; i++)
            {
                if (_id == database[i].ID)
                {
                    return database[i];
                }
            }
            return null;
        }
 
    }

}