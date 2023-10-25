
using UnityEngine;

public class SoldierFireState : IEnemyState
{
    private Enemy enemy;
    private Player player;
    private Health health;
    private Animator enemyAnimator;
    private float timeBetweenFires;
    private float elapsedLastFireTime;
    public SoldierFireState(Enemy enemy, float timeBetweenFires)
    {
        this.enemy = enemy;
        enemyAnimator = enemy.GetComponent<Animator>();
        player = GameManager.Instance.Player;
        health = player.GetComponent<Health>();
        this.timeBetweenFires = timeBetweenFires;
    }
    public void OnEnter()
    {
        enemyAnimator.SetBool("Fire", true);
    }

    public void OnExit()
    {
        enemyAnimator.SetBool("Fire", false);
    }

    public void OnLogic()
    {
        if (enemy.IsDead())
        {
            enemy.StateMachine.ChangeState(enemy.DieState);
        }

        if (GameManager.Instance.IsGameOver())
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
        }

        elapsedLastFireTime += Time.deltaTime;

        if(elapsedLastFireTime >= timeBetweenFires)
        {
            health.TakeDamage();
            elapsedLastFireTime = 0;
            enemy.OnFire();
        }
    }

    
}
