using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackState : State
{
    private Timer timer;

    private EnemySlime enemySlime;

    private float knockBackTime;

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
        timer = new Timer(knockBackTime, () => enemySlime.stateMachine.SetState(enemySlime.chaseState), enemySlime);
    }

    public override void OnExit()
    {
        timer.Stop();
    }

    public override void OnUpdate()
    {   
    }
}
