using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackState : State
{
    private EnemySlime enemySlime;

    private float knockBackTime;
    private State stateToReturn;

    private float currentTime;

    public EnemyKnockbackState(object owner)
    {
        enemySlime = owner as EnemySlime;
    }

    public void Setup(float knockBackTime)
    {
        this.knockBackTime = knockBackTime;
    }

    public override void OnEnter()
    {
        currentTime = 0f;   
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {

        currentTime += Time.deltaTime;
        if (currentTime >= knockBackTime)
        {
            enemySlime.stateMachine.SetState(enemySlime.chaseState);
        }
        
    }
}
