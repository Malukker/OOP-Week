using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField, ValidateInput(nameof(ValidateMaxLife), "incorrect")] int _maxLife;

    [ShowNonSerializedField] int _currentLife;
    public int CurrentLife { get { return _currentLife; } }

    [SerializeField] private UnityEvent _onDie;

    public event Action OnDamaged;

    [SerializeField] private Slider _lifebar;

    public int CurrentHealth
    {
        get { return _currentLife; }
        set { _currentLife = value; }
    }
    public int MaxHealth
    {
        get { return _maxLife; }
        private set { _maxLife = value; }
    }

    public bool IsDead => _currentLife <= 0;

    public int GetCurrentHealth()
    {
        return _currentLife;
    }

    public Health(int maxHealth, int currentHealth)
    {
        if (maxHealth <= 0)
        {
            throw new AssertionException("MaxHealth must be greater than 0", "Health");
        }
        if (currentHealth < 0)
        {
            throw new AssertionException("MaxHealth must be greater than 0", "Health");
        }
        if (maxHealth < currentHealth)
        {
            throw new AssertionException("CurrentHealth must be lesser or equal to MaxHealth", "Health");
        }
        _maxLife = maxHealth;
        _currentLife = currentHealth;
        _onDie = null;
    }
    public Health(int maxHealth)
    {
        if (maxHealth <= 0)
        {
            throw new AssertionException("MaxHealth must be greater than 0", "Health");
        }
        _maxLife = maxHealth;
        _onDie = null;
    }


    #region Editor
    void Reset()
    {
        _maxLife = 100;
    }

    [Button]
    void TestTakeDamage()
    {
        TakeDamage(10);
    }

    [Button]
    void TestTakeDamageNeg()
    {
        TakeDamage(-10);
    }

    bool ValidateMaxLife() => _maxLife > 0;

    #endregion

    void Awake()
    {
        _lifebar.maxValue = _maxLife;
        _currentLife = _maxLife;
        _lifebar.value = _currentLife;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            throw new AssertionException("Damage must be greater than 0", "Health");
        }

        _currentLife = Mathf.Clamp(_currentLife - damage, min: 0, _maxLife);
        _lifebar.value = _currentLife;
        OnDamaged?.Invoke();

        if (_currentLife <= 0)
        {
            //Die();
        }
    }
    public void Heal(int heal)
    {
        if (heal < 0)
        {
            throw new AssertionException("Heal must be greater than 0", "Health");
        }

        _currentLife = Mathf.Clamp(_currentLife + heal, min: 0, _maxLife);
        _lifebar.value += _currentLife;
    }

    void Die()
    {
        _onDie?.Invoke();
    }
}