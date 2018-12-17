using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpBar : Base
{
    UIGameControl controller;
    Image energyLines;

    protected override void _Awake()
    {
        base._Awake();
        energyLines = FindChild("EnergyLines").GetComponent<Image>();
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
    void ShowExp(float percent) {
        if (percent > 1) { percent = 1; }
        else if (percent < 0) { percent = 0; }
        float widthParent = (this.energyLines.transform.parent as RectTransform).sizeDelta.x;
        float width = widthParent * percent;
        Vector2 sizeDelta = (this.energyLines.transform as RectTransform).sizeDelta;
        sizeDelta.x = width;
        (this.energyLines.transform as RectTransform).sizeDelta = sizeDelta;
    }
    protected override void _Update()
    {
        base._Update();
        ShowExp(TableArea.Instance.Experience);
    }





}
