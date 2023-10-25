using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStateMachine StateMachine;
    public SoldierIdleState IdleState;
    public SoldierPatrollState PatrollState;
    public SoldierFireState FireState;
    public SoldierDieState DieState;
    [Header("Movement Properties")]
    [SerializeField] private float idleTime;
    [SerializeField] private Transform patrollDestination;
    [SerializeField] private float rotationSpeed;
    [Header("Attack Properties")]
    [SerializeField] private float timeBetweenFires;
    [SerializeField] private Transform muzzlePosition;
    [SerializeField] private MuzzleShotObjectPool pool;
    [Header("Health")]
    private bool isDead;

    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        PatrollState = new SoldierPatrollState(this, patrollDestination);
        IdleState = new SoldierIdleState(this, idleTime);
        FireState = new SoldierFireState(this, timeBetweenFires);
        DieState = new SoldierDieState(this);

        StateMachine.ChangeState(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.OnLogic();
    }
    public virtual void TakeDamage()
    {
        isDead = true;
        WaypointManager.Instance.OnEnemyDied(this);
    }
    public bool IsDead()
    {
        return isDead;
    }
    public void AlertEnemy()
    {
        StateMachine.ChangeState(FireState);
        TurnToPlayer();
    }
    public void TurnToPlayer()
    {
        StartCoroutine(FaceToTarget());
    }
    public void OnFire()
    {
        GameObject muzzleShot = pool.Get();
        muzzleShot.transform.position = muzzlePosition.position;
        muzzleShot.SetActive(true);
    }
    private IEnumerator FaceToTarget()
    {
        Player player = GameManager.Instance.Player;
        Quaternion lookRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        float time = 0;

        while(time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime / 2;
            yield return null;
        }
    }
    
}
