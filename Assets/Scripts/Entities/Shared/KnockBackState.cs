using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackState : State
{
    [SerializeField] private Entity entity;

    private float knockBackTime;
    private int stateIndexToExit;
    private float currentTimer = 0f;

    public void SetupTimer(float knockBackTime, int stateIndexToExit)
    {
        this.knockBackTime = knockBackTime;
        this.stateIndexToExit = stateIndexToExit;
    }

    public override void OnEnter(StateEnterArgs args = null)
    {
        currentTimer = 0f;

        this.knockBackTime = ((State.KnockbackStateEnterArgs)args).knockBackTime;
        this.stateIndexToExit = ((State.KnockbackStateEnterArgs)args).returnState;
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        
        currentTimer += Time.deltaTime;
        if (currentTimer >= knockBackTime)
        {
            entity.stateMachine.SetState(stateIndexToExit);
        }
        
    }
}
