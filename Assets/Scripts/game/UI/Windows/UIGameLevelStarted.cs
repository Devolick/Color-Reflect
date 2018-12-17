using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGameLevelStarted : UIGameWindow
{
    Delay delayMethod;

    protected override void _Awake()
    {
        base._Awake();
        SendAsk<Base>(this);
        delayMethod = new Delay();
    }
    void CloseWindow() {
        Open(false);
    }
    public override void Open(bool open)
    {
        base.Open(open);
        if (open) {
            GameState.StateMain = GameMainState.Pause;
        }
    }
    protected override void OpenOver()
    {
        base.OpenOver();
        if (open)
        {
            delayMethod.RegisterOnce(CloseWindow, 2f, true);
            TableArea.Instance.ClearStart();
            GameState.StatePlay = GamePlayState.Clear;
        }
        else {
            GameState.StateMain = GameMainState.Game;
            GameState.StatePlay = GamePlayState.Play;
            TableArea.Instance.GetElement<MagniteSupplyArea>("MagniteSupplyArea").SendBall(this);
        }
    }
    protected override void _Update()
    {
        base._Update();
        delayMethod.PlayDelay(Time.deltaTime);
    }




}
