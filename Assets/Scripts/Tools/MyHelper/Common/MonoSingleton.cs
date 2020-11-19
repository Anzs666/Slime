using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    //单例模板类
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>//T必须继承了MonoSingleton
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    //在场景中找到引用
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        //创建脚本对象立即执行Awake
                        new GameObject("Signleton of" + typeof(T)).AddComponent<T>();
                    }
                    else
                    {
                        _instance.Init();
                    }
                }

                return _instance;
            }
        }

        protected void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                Init();
            }
        }

        public virtual void Init() { }
    }
}
