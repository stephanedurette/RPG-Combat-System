using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsiveState : State
{
    Player player;

    public ResponsiveState(object owner)
    {
        player = owner as Player;
    }

    public override void OnEnter()
    {

        InputManager.OnAttackPressed += player.OnAttackPressed;
        InputManager.OnDashPressed += player.OnDashPressed;
        InputManager.OnShieldPressed += player.OnShieldPressed;
    }

    public override void OnExit()
    {

        InputManager.OnAttackPressed -= player.OnAttackPressed;
        InputManager.OnDashPressed -= player.OnDashPressed;
        InputManager.OnShieldPressed -= player.OnShieldPressed;
    }

    public override void OnUpdate()
    {
        var normalizedInputVector = InputManager.MoveVector.normalized;

        player.lastMoveDirection = normalizedInputVector == Vector2.zero ? player.lastMoveDirection : normalizedInputVector;

        player.animator.SetBool("IsMoving", normalizedInputVector != Vector2.zero);
        player.animator.SetFloat("X Motion", player.lastMoveDirection.x);
        player.animator.SetFloat("Y Motion", player.lastMoveDirection.y);

        player.rigidBody.velocity = normalizedInputVector * player.walkSpeed;
    }
}
