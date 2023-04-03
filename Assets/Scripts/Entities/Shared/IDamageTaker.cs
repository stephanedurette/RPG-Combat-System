using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageTaker
{
    public void TakeDamage(Vector3 sourcePosition, AttackAttributesSO attackAttributes);

}
