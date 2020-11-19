using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GenerateResConfig : Editor
{
    //
    //
    //
    //
    //生成菜单种Tool选项
    [MenuItem("Tools/Rescources/Generate ResConfig File")]
    public static void GeneratePrefab()
    {
        //生成资源配置文件
        //1、找到Resources目录下的所有预制件完整路径 t固定:+类型 后面是文件范围
        string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" });
        //2、生成对应关系
        for (int i = 0; i < resFiles.Length; i++)
        {
            //
            resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
            //去掉后缀
            string fileName = Path.GetFileNameWithoutExtension(resFiles[i]);
            string filePath = resFiles[i].Replace("Assets/Resources/", string.Empty).Replace(".prefab", string.Empty);
            //名称=路径
            resFiles[i] = fileName + "=" + filePath;
        }
        //在移动端必须放到Streaming中，该目录在移动端只读不会压缩，在pc端可以写入
        File.WriteAllLines("Assets/StreamingAssets/ConfigMap.txt", resFiles);
        Debug.Log("成功配置预制件路径");
    }

}
