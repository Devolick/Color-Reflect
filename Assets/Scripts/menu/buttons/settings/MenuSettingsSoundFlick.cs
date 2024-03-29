﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MenuSettingsSoundFlick : FlickButton
{
    UIMenuSettingsControl myControl;
    AudioSource audio;

    protected override void _Awake()
    {
        base._Awake();
        audio = this.transform.GetComponent<AudioSource>();
    }
    protected override void _Start()
    {
        base._Start();
        DefaultEnable("MenuSettingsSoundFlick");
        LevelTransferCamera.MuteGlobalSound = flickEnable;
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "Settings")
        {
            myControl = sender.GetComponent<UIMenuSettingsControl>();
        }
    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (UIMenuControl.State == MenuControlState.Lock) { return; }
        if (e.Gesture != InputGestures.Tap || e.TouchID != 0) { return; }
        if (playFlick) { return; }

        base.Touch(sender, e);
        if (GameInstance.GameEffects) { audio.Play(); }
    }
    protected override void Click()
    {
        base.Click();
        LevelTransferCamera.MuteGlobalSound = flickEnable;
    }
    void OnDisable()
    {
        PlayerPrefs.SetInt("MenuSettingsSoundFlick", flickEnable ? 1:0);
    }





}
