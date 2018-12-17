using UnityEngine;
using System.Collections;

public delegate void PauseCallWindow(bool open);
public class UIGameWindow : BaseControl
{
    Delay delayMethod;
    [SerializeField]
    bool playMove = false;
    [SerializeField]
    protected bool open = false;
    Vector3 startMove;
    Vector3 closeMoveTo;
    Vector3 mainPosition;
    [SerializeField]
    bool testMove = false;
    protected UIGameControl controller;
    protected PauseCallWindow callWindow;

    protected override void _Awake()
    {
        base._Awake();
        delayMethod = new Delay();
        mainPosition = (this.transform as RectTransform).anchoredPosition;
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
    Vector3 VectorByAngleDistance(float angle, float distance)
    {
        return Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * distance;
    }
    public virtual void Open(bool open)
    {
        if (this.open == open) { return; }

        playMove = true;
        this.open = open;
        delayMethod.RegisterOnce(OpenOver, 1.2f, true);
        if (open)
        {
            callWindow = null;
            UIGameControl.Instance.WindowOpen = true;
            (this.transform as RectTransform).anchoredPosition = VectorByAngleDistance(315f, 2100f);
        }
        else
        {
            closeMoveTo = VectorByAngleDistance(135f, 2100f);
            if (callWindow != null)
            {
                callWindow(true);
            }
            else {
                UIGameControl.Instance.WindowOpen = false;
            }
        }
        startMove = (this.transform as RectTransform).anchoredPosition;
    }
    void PlayMove()
    {
        if (playMove)
        {
            if (open)
            {
                (this.transform as RectTransform).anchoredPosition = Vector3.Lerp(startMove, Vector3.zero, delayMethod.Percent);
            }
            else
            {
                (this.transform as RectTransform).anchoredPosition = Vector3.Lerp(Vector3.zero, closeMoveTo, delayMethod.Percent);
            }

        }
    }
    protected virtual void OpenOver()
    {
        if (open)
        {
            playMove = false;
            (this.transform as RectTransform).anchoredPosition = Vector3.zero;
        }
        else
        {
            playMove = false;
            (this.transform as RectTransform).anchoredPosition = mainPosition;
        }
    }
    public bool IsVisibleToCamera(Transform transform)
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        return (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0;
    }
    protected override void _Update()
    {
        base._Update();
        delayMethod.PlayDelay(Time.deltaTime);
        PlayMove();

    }
    public void CallWindow<T>(string nameWindow) where T : UIGameWindow
    {
        callWindow = controller.GetElement<T>(nameWindow).Open;
        Open(false);
    }

















}
