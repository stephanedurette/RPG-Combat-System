using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : State
{
    private Player player;

    private float knockBackTime;

    private float currentTime;

    public PlayerKnockbackState(object owner)
    {
        player = owner as Player;
    }

    public void Setup(float knockBackTime)
    {
        this.knockBackTime = knockBackTime;
    }

    public override void OnEnter()
    {
        currentTime = 0f;
        player.SetInvincibleEffect(true);
    }

    public override void OnExit()
    {
        player.SetInvincibleEffect(false);
    }

    public override void OnUpdate()
    {

        currentTime += Time.deltaTime;
        if (currentTime >= knockBackTime)
        {
            player.stateMachine.SetState(player.invincibleState);
        }
        
    }
}
