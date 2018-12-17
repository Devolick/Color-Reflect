using UnityEngine;
using System.Collections;
using System;

public abstract class FlickButton : Button {
    [SerializeField]
    Transform flick;
    [SerializeField]
    protected bool flickEnable = true;
    [SerializeField]
    bool flickState = false;
    protected bool playFlick = false;
    [SerializeField]
    float distanceFlick = 0;

    protected void DefaultEnable(string saveName)
    {
        flickEnable = PlayerPrefs.GetInt(saveName) > 0;
        flickState = !flickEnable;
        Vector3 posAch = (flick as RectTransform).anchoredPosition;
        float newDistance = distanceFlick - (flick as RectTransform).sizeDelta.x;
        if (flickEnable)
        {
            posAch.x = newDistance;
            (flick as RectTransform).anchoredPosition = posAch;
        }
        else {
            posAch.x = 2;
            (flick as RectTransform).anchoredPosition = posAch;
        }
    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (buttonEnable) { return; }
        flickState = !flickState;
        playFlick = true;
        base.Touch(sender, e);

    }
    protected override void Click()
    {
        base.Click();
        flickEnable = !flickState;
    }
    void ShowFlick() {
        if (playFlick) {
            Vector3 posAch = (flick as RectTransform).anchoredPosition;
            float newDistance = distanceFlick - (flick as RectTransform).sizeDelta.x;
            if (flickState)
            {
                posAch.x = (newDistance - (newDistance * delayMethod.Percent)) +2;
                (flick as RectTransform).anchoredPosition = posAch;
            }
            else {
                posAch.x = (newDistance * delayMethod.Percent)+2;
                (flick as RectTransform).anchoredPosition = posAch;
            }
            if (!delayMethod.Run) {
                playFlick = false;
            }
        }
    }
    protected override void _Update()
    {
        base._Update();
        ShowFlick();
    }

}
