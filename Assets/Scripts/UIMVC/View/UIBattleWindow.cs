using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UGUI.Framework;
using System;
using Common;
using UnityEngine.UI;
using ARPG.Skill;
using ARPG.Character;

public class UIBattleWindow : UIWindow
{
    public delegate void DelSkillButtonDown(string buttonName);
    public event DelSkillButtonDown onSkillButtonDown;

    private void Start()
    {
        //UIManager.Instance.GetWindow<UISimpleTouchControllerWindow>().SetVisible(false);
     
        //GetUIEventListener("AttackButton").onDown += OnAttackButtonDown;
        //GetUIEventListener("AttackButton").onUp += OnAttackButtonUp;
    }

    protected override void OnAddListener()
    {

    }

    protected override void OnRemoveListener()
    {

    }

    #region AttackButton
    private void OnAttackButtonDown(GameObject AttackButton)
    {
        string buttonName = AttackButton.name;
        onSkillButtonDown?.Invoke(buttonName);
       // AttackButton.GetComponent<Image>().sprite = ResourcesManager.Load<Sprite>();
    }

    private void OnAttackButtonUp(GameObject AttackButton)
    {
        // AttackButton.GetComponent<Image>().sprite = ResourcesManager.Load<Sprite>();
    }
    #endregion

}
