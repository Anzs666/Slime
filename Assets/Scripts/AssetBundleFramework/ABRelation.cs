using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ABFW
{
    public class ABRelation
    {
        //当前AB包名称
        private string _ABName;
        //本包所有的依赖包集合
        private List<string> _listAllDependenceAB;
        //本包所有的引用包集合
        private List<string> _listAllReferenceAB;

        public ABRelation(string abName)
        {
            if (!string.IsNullOrEmpty(abName))
                _ABName = abName;
            _listAllDependenceAB = new List<string>();
            _listAllReferenceAB = new List<string>();
        }
        /*依赖关系*/
        /// <summary>
        /// 增加依赖关系
        /// </summary>
        /// <param name="abName"></param>
        public void AddDependence(string abName)
        {
            if (!_listAllDependenceAB.Contains(abName))
                _listAllDependenceAB.Add(abName);
        }
        /// <summary>
        /// 移除引用关系
        /// true: 此ab包没有依赖项
        /// false：还有其它依赖项
        /// </summary>
        /// <param name="abName"></param>
        /// <returns></returns>
        public bool RemoveDependence(string abName)
        {
            if (_listAllDependenceAB.Contains(abName))
                _listAllDependenceAB.Remove(abName);
            if (_listAllDependenceAB.Count > 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 获取所有的引用关系
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllDependence()
        {
            return _listAllDependenceAB;
        }
        //引用关系
        /// <summary>
        /// 增加引用关系
        /// </summary>
        /// <param name="abName"></param>
        public void AddReference(string abName)
        {
            if (!_listAllReferenceAB.Contains(abName))
                _listAllReferenceAB.Add(abName);
        }
        /// <summary>
        /// 获取所有的引用关系
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllReference()
        {
            return _listAllReferenceAB;
        }
        /// <summary>
        /// 移除引用关系
        /// </summary>
        /// <param name="abName"></param>
        /// <returns></returns>
        public bool RemoveReference(string abName)
        {
            if (_listAllReferenceAB.Contains(abName))
                _listAllReferenceAB.Remove(abName);
            if (_listAllReferenceAB.Count > 0)
                return false;
            else
                return true;
        }
    }
}
