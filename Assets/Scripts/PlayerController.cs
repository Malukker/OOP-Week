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

    [SerializeField, MinValue(0f), MaxValue(60f), BoxGroup("Movement")] float _speed = 10f;

    Coroutine _moveRoutine;

    [SerializeField, BoxGroup("Jump")] InputActionReference _jump;
    [SerializeField, MinValue(500f), MaxValue(1000f), BoxGroup("Jump")] private float _jumpSpeed = 500f;
    [SerializeField, BoxGroup("Jump")] float _groundedDecalage;
    [SerializeField, BoxGroup("Jump")] LayerMask _layerMask;
    bool _grounded = false;

    [SerializeField, BoxGroup("Attack")] InputActionReference _attack;
    [SerializeField, BoxGroup("Attack")] GameObject _attackZone;

    [SerializeField, BoxGroup("Health")] Health _selfHealth;

    private Animator _anim;

    enum STATE
    {
        DEFAULT = 1,
        ATTACKING = 2,
        JUMPING = 3,
        STAGGERED = 4,
    }

    bool _isMoving;

    STATE _state = STATE.DEFAULT;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _move.action.started += StartMove;
        _move.action.canceled += StopMove;

        _jump.action.started += StartJump;

        _attack.action.started += StartAttack;

        _selfHealth.OnDamagedPlayer += GetStaggered;

        _anim = GetComponent<Animator>();
    }

    void StartMove(InputAction.CallbackContext obj)
    {
        _isMoving = true;
        _moveRoutine = StartCoroutine(Move());

        IEnumerator Move()
        {
            while (true)
            {
                var joystickDir = obj.ReadValue<Vector2>();

                Vector3 realDirection;
                if (_isCameraBased)
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
        _isMoving = false;
        StopCoroutine(_moveRoutine);
    }

    void StartAttack(InputAction.CallbackContext obj)
    {
        _rb.linearVelocity = Vector3.zero;
        _state = STATE.ATTACKING;
    }

    public void ChangeAttackZoneState()
    {
        if (_attackZone.activeInHierarchy) _attackZone.SetActive(false);
        else _attackZone.SetActive(true);
    }

    void StartJump(InputAction.CallbackContext obj)
    {
        _state = STATE.JUMPING;
        if (_grounded) _rb.AddForce(transform.up * _jumpSpeed);
    }

    void GetStaggered()
    {
        _anim.SetTrigger("IsHit");
        _rb.linearVelocity = Vector3.zero;
        _state = STATE.STAGGERED;
    }

    public void ResetState()
    {
        _state = STATE.DEFAULT;
    }

    private void Update()
    {
        _anim.SetBool("IsMoving", _isMoving);
        _anim.SetBool("IsAttacking", _state == STATE.ATTACKING);
        _anim.SetBool("IsJumping", !_grounded);

        _grounded = false;
        Vector3 position = new(transform.position.x, transform.position.y - _groundedDecalage, transform.position.z);
        Collider[] results = new Collider[25];
        int collsNumber = Physics.OverlapSphereNonAlloc(position, .5f, results, _layerMask);
        if (collsNumber > 1) 
        { 
            _grounded = true;
        }
    }

    private void OnDestroy()
    {
        _move.action.started -= StartMove;
        _move.action.canceled -= StopMove;

        _jump.action.started -= StartJump;

        _attack.action.started -= StartAttack;

        _selfHealth.OnDamagedPlayer -= GetStaggered;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y - _groundedDecalage, transform.position.z), .5f);
    }
}
