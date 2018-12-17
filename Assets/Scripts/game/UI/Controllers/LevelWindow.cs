using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class LevelWindow : Base
{
    UIGameControl controller;
    Text text;

    public string LevelText {
        get { return text.text; }
        set { text.text = value; }
    }

    protected override void _Awake()
    {
        base._Awake();
        text = FindChild("Text").GetComponent<Text>();
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
    void CheckSaveData() {
        SaveData save = TableArea.Instance.Save;
        if (save != null) {
            text.text = save.Level.ToString(); ;
        }
    }
    protected override void _Update()
    {
        base._Update();
        CheckSaveData();
    }






}
