using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : State
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
            FarmManager.Instance.OnCrops(cropsPos);

            stateData.onActionEnd?.Invoke();
        }
    }

    public override void ExitState()
    {
    }



}
