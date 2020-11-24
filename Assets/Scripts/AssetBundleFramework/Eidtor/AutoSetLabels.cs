/*
 * Title:"AssetBundle简单框架"
 *  
 * Description：
 *      开发思路：
 *          1、定义需要打包资源的文件夹根目录
            2、遍历每个“场景”文件夹（目录）
                A： 遍历本场景目录下所有的目录或者文件，
                    如果是目录，则继续“递归”访问里面的文件，知道定位到文件
                B： 找到文件，则使用AssetImporter类，标记“包名”与“后缀名”

 * Author:Anzs
 * 
 * Date:2020.11.24
 * 
 * Modify:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;//引入
using System.IO;

namespace ABFW{
	public class AutoSetLabels
	{
        /// <summary>
        /// 设置AB包名
        /// </summary>
        [MenuItem("AssetBundleTools/Set AB Label")]
        public static void SetABLabel()
        {
            /*方法局部变量*/
            //需要给AB做标记的根目录
            string strNeedSetLabelRoot = string.Empty;
            //目录信息(场景目录信息数组,表示所有根目录下场景目录)
            DirectoryInfo[] dirScenesDIRArray = null;
            //清空无用AB标记
            AssetDatabase.RemoveUnusedAssetBundleNames();
            strNeedSetLabelRoot = Application.dataPath + "/" + "AB_Res";
            //Debug.Log("")
        }
    
	}
}
