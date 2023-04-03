using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    State currentState = null;

    public StateMachine(State startingState)
    {
        SetState(startingState);
    }

    public void SetState(State newState)
    {
        if (currentState == newState) return;

        if (currentState != null)
            currentState.OnExit();

        currentState = newState;

        currentState.OnEnter();
    }

    public void OnUpdate()
    {
        currentState.OnUpdate();
    }


}
