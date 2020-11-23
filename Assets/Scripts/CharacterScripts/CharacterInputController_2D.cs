using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARPG.Skill;
using UGUI.Framework;
using UGUI.Package;

namespace ARPG.Character
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [RequireComponent(typeof(CharacterMotor), typeof(CharaterStatus))]//依赖组件
    public class CharacterInputController_2D : MonoBehaviour
    {

        private Animator anim;
        private CharaterStatus playerStatus;
        private PlayerMotor chMotor;
        private CharacterSkillSystem skillSystem;

        private void Start()
        {
            anim = GetComponent<Animator>();
            playerStatus = GetComponent<CharaterStatus>();
            chMotor = GetComponent<PlayerMotor>();
            chMotor.InitMotor(playerStatus);
            skillSystem = GetComponent<CharacterSkillSystem>();
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            //判断碰撞物体的种类
            if (other.tag == "Item")
            {
                Item aimItem = other.GetComponent<ItemGameEntity>().item;
                switch (aimItem.itemType)
                {
                    case ItemType.Equipment:
                        break;
                    case ItemType.SkillItem://添加到快捷栏中
                        Debug.Log("按E捡起");
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            UIPCBattleWindow uIPCBattleWindow = (UIPCBattleWindow)UIManager.Instance.GetWindow(UIWindowType.PCBattle);
                            uIPCBattleWindow.ShortCutAddItem(aimItem);
                            Destroy(other.gameObject);
                        }
                        break;
                    case ItemType.Disposable:
                    default:
                        break;
                }

            }
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        //在编译器下||pc下   
        private void FixedUpdate()
        {
            Walk();
            Jump();
            JumpAnimation();
            //SkillUse();
        }
        //使用技能
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
        //跳跃
        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.W)) chMotor.Jumping = true;
            else chMotor.Jumping = false;
            if (Input.GetKey(KeyCode.W))
            {
                chMotor.Jump();
            }
        }
        //跳跃动画设置
        private void JumpAnimation()
        {
            anim.SetFloat("Velocity_y", chMotor.GetVelocity().y);
            if (chMotor.IsGround)
                anim.SetBool(playerStatus.chParameter.Jump, false);
            else
                anim.SetBool(playerStatus.chParameter.Jump, true);
        }

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
