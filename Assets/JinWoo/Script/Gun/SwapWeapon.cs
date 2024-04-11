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
    public int pickGun;     // 주우려는 아이템

    public void Swap()
    {
        Item item = player.nearObject.GetComponent<Item>();
        int index = item.value;
        
        //if(player.equipWeaponIndex == -1)
           
            // 현재 들고 있는 총이 0 (AK) 라면?
        if (player.equipWeaponIndex == 0)
        {
            player.hasWeapon[0] = false;    //Ak의 hasWeapon을 false로 바꾸고
            player.weapons[0] = player.weapons[index];          //Ak의 weapons순서를 맨뒤로 바꾼뒤.
            player.weapons[index] = player.weapons[0];         // 교체할 총의 위치를 Ak가 있던 자리로 바꾼후
            player.hasWeapon[index] = true; // 교체되는 총의 hasWeapon을 true로 바꾼다.
        }
        else if(player.equipWeaponIndex == 1)
        {
            player.hasWeapon[1] = false;
            player.weapons[1] = player.weapons[index];          //Ak의 weapons순서를 맨뒤로 바꾼뒤.
            player.weapons[index] = player.weapons[1];         // 교체할 총의 위치를 Ak가 있던 자리로 바꾼후
            player.hasWeapon[index] = true;
        }
        else if(player.equipWeaponIndex == 2)
        {
            player.hasWeapon[2] = false;
            player.weapons[2] = player.weapons[index];          //Ak의 weapons순서를 맨뒤로 바꾼뒤.
            player.weapons[index] = player.weapons[2];         // 교체할 총의 위치를 Ak가 있던 자리로 바꾼후
            player.hasWeapon[index] = true;
        }
        else if(player.equipWeaponIndex == 3)
        {
            player.hasWeapon[3] = false;
            player.weapons[3] = player.weapons[index];          //Ak의 weapons순서를 맨뒤로 바꾼뒤.
            player.weapons[index] = player.weapons[3];         // 교체할 총의 위치를 Ak가 있던 자리로 바꾼후
            player.hasWeapon[index] = true;
        }
    }
}
