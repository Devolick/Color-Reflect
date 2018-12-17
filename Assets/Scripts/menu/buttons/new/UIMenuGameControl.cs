using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UIMenuGameControl : UIMenuWindow
{
    UIMenuControl controller;
    bool loadScene = false;

    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if ((sender as Base).name == "MenuUI")
        {
            controller = sender as UIMenuControl;
            controller.Reconnect(this);
        }
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
            GameInstance.Save.LoadFrom = "New";
        }
    }
    public void LoadScene(string sceneName,string level) {
        loadScene = true;
        Open(false);
        GameInstance.Save.Difficulty = level;
        GameInstance.Loader.LoadContent(sceneName);
    }
    protected override void OpenOver()
    {
        base.OpenOver();
        if (loadScene) {
            UIMenuControl.Instance.UnloadContent();
        }
    }






}
