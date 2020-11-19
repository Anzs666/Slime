using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace UGUI.Package
{
    public class ItemGenerator : MonoSingleton<ItemGenerator>
    {

        //在指定地点生成物品
        public GameObject CreateItemGameEntityAtPos(Transform pos, int id)
        {
            Item item = ItemManager.Instance.FetchItemByID(id);
            if (item == null)
                return null;
            GameObject itemObj = GameObject.Instantiate(item.Entity);
            itemObj.AddComponent<ItemGameEntity>();
            itemObj.transform.position = pos.position;
            itemObj.GetComponent<ItemGameEntity>().item = item;
            return itemObj;
        }
    }
}
