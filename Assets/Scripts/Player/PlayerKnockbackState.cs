using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : State
{
    Timer timer;
    private Player player;

    private float knockBackTime;

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
        timer = new Timer(knockBackTime, () => player.stateMachine.SetState(player.invincibleState), player);
        player.SetInvincibleEffect(true);
    }

    public override void OnExit()
    {
        timer.Stop();
        player.SetInvincibleEffect(false);
    }

    public override void OnUpdate()
    {

    }
}
