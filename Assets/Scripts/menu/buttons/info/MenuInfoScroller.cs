using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MenuInfoScroller : Base, ITouchAddressee
{
    RectTransform content;
    Vector2 delta;
    float moveY = 0f;
    float moveLimit = 0f;

    protected override void _Awake()
    {
        base._Awake();
        content = FindChild("Content").transform as RectTransform;
        moveLimit = content.sizeDelta.y - 1920f;
        if (moveLimit < 0) { moveLimit = 0f; }
    }
    public void Touch(object sender, GameInputArgs e)
    {
        if (e.TouchID != 0) { return; }
        delta = e.DeltaPosition;
        AddMoveY(e.DeltaPosition.y);
    }
    void AddMoveY(float y) {
        moveY += y*3f;
        if (moveY > moveLimit)
        {
            moveY = moveLimit;
        }
        else if(moveY < 0) {
            moveY = 0f;
        }
    }
    void Scrolling() {
        Vector2 pos = content.anchoredPosition;
        pos.y = Mathf.Lerp(pos.y,moveY, 0.3f);
        content.anchoredPosition = pos;
    }
    protected override void _Update()
    {
        base._Update();
        Scrolling();
    }


















}
