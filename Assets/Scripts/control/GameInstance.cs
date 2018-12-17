using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SaveAndroid))]
public class GameInstance : Base {
    static GameInstance instance;

    public static SaveMode Save {
        get;
        protected set;
    }
    public static ContentLoader Loader {
        get;
        protected set;
    }
    public static bool GameEffects {
        get;
        set;
    }

    protected override void _Awake()
    {
        base._Awake();
        Transfer();
    }
    void Transfer()
    {
        if (instance == null)
        {
            GameObject.DontDestroyOnLoad(gameObject);
            instance = this;
            Loader = FindChild("ContentLoader").GetComponent<ContentLoader>();
            Save = this.transform.GetComponent<SaveAndroid>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }












}
