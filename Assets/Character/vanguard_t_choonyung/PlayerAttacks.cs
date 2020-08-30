using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerAttacks : MonoBehaviour
{

    public CharacterController character;

    public Animator animator;

    // private bool _isAttack = false;

    private float _lastAttackTime = 0;

    private int _attackValue = -1;


    public void OnFire(InputAction.CallbackContext ctx)
    {
        var isButtonDown = ctx.ReadValueAsButton();
        if (isButtonDown)
        {
            _lastAttackTime = Time.time;
        }
    }

    public void Update()
    {
        calAttackAction();
        animator.SetInteger(AnimatorParams.Attack, _attackValue);
    }

    public void calAttackAction()
    {
        if (Time.time - _lastAttackTime < 1)
        {
            _attackValue++;
        }
        else
        {
            _attackValue = -1;
        }
    }
}
