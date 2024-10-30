using System;
using UnityEngine;
using UnityEngine.Events;

public class Coin : Item
{
    [SerializeField] private int _value;
    public static event Action<int> OnCoinPickup;
    public static event Action<Coin> OnCoinDestruction; 

    protected override void Effect()
    {
        OnCoinPickup?.Invoke(_value);
        OnCoinDestruction?.Invoke(this);
        Destroy(gameObject);
    }
}
