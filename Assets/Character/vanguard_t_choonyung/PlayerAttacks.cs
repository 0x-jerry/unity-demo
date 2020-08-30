using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerAttacks : MonoBehaviour
{
    public CharacterController character;

    public Animator animator;

    public PlayerMovement playerMovement;

    [Range(0.1f, 1)]
    public float attackGap = 0.2f;

    private float _lastAttackTime = 0;
    private bool _isFire = false;

    private int _attackValue = 0;

    public void OnFire(InputAction.CallbackContext ctx)
    {
        _isFire = ctx.ReadValueAsButton();
        if (_isFire)
        {
            _lastAttackTime = Time.time;
            _attackValue++;
        }
    }

    public void Update()
    {
        calAttackAction();
        animator.SetInteger(AnimatorParams.Attack, _attackValue);
    }

    public void calAttackAction()
    {
        if (Time.time > _lastAttackTime + attackGap)
        {
            _attackValue = 0;
        }
    }
}
