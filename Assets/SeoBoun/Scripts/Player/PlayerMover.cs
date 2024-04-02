using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] PlayerStat playerStat;
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;

    [SerializeField] float moveSpeed;

    private Vector3 moveDir;
    private float ySpeed;

    private void OnEnable()
    {
        moveSpeed = playerStat.MoveSpeed;
    }

    private void Update()
    {
        Move();
        Ymove();
    }
    private void Move()                     // feat : Move
    {
        controller.Move(moveDir * moveSpeed * Time.deltaTime);
        animator.SetFloat("PosX", moveDir.x * moveSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("PosY", moveDir.z * moveSpeed, 0.1f, Time.deltaTime);
    }
    private void OnMove(InputValue value)   // InputAction
    {
        Vector2 inputDir = value.Get<Vector2>();

        moveDir.x = inputDir.x;
        moveDir.z = inputDir.y;
    }
    private void Ymove()                    // feat : Gravity
    {
        ySpeed += Physics.gravity.y * Time.deltaTime;

        controller.Move(Vector3.up * ySpeed * Time.deltaTime);
    }
}
