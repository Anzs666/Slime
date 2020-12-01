using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABFW {
    public class MultiABMgr
    {
        //引用类：“单个AB包加载实现类”
        private SingleABLoader _CurrentSingleABLoader;
        //AB包实现类缓存集合
        private Dictionary<string, SingleABLoader> _DicSingleABLoaderCache;
        //当前场景
        private string _CurrentScenesName;
        //当前AB名称
        private string _CurrentABName;
        //AB包名称与对应依赖关系集合
        private Dictionary<string, ABRelation> _DicABRelation;
        //委托；所有AB包加载完成
        private DelLoadComplete _LoadAllABPackageCompleteHandle;

        //构造函数
        public MultiABMgr(string scenesName, string abName, DelLoadComplete loadAllABPackageCompleteHandle)
        {
            _CurrentScenesName = scenesName;
            _CurrentABName = abName;
            _DicSingleABLoaderCache = new Dictionary<string, SingleABLoader>();
            _DicABRelation = new Dictionary<string, ABRelation>();
            //委托
            _LoadAllABPackageCompleteHandle = loadAllABPackageCompleteHandle;
        }
        /// <summary>
        /// 完成指定AB包的调用
        /// </summary>
        private void CompleteLoadAB(string abName)
        {
            Debug.Log(GetType() + "/当前完成ABName：" + abName+"的加载");
            if (abName.Equals(_CurrentABName))
            {
                _LoadAllABPackageCompleteHandle?.Invoke(abName);
            }
        }
        //相互递归调用
        /// <summary>
        /// 加载AB包
        /// </summary>
        /// <param name="abName"></param>
        /// <returns></returns>
        public IEnumerator LoadAssetBundle(string abName)
        {
            //1AB包关系建立起来
            if (!_DicABRelation.ContainsKey(abName))
            {
                ABRelation abRelationObj = new ABRelation(abName);
                _DicABRelation.Add(abName, abRelationObj);
            }
            ABRelation tmpABRelationObj = _DicABRelation[abName];
            //得到AB包所有的依赖关系（查询Manifest清单文件）
            string[] strDependenceArray = ABManifestLoader.GetInstance().RetrivalDependce(abName);
            foreach (string t in strDependenceArray)
            {
                //添加依赖项
                tmpABRelationObj.AddDependence(t);
                //添加引用项
                //协程方法(递归调用)
                yield return LoadReference(t, abName);
            }
            //开始加载AB包
            if (_DicSingleABLoaderCache.ContainsKey(abName))
            {
                yield return _DicSingleABLoaderCache[abName].LoadAssetBundle();
            }
            else {
                _CurrentSingleABLoader = new SingleABLoader(abName, CompleteLoadAB);
                _DicSingleABLoaderCache.Add(abName, _CurrentSingleABLoader);
                yield return _CurrentSingleABLoader.LoadAssetBundle();
            }
        }
        /// <summary>
        /// 加载引用Ab包
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="refABName"></param>
        /// <returns></returns>
        private IEnumerator LoadReference(string abName, string refABName)
        {
            if (_DicABRelation.ContainsKey(abName))
            {
                ABRelation tmpRelationObj = _DicABRelation[abName];
                //添加Ab包引用关系
                tmpRelationObj.AddReference(refABName);
            }
            else {
                ABRelation tmpABRelationObj = new ABRelation(abName);
                tmpABRelationObj.AddReference(refABName);
                _DicABRelation.Add(abName, tmpABRelationObj);

                //开始加载依赖包（递归调用）
                yield return LoadAssetBundle(abName);
            }
            yield return null;
        }     
        /// <summary>
        /// 加载Ab包中资源
        /// </summary>
        /// <param name="abName"></param>
        /// <param name="assetName"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public UnityEngine.Object LoadAsset(string abName, string assetName, bool isCache)
        {
            foreach (var item in _DicSingleABLoaderCache.Keys)
            {
                if (abName == item)
                {
                    return _DicSingleABLoaderCache[item].LoadAsset(assetName, isCache);
                }                 
            }
            Debug.LogError(GetType() + "/LoadAsset()/找不到Asset bundle，无法加载资源，请检查！abName=" + abName + "assetName=" + assetName);
            return null;
        }
        /// <summary>
        /// 释放本场景中所有资源
        /// </summary>
        public void DisposeAllAsset()
        {
            try
            {
                foreach (SingleABLoader aBLoader in _DicSingleABLoaderCache.Values)
                {
                    aBLoader.DisposeAll();
                }
            }
            catch {
                _DicSingleABLoaderCache.Clear();
                _DicSingleABLoaderCache = null;
            }
        }

	}
}
