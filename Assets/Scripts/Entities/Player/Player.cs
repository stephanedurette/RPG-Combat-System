using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity, IDamageTaker
{
    public override void TakeDamage(Vector3 sourcePosition, AttackAttributesSO attackAttributes)
    {
        health.ChangeHealth(-attackAttributes.damage);

        rigidBody.velocity = (transform.position - sourcePosition).normalized * attackAttributes.knockBackVelocity;
        stateMachine.SetState(1, new State.KnockbackStateEnterArgs(){ knockBackTime = attackAttributes.knockBackTime, returnState = 0});
    }

    internal override void OnHealthEmpty(object sender, EventArgs e)
    {
        FindObjectOfType<GameOverPanel>().TogglePanel(true);
        gameObject.SetActive(false);
    }
}
