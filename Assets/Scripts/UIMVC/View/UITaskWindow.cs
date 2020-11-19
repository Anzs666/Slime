using System.Collections;
using System.Collections.Generic;
using UGUI.Framework;
using UnityEngine;

public class UITaskWindow :UIWindow
{
    protected override void OnAddListener()
    {
        GetChildUIEventListener("CloseButton").onClick += CloseButtonClick;
    }
    protected override void OnRemoveListener()
    {
        GetChildUIEventListener("CloseButton").onClick -= CloseButtonClick;
    }
    private void CloseButtonClick(GameObject gameObject)
    {
        UIManager.Instance.CloseWindow(UIWindowType.Task);
    }
}
