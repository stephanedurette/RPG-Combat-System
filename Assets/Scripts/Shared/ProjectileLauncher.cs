using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;

    public static event EventHandler<OnProjectileLaunchRequestedEventArgs> OnProjectileLaunchRequested;

    public class OnProjectileLaunchRequestedEventArgs : EventArgs {
        public GameObject Prefab;
        public float Speed;
        public Vector2 DirectionUnitVector;
        public Vector2 StartingPosition;
    }


    public void LaunchProjectile()
    {
        OnProjectileLaunchRequested?.Invoke(this, new OnProjectileLaunchRequestedEventArgs {
            Prefab = projectilePrefab,
            Speed = projectileSpeed,
            DirectionUnitVector = transform.right,
            StartingPosition = transform.position
        });

        Debug.Log("launching");
    }
}
