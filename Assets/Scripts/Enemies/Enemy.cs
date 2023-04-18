using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] internal float playerAggroDistance;
    [SerializeField] internal float walkSpeed, runSpeed;
    [SerializeField] internal Hurtbox hurtbox;
    [SerializeField] internal CollectionSO health;
    [SerializeField] internal SpriteRenderer spriteRenderer;
    [SerializeField] internal Color reColor;
    [SerializeField] internal List<Transform> patrolTransforms;

    internal PatrolState patrolState;
    internal EnemyKnockbackState enemyKnockbackState;

    internal Rigidbody2D rigidBody;
    internal Animator animator;

    internal Vector2 lastMoveDirection = Vector2.zero;

    internal bool canDetectPlayer = true;

    internal float currentSpeed;

    internal StateMachine stateMachine;
    // Start is called before the first frame update
    internal virtual void Start()
    {
        //create an instance of the health SO
        health = health.Copy();

        spriteRenderer.material = new Material(spriteRenderer.material);
        spriteRenderer.material.SetColor("_ReferenceColor", reColor);

        patrolState = new PatrolState(this);
        enemyKnockbackState = new EnemyKnockbackState(this);

        stateMachine = new StateMachine(patrolState);
    }

    internal virtual void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        hurtbox.OnHurtboxHit += Hurtbox_OnHurtboxHit;
    }

    private void Hurtbox_OnHurtboxHit(object sender, Hurtbox.OnHurtboxHitEventArgs e)
    {
        health.CurrentValue -= e.hitData.damage;

        rigidBody.velocity = (transform.position - e.other.transform.position).normalized * e.hitData.knockBackVelocity;
        enemyKnockbackState.Setup(e.hitData.knockBackTime);
        stateMachine.SetState(enemyKnockbackState);
    }

    internal virtual void OnDisable()
    {
        hurtbox.OnHurtboxHit -= Hurtbox_OnHurtboxHit;
    }

    private void Update()
    {
        stateMachine.OnUpdate();
    }

    public void ReturnToPatrolState()
    {
        stateMachine.SetState(patrolState);
        float playerDetectTimeoutLength = 2f;
        TurnOffDetectionForSeconds(playerDetectTimeoutLength);
    }

    internal virtual void OnDetectPlayer()
    {

    }

    internal void SetMovement(Vector2 targetPosition)
    {
        var normalizedInputVector = (targetPosition - (Vector2)transform.position).normalized;

        lastMoveDirection = normalizedInputVector == Vector2.zero ? lastMoveDirection : normalizedInputVector;

        animator.SetBool("IsMoving", normalizedInputVector != Vector2.zero);
        animator.SetFloat("X Motion", lastMoveDirection.x);
        animator.SetFloat("Y Motion", lastMoveDirection.y);

        rigidBody.velocity = normalizedInputVector * currentSpeed;
    }

    public void TurnOffDetectionForSeconds(float seconds)
    {
        canDetectPlayer = false;
        Timer t = new Timer(seconds, () => canDetectPlayer = true, this);
    }

    public static bool IsPlayerInRange(float range, Vector2 position)
    {
        var player = FindObjectOfType<Player>();
        if (player == null) return false;

        float distanceFromPlayer = ((Vector2)player.transform.position - position).magnitude;

        return distanceFromPlayer <= range;

    }
}
