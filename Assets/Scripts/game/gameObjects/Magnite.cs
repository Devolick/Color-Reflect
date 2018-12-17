using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PathSystem))]
public class Magnite : GameSubject {

    protected PathSystem path;
    [SerializeField]
    float startPercentPosition = 0f;
    protected float speedDivideTimes = 1f;

    public float StartPercentPosition
    {
        set { startPercentPosition = value; }
    }
    public float PathDistance {
        get {return path.Distance; }
    }
    public float Percent {
        get { return path.Percent; }
    }

    protected override void _Awake()
    {
        base._Awake();
        path = this.transform.GetComponent<PathSystem>();
    }
    protected override void _Start()
    {
        base._Start();
        path.Move(startPercentPosition);
        DistanceByScale(TableArea.Instance.Save.Difficulty);
    }
    public void Move(float percent) {
       path.Move(percent);
    }
    public void AddMove(float percent)
    {
        path.AddMove(percent);
    }
    void DistanceByScale(string difficulty)
    {
        switch (difficulty)
        {
            case "Easy":
                {
                    this.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    path.PercentPadding = 0.35f;
                    speedDivideTimes = 1f;
                    break;
                }
            case "Normal":
                {
                    this.transform.localScale = new Vector3(1.05f, 1.05f, 1.05f);
                    path.PercentPadding = 0.25f;
                    speedDivideTimes = 0.55f;
                    break;
                }
            case "Hard":
                {
                    this.transform.localScale = new Vector3(0.9f,0.9f, 0.9f);
                    path.PercentPadding = 0.15f;
                    speedDivideTimes = 0.25f;
                    break;
                }
        }
    }
    protected void ReturnCenter() {
        path.ReturnCenter();
    }
    protected override void _Update()
    {
        base._Update();
        if (GameState.StatePlay == GamePlayState.Clear)
        {
            ReturnCenter();
        }
    }








}
