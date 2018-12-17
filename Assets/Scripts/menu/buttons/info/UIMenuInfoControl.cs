using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UIMenuInfoControl : UIMenuWindow
{
    UIMenuControl controller;

    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "MenuUI")
        {
            controller = sender as UIMenuControl;
            controller.Reconnect(this);
        }
    }
    protected override void _Awake()
    {
        base._Awake();
        SendAsk<Base>(this);
    }







}
