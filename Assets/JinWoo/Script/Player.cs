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
    public int equipWeaponIndex = -1;   //1��Ű�� ���� �ε���

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

    public void OnPick()                // f Ű ������ ��
    {
        if (iDown && nearObject != null)
        {   // ��ó�� ������Ʈ �ױװ� ���� �̸�
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();    //�����۽�ũ��Ʈ����
                int weaponIndex = item.value;                   // �������� ��������
                hasWeapon[weaponIndex] = true;                  // hasWeapon�� �迭�ǰ��� true�� ������ش�.
                Destroy(nearObject);
                if (weapons[0] != null && weapons[1] != null)   //weapons�� 0��ĭ, 1��ĭ�� null�� �ƴ϶��  
                {
                    hasWeapon[weaponIndex] = false;
                    weapons[equipWeaponIndex] = WeaponList[weaponIndex];   //�տ� ���Ⱑ ��� ���� ��, ��������Ʈ���� ���� ��ü
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
                Debug.Log($"{nearObject.name}�� �Ծ����ϴ�.");
            }
        }
    }

    private void OnShow()
    {
        //if (sDown1 && (!hasWeapon[0] || equipWeaponIndex == 0))     //1�����⿡      
        //    return;
        //if (sDown2 && (!hasWeapon[1] || equipWeaponIndex == 1))
        //    return;


        int weaponIndex = -1;
        if (sDown1) weaponIndex = (int)equipWeapon.type;

        if (sDown2) weaponIndex = (int)equipWeapon.type;

        if (sDown1 || sDown2)   // 1���� ������ ��
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
