using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class EnemySlime : MonoBehaviour
{
    [SerializeField] internal float playerAggroDistance, playerAttackDistance;
    [SerializeField] internal float walkSpeed, runSpeed;
    [SerializeField] private Hurtbox hurtbox;
    [SerializeField] private CollectionSO health;

    [SerializeField] internal List<Transform> patrolTransforms;

    private PatrolState patrolState;
    internal ChaseState chaseState;
    private EnemyKnockbackState enemyKnockbackState;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private Hitbox hitbox;

    private Vector2 lastMoveDirection = Vector2.zero;

    internal float currentSpeed;

    internal StateMachine stateMachine;

    private void Start()
    {
        //create an instance of the health SO
        health = health.Copy();

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
        hitbox = GetComponentInChildren<Hitbox>();

        hitbox.OnHitboxHit += Hitbox_OnCollision;
        hurtbox.OnHurtboxHit += Hurtbox_OnHurtboxHit;
    }

    private void Hurtbox_OnHurtboxHit(object sender, Hurtbox.OnHurtboxHitEventArgs e)
    {
        health.CurrentValue -= e.hitData.damage;

        rigidBody.velocity = (transform.position - e.other.transform.position).normalized * e.hitData.knockBackVelocity;
        enemyKnockbackState.Setup(e.hitData.knockBackTime);
        stateMachine.SetState(enemyKnockbackState);
    }

    private void Hitbox_OnCollision(object sender, Hitbox.OnHitboxHitEventArgs e)
    {
        var knockBackVelocity = 30f;
        var knockBackTime = .2f;

        rigidBody.velocity = (transform.position - e.collision.transform.position).normalized * knockBackVelocity;
        enemyKnockbackState.Setup(knockBackTime);
        stateMachine.SetState(enemyKnockbackState);
    }

    private void OnDisable()
    {
        hitbox.OnHitboxHit -= Hitbox_OnCollision;
        hurtbox.OnHurtboxHit -= Hurtbox_OnHurtboxHit;
    }
}
