using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIGameControl : BaseControl
{
    static UIGameControl instance;
    Image imageBackground;
    [SerializeField]
    bool windowOpen = false;
    [SerializeField]
    float openPercent = 0f;
    bool openAnimWindow = false;
    Collider2D colliderBackground;

    public static UIGameControl Instance {
        get { return instance; }
    }
    public bool WindowOpen {
        get { return windowOpen; }
        set {                   
            windowOpen = value;
            openAnimWindow = true;
            if (value) {
                colliderBackground.enabled = true;
            }
        }
    }

    protected override void _Awake()
    {
        base._Awake();
        instance = this;
        imageBackground = FindChild("Windows").transform.FindChild("Background").GetComponent<Image>();
        colliderBackground = imageBackground.GetComponent<Collider2D>();
        SendAsk<Base>(this);
    }
    protected override void _Start()
    {
        base._Start();
        Canvas canvas = this.transform.GetComponent<Canvas>();
        //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }
    void Background() {
        if (openAnimWindow)
        {
            if (windowOpen && openPercent < 1)
            {
                openPercent += 0.75f * Time.deltaTime;
                if (openPercent > 1) {
                    openPercent = 1;
                    openAnimWindow = false;
                }
            }
            else
            {
                openPercent -= 0.75f * Time.deltaTime;
                if (openPercent < 0) {
                    openPercent = 0;
                    openAnimWindow = false;
                    colliderBackground.enabled = false;
                }
            }
            Color colorA = imageBackground.color;
            colorA.a = openPercent * 0.65f;
            imageBackground.color = colorA;
        }
    }
    protected override void _Update()
    {
        base._Update();
        Background();
    }
    public void UnloadContent()
    {
        Destroy(this.gameObject);
    }







}
