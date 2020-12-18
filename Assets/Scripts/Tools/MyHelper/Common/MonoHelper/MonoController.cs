using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common
{
    /// <summary>
    /// 能让普通类也使用Update协程等功能
    /// </summary>
    public class MonoController : MonoBehaviour
    {
        private event UnityAction updateEvent;
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
        private void Update()
        {
            updateEvent?.Invoke();
        }
        
        public void AddUpdateListener(UnityAction fun)
        {
            updateEvent += fun;
        }

        public void RemoveUpdateListener(UnityAction fun)
        {
            updateEvent -= fun;
        }

    }
}
