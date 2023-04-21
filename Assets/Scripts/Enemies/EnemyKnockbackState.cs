using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackState : State
{
    private Timer timer;

    private Enemy enemy;

    private float hitKnockBackTime;
    private float deathKnockBackTime = .5f;

    private bool hasBeenHit;
    private bool hasBeenKilled;

    float timeSinceKnockedBack;
    public EnemyKnockbackState(object owner)
    {
        enemy = owner as Enemy;
    }

    public void Setup(float knockBackTime, bool hasBeenHit)
    {
        hasBeenKilled = enemy.health.CurrentValue <= 0;

        this.hitKnockBackTime = knockBackTime;
        this.hasBeenHit = hasBeenHit;

    }

    public override void OnEnter()
    {
        

        if (hasBeenKilled)
        {
            timer = new Timer(deathKnockBackTime, () => GameObject.Destroy(enemy.gameObject), enemy);
        } else
        {
            timer = new Timer(hitKnockBackTime, enemy.OnDetectPlayer, enemy);
        }

        timeSinceKnockedBack = 0f;
    }

    public override void OnExit()
    {
        timer.Stop();
        enemy.spriteRenderer.material.SetFloat("_FlashOpacity", 0f);
    }

    public override void OnUpdate()
    {
        if (!hasBeenHit) return;

        timeSinceKnockedBack += Time.deltaTime;

        enemy.spriteRenderer.material.SetFloat("_FlashOpacity", enemy.knockBackFlashOpacity.Evaluate(timeSinceKnockedBack / hitKnockBackTime));

        if (!hasBeenKilled) return;

        enemy.spriteRenderer.material.SetFloat("_Dissolve", 1 - timeSinceKnockedBack / deathKnockBackTime);

    }
}
