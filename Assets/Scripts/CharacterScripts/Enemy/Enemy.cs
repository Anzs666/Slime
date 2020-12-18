using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Character;
using Common;
using Common.Helper.RoomSystem;
using System;

namespace gojiu.slim
{
    ///<summary>
    ///敌人基类
    ///<summary>
    public class Enemy : MonoBehaviour
    {
        protected SpriteRenderer sp;
        private CharacterStatus status;
        private FlyCharacterMotor motor;
        private RoomMap map;
        private List<Common.Helper.RoomSystem.Grid> followRoad;
        protected void Start()
        {
            sp = GetComponent<SpriteRenderer>();
            status = GetComponent<CharacterStatus>();
            motor = GetComponent<FlyCharacterMotor>();
            motor.InitMotor(status);
            map = RoomManager.Instance.GetRoom();


        }
        private void FollowPlayer()
        {
            Vector2 aimPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            followRoad = SearchRoadHelper.AStar(transform.position, aimPos);
            if (followRoad != null && followRoad.Count > 0){
                motor.DirectMove(new Vector2(followRoad[0].x+0.5f - transform.position.x, followRoad[0].y+0.5f - transform.position.y));
            }
        }
        private IEnumerator MoveToPos(Vector2 pos, Func<int> complete)
        {
            while ((Vector2)transform.position != pos)
            {
                motor.DirectMove(pos - (Vector2)transform.position);
                yield return 0;
            }
            complete.Invoke();
        }
        int i = 0;
        private void Update()
        {
            FollowPlayer();
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
        //private void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireCube(RoomManager.Instance.tile.transform.position, new Vector3(RoomManager.ROOM_MAXWIDTH, RoomManager.ROOM_MAXHEIGHT, 1));
        //    if (followRoad != null)
        //    {
        //        foreach (var grid in followRoad)
        //        {
        //            Gizmos.color = Color.yellow;
        //            Gizmos.DrawCube(new Vector2(grid.x, grid.y), Vector3.one);
        //        }
        //    }
        //}
    }
}
