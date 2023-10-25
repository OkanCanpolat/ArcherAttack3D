using UnityEngine;
using UnityEngine.AI;

public class SoldierDieState : IEnemyState
{
    private Enemy enemy;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public SoldierDieState(Enemy enemy)
    {
        this.enemy = enemy;
        navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        animator = enemy.GetComponent<Animator>();
    }
    public void OnEnter()
    {
        navMeshAgent.enabled = false;
        animator.SetTrigger("Die");
    }

    public void OnExit()
    {
        
    }

    public void OnLogic()
    {
        
    }
}
