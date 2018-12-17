using UnityEngine;
using System.Collections;
using System;

public class FireColorButton : Button
{
    SpriteRenderer spriteMain;
    MagnitePipe pipeGun;
    [SerializeField]
    BallColor colorButton;
    public Sprite pressed;
    public Sprite released;


    protected override void _Awake()
    {
        base._Awake();
        spriteMain = this.transform.GetComponent<SpriteRenderer>();
    }
    protected override void Click()
    {
        base.Click();
        spriteMain.sprite = released;
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "PipeGun")
        {
            pipeGun = sender as MagnitePipe;
        }
    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (GameState.StateMain != GameMainState.Game) { return; }

        if (!pipeGun.CanFire) return;

        buttonEnable = false;
        delayMethod.Stop();
        spriteMain.sprite = pressed;
        base.Touch(sender, e);
        if (pipeGun != null)
        {
            pipeGun.Fire(colorButton);
        }
    }
    protected override void _Update()
    {
        if (GameState.StateMain != GameMainState.Game) { return; }

        base._Update();
    }








}
