using System;
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

    Coroutine staminaRoutine;

    bool isUseStamina = false;

    private void OnEnable()
    {
        moveSpeed = playerStat.MoveSpeed;
        staminaRoutine = StartCoroutine(StaminaRoutine());
    }

    private void Update()
    {
        Move();
        Ymove();
    }
    private void Move()                     // feat : Move
    {
        Vector3 forwardDir = Camera.main.transform.forward;
        Vector3 rightDir = Camera.main.transform.right;

        forwardDir = new Vector3(forwardDir.x, 0, forwardDir.z).normalized;
        rightDir = new Vector3(rightDir.x, 0, rightDir.z).normalized;

        controller.Move(forwardDir * moveDir.z * moveSpeed * Time.deltaTime);
        controller.Move(rightDir * moveDir.x * moveSpeed * Time.deltaTime);

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

    private void OnRun(InputValue value)
    {
        if(value.isPressed && playerStat.CurStamina > 0)
        {
            moveSpeed = playerStat.MoveSpeed * 1.8f;
            isUseStamina = true;
        }
        else
        {
            moveSpeed = playerStat.MoveSpeed;
            isUseStamina = false;
        }
    }

    IEnumerator StaminaRoutine()
    {
        while(true)
        {
            if (isUseStamina)
            {
                playerStat.CurStamina--;
            }
            else
            {
                playerStat.CurStamina++;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
