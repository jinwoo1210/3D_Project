using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Component")]

    bool iDown;
    bool sDown1;
    bool sDown2;
    bool fDown;
    public bool isSwap;
    public int equipWeaponIndex = -1;

    GameObject nearObject;
    Gun equipWeapon;
    public PlayerHealth health;
    public BulletData[] bulletData;
    public Bullet bullet;
    public GameObject[] weapons;
    public bool[] hasWeapon;
    
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
        fDown = Input.GetButtonDown("Fire1");
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
                bullet.ammoRemain += bullet.bulletCount;
                Debug.Log($"{nearObject.name}을 먹었습니다.");
            }
        }
    }

    private void OnShow()
    {
        if (sDown1 && (!hasWeapon[0] || equipWeaponIndex == 0))
            return;
        if (sDown2 && (!hasWeapon[1] || equipWeaponIndex == 1))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;

        if (sDown1 || sDown2)
        {
            if (equipWeapon != null)
                equipWeapon.gameObject.SetActive(false);
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Gun>();
            equipWeapon.gameObject.SetActive(true);

            //anim.SetTrigger("doSwap");        // 애니메이터 추가 예정
            isSwap = true;

            Invoke("SwapOut", 0.4f);
        }
    }

    private void SwapOut()
    {
        isSwap = false;
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
