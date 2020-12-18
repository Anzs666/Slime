using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Common;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    public class MonoMrg:Singleton<MonoMrg>
    {
        private MonoController controller;

        public MonoMrg()
        {
            GameObject obj = new GameObject("MonoController");
            controller=obj.AddComponent<MonoController>();
        }

        public void AddUpdateListener(UnityAction fun)
        {
            controller.AddUpdateListener(fun);
        }

        public void RemoveUpdateListener(UnityAction fun)
        {
            controller.RemoveUpdateListener(fun);
        }
        #region 协程操作
        /// <summary>
        /// 开启协程
        /// </summary>
        /// <returns></returns>
        public Coroutine StartCoroutine(string methodName) {
            return controller.StartCoroutine(methodName);
        }

        public Coroutine StartCoroutine(IEnumerator enumerator) {
            return controller.StartCoroutine(enumerator);
        }

        public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return controller.StartCoroutine(methodName, value);
        }


        public void StopAllCoroutines()
        {
            controller.StopAllCoroutines();
        }

        public void StopCoroutine(IEnumerator routine)
        {
            controller.StopCoroutine(routine);
        }

        public void StopCoroutine(Coroutine routine)
        {
            controller.StopCoroutine(routine);
        }

        public void StopCoroutine(string methodName)
        {
            controller.StopCoroutine(methodName);
        }
        #endregion
    }
}
