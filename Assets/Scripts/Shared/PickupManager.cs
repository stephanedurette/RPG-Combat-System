using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupManager : MonoBehaviour
{
    [SerializeField] private List<CollectionSO> collections;
    [SerializeField] private PickupTypeEnumSO pickupTypeEnumSO;

    public static Action<PickupData> OnWeaponPickup;

    private Dictionary<CollectionType, CollectionSO> collectionsByType;

    private void Start()
    {
        SetupCollectionDict();
        ResetCollections();
    }

    void ResetCollections()
    {
        foreach(CollectionSO col in collections)
        {
            col.Reset();
        }
    }

    void SetupCollectionDict()
    {
        collectionsByType = new Dictionary<CollectionType, CollectionSO>();

        foreach (CollectionSO c in collections)
        {
            collectionsByType[c.collectionType] = c;
        }
    }

    private void OnEnable()
    {
        Pickup.OnPickupAction += HandlePickUp;
    }

    private void OnDisable()
    {
        Pickup.OnPickupAction -= HandlePickUp;
    }

    void HandleCollectionPickup(PickupData data, Action onResolve)
    {
        var col = collectionsByType[data.collectionType];
        if (col.CurrentValue != col.MaxValue)
        {
            col.CurrentValue += 1;
            onResolve?.Invoke();
        }
    }

    void HandleWeaponPickup(PickupData data, Action onResolve)
    {
        OnWeaponPickup?.Invoke(data);
        onResolve?.Invoke();
    }

    void HandleHeartContainerPickup(PickupData data, Action onResolve)
    {
        var col = collectionsByType[data.heartContainerCollectionType];
        if (col.MaxValue != col.MaxMaxValue)
        {
            collectionsByType[data.heartContainerCollectionType].MaxValue += 1;
            collectionsByType[data.heartContainerCollectionType].CurrentValue += 1;

            onResolve?.Invoke();
        }
    }

    void HandlePickUp(PickupData data, Action onResolve)
    {
        if (data.pickupType == pickupTypeEnumSO.Collection)
        {
            HandleCollectionPickup(data, onResolve);
        }
        else if (data.pickupType == pickupTypeEnumSO.HeartContainer)
        {
            HandleHeartContainerPickup(data, onResolve);
        }
        else if (data.pickupType == pickupTypeEnumSO.Weapon)
        {
            HandleWeaponPickup(data, onResolve);
        }
    }
}
