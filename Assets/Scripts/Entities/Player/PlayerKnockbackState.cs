using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockbackState : State
{
    private Player player;

    private float knockBackTime;
    private State stateToReturn;

    private float currentTime;

    public PlayerKnockbackState(object owner)
    {
        player = owner as Player;
    }

    public void Setup(float knockBackTime, State stateToReturn)
    {
        this.knockBackTime = knockBackTime;
        this.stateToReturn = stateToReturn;
    }

    public override void OnEnter()
    {
        currentTime = 0f;   
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {

        currentTime += Time.deltaTime;
        if (currentTime >= knockBackTime)
        {
            player.stateMachine.SetState(stateToReturn);
        }
        
    }
}
