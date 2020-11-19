using System.Collections;
using System.Collections.Generic;
using UGUI.Framework;
using UnityEngine;

public class UIMainWindow : UIWindow
{

    protected override void OnAddListener()
    {
        GetChildUIEventListener("PackageButton").onClick += OpenPackageWindow;
        GetChildUIEventListener("TaskButton").onClick += OpenTaskWindow;
        GetChildUIEventListener("StoreButton").onClick += OpenStoreWindow;          
    }
    protected override void OnRemoveListener()
    {
        GetChildUIEventListener("PackageButton").onClick -= OpenPackageWindow;
        GetChildUIEventListener("TaskButton").onClick -= OpenTaskWindow;
        GetChildUIEventListener("StoreButton").onClick -= OpenStoreWindow;
    }

    public void OpenTaskWindow(GameObject gameObject)
    {
        UIManager.Instance.OpenWindow(UIWindowType.Task);      
    }

    public void OpenPackageWindow(GameObject gameObject)
    {
        UIManager.Instance.OpenWindow(UIWindowType.Package);
    }

    public void OpenStoreWindow(GameObject gameObject)
    {
        UIManager.Instance.OpenWindow(UIWindowType.Store);
    }
}
