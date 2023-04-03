using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private List<Transform> patrolTransforms;
    [SerializeField] private EnemySlime enemySlime;

    private List<Vector2> patrolPositions;

    private int currentPatrolPositionIndex = -1;

    private void Start()
    {
        InitializePatrolPositions();
    }

    public override void OnEnter(StateEnterArgs args = null)
    {
        enemySlime.SetSpeed(EnemySlime.Speed.Walk);
        currentPatrolPositionIndex = -1;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
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

        foreach (Transform t in patrolTransforms)
            patrolPositions.Add(t.position);
    }
}
