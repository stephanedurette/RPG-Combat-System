using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class EnemySlime : MonoBehaviour, IDamageTaker
{
    [SerializeField] internal float playerAggroDistance, playerAttackDistance;
    [SerializeField] internal float walkSpeed, runSpeed;
    [SerializeField] private HitData selfKnockback;

    [SerializeField] internal List<Transform> patrolTransforms;

    private PatrolState patrolState;
    internal ChaseState chaseState;
    private EnemyKnockbackState enemyKnockbackState;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private Health health;
    private Hitbox hitbox;

    private Vector2 lastMoveDirection = Vector2.zero;

    internal float currentSpeed;

    internal StateMachine stateMachine;

    private void Start()
    {
        patrolState = new PatrolState(this);
        chaseState = new ChaseState(this);
        enemyKnockbackState = new EnemyKnockbackState(this);

        stateMachine = new StateMachine(patrolState);
    }

    // Update is called once per frame
    private void Update()
    {

        stateMachine.OnUpdate();
    }

    internal bool IsPlayerInRange(float range)
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

    private void OnHealthEmpty(object sender, EventArgs e)
    {
        Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        hitbox = GetComponentInChildren<Hitbox>();

        health.OnHealthEmpty += OnHealthEmpty;
        hitbox.OnCollision += Hitbox_OnCollision;
    }

    private void Hitbox_OnCollision(object sender, Hitbox.OnCollisionEventArgs e)
    {
        //simulate getting hit but without the damage
        TakeDamage(e.collision.gameObject, selfKnockback);
    }

    private void OnDisable()
    {
        health.OnHealthEmpty -= OnHealthEmpty;
        hitbox.OnCollision -= Hitbox_OnCollision;
    }

    public void TakeDamage(GameObject source, HitData hitData)
    {
        health.ChangeHealth(-hitData.damage);

        rigidBody.velocity = (transform.position - source.transform.position).normalized * hitData.knockBackVelocity;
        enemyKnockbackState.Setup(hitData.knockBackTime, chaseState);
        stateMachine.SetState(enemyKnockbackState);
    }
}
