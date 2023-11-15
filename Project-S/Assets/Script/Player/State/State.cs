using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct StateData
{
    public Animation anim;
    public Action onActionEnd;
}

public abstract class State : MonoBehaviour
{
    protected StateData stateData;
    
    public void Init(StateData _stateData)
    {
        stateData = _stateData;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
