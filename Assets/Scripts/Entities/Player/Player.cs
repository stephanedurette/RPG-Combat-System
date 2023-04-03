using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, IDamageTaker
{
    [SerializeField] internal float walkSpeed = 10f;

    internal Vector2 lastMoveDirection = Vector2.zero;

    internal Rigidbody2D rigidBody;
    internal Animator animator;
    private Health health;

    private StateMachine stateMachine;

    private ResponsiveState responsiveState;
    private void Start()
    {
        responsiveState = new ResponsiveState(this);

        stateMachine = new StateMachine(responsiveState);
    }

    private void Attack()
    {
        Debug.Log("attacking");
    }

    internal void OnEnable()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        health.OnHealthEmpty += OnHealthEmpty;
    }

    internal void OnAttackPressed(object sender, EventArgs e)
    {
        Attack();
    }

    internal void OnDisable()
    {
        health.OnHealthEmpty -= OnHealthEmpty;
    }


    private void OnHealthEmpty(object sender, EventArgs e)
    {
        FindObjectOfType<GameOverPanel>().TogglePanel(true);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        stateMachine.OnUpdate();
    }

    public void TakeDamage(UnityEngine.Object source, HitData hitData)
    {
        Debug.Log("taking damage");
    }
}
