
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReadyToAttackState : IPlayerState
{
    private Player player;
    private Health playerHealth;
    private Animator playerAnimator;
    private CinemachineBrain cinemachineBrain;
    private EventSystem current;
    private PlayerAttack playerAttack;
    public ReadyToAttackState(Player player)
    {
        this.player = player;
        playerHealth = player.GetComponent<Health>();
        playerAnimator = player.GetComponent<Animator>();
        playerAttack = player.GetComponent<PlayerAttack>();
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        current = EventSystem.current;
    }
    public void OnEnter()
    {
        CameraController.Instance.ChangeToIdleCam();
    }
    public void OnLogic()
    {
        if (Input.GetMouseButtonDown(0) && !cinemachineBrain.IsBlending &&
            !playerHealth.IsDead() && !current.IsPointerOverGameObject())
        {
            playerAttack.Aim();
            playerAnimator.SetTrigger("Aim");
            player.StateMachine.ChangeState(player.AimState);
        }
    }

    public void OnExit()
    {
        
    }

    public void OnLateLogic()
    {
        
    }
}
