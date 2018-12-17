using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ContenWindow : BaseControl
{


    Delay delayMethod;
    [SerializeField]
    bool playMove = false;
    [SerializeField]
    protected bool open = false;
    Vector3 startMove;
    Vector3 closeMoveTo;
    [SerializeField]
    Vector3 mainPosition;

    [SerializeField]
    Image imageBackground;
    [SerializeField]
    bool windowOpen = false;
    [SerializeField]
    float openPercent = 0f;
    bool openAnimWindow = false;
    [SerializeField]
    Collider2D colliderBackground;
    [SerializeField]
    protected GameObject angleUp;
    [SerializeField]
    protected GameObject angleDown;


    protected override void _Awake()
    {
        base._Awake();
        delayMethod = new Delay();
    }
    Vector3 VectorByAngleDistance(float angle, float distance)
    {
        return Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * distance;
    }
    protected void Open(bool open)
    {
        if (this.open == open) { return; }

        playMove = true;
        this.open = open;
        openAnimWindow = true;
        windowOpen = open;
        delayMethod.RegisterOnce(OpenOver, 1.2f, true);
        if (open)
        {
            (this.transform as RectTransform).anchoredPosition = VectorByAngleDistance(315f, 1500f);
            angleUp.gameObject.SetActive(open);
            angleDown.gameObject.SetActive(open);
        }
        else
        {
            closeMoveTo = VectorByAngleDistance(135f, 1500f);
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
    void Background()
    {
        if (openAnimWindow)
        {
            if (windowOpen && openPercent < 1)
            {
                openPercent += 0.75f * Time.deltaTime;
                if (openPercent > 1)
                {
                    openPercent = 1;
                    openAnimWindow = false;
                }
            }
            else
            {
                openPercent -= 0.75f * Time.deltaTime;
                if (openPercent < 0)
                {
                    openPercent = 0;
                    openAnimWindow = false;
                    colliderBackground.enabled = false;
                    angleUp.gameObject.SetActive(false);
                    angleDown.gameObject.SetActive(false);
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
        delayMethod.PlayDelay(Time.deltaTime);
        PlayMove();
        Background();
    }


















}
