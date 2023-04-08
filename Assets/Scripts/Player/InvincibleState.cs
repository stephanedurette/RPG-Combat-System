using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleState : ResponsiveState
{
    Timer timer;
    Player player;

    public InvincibleState(object owner) : base(owner)
    {
        player = owner as Player;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        timer = new Timer(player.invincibleTimeInSeconds, () => player.stateMachine.SetState(player.responsiveState), player);
        player.SetInvincibleEffect(true);
    }

    public override void OnExit()
    {
        base.OnExit();

        timer.Stop();
        player.SetInvincibleEffect(false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
