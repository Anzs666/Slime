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
        public GameObject roadSign;
        private CharacterStatus status;
        private FlyCharacterMotor motor;
        private RoomMap map;
        private List<Vector2> followRoad;
        protected void Start()
        {
            sp = GetComponent<SpriteRenderer>();
            status = GetComponent<CharacterStatus>();
            motor = GetComponent<FlyCharacterMotor>();
            motor.InitMotor(status);
            map = RoomCellSMGenerator.Instance.GetRoom();
            SetFollowRoad();

        }
        private void SetFollowRoad()
        {
            Vector2 aimPos = GameObject.FindGameObjectWithTag("Player").transform.position;
            followRoad = SearchRoadHelper.AStar(map, transform.position, aimPos);
            foreach (var i in followRoad)
            {
                GameObject s = Instantiate(roadSign);
                s.transform.position = i + new Vector2(0.5f, 0.5f);
            }
        }
        private IEnumerator MoveToPos(Vector2 pos,Func<int> complete)
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
            StartCoroutine(MoveToPos(followRoad[i],()=> { return i++; }));
            Debug.Log(i);
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
