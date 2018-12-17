using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BallsWindow : Base
{
    UIGameControl controller;
    Text text;

    public string SetText {
        set { text.text = value; }
    }

    protected override void _Awake()
    {
        base._Awake();
        text = FindChild("TextBallsCount").GetComponent<Text>();
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





}
