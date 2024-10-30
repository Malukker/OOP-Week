using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField, BoxGroup("Movement")] InputActionReference _move;
    private Rigidbody _rb;

    [SerializeField, BoxGroup("Movement")] private bool _isCameraBased;
    [SerializeField, ShowIf("_isCameraBased"), BoxGroup("Movement")] private Camera _cam;

    [SerializeField, MinValue(20f), MaxValue(60f), BoxGroup("Movement")] float _speed = 10f;

    Coroutine _moveRoutine;

    [SerializeField, BoxGroup("Jump")] InputActionReference _jump;

    [SerializeField, BoxGroup("Attack")] InputActionReference _attack;
    [SerializeField, BoxGroup("Attack")] GameObject _attackZone;

    enum STATE
    {
        DEFAULT = 1,
        ATTACKING = 2,
        JUMPING = 3,
        STAGGERED = 4,
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _move.action.started += StartMove;
        _move.action.canceled += StopMove;

        _jump.action.started += StartJump;

        _attack.action.started += StartAttack;
    }

    void StartMove(InputAction.CallbackContext obj)
    {
        _moveRoutine = StartCoroutine(Move());

        IEnumerator Move()
        {
            while (true)
            {
                var joystickDir = obj.ReadValue<Vector2>();

                Vector3 realDirection;
                if(_isCameraBased)
                { 
                    realDirection = _cam.transform.forward * joystickDir.y + _cam.transform.right * joystickDir.x;
                    realDirection.Normalize();
                    realDirection.y = 0;
                }
                else realDirection = new Vector3(joystickDir.y, 0, joystickDir.x);

                _rb.AddForce(realDirection * _speed);

                transform.LookAt(transform.position + realDirection);

                yield return new WaitForEndOfFrame();
            }
        }
    }

    void StopMove(InputAction.CallbackContext obj)
    {
        StopCoroutine(_moveRoutine);
    }

    void StartAttack(InputAction.CallbackContext obj)
    {

    }

    void StartJump(InputAction.CallbackContext obj)
    {

    }

    private void OnDestroy()
    {
        _move.action.started -= StartMove;
        _move.action.canceled -= StopMove;
    }
}
