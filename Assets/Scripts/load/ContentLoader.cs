using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ContentLoader : ContenWindow
{
    Transform angleUpEffectMask;
    Transform angleDownEffectMask;
    Delay delayMethod;
    string sceneLoad = "";
    bool ready = true;
    bool loadEffect = false;
    bool firstOpen = true;
    [SerializeField]
    GameObject backgroundTile;

    public bool Ready {
        get { return ready; }
    }


    protected override void _Awake()
    {
        base._Awake();
        delayMethod = new Delay();
        angleUpEffectMask = FindChildIn("Mask", angleUp.gameObject).transform;
        angleDownEffectMask = FindChildIn("Mask", angleDown.gameObject).transform;
    }
    protected override void _Start()
    {
        base._Start();
        LoadContent("menu");
    }
    public void LoadContent(string scene)
    {
        sceneLoad = scene;
        ready = false;
        backgroundTile.SetActive(true);
        if (!firstOpen)
        {
            Open(true);
        }
        else {
            LoadEffect();
            firstOpen = false;
        }
    }
    protected override void OpenOver()
    {
        base.OpenOver();
        if (open)
        {
            LoadEffect();
        }
    }
    void LoadEffect() {
        loadEffect = true;
        delayMethod.RegisterOnce(LoadDo, 3f, true);
    }
    void LoadEffectAnim() {
        if (loadEffect)
        {
            if (!delayMethod.Run)
            {
                loadEffect = false;
                return;
            }
            float width = (angleDownEffectMask.parent as RectTransform).sizeDelta.x;
            (angleUpEffectMask as RectTransform).sizeDelta = new Vector2((width * delayMethod.Percent), 20);
            (angleDownEffectMask as RectTransform).sizeDelta = new Vector2((width * delayMethod.Percent), 20);
        }
    }
    void LoadDo() {
        SceneManager.LoadScene(sceneLoad);
        ready = true;
        (angleUpEffectMask as RectTransform).sizeDelta = new Vector2(0, 20);
        (angleDownEffectMask as RectTransform).sizeDelta = new Vector2(0, 20);
    }
    public void ContentReady() {
        Open(false);
        ready = false;
        if (backgroundTile.activeInHierarchy) {
            backgroundTile.SetActive(false);
        }
    }
    protected override void _Update()
    {
        base._Update();
        delayMethod.PlayDelay(Time.deltaTime);

        LoadEffectAnim();
    }









}
