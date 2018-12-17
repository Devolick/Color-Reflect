using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIPauseRepeatButton : ClickButton
{
    UIGamePause menuController;
    Image image;


    protected override void _Awake()
    {
        base._Awake();
        image = this.transform.GetComponent<Image>();
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "Pause")
        {
            menuController = sender as UIGamePause;
            menuController.Reconnect(this);
        }
    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (menuController.State == GamePauseState.Lock) { return; }
        if (e.Gesture != InputGestures.Tap || e.TouchID != 0) { return; }
        if (buttonEnable) { return; }

        base.Touch(sender, e);
        image.sprite = pressed;
    }
    protected override void Click()
    {
        base.Click();
        image.sprite = released;
        menuController.CallWindow<UIGameLevelStarted>("LevelStarted");
    }










}
