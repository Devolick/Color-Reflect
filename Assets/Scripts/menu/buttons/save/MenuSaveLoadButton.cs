using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MenuSaveLoadButton : ClickButton
{
    UIMenuSaveControl menuController;
    TableSave table;
    Image image;
    public Sprite locked;
    bool buttonLocked = false;

    protected override void _Awake()
    {
        base._Awake();
        image = this.transform.GetComponent<Image>();
    }
    protected override void _Start()
    {
        base._Start();
        table = menuController.GetElement<TableSave>("Table");
        if (table.RowSelect == null) {
            image.sprite = locked;
            buttonLocked = true;
        }
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "Save")
        {
            menuController = sender as UIMenuSaveControl;
            menuController.Reconnect(this);
        }
    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (UIMenuControl.State == MenuControlState.Lock) { return; }
        if (e.Gesture != InputGestures.Tap || e.TouchID != 0) { return; }
        if (buttonLocked) { return; }

        base.Touch(sender, e);
        image.sprite = pressed;
    }
    protected override void Click()
    {
        base.Click();
        image.sprite = released;
        RowSave row = table.RowSelect;
        menuController.LoadScene("game", row.LevelDifficulty);
    }
    protected override void _Update()
    {
        base._Update();
        if (table.RowSelect != null) {
            if (buttonLocked) {
                image.sprite = released;
            }
            buttonLocked = false;
        }
    }









}
