using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class ArrayHelper
    {
        /// <summary>
        /// 查找满足条件的单个数组元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T Find<T>(this T[] array, Func<T, bool> condition)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    return array[i];
                }
            }
            return default(T);//不满足返回默认值
        }

        /// <summary>
        /// 查找所有符合条件的数组元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T[] FindAll<T>(this T[] array, Func<T, bool> condition)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                if (condition(array[i]))
                {
                    list.Add(array[i]);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 获得最大值
        /// </summary>
        /// <typeparam name="T">比较的数组</typeparam>
        /// <typeparam name="Q">返回的类型</typeparam>
        /// <param name="array"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public static T GetMax<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            T max = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                //比较条件
                if (condition(max).CompareTo
                    (condition(array[i])) < 0)
                {
                    max = array[i];
                }
            }
            return max;
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static T GetMin<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            if (array.Length == 0)
                return default;
            if (array.Length == 1)
                return array[0];
            T min = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                //比较条件
                if (condition(min).CompareTo
                    (condition(array[i])) < 0)
                {
                    min = array[i];
                }
            }
            return min;
        }

        /// <summary>
        /// 升序排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q">返回值类型</typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        public static void OrderBy<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (condition(array[j]).CompareTo(condition(array[j + 1])) > 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        ///降序排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q">返回值类型</typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        public static void OrderDescending<T, Q>(this T[] array, Func<T, Q> condition) where Q : IComparable
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (condition(array[j]).CompareTo(condition(array[j + 1])) < 0)
                    {
                        T temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// 筛选数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="array"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static Q[] Select<T, Q>(this T[] array, Func<T, Q> condition)
        {
            Q[] result = new Q[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                //加入满足条件的元素
                result[i] = condition(array[i]);
            }
            return result;
        }
    }
}
