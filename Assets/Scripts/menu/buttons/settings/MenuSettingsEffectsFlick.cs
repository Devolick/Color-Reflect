using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MenuSettingsEffectsFlick : FlickButton
{
    UIMenuSettingsControl menuController;
    AudioSource audio;


    protected override void _Awake()
    {
        base._Awake();
        audio = this.transform.GetComponent<AudioSource>();
    }
    protected override void _Start()
    {
        base._Start();
        DefaultEnable("MenuSettingsEffectsFlick");
        GameInstance.GameEffects = !flickEnable;
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "Settings")
        {
            menuController = this.transform.GetComponent<UIMenuSettingsControl>();
        }
    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (UIMenuControl.State == MenuControlState.Lock) { return; }
        if (e.Gesture != InputGestures.Tap || e.TouchID != 0) { return; }
        if (playFlick) { return; }

        base.Touch(sender, e);
        audio.Play();
    }
    protected override void Click()
    {
        base.Click();
        GameInstance.GameEffects = !flickEnable;
    }
    void OnDisable()
    {
        PlayerPrefs.SetInt("MenuSettingsEffectsFlick", flickEnable ? 1 : 0);
    }





}
