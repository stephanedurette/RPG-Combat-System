using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private List<Vector2> patrolPositions;
    private int currentPatrolPositionIndex = -1;

    private EnemySlime enemySlime;

    public PatrolState(object owner)
    {
        enemySlime = owner as EnemySlime;

        InitializePatrolPositions();
    }

    public override void OnEnter()
    {

        enemySlime.currentSpeed = enemySlime.walkSpeed;
        currentPatrolPositionIndex = -1;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        FollowPatrol();

        if (enemySlime.IsPlayerInRange(enemySlime.playerAggroDistance))
        {
            enemySlime.stateMachine.SetState(enemySlime.chaseState);
        }

    }

    void FollowPatrol()
    {
        if (currentPatrolPositionIndex == -1)
        {
            currentPatrolPositionIndex = 0;
            enemySlime.SetMovement(patrolPositions[currentPatrolPositionIndex]);
        }

        var maxDistanceFromPatrolPosition = 0.1f;
        var distanceToPatrolPosition = ((Vector2)enemySlime.transform.position - patrolPositions[currentPatrolPositionIndex]).magnitude;

        if (distanceToPatrolPosition <= maxDistanceFromPatrolPosition)
        {
            currentPatrolPositionIndex = (currentPatrolPositionIndex + 1) % patrolPositions.Count;
            enemySlime.SetMovement(patrolPositions[currentPatrolPositionIndex]);
        }
    }

    void InitializePatrolPositions()
    {
        patrolPositions = new List<Vector2>();

        foreach (Transform t in enemySlime.patrolTransforms)
            patrolPositions.Add(t.position);
    }
}
