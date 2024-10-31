using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] private GameObject _damagedEffect, _healEffect, _upgradeEffect, _goldEffect, _dieEffect;

    private void Awake()
    {
        Health.OnTakeDamage += Damaged;
        Health.OnDie += Death;
        HealingPot.OnPotionDestruction += Heal;
        PowerUp.OnPowerUpDestruction += Upgrade;
        Coin.OnCoinDestruction += Gold;
    }

    void Damaged(Health obj)
    {
        Instantiate(_damagedEffect, obj.transform.position, obj.transform.rotation);
    }

    void Heal(HealingPot obj)
    {
        Instantiate(_healEffect, obj.transform.position, obj.transform.rotation);
    }

    void Upgrade(PowerUp obj)
    {
        Instantiate(_upgradeEffect, obj.transform.position, obj.transform.rotation);
    }

    void Gold(Coin obj)
    {
        Instantiate(_goldEffect, obj.transform.position, obj.transform.rotation);
    }

    void Death(Health obj)
    {
        Instantiate(_dieEffect, obj.transform.position, obj.transform.rotation);
    }
}
