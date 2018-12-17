using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SaveData:ICloneable {

    string difficulty = "";
    DateTime date;
    int level = 1;
    int ballsCatch = 0;
    int ballsLost = 0;

    public string Difficulty {
        get { return difficulty; }
        set { difficulty = value; }
    }
    public DateTime Date {
        get { return date; }
        set { date = value; }
    }
    public int Level {
        get { return level; }
        set { level = value; }
    }
    public int BallsCatch {
        get { return ballsCatch; }
        set { ballsCatch = value; }
    }
    public int BallsLost {
        get { return ballsLost; }
        set { ballsLost = value; }
    }

    public object Clone()
    {
        return new SaveData() {
            Difficulty = this.difficulty,
            Date = this.date,
            Level = this.level,
            BallsCatch = this.ballsCatch,
            BallsLost = this.ballsLost
        };
    }


}
