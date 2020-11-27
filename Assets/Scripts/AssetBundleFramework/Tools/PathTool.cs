using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABFW{
	public class PathTool
	{
        //路径常量(根目录)
        public const string AB_RESOURCES = "AB_Res";

        public static string GetABResourcePath()
        {
            return Application.dataPath + "/" + AB_RESOURCES;
        }
        /// <summary>
        /// 获取AB输出路径 ：平台路径+平台名称
        /// </summary>
        /// <returns></returns>
        public static string GetABOutPath()
        {
            return GetPlatformPath() + "/" + GetPlatformName();
        }

        private static string GetPlatformPath()
        {
            string strReturnPlatformPath = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnPlatformPath = Application.streamingAssetsPath;
                    break;
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    strReturnPlatformPath = Application.persistentDataPath;
                    break;               
                default:
                    break;
            }
            return strReturnPlatformPath;
        }

        public static string GetPlatformName()
        {
            string strReturnPlatformName = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnPlatformName ="Windows";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    strReturnPlatformName = "Iphone";
                    break;
                case RuntimePlatform.Android:
                    strReturnPlatformName = "Android";
                    break;
                default:
                    break;
            }
            return strReturnPlatformName;
        }
        /// <summary>
        /// 获取平台www路径（格式固定）
        /// </summary>
        /// <returns></returns>
        public static string GetWWWPath()
        {
            string strReturnWWWPath = string.Empty;
            switch (Application.platform)
            {
                //Windows主平台
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
                    strReturnWWWPath = "file://" + GetABOutPath();
                    break;
                case RuntimePlatform.Android:
                    strReturnWWWPath = "jar:file://"+GetABOutPath();
                    break;
                default:
                    break;
            }
            return strReturnWWWPath;
        }
	}
}
