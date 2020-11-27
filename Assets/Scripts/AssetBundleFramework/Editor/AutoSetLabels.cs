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
using UnityEditor;//AssetDatabase
using System.IO;//DirectoryInfo

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
            strNeedSetLabelRoot = PathTool.GetABResourcePath();
            //Debug.Log("### strNeedSetLabelRoot=="+strNeedSetLabelRoot);
            DirectoryInfo direTempInfo = new DirectoryInfo(strNeedSetLabelRoot);
            //获得根目录下的所有文件夹
            dirScenesDIRArray = direTempInfo.GetDirectories();
            //遍历每个场景文件夹
            foreach (DirectoryInfo currentDIR in dirScenesDIRArray)
            {
                string tmpScenesDIR = strNeedSetLabelRoot + "/" + currentDIR.Name;//拼接每个文件夹的全路径
                //DirectoryInfo tmpScenesDIRInfo = new DirectoryInfo(tmpScenesDIR);

                int tmpIndex = tmpScenesDIR.LastIndexOf("/");//找到最后一个子文件

                string tmpScenesName = tmpScenesDIR.Substring(tmpIndex + 1);
                JudgeDIRorFileByRecursive(currentDIR, tmpScenesName);
                //递归调用  找到文件
            }
            AssetDatabase.Refresh();
            Debug.Log("AssetBundle文本文件配置完成");          
        }
        /// <summary>
        /// 判断是否为目录与文件，修改Asset Bundle标记
        /// </summary>
        /// <param name="currentDIR"></param>
        /// <param name="scenesName"></param>
        private static void JudgeDIRorFileByRecursive(FileSystemInfo fileSysInfo, string scenesName)
        {
            if (!fileSysInfo.Exists)
            {
                Debug.LogError("文件"+fileSysInfo+"不存在");
                return;
            }
            //文件信息转换为目录信息
            DirectoryInfo dirInfoObj = fileSysInfo as DirectoryInfo;            
            FileSystemInfo[] fileSystemInfos = dirInfoObj.GetFileSystemInfos();
            //遍历
            foreach (FileSystemInfo fileInfo in fileSystemInfos)
            {
                FileInfo fileInfoObj = fileInfo as FileInfo;
                //文件类型
                if (fileInfoObj != null){
                    //修改标签
                    SetFileABLabel(fileInfoObj, scenesName);
                }
                //目录类型
                else {
                    //递归调用
                    JudgeDIRorFileByRecursive(fileInfo, scenesName);
                }
            }
        }
        /// <summary>
        /// 设置文件标签
        /// </summary>
        /// <param name="fileInfoObj"></param>
        /// <param name="scenesName"></param>
        private static void SetFileABLabel(FileInfo fileInfoObj,string scenesName)
        {
            //Debug.Log("fileInfoObj" + fileInfoObj.Name);
            //Debug.Log("scenesName" + scenesName);
            string strAssetFilePath = string.Empty;
            string strABName = string.Empty;
            //文件检查 对Meta文件不做处理
            if (fileInfoObj.Extension == ".meta") return;
            //得到AB包名称
            strABName = GetABName(fileInfoObj, scenesName);
            //获取文件的相对路径
            int tmpIndex = fileInfoObj.FullName.IndexOf("Assets");
            strAssetFilePath = fileInfoObj.FullName.Substring(tmpIndex);
            //给资源设置AB名称
            AssetImporter tmpImporterObj = AssetImporter.GetAtPath(strAssetFilePath);
            tmpImporterObj.assetBundleName = strABName;
            if (fileInfoObj.Extension == ".unity")
            {
                //定义AB包的扩展名
                tmpImporterObj.assetBundleVariant = "u3d";
            }
            else {
                tmpImporterObj.assetBundleVariant = "ab";
            }
        }
        /// <summary>
        /// 获取AB包名称
        /// </summary>
        /// <param name="fileInfoObj"></param>
        /// <param name="scenesName"></param>
        /// <returns></returns>
        private static string GetABName(FileInfo fileInfoObj, string scenesName)
        {
            string strABName = string.Empty;
            //win路径
            string tmpWinPath = fileInfoObj.FullName;
            //Unity路径
            string tmpUnityPath = tmpWinPath.Replace("\\", "/");
            //定位"场景名称"后面字符位置
            int tmpSceneNamePosition = tmpUnityPath.IndexOf(scenesName) + scenesName.Length;
            //AB包中”类型名称“所在区域
            string strABFileNameArea = tmpUnityPath.Substring(tmpSceneNamePosition + 1);
            //测试
            if (strABFileNameArea.Contains("/"))
            {
                string[] tempStrArray = strABFileNameArea.Split('/');
                //Debug.Log("###tempStrArray[0]:" + tempStrArray[0]);
                strABName = scenesName + "/" + tempStrArray[0];
            }
            else {
                strABName = scenesName + "/" + scenesName;
            }
            return strABName;
        }
	}
}
