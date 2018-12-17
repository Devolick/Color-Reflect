using UnityEngine;
using System.Collections;

public abstract class SaveMode:Base {

    public string LoadFrom {
        get;
        set;
    }
    public string Difficulty {
        get;
        set;
    }

    public abstract SaveType Open();
    public abstract SaveData Open(string difficulty);
    public abstract SaveData GetLevel();
    public abstract void Save(SaveData save);










}
