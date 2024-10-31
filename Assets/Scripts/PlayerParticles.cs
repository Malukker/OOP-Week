using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private GameObject _particleSystem;
    public void SpawnFootStep()
    {
        Instantiate(_particleSystem, new(transform.position.x, transform.position.y+.01f, transform.position.z), Quaternion.identity);
    }
}
