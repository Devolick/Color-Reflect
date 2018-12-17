using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Advertisements;

public class UIGameLevelPassed : UIGameWindow,IHasAd
{
    Delay delayMethod;

    protected override void _Awake()
    {
        base._Awake();
        SendAsk<Base>(this);
        delayMethod = new Delay();
    }
    void OpenNextLevel() {
        CallWindow<UIGameLevelStarted>("LevelStarted");
    }
    protected override void OpenOver()
    {
        base.OpenOver();
        if (open){
            if (!AdManager.ShowAd(CallBackAd))
            {
                GameState.StateMain = GameMainState.Pause;
                delayMethod.RegisterOnce(OpenNextLevel, 3f, true);
            }
        }
    }
    protected override void _Update()
    {
        base._Update();
        delayMethod.PlayDelay(Time.deltaTime);
    }
    public void CallBackAd(ShowResult result)
    {
        delayMethod.RegisterOnce(OpenNextLevel, 3f, true);
    }



}
