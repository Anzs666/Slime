using System.Collections;
using System.Collections.Generic;
using ABFW;
using UnityEngine;

public class TestABFramework : MonoBehaviour
{
    private string _ScenesName = "scenes_1";
    private string _AssetBundleName = "scenes_1/prefabs.ab";
    private string _AssetName = "SlimeA.prefab";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AssetBundleMgr.Instance.LoadAssetBundlePack(_ScenesName, _AssetBundleName, loadAllComplete));
        //StartCoroutine(AssetBundleMgr.Instance.Test());
    }

    private void loadAllComplete(string abName)
    {   
        UnityEngine.Object tmpObj = null;
        tmpObj=AssetBundleMgr.Instance.LoadAsset(_ScenesName, _AssetBundleName, _AssetName, false);
        if (tmpObj != null)
        {
            Debug.Log(1);
            Instantiate(tmpObj);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AssetBundleMgr.Instance.DisposeAllAssets(_ScenesName);
        }
    }
}
