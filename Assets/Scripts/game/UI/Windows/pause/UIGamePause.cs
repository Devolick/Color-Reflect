using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum GamePauseState { None,Lock,Unlock}
public class UIGamePause : UIGameWindow
{
    GamePauseState state = GamePauseState.Unlock;
    bool loadScene = false;



    public GamePauseState State {
        get { return state; }
    }

    protected override void _Awake()
    {
        base._Awake();
        SendAsk<Base>(this);
    }
    public override void Open(bool open)
    {
        base.Open(open);
        if (open)
        {
            loadScene = false;
            GameState.StateMain = GameMainState.Pause;
            UIGameControl.Instance.GetElement<PauseButton>("PauseButton").Pause = true;
        }
        state = GamePauseState.Lock;
    }
    protected override void OpenOver()
    {
        base.OpenOver();
        if (!loadScene)
        {
            if (open) { }
            else
            {   if (callWindow == null)
                {
                    GameState.StateMain = GameMainState.Game;
                }
                UIGameControl.Instance.GetElement<PauseButton>("PauseButton").Pause = false;
            }
            state = GamePauseState.Unlock;
        }
        else
        {
            TableArea.Instance.UnloadContent();
            GUIDestroyPointArea.Instance.UnloadContent();
            UIGameControl.Instance.UnloadContent();
        }
    }
    public void LoadScene(string sceneName)
    {
        loadScene = true;
        Open(false);
        GameInstance.Loader.LoadContent(sceneName);
    }










}
