using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class RowSave : Row, ITouchAddressee
{
    public TableSave MyTable {
        set;
        get;
    }
    public bool Select {
        get; set;
    }
    Image image;
    AudioSource audio;
    Text textLevel;
    
    protected override void _Awake()
    {
        base._Awake();
        image = FindChild("Level").GetComponent<Image>();
        audio = this.transform.GetComponent<AudioSource>();
        textLevel = GetText("Level", "lvl");
    }
    public void Touch(object sender, GameInputArgs e)
    {
        if (Select) { return; }
        if (e.TouchID != 0) { return; }
        if (Int32.Parse(textLevel.text) >= 50) { return;}
        
        Select = true;
        MyTable.SwapSelect(this);
        GameInstance.Save.Difficulty = LevelDifficulty;
        if (GameInstance.GameEffects) { audio.Play(); }
    }
    void RowSelect() {
        Color color = image.color;
        if (Select)
        {
            color = new Color(1f, 0.7f, 0f,0.23f);
        }
        else {
            color = new Color(1f, 1f, 1f, 0.23f);
        }
        image.color = color;
    }
    protected override void _Update()
    {
        base._Update();
        RowSelect();
    }












}
