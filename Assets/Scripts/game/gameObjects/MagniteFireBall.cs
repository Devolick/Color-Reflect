using UnityEngine;
using System.Collections;
using System;

public class MagniteFireBall : MagniteBall
{


    public static MagniteFireBall CreateMe(Transform ball, BallType typeBall, BallColor color)
    {
        MagniteFireBall t = null;
        Transform ot = Instantiate(ball) as Transform;
        t = ot.GetComponent<MagniteFireBall>();
        t.BallTypeState = typeBall;
        t.ColorState = color;
        t.DestroyAfter(8f);
        return t;
    }
    protected override void CollideDetect(Collider2D other)
    {
        MagniteBall ball = other.transform.GetComponent<MagniteBall>();
        if (ball != null) {
            if (GameInstance.GameEffects)
            {
                audio.clip = clipCollision;
                audio.Play();
            }
        }
        this.Sprite.sortingOrder = 10;
        DestroyAfter(0.75f);
    }
    void OnTriggerEnter2D(Collider2D collider) {
        CollideDetect(collider);
    }
    public override bool IsVisibleToCamera(Transform transform)
    {
        bool vis = base.IsVisibleToCamera(transform);
        if (!vis)
        { DestroyIt(); }
        return vis;
    }
    protected override void FillDestroyPointAngle()
    {
        base.FillDestroyPointAngle();
        if (delayMethod.Run)
        {
            DestroyPoint.gameObject.SetActive(true);
            DestroyPoint.SetText = ((int)delayMethod.LeftTime).ToString();
        }
        else
        {
            DestroyPoint.gameObject.SetActive(false);
        }
    }
    protected override void _Update()
    {
        IsVisibleToCamera(this.transform);
        FillDestroyPointAngle();

        base._Update();

    }


}
