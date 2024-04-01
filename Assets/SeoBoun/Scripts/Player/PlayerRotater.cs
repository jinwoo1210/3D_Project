using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotater : MonoBehaviour
{
    // feat : PlayerRotate(ƒı≈Õ∫‰ Ω√¡°)
    [SerializeField] float mouseSensitivity;
    [SerializeField] Vector3 mousePos;

    [SerializeField] private Vector2 inputDir;
    private float mouseY;
    private float mouseX;

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
        // transform.Rotate(Vector3.up, mouseSensitivity * inputDir.x * Time.deltaTime);
        Rotate();
    }
/*
    private void OnLook(InputValue value)
    {
        inputDir = value.Get<Vector2>();
        transform.rotation = Quaternion.Euler(0f, transform.rotation.y + inputDir.x, 0f);
    }
*/
    private void Rotate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }
    }
}

