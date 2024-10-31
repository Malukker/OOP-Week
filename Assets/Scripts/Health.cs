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

    [SerializeField] bool _hasLifebar;

    [SerializeField, ShowIf(nameof(_hasLifebar))] private Slider _lifebar;
    private RectTransform _lifebarRect;
    private float _lifebarSize;

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
    public float LifebarSize
    {
        get { return _lifebarSize; }
        set { _lifebarSize = value; }
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
        _currentLife = _maxLife;

        if (_hasLifebar)
        {
            _lifebar.maxValue = _maxLife;
            _lifebar.value = _currentLife;
            _lifebarRect = _lifebar.GetComponent<RectTransform>();
            _lifebarSize = _lifebarRect.sizeDelta.x;

            PowerUp.OnPowerUp += ExtendLifebar;
        }

        HealingPot.OnPotionPickup += Heal;
    }

    private void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            throw new AssertionException("Damage must be greater than 0", "Health");
        }

        _currentLife = Mathf.Clamp(_currentLife - damage, min: 0, _maxLife);
        if(_hasLifebar) _lifebar.value = _currentLife;
        OnDamaged?.Invoke();

        if (_currentLife <= 0)
        {
            //Die();
        }
    }
    private void Heal(int heal)
    {
        if (heal < 0)
        {
            throw new AssertionException("Heal must be greater than 0", "Health");
        }

        _currentLife = Mathf.Clamp(_currentLife + heal, min: 0, _maxLife);
        if (_hasLifebar) _lifebar.value += _currentLife;
    }

    public static event Action<float> OnLifebarExtension;

    private void ExtendLifebar(float extension, int upgrade)
    {
        if (_hasLifebar) 
        {
            _lifebarRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, _lifebarRect.offsetMin.x, _lifebarSize + extension);
            _lifebarSize = _lifebarRect.sizeDelta.x;
            _currentLife += upgrade;
            _maxLife += upgrade;
            OnLifebarExtension?.Invoke(_lifebarSize);
        }
    }

    void Die()
    {
        _onDie?.Invoke();
    }
}