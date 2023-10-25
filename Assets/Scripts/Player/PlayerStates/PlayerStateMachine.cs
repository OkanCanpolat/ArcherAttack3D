
public class PlayerStateMachine 
{
    public IPlayerState CurrentState;

    public void ChangeState(IPlayerState state)
    {
        if(CurrentState != null)
        {
            CurrentState.OnExit();
        }

        CurrentState = state;
        CurrentState.OnEnter();
    }
}
