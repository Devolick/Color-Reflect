using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGameGameWon : UIGameWindow
{
    Delay delayMethod;

    protected override void _Awake()
    {
        base._Awake();
        SendAsk<Base>(this);
        delayMethod = new Delay();
    }
    protected override void OpenOver()
    {
        base.OpenOver();
        if (open)
        {
            delayMethod.RegisterOnce(OpenNextLevel, 3f, true);
        }
        else {
            TableArea.Instance.UnloadContent();
            UIGameControl.Instance.UnloadContent();
        }
    }
    void OpenNextLevel() {
        Open(false);
        GameInstance.Loader.LoadContent("menu");
    }
    protected override void _Update()
    {
        base._Update();
        delayMethod.PlayDelay(Time.deltaTime);
    }




}
