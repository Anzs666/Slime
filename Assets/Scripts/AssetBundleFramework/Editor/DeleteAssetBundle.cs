using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace ABFW{
	public class DelectAssetBundle
	{
        [MenuItem("AssetBundleTools/DeleteAllAssetBundles")]
        public static void DelAssetBundle()
        {
            string strNeedDeleteDIR = string.Empty;

            strNeedDeleteDIR = PathTool.GetABOutPath();
            if (!string.IsNullOrEmpty(strNeedDeleteDIR))
            {
                Directory.Delete(strNeedDeleteDIR, true);
                File.Delete(strNeedDeleteDIR + ".meta");
                AssetDatabase.Refresh();
            }
        }
    
	}
}
