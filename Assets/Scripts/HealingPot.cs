using System;
using UnityEngine;

public class HealingPot : Item
{
    [SerializeField] private int _healAmount;

    public static event Action<int> OnPotionPickup;
    public static event Action<HealingPot> OnPotionDestruction;

    protected override void Effect()
    {
        OnPotionPickup?.Invoke(_healAmount);
        OnPotionDestruction?.Invoke(this);
        Destroy(gameObject);
    }
}
