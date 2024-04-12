using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Component")]

    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;
    bool sDown4;
    bool fDown;
    public bool isSwap;
    public int equipWeaponIndex = -1;   //1번키의 웨폰 인덱스

    SwapWeapon swap;

    public GameObject nearObject;
    Gun equipWeapon;
    public PlayerHealth health;
    public BulletData[] bulletData;
    public BulletUI bullet;
    public GameObject[] weapons;
    public bool[] hasWeapon;
    public GameObject[] WeaponList;
    Item item;
    
    public bool hasTrue = false;


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
        sDown3 = Input.GetButtonDown("Swap3");
        sDown4 = Input.GetButtonDown("Swap4");
        fDown = Input.GetButtonDown("Fire1");
    }

    public void OnPick()                // f 키 눌렀을 때
    {
        if (iDown && nearObject != null)
        {   // 근처의 오브젝트 테그가 무기 이면
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();    //아이템스크립트에서
                int weaponIndex = item.value;                   // 벨류값을 가져오고
                hasWeapon[weaponIndex] = true;                  // hasWeapon의 배열의값을 true로 만들어준다.
                Destroy(nearObject);
                if (weapons[0] != null && weapons[1] != null)   //weapons의 0번칸, 1번칸이 null이 아니라면  
                {
                    hasWeapon[weaponIndex] = false;
                    weapons[equipWeaponIndex] = WeaponList[weaponIndex];   //손에 무기가 들려 있을 때, 웨폰리스트에서 무기 교체
                }
                else if (weapons[1] == null)
                {
                    hasWeapon[weaponIndex] = true;
                    weapons[1] = WeaponList[weaponIndex];
                }
                return;
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
        //if (sDown1 && (!hasWeapon[0] || equipWeaponIndex == 0))     //1번무기에      
        //    return;
        //if (sDown2 && (!hasWeapon[1] || equipWeaponIndex == 1))
        //    return;


        int weaponIndex = -1;
        if (sDown1) weaponIndex = (int)equipWeapon.type;

        if (sDown2) weaponIndex = (int)equipWeapon.type;

        if (sDown1 || sDown2)   // 1번을 눌렀을 때
        {
            Debug.Log(weaponIndex);
            if (equipWeapon != null)            //
                equipWeapon.gameObject.SetActive(false);
            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex].GetComponent<Gun>();
            equipWeapon.gameObject.SetActive(true);
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
        if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;
        }
        else if (other.tag == "Item")
        {
            nearObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = null;
        }
        else if (other.tag == "Item")
        {
            nearObject = null;
        }
    }
}
