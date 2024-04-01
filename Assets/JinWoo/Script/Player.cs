using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] CharacterController Controller;

    [SerializeField] float moveSpeed;

    private Vector3 moveDir;


    bool iDown;
    GameObject nearObject;
    GameObject equipWeapon;
    Animator anim;
    public GameObject[] weapons;
    public bool[] hasWeapon;
    bool sDown1;
    bool sDown2;
    bool isSwap;



    private void Update()
    {
        GetInput();
        Controller.Move(moveDir * moveSpeed * Time.deltaTime);
        //Interation();
        OnPick();
        //Swap();
        OnShow();
    }

    void GetInput()
    {
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
    }

    private void OnMove(InputValue value)
    {
        Vector2 inputDir = value.Get<Vector2>();
        moveDir.x = inputDir.x;
        moveDir.z = inputDir.y;
    }

    private void OnPick()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapon[weaponIndex] = true;

                Destroy(nearObject);
            }
        }
    }

    private void OnShow()
    {
        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;

        if (sDown1 || sDown2)
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false);
            equipWeapon = weapons[weaponIndex];
            equipWeapon.SetActive(true);

            //anim.SetTrigger("doSwap");        // �ִϸ����� �߰� ����
            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }

        Debug.Log(nearObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Weapon")
        {
            nearObject = null;
        }
    }
}