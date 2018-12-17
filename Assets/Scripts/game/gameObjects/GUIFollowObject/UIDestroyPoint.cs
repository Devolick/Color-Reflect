using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDestroyPoint : Base {

    Text text; 

    public string SetText {
        set { text.text = value; }
    }

    protected override void _Awake()
    {
        base._Awake();
        text = FindChild("Text").GetComponent<Text>();
    }














}
