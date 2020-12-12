using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
namespace ABFW{
    /// <summary>
    /// 读取清单文件
    /// </summary>
    public class ABManifestLoader : System.IDisposable
    {
        //本类实例，单例
        private static ABManifestLoader instance;
        //AssetBundle（清单文件)系统类
        private AssetBundleManifest _ManifestObj;
        //AssetBundle清单文件的路径
        private string _StrManifestPath;
        //读取AB清单文件的AssetBundle
        private AssetBundle _AbReadManifest;
        //是否加载完成
        private bool isLoadFinish;
        public bool IsLoadFinish { get => isLoadFinish;}
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public static ABManifestLoader GetInstance()
        {
            if (instance == null)
            {
                instance=new ABManifestLoader();
            }
            return instance;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ABManifestLoader()
        {
            _StrManifestPath = PathTool.GetWWWPath()+'/'+PathTool.GetPlatformName();
            _ManifestObj = null;
            _AbReadManifest = null;
            isLoadFinish = false;
        }
        /// <summary>
        /// 加载清单文件
        /// </summary>
        /// <returns></returns>
        public IEnumerator loadManifestFile()
        {           
            using (WWW www = new WWW(_StrManifestPath))
            {
                yield return www;
                if (www.progress >= 1)
                {
                    //加载完成获取AssetBundle实例
                    AssetBundle abObj = www.assetBundle;
                    if (abObj != null)
                    {
                        _AbReadManifest = abObj;
                        //读取清单文件资源
                        _ManifestObj = _AbReadManifest.LoadAsset(ABDefine.ASSETBUNDLE_MANIFEST) as AssetBundleManifest;
                        //本次读取完成
                        isLoadFinish = true;
                    }
                    else
                    {
                        Debug.Log(GetType() + "/LoadManifest()/www下载出错，请检查!_StrManifestPath" + _StrManifestPath+"错误信息"+www.error);
                    }

                }
            }
        }
        /// <summary>
        /// 获取AssetBundleManifest实例
        /// </summary>
        /// <returns></returns>
        public AssetBundleManifest GetABManifest()
        {
            if (isLoadFinish){
                if (_ManifestObj != null){
                    return _ManifestObj;
                }
                else {
                    Debug.Log(GetType()+"/GetManifest()/_ManifestObj==null!请检查！");
                }
            }
            else {
                Debug.Log(GetType()+"/GetManifest()/_IsLoadFinish==false,Manifest没有加载完毕，请检查！");
            }
            return null;
        }
        /// <summary>
        /// 获取Asset bundle所有依赖项
        /// </summary>
        /// <param name="abName"></param>
        /// <returns></returns>
        public string[] RetrivalDependce(string abName)
        {
            if (_ManifestObj != null && !string.IsNullOrEmpty(abName))
            {
                return _ManifestObj.GetAllDependencies(abName);
            }
            return null;
        }
        /// <summary>
        /// 释放本类资源
        /// </summary>
        public void Dispose()
        {
            if (_AbReadManifest != null)
            {
                _AbReadManifest.Unload(true);
            }
            throw new System.NotImplementedException();
        }
    }
}
