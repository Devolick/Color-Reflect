using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


[RequireComponent(typeof(GameState))]
public class TableArea : BaseControl {
    static TableArea instance;
    [SerializeField]
    List<MagniteBall> ballsInGame;
    bool needSave = false;
    SaveData save;
    SaveData clone;

    float experience = 0f;
    [SerializeField]
    float points = 0f;
    [SerializeField]
    float maxPoints = 250f;
    [SerializeField]
    int timesBallsOut = 50;
    Delay delayMethod;
    float testYouGet = 0f;
    bool levelOver = false;

    public static TableArea Instance
    {
        get { return instance; }
    }
    public SaveData Save {
        get { return save; }
    }
    public float Experience {
        get { return experience; }
    }
    public bool LevelOver {
        get { return levelOver; }
        set { levelOver = value; }
    }

    protected override void _Awake()
    {
        base._Awake();
        instance = this;
        SendAsk<GameSubject>(this);
        save = GameInstance.Save.GetLevel();
        clone = save.Clone() as SaveData;
        delayMethod = new Delay();
        ballsInGame = new List<MagniteBall>();
    }
    protected override void _Start()
    {
        base._Start();
        GameInstance.Loader.ContentReady();
        UIGameControl.Instance.GetElement<UIGameLevelStarted>("LevelStarted").Open(true);
    }
    public void CountOutBall() {
        ++save.BallsLost;
        CountSendAreaBalls();
    }
    void CountSendAreaBalls() {
        --timesBallsOut;
        UIGameControl.Instance.GetElement<BallsWindow>("BallsWindow").SetText = "" + timesBallsOut;
        CheckGameStatus();
        if (timesBallsOut >= 1 && !levelOver)
        {
            GetElement<MagniteSupplyArea>("MagniteSupplyArea").SendBall(this);
        }
    }
    void CheckGameStatus()
    {
        if (experience >= 1)
        {
            save.Level += 1;
            needSave = true;
            levelOver = true;
            experience = 0f;
            if (save.Level > 50)
            {
                save.Level = 50;
                UIGameControl.Instance.GetElement<UIGameGameWon>("GameWon").Open(true);
            }
            else if (save.Level <= 50)
            {
                UIGameControl.Instance.GetElement<UIGameLevelPassed>("LevelPassed").Open(true);
                clone = save.Clone() as SaveData;
            }
        }
        else if (experience < 1 && timesBallsOut <= 0)
        {
            UIGameControl.Instance.GetElement<UIGameLevelFailed>("LevelFailed").Open(true);
            levelOver = true;
            save = clone.Clone() as SaveData;
        }
    }
    public void CountPointsOfBall(BallState ballState, float shotDistance)
    {
        ++save.BallsCatch;
        UpdateExpAndPoints(ballState, shotDistance);
        CountSendAreaBalls();
    }
    void UpdateExpAndPoints(BallState ballState, float shotDistance) {
        points += (1f * shotDistance);
        experience = points / maxPoints;
    }
    public void ClearStart() {
        levelOver = false;
        RemoveAllBalls();
        experience = 0;
        timesBallsOut = 50;
        UIGameControl.Instance.GetElement<BallsWindow>("BallsWindow").SetText = "" + timesBallsOut;
        GetElement<MagniteSupplyArea>("MagniteSupplyArea").StopSupplys();
        points = 0;
        maxPoints = 50 + (100f * (save.Level / 50f));
    }
    public void UnloadContent() {
        if (needSave)
        {
            save.Date = DateTime.Now;
            GameInstance.Save.Save(save);
        }
        RemoveAllBalls();
        Destroy(this.gameObject);
    }

    public void AddBall(MagniteBall ball) {
        ballsInGame.Add(ball);
    }
    public void RemoveBall(MagniteBall ball) {
        ballsInGame.Remove(ball);
        Destroy(ball.gameObject);
    }
    public void RemoveAllBalls() {
        foreach (MagniteBall b in ballsInGame) {
            b.DestroyOnly();
        }
        ballsInGame.Clear();
    }
    public void FreezePhysicsBalls(bool freeze) {
        foreach (MagniteBall b in ballsInGame)
        {
            b.FreezePhysics(freeze);
        }
    }





}
