using UnityEngine;
using System.Collections;

public sealed class Delay
{
    public delegate void CallBack();
    CallBack callback = null;

    float elapsedTime = 0;
    float delayTime = 0;
    bool delayRun = false;
    bool delayRepeat = false;
    bool delayTimes = false;
    bool delayOnce = false;
    int execTimes = 1;

    public Delay() { }

    public float Percent
    {
        get
        {
            return ((elapsedTime * 100) / delayTime) / 100;
        }
    }
    public bool Run
    {
        get { return delayRun; }
    }
    public void Stop()
    {
        Clear();
    }
    public float Timer {
        get { return elapsedTime; }
    }
    public float LeftTime {
        get { return delayTime - elapsedTime; }
    }

    /// <summary>
    /// Register Method to callback, warning it's like recursion style
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="delayTime"></param>
    void RegisterRepeat(CallBack callback, float delayTime)
    {
        if (delayRepeat)
            return;
        Clear();
        this.delayRun = true;
        this.delayRepeat = true;
        this.callback = callback;
        this.delayTime = delayTime;
    }
    /// <summary>
    /// Register method to callback, callback called once
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="delayTime"></param>
    /// <param name="repeatStyle"></param>
    public void RegisterOnce(CallBack callback, float delayTime, bool once)
    {
        if (!once)
        {
            if (delayRepeat)
                return;
            RegisterRepeat(callback, delayTime);
        }
        else
        {
            if (delayOnce)
                return;
            Clear();

            this.delayRun = true;
            this.delayOnce = true;
            this.callback = callback;
            this.delayTime = delayTime;
        }
    }
    /// <summary>
    /// Register method to callback, repeat by execTimes parameter
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="delayTime"></param>
    /// <param name="execTimes"></param>
    public void RegisterTimes(CallBack callback, float delayTime, int execTimes)
    {
        if (delayTimes)
            return;
        Clear();

        this.delayRun = true;
        this.delayTimes = true;
        this.callback = callback;
        this.delayTime = delayTime;
        this.execTimes = execTimes;
    }

    void Clear()
    {
        callback = null;
        delayRun = false;
        elapsedTime = 0;
        delayTime = 0;
        delayTimes = false;
        delayOnce = false;
        delayRepeat = false;
        delayOnce = false;
        execTimes = 1;
    }

    void Repeat(float delta)
    {
        if (delayRepeat)
        {
            elapsedTime += delta;
            if (elapsedTime >= delayTime)
            {
                elapsedTime = elapsedTime - delayTime;
                if (callback != null)
                {
                    callback();
                }
            }
        }
    }
    void RepeatTimes(float delta)
    {
        if (delayTimes)
        {
            elapsedTime += delta;
            if (elapsedTime >= delayTime)
            {
                --execTimes;
                elapsedTime = elapsedTime - delayTime;
                if (callback != null)
                {
                    callback();
                }
            }
            if (execTimes <= 0)
            {
                delayRun = false;
                delayTimes = false;
            }
        }
    }
    void RepeatOnce(float delta)
    {
        if (delayOnce)
        {
            elapsedTime += delta;
            if (elapsedTime >= delayTime)
            {
                elapsedTime = delayTime;
                delayRun = false;
                delayOnce = false;
                if (callback != null)
                {
                    callback();
                }
            }
        }
    }

    /// <summary>
    /// run logic, warning loop will repeat TRUE if OverProp does not set False
    /// </summary>
    public void PlayDelay(float delta)
    {
        Repeat(delta);
        RepeatTimes(delta);
        RepeatOnce(delta);
    }




}
