using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Component")]

    bool iDown;
    GameObject nearObject;
    GameObject equipWeapon;
    public GameObject[] weapons;
    public bool[] hasWeapon;
    bool sDown1;
    bool sDown2;
    bool isSwap;
    public PlayerHealth health;



    private void Update()
    {
        GetInput();
        OnPick();
        OnShow();
    }


    void GetInput()
    {
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
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
            else if (nearObject.tag == "Item")
            {
                Destroy(nearObject);
                Debug.Log($"{nearObject.name}을 먹었습니다.");
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

            //anim.SetTrigger("doSwap");        // 애니메이터 추가 예정
            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }

    private void OnHeal(InputValue value)
    {
        health.Heal();
    }

    private void OnInteract()
    {
        Physics.OverlapSphere(transform.position, 5f);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Item")
        {
            nearObject = other.gameObject;
        }

        //Debug.Log(nearObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Weapon")
        {
            nearObject = null;
        }
        else if (other.tag == "Item")
        {
            nearObject = null;
        }
    }
}
