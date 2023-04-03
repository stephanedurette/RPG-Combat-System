using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : State
{
    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rigidBody;

    public override void OnEnter(StateEnterArgs args = null)
    {
        InputManager.OnAttackPressed += OnAttackPressed;
    }

    public override void OnExit()
    {
        InputManager.OnAttackPressed -= OnAttackPressed;
    }

    public override void OnUpdate()
    {
        var normalizedInputVector = InputManager.MoveVector.normalized;

        player.lastMoveDirection = normalizedInputVector == Vector2.zero ? player.lastMoveDirection : normalizedInputVector;

        animator.SetBool("IsMoving", normalizedInputVector != Vector2.zero);
        animator.SetFloat("X Motion", player.lastMoveDirection.x);
        animator.SetFloat("Y Motion", player.lastMoveDirection.y);

        rigidBody.velocity = normalizedInputVector * player.walkSpeed;
    }

    private void OnAttackPressed(object sender, System.EventArgs e)
    {
        float attackRadius = 1f;
        float attackDistance = 2.5f;
        player.Attack(attackRadius, attackDistance);
    }
}
