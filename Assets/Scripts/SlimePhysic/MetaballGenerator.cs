using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SlimePhysics
{
    public class MetaballGenerator : MonoBehaviour
    {
        private Transform metaballParent;
        /// <summary>
        /// 设置父物体
        /// </summary>
        /// <param name="parent"></param>
        public void SetTransParent(Transform parent)
        {
            metaballParent = parent;
        }
        /// <summary>
        /// 生成原子球
        /// </summary>
        /// <param name="metaballState"></param>
        public GameObject CreateMetabll(MetaballState metaballState)
        {
            GameObject newLiquidParticle = (GameObject)Instantiate(Resources.Load("LiquidPhysics/DynamicParticle"));
            DynamicParticle particleScript = newLiquidParticle.GetComponent<DynamicParticle>();
            newLiquidParticle.transform.position = metaballParent.position;
            //newLiquidParticle.transform.parent = metaballParent;
            return newLiquidParticle;
        }
    }
}
