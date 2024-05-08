using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;

    [SerializeField] float moveSpeed;

    private Vector3 moveDir;
    private float ySpeed;

    Coroutine staminaRoutine;

    bool isUseStamina = false;
    bool isTired = false;

    private void Start()
    {
        staminaRoutine = StartCoroutine(StaminaRoutine());
        PlayerStatManager.Inventory.playerStat.SetUp();
        moveSpeed = PlayerStatManager.Inventory.playerStat.MoveSpeed;
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

        animator.SetFloat("PosX", moveDir.x * moveSpeed, 0.25f, Time.deltaTime);
        animator.SetFloat("PosY", moveDir.z * moveSpeed, 0.25f, Time.deltaTime);

        if (PlayerStatManager.Inventory.playerStat.CurStamina <= 0)
        {
            isTired = true;
            isUseStamina = false;
            moveSpeed = PlayerStatManager.Inventory.playerStat.MoveSpeed;
        }
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
        if (value.isPressed && !isTired)
        {
            moveSpeed = PlayerStatManager.Inventory.playerStat.MoveSpeed * 1.8f;
            isUseStamina = true;
        }
        else
        {
            moveSpeed = PlayerStatManager.Inventory.playerStat.MoveSpeed;
            isUseStamina = false;

            if(PlayerStatManager.Inventory.playerStat.CurStamina >= 10)
            {
                isTired = false;
            }
        }
    }

    IEnumerator StaminaRoutine()
    {
        while(true)
        {
            if (isUseStamina)
            {
                PlayerStatManager.Inventory.playerStat.CurStamina -= 2;
            }
            else
            {
                PlayerStatManager.Inventory.playerStat.CurStamina += 1;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

}

