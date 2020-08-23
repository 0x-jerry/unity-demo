using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControll : MonoBehaviour
{
    public Transform followingTransform;

    [Range(.1f, 1f)]
    public float smoothSpeed = 0.5f;

    public float rotateSpeed = 10f;

    private Vector3 m_offset;

    private float m_rotate;

    // Start is called before the first frame update
    void Start()
    {
        m_offset = transform.position - followingTransform.position;
    }


    void Update()
    {
        Rotate(m_rotate);
    }

    public void OnRotate(InputAction.CallbackContext ctx)
    {
        m_rotate = ctx.ReadValue<float>();
        Debug.Log("Rotate !");
    }

    private void Rotate(float rotate)
    {
        if (rotate * rotate < 0.01)
            return;

        var deg = (rotate > 0 ? 1 : -1) * Time.deltaTime * rotateSpeed;

        transform.Rotate(Vector3.left, deg);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Following();
    }

    private void Following()
    {
        var pos = followingTransform.position + m_offset;
        transform.position = Vector3.Slerp(transform.position, pos, smoothSpeed);

        transform.LookAt(followingTransform);
    }
}
