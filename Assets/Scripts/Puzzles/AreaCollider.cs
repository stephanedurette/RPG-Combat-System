using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaCollider : MonoBehaviour
{
    [SerializeField] LayerMask targets;

    public Action OnAreaColliderHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Helpers.IsInLayerMask(collision.gameObject.layer, targets)) return;

        OnAreaColliderHit?.Invoke();
    }
}
