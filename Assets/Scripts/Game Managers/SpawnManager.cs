using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float projectileLifeTime = 5f;

    private void OnEnable()
    {
        ProjectileLauncher.OnProjectileLaunchRequested += ProjectileLauncher_OnProjectileLaunchRequested;   
    }

    private void OnDisable()
    {
        ProjectileLauncher.OnProjectileLaunchRequested -= ProjectileLauncher_OnProjectileLaunchRequested;
    }

    private void ProjectileLauncher_OnProjectileLaunchRequested(object sender, ProjectileLauncher.OnProjectileLaunchRequestedEventArgs e)
    {
        GameObject g = Instantiate(e.Prefab);
        g.transform.parent = this.transform;
        g.transform.right = e.DirectionUnitVector;
        g.transform.position = e.StartingPosition;
        g.GetComponent<Rigidbody2D>().velocity = e.Speed * e.DirectionUnitVector;
        Destroy(g, projectileLifeTime);
    }

}
