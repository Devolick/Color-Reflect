using UnityEngine;
using System.Collections;
using System;

public class MagniteWellBall : MagniteBall
{
    MagniteSupply supply;
    SpriteRenderer electricFieldSprite;
    BallColor colorElectricField = BallColor.None;
    BallState startBallType = BallState.None;
    Vector3 distanceCollide;
    [SerializeField]
    AudioClip clipBouncing;

    public BallColor ColorElectricField {
        set { colorElectricField = value; }
    }
    public BallState StartBallState {
        get { return startBallType; }
    }
    public MagniteSupply Supply {
        get { return supply; }
        set { supply = value; }
    }
    public Vector3 DistanceCollide {
        get { return distanceCollide; }
    }

    protected override void _Awake()
    {
        base._Awake();
        electricFieldSprite = FindChild("ElectricField").GetComponent<SpriteRenderer>();
    }
    protected override void _Start()
    {
        base._Start();
        startBallType = State;
        electricFieldSprite.enabled = TriggerCollision;
        BallHasElectric();
    }
    void BallHasElectric() {
        if (electricFieldSprite.enabled) {
            electricFieldSprite.sprite = ResourceConnect.GetSprite("BallElectric"+colorElectricField);
        }
    }
    public static MagniteWellBall CreateMe(Transform ball, BallType typeBall, BallColor color)
    {
        MagniteWellBall t = null;
        Transform ot = Instantiate(ball) as Transform;
        t = ot.GetComponent<MagniteWellBall>();
        t.BallTypeState = typeBall;
        t.ColorState = color;
        //int range = UnityEngine.Random.Range(0, ((50 - TableArea.Instance.Save.Level) + 5));
        //if (range <= 2)
        //{
            t.State = BallState.ElectricField;
            t.TriggerCollision = true;
            t.ColorElectricField = (BallColor)UnityEngine.Random.Range(1, 4);
            
        //}
        //else
        //{
        //    t.State = BallState.Demagnetized;
        //    t.TriggerCollision = false;
        //}

        return t;
    }
    protected override void CollideDetect(Collider2D other)
    {
        MagniteFireBall fireBall = other.transform.GetComponent<MagniteFireBall>();
        if (fireBall != null)
        {
            this.Sprite.sortingOrder = 10;
            CheckWellState(fireBall);
        }
    }
    void OnTriggerEnter2D(Collider2D collider) {
        MagniteBall fireBall = collider.transform.GetComponent<MagniteFireBall>();
        if (fireBall != null)
        {
            CollideDetect(collider);
            this.Sprite.sortingOrder = 10;
        }
    }
    public void DetectWrongBall(MagniteBall ball)
    {
        Vector3 dir = ball.transform.position - this.transform.position;
        dir.Normalize();
        float rad = Mathf.Atan2(dir.y, dir.x);
        float angle = ((180 / Mathf.PI) * rad) - 90 + UnityEngine.Random.Range(-15, 16);
        Vector3 newVecDirection = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
        ball.Rigid.AddForce(-ball.Rigid.velocity * ball.Rigid.mass, ForceMode2D.Impulse);
        ball.Rigid.AddForce((new Vector2(newVecDirection.x, newVecDirection.y) * 5), ForceMode2D.Impulse);
    }
    void CheckWellState(MagniteFireBall fireBall)
    {
        if (State == BallState.Demagnetized)
        {
            Supply.UnlinkBall();
            if (fireBall.ColorState == colorState)
            {
                State = BallState.Magnetized;
                distanceCollide = this.transform.position;
            }
            if (GameInstance.GameEffects)
            {
                audio.clip = clipCollision;
                audio.Play();
            }
        }
        else if (State == BallState.ElectricField)
        {
            if (fireBall.ColorState == colorElectricField)
            {
                State = BallState.Demagnetized;
                fireBall.TriggerCollision = true;
                TriggerCollision = false;
                electricFieldSprite.enabled = false;
            }
            fireBall.DestroyIt();
            if (GameInstance.GameEffects)
            {
                audio.clip = clipBouncing;
                audio.Play();
            }
        }
    }
    public override bool IsVisibleToCamera(Transform transform)
    {
        bool vis = base.IsVisibleToCamera(transform);
        if (!vis)
        { DestroyOutOfTime(); }
        return vis;
    }
    public override void DestroyDelayStop() {
        delayMethod.Stop();
    }
    public override void DestroyAfter(float time) {
        delayMethod.Stop();
        delayMethod.RegisterOnce(DestroyOutOfTime, time, true);
    }
    public override void DestroyOutOfTime() {
        TableArea.Instance.CountOutBall();
        DestroyIt();
    }
    protected override void FillDestroyPointAngle()
    {
        base.FillDestroyPointAngle();
        if (delayMethod.Run)
        {
            DestroyPoint.gameObject.SetActive(true);
            DestroyPoint.SetText = ((int)delayMethod.LeftTime).ToString();
        }
        else {
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
