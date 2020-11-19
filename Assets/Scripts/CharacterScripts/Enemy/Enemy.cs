using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Character;
using Common;

namespace gojiu.slim
{
	///<summary>
	///敌人基类
	///<summary>
	public class Enemy : MonoBehaviour
	{
        protected SpriteRenderer sp;

        protected void Awake()
        {
            sp = GetComponent<SpriteRenderer>();
        }
        //设置受伤动画
        public void SetHurtFlash()
        {
            sp.material.SetFloat("_FlashAmount", 1);
            StartCoroutine(CancelHurtFlashDelay(0.2f));
        }
        private IEnumerator CancelHurtFlashDelay(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            sp.material.SetFloat("_FlashAmount", 0);
        }
    }
}
