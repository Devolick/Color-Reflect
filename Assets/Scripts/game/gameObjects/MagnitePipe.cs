using UnityEngine;
using System.Collections;

public class MagnitePipe : Magnite {
    [SerializeField]
    Transform ballFirePrefab;
    [SerializeField]
    float fireSpeedPower = 2f;
    public bool testFire = false;
    Transform fireOut;
    [SerializeField]
    float intervalFireTime = 1f;
    [SerializeField]
    float delayInterval = 0.75f;
    bool fire = false;
    AudioSource audio;

    public float IntervalFireTime {
        get { return intervalFireTime; }
        set { intervalFireTime = value; }
    }
    public bool CanFire {
        get { return !fire; }
    }

    protected override void _Awake()
    {
        base._Awake();
        fireOut = FindChild("FireOut").transform;
        audio = this.transform.GetComponent<AudioSource>();
        intervalFireTime = delayInterval;
    }
    protected override void _Start()
    {
        base._Start();
        ballFirePrefab = ResourceConnect.GetPrefab("MagniteFireBall").transform;
        GameControl.Instance.SendControlAgain(this);
    }
    public void Fire(BallColor fireColor) {
        fire = true;
        MagniteFireBall ball = MagniteFireBall.CreateMe(ballFirePrefab, BallType.Fire, fireColor);
        ball.transform.position = fireOut.position;
        FillFireForward(ball);
        ball.FillForce(fireSpeedPower);
        if (GameInstance.GameEffects)
        {
            audio.Play();
        }
    }
    void FillFireForward(MagniteBall ball) {
        Vector3 dir = (fireOut.position + Vector3.up) - ball.transform.position;
        dir.Normalize();
        float rad = Mathf.Atan2(dir.y, dir.x);
        float angle = ((180 / Mathf.PI) * rad)-90;
        ball.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    void FireInterval() {
        if (intervalFireTime > 0 && fire)
        {
            intervalFireTime += -Time.deltaTime;
            if (intervalFireTime <= 0)
            {
                intervalFireTime = delayInterval;
                fire = false;
            }
        }
    }
    protected override void _Update()
    {
        base._Update();

        if (GameState.StateMain != GameMainState.Game) { return; }
        FireInterval();
    }






}
