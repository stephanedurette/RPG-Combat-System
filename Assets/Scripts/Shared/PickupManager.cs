using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickupManager : MonoBehaviour
{
    [SerializeField] private Collection playerHealth, playerShields, coins;

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
                break;
            case PickupType.Coin:
                coins.ChangeValue(1);
                break;
            case PickupType.Shield:
                playerShields.ChangeValue(1);
                break;
            case PickupType.Key_Green:
                Debug.Log("blah");
                break;
            case PickupType.Key_Red:
                Debug.Log("blah");
                break;
            case PickupType.Key_Blue:
                Debug.Log("blah");
                break;
            case PickupType.Sword:
                Debug.Log("blah");
                break;
            case PickupType.Spear:
                Debug.Log("blah");
                break;
            default:
                break;
        }
    }
}
