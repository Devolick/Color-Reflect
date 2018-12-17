using UnityEngine;
using System.Collections;

public class NewMobileTouch : Base {

    bool touched = false;
    bool touchedWait = false;

    #region Touch vars 
    GameInputArgs lastArgs = null;
    bool firstTouch = false;
    bool firstTargetChangedByMove = false;
    [SerializeField]
    bool doubleTapStartTime = false;
    GameInputArgs lastArgsBetweenTaps = null;
    float doubleTapElapsed = 0f;
    #endregion

    public GameInputArgs Arg {
        get { return lastArgs; }
    }

    public int Hash() {
        if (lastArgs != null)
        {
            if (lastArgs.Trans != null)
            return lastArgs.Trans.GetHashCode();
        }
            return 0;
    }
    public void Check(UnityEngine.Touch touch, GameInputArgs resultArgs) {
        touched = true;
        UpdateTouch(touch, resultArgs);
    }
    public void UnChecked() {
        if (touched)
        {
            //has phased
            touched = false;
            touchedWait = true;
        }
        else {
            //end phase if does has repeat
            touchedWait = false;
            EndUpdate();
        }
    }
    public void EndCheck() {
        //end if touched overed
        if (touchedWait)
        {
            touchedWait = false;
            EndUpdate();
        }
    }
    #region touch logic
    public void UpdateTouch(UnityEngine.Touch t,GameInputArgs resultArgs)
    {
        //GameInputArgs resultArgs;
        //FillScreenObject(out resultArgs, t.position);
        //MultiTouchSettings(resultArgs, t);
        FirstPhase(resultArgs);
        TapTarget(lastArgs, resultArgs);
        MovePhase(resultArgs, t);
        StaticPhase(resultArgs, t);
    }
    public void RunUpdate() {
        CountElapsedDoubleTap();
    }
    public void EndUpdate()
    {
        TapOrDoubleTap(lastArgs);
        EndPhase(lastArgs);
        lastArgs = null;
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
    protected void InputToObject(GameInputArgs resultArgs)
    {
        if (resultArgs.Trans != null)
        {
            ITouchAddressee touchSend = resultArgs.Trans.gameObject.GetComponent<ITouchAddressee>();
            if (touchSend != null)
            {
                touchSend.Touch(this, resultArgs);
            }
        }
    }
    protected void FirstPhase(GameInputArgs resultArgs)
    {
        if (!firstTouch)
        {
            firstTouch = true;
            firstTargetChangedByMove = false;
            if (resultArgs.Trans != null)
            {
                resultArgs.Gesture = InputGestures.First;
                InputToObject(resultArgs);
            }

            this.lastArgs = resultArgs;
        }
    }
    protected void StaticPhase(GameInputArgs resultArgs, UnityEngine.Touch t) {
        if (t.phase == TouchPhase.Stationary) {
            if (resultArgs.Trans != null) {
                resultArgs.Gesture = InputGestures.Stationary;
                InputToObject(resultArgs);
            }
        }
    }
    protected void MovePhase(GameInputArgs resultArgs, UnityEngine.Touch t)
    {
        if (t.phase == TouchPhase.Moved)
        {
            if (resultArgs.Trans != null)
            {
                resultArgs.Gesture = InputGestures.Move;
                InputToObject(resultArgs);
            }
            MoveSwap(lastArgs, resultArgs);
        }
    }
    protected void MoveSwap(GameInputArgs lastTouchArgs, GameInputArgs resultArgs)
    {
        if (!resultArgs.NullEquals(lastTouchArgs))
        {
            if (lastTouchArgs.Trans != null)
            {
                lastTouchArgs.Gesture = InputGestures.End;
                InputToObject(lastTouchArgs);
            }
            if (resultArgs.Trans != null)
            {
                resultArgs.Gesture = InputGestures.First;
                InputToObject(resultArgs);
            }
            this.lastArgs = resultArgs;
        }
    }
    protected void TapTarget(GameInputArgs lastTouchArgs, GameInputArgs resultArgs)
    {
        if (!resultArgs.NullEquals(lastTouchArgs))
        {
            firstTargetChangedByMove = true;
        }
    }
    void TapOrDoubleTap(GameInputArgs resultArgs)
    {
        if (doubleTapElapsed <= 0.43f &&
            doubleTapStartTime &&
            resultArgs.NullEquals(lastArgsBetweenTaps))
        {
            DoubleTapPhase(resultArgs);
            return;
        }
        TapPhase(resultArgs);
    }
    protected void TapPhase(GameInputArgs resultArgs)
    {
        if (resultArgs.Trans != null && !firstTargetChangedByMove)
        {
            lastArgsBetweenTaps = resultArgs;
            doubleTapStartTime = true;
            doubleTapElapsed = 0f;
            resultArgs.Gesture = InputGestures.Tap;
            InputToObject(resultArgs);
        }
    }
    protected void DoubleTapPhase(GameInputArgs resultArgs)
    {
        if (resultArgs.Trans != null && !firstTargetChangedByMove)
        {
            resultArgs.Gesture = InputGestures.Double;
            InputToObject(resultArgs);
            doubleTapElapsed = 0f;
            doubleTapStartTime = false;
        }
    }
    void CountElapsedDoubleTap()
    {
        if (doubleTapStartTime)
        {
            doubleTapElapsed += Time.deltaTime;
            if (doubleTapElapsed > 10f)
            {
                doubleTapStartTime = false;
                doubleTapElapsed = 0f;
            }
        }
    }
    protected void EndPhase(GameInputArgs resultArgs)
    {
        if (resultArgs.Trans != null)
        {
            resultArgs.Gesture = InputGestures.End;
            InputToObject(resultArgs);
        }
        firstTouch = false;
        firstTargetChangedByMove = false;
    }
    #endregion
























}
