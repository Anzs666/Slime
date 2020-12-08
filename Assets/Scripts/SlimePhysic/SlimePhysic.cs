using System.Collections;
using System.Collections.Generic;
using Common.Helper.DataStruct;
using UnityEngine;
namespace SlimePhysics
{
    [RequireComponent(typeof(MetaballGenerator))]
    public class SlimePhysic : MonoBehaviour
    {
        private MetaballGenerator metaballGenerator;
        private GameObject metaCore;            //史莱姆核心
        private List<GameObject> metaballArray;  //用于存储每一个原子球的循环链表
        private const float G = 1;
        private void Start()
        {
            metaballArray = new List<GameObject>();
            metaballGenerator = GetComponent<MetaballGenerator>();
            metaballGenerator.SetTransParent(transform);
            metaCore = transform.gameObject;
            float x = 0;
            for (int i = 0; i < 20; i++)
            {
                GameObject ball = metaballGenerator.CreateMetabll(MetaballState.Water);
                ball.GetComponent<Transform>().Translate(Vector3.right * (x += 0.02f));
                metaballArray.Add(ball);
            }
        }
        public void AddScale()
        {
            GameObject ball = metaballGenerator.CreateMetabll(MetaballState.Water);
            //ball.GetComponent<Transform>().Translate(Vector3.right * (x += 0.02f));
            metaballArray.Add(ball);
        }
        /// <summary>
        /// 判断外层圆球，圆球之间受到的作用力
        /// </summary>
        /// <param name="affectedPos">被作用球</param>
        /// <param name="affectPos">作用球</param>
        /// <returns></returns>
        private Vector3 MetaBallOnebyOneForce(Vector3 affectedPos, Vector3 affectPos, float forceStength)
        {
            Vector3 addForce = Vector3.zero;
            Vector3 outDirect = (affectedPos - affectPos).normalized, inDirect = (affectPos - affectedPos).normalized;
            float r = (affectedPos - affectPos).magnitude;//半径
            addForce = forceStength * outDirect /r+ inDirect * r * forceStength;
            return addForce;
        }
        /// <summary>
        /// 判断外层圆球和原子间的作用力
        /// </summary>
        /// <param name="affectedPos"></param>
        /// <param name="affectPos"></param>
        /// <param name="forceStength"></param>
        /// <returns></returns>
        private Vector3 MetaBallByCoreForce(Vector3 affectedPos, Vector3 affectPos, float forceStength)
        {
            Vector3 addForce = Vector3.zero;
            Vector3 outDirect = (affectedPos - affectPos).normalized, inDirect = (affectPos - affectedPos).normalized;
            float r = (affectedPos - affectPos).magnitude;//半径
            addForce = forceStength * outDirect /r + inDirect * r * forceStength;
            return addForce;
        }
        private void FixedUpdate()
        {
            Vector3 addForce = Vector3.zero;

            for (int i = 0; i < metaballArray.Count; i++)
            {
                //添加到圆球上的合力
                addForce = Vector3.zero;
                //foreach (var b in metaballArray)
                //{                   
                    //前一个力
                    addForce += MetaBallOnebyOneForce(
                        metaballArray[i].transform.position,
                        metaballArray[i == 0 ? metaballArray.Count - 1 : i - 1].transform.position, 5f);
                    //后一个力
                    addForce += MetaBallOnebyOneForce(
                        metaballArray[i].transform.position,
                        metaballArray[i == metaballArray.Count - 1 ? 0 : i + 1].transform.position, 5f);
                    //球心力
                    addForce += MetaBallByCoreForce(
                        metaballArray[i].transform.position,
                        metaCore.transform.position, 5);
                //}
                metaballArray[i].GetComponent<Rigidbody2D>().AddForce(addForce);
                //添加限制
                if ((metaballArray[i].transform.position - metaCore.transform.position).magnitude > 2f)
                    metaballArray[i].GetComponent<Rigidbody2D>().velocity *= 1-5f*Time.deltaTime;
            }
        }
    }

}