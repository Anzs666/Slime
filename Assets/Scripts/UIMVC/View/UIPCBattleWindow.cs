using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using UGUI.Framework;
using UGUI.Package;

public class UIPCBattleWindow:UIWindow
{
    public void ShortCutAddItem(Item item)
    {
        TransformHelper.FindChildByName(transform,"ShortCutPanel").GetComponent<ShortCutManager>().AddItem(item);
    }
}
