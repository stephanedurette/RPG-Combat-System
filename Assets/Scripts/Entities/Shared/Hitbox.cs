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

    public event EventHandler<OnHitboxHitEventArgs> OnHitboxHit;

    public class OnHitboxHitEventArgs : EventArgs
    {
        public Collider2D collision;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInLayerMask(collision.gameObject.layer, targets)) return;

        if (collision.gameObject.TryGetComponent(out Hurtbox hurtBox))
        {
            hurtBox.OnHit(this.gameObject, hitData);
            OnHitboxHit?.Invoke(this, new OnHitboxHitEventArgs() { collision = collision});
        }
    }

    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
