using UnityEngine;
using UnityEngine.AI;

public class MoveToWaypointState : IPlayerState
{
    private Player player;
    private Health playerHealth;
    private PlayerMovement playerMovement;
    private NavMeshAgent playerNavmesh;
    private Transform targetPosition;
    private Transform playerTransform;
    private Animator playerAnimator;
    private float arriveOffset = 0.1f;
    public MoveToWaypointState(Player player)
    {
        this.player = player;
        playerHealth = player.GetComponent<Health>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerTransform = player.transform;
        playerAnimator = player.GetComponent<Animator>();
        playerNavmesh = player.GetComponent<NavMeshAgent>();
    }
    public void OnEnter()
    {
        playerHealth.RestoreFullHealth();
        CameraController.Instance.ChangeToMovementCam();
        targetPosition = WaypointManager.Instance.GetNextPosition();
        playerNavmesh.SetDestination(targetPosition.position);
        playerMovement.LookTarget(targetPosition);
        playerAnimator.SetBool("Walk", true);
    }

    public void OnLogic()
    {
        if(playerNavmesh.remainingDistance < arriveOffset && !playerNavmesh.pathPending/*Vector3.Distance(targetPosition.position, playerTransform.position) <= arriveOffset*/)
        {
            playerMovement.LookTarget();
            WaypointManager.Instance.OnReach();
            player.StateMachine.ChangeState(player.ReadyAttackState);
        }
    }
    public void OnExit()
    {
        playerAnimator.SetBool("Walk", false);
    }

    public void OnLateLogic()
    {
        
    }
}
