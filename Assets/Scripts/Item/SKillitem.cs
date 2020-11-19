using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGUI.Package;
public class SKillitem : Item
{
    public int skillId { get; set; }
    public SKillitem(int id, string title, int value, int weight,string desp, string _sprite, string _entityStr, ItemType type, int _skillId)
        : base(id, title, value,weight, desp, _sprite, _entityStr, type)
    {
        this.skillId = _skillId;
    }
    public override string ToString()
    {
        return base.ToString() + "/" + skillId.ToString();
    }

}
