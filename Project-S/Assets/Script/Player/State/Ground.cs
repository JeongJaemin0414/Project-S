using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : IPlayerState
{
    public event Action<PlayerStateType> OnEnterStateEnter;
    public event Action OnUpdateStateEnter;
    public event Action OnExitStateEnter;
    public void EnterState()
    {
        OnEnterStateEnter?.Invoke(PlayerStateType.Ground);
    }

    public void ExitState()
    {
        OnUpdateStateEnter?.Invoke();
    }

    public void UpdateState()
    {
        OnExitStateEnter?.Invoke();
    }

}