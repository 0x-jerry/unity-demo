﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

static class AnimatorParams
{
    public const string Walk = "walk";
    public const string RUN = "run";
}

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{

    public CharacterController character;

    public LayerMask groundLayer;

    public Camera cam;

    public float moveSpeed;

    public float turnSmoothTime = 0.1f;

    public Transform groundChecker;

    public float gravity = -9.81f;
    public float groundDistance = .2f;

    public Animator _animator;

    private Vector2 _pointer;

    private Vector2 _move;

    private bool _isWalk = false;

    private bool _isGrounded = false;

    private Vector3 _velocity = new Vector3();

    private float _turnSmoothVelocity;


    public void OnMove(InputAction.CallbackContext ctx)
    {
        _move = ctx.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        Debug.Log("Fire !");
    }

    public void OnMouseMove(InputAction.CallbackContext ctx)
    {
        _pointer = ctx.ReadValue<Vector2>();
    }

    public void Update()
    {
        _checkGround();

        _Rotate(_pointer, _move);

        _Move(_move);
    }

    private void _checkGround()
    {

        _isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundLayer);
    }

    private void _Rotate(Vector2 pointer, Vector2 move)
    {
        var width = cam.pixelWidth / 2;
        var height = cam.pixelHeight / 2;

        var x = (pointer.x - width) / width;
        var y = (pointer.y - height) / height;


        var mouseDir = new Vector2(x, y);

        var mouseAngle = Mathf.Atan2(mouseDir.x, mouseDir.y) * Mathf.Rad2Deg;

        float moveAngle = 0;

        if (move.sqrMagnitude > 0.01)
        {
            moveAngle = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;
        }

        var angle = mouseAngle + moveAngle;

        var targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _turnSmoothVelocity, turnSmoothTime);

        transform.rotation = Quaternion.Euler(new Vector3(0, targetAngle, 0));
    }

    private void _Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.01)
        {
            if (_isWalk)
            {
                _animator.SetBool(AnimatorParams.Walk, false);
                _isWalk = false;
            }
            return;
        }

        if (!_isWalk)
        {
            _animator.SetBool(AnimatorParams.Walk, true);
            _isWalk = true;
        }

        var scaledMoveSpeed = moveSpeed * Time.deltaTime;

        var dir = transform.forward;

        var moveStep = dir.normalized * scaledMoveSpeed;

        character.Move(moveStep);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        else
        {
            _velocity.y += gravity * Time.deltaTime;
        }

        var moveVertical = _velocity * Time.deltaTime;

        character.Move(moveVertical);
    }
}