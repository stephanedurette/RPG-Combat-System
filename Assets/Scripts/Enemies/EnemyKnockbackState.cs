using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockbackState : State
{
    private Timer timer;

    private Enemy enemy;

    private float knockBackTime;
    private bool hasBeenHit;

    float timeSinceKnockedBack;
    public EnemyKnockbackState(object owner)
    {
        enemy = owner as Enemy;
    }

    public void Setup(float knockBackTime, bool hasBeenHit = true)
    {
        this.knockBackTime = knockBackTime;
        this.hasBeenHit = hasBeenHit;
    }

    public override void OnEnter()
    {
        timer = new Timer(knockBackTime, enemy.OnDetectPlayer, enemy);
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
        enemy.spriteRenderer.material.SetFloat("_FlashOpacity", enemy.knockBackFlashOpacity.Evaluate(timeSinceKnockedBack / knockBackTime));
    }
}
