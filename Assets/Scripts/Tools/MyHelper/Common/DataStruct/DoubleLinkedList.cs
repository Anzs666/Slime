using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Helper.DataStruct
{
    /// <summary>
    /// 链表节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T>
    {
        private T value;//保存的值
        private Node<T> nextNode;//后一个节点
        private Node<T> preNode;//前一个节点
        public Node()
        {
            value = default(T);
            nextNode = null;
            preNode = null;
        }
        public Node(T _value)
        {
            value = _value;
            nextNode = null;
            preNode = null;
        }
        public Node<T> NextNode
        {
            get => nextNode;
            set
            {
                nextNode = value;
                if (value != null&&value.preNode!=this) value.preNode = this;//让下一个节点的前节点连接本类
            }
        }
        public Node<T> PreNode
        {
            get => preNode;
            set
            {
                preNode = value;
                if (value != null&&value.nextNode!=this) value.nextNode = this;//让上一个节点的后节点连接本类
            }
        }
        public T Value { get => value; set => this.value = value; }
    }
    /// <summary>
    /// 双向链表
    /// </summary>
    public class DoubleLinkedList<T>
    {
        private Node<T> head;//头指针
        private int count;
        public DoubleLinkedList()
        {
            head = null;
            count = 0;
        }
        /// <summary>
        /// 添加在最后
        /// </summary>
        /// <param name="item"></param>
        public void AddLast(T item)
        {
            if (isEmpty())
            {
                head = new Node<T>(item);
                head.NextNode = head;
                head.PreNode = head;
            }
            else//不为空
            { 
                Node<T> Temp =head;
                //找到下一个为空的节点
                while (Temp.NextNode!= head)
                {
                    Temp = Temp.NextNode;
                }
                Temp.NextNode = new Node<T>(item) { NextNode = head, PreNode = Temp };
            } 
            count++;
        }     
        /// <summary>
        /// 获得指定位置的节点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Node<T> GetNode(int index)
        {
            if (isEmpty() || index > count || index < 0)
                return default;
            Node<T> Temp = head;
            int i = 0;
            //当下一个节点不为头指针且索引没到
            while (Temp.NextNode != head && i < index - 1)
            {
                Temp = Temp.NextNode;
                i++;
            }
            return Temp;
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="index"></param>
        public void Delete(int index)
        {
            Node<T> Temp=null;
            if ((Temp = GetNode(index)) == default)
                return;
            Temp.PreNode.NextNode = Temp.NextNode;//由于构造时包含了连接不用重写
            Temp.NextNode = null;
            Temp.PreNode = null;
            count--;
        }   
        /// <summary>
        /// 获得指定节点的前节点值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetElemBefore(int index)
        {
            Node<T> Temp = null;
            if ((Temp = GetNode(index)) == default)
                return default;
            return Temp.PreNode.Value;
        }
        /// <summary>
        /// 获取指定结点的后节点值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetElemNext(int index)
        {
            Node<T> Temp = null;
            if ((Temp = GetNode(index)) == default)
                return default;
            return Temp.NextNode.Value;
        }
        /// <summary>
        /// 获得指定节点的值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetElem(int index)
        {
            return GetNode(index).Value;
        }

        /// <summary>
        /// 插入节点
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        public void Insert(T item, int index)
        {

        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <returns></returns>
        public int GetLength()
        {
            return count;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <returns></returns>
        public bool isEmpty()
        {
            return head == null;
        }
        /// <summary>
        /// 清空
        /// </summary>
        public void Clear()
        {
            if (!isEmpty())
            {
               Node<T> temp =head;
                while (true)
                {
                    temp.Value = default(T);
                    if (temp.NextNode== this.head)
                        break;
                    temp = temp.NextNode;
                }
               count = 0;
                this.head = null;
            }
        }


        ///// <summary>
        /////返回值对应的索引
        ///// </summary>
        ///// <returns></returns>
        //public int IndexOfValue()
        //{
        //}

    }
}

