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
        Rotate();
    }

    private void Rotate()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(cameraRay, out RaycastHit hitInfo))
        {
            transform.LookAt(new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z));
        }
        /*
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }
        */
    }
}

