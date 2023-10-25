using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    public event Action OnBlendFinish;
    [SerializeField] private CinemachineVirtualCamera movementCamera;
    [SerializeField] private CinemachineVirtualCamera IdleCamera;
    [SerializeField] private CinemachineVirtualCamera shotCamera;
    private CinemachineBrain cinemachineBrain;
    private const int lowPriority = 10;
    private const int highPriority = 20;
    private float aimCameraForwardSpeed = 2;
    private float aimCameraForwardAmount = -1f;
    private Vector3 aimCameraFollowOffset;
    private Vector3 aimCameraAimOffset;

    private Coroutine aimCameraMovementCoroutine;
    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        CinemachineTransposer transposer = shotCamera.GetCinemachineComponent<CinemachineTransposer>();
        CinemachineComposer composer = shotCamera.GetCinemachineComponent<CinemachineComposer>();
        aimCameraAimOffset = composer.m_TrackedObjectOffset;
        aimCameraFollowOffset = transposer.m_FollowOffset;

    }
    public void ChangeToMovementCam()
    {
        IdleCamera.gameObject.SetActive(false);
        shotCamera.gameObject.SetActive(false);
        movementCamera.gameObject.SetActive(true);
        StartCoroutine(ControlBlend());
    }
    public void ChangeToIdleCam()
    {
        shotCamera.gameObject.SetActive(false);
        movementCamera.gameObject.SetActive(false);
        IdleCamera.gameObject.SetActive(true);
        StartCoroutine(ControlBlend());
    }
    public void ChangeToShotCam()
    {
        movementCamera.gameObject.SetActive(false);
        IdleCamera.gameObject.SetActive(false);
        shotCamera.gameObject.SetActive(true);
        StartCoroutine(ControlBlend());
    }

    private IEnumerator ControlBlend()
    {
        float blendTime = cinemachineBrain.m_DefaultBlend.m_Time;
        yield return new WaitForSeconds(blendTime);
        OnBlendFinish?.Invoke();
    }

    public void ForwardAimCamera()
    {
        aimCameraMovementCoroutine = StartCoroutine(TranslateAimCamera());
    }
    private IEnumerator TranslateAimCamera()
    {
        CinemachineTransposer transposer = shotCamera.GetCinemachineComponent<CinemachineTransposer>();
        float startingX = transposer.m_FollowOffset.x;
        float finalX = startingX + aimCameraForwardAmount;
        float time = 0;

        while(time < 1)
        {
            transposer.m_FollowOffset.x = Mathf.Lerp(startingX, finalX, time);
            time += Time.deltaTime * aimCameraForwardSpeed;
            yield return null;
        }
    }
    public void StopAimCameraMovement()
    {
        StopCoroutine(aimCameraMovementCoroutine);
    }
    public void ResetAimCamera()
    {
        CinemachineComposer composer = shotCamera.GetCinemachineComponent<CinemachineComposer>();
        CinemachineTransposer transposer = shotCamera.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = aimCameraFollowOffset;
        composer.m_TrackedObjectOffset = aimCameraAimOffset;
    }

    public void Test()
    {
        if((movementCamera.Priority < shotCamera.Priority) || (movementCamera.Priority < IdleCamera.Priority))
        {
            Debug.Log("SOMETHÝNG WRONG");
        }
    }
}
