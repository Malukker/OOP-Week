using System;
using UnityEngine;
using UnityEngine.Events;

public class Hitzone : MonoBehaviour
{
    public static event Action<Hitzone, GameObject> OnCollision;
    [SerializeField] private UnityEvent<int> _onDamageTaken;

    [SerializeField] bool _isUsedByPlayer;

    public void Awake()
    {
        Attackzone.OnDamageDealt += RegisterDamage;

        if (gameObject.layer != 6)
        {
            throw new Exception("This gameobject's layer should be Hitzone");
        }
    }

    private void Update()
    {
        if (_isUsedByPlayer) transform.localPosition = Vector3.zero;
    }

    void RegisterDamage(int damage, Hitzone hitzone)
    {
        if(hitzone != null && hitzone == this)
        {
            _onDamageTaken?.Invoke(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollision?.Invoke(this, other.gameObject);
    }

    private void OnDestroy()
    {
        Attackzone.OnDamageDealt -= RegisterDamage;
    }
}
