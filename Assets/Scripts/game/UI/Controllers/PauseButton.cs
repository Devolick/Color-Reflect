using UnityEngine;
using System.Collections;
using System;

public class PauseButton : Button
{
    UIGameControl controller;
    bool pauseEnable = false;
    Transform buttonCenter;
    AudioSource audio;


    public bool Pause {
        set { pauseEnable = value; }
    }

    protected override void _Awake()
    {
        base._Awake();
        buttonCenter = FindChild("Button").transform;
        audio = this.transform.GetComponent<AudioSource>();
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "GameUI")
        {
            controller = sender as UIGameControl;
            controller.Reconnect(this);
        }
    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (GameState.StateMain != GameMainState.Game) { return; }
        if (e.Gesture != InputGestures.Double || e.TouchID != 0) { return; }
        if (pauseEnable) { return; }

        base.Touch(sender, e);
        pauseEnable = true;
        ClickEffect(pauseEnable);
        if (GameInstance.GameEffects) { audio.Play(); }
    }
    protected override void Click()
    {
        base.Click();
        controller.GetElement<UIGamePause>("Pause").Open(true);
    }
    void ClickEffect(bool pause) {
        Vector3 scl = buttonCenter.localScale;
        scl.y *= -1;
        buttonCenter.localScale = scl;
    }











}
