using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    [SerializeField] LayerMask targets;
    [SerializeField] HitData hitData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsInLayerMask(collision.gameObject.layer, targets)) return;

        if (collision.gameObject.TryGetComponent(out IDamageTaker damageTaker))
            damageTaker.TakeDamage(this, hitData);
    }

    public static bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
