using UnityEngine;
using System.Collections;
using System;

public abstract class ClickButton : Button {

    public Sprite pressed;
    public Sprite released;
    AudioSource audio;


    protected override void _Awake()
    {
        base._Awake();
        audio = this.transform.GetComponent<AudioSource>();
    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (buttonEnable) return;
            base.Touch(sender, e);
        buttonEnable = true;
        if (GameInstance.GameEffects) { audio.Play(); }
    }




}
