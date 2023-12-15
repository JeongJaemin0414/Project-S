using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : State
{
    private Vector3 cropsPos;
    public override void Init(StateInfo newStateInfo)
    {
        stateInfo = newStateInfo;
    }

    public override void EnterState()
    {
        cropsPos = GetMousePointinDistance();

        if (FarmManager.Instance.IsCreateCropsAble(cropsPos))
        {
            LookTarget(cropsPos);
            stateInfo.animController.PlayAnimCrossFade(GetType().Name, 0.3f);
        }
        else
        {
            stateInfo.onActionEnd?.Invoke();
        }
    }

    public override void UpdateState()
    {
        if (!stateInfo.animController.anim.isPlaying)
        {
            FarmManager.Instance.OnCrops(cropsPos);

            stateInfo.onActionEnd?.Invoke();
        }
    }

    public override void ExitState()
    {
    }



}
