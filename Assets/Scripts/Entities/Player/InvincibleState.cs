using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleState : ResponsiveState
{
    Player player;

    private float currentTime;

    public InvincibleState(object owner) : base(owner)
    {
        player = owner as Player;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        currentTime = 0f;
        player.col.enabled = false;
    }

    public override void OnExit()
    {
        base.OnExit();

        player.col.enabled = true;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        currentTime += Time.deltaTime;

        if (currentTime >= player.invincibleTimeInSeconds)
        {
            player.stateMachine.SetState(player.responsiveState);
        }
    }
}
