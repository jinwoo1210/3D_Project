using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Gun
{
    // TODO 벽 관련하여 추가작업 필요
    // 벽까지 레이쏴보기.
    public override void Init()
    {
        ammoRemain = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SMG].magCapacityLevel].magCapacity * 3;
        magCapacity = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SMG].magCapacityLevel].magCapacity;
        magAmmo = magCapacity;
        curDamage = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SMG].damageLevel].damage;
        curShootSpeed = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SMG].shootSpeedLevel].shootSpeed;
        curFireDistance = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SMG].fireDistanceLevel].fireDistance;
        curReloadSpeed = gunData.gunLevelData[PlayerStatManager.Inventory.gunStatLevel[(int)WeaponType.SMG].reloadLevel].reloadTime;
    }
}
