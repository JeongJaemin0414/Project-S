using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : State
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
            stateData.onActionEnd?.Invoke();
            FarmManager.Instance.HarvestCrops(cropsPos);
        }
    }

    public override void ExitState()
    {

    }


}