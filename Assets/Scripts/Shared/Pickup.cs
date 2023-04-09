using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupManager.PickupType pickUpType;
    [SerializeField] private LayerMask pickupCollisionLayers;

    private Animator animator;

    public static Action<PickupManager.PickupType> OnPickupAction;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Helpers.IsInLayerMask(collision.gameObject.layer, pickupCollisionLayers)) return;

        OnPickupAction?.Invoke(pickUpType);

        animator.SetTrigger("Destroy");
    }

    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
