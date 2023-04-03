using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class State : MonoBehaviour
{
    public class StateEnterArgs
    {
        
    }


    public class KnockbackStateEnterArgs : StateEnterArgs
    {
        public float knockBackTime;
        public int returnState;
    }

    public abstract void OnEnter(StateEnterArgs args = null);

    public abstract void OnExit();

    public abstract void OnUpdate();
}
