using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common.Helper.RandomRoomSystem;
namespace Common
{
    //关卡管理器
    public class LevelManager :MonoSingleton<LevelManager>
    {
        private void Start()
        {
            GetComponent<RoomGenerator>().InitRooms();
        }
    }
}
