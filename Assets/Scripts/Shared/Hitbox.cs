using System;
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
        if (!Helpers.IsInLayerMask(collision.gameObject.layer, targets)) return;

        if (collision.gameObject.TryGetComponent(out Hurtbox hurtBox))
        {
            hurtBox.OnHit(this.gameObject, hitData);
            OnHitboxHit?.Invoke(this, new OnHitboxHitEventArgs() { collision = collision});
        }
    }
}
