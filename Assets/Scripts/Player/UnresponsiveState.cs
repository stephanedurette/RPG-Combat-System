using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnresponsiveState : State
{
    Player player;

    public UnresponsiveState(object owner)
    {
        player = owner as Player;
    }

    private void EnableAllColliders(bool enabled)
    {
        foreach (Hurtbox hurtbox in player.hurtboxes)
            hurtbox.gameObject.SetActive(enabled);

        player.GetComponent<Collider2D>().enabled = enabled;
    }

    public override void OnEnter()
    {
        player.rigidBody.velocity = Vector2.zero;

        player.lastMoveDirection = Vector2.down;

        player.animator.SetBool("IsMoving", false);
        player.animator.SetFloat("X Motion", 0f);
        player.animator.SetFloat("Y Motion", 0f);

        EnableAllColliders(false);
    }

    public override void OnExit()
    {
        EnableAllColliders(true);
    }

    public override void OnUpdate()
    {
    }
}
