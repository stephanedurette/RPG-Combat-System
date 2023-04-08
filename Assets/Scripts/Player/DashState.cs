using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    Timer timer;
    float timeSinceDashStarted;
    Player player;

    public DashState(object owner)
    {
        player = owner as Player;
    }

    public override void OnEnter()
    {
        timer = new Timer(player.dashTime, () => player.stateMachine.SetState(player.responsiveState), player);
        timeSinceDashStarted = 0f;
        player.canDash = false;
    }

    public override void OnExit()
    {
        timer.Stop();
        timer = new Timer(player.dashCooldown, () => player.canDash = true, player);
    }

    public override void OnUpdate()
    {
        timeSinceDashStarted += Time.deltaTime;
        float dashSpeed = player.dashSpeedOverTime.Evaluate(timeSinceDashStarted / player.dashTime) * player.dashSpeed;
        player.rigidBody.velocity = player.lastMoveDirection * dashSpeed;
    }
}
