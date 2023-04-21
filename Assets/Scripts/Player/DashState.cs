using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    Timer timer;
    Timer trailTimer;
    Timer dashCooldownTimer;

    float timeSinceDashStarted;
    Player player;

    public DashState(object owner)
    {
        player = owner as Player;

        player.trailRenderer.enabled = false;
    }

    public override void OnEnter()
    {
        float timeForTrailEffect = 0.3f;

        timer = new Timer(player.dashTime, () => player.stateMachine.SetState(player.responsiveState), player);

        player.trailRenderer.enabled = true;
        trailTimer = new Timer(timeForTrailEffect, () => player.trailRenderer.enabled = false, player);

        timeSinceDashStarted = 0f;
        player.canDash = false;
    }

    public override void OnExit()
    {
        timer.Stop();
        dashCooldownTimer = new Timer(player.dashCooldown, () => player.canDash = true, player);
    }

    public override void OnUpdate()
    {
        timeSinceDashStarted += Time.deltaTime;
        float dashSpeed = player.dashSpeedOverTime.Evaluate(timeSinceDashStarted / player.dashTime) * player.dashSpeed;
        player.rigidBody.velocity = player.lastMoveDirection * dashSpeed;
    }
}
