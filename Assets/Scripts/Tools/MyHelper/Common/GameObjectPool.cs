using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public interface IResetable
    {
       void OnReset();
    }
    /*
     * 使用方式：1所有频繁创建删除的物体，通过创建回收
     * GameObjectPool.Instance.CreateObject
     * GameObjectPool.Instance.CollectObject
     * 所有需要通过对象池创建的物体，如果每次创建时执行，则让脚本继承IResetable接口
     * 不使用awake初始化
     */
    /// <summary>
    /// 对象池用于创建需要平凡创建的预制体
    /// </summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
    {
        private Dictionary<string, List<GameObject>> cache;//存多个游戏对象

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 外部调用的创建游戏对象
        /// </summary>
        public GameObject CreateObject(string key, GameObject prefab, Vector3 pos, Quaternion rotate)
        {
            //寻找
            GameObject go = FindUseableObject(key);
            //若没有就添加对象
            if (go == null)
            {
                go = AddObject(key, prefab);
            }
            //使用
            UseObject(go, pos, rotate);
            return go;
        }

        /// <summary>
        /// 使用对象
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="pos"></param>
        /// <param name="rotate"></param>
        private void UseObject(GameObject go, Vector3 pos, Quaternion rotate)
        {
            go.transform.position = pos;
            go.transform.rotation = rotate;
            go.SetActive(true);
            //重置所有go上组件的数据
            foreach (var g in go.GetComponents<IResetable>())
            {
                g.OnReset();
            }
        }

        /// <summary>
        /// 添加对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="prefab"></param>
        private GameObject AddObject(string key, GameObject prefab)
        {
            GameObject go = Instantiate(prefab);
            if (!cache.ContainsKey(key))
                //如果池中没有记录则添加建
                cache.Add(key, new List<GameObject>());
            //添加预制件
            cache[key].Add(go);
            return go;
        }

        /// <summary>
        /// 查找可使用的物体（未被使用）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private GameObject FindUseableObject(string key)
        {
            if (cache.ContainsKey(key))
                return cache[key].Find(g => !g.activeInHierarchy);//未被使用
            return null;
        }

        /// <summary>
        /// 外部调用的回收预制件
        /// </summary>
        /// <param name="go"></param>
        /// <param name="delay"></param>
        public void CollectObject(GameObject go, float delay = 0)
        {
            StartCoroutine(CollectObjectDelay(go, delay));
        }

        /// <summary>
        /// 延时回收销毁
        /// </summary>
        /// <param name="go"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }

        /// <summary>
        /// 清除某个类别的游戏对象
        /// </summary>
        /// <param name="key"></param>
        public void Clear(string key)
        {
            for (int i = cache[key].Count - 1; i >= 0; i--)
            {
                Destroy(cache[key][i]);
            }
            //删除键
            cache.Remove(key);
        }

        /// <summary>
        /// 清空全部
        /// </summary>
        public void ClearAll()
        {
            //foreach (var key in cache.Keys) 遍历字典不能删除所遍历字典的元素
            //所有字典集合都继承了IEnumerable
            //遍历a删b
            foreach(var key in new List<string>(cache.Keys))
            {
                Clear(key);
            }
        }
    }
}
