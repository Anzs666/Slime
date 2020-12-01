using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace ABFW{
    /// <summary>
    /// 以场景为单位
    /// 读取manifest清单文件，缓存脚本
    /// 以场景为单位管理整个项目中所有的AssetBundle包
    /// </summary>
	public class AssetBundleMgr:MonoSingleton<AssetBundleMgr>
	{
        //场景集合
        private Dictionary<string, MultiABMgr> _DicAllScenes = new Dictionary<string, MultiABMgr>();
        //AssetBundle(清单文件)系统类
        private AssetBundleManifest _ManifestObj = null;
        //Awake调用Init
        public override void Init()
        {
            StartCoroutine(ABManifestLoader.GetInstance().loadManifestFile());
        }
        /// <summary>
        /// 下载指定包
        /// </summary>
        /// <param name="scenesName"></param>
        /// <param name="abName"></param>
        /// <param name="loadAllCompleteHandle"></param>
        /// <returns></returns>
        public IEnumerator LoadAssetBundlePack(string scenesName, string abName, DelLoadComplete loadAllCompleteHandle)
        {          
            //参数检查
            if (string.IsNullOrEmpty(scenesName) || string.IsNullOrEmpty(abName))
            {
                Debug.LogError(GetType() + "/loadAssetBundlePack()/ScenesName or abName is null,请检查!");
            }
            //等待Manifest清单文件加载
            while (!ABManifestLoader.GetInstance().IsLoadFinish)
            {
                yield return null;
            }
            _ManifestObj = ABManifestLoader.GetInstance().GetABManifest();
            if (_ManifestObj == null)
            {
                Debug.LogError(GetType() + "/loadAssetBundlePack()/_ManifestObj is null,请检查!");
                yield return null;
            }
            //当前场景加入集合中
            if (!_DicAllScenes.ContainsKey(scenesName))
            {
                MultiABMgr multiMgrObj = new MultiABMgr(scenesName, abName, loadAllCompleteHandle);
                _DicAllScenes.Add(scenesName, multiMgrObj);
            }
           
            //调用下一层("多包管理类")
            MultiABMgr tmpMultiMgrObj = _DicAllScenes[scenesName];
            if (tmpMultiMgrObj == null)
            {
                Debug.LogError(GetType() + "/loadAssetBundlePack()/tmpMultiMgrObj is null,请检查!");
            }
            //调用多包管理类的加载指定ab包
            yield return tmpMultiMgrObj.LoadAssetBundle(abName);
        }
        /// <summary>
        /// 加载AB包中的资源
        /// </summary>
        /// <param name="scenesName"></param>
        /// <param name="abName"></param>
        /// <param name="isCache2">是否使用资源缓存</param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string scenesName, string abName, string assetName, bool isCache)
        {
            if (_DicAllScenes.ContainsKey(scenesName))
            {
                MultiABMgr multiObj = _DicAllScenes[scenesName];
                return multiObj.LoadAsset(abName, assetName, isCache);
            }
            Debug.LogError(GetType() + "LoadAsset()/找不到场景名称,无法加载资源,请检查...scenesName" + scenesName);
            return null;
        }
        /// <summary>
        /// 释放场景
        /// </summary>
        public void DisposeAllAssets(string scenesName)
        {
            if (_DicAllScenes.ContainsKey(scenesName))
            {
                MultiABMgr multiObj = _DicAllScenes[scenesName];
                multiObj.DisposeAllAsset();
            }
            else
            Debug.LogError(GetType() + "DisposeAllAssets()/找不到场景名称,无法释放资源,请检查...scenesName" + scenesName);          
        }
    }
}
