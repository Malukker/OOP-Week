using NaughtyAttributes;
using System;
using UnityEngine;

public class PowerUp : Item
{
    [SerializeField] private float _healthUpgradeAmount;

    private float _lifebarSize;
    private float _maxPlayerLife;
    private GameObject _player;

    public static event Action<float, int> OnPowerUp;
    public static event Action<PowerUp> OnPowerUpDestruction;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _lifebarSize = _player.GetComponent<Health>().LifebarSize;
        _maxPlayerLife = _player.GetComponent<Health>().MaxHealth;

        Health.OnLifebarExtension += UpdateLifebarSize;
    }

    void UpdateLifebarSize(float lifebarSize)
    {
        _lifebarSize = lifebarSize;
    }

    protected override void Effect()
    {
        float amount = (_healthUpgradeAmount / _maxPlayerLife) * _lifebarSize;

        OnPowerUp?.Invoke(amount, (int)_healthUpgradeAmount);
        OnPowerUpDestruction?.Invoke(this);
        Destroy(gameObject);
    }
}
