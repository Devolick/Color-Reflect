using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum BallState {
None, Magnetized,Demagnetized,ElectricField
}
public enum BallColor {
None,Red,Green,Blue
}
public enum BallType {
None,Well,Fire
}

public abstract class MagniteBall : Base {

    [SerializeField]
    protected BallType typeBall = BallType.None;
    [SerializeField]
    SpriteRenderer spriteMain;
    [SerializeField]
    Rigidbody2D rigidBody;
    protected BallState state = BallState.Demagnetized;
    protected BallColor colorState = BallColor.Red;
    Collider2D collider;
    bool trigged = false;
    MagniteKeeper magniteKeeper;
    protected Delay delayMethod;
    [SerializeField]
    float destroyPointDistance = 1f;
    Vector2 saveVelocityOfPause;

    protected AudioSource audio;
    [SerializeField]
    protected AudioClip clipCollision;

    public BallType BallTypeState {
        get { return typeBall; }
        set { typeBall = value; }
    }
    public BallState State {
        set { state = value; }
        get { return state; }
    }
    public BallColor ColorState {
        get { return colorState; }
        set { colorState = value; }
    }
    public Rigidbody2D Rigid {
        get { return rigidBody; }
    }
    public SpriteRenderer Sprite {
        get { return spriteMain; }
    }
    public bool TriggerCollision {
        set {
            trigged = value;
            if (collider != null)
                collider.isTrigger = trigged;
        }
        get { return trigged; }
    }
    public UIDestroyPoint DestroyPoint {
        get;
        set;
    }

    protected override void _Awake()
    {
        base._Awake();
        rigidBody = this.transform.GetComponent<Rigidbody2D>();
        spriteMain = this.transform.GetComponent<SpriteRenderer>();
        collider = this.transform.GetComponent<Collider2D>();
        delayMethod = new Delay();
        TableArea.Instance.AddBall(this);
        DestroyPoint = GUIDestroyPointArea.Instance.CreatePoint(this);
        FillDestroyPointAngle();
        DestroyPoint.gameObject.SetActive(false);
        audio = this.transform.GetComponent<AudioSource>();
    }
    protected override void _Start()
    {
        base._Start();
        spriteMain.sprite = ResourceConnect.GetSprite("Ball" + typeBall.ToString() + colorState.ToString());
        DistanceByScale(TableArea.Instance.Save.Difficulty);
        destroyPointDistance = DestroyPointDistance(TableArea.Instance.Save.Difficulty);
    }
    public void FillForce(float speedForce) {
        //this.transform.Translate(Vector3.up * speedForce * Time.fixedDeltaTime);

        this.rigidBody.AddForce(new Vector2(0, speedForce), ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts) {
            CollideDetect(contact.collider);
        }
    }
    protected abstract void CollideDetect(Collider2D other);
    void DistanceByScale(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                {
                    this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    break;
                }
            case "Normal":
                {
                    this.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                    break;
                }
            case "Hard":
                {
                    this.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    break;
                }
        }
    }
    protected virtual void FillDestroyPointAngle() {
        if (DestroyPoint.gameObject.activeInHierarchy)
        {
            float angle = this.transform.rotation.z + (-135f);
            Vector3 rot = DestroyPoint.transform.rotation.eulerAngles;
            rot.z = angle;
            Vector3 pos = this.transform.position;
            pos.y += -destroyPointDistance;
            pos.x += destroyPointDistance;
            DestroyPoint.transform.position = pos;
            DestroyPoint.transform.rotation = Quaternion.Euler(rot);
        }
    }
    float DestroyPointDistance(string difficulty) {
        float dist = 1f;
        switch (difficulty)
        {
            case "Easy":
                {
                    dist = 0.85f;
                    break;
                }
            case "Normal":
                {
                    dist = 0.75f;
                    break;
                }
            case "Hard":
                {
                    dist = 0.68f;
                    break;
                }
        }
        return dist;
    }
    public virtual bool IsVisibleToCamera(Transform transform)
    {
        Vector3 visTest = Camera.main.WorldToViewportPoint(transform.position);
        return (visTest.x >= 0 && visTest.y >= 0) && (visTest.x <= 1 && visTest.y <= 1) && visTest.z >= 0;
    }
    public virtual void DestroyDelayStop()
    {
        delayMethod.Stop();
    }
    public virtual void DestroyAfter(float time)
    {
        delayMethod.Stop();
        delayMethod.RegisterOnce(DestroyOutOfTime, time, true);
    }
    public virtual void DestroyOutOfTime()
    {
        DestroyIt();
    }
    public virtual void DestroyIt() {
        delayMethod.Stop();
        Destroy(this.DestroyPoint.gameObject);
        TableArea.Instance.RemoveBall(this);
    }
    public void DestroyOnly() {
        delayMethod.Stop();
        Destroy(this.DestroyPoint.gameObject);
        Destroy(this.gameObject);
    }
    public void FreezePhysics(bool freeze) {
        if (freeze)
        {
            saveVelocityOfPause = Rigid.velocity;
            Rigid.isKinematic = true;
        }
        else {
            Rigid.isKinematic = false;
            Rigid.AddForce(saveVelocityOfPause,ForceMode2D.Impulse);
        }
    }
    protected override void _Update()
    {
        base._Update();

        if (GameState.StateMain != GameMainState.Game) { return; }
            delayMethod.PlayDelay(Time.deltaTime);
    }






}
