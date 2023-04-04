using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    [SerializeField] LayerMask targets;
    [SerializeField] HitData hitData;

    public event EventHandler<OnCollisionEventArgs> OnCollision;

    public class OnCollisionEventArgs : EventArgs
    {
        public Collider2D collision;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInLayerMask(collision.gameObject.layer, targets)) return;

        if (collision.gameObject.TryGetComponent(out IDamageTaker damageTaker))
        {
            damageTaker.TakeDamage(this.gameObject, hitData);
            OnCollision?.Invoke(this, new OnCollisionEventArgs() { collision = collision});
        }
    }

    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
