using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupManager : MonoBehaviour
{
    [SerializeField] private Collection playerHealth, playerShields, coins, blueKeys, greenKeys, redKeys;
    [SerializeField] private GameObject spearWeaponPrefab, swordWeaponPrefab;

    public static Action<GameObject> OnWeaponPickup;

    private void OnEnable()
    {
        Pickup.OnPickupAction += HandlePickUp;
    }

    private void OnDisable()
    {
        Pickup.OnPickupAction -= HandlePickUp;
    }

    public enum PickupType
    {
        None,
        Heart,
        HeartContainer,
        Coin,
        Shield,
        Key_Green,
        Key_Red,
        Key_Blue,
        Sword,
        Spear
    }

    void HandlePickUp(PickupType p)
    {
        switch (p)
        {
            case PickupType.Heart:
                playerHealth.ChangeValue(1);
                break;
            case PickupType.HeartContainer:
                playerHealth.ChangeMaxValue(1);
                playerHealth.ChangeValue(1);
                break;
            case PickupType.Coin:
                coins.ChangeValue(1);
                break;
            case PickupType.Shield:
                playerShields.ChangeValue(1);
                break;
            case PickupType.Key_Green:
                greenKeys.ChangeValue(1);
                break;
            case PickupType.Key_Red:
                redKeys.ChangeValue(1);
                break;
            case PickupType.Key_Blue:
                blueKeys.ChangeValue(1);
                break;
            case PickupType.Sword:
                OnWeaponPickup?.Invoke(swordWeaponPrefab);
                break;
            case PickupType.Spear:
                OnWeaponPickup?.Invoke(spearWeaponPrefab);
                break;
            default:
                break;
        }
    }
}
