using UnityEngine;
using System.Collections;

public enum GameDirection {
None,Top,Bottom,Left,Right
}

public class MagniteSupply : Magnite {

    [SerializeField]
    int fillConnectIndex = 0;
    [SerializeField]
    MagniteSupplyArea areaSupply;
    [SerializeField]
    string connectSupplyName = "";
    [SerializeField]
    MagniteSupply connectMagnite;
    Transform centerOfTower;
    [SerializeField]
    float centerAddAngle = 0;
    SpriteRenderer centerSprite;
    Animator centerAnimator;
    [SerializeField]
    float signalTime = 2f;
    float signalElapsed = 0;
    bool signalEnable = false;
    Delay delayMethod;
    bool fillEnergyLine = false;

    bool sendBallOtherSupply = false;
    float distanceSupply = 0;
    MagniteWellBall ball;
    bool moveUpDown = false;

    [SerializeField]
    Transform ballPrefab;
    AudioSource audio;
    [SerializeField]
    AudioClip clipSignal;
    [SerializeField]
    AudioClip clipLine;


    public Transform CenterOfTower {
        get { return centerOfTower; }
    }
    public int FillConnectIndex {
        get { return fillConnectIndex; }
    }

    protected override void _Awake()
    {
        base._Awake();
        connectMagnite = this.transform.parent.FindChild(connectSupplyName).GetComponent<MagniteSupply>();
        centerOfTower = this.transform.FindChild("Center").transform;
        centerSprite = centerOfTower.GetComponent<SpriteRenderer>();
        centerAnimator = centerOfTower.GetComponent<Animator>();
        areaSupply = FindParent("MagniteSupplyArea").GetComponent<MagniteSupplyArea>();
        delayMethod = new Delay();
        audio = this.transform.GetComponent<AudioSource>();
    }
    protected override void _Start()
    {
        base._Start();
        moveUpDown = UnityEngine.Random.Range(0, 2) > 0;
        ballPrefab = ResourceConnect.GetPrefab("MagniteWellBall").transform;
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "TableArea") {
            (sender as TableArea).Reconnect(this);
        }
    }
    void FollowMagneticField() {
        if (connectMagnite != null)
        {
            Vector3 dir = connectMagnite.CenterOfTower.position - centerOfTower.position;
            dir.Normalize();
            float rad = Mathf.Atan2(dir.y, dir.x);
            float angle = ((180 / Mathf.PI) * rad) + centerAddAngle;
            centerOfTower.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    public void SetBall() {
        sendBallOtherSupply = true;
        distanceSupply = 0f;
        SignalSettings();
    }
    void SignalSettings() {
        signalEnable = true;
        signalElapsed = 0;
        centerAnimator.SetBool("Signal", true);
    }
    void SignalArea() {
        if (signalEnable) {
            signalElapsed += Time.deltaTime;
            if (GameInstance.GameEffects)
            {
                audio.clip = clipSignal;
                if (!audio.isPlaying) { audio.Play(); }
            }
            if (signalElapsed > signalTime) {
                centerAnimator.SetBool("Signal", false);
                signalEnable = false;
                fillEnergyLine = true;
                this.ball = MagniteWellBall.CreateMe(ballPrefab, BallType.Well, (BallColor)UnityEngine.Random.Range(1,4));
                this.ball.Supply = this;
                TableArea.Instance.GetElement<MagniteKeeper>("MagniteKeeper").BallStarted();
            }
        }
    }
    public void Stop() {
        signalEnable = false;
        sendBallOtherSupply = false;
        fillEnergyLine = false;
        signalElapsed = 0f;
        centerAnimator.SetBool("Signal", false);
        this.ball = null;
    }
    public void UnlinkBall() {
        if (this.ball != null)
        {
            sendBallOtherSupply = false;
            fillEnergyLine = false;
            this.ball.DestroyAfter(8f);
            this.ball = null;
        }
    }
    void FillEnergyLine() {
        if (fillEnergyLine && this.ball != null)
        {
            if (sendBallOtherSupply)
            {
                distanceSupply += (0.2f * Time.deltaTime);
                if (distanceSupply > 1)
                {
                    distanceSupply = 1;
                }
                if (GameInstance.GameEffects)
                {
                    audio.clip = clipLine;
                    if (!audio.isPlaying) { audio.Play(); }
                }
                
                Vector3 movePosition = Vector3.Lerp(
                    this.transform.position,
                    connectMagnite.transform.position,
                    distanceSupply);
                ball.transform.position = movePosition;

                if (distanceSupply >= 1)
                {
                    sendBallOtherSupply = false;
                    fillEnergyLine = false;
                    this.ball.DestroyOutOfTime();
                    TableArea.Instance.GetElement<MagniteKeeper>("MagniteKeeper").BallOverDistance();
                }
            }
        }
    }
    void MoveRandSide() {
        int rand = UnityEngine.Random.Range(0, (int)(2500f / (TableArea.Instance.Save.Level / 25f)));
        if (rand < 20)
        {
            moveUpDown = !moveUpDown;
        }
    }
    void MoveSystem() {
        MoveRandSide();
        if (moveUpDown)
        {
            AddMove((0.2f + ((0.006f *speedDivideTimes) * (TableArea.Instance.Save.Level /10)) ) * Time.deltaTime);
            if (path.Percent >= 1)
            {
                moveUpDown = false;
            }
        }
        else
        {
            AddMove(-(0.2f + ((0.006f * speedDivideTimes) * (TableArea.Instance.Save.Level /10)) ) * Time.deltaTime);
            if (path.Percent <= 0)
            {
                moveUpDown = true;
            }
        }
    }
    protected override void _Update()
    {
        base._Update();
        FollowMagneticField();

        if (GameState.StateMain != GameMainState.Game) { return; }
        SignalArea();
        FillEnergyLine();
        MoveSystem();
    }

    





}
