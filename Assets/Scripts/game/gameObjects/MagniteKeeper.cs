using UnityEngine;
using System.Collections;

public class MagniteKeeper : Magnite {

    [SerializeField]
    public bool testLook =false;
    bool moveRight = false;
    int randByLevel = 1;
    Animator anim;
    SpriteRenderer colorEffectSprite;
    bool destroyIt = false;
    float destoyPercent = 0;
    [SerializeField]
    bool wrongBall = false;
    [SerializeField]
    Delay delayMethod;
    MagniteBall ball;
    AudioSource audio;
    [SerializeField]
    AudioClip clipExperience;
    [SerializeField]
    AudioClip clipBouncing;

    public int RandByLevel {
        set { randByLevel = value; }
    }
    protected override void _Awake()
    {
        base._Awake();
        anim = FindChild("ColorEffect").GetComponent<Animator>();
        colorEffectSprite = FindChild("ColorEffect").GetComponent<SpriteRenderer>();
        delayMethod = new Delay();
        audio = this.transform.GetComponent<AudioSource>();
        audio.clip = ResourceConnect.GetSound("expirience_plus");
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "TableArea") {
            (sender as TableArea).Reconnect(this);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        DetectBall(other);
    }
    void DetectBall(Collider2D other)
    {
        MagniteBall ballCollide = other.gameObject.GetComponent<MagniteBall>();
        if ((ballCollide as MagniteBall) != null)
        {
            if ((ballCollide as MagniteWellBall) != null)
            {
                this.ball = ballCollide as MagniteWellBall;
                this.ball.DestroyDelayStop();
                if (ballCollide.State == BallState.Magnetized)
                {
                    destroyIt = true;
                    destoyPercent = 0f;
                    anim.SetBool("Reaction", false);
                    this.ball.Rigid.AddForce(-(this.ball.Rigid.velocity * 0.4f) * this.ball.Rigid.mass, ForceMode2D.Impulse);
                    delayMethod.RegisterOnce(CompleteDetect, 1.5f, true);
                }
                else
                {
                    DetectWrongBall(ballCollide);
                }
            }
            else
            {
                DetectWrongBall(ballCollide);
            }
        }
    }
    void MoveDetectEffect()
    {
        if (destroyIt && this.ball != null)
        {
            destoyPercent += 0.8f * Time.deltaTime;
            Vector3 newPos = Vector3.Lerp(this.ball.transform.position, this.transform.position, destoyPercent);
            this.ball.transform.position = newPos;
        }
    }
    void CompleteDetect() {
        this.ball.DestroyIt();
        anim.SetTrigger("Complete");
        colorEffectSprite.color = new Color(0, 1, 0);
        TableArea.Instance.CountPointsOfBall((this.ball as MagniteWellBall).StartBallState,
                                             Vector3.Distance(
                                                 (this.ball as MagniteWellBall).DistanceCollide,this.transform.position));
        if (GameInstance.GameEffects)
        {
            audio.clip = clipExperience;
            audio.Play();
        }
        this.ball = null;
    }
    public void DetectWrongBall(MagniteBall ball)
    {
        if (this.ball != null) {
            if (this.ball.Equals(ball)) {
                this.ball = null;
            }
        }
        anim.SetBool("Reaction", false);
        anim.SetTrigger("Wrong");
        colorEffectSprite.color = new Color(1, 0, 0);
        if (GameInstance.GameEffects)
        {
            audio.clip = clipBouncing;
            audio.Play();
        }

        Vector3 dir = ball.transform.position - this.transform.position;
        dir.Normalize();
        float rad = Mathf.Atan2(dir.y, dir.x);
        float angle = ((180 / Mathf.PI) * rad) - 90 + UnityEngine.Random.Range(-15, 16);
        Vector3 newVecDirection = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up;
        ball.Rigid.AddForce(-ball.Rigid.velocity * ball.Rigid.mass, ForceMode2D.Impulse);
        ball.Rigid.AddForce((new Vector2(newVecDirection.x, newVecDirection.y) * 5), ForceMode2D.Impulse);
        wrongBall = false;
        ball.Sprite.sortingOrder = 10;
        ball.DestroyAfter(2f);
    }
    void MoveSystem() {
        if (moveRight)
        {
            AddMove((0.2f   + ((0.004f * speedDivideTimes) * (TableArea.Instance.Save.Level /10))) * Time.deltaTime);
            if (path.Percent >= 1)
            {
                moveRight = false;
            }
        }
        else
        {
            AddMove(-(0.2f + ((0.004f *speedDivideTimes) * (TableArea.Instance.Save.Level/10))) * Time.deltaTime);
            if (path.Percent <= 0)
            {
                moveRight = true;
            }
        }
    }
    public void BallStarted() {
        anim.SetBool("Reaction", true);
        colorEffectSprite.color = new Color(0, 0, 1);
    }
    public void BallOverDistance() {
        anim.SetBool("Reaction", false);
    }
    protected override void _Update()
    {
        base._Update();

        if (GameState.StateMain != GameMainState.Game) { return; }
        delayMethod.PlayDelay(Time.deltaTime);
        MoveSystem();
        MoveDetectEffect();

    }




}
