using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Collection
{
    public event EventHandler<EventArgs> OnHealthEmpty;

    public override void ChangeValue(int amount)
    {
        base.ChangeValue(amount);

        if (CurrentValue == 0)
        {
            OnHealthEmpty?.Invoke(this, EventArgs.Empty);
        }
    }
}
