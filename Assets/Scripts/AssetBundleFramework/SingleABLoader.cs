using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABFW{
    public class SingleABLoader : System.IDisposable
    {
        private AssetLoader assetLoader;
        //委托
        private DelLoadComplete _LoadCompleteHandle;
        private string _ABName;
        private string _ABDownLoadPath;

        public SingleABLoader(string abName,DelLoadComplete loadComplete)
        {
            assetLoader = null;
            _ABName = abName;
            //委托定义
            _LoadCompleteHandle = loadComplete;
            _ABDownLoadPath = PathTool.GetWWWPath() + "/" + _ABName;
        }
        /// <summary>
        /// 加载AB资源包
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadAssetBundle() {
            using (WWW www = new WWW(_ABDownLoadPath))
            {
                yield return www;
                ///www下载ab包完成
                if (www.progress >= 1)
                {
                    //获取AssetBundle的实例
                    AssetBundle abObj = www.assetBundle;
                    if (abObj != null)
                    {
                        //实例化引用类
                        assetLoader = new AssetLoader(abObj);
                        //AssetBundle下载完毕通知
                        if (_LoadCompleteHandle != null)
                        {
                            _LoadCompleteHandle.Invoke(_ABName);
                        }
                    }
                    else {
                        Debug.LogError(GetType() + "/LoadAssetBundle()/www下载出错，请检查" + _ABDownLoadPath + "错误");
                    }
                }
            }
        }
        /// <summary>
        /// 加载Ab包内资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string assetName,bool isCache)
        {
            if (assetLoader != null)
            {
                return assetLoader.LoadAsset(assetName, isCache);
            }
            Debug.LogError(GetType() + "/LoadAsset()/参数_AssetLoader==null,请检查！");
            return null;
        }
        /// <summary>
        /// 卸载（AB包中）资源
        /// </summary>
        public void UnLoadAsset(UnityEngine.Object asset)
        {
            if (assetLoader != null)
            {
                assetLoader.UnLoadAsset(asset);
            }
            else {
                Debug.Log(GetType() + "/UnLoadAsset()/参数_AssetLoader==Null!");
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (assetLoader != null)
            {
                assetLoader.Dispose();
                assetLoader = null;
            }
            else
            {
                Debug.Log(GetType() + "/UnLoadAsset()/参数_AssetLoader==Null!");
            }
        }
        /// <summary>
        /// 释放当前AB资源包，并卸载所有资源
        /// </summary>
        public void DisposeAll()
        {
            if (assetLoader != null)
            {
                assetLoader.DisposeAll();
                assetLoader = null;
            }
            else
            {
                Debug.Log(GetType() + "/UnLoadAsset()/参数_AssetLoader==Null!");
            }
        }
        /// <summary>
        /// 查询当前Ab包中所有资源
        /// </summary>
        /// <returns></returns>
        public string[] RetrivalAllAssetName()
        {
            if (assetLoader != null)
            {
                return assetLoader.RetriveAllAssetName();
            }
            Debug.Log(GetType() + "/UnLoadAsset()/参数_AssetLoader==Null!");
            return null;
        }
    }
}
