using UnityEngine;
using System.Collections;
using System;

public class GameInputArgs : EventArgs {

	public Transform Trans {
        get;
        set;
    }
    public InputGestures Gesture {
        get;
        set;
    }
    public int TouchID {
        get; set;
    }
    public Vector2 Position {
        get; set;
    }
    public Vector2 DeltaPosition {
        get; set;
    }

    /// <summary>
    /// Equals object even they two got null return true
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    public bool NullEquals(GameInputArgs arg)
    {
        if (this.Trans == null && arg.Trans == null)
        {
            return true;
        }
        else
        {
            if (this.Trans != null && arg.Trans != null)
            {
                if (this.Trans.Equals(arg.Trans))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }


}
