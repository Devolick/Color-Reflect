using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using System;

public class UIGameLevelFailed : UIGameWindow,IHasAd
{
    Delay delayMethod;

    protected override void _Awake()
    {
        base._Awake();
        SendAsk<Base>(this);
        delayMethod = new Delay();
    }
    public override void Open(bool open)
    {
        base.Open(open);
        if (open) {
            GameState.StateMain = GameMainState.Pause;
        }
    }
    void OpenPause()
    {
        if (!AdManager.ShowAd(CallBackAd))
        {
            CallWindow<UIGamePause>("Pause");
        }
    }
    public void CallBackAd(ShowResult result)
    {
        CallWindow<UIGamePause>("Pause");
    }
    protected override void OpenOver()
    {
        base.OpenOver();
        if(open)
            delayMethod.RegisterOnce(OpenPause, 3f, true);
    }
    protected override void _Update()
    {
        base._Update();
        delayMethod.PlayDelay(Time.deltaTime);
    }


}
