using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private List<Vector2> patrolPositions;
    private int currentPatrolPositionIndex = -1;

    private Enemy enemy;

    public PatrolState(object owner)
    {
        enemy = owner as Enemy;

        InitializePatrolPositions();
    }

    public override void OnEnter()
    {

        enemy.currentSpeed = enemy.walkSpeed;
        currentPatrolPositionIndex = -1;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        FollowPatrol();

        if (Enemy.IsPlayerInRange(enemy.playerAggroDistance, enemy.transform.position) && enemy.canDetectPlayer)
        {
            enemy.OnDetectPlayer();
        }

    }

    void FollowPatrol()
    {
        if (currentPatrolPositionIndex == -1)
        {
            currentPatrolPositionIndex = 0;
            enemy.SetMovement(patrolPositions[currentPatrolPositionIndex]);
        }

        var maxDistanceFromPatrolPosition = 1f;
        var distanceToPatrolPosition = ((Vector2)enemy.transform.position - patrolPositions[currentPatrolPositionIndex]).magnitude;

        if (distanceToPatrolPosition <= maxDistanceFromPatrolPosition)
        {
            currentPatrolPositionIndex = (currentPatrolPositionIndex + 1) % patrolPositions.Count;
            enemy.SetMovement(patrolPositions[currentPatrolPositionIndex]);
        }
    }

    void InitializePatrolPositions()
    {
        patrolPositions = new List<Vector2>();

        foreach (Transform t in enemy.patrolTransforms)
            patrolPositions.Add(t.position);
    }
}
