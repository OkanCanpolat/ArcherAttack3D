using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public event Action OnAim;
    public event Action OnRecoil;
    public event Action OnArrowFinish;
    public event Action OnEnemyFinish;
    public event Action<int> OnArrowCountChanged;
    [SerializeField] private Bow currentBowMesh;
    [SerializeField] private GameObject currentArrowMesh;
    [SerializeField] private GameObject currentArrowPrefab;
    [SerializeField] private Transform rightHand;
    private Player player;
    private Vector3 stringStartPosition;

    private int[] array;
    private void Awake()
    {
        player = GetComponent<Player>();
        stringStartPosition = currentBowMesh.String.transform.localPosition;
    }
    private void Start()
    {
        OnArrowCountChanged?.Invoke(currentBowMesh.CurrentArrowCount);
    }
    public void PullString()
    {
        GameObject bowString = currentBowMesh.String;
        bowString.transform.SetParent(rightHand);
    }
    public void OnArrowPulled()
    {
        currentArrowMesh.SetActive(true);
    }

    public void OnArrowReleased()
    {
        GameObject bowString = currentBowMesh.String;
        bowString.transform.SetParent(currentBowMesh.transform);
        currentBowMesh.String.transform.localPosition = stringStartPosition;
        currentArrowMesh.SetActive(false);
        CreateArrow();
    }
    private void CreateArrow()
    {
        GameObject obj = Instantiate(currentArrowPrefab);
        obj.transform.parent = currentArrowMesh.transform.parent;
        obj.transform.localPosition = currentArrowMesh.transform.localPosition;
        obj.transform.localRotation = currentArrowMesh.transform.localRotation;
        obj.transform.parent = null;
        Arrow arrow = obj.GetComponent<Arrow>();
        Vector3 direction = CrosshairUIManager.Instance.GetRayDirection();
        arrow.Init(direction);
        arrow.onCollide += OnArrowCollide;
        arrow.onNotCollide += OnArrowNotCollide;
    }
    public void Aim()
    {
        OnAim?.Invoke();
    }
    public void Recoil()
    {
        OnRecoil?.Invoke();
        currentBowMesh.CurrentArrowCount--;
        OnArrowCountChanged?.Invoke(currentBowMesh.CurrentArrowCount);
    }

    private void OnArrowCollide()
    {
        if (WaypointManager.Instance.ThereIsEnemy())
        {
            ControlArrow();
        }

        else
        {
            ControlWaypoint();
        }
    }
    private void OnArrowNotCollide()
    {
        ControlArrow();
    }

    private void ControlArrow()
    {
        if (currentBowMesh.CurrentArrowCount > 0)
        {
            player.StateMachine.ChangeState(player.ReadyAttackState);
        }

        else
        {
            OnArrowFinish?.Invoke();
        }
    }
    private void ControlWaypoint()
    {
        if (WaypointManager.Instance.ThereIsWaypoint())
        {
            if(currentBowMesh.CurrentArrowCount > 0)
            {
                player.StateMachine.ChangeState(player.MovementState);
            }
            else
            {
                OnArrowFinish?.Invoke();
            }
        }

        else
        {
            OnEnemyFinish?.Invoke();
        }
    }
}
