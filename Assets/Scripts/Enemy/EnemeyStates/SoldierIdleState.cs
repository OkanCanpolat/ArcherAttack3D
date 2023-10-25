using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierIdleState : IEnemyState
{
    private Enemy enemy;
    private float idleTime;
    private float elapsedTime;
    public SoldierIdleState(Enemy enemy, float idleTime)
    {
        this.enemy = enemy;
        this.idleTime = idleTime;
    }

    public void OnEnter()
    {
        elapsedTime = 0;
    }

    public void OnExit()
    {
        
    }

    public void OnLogic()
    {
        if (enemy.IsDead())
        {
            enemy.StateMachine.ChangeState(enemy.DieState);
        }

        elapsedTime += Time.deltaTime;

        if(elapsedTime >= idleTime)
        {
            enemy.StateMachine.ChangeState(enemy.PatrollState);
        }
    }
}
