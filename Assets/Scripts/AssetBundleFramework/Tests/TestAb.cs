using System.Collections;
using System.Collections.Generic;
using ABFW;
using UnityEngine;

public class TestAb : MonoBehaviour
{
    //引用类
    private SingleABLoader _LoadObj = null;
    //依赖AB包名称

    //AB包名称
    private string _ABName1 = "scenes_1/Prefabs.ab";
    //AB包资源名称
    private string _AssetName1 = "SlimeA.prefab";

    #region 简单（无依赖包）加载
    //// Start is called before the first frame update
    //void Start()
    //{
    //    _LoadObj = new SingleABLoader(_ABName1, LoadComplete);
    //    StartCoroutine(_LoadObj.LoadAssetBundle());
    //}
    ///// <summary>
    ///// 回调函数
    ///// </summary>
    ///// <param name="abName"></param>
    //private void LoadComplete(string abName)
    //{
    //    //加载AB包中的资源
    //    UnityEngine.Object tmpObj = _LoadObj.LoadAsset(_AssetName1, false);
    //    Instantiate(tmpObj);
    //}
    //// Update is called once per frame
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _LoadObj = new SingleABLoader(_ABName1, LoadComplete);
        StartCoroutine(_LoadObj.LoadAssetBundle());
    }
    private void LoadDependComplete(string abName)
    {
        Debug.Log("");
    }
    /// <summary>
    /// 回调函数
    /// </summary>
    /// <param name="abName"></param>
    private void LoadComplete(string abName)
    {
        //加载AB包中的资源
        UnityEngine.Object tmpObj = _LoadObj.LoadAsset(_AssetName1, false);
        Instantiate(tmpObj);
    }
    // Update is called once per frame

}
