using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : BaseControl {
    static GameControl instance;

    public static GameControl Instance {
        get { return instance; }
    }

    protected override void _Awake()
    {
        base._Awake();
        instance = this;
    }








}
