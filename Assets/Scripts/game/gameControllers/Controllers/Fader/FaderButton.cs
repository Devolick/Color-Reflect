using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FaderButton : Base
{
    MagnitePipe pipeGun;

    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "PipeGun")
        {
            pipeGun = sender as MagnitePipe;
        }
    }
    public void DistanceMove(bool left) {
        if (!left)
        {
            pipeGun.AddMove(0.5f * Time.deltaTime);
        }
        else
        {
            pipeGun.AddMove(-0.5f * Time.deltaTime);
        }
    }





}
