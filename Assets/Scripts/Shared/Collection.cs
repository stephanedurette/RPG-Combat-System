using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    public event EventHandler<OnValueChangedEventArgs> OnValueChanged;
    public event EventHandler<OnValueChangedEventArgs> OnMaxValueChanged;

    [SerializeField] private int maxMaxValue = int.MaxValue;
    [SerializeField] private int maxValue;
    [SerializeField] private int currentValue;

    public int MaxValue => maxValue;

    public int CurrentValue => currentValue;

    public class OnValueChangedEventArgs : EventArgs
    {
        public int oldValue;
        public int newValue;
    }

    public virtual void ChangeMaxValue(int amount)
    {
        int oldValue = maxValue;
        maxValue = Math.Clamp(maxValue + amount, 0, maxMaxValue);

        if (oldValue != maxValue)
            OnMaxValueChanged?.Invoke(this, new OnValueChangedEventArgs { newValue = maxValue, oldValue = oldValue });
    }

    public virtual void ChangeValue(int amount)
    {
        int oldValue = currentValue;
        currentValue = Math.Clamp(currentValue + amount, 0, maxValue);

        if (oldValue != currentValue)
            OnValueChanged?.Invoke(this, new OnValueChangedEventArgs { newValue = currentValue, oldValue = oldValue });
    }
}
