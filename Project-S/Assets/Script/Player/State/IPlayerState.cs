using System;

public interface IPlayerState
{
    public event Action<PlayerStateType> OnEnterStateEnter;
    public event Action OnUpdateStateEnter;
    public event Action OnExitStateEnter;

    public void EnterState();
    public void UpdateState();
    public void ExitState();
}
