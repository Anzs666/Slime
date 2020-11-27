using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABFW
{

    public class AssetLoader : System.IDisposable
    {
        //当前Asset
        private AssetBundle _CurrentAssetBundle;
        //缓存容器集合
        private Hashtable _Ht;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="abObj">选择特定的Asset</param>
        public AssetLoader(AssetBundle abObj)
        {
            if (abObj != null)
            {
                _CurrentAssetBundle = abObj;
                _Ht = new Hashtable();
            }
            else
            {
                Debug.Log(GetType() + "AssetLoader/AssetLoader(AssetBundle abObj)/abObj==null");
            }

        }
        /// <summary>
        /// 加载包中指定资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string assetName, bool isCache = false)
        {
            return LoadResource<UnityEngine.Object>(assetName, isCache);
        }
    
        private T LoadResource<T>(string assetName, bool isCache) where T : UnityEngine.Object
        {
            if (_Ht.Contains(assetName))
            {
                return _Ht[assetName] as T;
            }
            T tmpTResource = _CurrentAssetBundle.LoadAsset<T>(assetName);
            if (tmpTResource != null && isCache)
            {
                _Ht.Add(assetName, tmpTResource);
            }
            else if (tmpTResource == null)
            {
                Debug.LogError(GetType() + "/LoadResource<T>()/参数tmpTResource==null!");
            }
            return tmpTResource;
        }
        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public bool UnLoadAsset(UnityEngine.Object asset)
        {
            if (asset != null)
            {
                Resources.UnloadAsset(asset);
                return true;
            }
            Debug.LogError(GetType() + "/UnLoadAsset()/参数asset==null!");
            return false;
        }
        /// <summary>
        /// 释放包中镜像资源
        /// </summary>
        public void Dispose()
        {
            //throw new System.NotImplementedException();
            _CurrentAssetBundle.Unload(false);
        }
        /// <summary>
        /// 释放当前包内所有资源
        /// </summary>
        public void DisposeAll()
        {
            _CurrentAssetBundle.Unload(true);
        }
        /// <summary>
        /// 查询assetBundle所有资源
        /// </summary>
        /// <returns></returns>
        public string[] RetriveAllAssetName()
        {
            return _CurrentAssetBundle.GetAllAssetNames();
        }

    }

}