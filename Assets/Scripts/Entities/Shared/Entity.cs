using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(StateMachine))]

public abstract class Entity : MonoBehaviour, IDamageTaker
{
    [SerializeField] internal float walkSpeed = 10f;
    [SerializeField] internal LayerMask attackTargets;
    [SerializeField] internal Health health;
    [SerializeField] internal AttackAttributesSO attackAttributes;

    internal Vector2 lastMoveDirection = Vector2.zero;

    internal Rigidbody2D rigidBody;
    internal Animator animator;
    internal StateMachine stateMachine;
    
    public abstract void TakeDamage(Vector3 sourcePosition, AttackAttributesSO attackAttributes);
    internal void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
    }

    internal void Attack(float attackRadius, float attackDistance)
    {
        animator.SetTrigger("Attack");
        Singleton.Instance.SoundManager.Play(attackAttributes.attackSound);
        var rayCastHit = Physics2D.CircleCast(transform.position, attackRadius, lastMoveDirection, attackDistance, attackTargets);

        if (rayCastHit && rayCastHit.collider.gameObject.TryGetComponent(out IDamageTaker damageTaker))
        {
            Singleton.Instance.SoundManager.Play(attackAttributes.attackHitSound);
            damageTaker.TakeDamage(transform.position, attackAttributes);
        }
    }

    internal void OnEnable()
    {
        health.OnHealthEmpty += OnHealthEmpty;
    }
    internal void OnDisable()
    {
        health.OnHealthEmpty -= OnHealthEmpty;
    }

    internal abstract void OnHealthEmpty(object sender, System.EventArgs e);

}
