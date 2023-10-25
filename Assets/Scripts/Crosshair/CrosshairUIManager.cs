using System.Collections;
using UnityEngine;

public class CrosshairUIManager : MonoBehaviour
{
    public static CrosshairUIManager Instance;
    [SerializeField] private GameObject crosshairCanvas;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Health playerHealth;
    [Header ("Reticle")]
    [SerializeField] private RectTransform reticle;
    [SerializeField] private float reticleMaxSize;
    [SerializeField] private float reticleMinSize;
    [SerializeField] private float reticleSpeed;
    [SerializeField] private Material reticleFrameMaterial;
    [SerializeField] private Color originalReticleColor;
    private float reticleCurrentSize;
    private bool isAiming;

    [Header("Raycasy")]
    [SerializeField] private Transform reticleRayPosition;
    [SerializeField] private LayerMask castMask;
    private Vector2 raycastOrigin;
    private RaycastHit hit;
    private Camera raycastCam;

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

        playerAttack.OnAim += OnAim;
        playerAttack.OnRecoil += OnRecoil;
        reticleCurrentSize = reticleMinSize;
        raycastCam = Camera.main;
        raycastOrigin = GetRaycastOrigin();

        playerHealth.OnDie += OnPlayerDie;
    }

    private void OnAim()
    {
        crosshairCanvas.SetActive(true);
        raycastOrigin = GetRaycastOrigin();
        isAiming = true;
        StartCoroutine(ControlCrosshairSize());
        StartCoroutine(ControlRaycast());
    }
    private void OnRecoil()
    {
        crosshairCanvas.SetActive(false);
        isAiming = false;
    }

    private IEnumerator ControlCrosshairSize()
    {
        while (isAiming)
        {
            if (IsMoving())
            {
                reticleCurrentSize = Mathf.Lerp(reticleCurrentSize, reticleMaxSize, Time.deltaTime * reticleSpeed);
            }
            else
            {
                reticleCurrentSize = Mathf.Lerp(reticleCurrentSize, reticleMinSize, Time.deltaTime * reticleSpeed);
            }

            reticle.sizeDelta = new Vector2(reticleCurrentSize, reticleCurrentSize);
            yield return null;
        }
    }

    private IEnumerator ControlRaycast()
    {
        while (isAiming)
        {
            yield return null;
            RayCast();
        }
    }

    private bool IsMoving()
    {
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            return true;
        }

        return false;
    }

    private void RayCast()
    {
        Ray ray = raycastCam.ScreenPointToRay(raycastOrigin);

        if(Physics.Raycast(ray, out hit, float.MaxValue, castMask))
        {
            CrosshairTarget target = hit.transform.root.GetComponent<CrosshairTarget>();
            Color color = target.Color;
            ChangeReticleColor(color);
        }

        else
        {
            ChangeReticleColor(originalReticleColor);
        }
    }

    private Vector2 GetRaycastOrigin()
    {
        Vector2 correctScreenPos = RectTransformUtility.PixelAdjustPoint(reticle.transform.position, reticle, crosshairCanvas.GetComponent<Canvas>());
        return correctScreenPos;
    }
    private void ChangeReticleColor(Color color)
    {
        reticleFrameMaterial.color = color;
    }

    public Vector3 GetRayDirection()
    {
        Ray ray = raycastCam.ScreenPointToRay(raycastOrigin);
        return ray.direction;
    }

    public void OnPlayerDie()
    {
        crosshairCanvas.SetActive(false);
        isAiming = false;
    }
}
