using Cinemachine;
using UnityEngine;

public class AimState : IPlayerState
{
    private Player player;
    private Health playerHealth;
    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private CinemachineBrain cinemachineBrain;
    private Animator playerAnimator;
    private float maxXRotation;
    private float minXRotation;
    private float maxYRotation;
    private float minYRotation;

    private float rotationX;
    private float rotationY;
    private Transform playerSpine;
    private Transform aimCameraTarget;
    private Vector3 startingRotation;
    private Vector3 startingAimCameraRotation;
    private bool isBlendEnd;
    private bool canShot;
    public AimState(Player player)
    {
        this.player = player;
        playerHealth = player.GetComponent<Health>();
        playerMovement = player.GetComponent<PlayerMovement>();
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        playerAnimator = player.GetComponent<Animator>();
        playerAttack = player.GetComponent<PlayerAttack>();

        playerSpine = playerMovement.SpineTransform;
        aimCameraTarget = playerMovement.AimCameraFollowTarget;

        maxXRotation = playerMovement.GetMaxXRotation();
        minXRotation = playerMovement.GetMinXRotation();
        maxYRotation = playerMovement.GetMaxYRotation();
        minYRotation = playerMovement.GetMinYRotation();
    }
    public void OnEnter()
    {
        isBlendEnd = false;
        canShot = true;
        CameraController.Instance.OnBlendFinish += OnBlendEnd;
        CameraController.Instance.ChangeToShotCam();
        startingRotation = playerSpine.transform.localRotation.eulerAngles;
        startingAimCameraRotation = aimCameraTarget.transform.localRotation.eulerAngles;

        rotationX = 0f;
        rotationY = 0f;

    }
    public void OnLogic()
    {
        
        if (isBlendEnd && canShot && !playerHealth.IsDead())
        {
            rotationX += Input.GetAxis("Mouse Y");
            rotationY += Input.GetAxis("Mouse X");

            rotationX = Mathf.Clamp(rotationX, minXRotation, maxXRotation);
            rotationY = Mathf.Clamp(rotationY, minYRotation, maxYRotation);

            if (Input.GetMouseButtonUp(0))
            {
                canShot = false;
                playerAttack.Recoil();
                playerAttack.OnArrowReleased();
                playerAnimator.SetTrigger("Shot");
                CameraController.Instance.ForwardAimCamera();
            }
        }
    }
    public void OnExit()
    {
        CameraController.Instance.OnBlendFinish -= OnBlendEnd;
        CameraController.Instance.StopAimCameraMovement();
        CameraController.Instance.ResetAimCamera();
        aimCameraTarget.localRotation = Quaternion.Euler(startingAimCameraRotation);
    }

    public void OnLateLogic()
    {
        if (isBlendEnd && canShot)
        {
            Quaternion targetRotation = Quaternion.Euler(startingRotation.x, startingRotation.y + rotationY, -(startingRotation.x + rotationX));
            playerSpine.localRotation = targetRotation;
            aimCameraTarget.rotation = playerSpine.rotation;
        }
    }

    private void OnBlendEnd()
    {
        isBlendEnd = true;
    }
}
