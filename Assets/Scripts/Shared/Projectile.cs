using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private AreaCollider areaCollider;

    private void OnEnable()
    {
        areaCollider.OnAreaColliderHit += OnAreaColliderHit;
    }
    private void OnDisable()
    {
        areaCollider.OnAreaColliderHit -= OnAreaColliderHit;
    }

    private void OnAreaColliderHit(GameObject other)
    {
        Destroy(this.gameObject);
    }
}
