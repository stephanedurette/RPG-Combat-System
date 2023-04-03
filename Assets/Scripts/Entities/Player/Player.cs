using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, IDamageTaker
{
    [SerializeField] private float walkSpeed = 10f;

    private Vector2 lastMoveDirection = Vector2.zero;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private Health health;

    private void Start()
    {

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
        InputManager.OnAttackPressed += OnAttackPressed;
    }

    private void OnAttackPressed(object sender, EventArgs e)
    {
        Attack();
    }

    internal void OnDisable()
    {
        health.OnHealthEmpty -= OnHealthEmpty;
        InputManager.OnAttackPressed -= OnAttackPressed;
    }


    private void OnHealthEmpty(object sender, EventArgs e)
    {
        FindObjectOfType<GameOverPanel>().TogglePanel(true);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        var normalizedInputVector = InputManager.MoveVector.normalized;

        lastMoveDirection = normalizedInputVector == Vector2.zero ? lastMoveDirection : normalizedInputVector;

        animator.SetBool("IsMoving", normalizedInputVector != Vector2.zero);
        animator.SetFloat("X Motion", lastMoveDirection.x);
        animator.SetFloat("Y Motion", lastMoveDirection.y);

        rigidBody.velocity = normalizedInputVector * walkSpeed;
    }

    public void TakeDamage(UnityEngine.Object source, HitData hitData)
    {
        Debug.Log("taking damage");
    }
}
