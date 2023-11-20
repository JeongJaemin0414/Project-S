using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : State
{
    public override void EnterState()
    {
    }

    public override void UpdateState()
    {
        if (!stateData.anim.isPlaying)
        {
            stateData.onActionEnd?.Invoke();
            FarmManager.Instance.OnCrops(gameObject.transform.position);
        }
    }

    public override void ExitState()
    {
    }



}
