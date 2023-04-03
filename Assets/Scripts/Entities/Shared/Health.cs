using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public event EventHandler<EventArgs> OnHealthEmpty;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    public int MaxHealth => maxHealth;

    public int CurrentHealth => currentHealth;

    public class OnHealthChangedEventArgs : EventArgs
    {
        public int oldHealthValue;
        public int newHealthValue;
    }

    public void ChangeHealth(int amount)
    {
        int oldHealth = amount;
        currentHealth = Math.Clamp(currentHealth + amount, 0, maxHealth);

        if (currentHealth == 0)
        {
            OnHealthEmpty?.Invoke(this, EventArgs.Empty);
        }

        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs { newHealthValue = currentHealth, oldHealthValue = oldHealth });
    }
}
