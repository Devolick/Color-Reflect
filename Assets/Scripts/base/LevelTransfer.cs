using UnityEngine;
using System.Collections;

public class LevelTransfer : Base {
    protected static LevelTransfer instance;

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
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }





















}
