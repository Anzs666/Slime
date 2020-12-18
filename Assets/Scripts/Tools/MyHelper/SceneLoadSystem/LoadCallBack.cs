using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Helper
{
    public class LoadCallBack : MonoBehaviour
    {
        private bool isFirstUpdate=true;

        // Update is called once per frame
        void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;
                LoadScenesManager.LoadCallback();
            }
        }
    }
}
