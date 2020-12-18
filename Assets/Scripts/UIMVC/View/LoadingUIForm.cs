using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SUIFW
{
    public class LoadingUIForm : BaseUIForm
    {
        private Image ImgLoadingBar;
        // Start is called before the first frame update
        void Start()
        {
            ImgLoadingBar = transform.GetChildComponetScript<Image>("ImgLoadingBar");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}