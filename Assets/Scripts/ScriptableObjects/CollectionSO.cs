using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Collection", menuName = "ScriptableObjects/Collection")]
public class CollectionSO : ScriptableObject
{
    public CollectionType collectionType;

    public event Action OnValueChanged;
    public event Action OnMaxValueChanged;

    [SerializeField] private int maxMaxValue = int.MaxValue;

    [SerializeField] private int startingMaxValue;
    [SerializeField] private int startingValue;

    private int maxValue;
    private int currentValue;

    public int MaxValue { get { return maxValue; } set { SetMaxValue(value); } }

    public int CurrentValue { get { return currentValue; } set { SetValue(value); } }

    public int MaxMaxValue => maxMaxValue;

    public void Reset()
    {
        MaxValue = startingMaxValue;
        CurrentValue = startingValue;
    }

    public CollectionSO Copy()
    {
        CollectionSO copy = new CollectionSO();
        copy.maxMaxValue = this.maxMaxValue;
        copy.maxValue = this.startingMaxValue;
        copy.currentValue = this.startingValue;

        return copy;
    }

    public virtual void SetMaxValue(int amount)
    {
        int oldValue = maxValue;
        maxValue = Math.Clamp(amount, 0, maxMaxValue);

        if (oldValue != maxValue)
            OnMaxValueChanged?.Invoke();
    }

    public virtual void SetValue(int amount)
    {
        int oldValue = currentValue;
        currentValue = Math.Clamp(amount, 0, maxValue);

        if (oldValue != currentValue)
            OnValueChanged?.Invoke();
    }
}
