using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Common.Helper
{
    public enum Scene
    {
        Loading,
        Town,
        MainStart
    }
    public static class LoadScenesManager
    {
        //场景加载回调
        private static Action onLoaderCallback;
        //场景异步加载类
        private static AsyncOperation loadingAsyncOperation;
        public static void Load(Scene scene)
        {
            onLoaderCallback = () =>
            {
                //调用Mono公共类启用协程
                MonoMrg.Instance.StartCoroutine(LoadSceneAsync(scene));
            };
            SceneManager.LoadScene(Scene.Loading.ToString());
        }
        /// <summary>
        /// 异步加载场景1
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        private static IEnumerator LoadSceneAsync(Scene scene)
        {
            yield return null;
            loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
            while (!loadingAsyncOperation.isDone){
                yield return null;
            }
        }
        /// <summary>
        /// 获取Loading加载百分比
        /// </summary>
        /// <returns></returns>
        public static float GetLoadingProgress() {
            if (loadingAsyncOperation != null){
                return loadingAsyncOperation.progress;
            }
            else {
                return 0;
            }
        } 

        /// <summary>
        /// 场景加载回调
        /// </summary>
        public static void LoadCallback()
        {
            if (onLoaderCallback != null){
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }
    }

}
