using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : Enemy
{
    internal Hitbox hitbox;

    internal ChaseState chaseState;
    internal override void Start()
    {
        base.Start();
        chaseState = new ChaseState(this);
    }

    internal override void OnEnable()
    {
        base.OnEnable();
        hitbox = GetComponentInChildren<Hitbox>();

        hitbox.OnHitboxHit += Hitbox_OnCollision;
    }

    private void Hitbox_OnCollision(object sender, Hitbox.OnHitboxHitEventArgs e)
    {
        var knockBackVelocity = 30f;
        var knockBackTime = .2f;

        rigidBody.velocity = (transform.position - e.collision.transform.position).normalized * knockBackVelocity;
        enemyKnockbackState.Setup(knockBackTime);
        stateMachine.SetState(enemyKnockbackState);
    }

    internal override void OnDetectPlayer()
    {
        stateMachine.SetState(chaseState);
    }

    internal override void OnDisable()
    {
        base.OnDisable();

        hitbox.OnHitboxHit -= Hitbox_OnCollision;
    }
}
