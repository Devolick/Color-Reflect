using UnityEngine;
using System.Collections;

public class UIMenuWindow : BaseControl
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


    protected override void _Awake()
    {
        base._Awake();
        delayMethod = new Delay();
        mainPosition = (this.transform as RectTransform).anchoredPosition;
    }
    Vector3 VectorByAngleDistance(float angle, float distance)
    {
        return Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * distance;
    }
    public virtual void Open(bool open)
    {
        playMove = true;
        this.open = open;
        delayMethod.RegisterOnce(OpenOver, 1.2f, true);
        if (open)
        {
            (this.transform as RectTransform).anchoredPosition = VectorByAngleDistance(315f, 1500f);
        }
        else
        {
            closeMoveTo = VectorByAngleDistance(135f, 1500f);
        }
        UIMenuControl.State = MenuControlState.Lock;
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
        UIMenuControl.State = MenuControlState.Unlock;
    }
    protected override void _Update()
    {
        base._Update();
        delayMethod.PlayDelay(Time.deltaTime);
        PlayMove();
    }















}
