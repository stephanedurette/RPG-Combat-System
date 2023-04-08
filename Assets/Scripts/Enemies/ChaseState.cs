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
        
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        var player = Object.FindObjectOfType<Player>();
        if (player == null) return;

        float minDistance = 0.5f;

        enemySlime.currentSpeed = enemySlime.IsPlayerInRange(minDistance) ? 0f : enemySlime.runSpeed;

        enemySlime.SetMovement(player.transform.position);

    }
}
