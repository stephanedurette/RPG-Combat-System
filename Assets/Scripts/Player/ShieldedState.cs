using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedState : ResponsiveState
{
    Timer timer;
    Player player;

    public ShieldedState(object owner) : base(owner)
    {
        player = owner as Player;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        timer = new Timer(player.shieldDuration, () => player.stateMachine.SetState(player.responsiveState), player);
        player.SetShield(true);
    }

    public override void OnExit()
    {
        base.OnExit();

        timer.Stop();
        player.SetShield(false);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
