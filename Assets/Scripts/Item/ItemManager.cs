using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace UGUI.Package
{
    public class ItemManager : MonoSingleton<ItemManager>
    {
        private ItemDatabase itemDatabase;

        public void ParseItems()
        {
            itemDatabase = new ItemDatabase();
        }

        public Item FetchItemByID(int id)
        {
           if (itemDatabase == null) ParseItems();
           return itemDatabase.FetchItemByID(id);
        }
    }

}