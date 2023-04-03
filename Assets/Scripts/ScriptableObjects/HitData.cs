using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/HitData")]
public class HitData : ScriptableObject
{
    public int damage;
    public float knockBackVelocity;
    public float knockBackTime;
}
