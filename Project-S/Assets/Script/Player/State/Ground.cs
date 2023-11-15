using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : State
{
    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        if (!stateData.anim.isPlaying)
        {
            stateData.onActionEnd?.Invoke();
        }
    }

    public override void ExitState()
    {

    }


}