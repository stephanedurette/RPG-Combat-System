using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupTypeEnumSO", menuName = "ScriptableObjects/PickupTypeEnumSO")]
public class PickupTypeEnumSO : ScriptableObject
{
    public PickupType Weapon;
    public PickupType HeartContainer;
    public PickupType Collection;
}
