using UnityEngine;

public class EnemyStateMachine 
{
    public IEnemyState CurrentState;

    public void ChangeState(IEnemyState state)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }

        CurrentState = state;
        CurrentState.OnEnter();
    }

}
