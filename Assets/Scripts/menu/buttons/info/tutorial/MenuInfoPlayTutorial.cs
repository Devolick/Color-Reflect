using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;



public class MenuInfoPlayTutorial : Button
{
    float delayFirstFrame = 0.2f;
    float elapsedFirstFrame = 0f;
    [SerializeField]
    MovieSlide movie;
    [SerializeField]
    RawImage raw;
    Image image;
    public Sprite playButton;
    public Color colorPlay;
    public Sprite repeatButton;
    public Color colorRepeat;
    bool videoPlay = false;
    bool videoOverPlay = true;
    
    protected override void _Awake()
    {
        base._Awake();
        image = this.transform.GetComponent<Image>();

    }
    public override void Touch(object sender, GameInputArgs e)
    {
        if (UIMenuControl.State == MenuControlState.Lock) { return; }
        if (e.Gesture != InputGestures.Tap || e.TouchID != 0) { return; }
        if (buttonEnable) { return; }

        base.Touch(sender, e);
        if (!videoPlay)
        {
            videoPlay = true;
            videoOverPlay = false;
            image.color = colorPlay;
            movie.Play();
        }
        else {
            image.color = colorRepeat;
            movie.Repeat();
        }
    }
    protected override void Click()
    {
        base.Click();
        ChangeStateButton(videoPlay);
    }
    void ChangeStateButton(bool state) {
        if (state)
        {
            image.sprite = repeatButton;
        }
        else
        {
            image.sprite = playButton;
        }
        image.color = new Color(1, 1, 1, 1);
    }
    protected override void _Update()
    {
        base._Update();
        if (!videoOverPlay)
        {
            if (!movie.isPlaying)
            {
                videoOverPlay = true;
                ChangeStateButton(false);
            }
        }
    }








}
