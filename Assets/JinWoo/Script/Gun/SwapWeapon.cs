using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class SwapWeapon : MonoBehaviour
{
    public enum GunType { AR, SMG, SG, SR}

    [SerializeField] GunType type;
    public Player player;
    public Item item;
    public int index;
    public int pickGun;     // �ֿ���� ������

    public void Swap()
    {
        Item item = player.nearObject.GetComponent<Item>();
        int index = item.value;
        
        //if(player.equipWeaponIndex == -1)
           
            // ���� ��� �ִ� ���� 0 (AK) ���?
        if (player.equipWeaponIndex == 0)
        {
            player.hasWeapon[0] = false;    //Ak�� hasWeapon�� false�� �ٲٰ�
            player.weapons[0] = player.weapons[index];          //Ak�� weapons������ �ǵڷ� �ٲ۵�.
            player.weapons[index] = player.weapons[0];         // ��ü�� ���� ��ġ�� Ak�� �ִ� �ڸ��� �ٲ���
            player.hasWeapon[index] = true; // ��ü�Ǵ� ���� hasWeapon�� true�� �ٲ۴�.
        }
        else if(player.equipWeaponIndex == 1)
        {
            player.hasWeapon[1] = false;
            player.weapons[1] = player.weapons[index];          //Ak�� weapons������ �ǵڷ� �ٲ۵�.
            player.weapons[index] = player.weapons[1];         // ��ü�� ���� ��ġ�� Ak�� �ִ� �ڸ��� �ٲ���
            player.hasWeapon[index] = true;
        }
        else if(player.equipWeaponIndex == 2)
        {
            player.hasWeapon[2] = false;
            player.weapons[2] = player.weapons[index];          //Ak�� weapons������ �ǵڷ� �ٲ۵�.
            player.weapons[index] = player.weapons[2];         // ��ü�� ���� ��ġ�� Ak�� �ִ� �ڸ��� �ٲ���
            player.hasWeapon[index] = true;
        }
        else if(player.equipWeaponIndex == 3)
        {
            player.hasWeapon[3] = false;
            player.weapons[3] = player.weapons[index];          //Ak�� weapons������ �ǵڷ� �ٲ۵�.
            player.weapons[index] = player.weapons[3];         // ��ü�� ���� ��ġ�� Ak�� �ִ� �ڸ��� �ٲ���
            player.hasWeapon[index] = true;
        }
    }
}
