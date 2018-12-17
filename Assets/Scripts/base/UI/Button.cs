using UnityEngine;
using System.Collections;
using System;

public abstract class Button : Base,ITouchAddressee {

    protected Delay delayMethod = null;
    protected bool buttonEnable = false;
    [SerializeField]
    protected float delayClick = 0.25f;

    protected override void _Awake()
    {
        base._Awake();
        delayMethod = new Delay();
    }
    protected virtual void Click() { buttonEnable = false; }
    protected override void _Update()
    {
        base._Update();
        delayMethod.PlayDelay(Time.deltaTime);
    }
    public virtual void Touch(object sender, GameInputArgs e)
    {
        if (buttonEnable) return;

        buttonEnable = true;
        delayMethod.RegisterOnce(Click, delayClick, true);
    }





}
