using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Base : MonoBehaviour
{


    #region Main Events/ Polymorth
    void Awake()
    {
        _Awake();
    }
    void Start()
    {
        _Start();
    }
    void FixedUpdate()
    {
        _FixedUpdate();
    }
    void Update()
    {
        _Update();
    }
    protected virtual void _Awake() { }
    protected virtual void _Start() { }
    protected virtual void _FixedUpdate() { }
    protected virtual void _Update() { }
    #endregion
    #region Find Object in tree
    public GameObject FindRoot() {
        if (gameObject.transform.parent != null)
            return SearchTreeRoot(this.gameObject);
        else
            return this.gameObject;
    }
    GameObject SearchTreeRoot(GameObject gameObject) {
        if (gameObject.transform.parent != null)
        {
            GameObject o = gameObject.transform.parent.gameObject;
            GameObject oo = SearchTreeRoot(o);
            if (oo == null)
            {
                return o;
            }
            else {
                return oo;
            }
        }
        else
            return null;
    }
    public bool FindParent(string name, out GameObject gameObject)
    {
        GameObject o = SearchTreeUp(name, this.gameObject);
        if (o != null)
        {
            gameObject = o;
            return true;
        }
        gameObject = null;
        return false;
    }
    public GameObject FindParent(string name)
    {
        return SearchTreeUp(name, this.gameObject);
    }
    GameObject SearchTreeUp(string name, GameObject gameObject) {
        GameObject o = null;
        if (gameObject.transform.parent != null)
        {
            o = gameObject.transform.parent.gameObject;
            if (o.name == name)
            {
                return o;
            }
            else
            {
                return SearchTreeUp(name, o);
            }
        }
        return null;
    }
    GameObject SearchTreeDown(string name, GameObject gameObject)
    {
        if (gameObject != null)
        {
            foreach (Transform trs in gameObject.transform)
            {
                if (trs.gameObject.name == name)
                {
                    return trs.gameObject;
                }
                else
                {
                    GameObject o = SearchTreeDown(name, trs.gameObject);
                    if (o != null)
                    {
                        return o;
                    }
                }
            }
        }
        return null;
    }
    T[] SearchTreeDownItems<T>(GameObject gameObject) where T :Base
    {
        List<T> list = new List<T>();
        if (gameObject != null)
        {
            T t = null;
            foreach (Transform trs in gameObject.transform)
            {
                t = trs.GetComponent<T>();
                if (t != null)
                {
                    list.Add(t);
                }
                T[] tt = SearchTreeDownItems<T>(trs.gameObject);
                if (tt != null)
                {
                    if (tt.Length > 0)
                    {
                        list.AddRange(tt);
                    }
                }
            }
        }
        return list.ToArray();
    }
    public bool FindChild(string name, out GameObject gameObject)
    {
        GameObject o = SearchTreeDown(name, this.gameObject);
        if (o != null)
        {
            gameObject = o;
            return true;
        }
        gameObject = null;
        return false;
    }
    public T[] FindChilds<T>() where T : Base
    {
        return SearchTreeDownItems<T>(this.gameObject);
    }
    public GameObject FindChild(string name)
    {
        return SearchTreeDown(name, this.gameObject);
    }
    public GameObject FindChild(string parentChild, string child) {
        GameObject parent = SearchTreeDown(parentChild, this.gameObject);
        return SearchTreeDown(child, parent);
    }
    public GameObject FindChildIn(string name,GameObject gameObject)
    {
        return SearchTreeDown(name, gameObject);
    }
    #endregion
    #region Search in array
    public T GetElement<T>(List<Base> list) where T : Base
    {
        if (list.Count > 0)
        {
            foreach (var c in list)
            {
                if (c.GetType() == typeof(T))
                {
                    return c as T;
                }
            }
        }
        return null;
    }
    public T GetElement<T>(List<Base> list, string name) where T : Base
    {
        if (list.Count > 0)
        {
            foreach (var c in list)
            {
                if (c.name == name)
                {
                    return c as T;
                }
            }
        }
        return null;
    }
    #endregion
    public virtual void AskYou(Base sender) {
    }
    protected void SendAsk<T>(Base sender) where T : Base
    {
        T[] arr = FindChilds<T>();
        if (arr.Length > 0)
        {
            foreach (T b in arr)
            {
                b.AskYou(sender);
            }
        }
    }














}
