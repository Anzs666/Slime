using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Skill;
using UGUI.Package;

namespace ARPG.Character
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [RequireComponent(typeof(PlayerMotor), typeof(PlayerStatus))]//依赖组件
    public class CharacterInputController_2D : MonoBehaviour
    {

        private Animator anim;
        private PlayerStatus playerStatus;
        private PlayerMotor chMotor;
        private CharacterSkillSystem skillSystem;
        private BoxCollider2D colllider;

        private void Start()
        {
            anim = GetComponent<Animator>();
            playerStatus = GetComponent<PlayerStatus>();
            chMotor = GetComponent<PlayerMotor>();
            chMotor.InitMotor(playerStatus);
            skillSystem = GetComponent<CharacterSkillSystem>();
            colllider = GetComponent<BoxCollider2D>();
        }

        //public void OnTriggerStay2D(Collider2D other)
        //{
        //    //判断碰撞物体的种类
        //    if (other.tag == "Item")
        //    {
        //        Item aimItem = other.GetComponent<ItemGameEntity>().item;
        //        switch (aimItem.itemType)
        //        {
        //            case ItemType.Equipment:
        //                break;
        //            case ItemType.SkillItem://添加到快捷栏中
        //                Debug.Log("按E捡起");
        //                if (Input.GetKeyDown(KeyCode.E))
        //                {
        //                   // UIPCBattleWindow uIPCBattleWindow = (UIPCBattleWindow)UIManager.Instance.GetWindow(UIWindowType.PCBattle);
        //                   // uIPCBattleWindow.ShortCutAddItem(aimItem);
        //                   // Destroy(other.gameObject);
        //                }
        //                break;
        //            case ItemType.Disposable:
        //            default:
        //                break;
        //        }

        //    }
        //}

#if UNITY_EDITOR || UNITY_STANDALONE
        //在编译器下||pc下   
        private void Update()
        {
            Jump();
            ShapeShifting();
        }
        private void FixedUpdate()
        {
            Walk();
            JumpAnimation();
            //SkillUse();
        }
        /// <summary>
        /// 使用技能
        /// </summary>
        private void SkillUse()
        {
            int skillid = 0;
            if (Input.GetMouseButtonDown(0))
            {
                skillid = 10001;
            }
            else
            {
                return;
            }
            skillSystem.AttackUseSkill(skillid);
        }
        /// <summary>
        /// 跳跃
        /// </summary>
        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.W)) { chMotor.Jump(); chMotor.Jumping = true; }
            else chMotor.Jumping = false;
        }
        /// <summary>
        /// 跳跃动画设置
        /// </summary>
        private void JumpAnimation()
        {
            anim.SetFloat("Velocity_y", chMotor.GetVelocity().y);
            if (chMotor.IsGround)
                anim.SetBool(playerStatus.chParameter.Jump, false);
            else
                anim.SetBool(playerStatus.chParameter.Jump, true);
        }
        /// <summary>
        /// 改变外形
        /// </summary>
        private void ShapeShifting()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerStatus.ChangeShape();
                anim.SetBool("Slimer", playerStatus.IsChangingShape);
                //进入回调
                StartCoroutine(AnimationComplete(1.5f, ChangeColliderControl));
            }

        }
        private void ChangeColliderControl()
        {
            if (playerStatus.IsChangingShape)
                ChangeCollider(playerStatus.BCOSs.offests[1], playerStatus.BCOSs.sizes[1]);
            else
                ChangeCollider(playerStatus.BCOSs.offests[0], playerStatus.BCOSs.sizes[0]);
        }
        /// <summary>
        /// 改变碰撞体
        /// </summary>
        /// <param name="_offset"></param>
        /// <param name="_size"></param>
        private void ChangeCollider(Vector2 _offset, Vector2 _size)
        {
            colllider.offset = _offset;
            colllider.size = _size;
        }
        /// <summary>
        /// 移动
        /// </summary>
        private void Walk()
        {
            float dx = 0;
            dx = Input.GetAxisRaw("Horizontal");
            chMotor.HorizontalMove(dx);
            if (dx != 0)
            {
                anim.SetBool(playerStatus.chParameter.Walk, true);//Mathf.Abs(chMotor.GetVelocity().x)); 
                anim.SetFloat("Velocity_x", 1f);
            }
            else
            {
                anim.SetBool(playerStatus.chParameter.Walk, false);
                anim.SetFloat("Velocity_x", 0f);
            }
        }
        public delegate void DelAnimationComplete();
        /// <summary>
        /// 动画结束回调控制函数
        /// </summary>
        /// <param name="animTime"></param>
        /// <param name="animationCompleteHandle"></param>
        /// <returns></returns>
        public IEnumerator AnimationComplete(float animTime, DelAnimationComplete animationCompleteHandle)
        {
            yield return new WaitForSeconds(animTime);
            animationCompleteHandle.Invoke();
        }

#elif UNITY_IPHONE
            //在Iphone下
 
#elif UNITY_ANDROID
            //在Android下
        //获取操纵杆脚本
        private UISimpleTouchControllerWindow sTWindow;
        private UIBattleWindow uIMainWindow;

        private void AddControllEvent()
        {
            sTWindow.ValueChangeEvent+= OnSimpleTouchMove;
            uIMainWindow.onSkillButtonDown += OnSkillButtonDown;
        }

        private void RemoveControlEvent()
        {
            sTWindow.ValueChangeEvent -= OnSimpleTouchMove;
            uIMainWindow.onSkillButtonDown -= OnSkillButtonDown;
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void OnEnable()
        {
            //AddListener
            AddControllEvent();
        }

        /// <summary>
        /// 销毁事件
        /// </summary>
        private void OnDisable()
        {
            //RemoveListener
            RemoveControlEvent();
        }

        #region 技能
        private void OnSkillButtonDown(string buttonName)
        {
            int skillid;
            switch (buttonName)
            {
                case "AttackButton":
                    skillid = 10001;
                    break;
                case "SkillButton1":
                    skillid = 10002;
                    break;
                case "SkillButton2":
                    skillid = 10003;
                    break;
                case "SkillButton3":
                    skillid = 10004;
                    break;
                default:
                    return;
            }
            skillSystem.AttackUseSkill(skillid);
        }

        #endregion

        #region SimpleTouch移动控制
        /// <summary>
        /// 移动开始
        /// </summary>
        private void OnSimpleTouchMoveStart()
        {
            //控制动画开始
            anim.SetBool(playerStatus.chParameter.Walk, true);
        }

        /// <summary>
        /// 移动结束
        /// </summary>
        private void OnSimpleTouchMoveEnd()
        {
            //控制动画结束
            anim.SetBool(playerStatus.chParameter.Walk, false);
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void OnSimpleTouchMove(Vector2 direction)
        {
            //调用马达功能
            chMotor.Movement(new Vector3(direction.x, 0, direction.y));
        }
        #endregion
#endif
    }
}
