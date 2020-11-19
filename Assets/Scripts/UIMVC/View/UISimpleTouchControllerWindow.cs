using System.Collections;
using System.Collections.Generic;
using Common;
using UGUI.Framework;
using UnityEngine;

public class UISimpleTouchControllerWindow : UIWindow
{

    /// <summary>
    /// 摇杆背景
    /// </summary>
    private Transform _joyBg;
    /// <summary>
    /// 摇杆中心
    /// </summary>
    private Transform _joyCenter;
    /// <summary>
    /// 摇杆半径
    /// </summary>
    private float _radius = 50;
    /// <summary>
    /// 移动中心
    /// </summary>
    private Vector2 _moveCenter;
    /// <summary>
    /// 鼠标到移动中心的向量
    /// </summary>
    private Vector2 _mouseToCenterVect;
    /// <summary>
    /// 鼠标到中心点的距离
    /// </summary>
    private float _mouseToCenterDistance;
    /// <summary>
    ///移动二维向量
    /// </summary>
    private Vector2 movementVector;
    /// <summary>
    /// 旋转角度
    /// </summary>
    private float _rotAngle;
    /// <summary>
    /// 
    /// </summary>
    private UIEventListener uIEventListener;
    [SerializeField]
    private bool touchPresent = false;
 
    //拖拽事件
    //public delegate void OnDragDelegate(Vector2 value);
    //public event OnDragDelegate DragEvent;
    public delegate void OnValueChangeDelegate(Vector2 value);
    public event OnValueChangeDelegate ValueChangeEvent;
    public delegate void TouchStateDelegate(bool touchPresent);
    public event TouchStateDelegate TouchStateEvent;

    private void Start()
    {      
        //摇杆初始化
        _joyBg = transform.FindChildByName("LeftTouch Joystick");
        _joyCenter = transform.FindChildByName("Content");
        uIEventListener = GetComponent<UIEventListener>();
        //注册事件
        uIEventListener.onBeginDrag += BeginDrag;
        uIEventListener.onDrag += Drag;
        uIEventListener.onEndDrag += EndDrag;
    }

    private void Update()
    {
        //触发事件 
        if (movementVector != Vector2.zero)
        {
            ValueChangeEvent?.Invoke(movementVector);
        }
    }

    private void BeginDrag(GameObject gameObject)
    {
        //中心位置记录
        _moveCenter = Input.mousePosition;
        _joyCenter.position = _joyBg.position = _moveCenter;
        //显示
        //UIManager.Instance.GetWindow<UISimpleTouchControllerWindow>().SetVisible(true);
        //事件
        touchPresent = true;
        TouchStateEvent?.Invoke(touchPresent);
    }

    private void Drag(GameObject gameObject)
    {
        //获得鼠标位置到中心点的距离
        _mouseToCenterVect = (Vector2)Input.mousePosition - _moveCenter;
        //magnitude向量长度
        _mouseToCenterDistance = Mathf.Clamp(_mouseToCenterVect.magnitude, 0, _radius);
        //控制水平位置在1 ~-1之间
        movementVector.x = (_joyCenter.position.x - _moveCenter.x) / 100;
        //控制垂直位置在1 ~-1之间
        movementVector.y = (_joyCenter.position.y - _moveCenter.y) / 100;
        //设置摇杆位置normalized标准化
        _joyCenter.position = _moveCenter + _mouseToCenterVect.normalized * _mouseToCenterDistance;  
        //角度
        //_rotAngle = Vector3.Angle(_mouseToCenterVect, Vector3.up);
        //if (movementVector.x < 0)
        //{
        //    _rotAngle = 360 - _rotAngle;
        //}    
    }

    private void EndDrag(GameObject gameObject)
    {
        //归零     
        movementVector = Vector2.zero;
        //消失
        //UIManager.Instance.GetWindow<UISimpleTouchControllerWindow>().SetVisible(false);
        //事件
        touchPresent = false;
        TouchStateEvent?.Invoke(touchPresent);
    }
}
