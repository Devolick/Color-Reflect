using UnityEngine;
using System.Collections;

public interface ITouchAddressee {
    void Touch(object sender, GameInputArgs args);
}
public enum InputGestures {
None,Tap,Double,First,End,Move,Stationary
}

public class NewMobileMultiTouch : Base
{

    NewMobileTouch[] touchFinders;


    protected override void _Awake()
    {
        base._Awake();
        touchFinders = new NewMobileTouch[3];
        touchFinders[0] = new NewMobileTouch();
        touchFinders[1] = new NewMobileTouch();
        touchFinders[2] = new NewMobileTouch();
    }
    protected override void _Update()
    {
        base._Update();
        TouchInput();
        foreach (NewMobileTouch unTouched in touchFinders)
        {
            unTouched.RunUpdate();
        }
    }
    bool ContainsArgs(GameInputArgs arg,NewMobileTouch tm, NewMobileTouch[] arr) {
        for (int i = 0; i < arr.Length; ++i)
        {
            NewMobileTouch t = arr[i];
            if (!tm.Equals(arr[i]))
            {
                if (t.Arg != null)
                {
                    if (t.Arg.Trans != null)
                    {
                        if (arg != null)
                        {
                            if (arg.Trans != null)
                            {
                                if (arg.Trans.Equals(t.Arg.Trans)) { return true; }
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    void MultiTouchSettings(GameInputArgs resultArgs, UnityEngine.Touch t)
    {
        resultArgs.TouchID = t.fingerId;
        resultArgs.Position = t.position;
        resultArgs.DeltaPosition = t.deltaPosition;
    }
    protected void FillScreenObject(out GameInputArgs resultArgs, Vector2 position)
    {
        resultArgs = new GameInputArgs();
        Ray ray = Camera.main.ScreenPointToRay(position);
        resultArgs.Trans = Physics2D.Raycast(ray.origin, ray.direction).transform;
    }
    void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            foreach (UnityEngine.Touch t in Input.touches)
            {
                if (t.fingerId < 3)
                {
                    GameInputArgs resultArgs;
                    FillScreenObject(out resultArgs, t.position);
                    MultiTouchSettings(resultArgs, t);
                    NewMobileTouch tm = touchFinders[t.fingerId];
                    if (!ContainsArgs(resultArgs, tm, touchFinders))
                    {
                        tm.Check(t, resultArgs);
                    }
                }
            }
            foreach (NewMobileTouch unTouched in touchFinders)
            {
                unTouched.UnChecked();
            }
        }
        else
        {
            foreach (NewMobileTouch unTouched in touchFinders)
            {
                unTouched.EndCheck();
            }
        }
    }





}
