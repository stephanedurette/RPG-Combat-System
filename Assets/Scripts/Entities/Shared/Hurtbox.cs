using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class Hurtbox : MonoBehaviour
{
    public event EventHandler<OnHurtboxHitEventArgs> OnHurtboxHit;

    public class OnHurtboxHitEventArgs : EventArgs {
        public GameObject other;
        public HitData hitData;
    }

    public void OnHit(GameObject other, HitData hitData)
    {
        OnHurtboxHit?.Invoke(this, new OnHurtboxHitEventArgs() { other = other, hitData = hitData });
    }
}
