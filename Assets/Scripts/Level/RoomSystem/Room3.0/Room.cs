using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Common.Helper.RandomRoomSystem
{
    public class Room : MonoBehaviour
    {
        private List<Transform> BlocksGrid;
        //初始化方块类型
        public void InitBlocks(GameObject[] _blockType)
        {
            if (BlocksGrid == null) BlocksGrid = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                BlocksGrid.Add(transform.GetChild(i));
                GameObject block = GameObject.Instantiate(_blockType[Random.Range(0, _blockType.Length)], BlocksGrid[i].position, Quaternion.identity);
                block.transform.SetParent(BlocksGrid[i]);
            }
        }
    }
}