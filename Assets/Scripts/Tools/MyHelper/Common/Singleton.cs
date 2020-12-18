using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common
{
    /// <summary>
    /// 单例模式基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T>where T:Singleton<T>,new()
    {
        private static T instance;     
        public static T Instance{
            get
            {
                if (instance == null)
                    instance = new T();
                else {
                    instance.Init();
                }
                return instance;
            }
        }
        public virtual void Init() { }
    }
}
