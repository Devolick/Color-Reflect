﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MenuGameEasyButton : ClickButton
{
    UIMenuGameControl menuController;
    Image image;


    protected override void _Awake()
    {
        base._Awake();
        image = this.transform.GetComponent<Image>();
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "New")
        {
            menuController = sender as UIMenuGameControl;
            menuController.Reconnect(this);
        }
    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (UIMenuControl.State == MenuControlState.Lock) { return; }
        if (e.Gesture != InputGestures.Tap || e.TouchID != 0) { return; }

        base.Touch(sender, e);
        image.sprite = pressed;
    }
    protected override void Click()
    {
        base.Click();
        image.sprite = released;        
        menuController.LoadScene("game","Easy");
       // UIMenuControl.Instance.UnloadContent();
    }










}
