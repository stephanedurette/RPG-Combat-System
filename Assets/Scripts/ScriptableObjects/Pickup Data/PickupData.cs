using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupData", menuName = "ScriptableObjects/PickupData")]
public class PickupData : ScriptableObject
{
    public PickupType pickupType;

    [Header("For Weapons")]
    //pickup type = weapon
    public WeaponType weaponType;
    public GameObject weaponPrefab;

    [Header("For Heart Containers")]
    //pickup type = heart container
    public CollectionType heartContainerCollectionType;

    [Header("For Collections")]
    //pickup type = collection
    public CollectionType collectionType;
    public int amount;
}
