using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterController : MonoBehaviour
{
    [Range(1.0f, 10.0f)]
    public float moveSpeed = 1.0f;

    Animator animator;

    Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            animator.SetBool("walk", true);

            var pos = transform.position;
            transform.position = pos + Vector3.up * moveSpeed;
        }
        else if (Input.GetKeyUp("up"))
        {
            animator.SetBool("walk", false);
        }
    }

    void FixedUpdate()
    {

    }

}
