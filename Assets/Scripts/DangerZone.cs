using System;
using System.Collections;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private float _cycleTime; private float _currentCycleTime;

    private Collider _collider;
    private MeshRenderer _meshRenderer;

    [SerializeField] private Material[] _renderMaterials;

    public static event Action<DangerZone> OnAttack;

    void Start()
    {
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _currentCycleTime = _cycleTime;
    }

    private void Update()
    {
        _currentCycleTime -= Time.deltaTime;
        if (_currentCycleTime < 0 )
        {
            OnAttack?.Invoke(this);
            ChangeAttackZoneState();
            ChangeColor();
            _currentCycleTime = _cycleTime;
        }
    }

    void ChangeAttackZoneState()
    {
        if (_collider.enabled) _collider.enabled = false;
        else _collider.enabled = true;
    }

    bool _isRed;
    void ChangeColor()
    {
        if (_isRed) { _meshRenderer.material = _renderMaterials[0]; _isRed = false; }
        else { _meshRenderer.material = _renderMaterials[1]; _isRed = true; }
    }
}