
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine;

    public ReadyToAttackState ReadyAttackState;
    public MoveToWaypointState MovementState;
    public AimState AimState;
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        ReadyAttackState = new ReadyToAttackState(this);
        MovementState = new MoveToWaypointState(this);
        AimState = new AimState(this);
    }

    private void Start()
    {
        StateMachine.ChangeState(MovementState);
    }
    private void Update()
    {
        StateMachine.CurrentState.OnLogic();
    }

    private void LateUpdate()
    {
        StateMachine.CurrentState.OnLateLogic();
    }
}
