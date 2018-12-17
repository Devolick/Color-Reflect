using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FaderLeft : Base, ITouchAddressee
{

    FaderButton button;
    SpriteRenderer render;
    AudioSource audio;

    string testGesture = "";

    protected override void _Awake()
    {
        base._Awake();
        button = FindParent("Fader").GetComponent<FaderButton>();
        render = this.transform.GetComponent<SpriteRenderer>();
        audio = this.transform.GetComponent<AudioSource>();
    }
    public void Touch(object sender, GameInputArgs e)
    {
        testGesture = e.Gesture.ToString();
        if (e.Gesture == InputGestures.End) { ChangeColor(false); return; }
        if (GameState.StateMain != GameMainState.Game) { return; }
        ChangeColor(true);
        button.DistanceMove(true);
        if (GameInstance.GameEffects) { if (!audio.isPlaying) { audio.Play(); } }
    }
    void ChangeColor(bool change)
    {
        if (change)
        {
            render.color = new Color(0, 1, 0);
        }
        else
        {
            render.color = new Color(1, 1, 1);
        }
    }













}
