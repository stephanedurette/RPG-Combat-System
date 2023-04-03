using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] AttackAttributesSO attackAttributes;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!HelperFunctions.IsInLayerMask(collision.gameObject.layer, targetLayer)) return;

        if (collision.gameObject.TryGetComponent(out IDamageTaker damageTaker))
        {
            damageTaker.TakeDamage(transform.position, attackAttributes);
        }
    }
}
