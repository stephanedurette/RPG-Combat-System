using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AttackScriptableObject")]
public class AttackAttributesSO : ScriptableObject
{
    public int damage;
    public float knockBackVelocity;
    public float knockBackTime;
    public AudioClip attackSound, attackHitSound;
}
