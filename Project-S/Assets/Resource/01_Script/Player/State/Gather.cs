using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gather : State
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
            stateInfo.onActionEnd?.Invoke();
        }
        else
        {
            LookTarget(cropsPos);
            stateInfo.animController.PlayAnimCrossFade(GetType().Name, 0.3f);
        }
    }

    public override void UpdateState()
    {
        if (!stateInfo.animController.anim.isPlaying)
        {
            FarmManager.Instance.HarvestCrops(cropsPos);

            stateInfo.onActionEnd?.Invoke();
        }
    }

    public override void ExitState()
    {
    }



}
