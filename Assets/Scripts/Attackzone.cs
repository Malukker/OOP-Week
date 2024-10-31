using System;
using UnityEngine;

public class Attackzone : MonoBehaviour
{
    [SerializeField] private int _damage;

    public static event Action<int, Hitzone> OnDamageDealt;

    [SerializeField] bool _isUsedByPlayer;

    private void Awake()
    {
        Hitzone.OnCollision += SendDamageToStruckCollider;

        if (gameObject.layer != 7)
        {
            throw new Exception("This gameobject's layer should be AttackZone");
        }
    }

    private void Update()
    {
        if (_isUsedByPlayer) transform.localPosition = Vector3.zero;
    }

    void SendDamageToStruckCollider(Hitzone hitzone, GameObject obj)
    {
        if (obj != null && obj == this.gameObject)
        {
            OnDamageDealt.Invoke(_damage, hitzone);
        }
    }

    private void OnDestroy()
    {
        Hitzone.OnCollision -= SendDamageToStruckCollider;
    }
}
