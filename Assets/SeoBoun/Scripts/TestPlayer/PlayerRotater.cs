using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotater : MonoBehaviour
{
    // feat : PlayerRotate
    [SerializeField] float mouseSensitivity;

    private Vector2 inputDir;
    private float xRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, mouseSensitivity * inputDir.x * Time.deltaTime);
    }

    private void OnLook(InputValue value)
    {
        inputDir = value.Get<Vector2>();
    }
}
