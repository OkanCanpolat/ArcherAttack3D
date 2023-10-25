using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierPatrollState : IEnemyState
{
    private Enemy enemy;
    private Animator enemyAnimator;
    private NavMeshAgent enemyNavmesh;
    private Vector3 startingPos;
    private Vector3 destinationPosition;
    private Vector3 currentPath;
    private List<Vector3> paths = new List<Vector3>();
    private int pathIndex;
    private float reachOffset = 0.2f;
    public SoldierPatrollState(Enemy enemy, Transform destination)
    {
        this.enemy = enemy;
        enemyAnimator = enemy.GetComponent<Animator>();
        enemyNavmesh = enemy.GetComponent<NavMeshAgent>();
        startingPos = enemy.transform.position;
        destinationPosition = destination.position;
        paths.Add(destinationPosition);
        paths.Add(startingPos);
    }
    public void OnEnter()
    {
        enemyAnimator.SetBool("Walk", true);
        enemyNavmesh.isStopped = false;

        currentPath = paths[pathIndex];
        enemyNavmesh.SetDestination(currentPath);
    }

    public void OnExit()
    {
        enemyAnimator.SetBool("Walk", false);
        enemyNavmesh.isStopped = true;
    }

    public void OnLogic()
    {
        if (enemy.IsDead())
        {
            enemy.StateMachine.ChangeState(enemy.DieState);
        }

        if(enemyNavmesh.remainingDistance < reachOffset/*Vector3.Distance(enemy.transform.position, currentPath) < reachOffset*/)
        {
            enemy.StateMachine.ChangeState(enemy.IdleState);
            pathIndex++;

            if(pathIndex == paths.Count)
            {
                pathIndex = 0;
            }
        }

    }
}
