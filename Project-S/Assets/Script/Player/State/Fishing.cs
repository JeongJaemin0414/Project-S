using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : State
{
    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        if (!stateData.anim.isPlaying)
        {
            FarmManager.Instance.CreateCrops(gameObject.transform.position);
            stateData.onActionEnd?.Invoke();
        }
    }

    public override void ExitState()
    {
    }


}