using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MenuControlState { None,Lock,Unlock}

public class UIMenuControl : BaseControl
{
    static UIMenuControl instance;
    static UIMenuWindow openedControl;
    MenuControlState state = MenuControlState.Unlock;

    public static UIMenuControl Instance {
        get { return instance; }
    }
    public static MenuControlState State {
        get { return instance.state; }
        set { instance.state = value; }
    }

    protected override void _Awake()
    {
        base._Awake();
        instance = this;
        openedControl = null;
        SendAsk<UIMenuWindow>(this);
    }
    protected override void _Start()
    {
        base._Start();
        Canvas canvas = this.transform.GetComponent<Canvas>();
        //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameObject.Find("Camera").GetComponent<Camera>();
        GameInstance.Loader.ContentReady();
        openedControl = GetElement<UIMenuWindow>("Main");
        openedControl.Open(true);
    }
    public void OpenWindow<T>() where T : UIMenuWindow {
        T[] arr = Controlled as T[];
        if (openedControl == null)
        {
            openedControl = GetElement<T>(Controlled);
            openedControl.Open(true);
        }
        else {
            openedControl.Open(false);
            openedControl = GetElement<T>(Controlled);
            openedControl.Open(true);
        }
    }
    public void UnloadContent() {
        Destroy(this.gameObject);
    }








}
