using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    [SerializeField] private EnemySlime enemySlime;

    public override void OnEnter(StateEnterArgs args = null)
    {
        enemySlime.SetSpeed(EnemySlime.Speed.Run);
        enemySlime.KnockedBack = false;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        enemySlime.SetMovement(FindObjectOfType<Player>().transform.position);
    }
}
