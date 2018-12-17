using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class BaseControl : Base
{
    List<Base> controlled;

    public List<Base> Controlled {
        get { return controlled; }
    }

    protected override void _Awake()
    {
        base._Awake();
        controlled = new List<Base>();
    }
    public void SendControlAgain(Base connecter)
    {
        controlled.Clear();
        SendAsk<Base>(connecter);
    }
    public void Reconnect(Base com) {
        if (controlled.Count > 0) {
            for (int i = 0; i < controlled.Count; ++i) {
                if (controlled[i].name == com.name) {
                    controlled[i] = com;
                    return;
                }
            }
        }
        controlled.Add(com);
    }
    public void DeleteConnect(Base com) {
        if (controlled.Count > 0)
        {
            int index = -1;
            for (int i = 0; i < controlled.Count; ++i)
            {
                if (controlled[i].name == com.name)
                {
                    index = i;
                }
            }
            if (index >= 0) {
                controlled.RemoveAt(index);
            }
        }
    }
    public T GetElement<T>(string name) where T : Base
    {
        if (controlled.Count > 0)
        {
            foreach (var c in controlled)
            {
                if (c.name == name)
                {
                    return c as T;
                }
            }
        }
        return null;
    }


















}
