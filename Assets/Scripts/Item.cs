using Unity.Cinemachine;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField, TagField] private string _targetedTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_targetedTag))
        {
            Effect();
        }
    }

    protected abstract void Effect();
}
