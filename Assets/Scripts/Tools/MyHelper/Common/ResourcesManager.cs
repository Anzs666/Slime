using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Common
{
    public class ResourcesManager//:MonoSingleton<ResourcesManager>
    {
        //作用初始化静态成员
        //时机：类被加载的时候执行一次

        private static Dictionary<string, string> configMap;


        //public override void Init()
        //{
        //    base.Init();
        //}
        static ResourcesManager()
        {
            //加载文件
            string fileContent = GetConfigFile("ConfigMap.txt");
            BuildMap(fileContent);
        }

        public static string GetConfigFile(string fileName)
        {
            string url;
            //file：//表示本地
            //ConfigMap.txt在那个目录下只能通过WWW类读取
#if UNITY_EDITOR||UNITY_STANDALONE
            //在编译器下||pc下
            url ="file://"+Application.dataPath + "/StreamingAssets/"+fileName;
#elif UNITY_IPHONE
            //在Iphone下
            url ="file://"+Application.dataPath  + "/Raw/"+fileName;
#elif UNITY_ANDROID
            //在Android下
            url ="jar:file://"+Application.dataPath  + "!/assets/"+fileName;
#endif
            WWW www = new WWW(url);
            while (true)
            {
                if (www.isDone)
                    return www.text;
            }
           
        }

        private static IEnumerator WebRead(string url)
        {
            UnityWebRequest unityWebRequest = new UnityWebRequest(url);
            yield return unityWebRequest.SendWebRequest();
            BuildMap(unityWebRequest.downloadHandler.text);
        }

        /// <summary>
        /// 解析文件
        /// </summary>
        /// <param name="fileContent"></param>
        private static void BuildMap(string fileContent)
        {
            configMap = new Dictionary<string, string>();
            //文件名=路径\r\n文件名=路径
            //当程序退出using代码块将自动调用reader.Dispose()
            //逐行读取字符串
            using (StringReader reader=new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine())!= null)//读一行
                {
                    string[] keyValue = line.Split('=');
                    //文件名keyValue[0]    路径keyValue[1]
                    configMap.Add(keyValue[0], keyValue[1]);
                }            
            }
        }

        /// <summary>
        /// 通过预制体名返回预制体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prefabName"></param>
        /// <returns></returns>
        public static T Load<T>(string prefabName) where T : Object
        {
            //Debug.Log("正在寻找"+prefabName)
            if (prefabName == null)return null;
               
            string prefabPath = configMap[prefabName];
            return Resources.Load<T>(prefabPath);
        }

        /// <summary>
        /// 通过预制体名返回预制体路径
        /// </summary>
        /// <param name="prefabName"></param>
        /// <returns></returns>
        public static string GetPath(string prefabName) 
        {
            if (prefabName == null) return null;
            return configMap[prefabName];
        }
    }
}
