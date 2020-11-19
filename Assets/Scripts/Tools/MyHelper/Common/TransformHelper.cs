using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    public static class TransformHelper 
    {
        /// <summary>
        /// 未知层级查找子类元素
        /// 核心思想如果没有找到子物体则交给后代
        /// </summary>
        /// <param name="currentTF"></param>
        /// <param name="childName"></param>
        /// <returns></returns>
        public static Transform FindChildByName(this Transform currentTF,string childName)
        {
            //在子物体查找
            Transform childTF = currentTF.Find(childName);
            if (childTF != null) return childTF;

            //交给子物体
            for (int i = 0; i < currentTF.childCount; i++)
            {
                childTF = FindChildByName(currentTF.GetChild(i), childName);
                if (childName != null) return childTF; 
            }
            return null;
        }

        //获取子物体脚本
        public static T GetChildComponetScript<T>(this Transform currentTF,string childName) where T:Component
        {
            Transform childTF = FindChildByName(currentTF, childName);
            if (childTF != null) return childTF.gameObject.GetComponent<T>();
            else return null;
        }

        //给子物体添加脚本
        public static T AddChildComponetScript<T>(this Transform currentTF, string childName) where T : Component
        {
            Transform childTF = FindChildByName(currentTF, childName);
            if (childTF != null)
            {
                T[] componetArray = childTF.GetComponents<T>();
                for (int i = 0; i < componetArray.Length; i++)
                {
                    if (componetArray[i] != null)
                    {
                        //componetArray[i].Destroy()
                    }
                }
                return childTF.gameObject.AddComponent<T>();
            }
            else return null;           
        }

        //给子节点添加父对象
        public static void AddChildToParent(Transform parent, Transform child)
        {
            child.SetParent(parent, false);
            child.localEulerAngles = Vector3.zero;
            child.localPosition= Vector3.zero;
            child.localScale= Vector3.one;
        }
    }
}
