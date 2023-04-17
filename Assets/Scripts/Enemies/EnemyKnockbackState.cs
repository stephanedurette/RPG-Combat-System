using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackState : State
{
    private Timer timer;

    private Enemy enemy;

    private float knockBackTime;

    public EnemyKnockbackState(object owner)
    {
        enemy = owner as Enemy;
    }

    public void Setup(float knockBackTime)
    {
        this.knockBackTime = knockBackTime;
    }

    public override void OnEnter()
    {
        timer = new Timer(knockBackTime, enemy.OnDetectPlayer, enemy);
    }

    public override void OnExit()
    {
        timer.Stop();
    }

    public override void OnUpdate()
    {   
    }
}
