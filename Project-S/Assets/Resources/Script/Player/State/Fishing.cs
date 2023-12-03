using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : State
{
    private Vector3 cropsPos;
    public override void EnterState()
    {
        cropsPos = GetMousePointinDistance();
        LookTarget(cropsPos);
    }

    public override void UpdateState()
    {
        if (!stateData.anim.isPlaying)
        {
            FarmManager.Instance.CreateCrops(cropsPos);
            stateData.onActionEnd?.Invoke();
        }
    }

    public override void ExitState()
    {
    }


}