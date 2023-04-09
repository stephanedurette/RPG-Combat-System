using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponPickupData", menuName = "ScriptableObjects/WeaponPickupData")]
public class WeaponPickUpData : PickupData
{
    public WeaponType weaponType;
    public GameObject weaponPrefab;
}
