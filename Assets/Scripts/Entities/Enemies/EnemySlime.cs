using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : Entity, IDamageTaker
{
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float playerAggroDistance, playerAttackDistance;

    private float currentSpeed;

    private bool hasAggro = false;
    private bool knockedBack = false;

    public bool KnockedBack { set { knockedBack = value; } }

    public enum Speed
    {
        Walk,
        Run,
        Stop
    }

    public float PlayerAttackDistance => playerAttackDistance;


    // Update is called once per frame
    private void Update()
    {
        if (knockedBack) return;

        if (FindObjectOfType<Player>() == null)
        {
            stateMachine.SetState(0);
        } else if (IsPlayerInRange(playerAttackDistance))
        {
            stateMachine.SetState(2);
        } else if (IsPlayerInRange(playerAggroDistance))
        {
            stateMachine.SetState(1);
            hasAggro = true;
        } else if (hasAggro)
        {
            stateMachine.SetState(1);
        }
    }

    private bool IsPlayerInRange(float range)
    {
        var player = FindObjectOfType<Player>();
        if (player == null) return false;
        
        float distanceFromPlayer = (FindObjectOfType<Player>().transform.position - transform.position).magnitude;

        return distanceFromPlayer <= range;

    }

    public void SetMovement(Vector2 targetPosition)
    {
        var normalizedInputVector = (targetPosition - (Vector2)transform.position).normalized;

        lastMoveDirection = normalizedInputVector == Vector2.zero ? lastMoveDirection : normalizedInputVector;

        animator.SetBool("IsMoving", normalizedInputVector != Vector2.zero);
        animator.SetFloat("X Motion", lastMoveDirection.x);
        animator.SetFloat("Y Motion", lastMoveDirection.y);

        rigidBody.velocity = normalizedInputVector * currentSpeed;
    }

    public void SetSpeed(Speed speed)
    {
        switch (speed)
        {
            case (Speed.Stop):
                currentSpeed = 0f;
                break;
            case (Speed.Walk):
                currentSpeed = walkSpeed;
                break;
            case (Speed.Run):
                currentSpeed = runSpeed;
                break;
            default:
                break;
        }
    }

    public override void TakeDamage(Vector3 sourcePosition, AttackAttributesSO attackAttributes)
    {
        health.ChangeHealth(-attackAttributes.damage);

        rigidBody.velocity = (transform.position - sourcePosition).normalized * attackAttributes.knockBackVelocity;
        knockedBack = true;
        stateMachine.SetState(3, new State.KnockbackStateEnterArgs() { knockBackTime = attackAttributes.knockBackTime, returnState = 1 });
    }

    internal override void OnHealthEmpty(object sender, EventArgs e)
    {
        Destroy(this.gameObject);
    }
}
