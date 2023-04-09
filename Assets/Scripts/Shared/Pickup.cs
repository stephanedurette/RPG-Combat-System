using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupData pickupData;
    [SerializeField] private LayerMask pickupCollisionLayers;

    private Animator animator;

    public static Action<PickupData, Action> OnPickupAction;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Helpers.IsInLayerMask(collision.gameObject.layer, pickupCollisionLayers)) return;

        
        OnPickupAction?.Invoke(pickupData, () => this.animator.SetTrigger("Destroy"));
    }

    public void OnAnimationDestroyEvent()
    {
        Destroy(this.gameObject);
    }
}
