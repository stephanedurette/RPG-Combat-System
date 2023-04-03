using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private int startingStateIndex;
    [SerializeField] private List<State> states;

    int currentStateIndex = -1;

    private void Start()
    {
        SetState(startingStateIndex);
    }

    public void SetState(int newStateIndex, State.StateEnterArgs args = null)
    {
        if (currentStateIndex == newStateIndex) return;

        if (currentStateIndex != -1)
            states[currentStateIndex].OnExit();

        currentStateIndex = newStateIndex;

        states[currentStateIndex].OnEnter(args);
    }

    public void Update()
    {
        states[currentStateIndex].OnUpdate();
    }


}
