using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    private EnemySlime enemySlime;

    public ChaseState(object owner)
    {
        enemySlime = owner as EnemySlime;
    }

    public override void OnEnter()
    {
        enemySlime.currentSpeed = enemySlime.runSpeed;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        var player = Object.FindObjectOfType<Player>();
        if (player != null)
            enemySlime.SetMovement(player.transform.position);
        
    }
}
